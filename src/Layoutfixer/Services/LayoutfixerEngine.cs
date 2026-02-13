using Layoutfixer.Models;
using System;

namespace Layoutfixer.Services;

public class LayoutfixerEngine
{
    private readonly KeyLayoutConverter _converter;

    public LayoutfixerEngine()
    {
        _converter = new KeyLayoutConverter();
    }

    public string Convert(string input, Settings settings)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        if (settings.EnableEnHe)
        {
            var outHe = _converter.ConvertEnHe(input);
            if (!string.Equals(outHe, input, StringComparison.Ordinal)) return outHe;
        }

        if (settings.EnableEnRu)
        {
            var outRu = _converter.ConvertEnRu(input);
            if (!string.Equals(outRu, input, StringComparison.Ordinal)) return outRu;
        }

        return input;
    }
}
