using System.Diagnostics;

namespace DiceProbabilitiesDebug;

public class DiceProbabilities_Stage2_E(int numberOfDice, int faces = 6) // This constructor style is weird for a full class
{
    protected readonly int numberOfDice = numberOfDice;
    protected readonly int faces = faces;
    protected readonly int totalCombinations = (int)Math.Pow(faces, numberOfDice);

    public virtual Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        var dice = Enumerable.Repeat(1, numberOfDice).ToArray();
        var combinations = Enumerable.Range(numberOfDice, numberOfDice * faces - numberOfDice + 1).ToDictionary(key => key, value => 0);

        combinations[numberOfDice] = 1; // set initial value to 1, could also set maxValue to 1 and flip the contents of the while loop?
        var total = numberOfDice;
        while (total < numberOfDice * faces)
        {
            for (int d = 0; d < numberOfDice; d++)
            {
                if (dice[d] == faces)
                {
                    dice[d] = 1;
                }
                else
                {
                    dice[d]++;
                    break;
                }
            }
            total = dice.Sum();
            combinations[total]++;
        }

        var probabilities = combinations.ToDictionary(
            combo => combo.Key, 
            combo => (double) combo.Value / totalCombinations
        );

        return probabilities;
    }

}
