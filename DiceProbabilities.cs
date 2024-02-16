using System.Diagnostics;

namespace DiceProbabilitiesDebug;

class DiceProbabilities
{
    public static Dictionary<int, Double> CalculateProbabilitiesForNumberOfDice(int n)
    {
        Dictionary<int, int> rollCalculations = new Dictionary<int, int>(); 
        // int mn = n; // replace mn in code with n
        int mx = n * 6;
        Debug.WriteLine($"Populate RollCalc dictionary for {n} dice (from {n} to {mx})");
        for (int i = n; i <= mx; i++) // populate dict with keys from number of dice to number of dice times the sides (6)
        {
            rollCalculations[i] = 0;
            Debug.WriteLine($"{i}: {0}");
        }
        
        int[] dice = new int[n]; // new array sized at number of dice.
        Debug.WriteLine($"Populate array of ({n}) ({n} dice)");
        for (int i = 0; i < n; i++)
        {
            dice[i] = 1;
            Debug.WriteLine($"{i}: {1}");
        }

        bool finished1 = false;
        Debug.WriteLine("Begin main outer loop");
        while (!finished1)
        {
            Debug.WriteLine($"Next outer");
            //int total = 0;
            //foreach (int d in dice)
            //{
            //    total += d;
            //}
            int total = dice.Sum();
            Debug.WriteLine($"Summation of 'dice' array={total}");

            rollCalculations[total] += 1;
            Debug.WriteLine($"Mutate RollCalcs dict, update key{total}={rollCalculations[total]} (from {rollCalculations[total]-1})");

            int i = 0;
            bool finished2 = false;
            Debug.WriteLine("Begin main inner loop");
            while (!finished2)
            {
                Debug.WriteLine($"Next inner ({i})");
                dice[i] += 1;
                Debug.WriteLine($"Update dice array [{i}]={dice[i]}");
                if (dice[i] <= 6)
                {
                    Debug.WriteLine($"dice array [{i}] value is less than 7 ({dice[i]}), main inner loop cancelled.");
                    finished2 = true;
                }
                else
                {
                    Debug.WriteLine($"dice array [{i}] value is now more than 6 ({dice[i]}), main inner loop active.");
                    if (i == n - 1)
                    {

                        finished1 = true;
                        finished2 = true;
                    }
                    else
                    {
                        dice[i] = 1;
                    }
                }
                i++;
            }
        }
        Dictionary<int, Double> rp = new Dictionary<int, double>();
        Double total2 = Math.Pow(6.0, (Double)n);
        for (int i = n; i <= mx; i++)
        {
            rp[i] = (Double)rollCalculations[i] / total2;
        }
        return rp;
    }
}
