using System.Threading;
using System.Windows;

namespace Layoutfixer.Services;

public class ClipboardService
{
    public string? GetText()
    {
        string? result = null;
        var t = new Thread(() =>
        {
            if (Clipboard.ContainsText()) result = Clipboard.GetText();
        });
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
        t.Join();
        return result;
    }

    public void SetText(string text)
    {
        var t = new Thread(() => Clipboard.SetText(text));
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
        t.Join();
    }
}
