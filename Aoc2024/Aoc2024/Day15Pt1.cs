namespace Aoc2024;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Day15Pt1
{
    private static List<char[]> map = new List<char[]>();
    private static int width;
    private static int height;

    private static int botRow;
    private static int botCol;

    private static string guide = "";

    public static void Main()
    {
        ProcessInput();
        Walk();
        Console.WriteLine("The answer is " + CountBoxesGps());
    }

    private static void ProcessInput()
    {
        string input = File.ReadAllText("Input/day15.txt").Trim();
        var sections = input.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

        var rawMap = sections[0].Trim();
        foreach (var rawLine in rawMap.Split('\n'))
        {
            map.Add(rawLine.Trim().ToCharArray());
        }

        height = map.Count;
        width = map[0].Length;

        guide = sections[1].Replace("\n", "").Trim();

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (map[row][col] == '@')
                {
                    botRow = row;
                    botCol = col;
                    return;
                }
            }
        }
    }

    private static void Walk()
    {
        foreach (char direction in guide)
        {
            if (direction == '^') { WalkTo(-1, 0); continue; }
            if (direction == 'v') { WalkTo(1, 0); continue; }
            if (direction == '>') { WalkTo(0, 1); continue; }
            if (direction == '<') { WalkTo(0, -1); continue; }
        }
    }

    private static void WalkTo(int deltaRow, int deltaCol)
    {
        int futureRow = botRow;
        int futureCol = botCol;

        // Search for the first free space ahead
        while (true)
        {
            futureRow += deltaRow;
            futureCol += deltaCol;

            char futureSpot = map[futureRow][futureCol];

            if (futureSpot == '#') return;
            if (futureSpot == '.') break;
        }

        // Move everything in the segment from the bot to the first free space ahead
        while (true)
        {
            int previousRow = futureRow - deltaRow;
            int previousCol = futureCol - deltaCol;

            map[futureRow][futureCol] = map[previousRow][previousCol];

            futureRow = previousRow;
            futureCol = previousCol;

            if (futureRow == botRow && futureCol == botCol) break;
        }

        map[botRow][botCol] = '.';
        botRow += deltaRow;
        botCol += deltaCol;
    }

    private static int CountBoxesGps()
    {
        int gps = 0;

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (map[row][col] == 'O')
                {
                    gps += 100 * row + col;
                }
            }
        }

        return gps;
    }

    private static void ShowMap()
    {
        foreach (var line in map)
        {
            Console.WriteLine(new string(line));
        }
    }
}
