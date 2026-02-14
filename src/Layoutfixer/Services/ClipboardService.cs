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
            if (System.Windows.Clipboard.ContainsText()) result = System.Windows.Clipboard.GetText();
        });
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
        t.Join();
        return result;
    }

    public void SetText(string text)
    {
        var t = new Thread(() => System.Windows.Clipboard.SetText(text));
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
        t.Join();
    }
}
