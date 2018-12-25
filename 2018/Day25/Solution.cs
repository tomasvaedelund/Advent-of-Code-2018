using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day25
{
    class Solution : Solver
    {
        public string GetName() => "Day25";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            var stars = ParseInput(input);
            var counter = 0;

            while (true)
            {
                var start = stars.ElementAt(counter++);
                start.c = (start.c == 0) ? counter : start.c;

                stars = stars.GetConstellations(start).ToList();

                if (!stars.Any(s => s.c == 0))
                {
                    break;
                }
            }

            var result = stars.Select(s => s.c).Distinct().Count();

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public IEnumerable<(int x, int y, int z, int d, int c)> ParseInput(string input)
        {
            var lines = input.Split("\r\n");

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var n = line.Split(',').Select(int.Parse).ToArray();
                yield return (x: n[0], y: n[1], z: n[2], d: n[3], 0);
            }
        }
    }

    public static class SolutionExtensions
    {
        public static IEnumerable<(int x, int y, int z, int d, int c)> GetConstellations(this IEnumerable<(int x, int y, int z, int d, int c)> stars)
        {
            var counter = 1;
            for (int i = 0; i < stars.Count(); i++)
            {
                var current = stars.ElementAt(i);
                var starsWithinDistance = stars.Where(s => s.GetManhattanDistanceTo(current) <= 3 && s.c > 0);
                var test = stars.Where(s => s.GetManhattanDistanceTo(current) <= 3);
                if (starsWithinDistance.Any())
                {
                    current.c = starsWithinDistance.Select(s => s.c).Min();
                }
                else
                {
                    current.c = counter++;
                }

                stars = stars.GetConstellations(current).ToList();
            }

            return stars;
        }

        public static IEnumerable<(int x, int y, int z, int d, int c)> GetConstellations(this IEnumerable<(int x, int y, int z, int d, int c)> stars, (int x, int y, int z, int d, int c) start)
        {
            for (int s = 0; s < stars.Count(); s++)
            {
                var star = stars.ElementAt(s);

                if (star.c == 0 && start.GetManhattanDistanceTo(star) <= 3)
                {
                    star.c = start.c;
                }

                yield return star;
            };
        }

        public static int GetManhattanDistanceTo(this (int x, int y, int z, int d, int c) start, (int x, int y, int z, int d, int c) end)
        {
            var test = Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y) + Math.Abs(start.z - end.z) + Math.Abs(start.d - end.d);
            return Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y) + Math.Abs(start.z - end.z) + Math.Abs(start.d - end.d);
        }
    }
}