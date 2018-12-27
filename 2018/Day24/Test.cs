using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day24
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var parsed = sut.ParseInput(input);

            ShouldSelectCorrectTargets(sut, parsed, 5216);

            Debug.Assert(true == true);
        }

        private void ShouldSelectCorrectTargets(Solution sut, IEnumerable<Unit> units, int expected)
        {
            var fact = sut.Solver(units);

            Debug.Assert(fact == expected);
        }
    }
}