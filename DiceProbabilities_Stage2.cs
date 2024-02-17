using System.Diagnostics;

namespace DiceProbabilitiesDebug;

/// <summary>
/// Stage 1 (other class)
/// ----------------------------------
/// This first stage has involved renaming + simple refactoring (no drastic changes to code structure)
/// At this point, after being able to clearly visualize what is happening (through the 'pretty' table logger), some refactoring is required (see Stage_2)
/// 1. Combinations are calculated by looping over number of dice AND number of faces
/// 2. Each combinations count is divided by the total number of possible combinations (which is 6^number of dice, 
///     ie, 1 die: 6^1=6 possible results, 2 die: 6^2=36 posible results), therefor there should be as many loop iterations
///     
/// Stage 2 (this class)
/// ----------------------------------
/// This stage will involve refactoring that will change the current code structure.
/// the original nested while loops iterated over dice across all faces.
/// it shifts a dice until value max
/// once it reaches value max, it shifts to the next dice, increments it, and then resets all previous dice to 1
/// this is repeated across all dice
/// 
/// wrote a nested loop structure that iteratest the die correctly but is still verbose
/// 
/// See classes Stage2_A thru D for refactoring stages
/// 
/// </summary>
/// <param name="numberOfDice"></param>
public class DiceProbabilities_Stage2(int numberOfDice, int faces = 6) // This constructor style is weird for a full class
{
    protected readonly TableLogger RcLog = new();
    protected readonly int numberOfDice = numberOfDice;
    protected readonly int faces = faces;
    protected readonly int totalCombinations = (int)Math.Pow(faces, numberOfDice);
    
    public virtual Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        var probabilities = new Dictionary<int, double>();

        var combinations = CalculateCombinations();
        Console.WriteLine($"{numberOfDice} dice combinations:");
        RcLog.Log();

        //return CalculateProbabilities(combinations);
        for (int i = numberOfDice; i <= numberOfDice * faces; i++)
        {
            Console.WriteLine($"Combinations for value {i} = ({combinations[i]} of {totalCombinations})");
            probabilities[i] = (Double)combinations[i] / totalCombinations;
            Console.WriteLine($"% [{i}] = {(Double)combinations[i] / totalCombinations * 100:F2}%");
        }
        return probabilities;
    }

    protected (int[] dice, Dictionary<int, int> combinations) SetupArrays()
    {
        var dice = Enumerable.Repeat(1, numberOfDice).ToArray();
        var combinations = Enumerable.Range(numberOfDice, numberOfDice * 6 - numberOfDice + 1).ToDictionary(key => key, value => 0);
        RcLog.SetHeaders(combinations.Keys.Select(k => k.ToString()).ToArray());

        return (dice, combinations);
    }

    /// <summary>
    /// Method copied over from Stage1 - unchanged
    /// </summary>
    /// <returns></returns>
    protected virtual Dictionary<int, int> CalculateCombinations()
    {
        (var dice, var combinations) = SetupArrays();
        //var dice = Enumerable.Repeat(1, numberOfDice).ToArray();
        //var combinations = Enumerable.Range(numberOfDice, numberOfDice * 6 - numberOfDice + 1).ToDictionary(key => key, value => 0);
        //Log(combinations, "Combos");
        //RcLog.SetHeaders(combinations.Keys.Select(k => k.ToString()).ToArray());

        bool finished1 = false;
        while (!finished1) // this keeps going until the die are all spent (ie , 2 die => 6, 6, 3 die => 6, 6, 6)
        {
            // dice starts at 1 and then increments at end of each loop, each loop is the next increment of face value
            int total = dice.Sum();
            Console.Write($"\t[Value={total}]");
            Log(dice, "Dice value", 1, false);

            combinations[total] += 1;       // add 1 more possible roll that will sum to the total value

            int i = 0;
            bool finished2 = false;

            // This while loop iterates over each die, and tries each combination of values,eg. 3 dice ->  (1, 1, 1), (2, 1, 1), (3, 1, 1) ..... (5, 6, 6), (6, 6, 6)
            // Each of the combinations is summed to its total value (1, 1, 1) => 3
            while (!finished2)
            {
                dice[i] += 1;
                if (dice[i] <= 6) // inner loop does not get entered until all sides run out
                {
                    finished2 = true;
                }
                else
                {
                    if (i == numberOfDice - 1) // both loops exit when the last die has exceeded 6
                    {
                        finished1 = true;
                        finished2 = true;
                        //Console.WriteLine($"\t    <XX> i={i}, n={numberOfDice}");
                    }
                    else
                    {
                        dice[i] = 1; // dice is reset each time to increment the next
                        //Console.WriteLine($"\t    <reset dice[{i}]=1>");
                    }
                }
                i++;
            }

            RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());
            //Log(rollCombinations, "Combinations", 2);
            //Log(dice, "Dice value", 2);
        }

        return combinations;
    }

    protected static void Log(Dictionary<int, int> dict, string name = "A", int indent = 0)
    {
        var _indent = string.Concat(Enumerable.Repeat("\t", indent));
        var s = string.Join("\t", dict.Select(pair => $"{pair.Key}"));
        var s1 = string.Join("\t", dict.Select(pair => $"{pair.Value}"));
        Console.WriteLine($"{_indent}{name} dict:");
        Console.WriteLine($"{_indent}[{s}]");
        Console.WriteLine($"{_indent}[{s1}]");
    }

    protected static void Log(int[] arr, string name = "A", int indent = 0, bool includeHeaders = true)
    {
        var _indent = string.Concat(Enumerable.Repeat("\t", indent));
        var s = string.Join(" ", arr.Select((value, index) => $"D{index+1, 2} "));
        var s1 = string.Join(" ", arr.Select((value, index) => $"{value, 2} "));
        Console.Write($"{_indent}{name} arr: ");
        if (includeHeaders) Console.WriteLine($"{_indent}[{s}]");
        Console.WriteLine($"{_indent}[{s1}]");
    }

}
