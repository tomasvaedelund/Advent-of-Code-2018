using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day16
{
    class Solution : Solver
    {
        public string GetName() => "Day16";

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

            var commands = new string[]
            {
                "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti",
                "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr"
            };

            var result = GetBehavioursCount(commands, parsed).Where(x => x >= 3).Count();

            return (result.ToString(), timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public int[] GetBehavioursCount(string[] commands, IEnumerable<(int[] before, int[] command, int[] after)> operations)
        {
            var result = new int[operations.Count()];

            for (int i = 0; i < operations.Count(); i++)
            {
                result[i] = GetBehaviours(commands, operations.ElementAt(i));
            }

            return result;
        }

        public int GetBehaviours(string[] commands, (int[] before, int[] command, int[] after) operation)
        {
            var count = 0;

            foreach (var c in commands)
            {
                var before = new List<int>(operation.before).ToArray();
                var command = operation.command;
                var after = operation.after;
                var result = typeof(SolutionExtensions).GetMethod(c).Invoke(null, new object[] { before, command[1], command[2], command[3] });

                count += (after.SequenceEqual(result as int[])) ? 1 : 0;
            }

            return count;
        }

        public IEnumerable<(int[] before, int[] command, int[] after)> ParseInput(string input)
        {
            var split = input.Split("\r\n");
            var result = new List<(int[] before, int[] command, int[] after)>();

            var operations = split
                .Select((d, i) => new { data = d, index = i })
                .GroupBy(p => p.index / 4)
                .Select(grp => grp.Select(g => g.data).ToArray())
                .ToArray();

            foreach (var op in operations)
            {
                if (op[0].Contains("###"))
                {
                    break;
                }

                var before = op[0].Replace("Before: ", "").Replace("[", "").Replace("]", "").Split(", ").Select(int.Parse).ToArray();
                var command = op[1].Split(" ").Select(int.Parse).ToArray();
                var after = op[2].Replace("After: ", "").Replace("[", "").Replace("]", "").Split(", ").Select(int.Parse).ToArray();

                result.Add((before, command, after));
            }

            return result;
        }
    }

    public static class SolutionExtensions
    {
        public static int[] addr(int[] o, int a, int b, int c)
        {
            o[c] = o[a] + o[b];
            return o;
        }

        public static int[] addi(int[] o, int a, int b, int c)
        {
            o[c] = o[a] + b;
            return o;
        }

        public static int[] mulr(int[] o, int a, int b, int c)
        {
            o[c] = o[a] * o[b];
            return o;
        }

        public static int[] muli(int[] o, int a, int b, int c)
        {
            o[c] = o[a] * b;
            return o;
        }

        public static int[] banr(int[] o, int a, int b, int c)
        {
            o[c] = o[a] & o[b];
            return o;
        }

        public static int[] bani(int[] o, int a, int b, int c)
        {
            o[c] = o[a] & b;
            return o;
        }

        public static int[] borr(int[] o, int a, int b, int c)
        {
            o[c] = o[a] | o[b];
            return o;
        }

        public static int[] bori(int[] o, int a, int b, int c)
        {
            o[c] = o[a] | b;
            return o;
        }

        public static int[] setr(int[] o, int a, int b, int c)
        {
            o[c] = o[a];
            return o;
        }

        public static int[] seti(int[] o, int a, int b, int c)
        {
            o[c] = a;
            return o;
        }

        public static int[] gtir(int[] o, int a, int b, int c)
        {
            o[c] = (a > o[b]) ? 1 : 0;
            return o;
        }

        public static int[] gtri(int[] o, int a, int b, int c)
        {
            o[c] = (o[a] > b) ? 1 : 0;
            return o;
        }

        public static int[] gtrr(int[] o, int a, int b, int c)
        {
            o[c] = (o[a] > o[b]) ? 1 : 0;
            return o;
        }

        public static int[] eqir(int[] o, int a, int b, int c)
        {
            o[c] = (a == o[b]) ? 1 : 0;
            return o;
        }

        public static int[] eqri(int[] o, int a, int b, int c)
        {
            o[c] = (o[a] == b) ? 1 : 0;
            return o;
        }

        public static int[] eqrr(int[] o, int a, int b, int c)
        {
            o[c] = (o[a] == o[b]) ? 1 : 0;
            return o;
        }
    }
}