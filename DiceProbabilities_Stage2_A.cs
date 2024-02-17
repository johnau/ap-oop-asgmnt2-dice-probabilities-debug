namespace DiceProbabilitiesDebug;

public class DiceProbabilities_Stage2_A(int numberOfDice, int faces = 6) : DiceProbabilities_Stage2(numberOfDice, faces)
{

    protected override Dictionary<int, int> CalculateCombinations()
    {
        (var dice, var combinations) = SetupArrays();

        // the original nested while loops iterated over dice across all faces.
        // it shifts a dice until value max
        // once it reaches value max, it shifts to the next dice, increments it, and then resets all previous dice to 1
        // this is repeated across all dice
        // recursion / loop

        //bool finished1 = false; // no longer required with new loop condition
        //while (!finished1) // this keeps going until the die are all spent (ie , 2 die => 6, 6, 3 die => 6, 6, 6)
        combinations[numberOfDice] = 1;
        int total = dice.Sum();
        RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());

        while (total != faces * numberOfDice)  // loop til max value
        {
            //// dice starts at 1 and then increments at end of each loop, each loop is the next increment of face value
            //total = dice.Sum();
            //Console.Write($"\t[Value={total}]");
            //Log(dice, "Dice value", 1, false);

            //combinations[total] += 1;       // add 1 more possible roll that will sum to the total value

            //int i = 0;
            //bool finished2 = false;

            // This while loop iterates over each die, and tries each combination of values,eg. 3 dice ->  (1, 1, 1), (2, 1, 1), (3, 1, 1) ..... (5, 6, 6), (6, 6, 6)
            // Each of the combinations is summed to its total value (1, 1, 1) => 3
            //while (!finished2)
            for (int i = 0; i < numberOfDice; i++)
            {
                dice[i] += 1;
                if (dice[i] <= 6) // inner loop does not get entered until all sides run out
                {
                    //finished2 = true;
                    break;
                }
                else
                {
                    if (i == numberOfDice - 1) // both loops exit when the last die has exceeded 6
                    {
                        //finished1 = true;
                        //finished2 = true;
                        break;
                        //Console.WriteLine($"\t    <XX> i={i}, n={numberOfDice}");
                    }
                    else
                    {
                        dice[i] = 1; // dice is reset each time to increment the next
                        //Console.WriteLine($"\t    <reset dice[{i}]=1>");
                    }
                }
                //i++;
            }

            // dice starts at 1 and then increments at end of each loop, each loop is the next increment of face value
            total = dice.Sum();
            Console.Write($"\t[Value={total}]");
            Log(dice, "Dice value", 1, false);

            combinations[total] += 1;       // add 1 more possible roll that will sum to the total value

            RcLog.AddResultRow(total, combinations.Values.Select(v => v).ToArray());
            //Log(rollCombinations, "Combinations", 2);
            //Log(dice, "Dice value", 2);
        }

        return combinations;
    }

}
