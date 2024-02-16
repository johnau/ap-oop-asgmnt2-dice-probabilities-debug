// See https://aka.ms/new-console-template for more information
using DiceProbabilitiesDebug;

Console.WriteLine("Hello, World!");

var result_1 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(1);
var result_2 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(2);
var result_3 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(3);
//var result_14 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(14);
//var result_845 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(845);

Console.WriteLine("\n \n Calculate probabilities for 1 die \n \n ");
var check_1 = DiceProbabilities.CalculateProbabilitiesForNumberOfDice(1);
Console.WriteLine("\n \n Calculate probabilities for 2 dice \n \n ");
var check_2 = DiceProbabilities.CalculateProbabilitiesForNumberOfDice(2);
Console.WriteLine("\n \n Calculate probabilities for 2 dice \n \n ");
var check_3 = DiceProbabilities.CalculateProbabilitiesForNumberOfDice(3);

if (!result_1.SequenceEqual(check_1))
{
    throw new Exception("Something wrong (1 dice)");
}

if (!result_2.SequenceEqual(check_2))
{
    throw new Exception("Something wrong (2 die)");
}

if (!result_3.SequenceEqual(check_3))
{
    throw new Exception("Something wrong (3 die)");
}

Console.WriteLine("Completed successfully!");