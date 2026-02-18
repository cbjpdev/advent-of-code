using System.Linq;

namespace Aoc2024;

public static class Day11
{
    private static List<BigInteger> ReadInput()
    {
        var text = File.ReadAllText("Input/day11.txt");
        return text.Split(' ').Select(BigInteger.Parse).ToList();
    }

    public static void PartOne()
    {
        List<BigInteger> list = ReadInput();

        for (int x = 0; x < 25; x++)
        {
            var newList = new List<BigInteger>();
            foreach (var item in list)
            {
                newList.AddRange(Blink(item));
            }

            list = newList;
        }


        Console.WriteLine($"Sum of scores for all trailheads (part 1): {list.Count}");
        return;

        List<BigInteger> Blink(BigInteger value)
        {
            if (value == 0)
            {
                return [1];
            }

            if (HasEvenNumberOfDigits(value, out List<BigInteger> halves))
            {
                return halves;
            }

            return [value * 2024];
        }

        static bool HasEvenNumberOfDigits(BigInteger number, out List<BigInteger> halves)
        {
            // Convert number to string to determine digit count
            string numStr = number.ToString();
            int length = numStr.Length;

            // Check if the number of digits is even
            if (length % 2 != 0)
            {
                halves = null; // Cannot split if the digit count is odd
                return false;
            }

            // Split the number into two halves
            string firstHalf = numStr.Substring(0, length / 2);
            string secondHalf = numStr.Substring(length / 2);

            // Parse the halves into BigInteger and return
            halves =
            [
                BigInteger.Parse(firstHalf),
                BigInteger.Parse(secondHalf)
            ];
            return true;
        }
    }

    public static void PartTwo()
    {
        List<BigInteger> Blink(BigInteger value)
        {
            if (value == 0)
            {
                return [1];
            }

            if (HasEvenNumberOfDigits(value, out List<BigInteger> halves))
            {
                return halves;
            }

            return [value * 2024];
        }

        static bool HasEvenNumberOfDigits(BigInteger number, out List<BigInteger> halves)
        {
            // Convert number to string to determine digit count
            string numStr = number.ToString();
            int length = numStr.Length;

            // Check if the number of digits is even
            if (length % 2 != 0)
            {
                halves = null; // Cannot split if the digit count is odd
                return false;
            }

            // Split the number into two halves
            string firstHalf = numStr.Substring(0, length / 2);
            string secondHalf = numStr.Substring(length / 2);

            // Parse the halves into BigInteger and return
            halves =
            [
                BigInteger.Parse(firstHalf),
                BigInteger.Parse(secondHalf)
            ];
            return true;
        }

        List<BigInteger> line = ReadInput();

        Dictionary<BigInteger, long> lineDict = line.ToDictionary(n => n, _ => 1L);

        // Perform the iteration 75 times
        for (int i = 0; i < 25; i++)
        {
            Dictionary<BigInteger, long> tmp = new();

            foreach (var kvp in lineDict)
            {
                BigInteger num = kvp.Key;
                long count = kvp.Value;

                // Process each result from the blink function
                foreach (var nNew in Blink(num).Where(nNew => !tmp.TryAdd(nNew, count)))
                {
                    tmp[nNew] += count;
                }
            }

            // Update lineDict with the new values
            lineDict = tmp;
        }

        // Calculate the result
        long result = lineDict.Values.Sum();
        Console.WriteLine("Result: " + result);
    }
}
