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

            var result = GetNumberOfConstellations(stars);

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }


        public int GetNumberOfConstellations(IEnumerable<int[]> stars)
        {
            var constellations = new List<List<int[]>>();

            foreach (var star in stars)
            {
                var starAlreadyInAConstellation = false;
                foreach (var constellation in constellations)
                {
                    if (constellation.Any(s => s.SequenceEqual(star)))
                    {
                        starAlreadyInAConstellation = true;
                    }
                }

                if (starAlreadyInAConstellation)
                {
                    continue;
                }

                var closeStars = new List<int[]>() { star };
                var foundNewCloseStar = true;
                while (foundNewCloseStar)
                {
                    foundNewCloseStar = false;
                    foreach (var candidate in stars)
                    {
                        if (closeStars.Any(s => s.SequenceEqual(candidate)))
                        {
                            continue;
                        }

                        foreach (var closeStar in closeStars.ToList())
                        {
                            if (ManhattanDistanceBetween(candidate, closeStar) <= 3)
                            {
                                closeStars.Add(candidate);
                                foundNewCloseStar = true;
                            }
                        }
                    }
                }

                constellations.Add(closeStars);
            }

            return constellations.Count;
        }

        public int ManhattanDistanceBetween(int[] a, int[] b) => Enumerable.Range(0, a.Length).Select(i => Math.Abs(a[i] - b[i])).Sum();

        public IEnumerable<int[]> ParseInput(string input)
        {
            var lines = input.Split("\r\n");

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                yield return line.Split(',').Select(int.Parse).ToArray();
            }
        }
    }
}