using System;
using System.Linq;
using AdventOfCode.Days;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any())
            {
                Command(args);
            }
            else
            {
                // Day01.GetResult();
                // Day02.GetResult();
                // Day03.GetResult();
                // Day04.GetResult();
                // Day05.GetResult();
                // Day06.GetResult();
                // Day07.GetResult();
                // Day08.GetResult();
                // Day09.GetResult();
                // Day10.GetResult();
                // Day11.GetResult();
                // Runner.Run(new AdventOfCode.Y2018.Day11.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day12.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day13.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day14.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day15.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day16.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day17.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day18.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day19.Solution());
                Runner.Run(new AdventOfCode.Y2018.Day20.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day21.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day22.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day23.Solution());
                Runner.Run(new AdventOfCode.Y2018.Day24.Solution());
                // Runner.Run(new AdventOfCode.Y2018.Day25.Solution());
            }
        }

        static void Command(string[] args)
        {
            if (args.Any() && args.Length == 2)
            {
                switch (args[0])
                {
                    case "prep":
                        Prepare(args[1]);
                        return;
                    default:
                        Console.WriteLine($"Unknown command '{args[0]}'");
                        return;
                }
            }
        }

        private static void Prepare(string arg)
        {
            try
            {
                var yearDay = arg.Split('/', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

                if (yearDay.Count() != 2)
                {
                    Usage(arg, "Unknown format");
                    return;
                }

                var result = Preparer.Run(yearDay.ElementAt(0), yearDay.ElementAt(1));
                Message(result);

            }
            catch (Exception e)
            {
                Usage(arg, e.Message);
            }
        }

        private static void Usage(string arg, string message)
        {
            Console.WriteLine($"Error parsing argument '{arg}'. Message: '{message}");
        }

        private static void Message(string message)
        {
            Console.WriteLine(message);
        }
    }
}
