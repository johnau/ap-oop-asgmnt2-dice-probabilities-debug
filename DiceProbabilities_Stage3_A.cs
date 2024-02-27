namespace DiceProbabilitiesDebug;

/// <summary>
/// Stage 3A - Incorporating the dice and combinations populating arrays
/// This has ended up looking very inefficient based on the output => a lot of unneccessary processing (0 dice, 0 target value)
/// Need to dig a little deeper, but not sure if it is worth it, the other solutions are OK.
/// 
/// </summary>
/// <param name="numberOfDice"></param>
/// <param name="faces"></param>
public class DiceProbabilities_Stage3_A
{
    protected readonly TableLogger RcLog = new();
    protected readonly int numberOfDice;
    protected readonly int faces;
    protected readonly int totalCombinations;

    public DiceProbabilities_Stage3_A(int numberOfDice, int faces = 6)
    {
        this.numberOfDice = numberOfDice;
        this.faces = faces;
        totalCombinations = (int)Math.Pow(faces, numberOfDice);
    }

    public Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        var probabilities = new Dictionary<int, double>();
        var die = numberOfDice;
        for (int targetValue = numberOfDice; targetValue <= numberOfDice * faces; targetValue++)
        {
            var probabilities_array = new double[die + 1, targetValue + 1]; // define results array (+1 to avoid 0 indexes and confusion)

            // Quick solve for 1 die (avoid hitting array twice)
            for (int i = 1; i <= faces && i <= targetValue; i++)
            {
                probabilities_array[1, i] = 1.0 / faces;
            }

            // Solve 2+ dice
            for (int total = 1; total <= targetValue; total++)
            {
                for (int d = 2; d <= die; d++) 
                {
                    for (int value = 1; value <= faces && value < total; value++)
                    {
                        probabilities_array[d, total] += probabilities_array[d - 1, total - value] / faces;
                    }
                    Console.WriteLine($"{probabilities_array[d, total]}");
                }
            }

            probabilities.Add(targetValue, probabilities_array[numberOfDice, targetValue]);
        }

        return probabilities;
    }


}


