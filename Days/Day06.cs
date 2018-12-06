using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day06
    {
        public static void GetResult()
        {
            // Init
            var day = "06";

            // Start
            var testData = Helpers.GetDataFromFile($"day{day}_test.txt");
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(GetLargestArea(testData) == 17);

            stopWatch = Stopwatch.StartNew();
            result = GetLargestArea(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(GetPartTwo(testData, 32) == 16);

            stopWatch = Stopwatch.StartNew();
            result = GetPartTwo(data, 10000).ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int GetPartTwo(string data, int maxTotalDistance)
        {
            var points = data.Split("\r\n").Select(p => new Point(p)).ToHashSet();

            var maxX = points.Max(p => p.X) + 1;
            var maxY = points.Max(p => p.Y) + 1;

            var dataTable = new int[maxX, maxY];

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var totalDistance = GetDistanceToAllPoints(x, y, points);
                    dataTable[x, y] = totalDistance;
                }
            }

            var result = dataTable
            .Cast<int>()
            .Where(x => x < maxTotalDistance)
            .Count();

            return result;
        }

        private static int GetDistanceToAllPoints(int x, int y, HashSet<Point> points)
        {
            var distance = 0;
            foreach (var point in points)
            {
                distance += GetDistance(x, y, point);
            }

            return distance;
        }

        private static int GetLargestArea(string data)
        {
            var points = data.Split("\r\n").Select(p => new Point(p)).ToHashSet();

            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);
            var minX = points.Min(p => p.X);
            var minY = points.Min(p => p.Y);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    var closestPoints = GetClosestPoints(x, y, points);
                    if (closestPoints.Count() == 1)
                    {
                        closestPoints.Single().Area++;
                        if (x == minX || y == minY || x == maxX || y == maxY)
                        {
                            closestPoints.Single().IsInfinite = true;
                        }
                    }
                }
            }

            var result = points
                .Where(p => p.IsInfinite == false)
                .OrderByDescending(p => p.Area)
                .First();

            return result.Area;
        }

        private static IEnumerable<Point> GetClosestPoints(int x, int y, IEnumerable<Point> points)
        {
            var test = new Dictionary<Point, int>();

            foreach (var point in points)
            {
                var distance = GetDistance(x, y, point);
                test.Add(point, distance);
            }

            var minDistance = test.Min(p2 => p2.Value);
            var closestPoints = test.Where(p => p.Value == minDistance);

            return closestPoints.Select(p => p.Key);
        }

        private static int GetDistance(int x, int y, Point p)
        {
            var a = Math.Abs(x - p.X);
            var b = Math.Abs(y - p.Y);

            return a + b;
        }
    }

    class Point
    {
        public int Area { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsInfinite { get; set; }

        public Point(string point)
        {
            X = int.Parse(point.Split(", ")[0]);
            Y = int.Parse(point.Split(", ")[1]);
        }
    }
}