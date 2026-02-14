using Layoutfixer.Core.Models;
using Layoutfixer.Services;
using System;
using System.Windows;

namespace Layoutfixer;

public partial class MainWindow : Window
{
    private readonly SettingsStore _settingsStore;
    private Settings _settings;

    public MainWindow()
    {
        InitializeComponent();
        _settingsStore = new SettingsStore();
        _settings = _settingsStore.Load();

        SelectionHotkeyText.Text = _settings.SelectionHotkey;
        LastWordHotkeyText.Text = _settings.LastWordHotkey;
        LangEnHe.IsChecked = _settings.EnableEnHe;
        LangEnRu.IsChecked = _settings.EnableEnRu;
        StartWithWindows.IsChecked = _settings.StartWithWindows;
        PauseInGames.IsChecked = _settings.PauseInFullscreen;

        SaveButton.Click += (_, _) => Save();
        CloseButton.Click += (_, _) => Close();

        Loaded += (_, _) => Hide(); // start minimized to tray
    }

    private void Save()
    {
        _settings.SelectionHotkey = SelectionHotkeyText.Text;
        _settings.LastWordHotkey = LastWordHotkeyText.Text;
        _settings.EnableEnHe = LangEnHe.IsChecked == true;
        _settings.EnableEnRu = LangEnRu.IsChecked == true;
        _settings.StartWithWindows = StartWithWindows.IsChecked == true;
        _settings.PauseInFullscreen = PauseInGames.IsChecked == true;

        _settingsStore.Save(_settings);
        MessageBox.Show("Saved", "Layoutfixer", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
