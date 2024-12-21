namespace Aoc2024;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Day14Pt1
{
    private const int Width = 101;
    private const int Height = 103;
    private const int VerticalDivision = 50;   // for width
    private const int HorizontalDivision = 51; // for height

    private static int quadrantA = 0;
    private static int quadrantB = 0;
    private static int quadrantC = 0;
    private static int quadrantD = 0;

    public static void Main()
    {
        ProcessInput();
        Console.WriteLine($"The answer is {quadrantA * quadrantB * quadrantC * quadrantD}");
    }

    private static void ProcessInput()
    {
        // Read and process input
        var input = File.ReadAllText("Input/day14.txt").Trim();
        var lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parts = line.Trim().Split(" ");

            // Parse position and velocity
            var tokensP = parts[0].Substring(2).Split(",");
            var tokensV = parts[1].Substring(2).Split(",");

            int posX = int.Parse(tokensP[0]);
            int posY = int.Parse(tokensP[1]);
            int velX = int.Parse(tokensV[0]);
            int velY = int.Parse(tokensV[1]);

            // Calculate brute positions
            int bruteX = posX + (100 * velX);
            int bruteY = posY + (100 * velY);

            // Adjust final positions within boundaries
            int finalX = bruteX % Width;
            if (finalX < 0) finalX += Width;

            int finalY = bruteY % Height;
            if (finalY < 0) finalY += Height;

            // Skip positions matching the divisions
            if (finalX == VerticalDivision || finalY == HorizontalDivision)
                continue;

            // Determine quadrant and increment respective counter
            if (finalX < VerticalDivision)
            {
                if (finalY < HorizontalDivision)
                    quadrantA++;
                else
                    quadrantC++;
            }
            else
            {
                if (finalY < HorizontalDivision)
                    quadrantB++;
                else
                    quadrantD++;
            }
        }
    }
}
