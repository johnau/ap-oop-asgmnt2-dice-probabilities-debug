namespace DiceProbabilitiesDebug;

public class DiceProbabilities_Stage2_C(int numberOfDice, int faces = 6) : DiceProbabilities_Stage2(numberOfDice, faces)
{

    protected override Dictionary<int, int> CalculateCombinations()
    {
        (var dice, var combinations) = SetupArrays();

        // the original nested while loops iterated over dice across all faces.
        // it shifts a dice until value max
        // once it reaches value max, it shifts to the next dice, increments it, and then resets all previous dice to 1
        // this is repeated across all dice
        // recursion / loop

        // the intial value in the combinations dict will always be 1
        combinations[numberOfDice] = 1;
        int total = dice.Sum();
        RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());

        while (dice.Sum() != faces * numberOfDice)
        {
            if (dice[0] == faces)
            {
                dice[0] = 1;

                for (int d = 1; d < numberOfDice; d++)
                {
                    if (dice[d] != 6)
                    {
                        dice[d]++;
                        break;
                    }
                    else
                    {
                        dice[d] = 1;
                    }
                }
            }
            else
            {
                dice[0]++;
            }

            Log(dice, "XX Dice", 1, false);
            total = dice.Sum();
            combinations[total]++;
            RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());
        }

        return combinations;
    }

}
