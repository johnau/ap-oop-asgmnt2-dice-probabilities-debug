namespace DiceProbabilitiesDebug;

/// <summary>
/// This first stage has involved renaming + simple refactoring (no drastic changes to code structure)
/// At this point, after being able to clearly visualize what is happening (through the 'pretty' table logger), some refactoring is required (see Stage_2)
/// 1. Combinations are calculated by looping over number of dice AND number of faces
/// 2. Each combinations count is divided by the total number of possible combinations (which is 6^number of dice, 
///     ie, 1 die: 6^1=6 possible results, 2 die: 6^2=36 posible results), therefor there should be as many loop iterations
/// </summary>
/// <param name="numberOfDice"></param>
public class DiceProbabilities_Stage1(int numberOfDice)
{
    private readonly TableLogger RcLog = new();

    public Dictionary<int, Double> CalculateProbabilitiesForNumberOfDice()
    {
        var probabilities = new Dictionary<int, double>();
        var totalCombinations = Math.Pow(6.0, (Double)numberOfDice);


        var combinations = CalculateCombinations();
        Console.WriteLine($"{numberOfDice} dice combinations:");
        RcLog.Log();

        //return CalculateProbabilities(combinations);
        for (int i = numberOfDice; i <= numberOfDice * 6; i++)
        {
            Console.WriteLine($"Combinations for value {i} = ({combinations[i]} of {totalCombinations})");
            probabilities[i] = (Double)combinations[i] / totalCombinations;
            Console.WriteLine($"% [{i}] = {(Double)combinations[i] / totalCombinations * 100:F2}%");
        }
        return probabilities;
    }

    private Dictionary<int, int> CalculateCombinations()
    {
        var dice = Enumerable.Repeat(1, numberOfDice).ToArray();
        var combinations = Enumerable.Range(numberOfDice, numberOfDice * 6 - numberOfDice + 1).ToDictionary(key => key, value => 0);
        Log(combinations, "Combos");
        RcLog.SetHeaders(combinations.Keys.Select(k => k.ToString()).ToArray());

        bool finished1 = false;
        while (!finished1) // this keeps going until the die are all spent (ie , 2 die => 6, 6, 3 die => 6, 6, 6)
        {
            // dice starts at 1 and then increments at end of each loop, each loop is the next increment of face value
            Log(dice, "Dice value", 3, false); 
            int total = dice.Sum();
            Console.WriteLine($"\t[Value={total}]");

            combinations[total] += 1;       // add 1 more possible roll that will sum to the total value

            int i = 0;
            bool finished2 = false;

            // This while loop iterates over each die, and tries each combination of values,eg. 3 dice ->  (1, 1, 1), (2, 1, 1), (3, 1, 1) ..... (5, 6, 6), (6, 6, 6)
            // Each of the combinations is summed to its total value (1, 1, 1) => 3
            while (!finished2)
            {
                dice[i] += 1;
                if (dice[i] <= 6) // inner loop does not get entered until all sides run out (exceeded 6)
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

    //private Dictionary<int, Double> CalculateProbabilities(Dictionary<int, int> combinations)
    //{
    //    var probabilities = new Dictionary<int, double>();
    //    var totalCombinations = Math.Pow(6.0, (Double)numberOfDice);

    //    for (int i = numberOfDice; i <= numberOfDice*6; i++)
    //    {
    //        Console.WriteLine($"Combinations for value {i} = ({combinations[i]} of {totalCombinations})");
    //        probabilities[i] = (Double)combinations[i] / totalCombinations;
    //        Console.WriteLine($"% [{i}] = {(Double)combinations[i] / totalCombinations*100:F2}%");
    //    }
    //    return probabilities;
    //}

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
