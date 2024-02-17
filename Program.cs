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
        Console.WriteLine($"\n \n Calculate probabilities for {i} die \n \n ");
        var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);
        var result_Edited = new DiceProbabilities_Stage1(i).CalculateProbabilitiesForNumberOfDice();
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

        //Console.WriteLine($"\n \n Calculate probabilities for {i} die \n \n ");
        //var result_Edited = new DiceProbabilities_Stage2(i).CalculateProbabilitiesForNumberOfDice();
        //if (!result_Original.SequenceEqual(result_Edited))
        //{
        //    throw new Exception($"Something wrong ({i} dice)");
        //}

        //Console.WriteLine($"\n \n REFACTOR 2A -- Calculate probabilities for {i} die \n \n ");
        //var r2_A = new DiceProbabilities_Stage2_A(i).CalculateProbabilitiesForNumberOfDice();
        //if (!result_Original.SequenceEqual(r2_A))
        //{
        //    throw new Exception($"2A: Something wrong ({i} dice)");
        //}

        //Console.WriteLine($"\n \n REFACTOR 2B -- Calculate probabilities for {i} die \n \n ");
        //var r2_B = new DiceProbabilities_Stage2_B(i).CalculateProbabilitiesForNumberOfDice();
        //if (!result_Original.SequenceEqual(r2_B))
        //{
        //    throw new Exception($"2B: Something wrong ({i} dice)");
        //}

        //Console.WriteLine($"\n \n REFACTOR 2C -- Calculate probabilities for {i} die \n \n ");
        //var r2_C = new DiceProbabilities_Stage2_C(i).CalculateProbabilitiesForNumberOfDice();
        //if (!result_Original.SequenceEqual(r2_C))
        //{
        //    throw new Exception($"2C: Something wrong ({i} dice)");
        //}

        Console.WriteLine($"\n \n REFACTOR 2D -- Calculate probabilities for {i} die \n \n ");
        var r2_D = new DiceProbabilities_Stage2_D(i).CalculateProbabilitiesForNumberOfDice();
        if (!result_Original.SequenceEqual(r2_D))
        {
            throw new Exception($"2D: Something wrong ({i} dice)");
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



