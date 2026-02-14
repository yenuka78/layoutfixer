using Layoutfixer.Core.Models;
using Layoutfixer.Core.Services;
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
        var originalClipboard = _clipboard.GetText();

        InputSimulator.SendCtrlC();
        Thread.Sleep(70);

        var selectedText = _clipboard.GetText();
        if (string.IsNullOrWhiteSpace(selectedText))
        {
            RestoreClipboard(originalClipboard);
            return;
        }

        var converted = _engine.Convert(selectedText, settings);
        if (converted == selectedText)
        {
            RestoreClipboard(originalClipboard);
            return;
        }

        _clipboard.SetText(converted);
        Thread.Sleep(40);
        InputSimulator.SendCtrlV();
        Thread.Sleep(40);

        RestoreClipboard(originalClipboard);
    }

    public void ConvertLastWord(Settings settings)
    {
        InputSimulator.SendCtrlShiftLeft();
        Thread.Sleep(50);
        ConvertSelection(settings);
    }

    private void RestoreClipboard(string? original)
    {
        if (original is null) return;
        _clipboard.SetText(original);
    }
}
