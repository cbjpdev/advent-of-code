namespace aoc2025;

public static class Day01
{
    public static void Part1()
    {
        string[] input = File.ReadAllLines("Inputs/day01.txt").Select(x => x).ToArray();

        int zeroCount = 0;
        int startingPoint = 50;

        foreach (string rotateDetails in input)
        {
            startingPoint = ProcessPart01(rotateDetails, startingPoint);
            if (startingPoint % 100 == 0) zeroCount++;
            
            Console.WriteLine(startingPoint);
        }

        Console.WriteLine(zeroCount);
    }

    public static int ProcessPart01(string rotateDetails, int startingPoint)
    {
        string rotationDirection = rotateDetails[0].ToString();
        int rotations = int.Parse(rotateDetails[1..]);

        int rotationAmount = rotations % 100;
        
        if (rotationDirection == "R")
        {
            startingPoint += rotationAmount;
        }
        else
        {
            startingPoint -= rotationAmount;
        }
        
        if (startingPoint < 0) startingPoint += 100;
        if (startingPoint > 100) startingPoint -= 100;
        if (startingPoint == 100) startingPoint = 0;

        return startingPoint;
    }

    public static void Part2()
    {
        string[] input = File.ReadAllLines("Inputs/day01.txt").Select(x => x).ToArray();

        int zeroCount = 0;
        int startingPoint = 50;
        int rountTripsCount = 0;
        int passingZeroRightCount = 0;
        int passingZeroLeftCount = 0;

        foreach (string rotateDetails in input)
        { 
            string direction = rotateDetails[0].ToString();
            int rotations = int.Parse(rotateDetails[1..]);
            int roundTrips = rotations / 100;
            int rotationAmount = rotations % 100;

            if (direction == "R")
            {
                startingPoint += rotationAmount;
            }
            else
            {
                startingPoint -= rotationAmount;
            }

            if (startingPoint > 100)
            {
                startingPoint -= 100;
                passingZeroRightCount++;
            }

            if (startingPoint < 0)
            {
                startingPoint += 100;
                passingZeroLeftCount++;
            }
            
            if (startingPoint == 100) startingPoint = 0;

            if (roundTrips > 0)
            {
                Console.WriteLine($"Round trip detected: {rotateDetails}, round trips: {roundTrips}");
                rountTripsCount += roundTrips;
            }
            if (startingPoint % 100 == 0) zeroCount++;
        }

        Console.WriteLine(zeroCount);
        Console.WriteLine(rountTripsCount);
        Console.WriteLine(passingZeroRightCount);
        Console.WriteLine(passingZeroLeftCount);
        Console.WriteLine(zeroCount + rountTripsCount + passingZeroRightCount + passingZeroLeftCount);
    }
    
    public static void Part02()
    {
        string[] input = File.ReadAllLines("Inputs/day01.txt").Select(x => x).ToArray();

        int answer = 0;
        int dial = 1000000000 + 50;

        foreach (string rotateDetails in input)
        { 
            string direction = rotateDetails[0].ToString();
            int rotateBy = int.Parse(rotateDetails[1..]);

            for (int i = 0; i < rotateBy; i++)
            {
                if (direction == "R")
                {
                    dial += 1;
                }
                else
                {
                    dial -= 1;
                }
                
                if ((dial % 100) == 0)
                {
                    answer++;
                }
            }
        }

        Console.WriteLine(answer);
    }
}