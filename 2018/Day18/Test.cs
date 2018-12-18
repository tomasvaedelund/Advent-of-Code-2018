using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day18
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var parsed = sut.ParseInput(input);

            var minuteOne = @".......##.
......|###
.|..|...#.
..|#||...#
..##||.|#|
...#||||..
||...|||..
|||||.||.|
||||||||||
....||..|.";

            var minuteFive = @"....|||#..
...||||#..
.|.##||||.
..####|||#
.|.###||#|
|||###||||
||||||||||
||||||||||
||||||||||
||||||||||";

            ShouldReturnCorrectMatrixAfterXMinutes(sut, parsed, minuteOne, 1);

            ShouldReturnCorrectMatrixAfterXMinutes(sut, parsed, minuteFive, 5);

            Test03(sut, parsed);

            Debug.Assert(true == true);
        }

        private void ShouldReturnCorrectMatrixAfterXMinutes(Solution sut, char[,] parsed, string expected, int minutes)
        {
            var matrix = sut.PassXMinutes(parsed, minutes);

            var fact = string.Join("", matrix.Cast<char>());
            expected = expected.Replace("\r\n", "");

            Debug.Assert(fact == expected);
        }

        private void Test03(Solution sut, char[,] parsed)
        {
            var matrix = sut.PassXMinutes(parsed, 10);

            //sut.PrintMatrix(matrix);

            var wooded = matrix.Cast<char>().Count(c => c == '|');
            var lumberyards = matrix.Cast<char>().Count(c => c == '#');
            var fact = wooded * lumberyards;
            var expected = 1147;

            Debug.Assert(fact == expected);
        }
    }
}