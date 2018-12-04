using System;
using System.Collections;
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
            var dataTest = Helpers.GetDataFromFile($"day{day}_test.txt");
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(GetGuardThatSleepsTheMost(dataTest) == 240);

            stopWatch = Stopwatch.StartNew();
            result = GetGuardThatSleepsTheMost(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(GetGuardThatSleepsTheMost(dataTest, "\r\n", true) == 4455);

            stopWatch = Stopwatch.StartNew();
            result = GetGuardThatSleepsTheMost(data, "\r\n", true).ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int GetGuardThatSleepsTheMost(string data, string splitter = "\r\n", bool second = false)
        {
            var dataArray = data.Split(splitter).Select(x => new Row(x)).OrderBy(x => x.Date).ToList();
            var guards = new HashSet<Guard>();

            Guard currentGuard = null;
            var currentRow = 0;
            dataArray.ForEach(row =>
            {
                if (row.Event.StartsWith("Guard"))
                {
                    currentGuard = new Guard(row.Event);
                    if (!guards.Any(x => x.ID == currentGuard.ID))
                    {
                        guards.Add(currentGuard);
                    }
                    else
                    {
                        currentGuard = guards.Single(x => x.ID == currentGuard.ID);
                    }
                }
                else if (row.Event.Equals("falls asleep"))
                {
                    var nextRow = dataArray[currentRow + 1];
                    for (int i = row.Minutes; i <= nextRow.Minutes; i++)
                    {
                        currentGuard.Sleeping[i]++;
                    }
                }

                currentRow++;
            });

            if (second)
            {
                var max = 0;
                var maxMinute = 0;
                guards.ToList().ForEach(g =>
                {
                    if (g.Sleeping.Max() > max)
                    {
                        max = g.Sleeping.Max();
                        maxMinute = g.Sleeping.ToList().IndexOf(max);
                        currentGuard = g;
                    }
                });

                return maxMinute * currentGuard.ID;
            }

            var guard = guards.OrderByDescending(x => x.TotalMinutesSlept).First();
            var minute = guard.Sleeping.ToList().IndexOf(guard.Sleeping.Max());

            return minute * guard.ID;
        }
    }

    class Row
    {
        public DateTime Date { get; set; }
        public string Event { get; set; }
        public int Minutes { get; set; }

        public Row(string row)
        {
            var rowArray = row.Split(new string[] { "[", "] " }, StringSplitOptions.RemoveEmptyEntries);

            Date = DateTime.Parse(rowArray[0]);
            Event = rowArray[1];
            Minutes = Date.Minute;
        }
    }

    class Guard
    {
        public int ID { get; }
        public int[] Sleeping { get; }

        public int TotalMinutesSlept
        {
            get
            {
                return Sleeping.Sum();
            }
        }

        public Guard(string shift)
        {
            var shiftArray = shift.Split(new string[] { " #", " " }, StringSplitOptions.RemoveEmptyEntries);
            ID = int.Parse(shiftArray[1]);
            Sleeping = new int[60];
        }
    }
}