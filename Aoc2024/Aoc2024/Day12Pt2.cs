namespace Aoc2024;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Day12Pt2
{
	static List<string> garden = [];
	static List<List<bool>> processed = [];

	static int width = 0;
	static int height = 0;

	static char currentSymbol = ' ';
	static int areaSize = 0;

	static Dictionary<int, List<int>> topBorderPlots = new();
	static Dictionary<int, List<int>> bottomBorderPlots = new();
	static Dictionary<int, List<int>> leftBorderPlots = new();
	static Dictionary<int, List<int>> rightBorderPlots = new();

	static int result = 0;

	public static void Main()
	{
		ProcessInput();

		for (int row = 0; row < height; row++)
		{
			for (int col = 0; col < width; col++)
			{
				ProcessPlot(row, col);
			}
		}

		Console.WriteLine($"The answer is {result}");

	    static void ProcessInput()
	    {
		    string input = File.ReadAllText("Input/day12.txt").Trim();
		    garden = input.Split("\n").Select(line => line.Trim()).ToList();

		    height = garden.Count;
		    width = garden[0].Length;

		    processed = garden.Select(line => new List<bool>(new bool[line.Length])).ToList();
	    }

	    static void ProcessPlot(int row, int col)
	    {
		    if (processed[row][col])
			    return;

		    processed[row][col] = true;
		    currentSymbol = garden[row][col];

		    areaSize = 0;
		    topBorderPlots = new();
		    bottomBorderPlots = new();
		    leftBorderPlots = new();
		    rightBorderPlots = new();

		    WalkFrom(row, col);

		    result += areaSize * FindNumberOfSides();
	    }

	    static void WalkFrom(int row, int col)
	    {
		    Stack<(int Row, int Col)> pointsToWalk = new();
		    pointsToWalk.Push((row, col));

		    while (pointsToWalk.Count > 0)
		    {
			    var point = pointsToWalk.Pop();
			    areaSize++;

			    TryCatchNeighbor(point.Row, point.Col, -1, 0, pointsToWalk);
			    TryCatchNeighbor(point.Row, point.Col, 1, 0, pointsToWalk);
			    TryCatchNeighbor(point.Row, point.Col, 0, -1, pointsToWalk);
			    TryCatchNeighbor(point.Row, point.Col, 0, 1, pointsToWalk);
		    }
	    }

	    static void TryCatchNeighbor(int baseRow, int baseCol, int deltaRow, int deltaCol, Stack<(int Row, int Col)> pointsToWalk)
	    {
		    int neighborRow = baseRow + deltaRow;
		    int neighborCol = baseCol + deltaCol;

		    if (neighborRow < 0)
		    {
			    PushToTopBorderPlots(baseRow, baseCol);
			    return;
		    }
		    if (neighborCol < 0)
		    {
			    PushToLeftBorderPlots(baseRow, baseCol);
			    return;
		    }
		    if (neighborRow == height)
		    {
			    PushToBottomBorderPlots(baseRow, baseCol);
			    return;
		    }
		    if (neighborCol == width)
		    {
			    PushToRightBorderPlots(baseRow, baseCol);
			    return;
		    }

		    if (garden[neighborRow][neighborCol] != currentSymbol)
		    {
			    if (neighborRow < baseRow) PushToTopBorderPlots(baseRow, baseCol);
			    if (neighborCol < baseCol) PushToLeftBorderPlots(baseRow, baseCol);
			    if (neighborRow > baseRow) PushToBottomBorderPlots(baseRow, baseCol);
			    if (neighborCol > baseCol) PushToRightBorderPlots(baseRow, baseCol);
			    return;
		    }

		    if (processed[neighborRow][neighborCol]) return;

		    processed[neighborRow][neighborCol] = true;
		    pointsToWalk.Push((neighborRow, neighborCol));
	    }

	    static void PushToTopBorderPlots(int row, int col)
	    {
		    if (!topBorderPlots.ContainsKey(row)) topBorderPlots[row] = [];
		    topBorderPlots[row].Add(col);
	    }

	    static void PushToBottomBorderPlots(int row, int col)
	    {
		    if (!bottomBorderPlots.ContainsKey(row)) bottomBorderPlots[row] = [];
		    bottomBorderPlots[row].Add(col);
	    }

	    static void PushToLeftBorderPlots(int row, int col)
	    {
		    if (!leftBorderPlots.ContainsKey(col)) leftBorderPlots[col] = [];
		    leftBorderPlots[col].Add(row);
	    }

	    static void PushToRightBorderPlots(int row, int col)
	    {
		    if (!rightBorderPlots.ContainsKey(col)) rightBorderPlots[col] = [];
		    rightBorderPlots[col].Add(row);
	    }

        static int FindNumberOfSides()
        {
            int sides = 0;

            foreach (var dict in new[] { topBorderPlots, bottomBorderPlots, leftBorderPlots, rightBorderPlots })
            {
                sides += FindNumberOfSidesThis(dict);
            }

            return sides;
        }

        static int FindNumberOfSidesThis(Dictionary<int, List<int>> dict)
        {
            int sides = 0;

            foreach (var list in dict.Values)
            {
                sides += FindNumberOfSidesThisList(list);
            }

            return sides;
        }

        static int FindNumberOfSidesThisList(List<int> list)
        {
            list.Sort(); // Sort the list in ascending order

            List<int> newList = new();

            foreach (int candidate in list)
            {
                if (newList.Count == 0) // If the list is empty
                {
                    newList.Add(candidate);
                }
                else
                {
                    int previous = newList[^1]; // Get the last element

                    if (candidate - previous == 1) // Neighbor condition
                    {
                        newList.RemoveAt(newList.Count - 1); // Remove last element
                    }

                    newList.Add(candidate); // Add candidate regardless
                }
            }

            return newList.Count;
        }
	}
}
