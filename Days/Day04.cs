using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day04
    {
        public static void GetResult()
        {
            // Init
            var day = "04";

            // Start
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(true == true);

            stopWatch = Stopwatch.StartNew();
            result = "".ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(true == true);

            stopWatch = Stopwatch.StartNew();
            result = "".ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }
    }
}
