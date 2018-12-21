using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Astar
    {
        public class Location
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int F { get; set; }
            public int G { get; set; }
            public int H { get; set; }
            public Location Parent { get; set; }
        }

        public static int GetDistance(Location start, Location target, char[,] matrix)
        {
            var openList = new List<Location>();
            var closedList = new List<Location>();
            var g = 0;
            Location current = null;

            // start by adding the original position to the open list
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // add the current square to the closed list
                closedList.Add(current);

                // remove it from the open list
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                {
                    break;
                }

                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, matrix);
                g++;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) != null)
                    {
                        continue;
                    }

                    // if it's not in the open list...
                    if (openList.FirstOrDefault(l => l.X == adjacentSquare.X && l.Y == adjacentSquare.Y) == null)
                    {
                        // compute its score, set the parent
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;

                        // and add it to the open list
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            var result = 0;
            // assume path was found; let's calculate it
            while (current != null)
            {
                current = current.Parent;
                result++;
            }

            // end
            return result;
        }

        static IEnumerable<Location> GetWalkableAdjacentSquares(int x, int y, char[,] matrix)
        {
            var proposedLocations = new List<Location>()
            {
                new Location { X = x, Y = y - 1 },
                new Location { X = x, Y = y + 1 },
                new Location { X = x - 1, Y = y },
                new Location { X = x + 1, Y = y },
            };

            return proposedLocations.Where(l =>
                l.X >= 0
                && l.X < matrix.GetLength(1)
                && l.Y >= 0
                && l.Y < matrix.GetLength(0)
                && matrix[l.Y, l.X] != '#');
        }

        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}