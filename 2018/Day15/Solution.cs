using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day15
{
    class Solution : Solver
    {
        public string GetName() => "Day15";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public IEnumerable<Point> GetClosesEnemies(Point player, IEnumerable<Point> enemies)
        {
            var minDistance = enemies
                .OrderBy(e => e.GetDistance(player))
                .First()
                .GetDistance(player);

            enemies = enemies
                .Where(e => e.GetDistance(player) == minDistance)
                .ToList();

            return enemies;
        }

        public LinkedList<Point> AStar(Point start, Point target, IEnumerable<Point> friends, IEnumerable<Point> enemies, IEnumerable<Point> walls)
        {
            start.G = start.H = start.F = 0;
            target.G = target.H = target.F = 0;

            var openList = new LinkedList<Point>();
            var closedList = new List<Point>();

            openList.AddFirst(start);

            while (openList.Count > 0)
            {
                var currentPoint = new Point(0, 0);
                var currentIndex = 0;
                for (int i = 0; i < openList.Count; i++)
                {
                    var point = openList.ElementAt(i);
                    if (point.F < currentPoint.F)
                    {
                        currentPoint = point;
                        currentIndex = i;
                    }
                }

                openList.Remove(currentPoint);
                openList.AddLast
            }

        }

        public IEnumerable<Point> GetValidTargets(IEnumerable<Point> friends, IEnumerable<Point> enemies, IEnumerable<Point> walls)
        {
            var validTargets = enemies
                .SelectMany(e => e.GetAdjacentPoints())
                .Distinct()
                .Where(e => !walls.Contains(e))
                .Where(e => !friends.Contains(e))
                .Where(e => !enemies.Contains(e));

            return validTargets;
        }

        public (IEnumerable<Point> elves, IEnumerable<Point> goblins, IEnumerable<Point> walls) ScanMap(string input)
        {
            var elves = new List<Point>();
            var goblins = new List<Point>();
            var walls = new List<Point>();

            var lines = input.Split("\r\n");

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            walls.Add(new Point(x, y));
                            continue;
                        case 'G':
                            goblins.Add(new Point(x, y));
                            continue;
                        case 'E':
                            elves.Add(new Point(x, y));
                            continue;
                        default:
                            continue;
                    }
                }
            }

            return (elves: elves, goblins: goblins, walls: walls);
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IEnumerable<Point> GetAdjacentPoints()
        {
            yield return new Point(X - 1, Y);
            yield return new Point(X + 1, Y);
            yield return new Point(X, Y - 1);
            yield return new Point(X, Y + 1);
        }

        public override bool Equals(object obj)
        {
            Point point = obj as Point;
            if (point == null)
            {
                return false;
            }
            return X == point.X && Y == point.Y;
        }

        public override int GetHashCode()
        {
            return (X.GetHashCode() ^ Y.GetHashCode()).GetHashCode();
        }
    }


    public static class SolutionExtensions
    {
        public static int GetDistance(this Point source, Point target)
        {
            return Math.Abs(source.X - target.X) + Math.Abs(source.Y - target.Y);
        }
    }
}