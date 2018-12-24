using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day23
{
    class Solution : Solver
    {
        public string GetName() => "Day23";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            var parsed = ParseInput(input);
            var max = parsed.OrderByDescending(p => p.r).First();

            var result = GetNumberOfReachedNanobots(max, parsed);

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            var parsed = ParseInput(input);
            var all = GetAllPoints(parsed).ToList();
            var inRange = all
                .Where(x => x.r == all.Max(y => y.r));

            var result = inRange
                .OrderByDescending(x => CalculatedDistance((0, 0, 0, 0), (x.x, x.y, x.z, 0)))
                .First();

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public IEnumerable<(int x, int y, int z, int r)> GetAllPoints(IEnumerable<(int x, int y, int z, int r)> bots)
        {
            var maxX = bots.OrderByDescending(b => b.x).First().x;
            var minX = bots.OrderBy(b => b.x).First().x;
            var maxY = bots.OrderByDescending(b => b.y).First().y;
            var minY = bots.OrderBy(b => b.y).First().y;
            var maxZ = bots.OrderByDescending(b => b.z).First().z;
            var minZ = bots.OrderBy(b => b.z).First().z;

            var best = 0;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    for (int z = minZ; z <= maxZ; z++)
                    {
                        var r = 0;
                        foreach (var bot in bots)
                        {
                            var d = CalculatedDistance((x, y, z, 0), bot);

                            if (d <= bot.r)
                            {
                                r++;
                            }
                        }

                        best = (best >= r) ? best : r;

                        if (r >= best)
                        {
                            yield return (x, y, z, r);
                        }
                    }
                }
            }
        }

        public int GetNumberOfReachedNanobots((int x, int y, int z, int r) start, IEnumerable<(int x, int y, int z, int r)> bots)
        {
            var counter = 0;

            foreach (var bot in bots)
            {
                var d = CalculatedDistance(start, bot);

                if (d <= start.r)
                {
                    counter++;
                }
            }

            return counter;
        }

        public int CalculatedDistance((int x, int y, int z, int r) start, (int x, int y, int z, int r) bot)
        {
            return Math.Abs(start.x - bot.x) + Math.Abs(start.y - bot.y) + Math.Abs(start.z - bot.z);
        }

        public IEnumerable<(int x, int y, int z, int r)> ParseInput(string input)
        {
            foreach (var line in input.Split("\r\n"))
            {
                var s = line
                    .Split(new string[] { "pos=<", ",", ">, r=" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                yield return (x: s[0], y: s[1], z: s[2], r: s[3]);
            }
        }
    }
}