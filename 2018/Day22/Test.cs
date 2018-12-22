using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day22
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var parsed = sut.ParseInput(input);

            ShouldGetCorrectType(sut, 0, 0, parsed, '.');
            ShouldGetCorrectType(sut, 1, 0, parsed, '=');
            ShouldGetCorrectType(sut, 0, 1, parsed, '.');
            ShouldGetCorrectType(sut, 1, 1, parsed, '|');
            ShouldGetCorrectType(sut, 10, 10, parsed, '.');

            ShouldGetCorrectRiskLevel(sut, parsed, 114);

            Debug.Assert(true == true);
        }

        private void ShouldGetCorrectRiskLevel(Solution sut, (int depth, int x, int y) parsed, int expected)
        {
            var cave = sut.GenerateCave(parsed.depth, parsed.x, parsed.y);
            var fact = sut.GetRiskLevel(cave, parsed.x, parsed.y);

            Debug.Assert(fact == expected);
        }

        private void ShouldGetCorrectType(Solution sut, int x, int y, (int depth, int x, int y) parsed, char expected)
        {
            var cave = sut.GenerateCave(parsed.depth, parsed.x, parsed.y);

            // sut.PrintCave(cave);

            var region = cave[y, x];
            var fact = region.Type;

            Debug.Assert(fact == expected);
        }
    }
}