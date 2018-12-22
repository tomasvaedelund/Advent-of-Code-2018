using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2018.Day20
{
    class Solution : Solver
    {
        public string GetName() => "Day20";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            var current = new Point() { X = 0, Y = 0, Type = 'X' };

            var matrix = GetAllPossiblePaths(input, new List<Point>() { current }, current);

            var result = CalculateMaxDoors(matrix);

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public IEnumerable<Point> GetAllPossiblePaths(string input, IEnumerable<Point> points, Point current)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];

                switch (c)
                {
                    case '(':
                        var last = GetMatchingEnd(input, i);
                        var options = input.Substring(i + 1, last - 1 - i);
                        i = last;
                        var branches = Tokenizer(options);
                        foreach (var branch in branches)
                        {
                            points = GetAllPossiblePaths(branch, points, current);
                        }
                        break;
                    case 'N':
                        current = new Point() { X = current.X, Y = current.Y - 1, Type = '-' };
                        points = points.Add(current);
                        current = new Point() { X = current.X, Y = current.Y - 1, Type = '.' };
                        points = points.Add(current);
                        break;
                    case 'E':
                        current = new Point() { X = current.X + 1, Y = current.Y, Type = '|' };
                        points = points.Add(current);
                        current = new Point() { X = current.X + 1, Y = current.Y, Type = '.' };
                        points = points.Add(current);
                        break;
                    case 'S':
                        current = new Point() { X = current.X, Y = current.Y + 1, Type = '-' };
                        points = points.Add(current);
                        current = new Point() { X = current.X, Y = current.Y + 1, Type = '.' };
                        points = points.Add(current);
                        break;
                    case 'W':
                        current = new Point() { X = current.X - 1, Y = current.Y, Type = '|' };
                        points = points.Add(current);
                        current = new Point() { X = current.X - 1, Y = current.Y, Type = '.' };
                        points = points.Add(current);
                        break;
                    default:
                        break;
                }
            }

            return points;
        }

        public int GetMatchingEnd(string input, int index)
        {
            var level = 0;
            for (int i = index; i < input.Length; i++)
            {
                var c = input[i];

                switch (c)
                {
                    case '(':
                        level++;
                        break;
                    case ')':
                        level--;
                        if (level == 0)
                        {
                            return i;
                        }
                        break;
                    default:
                        break;
                }
            }

            throw new Exception("Could not find matching ending par...");
        }

        public int CalculateMaxDoors(IEnumerable<Point> points)
        {
            var result = CalculateMaxDistance(points);

            return result / 2;
        }

        public int CalculateMaxDistance(IEnumerable<Point> points)
        {
            var result = 0;
            var start = points.Single(p => p.Type == 'X');
            var matrix = GenerateMatrix(points);

            var minX = points.Select(p => p.X).Min();
            var minY = points.Select(p => p.Y).Min();

            points = points.RecalculateXY(minX, minY);

            foreach (var point in points.Where(p => p.Type == '.'))
            {
                var distance = CalculateDistance(start, point, matrix);
                result = (distance > result) ? distance : result;
            }

            return result;
        }

        private int CalculateDistance(Point start, Point end, char[,] matrix)
        {
            var s = new Astar.Location() { X = start.X, Y = start.Y };
            var e = new Astar.Location() { X = end.X, Y = end.Y };

            var distance = Astar.GetDistance(s, e, matrix);

            return distance;
        }

        public IEnumerable<string> Tokenizer(string input)
        {
            var result = new List<string>();
            var level = 0;
            var temp = "";

            foreach (var c in input)
            {
                switch (c)
                {
                    case '(':
                        level++;
                        temp += c;
                        break;
                    case ')':
                        level--;
                        temp += c;
                        break;
                    case '|':
                        if (level == 0)
                        {
                            result.Add(temp);
                            temp = "";
                        }
                        else
                        {
                            temp += c;
                        }
                        break;
                    default:
                        temp += c;
                        break;
                }
            }

            result.Add(temp);

            return result;
        }

        public char[,] GenerateMatrix(IEnumerable<Point> points)
        {
            var maxX = points.Select(p => p.X).Max();
            var maxY = points.Select(p => p.Y).Max();
            var minX = points.Select(p => p.X).Min();
            var minY = points.Select(p => p.Y).Min();

            var width = maxX - minX + 3;
            var height = maxY - minY + 3;

            var result = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var point = points.FirstOrDefault(p => p.X - minX + 1 == x && p.Y - minY + 1 == y);
                    result[y, x] = point?.Type ?? '#';
                }
            }

            return result;
        }

        public void PrintMatrix(char[,] matrix)
        {
            Console.Write(" ");

            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                Console.Write(x);
            }

            Console.WriteLine();

            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                Console.Write(y);
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    Console.Write(matrix[y, x]);
                }

                Console.WriteLine();
            }
        }

        public void PrintMatrix(IEnumerable<Point> points)
        {
            var maxX = points.Select(p => p.X).Max();
            var maxY = points.Select(p => p.Y).Max();
            var minX = points.Select(p => p.X).Min();
            var minY = points.Select(p => p.Y).Min();

            for (int y = minY - 1; y < maxY + 2; y++)
            {
                for (int x = minX - 1; x < maxX + 2; x++)
                {
                    var point = points.FirstOrDefault(p => p.X == x && p.Y == y);
                    Console.Write(point?.Type ?? '#');
                }

                Console.WriteLine();
            }
        }
    }

    public static class SolutionExtensions
    {
        public static IEnumerable<Point> Add(this IEnumerable<Point> list, Point item)
        {
            if (!list.Contains(item))
            {
                return list.Append(item);
            }

            return list;
        }

        public static IEnumerable<Point> RecalculateXY(this IEnumerable<Point> points, int minX, int minY)
        {
            foreach (var point in points)
            {
                point.X = point.X - minX + 1;
                point.Y = point.Y - minY + 1;
                yield return point;
            }
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Type { get; set; }

        public int Distance { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Point other = (Point)obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return (X << 2) ^ Y;
        }

        public override string ToString()
        {
            return $"Point({X},{Y}) = {Type}";
        }
    }
}