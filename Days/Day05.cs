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
            return GetFullyReactingUnits(dataArray).Count();
        }

        private static List<int> GetFullyReactingUnits(List<int> data)
        {
            var index = 0;

            while (index < data.Count - 1)
            {
                if (DoesUnitsReact(data.ElementAt(index), data.ElementAt(index + 1)))
                {
                    data.RemoveRange(index, 2);
                    index = (index == 0) ? index : index -= 1;
                }
                else
                {
                    index++;
                }
            }

            return data;
        }

        private static int GetLengthOfShortestPolymer(string data)
        {
            var dataArray = GetFullyReactingUnits(data.StringToIntArray().ToList());

            var currIndex = 17;
            var stopIndex = 42;
            var result = int.MaxValue;

            while (currIndex <= stopIndex)
            {
                var tempArray = dataArray.Where(x => x != currIndex && x != currIndex + 32).ToList();
                var tempResult = GetFullyReactingUnits(tempArray).Count;
                result = (tempResult < result) ? tempResult : result;
                currIndex++;
            }

            return result;
        }
    }
}