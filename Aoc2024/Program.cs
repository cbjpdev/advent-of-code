// See https://aka.ms/new-console-template for more information

Stopwatch stopwatch = Stopwatch.StartNew();

// Day7.PartOne();
Day7.PartTwo();

stopwatch.Stop();

// Get the elapsed time in milliseconds or other units
Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed.TotalSeconds} seconds");


