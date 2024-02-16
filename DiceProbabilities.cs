using System.Collections.Generic;
using System.Diagnostics;

namespace DiceProbabilitiesDebug;

public static class DiceProbabilities
{
    public static Dictionary<int, Double> CalculateProbabilitiesForNumberOfDice(int n)
    {
        Dictionary<int, int> rollCombinations = new Dictionary<int, int>(); 
        // int mn = n; // replace mn in code with n
        int mx = n * 6;
        for (int i = n; i <= mx; i++) // populate dict with keys from number of dice to number of dice times the sides (6)
        {
            rollCombinations[i] = 0;
        }
        Log(rollCombinations, "Combinations", 2);
        
        int[] dice = new int[n]; // new array sized at number of dice.
        for (int i = 0; i < n; i++)
        {
            dice[i] = 1;
        }
        Log(dice, "Dice");

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

        Dictionary<int, Double> rollProbabilities = new Dictionary<int, double>();
        Double total2 = Math.Pow(6.0, (Double)n);
        Console.WriteLine($"Total combinations: 6^{n}={total2}");
        Console.WriteLine($"Process RollCalc dictionary for {n} dice (from {n} to {mx}) into roll probablities dict");
        for (int i = n; i <= mx; i++)
        {
            Console.WriteLine($"Roll calc [{i}]={rollCombinations[i]}");
            Console.WriteLine($"Roll divided by Total combinations = {(Double)rollCombinations[i] / total2}");
            rollProbabilities[i] = (Double)rollCombinations[i] / total2;
            Console.WriteLine($"Roll probability for [{i}] = {(Double)rollCombinations[i] / total2}");
        }
        return rollProbabilities;
    }

    private static void Log(Dictionary<int, int> dict, string name = "A", int indent = 0)
    {
        var _indent = string.Concat(Enumerable.Repeat("\t", indent));
        var s = string.Join("\t", dict.Select(pair => $"\"{pair.Key}\""));
        var s1 = string.Join("\t", dict.Select(pair => $"{pair.Value}"));
        Console.WriteLine($"{_indent}{name} dict:");
        Console.WriteLine($"{_indent}: [{s}]");
        Console.WriteLine($"{_indent}: [{s1}]");
    }

    private static void Log(int[] arr, string name = "A", int indent = 0)
    {
        var _indent = string.Concat(Enumerable.Repeat("\t", indent));
        var s = string.Join("\t", arr.Select((value, index) => $"D{index+1}"));
        var s1 = string.Join("\t", arr.Select((value, index) => $"{value}"));
        Console.WriteLine($"{_indent}{name} arr:");
        Console.WriteLine($"{_indent}: {s}");
        Console.WriteLine($"{_indent}: {s1}");

    }
}
