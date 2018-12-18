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

            var result = GetAllMatchingMethods(commands, parsed);

            return (result.Where(x => x.methodsCount.Sum() >= 3).Count().ToString(), timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();

            var parsed = ParseInput(input);

            var commands = new string[]
            {
                "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti",
                "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr"
            };

            var matchingMethods = GetAllMatchingMethodsCombined(commands, parsed);

            // This gives (pen and paper...)
            // 11 = 14 = eqri
            // 05 = 8 = setr
            // 10 = 15 = eqrr
            // 07 = 13 = eqir
            // 15 = 11 = gtri
            // 13 = 12 = gtrr
            // 04 = 10 = gtir
            // 00 = 7 = bori
            // 02 = 4 = banr
            // 03 = 5 = bani
            // 14 = 2 = mulr
            // 08 = 9 = seti
            // 12 = 6 = borr
            // 01 = 3 = muli
            // 06 = 0 = addr
            // 09 = 1 = addi

            var matched = new Dictionary<int, string>()
            {
                { 0, "bori"},
                { 1, "muli"},
                { 2, "banr"},
                { 3, "bani"},
                { 4, "gtir"},
                { 5, "setr"},
                { 6, "addr"},
                { 7, "eqir"},
                { 8, "seti"},
                { 9, "addi"},
                { 10, "eqrr"},
                { 11, "eqri"},
                { 12, "borr"},
                { 13, "gtrr"},
                { 14, "mulr"},
                { 15, "gtri"}
            };

            var result = RunProgram(matched, input);

            return ($"{result[0]}", timer.ElapsedMilliseconds);
        }

        private int[] RunProgram(Dictionary<int, string> matched, string input)
        {
            var commands = ParseInputTwo(input);
            var result = new int[] { 0, 0, 0, 0 };

            foreach (var c in commands)
            {
                result = RunCommand(c, matched[c[0]], result);
            }

            return result;
        }

        private int[] RunCommand(int[] command, string method, int[] data)
        {
            return typeof(SolutionExtensions).GetMethod(method).Invoke(null, new object[] { data, command[1], command[2], command[3] }) as int[];
        }

        public IEnumerable<(int command, int[] methodsCount)> GetAllMatchingMethodsCombined(string[] methods, IEnumerable<(int[] before, int[] command, int[] after)> operations)
        {
            var result = new List<(int command, int[] methodsCount)>();

            var matchingMethods = GetAllMatchingMethods(methods, operations);

            for (int i = 0; i < 16; i++)
            {
                var methodsCount = matchingMethods
                    .Where(m => m.command == i)
                    .Select(x => x.methodsCount)
                    .Aggregate((q, w) =>
                    {
                        return q.Zip(w, (x, y) => x + y).ToArray();
                    });

                result.Add((i, methodsCount));
            }

            return result;
        }

        public IEnumerable<(int command, int[] methodsCount)> GetAllMatchingMethods(string[] methods, IEnumerable<(int[] before, int[] command, int[] after)> operations)
        {
            var result = new List<(int command, int[] methodsCount)>();

            foreach (var o in operations)
            {
                var matchingMethods = GetMatchingMethods(methods, o);

                result.Add((o.command[0], matchingMethods));
            }

            return result;

        }

        public int[] GetMatchingMethods(string[] methods, (int[] before, int[] command, int[] after) operation)
        {
            var methodsCount = new int[methods.Length];

            for (int i = 0; i < methods.Length; i++)
            {
                var before = new List<int>(operation.before).ToArray();
                var command = operation.command;
                var after = operation.after;
                var result = typeof(SolutionExtensions).GetMethod(methods[i]).Invoke(null, new object[] { before, command[1], command[2], command[3] });

                methodsCount[i] = (after.SequenceEqual(result as int[])) ? 1 : 0;
            }

            return methodsCount;
        }

        public IEnumerable<(int[] before, int[] command, int[] after)> ParseInput(string input)
        {
            var split = input.Split("\r\n").Take(3260);
            var result = new List<(int[] before, int[] command, int[] after)>();

            var operations = split
                .Select((d, i) => new { data = d, index = i })
                .GroupBy(p => p.index / 4)
                .Select(grp => grp.Select(g => g.data).ToArray())
                .ToArray();

            foreach (var op in operations)
            {
                var before = op[0].Replace("Before: ", "").Replace("[", "").Replace("]", "").Split(", ").Select(int.Parse).ToArray();
                var command = op[1].Split(" ").Select(int.Parse).ToArray();
                var after = op[2].Replace("After: ", "").Replace("[", "").Replace("]", "").Split(", ").Select(int.Parse).ToArray();

                result.Add((before, command, after));
            }

            return result;
        }

        public IEnumerable<int[]> ParseInputTwo(string input)
        {
            var split = input.Split("\r\n").Skip(3262);
            var result = new List<int[]>();

            foreach (var line in split)
            {
                result.Add(line.Split(' ').Select(int.Parse).ToArray());
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