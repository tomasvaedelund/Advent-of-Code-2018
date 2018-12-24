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
            var result = "";

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public IEnumerable<((int x, int y) from, (int x, int y) to)> GenerateRoomsAndDoors(string input)
        {
            var result = new List<((int x, int y) from, (int x, int y) to)>();
            var stack = new Stack<(int x, int y)>();
            (int x, int y) next = (0, 0);
            foreach (var c in input)
            {
                var last = next;
                switch (c)
                {
                    case 'N':
                        next = (last.x - 1, last.y);
                        result.Add((last, next));
                        break;
                    case 'E':
                        next = (last.x, last.y + 1);
                        result.Add((last, next));
                        break;
                    case 'S':
                        next = (last.x + 1, last.y);
                        result.Add((last, next));
                        break;
                    case 'W':
                        next = (last.x, last.y - 1);
                        result.Add((last, next));
                        break;
                    case '(':
                        stack.Push(next);
                        break;
                    case '|':
                        next = stack.Peek();
                        result.Add((last, next));
                        break;
                    case ')':
                        next = stack.Pop();
                        result.Add((last, next));
                        break;
                }
            }

            return result;
        }
    }
}