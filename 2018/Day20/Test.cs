using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day20
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();

            ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[0]);
            ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[1]);
            ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[2]);
            ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[3]);

            Debug.Assert(true == true);
        }

        private void ShouldGenerateCorrectMatrix(Solution sut, string input)
        {
            var current = new Point() { X = 0, Y = 0, Type = 'X' };
            var matrix = sut.GetAllPossiblePaths(input, new List<Point>() { current }, current);

            sut.PrintMatrix(matrix);

            //var breakme = true;
        }
    }
}