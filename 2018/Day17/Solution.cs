using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day17
{
    class Solution : Solver
    {
        public string GetName() => "Day17";

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
            return ($"result", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public char[,] ParseInput(string input)
        {
            var lines = input.Split("\r\n");
            var rows = new List<Row>();

            foreach (var line in lines)
            {
                var data = line
                    .Split(new string[] { "x=", ", ", "y=", ".." }, StringSplitOptions.None)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(int.Parse)
                    .ToArray();

                if (Regex.Match(line, @"x=(\d+), y=(\d+\.\.\d+)").Success)
                {
                    var row = new Row(data[0], data[0], data[2], data[1]);

                    rows.Add(row);

                    continue;
                }

                Match xFirst = Regex.Match(line, @"y=(\d+), x=(\d+\.\.\d+)");
                if (xFirst.Success)
                {
                    var row = new Row(data[2], data[1], data[0], data[0]);

                    rows.Add(row);

                    continue;
                }
            }

            var minY = rows.Select(r => r.MinY).Min();
            var minX = rows.Select(r => r.MinX).Min();
            var maxY = rows.Select(r => r.MinY).Max();
            var maxX = rows.Select(r => r.MinX).Max();

            var matrix = new char[maxY - minY + 2, maxX - minX + 3];

            foreach (var row in rows)
            {
                for (int y = row.MinY - minY + 1; y < row.MaxY - minY + 2; y++)
                {
                    for (int x = row.MinX - minX + 1; x < row.MaxX - minX + 2; x++)
                    {
                        matrix[y, x] = '#';
                    }
                }
            }

            //PrintMatrix(matrix);

            return matrix;
        }

        public void PrintMatrix(char[,] matrix)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var c = (matrix[y, x] == '\0') ? '.' : matrix[y, x];
                    Console.Write(c);
                }

                Console.WriteLine();
            }
        }
    }

    public class Row
    {
        public int MaxX { get; set; }
        public int MinX { get; set; }
        public int MaxY { get; set; }
        public int MinY { get; set; }

        public Row(int maxX, int minX, int maxY, int minY)
        {
            MaxX = maxX;
            MinX = minX;
            MaxY = maxY;
            MinY = minY;
        }
    }
}