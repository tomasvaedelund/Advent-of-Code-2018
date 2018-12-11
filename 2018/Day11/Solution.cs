using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day11
{
    class Solution : Solver
    {
        public string GetName() => "Day11";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string, long)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string, long) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();

            var result = GetMaxGrid(7400, 3, 300);

            return ($"{result.Item1},{result.Item2}", timer.ElapsedMilliseconds);
        }

        public (string, long) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            var data = new List<(int x, int y, int powerLevel, int size)>();

            for (int i = 1; i <= 300; i++)
            {
                var temp = GetMaxGrid(7400, i, 300);
                data.Add((temp.Item1, temp.Item2, temp.Item3, i));
                if (temp.Item3 < 0)
                {
                    break;
                }
            }

            var result = data
                .OrderBy(d => d.powerLevel)
                .Last();

            return ($"{result.x},{result.y},{result.size}", timer.ElapsedMilliseconds);
        }

        public (int, int, int) GetMaxGrid(int serial, int gridSize, int max)
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

        public int GetGridPowerLevel(int x, int y, int serial, int gridSize)
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

        public int GetCellPowerLevel(int x, int y, int serial)
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
    }
}