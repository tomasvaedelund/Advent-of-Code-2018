using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day23
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var parsed = sut.ParseInput(input);

            ShouldCalculateCorrectNumberOfBotsInRange(sut, parsed, 7);

            ShouldCalculateCorrectCoordinate(sut, parsed, (12, 12, 12));

            ShouldReturnCorrectDistance(sut, parsed, 36);

            Debug.Assert(true == true);
        }

        private void ShouldReturnCorrectDistance(Solution sut, IEnumerable<(int x, int y, int z, int r)> parsed, int expected)
        {
            var filtered = parsed.Skip(9);
            var all = sut.GetAllPoints(filtered).ToList();

            var inRange = all
                .Where(x => x.r == all.Max(y => y.r));

            var result = inRange
                .OrderByDescending(x => sut.CalculatedDistance((0, 0, 0, 0), (x.x, x.y, x.z, 0)))
                .First();

            var fact = sut.CalculatedDistance((result.x, result.y, result.z, 0), (0, 0, 0, 0));

            Debug.Assert(fact == expected);
        }

        private void ShouldCalculateCorrectNumberOfBotsInRange(Solution sut, IEnumerable<(int x, int y, int z, int r)> parsed, int expected)
        {
            var filtered = parsed.Take(9);
            var max = filtered.OrderByDescending(p => p.r).First();

            var fact = sut.GetNumberOfReachedNanobots(max, parsed);

            Debug.Assert(fact == expected);
        }

        private void ShouldCalculateCorrectCoordinate(Solution sut, IEnumerable<(int x, int y, int z, int r)> parsed, (int x, int y, int z) expected)
        {
            var filtered = parsed.Skip(9);
            var all = sut.GetAllPoints(filtered).ToList();
            var fact = all
                .Where(x => x.r == all.Max(y => y.r))
                .First();

            Debug.Assert(fact.x == expected.x && fact.y == expected.y && fact.z == expected.z);
        }
    }
}