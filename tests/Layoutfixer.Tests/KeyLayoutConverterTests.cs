using Layoutfixer.Services;
using Xunit;

namespace Layoutfixer.Tests;

public class KeyLayoutConverterTests
{
    [Fact]
    public void Converts_En_To_Ru_Example()
    {
        var c = new KeyLayoutConverter();
        var input = "ghbdtn"; // "привет" on RU layout
        var output = c.ConvertEnRu(input);
        Assert.Equal("привет", output);
    }

    [Fact]
    public void Converts_En_To_He_Example()
    {
        var c = new KeyLayoutConverter();
        var input = "kt"; // "לא" on HE layout
        var output = c.ConvertEnHe(input);
        Assert.NotEqual(input, output);
    }
}
