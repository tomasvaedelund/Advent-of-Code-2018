using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day08
    {
        public static void GetResult()
        {
            // Init
            var day = "08";

            // Start
            var testData = Helpers.GetDataFromFile($"day{day}_test.txt");
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(Solve1(testData) == 138);

            stopWatch = Stopwatch.StartNew();
            result = Solve1(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            // Debug.Assert(Solve2(testData, 0, 2) == 15);

            // stopWatch = Stopwatch.StartNew();
            // result = Solve2(data, 60, 5).ToString();
            // Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int Solve1(string data)
        {
            var license = data.ToIntArray(" ");
            var metadata = new List<int>();
            var pos = 0;

            (pos, license, metadata) = GetNode(pos, license, metadata);

            return metadata.Sum();
        }

        private static (int, int[], List<int>) GetNode(int pos, int[] license, List<int> metadata)
        {
            var numChildren = license[pos++];
            var numMetadata = license[pos++];

            while (numChildren-- > 0)
            {
                (pos, license, metadata) = GetNode(pos, license, metadata);
            }

            metadata.AddRange(license.Skip(pos).Take(numMetadata));
            pos += numMetadata;

            return (pos, license, metadata);
        }
    }
}