namespace Aoc2024;

public static class Day7
{
    public static void PartOne()
    {
        var lines = File.ReadLines("Input/day7.txt");

        BigInteger totalCalibration = 0;

        foreach (var line in lines)
        {
            var values = line.Split(':');
            var key = BigInteger.Parse(values[0]);
            var value = values[1].Trim().Split(' ')
                .Select(BigInteger.Parse)
                .ToList();

            BigInteger target = key;
            var numbers = value;

            Console.WriteLine($"Target: {target}, Numbers: {string.Join(" ", numbers)}");

            if (CanMakeTrue(numbers, target))
            {
                Console.WriteLine($"Equation can be true for target {target}!");
                totalCalibration += target;
            }
            else
            {
                Console.WriteLine($"Equation cannot be true for target {target}.");
            }
        }

        Console.WriteLine($"\nTotal Calibration Result: {totalCalibration}");

        static bool CanMakeTrue(List<BigInteger> numbers, BigInteger target)
        {
            int n = numbers.Count;
            int combinations = (int)Math.Pow(2, n - 1); // Each gap has 2 choices (+ or *)

            for (int i = 0; i < combinations; i++)
            {
                string expression = BuildExpression(numbers, i);
                if (EvaluateExpression(expression) == target)
                {
                    Console.WriteLine($"Valid expression: {expression}");
                    return true;
                }
            }

            return false;
        }

        static string BuildExpression(List<BigInteger> numbers, int combination)
        {
            string expression = numbers[0].ToString();
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                char op = (combination & (1 << i)) == 0 ? '+' : '*';
                expression += $" {op} {numbers[i + 1]}";
            }
            return expression;
        }

        static BigInteger EvaluateExpression(string expression)
        {
            var tokens = expression.Split(' ');
            BigInteger result = BigInteger.Parse(tokens[0]);

            for (int i = 1; i < tokens.Length; i += 2)
            {
                char op = tokens[i][0];
                BigInteger value = BigInteger.Parse(tokens[i + 1]);
                result = op == '+' ? result + value : result * value;
            }

            return result;
        }
    }

    public static void PartTwo()
    {
        // var testCases = new Dictionary<BigInteger, List<BigInteger>>
        // {
        //     { 190, new List<BigInteger> { 10, 19 } },
        //     { 3267, new List<BigInteger> { 81, 40, 27 } },
        //     { 83, new List<BigInteger> { 17, 5 } },
        //     { 156, new List<BigInteger> { 15, 6 } },
        //     { 7290, new List<BigInteger> { 6, 8, 6, 15 } },
        //     { 161011, new List<BigInteger> { 16, 10, 13 } },
        //     { 192, new List<BigInteger> { 17, 8, 14 } },
        //     { 21037, new List<BigInteger> { 9, 7, 18, 13 } },
        //     { 292, new List<BigInteger> { 11, 6, 16, 20 } }
        // };

        BigInteger totalCalibration = 0;

        var lines = File.ReadLines("Input/day7.txt");

        foreach (var line in lines)
        {
            var values = line.Split(':');
            var key = BigInteger.Parse(values[0]);
            var value = values[1].Trim().Split(' ')
                .Select(BigInteger.Parse)
                .ToList();

            Console.WriteLine($"Target: {key}, Numbers: {string.Join(" ", value)}");

            if (CanMakeTrue(value, key))
            {
                Console.WriteLine($"Equation can be true for target {key}!");
                totalCalibration += key;
            }
            else
            {
                Console.WriteLine($"Equation cannot be true for target {key}.");
            }
        }

        Console.WriteLine($"\nTotal Calibration Result: {totalCalibration}");


        static bool CanMakeTrue(List<BigInteger> numbers, BigInteger target)
        {
            int n = numbers.Count;
            int combinations = (int)Math.Pow(3, n - 1); // Each gap has 3 choices (+, *, ||)

            for (int i = 0; i < combinations; i++)
            {
                string expression = BuildExpression(numbers, i);
                if (EvaluateExpression(expression) == target)
                {
                    Console.WriteLine($"Valid expression: {expression}");
                    return true;
                }
            }

            return false;
        }

        static string BuildExpression(List<BigInteger> numbers, int combination)
        {
            string expression = numbers[0].ToString();
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                int operatorType = (combination / (int)Math.Pow(3, i)) % 3;
                string op = operatorType switch
                {
                    0 => "+",
                    1 => "*",
                    2 => "||",
                    _ => throw new InvalidOperationException("Invalid operator type")
                };
                expression += $" {op} {numbers[i + 1]}";
            }
            return expression;
        }

        static BigInteger EvaluateExpression(string expression)
        {
            var tokens = expression.Split(' ');
            BigInteger result = BigInteger.Parse(tokens[0]);

            for (int i = 1; i < tokens.Length; i += 2)
            {
                string op = tokens[i];
                BigInteger value = BigInteger.Parse(tokens[i + 1]);
                result = op switch
                {
                    "+" => result + value,
                    "*" => result * value,
                    "||" => BigInteger.Parse(result.ToString() + value.ToString()),
                    _ => throw new InvalidOperationException("Invalid operator")
                };
            }

            return result;
        }
    }
}
