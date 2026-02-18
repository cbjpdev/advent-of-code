namespace Aoc2024;

public static class Day6
{
    private static List<List<char>> ReadInput()
    {
        string input = File.ReadAllText("Input/day6.txt");

        // Remove empty lines or extra spaces
        string[] grid = input.Trim().Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        // Convert grid to List<List<char>> if needed
        List<List<char>> charGrid = [];
        charGrid.AddRange(grid.Select(line => new List<char>(line)));

        return charGrid;
    }

    private static (int, int) GetGuard(List<List<char>> grid)
    {
        int row = -1, col = -1;
        for (int i = 0; i < grid.Count; i++)
        {
            int index = grid[i].IndexOf('^');
            if (index != -1)
            {
                row = i;
                col = index;
                break;
            }
        }

        return (row, col);
    }

    public static void PartOne()
    {
        var grid = ReadInput();
        (int row, int col) = GetGuard(grid);

        int loopCount = 0;

        int rowMax = grid.Count - 1;
        int columnMax = grid[0].Count - 1;

        while (true)
        {
            loopCount++;

            bool isOut = TraverseTop();
            PrintGrid();
            if (isOut) break;

            isOut = TraverseRight();
            PrintGrid();
            if (isOut) break;

            isOut = TraverseDown();
            PrintGrid();
            if (isOut) break;

            isOut = TraverseLeft();
            PrintGrid();
            if (isOut) break;


        }

        int count = grid.Sum(r => r.Count(cell => cell == 'X'));

        Console.WriteLine(count);
        Console.WriteLine(loopCount);
        return;

        bool TraverseTop()
        {
            for (; row >= 0; row--)
            {
                if (row == 0)
                {
                    grid[row][col] = 'X';
                    return true;
                }
                if (grid[row-1][col] == '#' )
                {
                    break;
                }

                grid[row][col] = 'X';
            }

            return false;
        }

        bool TraverseRight()
        {
            for (; col <= columnMax; col++)
            {
                if (col == columnMax)
                {
                    grid[row][col] = 'X';
                    return true;
                }

                if (grid[row][col+1] == '#')
                {
                    break;
                }
                grid[row][col] = 'X';
            }

            return false;
        }

        bool TraverseDown()
        {
            for (; row <= rowMax; row++)
            {
                if (row == rowMax)
                {
                    grid[row][col] = 'X';
                    return true;
                }

                if (grid[row+1][col] == '#')
                {
                    break;
                }
                grid[row][col] = 'X';
            }

            return false;
        }

        bool TraverseLeft()
        {
            for (; col >= 0; col--)
            {
                if (col == 0)
                {
                    grid[row][col] = 'X';
                    return true;
                }

                if (grid[row][col-1] == '#')
                {
                    break;
                }
                grid[row][col] = 'X';
            }

            return false;
        }

        void PrintGrid()
        {
            foreach (var line in grid)
            {
                foreach (char cell in line)
                {
                    Console.Write(cell);
                }
                Console.WriteLine();

            }
        }
    }

    public static void PartTwo()
    {
        var originalGrid = ReadInput();

        (int row, int col) = GetGuard(originalGrid);

        int loopCount = 0;

        int rowMax = originalGrid.Count - 1;
        int columnMax = originalGrid[0].Count - 1;

        for (int cloneRow = 0; cloneRow < originalGrid.Count; cloneRow++)
        {
            for (int cloneCol = 0; cloneCol < originalGrid[cloneRow].Count; cloneCol++)
            {
                if (originalGrid[cloneRow][cloneCol] == '.' && !(cloneRow == row && cloneCol == col))
                {
                    // Place obstruction
                    var gridCopy = CloneGrid(originalGrid);
                    gridCopy[cloneRow][cloneCol] = '#';

                    bool hasDup = TraverseGrid(gridCopy, row, col, rowMax, columnMax);

                    if (!hasDup)
                    {
                        continue;
                    }

                    loopCount++;
                }
            }
        }

        static List<List<char>> CloneGrid(List<List<char>> grid)
        {
            return grid.Select(row => (List<char>) [..row]).ToList();
        }

        static bool TraverseGrid(List<List<char>> grid, int row, int col, int rowMax, int columnMax)
        {
            List<(int, int)> revised = [];
            bool hasDuplicates;

            while (true)
            {
                bool isOut = TraverseTop();
                hasDuplicates = revised.Count != revised.Distinct().Count();
                if (isOut || hasDuplicates) break;

                isOut = TraverseRight();
                hasDuplicates = revised.Count != revised.Distinct().Count();
                if (isOut || hasDuplicates) break;

                isOut = TraverseDown();
                hasDuplicates = revised.Count != revised.Distinct().Count();
                if (isOut || hasDuplicates) break;

                isOut = TraverseLeft();
                hasDuplicates = revised.Count != revised.Distinct().Count();
                if (isOut || hasDuplicates) break;
            }

            return hasDuplicates;

            bool TraverseTop()
            {
                for (; row >= 0; row--)
                {
                    if (row == 0)
                    {
                        return true;
                    }
                    if (grid[row-1][col] == '#' )
                    {
                        AddIfNotLast(revised, (row, col));
                        break;
                    }
                }

                return false;
            }

            bool TraverseRight()
            {
                for (; col <= columnMax; col++)
                {
                    if (col == columnMax)
                    {
                        return true;
                    }

                    if (grid[row][col+1] == '#')
                    {
                        AddIfNotLast(revised, (row, col));
                        break;
                    }
                }

                return false;
            }

            bool TraverseDown()
            {
                for (; row <= rowMax; row++)
                {
                    if (row == rowMax)
                    {
                        return true;
                    }

                    if (grid[row+1][col] == '#')
                    {
                        AddIfNotLast(revised, (row, col));
                        break;
                    }
                }

                return false;
            }

            bool TraverseLeft()
            {
                for (; col >= 0; col--)
                {
                    if (col == 0)
                    {
                        return true;
                    }

                    if (grid[row][col-1] == '#')
                    {
                        AddIfNotLast(revised, (row, col));
                        break;
                    }
                }

                return false;
            }

            static void AddIfNotLast(List<(int, int)> list, (int, int) element)
            {
                if (list.Count == 0 || !list[^1].Equals(element))
                {
                    list.Add(element);
                }
            }
        }

        Console.WriteLine(loopCount);
    }
}
