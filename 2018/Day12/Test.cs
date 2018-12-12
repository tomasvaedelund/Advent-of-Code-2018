using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day12
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();

            sut.IsTest = true;

            Test1(sut, input);

            Test2(sut, input);

            Test3(sut, input);

            Test4(sut, input);
        }

        private void Test1(Solution sut, string input)
        {
            var expected = "...#..#.#..##......###...###...........";
            var fact = sut.GetParsedInput(input).initialState;

            Debug.Assert(expected.Contains(fact));
        }

        private void Test2(Solution sut, string input)
        {
            var tester = ".#..###.#..#.#.#######.#.#.#..#.#...#..";
            var notes = sut.GetParsedInput(input).notes;
            var expected = ".#....##....#####...#######....#.#..##.";
            var fact = sut.GetNextGeneration(tester, notes);

            Debug.Assert(expected.Contains(fact) || fact.Contains(expected));
        }

        private void Test3(Solution sut, string input)
        {
            var tester = "...#..#.#..##......###...###...........";
            var notes = sut.GetParsedInput(input).notes;
            var expected = "...#...#....#.....#..#..#..#...........";
            var fact = sut.GetNextGeneration(tester, notes);

            Debug.Assert(expected.Contains(fact) || fact.Contains(expected));
        }

        private void Test4(Solution sut, string input)
        {
            var expected = "325";
            var fact = sut.PartOne(input).Item1;

            Debug.Assert(expected.Contains(fact));
        }
    }
}