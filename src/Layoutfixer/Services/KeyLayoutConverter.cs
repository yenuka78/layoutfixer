using System.Collections.Generic;
using System.Text;

namespace Layoutfixer.Services;

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

    public string ConvertEnHe(string input)
    {
        return Convert(input, _enToHe, _heToEn);
    }

    public string ConvertEnRu(string input)
    {
        return Convert(input, _enToRu, _ruToEn);
    }

    private static string Convert(string input, Dictionary<char, char> forward, Dictionary<char, char> reverse)
    {
        var sb = new StringBuilder(input.Length);
        foreach (var ch in input)
        {
            if (forward.TryGetValue(ch, out var mapped)) sb.Append(mapped);
            else if (reverse.TryGetValue(ch, out var mapped2)) sb.Append(mapped2);
            else sb.Append(ch);
        }
        return sb.ToString();
    }

    private static Dictionary<char, char> BuildEnToHe()
    {
        // US QWERTY -> Hebrew layout (common mapping)
        return new Dictionary<char, char>
        {
            // letters
            ['q']='/', ['w']='\'', ['e']='ק', ['r']='ר', ['t']='א', ['y']='ט', ['u']='ו', ['i']='ן', ['o']='ם', ['p']='פ',
            ['a']='ש', ['s']='ד', ['d']='ג', ['f']='כ', ['g']='ע', ['h']='י', ['j']='ח', ['k']='ל', ['l']='ך',
            ['z']='ז', ['x']='ס', ['c']='ב', ['v']='ה', ['b']='נ', ['n']='מ', ['m']='צ',
            // punctuation keys
            [';']='ף', ['=']='+', ['-']='-', ['[']=']', [']']='[', ['\\']='\\', [',']='ת', ['.']='ץ', ['/']='/'
        };
    }

    private static Dictionary<char, char> BuildEnToRu()
    {
        // US QWERTY -> Russian layout (common mapping)
        return new Dictionary<char, char>
        {
            ['q']='й', ['w']='ц', ['e']='у', ['r']='к', ['t']='е', ['y']='н', ['u']='г', ['i']='ш', ['o']='щ', ['p']='з',
            ['[']='х', [']']='ъ',
            ['a']='ф', ['s']='ы', ['d']='в', ['f']='а', ['g']='п', ['h']='р', ['j']='о', ['k']='л', ['l']='д', [';']='ж', ['\'']='э',
            ['z']='я', ['x']='ч', ['c']='с', ['v']='м', ['b']='и', ['n']='т', ['m']='ь', [',']='б', ['.']='ю', ['/']='.'
        };
    }

    private static Dictionary<char, char> Invert(Dictionary<char, char> map)
    {
        var inv = new Dictionary<char, char>();
        foreach (var kv in map)
        {
            if (!inv.ContainsKey(kv.Value)) inv[kv.Value] = kv.Key;
        }
        return inv;
    }
}
