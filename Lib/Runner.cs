using System;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode
{
    class Runner
    {
        public static void Run(Solver solver)
        {
            var file = $"{solver.WorkingDir()}input.in";
            var fileTest = $"{solver.WorkingDir()}input_test.in";
            var input = File.ReadAllText(file);
            var inputTest = File.ReadAllText(fileTest);
            var counter = 1;

            solver.Test(inputTest);

            foreach (var solution in solver.Solve(input))
            {
                Console.WriteLine($"{solver.GetName()} - {counter} Time: {solution.time}ms");
                Console.WriteLine($"{solver.GetName()} - {counter} Result: {solution.result}");
                counter++;
            }
        }
    }
}