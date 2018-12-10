using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day10
    {
        public static void GetResult()
        {
            // Init
            var day = "10";

            // Start
            var testData = Helpers.GetDataFromFile($"day{day}_test.txt");
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(Solve1(testData) == 3);

            stopWatch = Stopwatch.StartNew();
            result = Solve1(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // // Second star
            // Debug.Assert(true == true);

            // stopWatch = Stopwatch.StartNew();
            // result = Solve2(data).ToString();
            // Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int Solve1(string data)
        {
            var points = data.Split("\r\n").Select(p => new Point(p)).ToList();
            var tempPoints = new List<Point>(points.Select(p => new Point(p)));

            var counter = 0;
            while (true)
            {
                var minX = points.Min(p => p.X);
                var minY = points.Min(p => p.Y);
                var maxX = points.Max(p => p.X);
                var maxY = points.Max(p => p.Y);

                points.ForEach(point =>
                {
                    point.X += point.Vx;
                    point.Y += point.Vy;
                });

                var curMinX = points.Min(p => p.X);
                var curMinY = points.Min(p => p.Y);
                var curMaxX = points.Max(p => p.X);
                var curMaxY = points.Max(p => p.Y);

                if (curMinX < minX || curMinY < minY || curMaxX > maxX || curMaxY > maxY)
                {
                    break;
                }

                tempPoints = new List<Point>(points.Select(p => new Point(p)));
                counter++;
            }

            DisplayMessage(tempPoints, counter);

            return counter;
        }

        private static void DisplayMessage(IEnumerable<Point> points, int counter)
        {
            var minX = points.Min(p => p.X) - 1;
            var minY = points.Min(p => p.Y) - 1;
            var maxX = points.Max(p => p.X) + 1;
            var maxY = points.Max(p => p.Y) + 1;

            Console.WriteLine($"{counter}{new String('~', maxX - minX + 1 - counter.ToString().Length)}");

            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var point = points.FirstOrDefault(p => p.X == x && p.Y == y);
                    if (point == null)
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write('#');
                    }
                }

                Console.WriteLine();
            }
        }

        private static int Solve2(string data)
        {
            return 0;
        }

        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Vx { get; set; }
            public int Vy { get; set; }

            public Point(string point)
            {
                var p = point.Split(new string[] { "=<", ", ", ">" }, StringSplitOptions.RemoveEmptyEntries);

                X = int.Parse(p[1]);
                Y = int.Parse(p[2]);
                Vx = int.Parse(p[4]);
                Vy = int.Parse(p[5]);
            }

            public Point(Point point)
            {
                X = point.X;
                Y = point.Y;
                Vx = point.Vx;
                Vy = point.Vy;
            }
        }
    }
}