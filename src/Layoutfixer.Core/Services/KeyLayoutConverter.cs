using System.Globalization;
using System.Text;

namespace Layoutfixer.Core.Services;

public class KeyLayoutConverter
{
    private readonly Dictionary<char, char> _enToHe;
    private readonly Dictionary<char, char> _heToEn;
    private readonly Dictionary<char, char> _enToRu;
    private readonly Dictionary<char, char> _ruToEn;

    public KeyLayoutConverter()
    {
        _enToHe = BuildEnToHe();
        _heToEn = Invert(_enToHe);
        _enToRu = BuildEnToRu();
        _ruToEn = Invert(_enToRu);
    }

    public string ConvertEnHe(string input) => ConvertBidirectional(input, _enToHe, _heToEn);
    public string ConvertEnRu(string input) => ConvertBidirectional(input, _enToRu, _ruToEn);

    public string ConvertAuto(string input, bool enableEnHe, bool enableEnRu)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        var candidates = new List<string> { input };

        if (enableEnHe) candidates.Add(ConvertEnHe(input));
        if (enableEnRu) candidates.Add(ConvertEnRu(input));

        return candidates
            .OrderByDescending(ScoreReadable)
            .ThenBy(s => s.Length == input.Length ? 0 : 1)
            .First();
    }

    private static string ConvertBidirectional(string input, Dictionary<char, char> forward, Dictionary<char, char> reverse)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var forwardHits = CountHits(input, forward);
        var reverseHits = CountHits(input, reverse);
        var selected = reverseHits > forwardHits ? reverse : forward;

        return Map(input, selected);
    }

    private static int CountHits(string input, Dictionary<char, char> map)
        => input.Count(ch => map.ContainsKey(ch) || map.ContainsKey(char.ToLowerInvariant(ch)));

    private static string Map(string input, Dictionary<char, char> map)
    {
        var sb = new StringBuilder(input.Length);

        foreach (var ch in input)
        {
            if (map.TryGetValue(ch, out var mapped))
            {
                sb.Append(mapped);
                continue;
            }

            var lower = char.ToLowerInvariant(ch);
            if (map.TryGetValue(lower, out var mappedLower))
            {
                sb.Append(PreserveCase(ch, mappedLower));
            }
            else
            {
                sb.Append(ch);
            }
        }

        return sb.ToString();
    }

    private static char PreserveCase(char original, char mapped)
    {
        if (!char.IsLetter(original)) return mapped;
        if (!char.IsUpper(original)) return mapped;

        return char.ToUpper(mapped, CultureInfo.InvariantCulture);
    }

    private static int ScoreReadable(string value)
    {
        var score = 0;
        foreach (var ch in value)
        {
            if (char.IsWhiteSpace(ch) || char.IsPunctuation(ch)) score += 1;
            if (char.IsLetter(ch)) score += 3;
            if (ch is >= '\u0590' and <= '\u05FF') score += 3; // Hebrew block
            if (ch is >= '\u0400' and <= '\u04FF') score += 3; // Cyrillic block
        }
        return score;
    }

    private static Dictionary<char, char> BuildEnToHe() => new()
    {
        ['q'] = '/', ['w'] = '\'', ['e'] = 'ק', ['r'] = 'ר', ['t'] = 'א', ['y'] = 'ט', ['u'] = 'ו', ['i'] = 'ן', ['o'] = 'ם', ['p'] = 'פ',
        ['a'] = 'ש', ['s'] = 'ד', ['d'] = 'ג', ['f'] = 'כ', ['g'] = 'ע', ['h'] = 'י', ['j'] = 'ח', ['k'] = 'ל', ['l'] = 'ך', [';'] = 'ף', ['\''] = ',',
        ['z'] = 'ז', ['x'] = 'ס', ['c'] = 'ב', ['v'] = 'ה', ['b'] = 'נ', ['n'] = 'מ', ['m'] = 'צ', [','] = 'ת', ['.'] = 'ץ', ['/'] = '.',
        ['['] = ']', [']'] = '[', ['-'] = '-', ['='] = '='
    };

    private static Dictionary<char, char> BuildEnToRu() => new()
    {
        ['q'] = 'й', ['w'] = 'ц', ['e'] = 'у', ['r'] = 'к', ['t'] = 'е', ['y'] = 'н', ['u'] = 'г', ['i'] = 'ш', ['o'] = 'щ', ['p'] = 'з', ['['] = 'х', [']'] = 'ъ',
        ['a'] = 'ф', ['s'] = 'ы', ['d'] = 'в', ['f'] = 'а', ['g'] = 'п', ['h'] = 'р', ['j'] = 'о', ['k'] = 'л', ['l'] = 'д', [';'] = 'ж', ['\''] = 'э',
        ['z'] = 'я', ['x'] = 'ч', ['c'] = 'с', ['v'] = 'м', ['b'] = 'и', ['n'] = 'т', ['m'] = 'ь', [','] = 'б', ['.'] = 'ю', ['/'] = '.',
        ['`'] = 'ё'
    };

    private static Dictionary<char, char> Invert(Dictionary<char, char> map)
    {
        var inv = new Dictionary<char, char>();
        foreach (var (k, v) in map)
        {
            inv.TryAdd(v, k);
        }
        return inv;
    }
}
