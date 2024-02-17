namespace DiceProbabilitiesDebug;

public class DiceProbabilities_Stage3_A(int numberOfDice, int faces = 6) : DiceProbabilities_Stage2(numberOfDice, faces)
{
    public override Dictionary<int, double> CalculateProbabilitiesForNumberOfDice()
    {
        var combinations = CalculateCombinations();
        return combinations.ToDictionary(kv => kv.Key, kv => (double)kv.Value / totalCombinations);
    }

    protected override Dictionary<int, int> CalculateCombinations()
    {
        (var dice, var combinations) = SetupArrays();

        bool finished1 = false;
        while (!finished1) // this keeps going until the die are all spent (ie , 2 die => 6, 6, 3 die => 6, 6, 6)
        {
            int total = dice.Sum();
        
            Console.Write($"\t[Value={total}]");
            Log(dice, "Dice value", 1, false);

            combinations[total] += 1;       // add 1 more possible roll that will sum to the total value

            int i = 0;
            bool finished2 = false;

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
                    }
                    else
                    {
                        dice[i] = 1; // dice is reset each time to increment the next
                    }
                }
                i++;
            }

            RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());
        }

        return combinations;
    }

}
