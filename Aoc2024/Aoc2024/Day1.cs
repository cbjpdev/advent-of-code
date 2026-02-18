namespace Aoc2024;

public static class Day1
{
    private static (List<int>, List<int>) ReadInput()
    {
        List<int> list1 = [];
        List<int> list2 = [];

        foreach (string line in File.ReadAllLines("Input/day1.txt"))
        {
            var arr = line.Split("  ");
            list1.Add(Convert.ToInt32(arr[0]));
            list2.Add(Convert.ToInt32(arr[1]));
        }

        return (list1, list2);
    }
    public static void Test1()
    {
        var (list1, list2) = ReadInput();

        list1.Sort();
        list2.Sort();

        int sum = 0;

        for (int i = 0; i < list1.Count; i++)
        {
            sum += Math.Abs(list1[i] - list2[i]);
        }

        Console.WriteLine(sum);
        Console.ReadLine();
    }

    public static void Test2()
    {
        var (list1, list2) = ReadInput();

        int sum = 0;

        foreach (int value in list1)
        {
            int count = list2.Count(n => n == value);
            sum += count * value;
        }

        Console.WriteLine(sum);
        Console.ReadLine();
    }

}
