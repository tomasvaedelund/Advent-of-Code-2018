using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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
            var parsed = ParseInput(input);
            var result = new int[] { 1, 0, 0, 0, 0, 0 };

            //result = RunProgram(parsed.pointer, parsed.commands, result);

            return ($"{result[0]}", timer.ElapsedMilliseconds);
        }

        public int[] RunProgram(int bound, IEnumerable<(string method, int[] instruction)> commands, int[] registry)
        {
            var pointer = 0;
            // var sb = new StringBuilder();
            var counter = 0;

            while (true)
            {
                // sb.AppendLine($"{pointer} [{string.Join(", ", registry)}]");

                counter++;
                if (pointer >= commands.Count() || counter > int.MaxValue)
                {
                    break;
                }

                var command = commands.ElementAt(pointer);

                registry[bound] = pointer;
                registry = RunCommand(command, registry);
                pointer = registry[bound];

                pointer++;
            }

            // WriteRegistry(sb);

            return registry;
        }

        private int[] RunCommand((string method, int[] instruction) command, int[] registry)
        {
            var instruction = command.instruction;
            var method = command.method;
            // return typeof(SolutionExtensions).GetMethod(method).Invoke(null, new object[] { registry, instruction[0], instruction[1], instruction[2] }) as int[];

            switch (method)
            {
                case "addr":
                    return registry.addr(instruction[0], instruction[1], instruction[2]);
                case "addi":
                    return registry.addi(instruction[0], instruction[1], instruction[2]);
                case "mulr":
                    return registry.mulr(instruction[0], instruction[1], instruction[2]);
                case "muli":
                    return registry.muli(instruction[0], instruction[1], instruction[2]);
                case "banr":
                    return registry.banr(instruction[0], instruction[1], instruction[2]);
                case "bani":
                    return registry.bani(instruction[0], instruction[1], instruction[2]);
                case "borr":
                    return registry.borr(instruction[0], instruction[1], instruction[2]);
                case "bori":
                    return registry.bori(instruction[0], instruction[1], instruction[2]);
                case "setr":
                    return registry.setr(instruction[0], instruction[1], instruction[2]);
                case "seti":
                    return registry.seti(instruction[0], instruction[1], instruction[2]);
                case "gtir":
                    return registry.gtir(instruction[0], instruction[1], instruction[2]);
                case "gtri":
                    return registry.gtri(instruction[0], instruction[1], instruction[2]);
                case "gtrr":
                    return registry.gtrr(instruction[0], instruction[1], instruction[2]);
                case "eqir":
                    return registry.eqir(instruction[0], instruction[1], instruction[2]);
                case "eqri":
                    return registry.eqri(instruction[0], instruction[1], instruction[2]);
                case "eqrr":
                    return registry.eqrr(instruction[0], instruction[1], instruction[2]);
                default:
                    throw new Exception($"Unknown method: '{command.method}'");
            }
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

        private void WriteRegistry(StringBuilder sb)
        {
            using (var sw = new StreamWriter(@"c:\temp\day19.txt", false))
            {
                sw.Write(sb.ToString());
            }
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
        public static int[] addr(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] + o[b];
            return o;
        }

        public static int[] addi(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] + b;
            return o;
        }

        public static int[] mulr(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] * o[b];
            return o;
        }

        public static int[] muli(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] * b;
            return o;
        }

        public static int[] banr(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] & o[b];
            return o;
        }

        public static int[] bani(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] & b;
            return o;
        }

        public static int[] borr(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] | o[b];
            return o;
        }

        public static int[] bori(this int[] o, int a, int b, int c)
        {
            o[c] = o[a] | b;
            return o;
        }

        public static int[] setr(this int[] o, int a, int b, int c)
        {
            o[c] = o[a];
            return o;
        }

        public static int[] seti(this int[] o, int a, int b, int c)
        {
            o[c] = a;
            return o;
        }

        public static int[] gtir(this int[] o, int a, int b, int c)
        {
            o[c] = (a > o[b]) ? 1 : 0;
            return o;
        }

        public static int[] gtri(this int[] o, int a, int b, int c)
        {
            o[c] = (o[a] > b) ? 1 : 0;
            return o;
        }

        public static int[] gtrr(this int[] o, int a, int b, int c)
        {
            o[c] = (o[a] > o[b]) ? 1 : 0;
            return o;
        }

        public static int[] eqir(this int[] o, int a, int b, int c)
        {
            o[c] = (a == o[b]) ? 1 : 0;
            return o;
        }

        public static int[] eqri(this int[] o, int a, int b, int c)
        {
            o[c] = (o[a] == b) ? 1 : 0;
            return o;
        }

        public static int[] eqrr(this int[] o, int a, int b, int c)
        {
            o[c] = (o[a] == o[b]) ? 1 : 0;
            return o;
        }
    }
}