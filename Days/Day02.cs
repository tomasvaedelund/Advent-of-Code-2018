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
            Debug.Assert(true == true);

            var data = Helpers.GetDataFromFile("day02.txt");
            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = "".ToString();
            Helpers.DisplayDailyResult("02 - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(true == true);

            stopWatch = Stopwatch.StartNew();
            result = "".ToString();
            Helpers.DisplayDailyResult("02 - 2", result, stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
        }
    }
}
