using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day22
{
    class Solution : Solver
    {
        public string GetName() => "Day22";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            var parsed = ParseInput(input);
            var cave = GenerateCave(parsed.depth, parsed.x, parsed.y);
            var result = GetRiskLevel(cave, parsed.x, parsed.y);

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public Region[,] GenerateCave(int depth, int targetX, int targetY)
        {
            var cave = new Region[targetY + 1, targetX + 1];

            for (int y = 0; y <= targetY; y++)
            {
                for (int x = 0; x <= targetX; x++)
                {
                    var region = new Region() { X = x, Y = y };
                    cave[y, x] = region;
                    region.GeoIndex = GetGeoIndex(x, y, targetX, targetY, cave);
                    region.EroLevel = GetEroLevel(x, y, depth, cave);
                }
            }

            return cave;
        }

        public int GetGeoIndex(int x, int y, int tX, int tY, Region[,] cave)
        {
            if (x == y && x == 0) return 0;
            if (x == tX && y == tY) return 0;
            if (y == 0) return x * 16807;
            if (x == 0) return y * 48271;
            return cave[y, x - 1].EroLevel * cave[y - 1, x].EroLevel;
        }

        public int GetEroLevel(int x, int y, int depth, Region[,] cave)
        {
            return (cave[y, x].GeoIndex + depth) % 20183;
        }

        public int GetRiskLevel(Region[,] cave, int tX, int tY)
        {
            var riskLevel = 0;

            for (int y = 0; y <= tY; y++)
            {
                for (int x = 0; x <= tX; x++)
                {
                    riskLevel += cave[y, x].EroLevel % 3;
                }
            }

            return riskLevel;
        }

        public (int depth, int x, int y) ParseInput(string input)
        {
            var depth = int.Parse(input.Split("\r\n")[0].Replace("depth: ", ""));
            var x = int.Parse(input.Split("\r\n")[1].Replace("target: ", "").Split(',')[0]);
            var y = int.Parse(input.Split("\r\n")[1].Replace("target: ", "").Split(',')[1]);

            return (depth: depth, x: x, y: y);
        }

        public void PrintCave(Region[,] cave)
        {
            for (int y = 0; y < cave.GetLength(0); y++)
            {
                for (int x = 0; x < cave.GetLength(1); x++)
                {
                    Console.Write(cave[y, x].Type);
                }

                Console.WriteLine();
            }
        }
    }

    public class Region
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int GeoIndex { get; set; }
        public int EroLevel { get; set; }

        public char Type
        {
            get
            {
                if (EroLevel % 3 == 0) return '.';
                if (EroLevel % 3 == 1) return '=';
                if (EroLevel % 3 == 2) return '|';
                return 'X';
            }
        }
    }
}