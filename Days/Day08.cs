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

        private static int[] License;
        private static List<int> Metadata;
        private static int Solve1(string data)
        {
            License = data.ToIntArray(" ");
            Metadata = new List<int>();

            GetMetaDataSum(0);

            return Metadata.Sum();
        }

        private static int GetMetaDataSum(int pos)
        {
            var numChildren = License[pos++];
            var numMetadata = License[pos++];

            while (numChildren-- > 0)
            {
                pos = GetMetaDataSum(pos);
            }

            Metadata.AddRange(License.Skip(pos).Take(numMetadata));
            pos += numMetadata;

            return pos;
        }

        class Node
        {
            public List<Node> Children { get; set; }
            public List<int> Metadata { get; set; }

            public Node(int numChildren, int numMetadata)
            {
                Children = new List<Node>(numChildren);
                Metadata = new List<int>(numMetadata);
            }
        }
    }
}