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
            Debug.Assert(true == true);

            stopWatch = Stopwatch.StartNew();
            result = "".ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int GetLargestArea(string data)
        {
            var id = 1;
            var points = data.Split("\r\n").Select(p => new Point(p, id++)).ToHashSet();

            var maxX = points.Max(p => p.X) + 1;
            var maxY = points.Max(p => p.Y) + 1;

            var dataTable = new int[maxX, maxY];

            foreach (var point in points)
            {
                for (int x = 0; x < maxX; x++)
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        var closestPoint = GetIdOfClosestPoint(x, y, points);
                        dataTable[x, y] = closestPoint;
                    }
                }
            }

            var infiniteIds = GetInfiniteIds(dataTable);

            var result = dataTable
            .Cast<int>()
            .Where(x => x > 0 && x < int.MaxValue && !infiniteIds.Contains(x))
            .GroupBy(x => x)
            .Select(x => new { p = x.Key, c = x.Count() })
            .OrderByDescending(x => x.c);

            return result.First().c;
        }

        private static IEnumerable<int> GetInfiniteIds(int[,] dataTable)
        {
            var height = dataTable.GetLength(0) - 1;
            var width = dataTable.GetLength(1) - 1;

            var left = GetColumn(dataTable, 0);
            var right = GetColumn(dataTable, height);
            var top = GetRow(dataTable, 0);
            var bottom = GetRow(dataTable, width);

            var result = left
                .Union(right)
                .Union(top)
                .Union(bottom)
                .Where(p => p > 0 && p < int.MaxValue)
                .Distinct();

            return result;
        }

        private static int[] GetRow(int[,] matrix, int colNum)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, colNum])
                .ToArray();
        }

        private static int[] GetColumn(int[,] matrix, int rowNum)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNum, x])
                    .ToArray();
        }
        private static int GetIdOfClosestPoint(int x, int y, IEnumerable<Point> points)
        {
            var test = new Dictionary<Point, int>();

            foreach (var point in points)
            {
                var distance = GetDistance(x, y, point);
                test.Add(point, distance);
            }

            var minDistance = test.Min(p2 => p2.Value);
            var closestPoints = test.Where(p => p.Value == minDistance);

            return (closestPoints.Count() == 1) ? closestPoints.Single().Key.Id : int.MaxValue;
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
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Point(string point, int id)
        {
            Id = id;
            X = int.Parse(point.Split(", ")[0]);
            Y = int.Parse(point.Split(", ")[1]);
        }
    }
}