namespace DiceProbabilitiesDebug;

public class TableLogger
{
    private string[] _headers = [];
    private List<ResultRow> _results = [];
    
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
        var _ = _headers.Length * 5 + 15;
        DrawHorizontalBorder(_);
        DrawHeader();
        DrawHorizontalBorder(_);

        var maxValue = _results.Last().Results.Max();
        var intervalSize = (double)maxValue / 13;

        for (int i = 0; i < _results.Count; i++) 
        {
            var row = _results[i];

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"|#{i+1,4}: {row.Key,3} >>|");
            Console.ResetColor();
            
            for (int j = 0; j < row.Results.Length; j++)
            {
                var r = row.Results[j];
                int color;
                if (r == maxValue)
                {
                    color = (int) ConsoleColor.White;
                } else
                {
                    color = (int)Math.Ceiling(r / intervalSize);
                    color = ColorMap(Math.Max(1, Math.Min(color, 13))); // map the color value to a better color value for the resultant visualization
                }

                Console.ForegroundColor = (ConsoleColor) color;
                Console.Write($"{row.Results[j],3} ");
                DrawDivider();
                Console.ResetColor();
            }
            DrawHorizontalBorder(_);
        }
    }

    private static void DrawHorizontalBorder(int length)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("\n" + new string('-', length));
        Console.ResetColor();
    }

    private static void DrawDivider()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("|");
        Console.ResetColor();
    }

    private void DrawHeader()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("|" + new string(' ', 13) + "|");
        for (int i = 0; i < _headers.Length; i++)
        {
            Console.Write($"{_headers[i],3} |");
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Fix ugly color ordering of ConsoleColor enum (can't be bothered going to a console prettifier library at this point
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private int ColorMap(int color)
    {
        //public enum ConsoleColor
        //{
        //    Gray = 7,
        //    DarkGray = 8,
        //    Black = 0,

        //    White = 15
        //    Yellow = 14,
        //    DarkYellow = 6,
        //    Cyan = 11,
        //    DarkCyan = 3,
        //    Green = 10,
        //    DarkGreen = 2,
        //    DarkBlue = 1,
        //    Blue = 9,
        //    DarkMagenta = 5,
        //    Magenta = 13,
        //    Red = 12,
        //    DarkRed = 4,

        if (color == 1) return 15;
        if (color == 2) return 14;
        if (color == 3) return 6;
        if (color == 4) return 11;
        if (color == 5) return 3;
        if (color == 6) return 10;
        if (color == 7) return 2;
        if (color == 8) return 1;
        if (color == 9) return 9;
        if (color == 10) return 5;
        if (color == 11) return 13;
        if (color == 12) return 12;
        if (color == 13) return 4;

        return color;
    }
}

internal record ResultRow(int Key, int[] Results) { }
