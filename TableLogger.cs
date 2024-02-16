using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiceProbabilitiesDebug;

internal class TableLogger
{
    public string[] _headers = [];
    public List<ResultRow> _results = [];
    
    public TableLogger()
    {
    }

    public void SetHeaders(string[] headers)
    {
        _headers = headers;
    }

    public void AddResultRow(int key, int[] values)
    {
        _results.Add(new ResultRow(key, values));
    }

    public void Log()
    {
        int _ = _headers.Length * 5 + 11;
        Console.WriteLine(new string('-', _));
        Console.Write("|" + new string(' ', 9) + "|");
        for (int i = 0; i < _headers.Length; i++)
        {
            Console.Write($"{_headers[i],3} |");
        }
        Console.WriteLine("\n" + new string('-', _));

        int maxValue = _results.Last().Results.Max();

        foreach (var row in _results)
        {
            Console.Write($"#: {row.Key,3} >> |");
            for (int i = 0; i < row.Results.Length; i++)
            {
                var r = row.Results[i];
                double intervalSize = (double)maxValue / 13;
                int color = (int)Math.Ceiling(r / intervalSize);
                color = ColorMap(Math.Max(1, Math.Min(color, 13)));

                Console.ForegroundColor = (ConsoleColor) color;
                Console.Write($"{row.Results[i],3} |");
                Console.ResetColor();
            }
            Console.WriteLine("\n" + new string('-', _));
        }
    }

    /// <summary>
    /// Fix ugly color ordering of ConsoleColor enum (can't be bothered going to a console prettifier library at this point
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private int ColorMap(int color)
    {
        if (color == 1) return 15;
        if (color == 2) return 14;
        if (color == 3) return 11;
        if (color == 4) return 6;
        if (color == 5) return 3;
        if (color == 6) return 10;
        if (color == 7) return 2;
        if (color == 8) return 9;
        if (color == 9) return 1;
        if (color == 10) return 5;
        if (color == 11) return 13;
        if (color == 12) return 12;
        if (color == 13) return 4;

        return color;
    }
}

internal record ResultRow(int Key, int[] Results) { }
