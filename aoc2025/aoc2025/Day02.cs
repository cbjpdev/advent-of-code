using System.Numerics;
using System.Text.RegularExpressions;

namespace aoc2025;

public static partial class Day02
{
    private static string[] GetInput()
    {
        string[] input = File.ReadAllText("Inputs/Day02.txt")
            .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => x).ToArray();
        return input;
    }

    public static void Part1()
    {
        string[] inputs = GetInput();
        BigInteger totalScore = 0;

        foreach (string input in inputs)
        {
            BigInteger left = Convert.ToInt64(input.Split('-')[0]);
            BigInteger right = Convert.ToInt64(input.Split('-')[1]);
            
            for (BigInteger i = left; i <= right; i++)
            {
                string number = i.ToString();
                int numberLenght = number.Length;
                if (numberLenght % 2 != 0) continue;
                
                string leftPart = number[..(numberLenght / 2)];
                string rightPart = number.Substring(numberLenght / 2, numberLenght / 2);
                
                if (leftPart != rightPart) continue;
                
                Console.WriteLine($"{i}: {leftPart} => {rightPart}");
                totalScore += i;
            }
            
        }

        Console.WriteLine($"Total Score: {totalScore}");
    }
    
    public static void Part2()
    {
        string[] inputs = GetInput();
        BigInteger totalScore = 0;

        foreach (string input in inputs)
        {
            BigInteger left = Convert.ToInt64(input.Split('-')[0]);
            BigInteger right = Convert.ToInt64(input.Split('-')[1]);
            
            for (BigInteger i = left; i <= right; i++)
            {
                string number = i.ToString();
                
                bool isRepeated = MyRegex().IsMatch(number);
                if (!isRepeated) continue;
                
                totalScore += i;
            }
            
        }

        Console.WriteLine($"Total Score: {totalScore}");
    }

    [GeneratedRegex(@"^(.+)\1+$")]
    private static partial Regex MyRegex();
}