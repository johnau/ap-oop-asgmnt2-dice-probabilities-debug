using DiceProbabilitiesDebug;
using System.Diagnostics;

Greeting();
// Set dice variables for probabilities calculation
int maxDice = 4;
int faceCount = 6;
RunAll(maxDice, faceCount);

#region Debugging process methods
// Run all refactored and alternate solutions (use bool's to turn stages on and off)
static void RunAll(int maxDice, int faceCount)
{
    Dictionary<(string, int), TimeSpan> timings = new Dictionary<(string, int), TimeSpan>();

    #region Stages of Refactoring

    //bool runStage1 = true;
    //bool runStage2 = true;
    //bool runStage2_A = true;
    //bool runStage2_B = true;
    //bool runStage2_C = true;
    //bool runStage2_D = true;
    //bool runStage2_E = true;
    bool runStage1 = false;
    bool runStage2 = false;
    bool runStage2_A = false;
    bool runStage2_B = false;
    bool runStage2_C = false;
    bool runStage2_D = false;
    bool runStage2_E = false;
    #endregion

    #region Alternate solutions
    // Exploring Dynamic Programming
    // Top-Down and Bottom-Up
    // Recursion+Memoization and Iteration
    // Note: Trying to time with C# Stop watch but results are too varied, might need to run 10000 times to get a nice average. 
    // Stage 2_E is consistently the fastest - which is the refactored code
    // Stage 3_A and 3_B move into a dynamic programming approach
    //
    //
    bool runStage3_A = true; // Dynamic programming approach, Top-Down with Memoization and Recursion (Very slow! 400x slower than iteration, function call overheads?)
    bool runStage3_B = true; // Dynamic programming approach, Bottom-up with Iteration - Minor rounding issue for the highest probability using this method due to the accumulation of probabilities(double)
    #endregion

    #region Run stages that are turned on

    if (runStage1) RunStage("Stage 1", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage1(diceCount).CalculateProbabilitiesForNumberOfDice(), timings);
    if (runStage2) RunStage("Stage 2", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage2(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    if (runStage2_A) RunStage("Stage 2_A", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage2_A(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    if (runStage2_B) RunStage("Stage 2_B", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage2_B(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    if (runStage2_C) RunStage("Stage 2_C", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage2_C(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    if (runStage2_D) RunStage("Stage 2_D", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage2_D(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    if (runStage2_E) RunStage("Stage 2_E", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage2_E(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    UserContinue();
    if (runStage3_A) RunStage("Stage 3_A", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage3_A(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    UserContinue();
    if (runStage3_B) RunStage("Stage 3_B", maxDice, faceCount, (int diceCount, int faces) => new DiceProbabilities_Stage3_B(diceCount, faces).CalculateProbabilitiesForNumberOfDice(), timings);
    UserContinue();

    #endregion

    PrintTimings(timings);
}

// Run the original code and refactored code,
// For 1 to `maxDice` number of dice,
// - Measure processing time, and
// - Compare and print results
static void RunStage(string name, int maxDice, int faces, Func<int, int, Dictionary<int, double>> probabilitiesFunction, Dictionary<(string, int), TimeSpan> timings)
{
    for (int i = 1; i <= maxDice; i++)
    {
        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);   // Run the original code

        ConsoleOut($"\n{name} {i} {(i == 1 ? "Die" : "Dice")}\n", ConsoleColor.Yellow, ConsoleColor.Black);
        
        var s = StartTimer();
        var result_Edited = probabilitiesFunction(i, faces);
        timings.Add((name, i), StopTimer(s, i));

        PrintProbabilities(i, faces, result_Edited);
        Check(name, i, result_Original, result_Edited);
    }
    ConsoleOut("\nCompleted successfully!", ConsoleColor.Cyan, ConsoleColor.DarkBlue);
}

// Method to compare results (used to compare original to refactored versions)
static void Check(string name, int diceCount, Dictionary<int, double> orignal, Dictionary<int, double> checking)
{
    if (orignal.SequenceEqual(checking))
    {
        ConsoleOut($">> Results Equal!  Result from orginal method and refactored method are equal for {diceCount} {(diceCount == 1 ? "die" : "dice")}", ConsoleColor.Green);
    }

    ConsoleOut("Original");
    ConsoleOut($"Original  |  Refactored");
    for (int i = diceCount; i < orignal.Count + diceCount; i++)
    {
        var o = orignal[i];
        var r = checking[i];
        if (o != r) Console.ForegroundColor = ConsoleColor.Red;
        ConsoleOut($"{orignal[i]} ?= {checking[i]}");
        Console.ResetColor();
        
        if (o == r) continue;
         
        var rounding = 15;
        if (Math.Round(o, rounding) == Math.Round(r, rounding))
            ConsoleOut($"Ok when rounded to {rounding} digits", ConsoleColor.Green);
        else
            throw new Exception($"{name}: Something wrong ({diceCount} dice)");
    }
}

// Pretty printing of probabilities dictionary - Provides a nice-ish visualization of the construction of the the table through each loop
static void PrintProbabilities(int diceCount, int faces, Dictionary<int, double> probabilities)
{
    for (int j = diceCount; j <= diceCount * faces; j++)
    {
        var isMax = probabilities[j] == probabilities.Values.Max();
        if (isMax) Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[Roll a {j,3}]: {probabilities[j] * 100:F2}%");
        if (isMax) Console.ResetColor();
    }
}

static void PrintTimings(Dictionary<(string, int), TimeSpan> timings)
{
    timings = timings.OrderBy(x => x.Key.Item2)
                             //.ThenBy(x => x.Key.Item1)
                             .ThenBy(x => x.Value)
                             .ToDictionary(x => x.Key, x => x.Value);
    foreach (var timing in timings)
    {
        (string name, int diceCount) = timing.Key;
        ConsoleOut($"{diceCount} dice, {name}: {timing.Key}={timing.Value}", (ConsoleColor)diceCount);
    }
}

// Output to console
static void ConsoleOut(string message, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
{
    Console.ForegroundColor = fg;
    Console.BackgroundColor = bg;
    Console.WriteLine(message);
    Console.ResetColor();
}

// Time measuring
static Stopwatch StartTimer()
{
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    return stopwatch;
}

// Time measuring
static TimeSpan StopTimer(Stopwatch stopwatch, int numberOfDice = -1)
{
    stopwatch.Stop();
    TimeSpan timeTaken = stopwatch.Elapsed;

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"This solution {(numberOfDice > 0 ? $"for {numberOfDice} dice " : " ")}took {timeTaken} to complete");
    Console.ResetColor();

    return timeTaken;
}

static void UserContinue()
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
}

// Display instructions in Console
static void Greeting()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Dice Probabilities\n");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Yellow;
    Console.Write("!!");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.BackgroundColor = ConsoleColor.Red;
    Console.Write(" Please maximize console window to avoid display errors ");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Yellow;
    Console.Write("!!\n\n");
    Console.ResetColor();
    UserContinue();
}

#endregion