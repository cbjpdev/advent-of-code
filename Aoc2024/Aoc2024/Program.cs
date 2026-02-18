// See https://aka.ms/new-console-template for more information

Stopwatch stopwatch = Stopwatch.StartNew();

Day15Pt2.Main();

stopwatch.Stop();

// Get the elapsed time in milliseconds or other units
Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Elapsed Time: {stopwatch.Elapsed.TotalSeconds} seconds");


