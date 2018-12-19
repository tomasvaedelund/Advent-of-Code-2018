using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2018.Day19
{
    class Solution : Solver
    {
        public string GetName() => "Day19";

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
            var result = new int[] { 0, 0, 0, 0, 0, 0 };

            result = RunProgram(parsed.pointer, parsed.commands, result);

            return ($"{result[0]}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public int[] RunProgram(int bound, IEnumerable<(string method, int[] instruction)> commands, int[] registry)
        {
            var pointer = 0;

            while (true)
            {
                if (pointer >= commands.Count())
                {
                    break;
                }

                var command = commands.ElementAt(pointer);

                registry[bound] = pointer;
                registry = RunCommand(command, registry);
                pointer = registry[bound];

                pointer++;
            }

            return registry;
        }

        private int[] RunCommand((string method, int[] instruction) command, int[] registry)
        {
            var instruction = command.instruction;
            var method = command.method;
            return typeof(SolutionExtensions).GetMethod(method).Invoke(null, new object[] { registry, instruction[0], instruction[1], instruction[2] }) as int[];
        }

        public (int pointer, IEnumerable<(string method, int[] instruction)> commands) ParseInput(string input)
        {
            var lines = input.Split("\r\n");
            var commands = new List<(string method, int[] instruction)>();
            var pointer = 0;

            foreach (var line in lines)
            {
                var lineArray = line.Split(' ');

                if (line.StartsWith("#ip"))
                {
                    pointer = int.Parse(lineArray[1]);
                }
                else
                {
                    commands.Add((lineArray[0], lineArray.Skip(1).Select(int.Parse).ToArray()));
                }
            }

            return (pointer: pointer, commands: commands);
        }

        private void WriteRegistry(int pointer, int[] registry)
        {
            using (var sw = new StreamWriter(@"c:\temp\day19.txt", true))
            {
                sw.WriteLine($"ip={pointer} [{string.Join(", ", registry)}]");
            }
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