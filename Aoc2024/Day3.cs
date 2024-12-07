namespace Aoc2024;

public static class Day3
{
    private static string  ReadInput()
    {
        var input = File.ReadAllText("Input/day3.txt");

        return input;
    }

    public static void Test1()
    {
        var input = ReadInput();
        var total = ProcessDoDont(input);
    }
    public static int ProcessString(string input)
    {

        // Regex to match "mul(number,number)" where number is 1-3 digits
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        // Find matches
        MatchCollection matches = Regex.Matches(input, pattern);

        // Calculate sum of products
        int totalSum = 0;

        Console.WriteLine("Matches and their products:");
        foreach (Match match in matches)
        {
            int num1 = int.Parse(match.Groups[1].Value);
            int num2 = int.Parse(match.Groups[2].Value);

            int product = num1 * num2;
            totalSum += product;

            Console.WriteLine($"{match.Value}: {num1} * {num2} = {product}");
        }

        Console.WriteLine($"\nTotal Sum: {totalSum}");

        return totalSum;
    }

    public static int ProcessDoDont(string input)
    {
        // Regex patterns for do(), don't(), and mul(x,x)
        string doPattern = @"\b\w*do\(\)";         // Matches "do()", "undo()", "redo()", etc.
        string dontPattern = @"\b\w*don't\(\)";    // Matches "don't()", "xdon't()", etc.
        string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";

        bool isEnabled = true; // Start with mul enabled by default
        int totalSum = 0;

        // Split input into tokens to process sequentially
        string[] tokens = Regex.Split(input, @"(?<=\))|(?<=\w\(\))"); // Ensure proper token splitting

        Console.WriteLine("Processing Tokens:");

        foreach (string token in tokens)
        {
            if (Regex.IsMatch(token, doPattern))
            {
                isEnabled = true; // Enable future mul instructions
                Console.WriteLine($"Found do(): Enabled = {isEnabled}");
            }
            else if (Regex.IsMatch(token, dontPattern))
            {
                isEnabled = false; // Disable future mul instructions
                Console.WriteLine($"Found don't(): Enabled = {isEnabled}");
            }
            else if (isEnabled) // Check mul instructions only when enabled
            {
                Match mulMatch = Regex.Match(token, mulPattern);
                if (mulMatch.Success)
                {
                    int num1 = int.Parse(mulMatch.Groups[1].Value);
                    int num2 = int.Parse(mulMatch.Groups[2].Value);
                    int product = num1 * num2;

                    totalSum += product;

                    Console.WriteLine($"Matched {mulMatch.Value}: {num1} * {num2} = {product}");
                }
            }
        }

        Console.WriteLine($"\nTotal Sum: {totalSum}");

        return totalSum;
    }
}
