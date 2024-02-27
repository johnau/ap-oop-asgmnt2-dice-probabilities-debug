using System.Diagnostics;

namespace DiceProbabilitiesDebug;

/// <summary>
/// Stage 3B
/// Dynamic Programming
/// --------------------
/// Avoid recalculating already calculated solutions
/// -----
/// Steps
/// 1. Define all base cases
/// 2. Recurring subproblem
/// 3. Memoize
/// </summary>
/// <param name="numberOfDice"></param>
/// <param name="faces"></param>
public class DiceProbabilities_Stage3_B
{
    protected readonly TableLogger RcLog = new();
    protected readonly int numberOfDice;
    protected readonly int faces;
    protected readonly int totalCombinations;

    // Step 3. Memoization data structure
    private Dictionary<(int, int), ulong> memoization; // Memoization is like Memorization - But nerds will get angry if you say Memorization => that is for the brain

    public DiceProbabilities_Stage3_B(int numberOfDice, int faces = 6)
    {
        this.numberOfDice = numberOfDice;
        this.faces = faces;
        totalCombinations = (int)Math.Pow(faces, numberOfDice);
        memoization = new Dictionary<(int, int), ulong>();
    }

    public Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        Dictionary<int, double> probabilities = new Dictionary<int, double>();

        for (int targetValue = numberOfDice; targetValue <= numberOfDice * faces; targetValue++)
        {
            var combinations = DiceCombinationsForTargetValue_Memiozation(numberOfDice, targetValue);
            probabilities.Add(targetValue, combinations / (double)totalCombinations);
        }

        return probabilities;
    }

    /// <summary>
    /// Dynamic Programming, Top-down with Memoization
    /// - Avoid recalculating subproblems by storing subproblem results in the memoization dictionary
    /// </summary>
    /// <param name="die"></param>
    /// <param name="targetValue"></param>
    /// <returns></returns>
    ulong DiceCombinationsForTargetValue_Memiozation(int die, int targetValue)
    {
        // Step 1: Define all Base Cases for recursion - Check this subproblem should continue
        //      a) No dice left and target reached   --- SUCCESS
        //      b) Target value exceeded             --- FAIL
        //      c) Avoid double processing           --- Return previous resuls
        // Step 2: Recurring subproblem - Iterate face values, subtract away current face value from the target and move to next die
        // Step 3: Memoize!

        if (die == 0 && targetValue == 0) return 1;
        if (die == 0 && targetValue != 0) return 0;
        if (targetValue <= 0) return 0;
        if (memoization.ContainsKey((die, targetValue))) return memoization[(die, targetValue)];

        ulong count = 0;
        for (int value = 1; value <= faces; value++)
        {
            count += DiceCombinationsForTargetValue_Memiozation(die - 1, targetValue - value);
        }

        memoization[(die, targetValue)] = count;
        return count;
    }
}


