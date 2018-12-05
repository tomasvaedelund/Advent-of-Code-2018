using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day05
    {
        public static void GetResult()
        {
            // Init
            var day = "05";

            // Start
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(GetFullyReactingUnits("dabAcCaCBAcCcaDA") == 10);

            stopWatch = Stopwatch.StartNew();
            result = GetFullyReactingUnits(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(GetLengthOfShortestPolymer("dabAcCaCBAcCcaDA") == 4);

            stopWatch = Stopwatch.StartNew();
            result = GetLengthOfShortestPolymer(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static bool DoesUnitsReact(int unitOne, int unitTwo)
        {
            return unitOne == unitTwo + 32 || unitOne + 32 == unitTwo;
        }

        private static int GetFullyReactingUnits(string data)
        {
            var dataArray = data.StringToIntArray().ToList();
            return GetFullyReactingUnits(dataArray);
        }

        private static int GetFullyReactingUnits(List<int> data)
        {
            var shouldLoop = true;

            while (shouldLoop)
            {
                shouldLoop = false;
                for (int i = 0; i < data.Count - 1; i++)
                {
                    if (DoesUnitsReact(data.ElementAt(i), data.ElementAt(i + 1)))
                    {
                        data.RemoveRange(i, 2);
                        shouldLoop = true;
                    }
                }
            }

            return data.Count();
        }

        private static int GetLengthOfShortestPolymer(string data)
        {
            var dataArray = data.StringToIntArray().ToList();

            var currIndex = 17;
            var stopIndex = 42;
            var result = int.MaxValue;

            while (currIndex <= stopIndex)
            {
                var tempArray = dataArray.Where(x => x != currIndex && x != currIndex + 32).ToList();
                var tempResult = GetFullyReactingUnits(tempArray);
                result = (tempResult < result) ? tempResult : result;
                currIndex++;
            }

            return result;
        }
    }
}