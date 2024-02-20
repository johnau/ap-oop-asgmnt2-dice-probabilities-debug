using DiceProbabilitiesDebug;

//RunComparisonBetweenOriginalAndRefactored();
Greeting();
//Debug();
//Debug2();
Debug3();

//static void RunComparisonBetweenOriginalAndRefactored()
//{
//    int faces = 6;
//    for (int i = 1; i <= 10; i+=2)
//    {
//        ConsoleOut($"\n\nCalculate probabilities for {i} {(i == 1 ? "die" : "dice")} \n\n", ConsoleColor.White, ConsoleColor.Black);
//        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);
//        var result_Edited = new DiceProbabilities_Stage2_E(i, faces).CalculateProbabilitiesForNumberOfDice();
//        if (result_Original.SequenceEqual(result_Edited))
//        {
//            ConsoleOut($"\t >> Results Equal!  Result from orginal method and refactored method are equal for {i} {(i==1?"die":"dice")}", ConsoleColor.Green);
//        } else
//        {
//            ConsoleOut($"Error processing {i} {(i == 1 ? "die" : "dice")}", ConsoleColor.Red);
//            throw new Exception($"Something wrong ({i} dice)");
//        }
//    }
//    ConsoleOut("\nCompleted successfully!", ConsoleColor.Cyan, ConsoleColor.DarkBlue);
//}


#region Debugging process methods

// Run debug stage 1
static void Debug()
{
    for (int i = 1; i <= 4; i++)
    {
        ConsoleOut($"\n \n Calculate probabilities for {i} die \n \n ", ConsoleColor.Yellow, ConsoleColor.Black);
        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);
        var result_Edited = new DiceProbabilities_Stage1(i).CalculateProbabilitiesForNumberOfDice();
        Check("Stage 1", i, result_Original, result_Edited);
    }
    ConsoleOut("\nCompleted successfully!", ConsoleColor.Cyan, ConsoleColor.DarkBlue);
}

// run debug stage 2
static void Debug2()
{
    
    var faces = 6;
    for (int i = 1; i <= 4; i++)
    {
        Console.WriteLine($"\nCalculate probabilities for {i} die\n\n");

        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);

        #region Refactor 1/2
        Console.WriteLine($"\n \n Calculate probabilities for {i} die \n \n ");
        var result_Edited = new DiceProbabilities_Stage2(i, faces).CalculateProbabilitiesForNumberOfDice();
        if (!result_Original.SequenceEqual(result_Edited))
        {
            throw new Exception($"Something wrong ({i} dice)");
        }
        #endregion

        #region Refactor 2A-2C
        
        Console.WriteLine($"\nREFACTOR 2A\n");
        var r2_A = new DiceProbabilities_Stage2_A(i, faces).CalculateProbabilitiesForNumberOfDice();
        Check("2A", i, result_Original, r2_A);

        Console.WriteLine($"\nREFACTOR 2B\n");
        var r2_B = new DiceProbabilities_Stage2_B(i, faces).CalculateProbabilitiesForNumberOfDice();
        Check("2B", i, result_Original, r2_B);

        Console.WriteLine($"\nREFACTOR 2C\n");
        var r2_C = new DiceProbabilities_Stage2_C(i, faces).CalculateProbabilitiesForNumberOfDice();
        Check("2C", i, result_Original, r2_C);

        #endregion

        #region Refactor 2D

        Console.WriteLine($"\nREFACTOR 2D\n");
        var r2_D = new DiceProbabilities_Stage2_D(i, faces).CalculateProbabilitiesForNumberOfDice();
        Check("2D", i, result_Original, r2_D);
        PrintProbabilities(i, faces, r2_D);

        #endregion

        #region Refactor 2E

        Console.WriteLine($"\nREFACTOR 2E\n");
        var r2_E = new DiceProbabilities_Stage2_E(i, faces).CalculateProbabilitiesForNumberOfDice();
        Check("2E", i, result_Original, r2_E);
        PrintProbabilities(i, faces, r2_E);

        #endregion
    }
    Console.WriteLine("\nCompleted successfully!");
}

// Run debug stage 3
static void Debug3()
{
    var faces = 6;
    for (int i = 1; i <= 4; i++)
    {
        ConsoleOut($"\n\nCalculate probabilities for {i} die\n\n", ConsoleColor.Yellow, ConsoleColor.Black);
        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);
        var result_Edited = new DiceProbabilities_Stage3_A(i, faces).CalculateProbabilitiesForNumberOfDice();
        Check("Stage 3", i, result_Original, result_Edited);
        PrintProbabilities(i, faces, result_Edited);
    }
    ConsoleOut("\nCompleted successfully!", ConsoleColor.Cyan, ConsoleColor.DarkBlue);
}

static void Check(string name, int diceCount, Dictionary<int, double> orignal, Dictionary<int, double> checking)
{
    if (!orignal.SequenceEqual(checking))
    {
        throw new Exception($"{name}: Something wrong ({diceCount} dice)");
    } else
    {
        ConsoleOut($"\t >> Results Equal!  Result from orginal method and refactored method are equal for {diceCount} {(diceCount == 1 ? "die" : "dice")}", ConsoleColor.Green);
    }
}

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

static void ConsoleOut(string message, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
{
    Console.ForegroundColor = fg;
    Console.BackgroundColor = bg;
    Console.WriteLine(message);
    Console.ResetColor();
}

// Display instructions in Console
static void Greeting()
{
    Console.ResetColor();
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
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
}

#endregion