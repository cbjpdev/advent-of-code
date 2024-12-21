namespace Aoc2024;

using System;
using System.Collections.Generic;
using System.IO;

public static class Day14Pt2
{
    private const int Width = 101;
    private const int Height = 103;

    private static readonly int[,] Map = new int[Height, Width];
    private static readonly List<Guard> AllGuards = new List<Guard>();
    private static int time = 0;

    public static void Main()
    {
        ProcessInput();
        SetMap();

        while (true)
        {
            time++;
            bool clashes = MoveGuards();

            if (!clashes)
            {
                ShowMap();
                break;
            }
        }

        Console.WriteLine($"If there is a xmas tree in the picture above, the answer is {time}");
    }

    private static void ProcessInput()
    {
        var input = System.IO.File.ReadAllText("Input/day14.txt").Trim();
        var lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parts = line.Trim().Split(" ");

            var tokensP = parts[0].Substring(2).Split(",");
            var tokensV = parts[1].Substring(2).Split(",");

            int posX = int.Parse(tokensP[0]);
            int posY = int.Parse(tokensP[1]);
            int velX = int.Parse(tokensV[0]);
            int velY = int.Parse(tokensV[1]);

            var guard = new Guard(posX, posY, velX, velY);
            AllGuards.Add(guard);
        }
    }

    private static void SetMap()
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                Map[row, col] = 0;
            }
        }
    }

    private static bool MoveGuards()
    {
        foreach (var guard in AllGuards)
        {
            if (MoveGuard(guard))
            {
                return true;
            }
        }
        return false;
    }

    private static bool MoveGuard(Guard guard)
    {
        int bruteX = guard.PosX + (time * guard.VelX);
        int bruteY = guard.PosY + (time * guard.VelY);

        int finalX = bruteX % Width;
        if (finalX < 0) finalX += Width;

        int finalY = bruteY % Height;
        if (finalY < 0) finalY += Height;

        if (Map[finalY, finalX] == time)
        {
            return true;
        }

        Map[finalY, finalX] = time;
        return false;
    }

    private static void ShowMap()
    {
        Console.WriteLine();

        for (int row = 0; row < Height; row++)
        {
            var line = "";
            for (int col = 0; col < Width; col++)
            {
                line += Map[row, col] == time ? "X" : ".";
            }
            Console.WriteLine(line);
        }

        Console.WriteLine();
    }

    private class Guard
    {
        public int PosX { get; }
        public int PosY { get; }
        public int VelX { get; }
        public int VelY { get; }

        public Guard(int posX, int posY, int velX, int velY)
        {
            PosX = posX;
            PosY = posY;
            VelX = velX;
            VelY = velY;
        }
    }
}
