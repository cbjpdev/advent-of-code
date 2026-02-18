namespace Aoc2024;

public static class Day12
{
    private static List<List<int>> ReadInput()
    {
        var grid = new List<List<int>>();
        var lines = File.ReadLines("Input/day12.txt");

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
        var garden = new List<List<string>>();
        var processed = new List<List<bool>>();
        var lines = File.ReadLines("Input/day12.txt");

        foreach (var line in lines)
        {
            List<string> numbers = [];
            List<bool> doneLine = [];
            foreach (var c in line)
            {
                numbers.Add(c.ToString());
                doneLine.Add(false);
            }

            garden.Add(numbers);
            processed.Add(doneLine);
        }


        var width = garden[0].Count;
        var height = garden.Count;
        var result = 0;

        var currentSymbol = "";
        var areaSize = 0;
        var areaPerimeter = 0;


        for (int row = 0; row < height; row++) {
            for (int col = 0; col < width; col++)
            {
                ProcessPlot(row, col);
            }
        }

        void ProcessPlot(int row, int col)
        {
            if (processed[row][col])
            {
                return;
            }

            processed[row][col] = true;

            currentSymbol = garden[row][col];

            areaSize = 0;
            areaPerimeter = 0;

            WalkFrom(row, col);

            result += areaSize * areaPerimeter;
        }

        void WalkFrom(int row, int col)
        {
            Queue<(int, int)> pointsToWalk = new();
            pointsToWalk.Enqueue((row, col));

            while (true) {

                if (pointsToWalk.Count == 0)
                    return;

                (int, int) point = pointsToWalk.Dequeue();

                areaSize += 1;

                var newRow = point.Item1;
                var newCol = point.Item2;

                TryCatchNeighbor(newRow - 1, newCol, pointsToWalk);
                TryCatchNeighbor(newRow + 1, newCol, pointsToWalk);
                TryCatchNeighbor(newRow, newCol + 1, pointsToWalk);
                TryCatchNeighbor(newRow, newCol - 1, pointsToWalk);
            }
        }

        void TryCatchNeighbor(int row, int col, Queue<(int, int)> pointsToWalk) {

            if (row < 0) { areaPerimeter += 1;
                return;
            }
            if (col < 0) { areaPerimeter += 1;
                return;
            }
            if (row == height) { areaPerimeter += 1;
                return;
            }
            if (col == width)  { areaPerimeter += 1;
                return;
            }

            if (garden[row][col] != currentSymbol) { areaPerimeter += 1;
                return;
            }

            if (processed[row][col])
            {
                return;
            }

            processed[row][col] = true;

            pointsToWalk.Enqueue((row, col));
        }

        Console.WriteLine($"the answer is {result}");
    }

    public static void PartTwo()
    {

    }
}
