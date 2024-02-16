using System.Collections.Generic;
using System.Diagnostics;

namespace DiceProbabilitiesDebug;

public static class DiceProbabilities
{
    public static Dictionary<int, Double> CalculateProbabilitiesForNumberOfDice(int n)
    {
        var rollCombinations = Enumerable.Range(n, n*6).ToDictionary(key => key, value => 0);
        var dice = Enumerable.Repeat(1, n).ToArray();

        CalculateCombinations(n, dice, ref rollCombinations);
        return CalculateProbabilities(n, rollCombinations);
    }

    private static void CalculateCombinations(int n, int[] dice, ref Dictionary<int, int> rollCombinations)
    {
        bool finished1 = false;
        while (!finished1)
        {
            //int total = 0;
            //foreach (int d in dice)
            //{
            //    total += d;
            //}
            int total = dice.Sum();
            Console.WriteLine($"\n\tCurrent: {total}");

            rollCombinations[total] += 1;

            int i = 0;
            bool finished2 = false;
            while (!finished2)
            {
                Log(dice, "Dice value", 2);
                dice[i] += 1;

                if (dice[i] <= 6)
                {
                    finished2 = true;
                }
                else
                {
                    if (i == n - 1)
                    {
                        finished1 = true;
                        finished2 = true;
                        Console.WriteLine($"\t    <XX> i={i}, n={n}");
                    }
                    else
                    {
                        dice[i] = 1;
                        Console.WriteLine($"\t    <reset dice[{i}]=1>");
                    }
                }
                i++;
            }
            Log(rollCombinations, "Combinations", 2);
            Log(dice, "Dice value", 2);
        }
    }

    private static Dictionary<int, Double> CalculateProbabilities(int n, Dictionary<int, int> rollCombinations)
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

    private static void Log(int[] arr, string name = "A", int indent = 0)
    {
        var _indent = string.Concat(Enumerable.Repeat("\t", indent));
        var s = string.Join("\t", arr.Select((value, index) => $"D{index+1}"));
        var s1 = string.Join("\t", arr.Select((value, index) => $"{value}"));
        Console.WriteLine($"{_indent}{name} arr:");
        Console.WriteLine($"{_indent}[{s}]");
        Console.WriteLine($"{_indent}[{s1}]");

    }
}
