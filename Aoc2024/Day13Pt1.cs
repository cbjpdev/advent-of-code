namespace Aoc2024;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Day13Pt1
{
    private static int minimumSpentTokens = 0;

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
            var lines = paragraph.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var parts1 = lines[0].Trim().Split(",");
            var parts2 = lines[1].Trim().Split(",");
            var parts3 = lines[2].Trim().Split(",");

            int buttonAx = int.Parse(parts1[0].Replace("Button A: X+", ""));
            int buttonAy = int.Parse(parts1[1].Replace(" Y+", ""));

            int buttonBx = int.Parse(parts2[0].Replace("Button B: X+", ""));
            int buttonBy = int.Parse(parts2[1].Replace(" Y+", ""));

            int prizeX = int.Parse(parts3[0].Replace("Prize: X=", ""));
            int prizeY = int.Parse(parts3[1].Replace(" Y=", ""));

            ProcessMachine(buttonAx, buttonAy, buttonBx, buttonBy, prizeX, prizeY);
        }
    }

    private static void ProcessMachine(int buttonAx, int buttonAy, int buttonBx, int buttonBy, int prizeX, int prizeY)
    {
        int aClicksXMultiplier = buttonAx * buttonBy;
        int aClicksYMultiplier = -(buttonAy * buttonBx);
        int prizeXMultiplied = prizeX * buttonBy;
        int prizeYMultiplied = -(prizeY * buttonBx);

        int aClicksMultiplierCombined = aClicksXMultiplier + aClicksYMultiplier;
        int prizeMultipliedCombined = prizeXMultiplied + prizeYMultiplied;

        if (prizeMultipliedCombined % aClicksMultiplierCombined != 0) return; // No solution

        int aClicks = prizeMultipliedCombined / aClicksMultiplierCombined;
        decimal bClicks = (prizeX - (buttonAx * aClicks)) / buttonBx;

        if (bClicks != Math.Floor(bClicks)) return; // No solution

        minimumSpentTokens += aClicks * 3 + Convert.ToInt32(bClicks);
    }
}
