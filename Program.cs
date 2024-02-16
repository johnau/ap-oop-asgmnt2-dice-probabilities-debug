// See https://aka.ms/new-console-template for more information
using DiceProbabilitiesDebug;

Console.WriteLine("Hello, World!");

var result_1 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(1);
var result_2 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(2);
var result_3 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(4);
//var result_14 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(14);
//var result_845 = DiceProbabilitiesOriginal.calculateProbabilitiesForNumberOfDice(845);

Console.WriteLine("\n \n Calculate probabilities for 1 die \n \n ");
var d1 = new DiceProbabilities(1);
var check_1 = d1.CalculateProbabilitiesForNumberOfDice();
Console.WriteLine("\n \n Calculate probabilities for 2 dice \n \n ");
var d2 = new DiceProbabilities(2);
var check_2 = d2.CalculateProbabilitiesForNumberOfDice();
Console.WriteLine("\n \n Calculate probabilities for 3 dice \n \n ");
var d3 = new DiceProbabilities(4);
var check_3 = d3.CalculateProbabilitiesForNumberOfDice();

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