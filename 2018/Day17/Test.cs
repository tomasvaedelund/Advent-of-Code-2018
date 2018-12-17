using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day17
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();

            var matrix = sut.ParseInput(input);

            Debug.Assert(true == true);
        }
    }
}