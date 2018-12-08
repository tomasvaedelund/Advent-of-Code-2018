using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day07
    {
        public static void GetResult()
        {
            // Init
            var day = "07";

            // Start
            var testData = Helpers.GetDataFromFile($"day{day}_test.txt");
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(Solve1(testData) == "CABDFE");

            stopWatch = Stopwatch.StartNew();
            result = Solve1(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(true == true);

            stopWatch = Stopwatch.StartNew();
            result = "".ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static string Solve1(string data)
        {
            var steps = data.Split("\r\n");
            var pairs = new Dictionary<string, List<string>>();

            foreach (var step in steps)
            {
                var parts = step.Split(' ');

                // Add all that has a prerequisite
                if (!pairs.TryAdd(parts[7], new List<string> { parts[1] }))
                {
                    pairs[parts[7]].Add(parts[1]);
                }

                // Add initial value, lacking prerequisite
                pairs.TryAdd(parts[1], new List<string>());
            }

            var result = new List<string>();

            while (result.Count < pairs.Count)
            {
                foreach (var pair in pairs.OrderBy(p => p.Key))
                {
                    if (!result.Any(r => r == pair.Key) && pair.Value.All(p => result.Contains(p)))
                    {
                        result.Add(pair.Key);
                        break;
                    }
                }
            }

            return string.Join("", result);
        }
    }
}