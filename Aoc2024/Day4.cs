namespace Aoc2024;

public static class Day4
{
    private static string[] ReadInput()
    {
        var input = File.ReadAllText("Input/day4.txt");

        // Remove empty lines or extra spaces
        string[] grid = input.Trim().Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        // Convert grid to List<List<char>> if needed
        List<List<char>> charGrid = [];
        charGrid.AddRange(grid.Select(line => new List<char>(line)));

        return grid;
    }

    public static void Test1()
    {
        var input = ReadInput();
        T2(input);
    }

    public static void T1(string[] grid)
    {
        string target = "XMAS";
        int count = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        // Helper function to check for word in all directions
        bool CheckDirection(int r, int c, int dr, int dc)
        {
            for (int i = 0; i < target.Length; i++)
            {
                int nr = r + i * dr; // Next row
                int nc = c + i * dc; // Next column

                if (nr < 0 || nr >= rows || nc < 0 || nc >= cols || grid[nr][nc] != target[i])
                    return false;
            }

            return true;
        }

        // Directions: (row step, col step)
        // Horizontal, vertical, and diagonal directions (8 in total)
        int[,] directions =
        {
            { 0, 1 }, // Right
            { 0, -1 }, // Left
            { 1, 0 }, // Down
            { -1, 0 }, // Up
            { 1, 1 }, // Diagonal Down-Right
            { -1, -1 }, // Diagonal Up-Left
            { 1, -1 }, // Diagonal Down-Left
            { -1, 1 } // Diagonal Up-Right
        };

        // Traverse the grid
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                // Check all directions from the current position
                for (int d = 0; d < directions.GetLength(0); d++)
                {
                    int dr = directions[d, 0];
                    int dc = directions[d, 1];
                    if (CheckDirection(r, c, dr, dc))
                        count++;
                }
            }
        }

        Console.WriteLine($"The word '{target}' appears {count} times in all directions.");
    }

    public static void T2(string[] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int count = 0;

        // Function to check for an X-MAS pattern at a given center
        bool IsXMasPattern(int r, int c)
        {
            // Check boundaries
            if (r - 1 >= 0 && r + 1 < rows && c - 1 >= 0 && c + 1 < cols)
            {
                // Top diagonal M.S and bottom diagonal M.S
                bool forwardMas1 =
                    grid[r - 1][c - 1] == 'M' && grid[r - 1][c + 1] == 'S' &&
                    grid[r + 1][c - 1] == 'M' && grid[r + 1][c + 1] == 'S' &&
                    grid[r][c] == 'A';

                // Top diagonal S.M and bottom diagonal S.M
                bool backwardMas2 =
                    grid[r - 1][c - 1] == 'S' && grid[r - 1][c + 1] == 'M' &&
                    grid[r + 1][c - 1] == 'S' && grid[r + 1][c + 1] == 'M' &&
                    grid[r][c] == 'A';

                bool backwardMas3 =
                    grid[r - 1][c - 1] == 'M' && grid[r - 1][c + 1] == 'M' &&
                    grid[r + 1][c - 1] == 'S' && grid[r + 1][c + 1] == 'S' &&
                    grid[r][c] == 'A';

                bool backwardMas4 =
                    grid[r - 1][c - 1] == 'S' && grid[r - 1][c + 1] == 'S' &&
                    grid[r + 1][c - 1] == 'M' && grid[r + 1][c + 1] == 'M' &&
                    grid[r][c] == 'A';

                // Return true if either configuration is valid
                return forwardMas1 || backwardMas2 || backwardMas3 || backwardMas4;
            }

            return false;
        }

        // Traverse the grid
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (IsXMasPattern(r, c))
                {
                    count++;
                    Console.WriteLine($"X-MAS pattern found at center ({r}, {c})");
                }
            }
        }

        Console.WriteLine($"\nTotal X-MAS patterns found: {count}");
    }
}
