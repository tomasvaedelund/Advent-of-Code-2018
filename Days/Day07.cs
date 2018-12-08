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
            Debug.Assert(Solve2(testData, 0, 2) == 15);

            stopWatch = Stopwatch.StartNew();
            result = Solve2(data, 60, 5).ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static Dictionary<string, List<string>> GetDataInAUsableFormat(string data)
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

            return pairs;
        }

        private static string Solve1(string data)
        {
            var pairs = GetDataInAUsableFormat(data);
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

        private static string GetWork(Dictionary<string, List<string>> pairs, List<string> result, List<string> currentWork)
        {
            foreach (var pair in pairs.OrderBy(p => p.Key))
            {
                if (!result.Any(r => r == pair.Key) && pair.Value.All(p => result.Contains(p)) && !currentWork.Any(w => w == pair.Key))
                {
                    return pair.Key;
                }
            }

            return string.Empty;
        }

        private static int GetTime(string work, int offset)
        {
            return offset + work[0] - 64;
        }

        private static int Solve2(string data, int offset, int numWorkers)
        {
            var pairs = GetDataInAUsableFormat(data);
            var result = new List<string>();
            var workers = Enumerable.Range(-1, numWorkers).ToList();
            var currentWork = Enumerable.Repeat(string.Empty, numWorkers).ToList();
            var tick = 0;

            while (result.Count < pairs.Count || workers.Any(w => w > tick))
            {
                for (int i = 0; i < workers.Count(); i++)
                {
                    if (workers[i] <= tick && currentWork[i] != string.Empty)
                    {
                        result.Add(currentWork[i]);
                        currentWork[i] = string.Empty;
                    }
                }

                for (int i = 0; i < workers.Count(); i++)
                {
                    if (workers[i] <= tick)
                    {
                        var work = GetWork(pairs, result, currentWork);
                        if (work != string.Empty)
                        {
                            currentWork[i] = work;
                            workers[i] = GetTime(work, offset) + tick;
                        }
                    }
                }

                tick++;
            }

            return tick - 1;
        }
    }
}