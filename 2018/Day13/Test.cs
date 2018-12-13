using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day13
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();

            Test1(sut, input);

            Debug.Assert(true == true);
        }

        private void Test1(Solution sut, string input)
        {
            var expected = "7,3";
            var fact = sut.PartOne(input);

            Debug.Assert(expected.Equals(fact.result));
        }
    }
}