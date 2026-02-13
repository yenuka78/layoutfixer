using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Layoutfixer.Services;

public class HotkeyManager : IDisposable
{
    public event Action<int>? HotkeyPressed;

    private readonly HwndSource _source;
    private int _currentId = 9000;

    public HotkeyManager(IntPtr hwnd)
    {
        _source = HwndSource.FromHwnd(hwnd) ?? throw new InvalidOperationException("No hwnd source");
        _source.AddHook(WndProc);
    }

    public int Register(uint modifiers, uint key)
    {
        var id = ++_currentId;
        if (!RegisterHotKey(_source.Handle, id, modifiers, key))
            throw new InvalidOperationException("Unable to register hotkey");
        return id;
    }

    public void Unregister(int id) => UnregisterHotKey(_source.Handle, id);

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        const int WM_HOTKEY = 0x0312;
        if (msg == WM_HOTKEY)
        {
            HotkeyPressed?.Invoke(wParam.ToInt32());
            handled = true;
        }
        return IntPtr.Zero;
    }

    public void Dispose() => _source.RemoveHook(WndProc);

    [DllImport("user32.dll")] private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
    [DllImport("user32.dll")] private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
}
