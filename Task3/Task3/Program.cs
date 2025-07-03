// See https://aka.ms/new-console-template for more information
using Task3.services;
var finder = new SubarraySumFinder();
finder.FindElementsForSum(new List<uint> { 1, 100, 2, 3, 4, 5, 6, 7, 8, 200 }, 35, out int start, out int end);
Console.WriteLine("[start]: " + start);
Console.WriteLine("[end]: " +  end);

SubarraySumFinder.RunTests();