using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day01
    {
        public static void GetResult()
        {
            // First star
            Debug.Assert(CalibrateDevice("+1, +1, +1", ",") == 3);
            Debug.Assert(CalibrateDevice("+1, +1, -2", ",") == 0);
            Debug.Assert(CalibrateDevice("-1, -2, -3", ",") == -6);

            var data = Helpers.GetDataFromFile("day01.txt");
            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = CalibrateDevice(data).ToString();
            Helpers.DisplayDailyResult("01 - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(FindFirstRecurranceOfFrequency("+1, -1", ",") == 0);
            Debug.Assert(FindFirstRecurranceOfFrequency("+3, +3, +4, -2, -4", ",") == 10);
            Debug.Assert(FindFirstRecurranceOfFrequency("-6, +3, +8, +5, -6", ",") == 5);
            Debug.Assert(FindFirstRecurranceOfFrequency("+7, +7, -2, -7, -4", ",") == 14);

            stopWatch = Stopwatch.StartNew();
            result = FindFirstRecurranceOfFrequency(data).ToString();
            Helpers.DisplayDailyResult("01 - 2", result, stopWatch.ElapsedMilliseconds);
            stopWatch.Stop();
        }

        private static int CalibrateDevice(string data, string splitter = "\r\n")
        {
            var result = data.ToIntArray(splitter).Aggregate((curr, next) => curr + next);

            return result;
        }

        private static int FindFirstRecurranceOfFrequency(string data, string splitter = "\r\n")
        {
            var dataArray = data.ToIntArray(splitter);
            var steps = new HashSet<int>() { 0 };
            var recurrenceFound = false;
            var result = 0;

            while (!recurrenceFound)
            {
                result = dataArray.TakeWhile(_ => !recurrenceFound).Aggregate(result, (curr, next) =>
                {
                    result = curr + next;
                    if (!steps.Add(result))
                    {
                        recurrenceFound = true;
                    }
                    return result;
                });
            }

            return result;
        }
    }
}
