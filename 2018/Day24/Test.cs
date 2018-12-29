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

            ShouldReturnCorrectNumberOfUnits(sut, parsed, 1570, 51);

            Debug.Assert(true == true);
        }

        private void ShouldReturnCorrectNumberOfUnits(Solution sut, IEnumerable<Unit> units, int boost, int expected)
        {
            var fact = sut.Solver(units, boost).units;

            Debug.Assert(fact == expected);
        }

        private void ShouldSelectCorrectTargets(Solution sut, IEnumerable<Unit> units, int expected)
        {
            var fact = sut.Solver(units).units;

            Debug.Assert(fact == expected);
        }
    }
}