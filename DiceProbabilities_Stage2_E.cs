namespace DiceProbabilitiesDebug;

public class DiceProbabilities_Stage2_E(int numberOfDice, int faces = 6) : DiceProbabilities_Stage2(numberOfDice, faces)
{

    public override Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        var combinations = CalculateCombinations();
        return combinations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / totalCombinations);
    }

    protected override Dictionary<int, int> CalculateCombinations()
    {
        (var dice, var combinations) = SetupArrays();
        combinations[numberOfDice] = 1; // set initial value to 1, could also set maxValue to 1 and flip the contents of the while loop?
        var total = dice.Sum();
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
            total = dice.Sum();
            combinations[total]++;
        }
        return combinations;
    }

}
