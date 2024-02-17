namespace DiceProbabilitiesDebug;

public class DiceProbabilities_Stage2_B(int numberOfDice, int faces = 6) : DiceProbabilities_Stage2(numberOfDice, faces)
{

    protected override Dictionary<int, int> CalculateCombinations()
    {
        (var dice, var combinations) = SetupArrays();

        // the original nested while loops iterated over dice across all faces.
        // it shifts a dice until value max
        // once it reaches value max, it shifts to the next dice, increments it, and then resets all previous dice to 1
        // this is repeated across all dice
        // recursion / loop

        combinations[numberOfDice] = 1;
        int total = dice.Sum();
        int d = 0;
        while (total != faces * numberOfDice)
        {
            if (dice[d] == faces)
            {
                dice[d] = 1;
                if (d + 1 == numberOfDice)
                {
                    d = 0;
                }
                else
                {
                    //d++;
                    for (int i = 1; i < numberOfDice; i++)
                    {
                        if (dice[i] != 6)
                        {
                            dice[i]++;
                            break;
                        }
                        else
                        {
                            dice[i] = 1;
                        }
                    }
                }
            }
            else
            {
                dice[d]++;
            }
            Log(dice, "XX Dice", 1, false);
            total = dice.Sum();
            combinations[total]++;
            RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());
        }

        return combinations;
    }

}
