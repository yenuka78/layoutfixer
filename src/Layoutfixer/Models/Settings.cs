namespace Layoutfixer.Models;

public class Settings
{
    public string SelectionHotkey { get; set; } = "Ctrl+Alt+L";
    public string LastWordHotkey { get; set; } = "Ctrl+Alt+K";
    public bool EnableEnHe { get; set; } = true;
    public bool EnableEnRu { get; set; } = true;
    public bool StartWithWindows { get; set; } = false;
    public bool PauseInFullscreen { get; set; } = true;
}
