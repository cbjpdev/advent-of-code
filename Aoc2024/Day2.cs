namespace Aoc2024;

public static class Day2
{
    private static List<List<int>>  ReadInput()
    {
        List<List<int>> list = [];
        list.AddRange(File.ReadAllLines("Input/day2.txt")
            .Select(line => line.Split(" ")
                .Select(int.Parse)
                .ToList()));

        return list;
    }
    public static void Test1()
    {
        var list = ReadInput();
        int count = 0;

        for (int index = 0; index < list.Count; index++)
        {
            List<int>? t = list[index];
            bool hasTrend = CheckTrend(t);
            if (hasTrend)
            {
                var correctDiff = CheckDifferences(t);

                if (correctDiff)
                {
                    count++;
                }
                else
                {
                    if (ReEvaluate(t))
                    {
                        count++;
                    }
                }
            }
            else
            {
                if (ReEvaluate(t))
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
        Console.ReadLine();
    }

    static bool ReEvaluate(List<int> list)
    {
        for (var j = 0; j < list.Count; j++)
        {
            var newList = new List<int>(list); // Create a copy of the original list
            newList.RemoveAt(j);
            var hasTrendX = CheckTrend(newList);

            if (hasTrendX)
            {
                bool correctDiffX = CheckDifferences(newList);

                if (correctDiffX)
                {
                    return true;
                }
            }
        }

        return false;
    }

    static bool CheckTrend(List<int> list)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;

        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] > list[i - 1])
                isDecreasing = false;
            else if (list[i] < list[i - 1])
                isIncreasing = false;
        }

        if (isIncreasing || isDecreasing)
            return true;

        return false;
    }

    static bool CheckDifferences(List<int> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            int diff = Math.Abs(list[i] - list[i - 1]);
            if (diff < 1 || diff > 3)
                return false;
        }
        return true;
    }
}
