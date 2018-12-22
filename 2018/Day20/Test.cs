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

            // ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[0]);
            // ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[1]);
            // ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[2]);
            // ShouldGenerateCorrectMatrix(sut, input.Split("\r\n")[3]);

            ShouldCalculateCorrectMaxDistance(sut, "^WNE$", 3);
            ShouldCalculateCorrectMaxDistance(sut, "^ENWWW(NEEE|SSE(EE|N))$", 10);
            ShouldCalculateCorrectMaxDistance(sut, "^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$", 18);
            ShouldCalculateCorrectMaxDistance(sut, "^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$", 23);
            ShouldCalculateCorrectMaxDistance(sut, "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$", 31);

            // ShouldGenerateCorrectMatrix(sut, "^WNE$");
            // ShouldCalculateCorrectMaxDistance(sut, "^WNE$", 3);
            Debug.Assert(true == true);
        }

        private void ShouldCalculateCorrectMaxDistance(Solution sut, string input, int expected)
        {
            var current = new Point() { X = 0, Y = 0, Type = 'X' };
            var points = sut.GetAllPossiblePaths(input, new List<Point>() { current }, current);

            var fact = sut.CalculateMaxDoors(points);
            var test = sut.CalculateMaxDistance(points);

            Debug.Assert(fact == expected);
        }

        private void ShouldGenerateCorrectMatrix(Solution sut, string input)
        {
            var current = new Point() { X = 0, Y = 0, Type = 'X' };
            var points = sut.GetAllPossiblePaths(input, new List<Point>() { current }, current);

            var matrix = sut.GenerateMatrix(points);

            sut.PrintMatrix(matrix);

            //var breakme = true;
        }
    }
}