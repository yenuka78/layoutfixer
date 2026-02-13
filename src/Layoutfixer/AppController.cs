using Layoutfixer.Models;
using Layoutfixer.Services;
using System;
using System.Windows;

namespace Layoutfixer;

public class AppController : IDisposable
{
    private readonly SettingsStore _settingsStore = new();
    private Settings _settings;
    private readonly TrayIcon _tray;
    private HotkeyManager? _hotkeys;
    private int _selectionHotkeyId;
    private int _lastWordHotkeyId;
    private readonly SelectionConverter _converter = new();

    public AppController(Window mainWindow)
    {
        _settings = _settingsStore.Load();
        _tray = new TrayIcon(mainWindow);
    }

    public void Init(Window window)
    {
        var hwnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
        _hotkeys = new HotkeyManager(hwnd);
        _hotkeys.HotkeyPressed += OnHotkey;

        _selectionHotkeyId = _hotkeys.Register(Hotkey.ModCtrl | Hotkey.ModAlt, Hotkey.VkL);
        _lastWordHotkeyId = _hotkeys.Register(Hotkey.ModCtrl | Hotkey.ModAlt, Hotkey.VkK);
    }

    private void OnHotkey(int id)
    {
        _settings = _settingsStore.Load();
        if (id == _selectionHotkeyId) _converter.ConvertSelection(_settings);
        else if (id == _lastWordHotkeyId) _converter.ConvertLastWord(_settings);
    }

    public void Dispose()
    {
        if (_hotkeys != null)
        {
            _hotkeys.Unregister(_selectionHotkeyId);
            _hotkeys.Unregister(_lastWordHotkeyId);
            _hotkeys.Dispose();
        }
        _tray.Dispose();
    }
}

public static class Hotkey
{
    public const uint ModAlt = 0x0001;
    public const uint ModCtrl = 0x0002;
    public const uint VkL = 0x4C;
    public const uint VkK = 0x4B;
}
