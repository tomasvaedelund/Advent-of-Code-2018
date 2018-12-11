using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day11
    {
        public static void GetResult()
        {
            // Init
            var day = "11";

            // Start
            // var testData = Helpers.GetDataFromFile($"day{day}_test.txt");
            // var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(GetCellPowerLevel(122, 79, 57) == -5);
            Debug.Assert(GetCellPowerLevel(217, 196, 39) == 0);
            Debug.Assert(GetCellPowerLevel(101, 153, 71) == 4);

            Debug.Assert(GetGridPowerLevel(33, 45, 18, 3) == 29);
            Debug.Assert(GetGridPowerLevel(21, 61, 42, 3) == 30);

            stopWatch = Stopwatch.StartNew();
            result = Solve1(7400, 3, 300).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // // Second star
            Debug.Assert(true == true);

            stopWatch = Stopwatch.StartNew();
            result = Solve2(7400, 300).ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int GetCellPowerLevel(int x, int y, int serial)
        {
            var powerLevel = 0;
            var rackID = x + 10;

            powerLevel += rackID;
            powerLevel *= y;
            powerLevel += serial;
            powerLevel *= rackID;
            powerLevel = (powerLevel / 100) % 10;
            powerLevel -= 5;

            return powerLevel;
        }

        private static int GetGridPowerLevel(int x, int y, int serial, int gridSize)
        {
            var powerLevel = 0;

            for (int gY = y; gY < y + gridSize; gY++)
            {
                for (int gX = x; gX < x + gridSize; gX++)
                {
                    powerLevel += GetCellPowerLevel(gX, gY, serial);
                }
            }

            return powerLevel;
        }

        private static (int, int, int) GetMaxGrid(int serial, int gridSize, int max)
        {
            var data = new List<(int x, int y, int powerLevel)>();

            for (int y = 1; y < max - gridSize; y++)
            {
                for (int x = 1; x < max - gridSize; x++)
                {
                    data.Add((x, y, GetGridPowerLevel(x, y, serial, gridSize)));
                }
            }

            var maxPower = data
                .OrderBy(d => d.powerLevel)
                .Last();

            return maxPower;
        }

        private static string Solve1(int serial, int gridSize, int max)
        {
            var result = GetMaxGrid(serial, gridSize, max);

            return $"{result.Item1},{result.Item2}";
        }

        private static string Solve2(int serial, int max)
        {
            var data = new List<(int x, int y, int powerLevel, int size)>();

            for (int i = 1; i <= 300; i++)
            {
                var temp = GetMaxGrid(serial, i, max);
                data.Add((temp.Item1, temp.Item2, temp.Item3, i));
                if (temp.Item3 < 0)
                {
                    break;
                }
            }

            var maxPower = data
                .OrderBy(d => d.powerLevel)
                .Last();

            return $"{maxPower.x},{maxPower.y},{maxPower.size}";
        }
    }
}