namespace Aoc2024;

public static class Day8
{
    private static List<List<char>> ReadInput()
    {
        string input = File.ReadAllText("Input/day8.txt");

        // Remove empty lines or extra spaces
        string[] grid = input.Trim().Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        // Convert grid to List<List<char>> if needed
        List<List<char>> charGrid = [];
        charGrid.AddRange(grid.Select(line => new List<char>(line)));

        return charGrid;
    }

    public static void PartOne()
    {
        var grid = ReadInput();

        List<(int, int)> positions = [];

        for (int row = 0; row < grid.Count; row++)
        {
            for (int col = 0; col < grid[0].Count; col++)
            {
                //  found an antenna
                if (grid[row][col] != '.')
                {
                    Console.WriteLine($"Found Antenna {grid[row][col]} at ({row}, {col})");

                    FindNextAntennaP1(grid[row][col], row, col, grid, positions);
                    Console.WriteLine();
                }
            }
        }

        var uniquePositions = positions.Distinct().ToList();
        Console.WriteLine($"Unique positions: {uniquePositions.Count}");
    }

    static void FindNextAntennaP1(char antenna, int originalRow, int originalCol, List<List<char>> grid,
        List<(int, int)> positions)
    {
        for (int row = 0; row < grid.Count; row++)
        {
            for (int col = 0; col < grid[0].Count; col++)
            {
                //  found an antenna
                if (grid[row][col] != '.' && grid[row][col] == antenna && col != originalCol && row != originalRow)
                {
                    DetermineAntidnodeLocationP1((originalRow, originalCol), (row, col), positions, grid);
                }
            }
        }
    }

    static void DetermineAntidnodeLocationP1((int row, int col) pOne, (int row, int col) pTwo,
        List<(int, int)> positions, List<List<char>> grid)
    {
        // Calculate the difference in row and column
        int deltaRow = pOne.row - pTwo.row;
        int deltaCol = pOne.col - pTwo.col;

        // Determine the location of the antinode
        int locRow = pOne.row + (deltaRow == 0 ? 0 : -deltaRow * 2);
        int locCol = pOne.col + (deltaCol == 0 ? 0 : -deltaCol * 2);

        // Check if the location is within grid bounds
        if (locRow >= 0 && locRow < grid.Count && locCol >= 0 && locCol < grid[0].Count)
        {
            Console.WriteLine($"Found Next Antenna at ({pTwo.col}, {pTwo.row}) antidode at ({locRow}, {locCol})");
            positions.Add((locRow, locCol));
        }
        else
        {
            Console.WriteLine($"Found Next Antenna at ({pTwo.col}, {pTwo.row}) No antidode at ({locRow}, {locCol})");
        }
    }

    // static void DetermineAntidodeLocationP1((int row, int col) pOne, (int row, int col) pTwo,
    //     List<(int, int)> positions, List<List<char>> grid)
    // {
    //     int locRow = 0;
    //     int locCol = 0;
    //     var height = pOne.row - pTwo.row;
    //     var width = pOne.col - pTwo.col;
    //
    //     if (height < 0)
    //     {
    //         locRow = pOne.row + Math.Abs(height) * 2;
    //     }
    //
    //     if (width > 0)
    //     {
    //         locCol = pOne.col - width * 2;
    //     }
    //
    //     if (height > 0)
    //     {
    //         locRow = pOne.row - height * 2;
    //     }
    //
    //     if (width < 0)
    //     {
    //         locCol = pOne.col + Math.Abs(width) * 2;
    //     }
    //
    //     if (height == 0)
    //     {
    //         locRow = pOne.row;
    //     }
    //
    //     if (width == 0)
    //     {
    //         locCol = pOne.col;
    //     }
    //
    //     if (locRow >= 0 && locCol >= 0 && locRow <= grid.Count- 1 && locCol <= grid[0].Count - 1)
    //     {
    //         Console.WriteLine($"Found Next Antenna at ({pTwo.col}, {pTwo.row}) antidode at ({locRow}, {locCol})");
    //         positions.Add((locRow, locCol));
    //     }
    //     else
    //     {
    //         Console.WriteLine($"Found Next Antenna at ({pTwo.col}, {pTwo.row}) No antidode at ({locRow}, {locCol})");
    //     }
    // }

    public static void PartTwo()
    {
        var grid = ReadInput();

        List<(int, int)> positions = [];

        for (int row = 0; row < grid.Count; row++)
        {
            for (int col = 0; col < grid[0].Count; col++)
            {
                //  found an antenna
                if (grid[row][col] != '.')
                {
                    Console.WriteLine($"Found Antenna {grid[row][col]} at ({row}, {col})");

                    FindNextAntennaP2(grid[row][col], row, col, grid, positions);
                    Console.WriteLine();
                }
            }
        }

        var uniquePositions = positions.Distinct().ToList();
        Console.WriteLine($"Unique positions: {uniquePositions.Count}");

        static void FindNextAntennaP2(char antenna, int originalRow, int originalCol, List<List<char>> grid,
            List<(int, int)> positions)
        {
            for (int row = 0; row < grid.Count; row++)
            {
                for (int col = 0; col < grid[0].Count; col++)
                {
                    //  found an antenna
                    if (grid[row][col] != '.' && grid[row][col] == antenna && col != originalCol && row != originalRow)
                    {
                        positions.Add((row, col));
                        positions.Add((originalRow, originalCol));
                        DetermineAntinodeLocationsP2((originalRow, originalCol), (row, col), positions, grid);
                    }
                }
            }
        }

        static void DetermineAntinodeLocationsP2((int row, int col) pOne, (int row, int col) pTwo,
            List<(int, int)> positions, List<List<char>> grid)
        {
            while (true)
            {
                int locRow = 0;
                int locCol = 0;
                var height = pOne.row - pTwo.row;
                var width = pOne.col - pTwo.col;

                if (height < 0)
                {
                    locRow = pTwo.row + Math.Abs(height);
                }

                if (width > 0)
                {
                    locCol = pTwo.col - width;
                }

                if (height > 0)
                {
                    locRow = pTwo.row - height;
                }

                if (width < 0)
                {
                    locCol = pTwo.col + Math.Abs(width);
                }

                if (height == 0)
                {
                    locRow = pOne.row;
                }

                if (width == 0)
                {
                    locCol = pOne.col;
                }

                if (locRow >= 0 && locCol >= 0 && locRow <= grid.Count - 1 && locCol <= grid[0].Count - 1)
                {
                    Console.WriteLine(
                        $"Found Next Antenna at ({pTwo.row}, {pTwo.col}) antidode at ({locRow}, {locCol})");
                    positions.Add((locRow, locCol));
                    pOne = pTwo;
                    pTwo = (locRow, locCol);
                }
                else
                {
                    Console.WriteLine(
                        $"Out Found Next Antenna at ({pTwo.col}, {pTwo.row}) No antidode at ({locRow}, {locCol})");
                    break;
                }
            }
        }
    }
}
