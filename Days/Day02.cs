using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day02
    {
        public static void GetResult()
        {
            // First star
            Debug.Assert(GetChecksumPart("abcdef, bababc, abbcde, abcccd, aabcdd, abcdee, ababab", ",") == 12);

            var data = Helpers.GetDataFromFile("day02.txt");
            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = GetChecksumPart(data).ToString();
            Helpers.DisplayDailyResult("02 - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(GetSimilarBoxes("abcde, fghij, klmno, pqrst, fguij, axcye, wvxyz", ",") == "fgij");

            stopWatch = Stopwatch.StartNew();
            result = GetSimilarBoxes(data);
            Helpers.DisplayDailyResult("02 - 2", result, stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
        }

        private static int GetChecksumPart(string data, string splitter = "\r\n")
        {
            var dataArray = data.Split(splitter);
            var counter = new int[dataArray.First().Length];

            foreach (var item in dataArray)
            {
                item
                .GroupBy(c => c)
                .Select(c => new { Char = c.Key, Count = c.Count() })
                .Where(c => c.Count > 1)
                .Select(c => c.Count)
                .Distinct()
                .ToList()
                .ForEach(c => counter[c]++);
            }

            return counter.Where(c => c > 0).Aggregate((c, n) => c * n);
        }

        private static string GetSimilarBoxes(string data, string splitter = "\r\n")
        {
            var result = "";
            var dataArray = data.Split(splitter);
            var itemLength = dataArray.First().Length;

            for (int i = 0; i < itemLength; i++)
            {
                var tempArray = dataArray.Select(c => c.Remove(i, 1));
                var tempResult = tempArray
                    .GroupBy(x => x)
                    .Select(x => new { ID = x, Count = x.Count() })
                    .Where(x => x.Count > 1)
                    .Select(x => x.ID)
                    .FirstOrDefault();

                if (tempResult != null)
                {
                    result = tempResult.Distinct().Single().Trim();
                    break;
                }
            }

            return result;
        }
    }
}
