namespace DiceProbabilitiesDebug;

public class DiceProbabilities(int numberOfDice)
{
    private readonly TableLogger RcLog = new();

    public Dictionary<int, Double> CalculateProbabilitiesForNumberOfDice()
    {
        var rollCombinations = Enumerable.Range(numberOfDice, numberOfDice * 6- numberOfDice + 1).ToDictionary(key => key, value => 0);
        Log(rollCombinations, "Combos");
        RcLog.SetHeaders(rollCombinations.Keys.Select(k => k.ToString()).ToArray());
        var dice = Enumerable.Repeat(1, numberOfDice).ToArray();

        CalculateCombinations(dice, ref rollCombinations);

        Console.WriteLine($"{numberOfDice} dice combinations:");
        RcLog.Log();

        return CalculateProbabilities(numberOfDice, rollCombinations);
    }

    private void CalculateCombinations(int[] dice, ref Dictionary<int, int> rollCombinations)
    {
        bool finished1 = false;
        while (!finished1)
        {
            int total = dice.Sum();
            Console.WriteLine($"\t[Value={total}]");

            rollCombinations[total] += 1;

            int i = 0;
            bool finished2 = false;
            while (!finished2)
            {
                Log(dice, "Dice value", 3, false);
                dice[i] += 1;

                if (dice[i] <= 6)
                {
                    finished2 = true;
                }
                else
                {
                    if (i == numberOfDice - 1)
                    {
                        finished1 = true;
                        finished2 = true;
                        //Console.WriteLine($"\t    <XX> i={i}, n={numberOfDice}");
                    }
                    else
                    {
                        dice[i] = 1;
                        //Console.WriteLine($"\t    <reset dice[{i}]=1>");
                    }
                }
                i++;
            }
            //Log(rollCombinations, "Combinations", 2);
            RcLog.AddResultRow(total, rollCombinations.Values.Select(v => v).ToArray());
            //Log(dice, "Dice value", 2);
        }
    }

    private Dictionary<int, Double> CalculateProbabilities(int n, Dictionary<int, int> rollCombinations)
    {
        var probabilities = new Dictionary<int, double>();
        var totalCombinations = Math.Pow(6.0, (Double)n);

        for (int i = n; i <= n*6; i++)
        {
            Console.WriteLine($"Combinations for value {i} = ({rollCombinations[i]} of {totalCombinations})");
            probabilities[i] = (Double)rollCombinations[i] / totalCombinations;
            Console.WriteLine($"% [{i}] = {(Double)rollCombinations[i] / totalCombinations*100:F2}%");
        }
        return probabilities;
    }

    private static void Log(Dictionary<int, int> dict, string name = "A", int indent = 0)
    {
        var _indent = string.Concat(Enumerable.Repeat("\t", indent));
        var s = string.Join("\t", dict.Select(pair => $"{pair.Key}"));
        var s1 = string.Join("\t", dict.Select(pair => $"{pair.Value}"));
        Console.WriteLine($"{_indent}{name} dict:");
        Console.WriteLine($"{_indent}[{s}]");
        Console.WriteLine($"{_indent}[{s1}]");
    }

    private static void Log(int[] arr, string name = "A", int indent = 0, bool includeHeaders = true)
    {
        var _indent = string.Concat(Enumerable.Repeat("\t", indent));
        var s = string.Join("\t", arr.Select((value, index) => $"D{index+1}"));
        var s1 = string.Join("\t", arr.Select((value, index) => $"{value}"));
        Console.Write($"{_indent}{name} arr: ");
        if (includeHeaders) Console.WriteLine($"{_indent}[{s}]");
        Console.WriteLine($"{_indent}[{s1}]");
    }

}
