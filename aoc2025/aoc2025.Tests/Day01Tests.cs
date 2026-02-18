namespace aoc2025.Tests;

public class Day01Tests
{
    [Theory]
    [InlineData("R18", 68)]
    [InlineData("R68", 18)]
    [InlineData("R168", 18)]
    [InlineData("L62", 88)]
    [InlineData("L162", 88)]
    public void Part01_Tests(string input, int expected)
    {
        //Console.WriteLine(268 % 100);
        int result = Day01.ProcessPart01(input, 50);
        Assert.Equal(expected, result);
    }
    
    // [Theory]
    // [InlineData("R10", 60, 0)]
    // [InlineData("R100", 50, 1)]
    // [InlineData("R150", 0, 2)]
    // [InlineData("L10", 40, 0)]
    // [InlineData("L100", 50, 1)]
    // [InlineData("L1000", 50, 10)]
    // public void Part02_Tests(string input, int expected1, int expected2)
    // {
    //     (int, int) result = Day01.ProcessPart02(input, 50, 0);
    //     Assert.Equal(expected1, result.Item1);
    //     Assert.Equal(expected2, result.Item2);
    // }
}