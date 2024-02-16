using System.Diagnostics;

namespace DiceProbabilitiesDebug;

public static class DiceProbabilities
{
    public static Dictionary<int, Double> CalculateProbabilitiesForNumberOfDice(int n)
    {
        Dictionary<int, int> rollCalculations = new Dictionary<int, int>(); 
        // int mn = n; // replace mn in code with n
        int mx = n * 6;
        Console.WriteLine($"Populate RollCalc dictionary for {n} dice (from {n} to {mx}) with 0's");
        for (int i = n; i <= mx; i++) // populate dict with keys from number of dice to number of dice times the sides (6)
        {
            rollCalculations[i] = 0;
            Console.WriteLine($"{i}: 0");
        }
        
        int[] dice = new int[n]; // new array sized at number of dice.
        Console.WriteLine($"Populate array of ({n}) ({n} dice)");
        for (int i = 0; i < n; i++)
        {
            dice[i] = 1;
            Console.WriteLine($"{i}: {1}");
        }

        bool finished1 = false;
        Console.WriteLine("Begin main outer loop");
        while (!finished1)
        {
            Console.WriteLine($"Next outer");
            //int total = 0;
            //foreach (int d in dice)
            //{
            //    total += d;
            //}
            int total = dice.Sum();
            Console.WriteLine($"Summation of 'dice' array={total}");

            rollCalculations[total] += 1;
            Console.WriteLine($"Mutate RollCalcs dict, update key{total}={rollCalculations[total]} (from {rollCalculations[total]-1})");

            int i = 0;
            bool finished2 = false;
            Console.WriteLine("Begin main inner loop");
            while (!finished2)
            {
                Console.WriteLine($"Next inner ({i})");
                dice[i] += 1;
                Console.WriteLine($"Update dice array [{i}]={dice[i]}");
                if (dice[i] <= 6)
                {
                    Console.WriteLine($"dice array [{i}] value is less than 7 ({dice[i]}), main inner loop cancelled.");
                    finished2 = true;
                }
                else
                {
                    Console.WriteLine($"dice array [{i}] value is now more than 6 ({dice[i]}), main inner loop active.");
                    if (i == n - 1)
                    {
                        Console.WriteLine($"Main out and inner loops exit (i({i}) == n({n}) - 1");
                        finished1 = true;
                        finished2 = true;
                    }
                    else
                    {
                        dice[i] = 1;
                        Console.WriteLine($"Update dice array[{i}]=1");
                    }
                }
                i++;
            }
        }

        Dictionary<int, Double> rollProbabilities = new Dictionary<int, double>();
        Double total2 = Math.Pow(6.0, (Double)n);
        Console.WriteLine($"Total combinations: 6^{n}={total2}");
        Console.WriteLine($"Process RollCalc dictionary for {n} dice (from {n} to {mx}) into roll probablities dict");
        for (int i = n; i <= mx; i++)
        {
            Console.WriteLine($"Roll calc [{i}]={rollCalculations[i]}");
            Console.WriteLine($"Roll divided by Total combinations = {(Double)rollCalculations[i] / total2}");
            rollProbabilities[i] = (Double)rollCalculations[i] / total2;
            Console.WriteLine($"Roll probability for [{i}] = {(Double)rollCalculations[i] / total2}");
        }
        return rollProbabilities;
    }
}
