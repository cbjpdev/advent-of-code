namespace Aoc2024;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Day15Pt2
{
    private static List<char[]> map = [];
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
            string line = "";
            foreach (var c in rawLine.Trim())
            {
                if (c == '#') line += "##";
                else if (c == '.') line += "..";
                else if (c == 'O') line += "[]";
                else if (c == '@') line += "@.";
            }
            map.Add(line.ToCharArray());
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
            if (direction == '^') WalkVertical(-1);
            else if (direction == 'v') WalkVertical(1);
            else if (direction == '>') WalkHorizontal(1);
            else if (direction == '<') WalkHorizontal(-1);
        }
    }

    private static void WalkVertical(int deltaRow)
    {
        int futureRow = botRow + deltaRow;
        char futureSpot = map[futureRow][botCol];

        if (futureSpot == '#') return;

        if (futureSpot == '.')
        {
            map[botRow][botCol] = '.';
            botRow = futureRow;
            map[botRow][botCol] = '@';
            return;
        }

        if (!MayMoveBoxVertical(futureRow, botCol, deltaRow)) return;

        MoveBoxVertical(futureRow, botCol, deltaRow);
        map[botRow][botCol] = '.';
        botRow = futureRow;
        map[botRow][botCol] = '@';
    }

    private static void WalkHorizontal(int deltaCol)
    {
        int futureCol = botCol;

        while (true)
        {
            futureCol += deltaCol;
            char futureSpot = map[botRow][futureCol];

            if (futureSpot == '#') return;
            if (futureSpot == '.') break;
        }

        while (true)
        {
            int previousCol = futureCol - deltaCol;
            map[botRow][futureCol] = map[botRow][previousCol];
            futureCol = previousCol;

            if (futureCol == botCol) break;
        }

        map[botRow][botCol] = '.';
        botCol += deltaCol;
    }

    private static bool MayMoveBoxVertical(int boxRow, int boxCol, int deltaRow)
    {
        if (map[boxRow][boxCol] == ']') boxCol--;

        int futureRow = boxRow + deltaRow;
        char futureSpotLeft = map[futureRow][boxCol];
        char futureSpotRight = map[futureRow][boxCol + 1];

        if (futureSpotLeft == '#' || futureSpotRight == '#') return false;

        if (futureSpotLeft == '[' && !MayMoveBoxVertical(futureRow, boxCol, deltaRow)) return false;
        if (futureSpotLeft == ']' && !MayMoveBoxVertical(futureRow, boxCol, deltaRow)) return false;
        if (futureSpotRight == '[' && !MayMoveBoxVertical(futureRow, boxCol + 1, deltaRow)) return false;

        return true;
    }

    private static void MoveBoxVertical(int boxRow, int boxCol, int deltaRow)
    {
        if (map[boxRow][boxCol] == ']') boxCol--;

        int futureRow = boxRow + deltaRow;
        char futureSpotLeft = map[futureRow][boxCol];
        char futureSpotRight = map[futureRow][boxCol + 1];

        if (futureSpotLeft == '[') MoveBoxVertical(futureRow, boxCol, deltaRow);
        if (futureSpotLeft == ']') MoveBoxVertical(futureRow, boxCol, deltaRow);
        if (futureSpotRight == '[') MoveBoxVertical(futureRow, boxCol + 1, deltaRow);

        map[boxRow][boxCol] = '.';
        map[boxRow][boxCol + 1] = '.';
        map[futureRow][boxCol] = '[';
        map[futureRow][boxCol + 1] = ']';
    }

    private static int CountBoxesGps()
    {
        int gps = 0;
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (map[row][col] == '[') gps += 100 * row + col;
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
