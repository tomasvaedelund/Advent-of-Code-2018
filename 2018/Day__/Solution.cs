using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day__
{
    class Solution : Solver
    {
        public string GetName() => "Day__";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string, long)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string, long) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public (string, long) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }
    }
}