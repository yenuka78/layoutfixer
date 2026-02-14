using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Layoutfixer.Services;

public class TrayIcon : IDisposable
{
    private readonly NotifyIcon _notifyIcon;
    private readonly Window _mainWindow;

    public TrayIcon(Window mainWindow)
    {
        _mainWindow = mainWindow;
        _notifyIcon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Visible = true,
            Text = "Layoutfixer"
        };

        var menu = new ContextMenuStrip();
        menu.Items.Add("Open", null, (_, _) => Show());
        menu.Items.Add("Exit", null, (_, _) => Exit());
        _notifyIcon.ContextMenuStrip = menu;
        _notifyIcon.DoubleClick += (_, _) => Show();
    }

    private void Show()
    {
        _mainWindow.Show();
        _mainWindow.WindowState = WindowState.Normal;
        _mainWindow.Activate();
    }

    private void Exit()
    {
        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        System.Windows.Application.Current.Shutdown();
    }

    public void Dispose() => _notifyIcon.Dispose();
}
