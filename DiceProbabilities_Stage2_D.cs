﻿namespace DiceProbabilitiesDebug;

public class DiceProbabilities_Stage2_D(int numberOfDice, int faces = 6) : DiceProbabilities_Stage2(numberOfDice, faces)
{

    public override Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        Console.WriteLine($"Stage 2_D version of CalculateProbabilitiesForNumberOfDice()");

        var combinations = CalculateCombinations();

        Console.WriteLine($"{numberOfDice} dice combinations:");
        RcLog.Log();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"Total Combinations: {totalCombinations}");
        Console.ResetColor();

        return combinations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / totalCombinations);
    }

    protected override Dictionary<int, int> CalculateCombinations()
    {
        (var dice, var combinations) = SetupArrays();

        Log(dice, "XX Dice", 1, false);
        combinations[numberOfDice] = 1; // set initial value to 1, could also set maxValue to 1 and flip the contents of the while loop?

        var total = dice.Sum();
        RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());

        while (total != faces * numberOfDice)
        {
            for (int i = 0; i < numberOfDice; i++)
            {
                if (dice[i] == faces)
                {
                    dice[i] = 1;
                }
                else
                {
                    dice[i]++;
                    break;
                }
            }

            Log(dice, "XX Dice", 1, false);
            total = dice.Sum();
            combinations[total]++;
            RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());
        }

        return combinations;
    }

}
