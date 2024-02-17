using DiceProbabilitiesDebug;

// ! Start
Greeting();
//Run();
Run2();
// ! End

// Run the program
static void Run()
{
    for (int i = 1; i <= 4; i++)
    {
        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);

        Console.WriteLine($"\n \n Calculate probabilities for {i} die \n \n ");
        var d = new DiceProbabilities_Stage1(i);
        var result_Edited = d.CalculateProbabilitiesForNumberOfDice();

        if (!result_Original.SequenceEqual(result_Edited))
        {
            throw new Exception($"Something wrong ({i} dice)");
        }
    }
    Console.WriteLine("\nCompleted successfully!");
}

static void Run2()
{
    for (int i = 1; i <= 4; i++)
    {
        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);

        Console.WriteLine($"\n \n Calculate probabilities for {i} die \n \n ");
        var d = new DiceProbabilities_Stage2(i);
        var result_Edited = d.CalculateProbabilitiesForNumberOfDice();

        if (!result_Original.SequenceEqual(result_Edited))
        {
            throw new Exception($"Something wrong ({i} dice)");
        }
    }
    Console.WriteLine("\nCompleted successfully!");
}

// Display instructions in Console
static void Greeting()
{
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



