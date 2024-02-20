namespace DiceProbabilitiesDebug;

/// <summary>
/// Stage 3A
/// Integrating the dice and combinations collections into the mix
/// </summary>
/// <param name="numberOfDice"></param>
/// <param name="faces"></param>
public class DiceProbabilities_Stage3_A(int numberOfDice, int faces = 6)
{
    protected readonly TableLogger RcLog = new();
    protected readonly int numberOfDice = numberOfDice;
    protected readonly int faces = faces;
    protected readonly int totalCombinations = (int)Math.Pow(faces, numberOfDice);

    public Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        var probabilities = new Dictionary<int, double>();
        var dice = Enumerable.Repeat(1, numberOfDice).ToArray();
        //var combinations = Enumerable.Range(numberOfDice, numberOfDice * 6 - numberOfDice + 1).ToDictionary(key => key, value => 0); // remove this loop and integrate to the main loops
        var combinations = Enumerable.Repeat(0, numberOfDice * 6).ToArray();

        for (int rollValue = numberOfDice; rollValue <= numberOfDice * 6; rollValue++)
        {
            var total = dice.Sum();
            combinations[total] += 1;
            RcLog.AddResultRow(total, combinations.Select(v => v).ToArray());

            while (total < numberOfDice * faces)
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
                total = dice.Sum();
                combinations[total]++;
                RcLog.AddResultRow(total, combinations.Select(v => v).ToArray());
            }
        }

        return probabilities;
        //return combinations.ToDictionary(
        //    combo => combo.Key,
        //    combo => (double)combo.Value / totalCombinations
        //);
    }

}
