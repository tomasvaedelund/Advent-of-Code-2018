using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode
{
    interface Solver
    {
        string GetName();
        void Test(string input);
        IEnumerable<(string result, long time)> Solve(string input);
        (string result, long time) PartOne(string input);
        (string result, long time) PartTwo(string input);
    }

    static class SolverExtensions
    {
        public static string WorkingDir(this Solver solver)
        {
            var year = solver.GetType().FullName.Split('.')[1].Substring(1);
            var day = solver.GetType().FullName.Split('.')[2];
            return $"{Path.Combine(year, day)}\\";
        }
    }
}