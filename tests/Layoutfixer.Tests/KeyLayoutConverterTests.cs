using Layoutfixer.Core.Models;
using Layoutfixer.Core.Services;
using Xunit;

namespace Layoutfixer.Tests;

public class KeyLayoutConverterTests
{
    [Fact]
    public void Converts_En_To_Ru_Example()
    {
        var c = new KeyLayoutConverter();
        var output = c.ConvertEnRu("ghbdtn");
        Assert.Equal("привет", output);
    }

    [Fact]
    public void Converts_Ru_To_En_Example()
    {
        var c = new KeyLayoutConverter();
        var output = c.ConvertEnRu("привет");
        Assert.Equal("ghbdtn", output);
    }

    [Fact]
    public void Converts_En_To_He_Example()
    {
        var c = new KeyLayoutConverter();
        var output = c.ConvertEnHe("akuo");
        Assert.Equal("שלום", output);
    }

    [Fact]
    public void Auto_Chooses_Best_Mapping()
    {
        var c = new KeyLayoutConverter();
        var output = c.ConvertAuto("привет", enableEnHe: true, enableEnRu: true);
        Assert.Equal("ghbdtn", output);
    }

    [Fact]
    public void Engine_Leaves_Unchanged_When_No_Pairs_Enabled()
    {
        var engine = new LayoutfixerEngine();
        var settings = new Settings { EnableEnHe = false, EnableEnRu = false };
        var output = engine.Convert("hello", settings);
        Assert.Equal("hello", output);
    }
}
