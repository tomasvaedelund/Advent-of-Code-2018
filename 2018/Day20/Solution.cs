using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day20
{
    class Solution : Solver
    {
        public string GetName() => "Day20";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            var doors = GenerateDoors(input);
            var result = doors.Select(x => x.to.d).Max();

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            var doors = GenerateDoors(input);
            var result = doors.Select(x => x.from.d).Where(x => x >= 1000).Count();

            // 9471 >
            // 9332 >
            // 9284 ?
            // 9288 ?
            return ($"{result - diff}", timer.ElapsedMilliseconds);
        }

        public int Solver(string input)
        {
            var doors = GenerateDoors(input);

            return 0;
        }

        public IEnumerable<((int x, int y, int d) from, (int x, int y, int d) to)> GenerateDoors(string input)
        {
            var result = new List<((int x, int y, int d) from, (int x, int y, int d) to)>();
            var stack = new Stack<(int x, int y, int d)>();
            (int x, int y, int d) next = (0, 0, 0);
            foreach (var c in input)
            {
                var last = next;
                switch (c)
                {
                    case 'N':
                        next = (last.x - 1, last.y, last.d + 1);
                        break;
                    case 'E':
                        next = (last.x, last.y + 1, last.d + 1);
                        break;
                    case 'S':
                        next = (last.x + 1, last.y, last.d + 1);
                        break;
                    case 'W':
                        next = (last.x, last.y - 1, last.d + 1);
                        break;
                    case '(':
                        stack.Push(next);
                        break;
                    case '|':
                        next = stack.Peek();
                        break;
                    case ')':
                        next = stack.Pop();
                        break;
                }

                if ("NESW".IndexOf(c) >= 0)
                {
                    result = GetLastNext(last, next, result);
                }
            }

            return result;
        }

        int diff = 0;
        private List<((int x, int y, int d) from, (int x, int y, int d) to)> GetLastNext((int x, int y, int d) last, (int x, int y, int d) next, List<((int x, int y, int d) from, (int x, int y, int d) to)> result)
        {
            var existingFroms = result.Where(r => r.from.x == next.x && r.from.y == next.y);
            if (existingFroms.Any())
            {
                var minD = Math.Min(next.d, existingFroms.Select(e => e.from.d).Min());

                if (next.d >= 1000 && minD < 1000)
                {
                    diff += (next.d > minD) ? next.d - minD : minD - next.d;
                }

                next.d = minD;
                last.d = minD - 1;

                existingFroms.ToList().ForEach(existing =>
                {
                    if (existing.from.d >= 1000 && minD < 1000)
                    {
                        diff += (existing.from.d > minD) ? existing.from.d - minD : minD - existing.from.d;
                    }

                    existing.to.d = minD;
                    existing.from.d = minD - 1;
                });
            }

            result.Add((last, next));
            return result;
        }
    }
}