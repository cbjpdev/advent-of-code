namespace Aoc2024;

public static class Day10
{
    private static List<List<int>> ReadInput()
    {
        var grid = new List<List<int>>();
        var lines = File.ReadLines("Input/day10.txt");

        foreach (var line in lines)
        {
            List<int> numbers = [];
            foreach (var c in line)
            {
                if (c == '.')
                {
                    numbers.Add(-1);
                }
                else
                {
                    numbers.Add(int.Parse(c.ToString()));
                }
            }

            grid.Add(numbers);
        }

        return grid;
    }

    public static void PartOne()
    {
        var map = ReadInput();

        int totalScore = 0;

        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[0].Count(); x++)
            {
                if (map[y][x] == 0)
                {
                    var endPositions = new HashSet<(int, int)>();
                    WalkTrail(x, y, endPositions);
                    totalScore += endPositions.Count;
                }
            }
        }

        Console.WriteLine($"Sum of scores for all trailheads (part 1): {totalScore}");

        void WalkTrail(int x, int y, HashSet<(int, int)> endPositions)
        {
            int height = map[y][x];
            if (height == 9)
            {
                endPositions.Add((x, y));
                return;
            }

            // left
            if (x > 0 && map[y][ x - 1] == height + 1)
            {
                WalkTrail(x - 1, y, endPositions);
            }
            // up
            if (y > 0 && map[y - 1][x] == height + 1)
            {
                WalkTrail(x, y - 1, endPositions);
            }
            // right
            if (x < map[0].Count - 1 && map[y][x + 1] == height + 1)
            {
                WalkTrail(x + 1, y, endPositions);
            }
            // down
            if (y < map.Count - 1 && map[y + 1][x] == height + 1)
            {
                WalkTrail(x, y + 1, endPositions);
            }
        }
    }

    public static void PartTwo()
    {
        var map = ReadInput();
        int totalScore = 0;

        int WalkTrail(int x, int y)
        {
            int height = map[y][x];
            if (height == 9)
            {
                return 1;
            }

            int score = 0;

            // left
            if (x > 0 && map[y][x - 1] == height + 1)
            {
                score += WalkTrail(x - 1, y);
            }
            // up
            if (y > 0 && map[y - 1][x] == height + 1)
            {
                score += WalkTrail(x, y - 1);
            }
            // right
            if (x < map[y].Count - 1 && map[y][x + 1] == height + 1)
            {
                score += WalkTrail(x + 1, y);
            }
            // down
            if (y < map.Count - 1 && map[y + 1][x] == height + 1)
            {
                score += WalkTrail(x, y + 1);
            }

            return score;
        }

        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == 0)
                {
                    totalScore += WalkTrail(x, y);
                }
            }
        }

        Console.WriteLine($"Sum of ratings for all trailheads (part 2): {totalScore}");
    }
}
