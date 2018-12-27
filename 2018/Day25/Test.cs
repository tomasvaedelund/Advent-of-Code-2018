using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day25
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var parsed = sut.ParseInput(input);

            ShouldCalculateCorrectNumberOfConstellations(sut, parsed.Take(8), 2);
            ShouldCalculateCorrectNumberOfConstellations(sut, parsed.Skip(8).Take(10), 4);
            ShouldCalculateCorrectNumberOfConstellations(sut, parsed.Skip(18).Take(10), 3);
            ShouldCalculateCorrectNumberOfConstellations(sut, parsed.Skip(28).Take(10), 8);

            Debug.Assert(true == true);
        }

        private void ShouldCalculateCorrectNumberOfConstellations(Solution sut, IEnumerable<int[]> stars, int expected)
        {
            var fact = sut.GetNumberOfConstellations(stars);

            Debug.Assert(fact == expected);
        }
    }
}