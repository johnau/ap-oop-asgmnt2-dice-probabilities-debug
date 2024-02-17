// See https://aka.ms/new-console-template for more information
using DiceProbabilitiesDebug;

Console.WriteLine("Dice Probabilities");

for (int i = 1; i <= 4; i++)
{
    var result_Original = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(i);

    Console.WriteLine($"\n \n Calculate probabilities for {i} die \n \n ");
    var d = new DiceProbabilities(i);
    var result_Edited = d.CalculateProbabilitiesForNumberOfDice();

    if (!result_Original.SequenceEqual(result_Edited))
    {
        throw new Exception($"Something wrong ({i} dice)");
    }
}

Console.WriteLine("\nCompleted successfully!");