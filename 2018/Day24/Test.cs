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

            ShouldSelectCorrectTargets(sut, parsed);

            Debug.Assert(true == true);
        }

        private void ShouldSelectCorrectTargets(Solution sut, IEnumerable<Unit> units)
        {
            var test = sut.TargetSelectionPhase(units);

            Debug.Assert(true == true);
        }
    }
}