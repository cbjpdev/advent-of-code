namespace Aoc2024;

using System;
using System.Collections.Generic;
using System.IO;

public static class Day13Pt2
{
    private static long minimumSpentTokens = 0;
    private const long OFFSET = 10_000_000_000_000;

    public static void Main()
    {
        ProcessInput();
        Console.WriteLine($"The answer is {minimumSpentTokens}");
    }

    private static void ProcessInput()
    {
        // Read and process input
        var input = File.ReadAllText("Input/day13.txt").Trim();
        var paragraphs = input.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var paragraph in paragraphs)
        {
            var lines = new Queue<string>(paragraph.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

            var parts1 = lines.Dequeue().Trim().Split(",");
            var parts2 = lines.Dequeue().Trim().Split(",");
            var parts3 = lines.Dequeue().Trim().Split(",");

            int buttonAx = int.Parse(parts1[0].Replace("Button A: X+", ""));
            int buttonAy = int.Parse(parts1[1].Replace(" Y+", ""));

            int buttonBx = int.Parse(parts2[0].Replace("Button B: X+", ""));
            int buttonBy = int.Parse(parts2[1].Replace(" Y+", ""));

            long prizeX = long.Parse(parts3[0].Replace("Prize: X=", ""));
            long prizeY = long.Parse(parts3[1].Replace(" Y=", ""));

            ProcessMachine(buttonAx, buttonAy, buttonBx, buttonBy, OFFSET + prizeX, OFFSET + prizeY);
        }
    }

    private static void ProcessMachine(int buttonAx, int buttonAy, int buttonBx, int buttonBy, long prizeX, long prizeY)
    {
        long aClicksXMultiplier = buttonAx * buttonBy;
        long aClicksYMultiplier = -(buttonAy * buttonBx);
        long prizeXMultiplied = prizeX * buttonBy;
        long prizeYMultiplied = -(prizeY * buttonBx);

        long aClicksMultiplierCombined = aClicksXMultiplier + aClicksYMultiplier;
        long prizeMultipliedCombined = prizeXMultiplied + prizeYMultiplied;

        if (prizeMultipliedCombined % aClicksMultiplierCombined != 0) return; // No solution

        long aClicks = prizeMultipliedCombined / aClicksMultiplierCombined;
        long bClicks = (prizeX - (buttonAx * aClicks)) / buttonBx;

        if (bClicks != Math.Floor((double)bClicks)) return; // No solution

        minimumSpentTokens += aClicks * 3 + bClicks;
    }
}
