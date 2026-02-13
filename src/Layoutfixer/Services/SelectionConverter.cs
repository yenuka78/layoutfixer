using Layoutfixer.Models;
using System.Threading;

namespace Layoutfixer.Services;

public class SelectionConverter
{
    private readonly ClipboardService _clipboard;
    private readonly LayoutfixerEngine _engine;

    public SelectionConverter()
    {
        _clipboard = new ClipboardService();
        _engine = new LayoutfixerEngine();
    }

    public void ConvertSelection(Settings settings)
    {
        // Copy selection
        InputSimulator.SendCtrlC();
        Thread.Sleep(50);

        var text = _clipboard.GetText();
        if (string.IsNullOrEmpty(text)) return;

        var converted = _engine.Convert(text, settings);
        if (converted == text) return;

        _clipboard.SetText(converted);
        Thread.Sleep(30);
        InputSimulator.SendCtrlV();
    }

    public void ConvertLastWord(Settings settings)
    {
        // Select previous word: Ctrl+Shift+Left
        InputSimulator.SendCtrlShiftLeft();
        Thread.Sleep(40);
        ConvertSelection(settings);
    }
}
