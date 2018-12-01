using System;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day01
    {
        public static void GetResult()
        {
            Debug.Assert(CalibrateDevice("+1, +1, +1", ",") == 3);
            Debug.Assert(CalibrateDevice("+1, +1, -2", ",") == 0);
            Debug.Assert(CalibrateDevice("-1, -2, -3", ",") == -6);

            var data = Helpers.GetDataFromFile("day01.txt");
            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = CalibrateDevice(data).ToString();
            Helpers.DisplayDailyResult("01 - 1", result, stopWatch.ElapsedMilliseconds);
        }

        private static int CalibrateDevice(string data, string splitter = "\r\n")
        {
            var dataArray = data.Split(splitter).Select(x => Convert.ToInt32(x));

            var result = dataArray.Aggregate((curr, next) => curr + next);

            return result;
        }
    }
}
