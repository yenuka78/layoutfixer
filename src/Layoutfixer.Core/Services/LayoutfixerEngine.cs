using Layoutfixer.Core.Models;

namespace Layoutfixer.Core.Services;

public class LayoutfixerEngine
{
    private readonly KeyLayoutConverter _converter = new();

    public string Convert(string input, Settings settings)
        => _converter.ConvertAuto(input, settings.EnableEnHe, settings.EnableEnRu);
}
