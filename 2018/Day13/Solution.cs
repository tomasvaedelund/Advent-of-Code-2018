using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day13
{
    class Solution : Solver
    {
        public string GetName() => "Day13";

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

            while (!parsed.carts.Any(c => c.IsCrashed))
            {
                parsed.carts = MoveCarts(parsed.tracks, parsed.carts);
            }

            var cart = parsed.carts.Where(c => c.IsCrashed).First();

            return ($"{cart.X},{cart.Y}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            var parsed = ParseInput(input);

            while (parsed.carts.Count(c => c.IsCrashed == false) != 1)
            {
                parsed.carts = MoveCarts(parsed.tracks, parsed.carts);
            }

            var cart = parsed.carts.Where(c => c.IsCrashed == false).Single();

            return ($"{cart.X},{cart.Y}", timer.ElapsedMilliseconds);
        }

        public IEnumerable<Cart> MoveCarts(char[][] tracks, IEnumerable<Cart> carts)
        {
            var data = new List<Cart>(carts.OrderBy(c => c.Y).ThenBy(c => c.X));

            for (int i = 0; i < carts.Count(); i++)
            {
                var cart = data.ElementAt(i);

                if (cart.IsCrashed)
                {
                    continue;
                }

                switch (cart.Direction)
                {
                    // North
                    case 0:
                        cart.Y -= 1;
                        break;
                    // East
                    case 90:
                        cart.X += 1;
                        break;
                    // South
                    case 180:
                        cart.Y += 1;
                        break;
                    // West
                    case 270:
                        cart.X -= 1;
                        break;
                    default:
                        throw new Exception("Unknown direction");
                }

                cart.Direction = GetNewDirection(tracks, cart);

                data
                    .Where(c => c.IsCrashed == false)
                    .GroupBy(c => c)
                    .Where(c => c.Count() > 1)
                    .SelectMany(c => c)
                    .ToList()
                    .ForEach(c => c.IsCrashed = true);
            }

            return data;
        }

        public int GetNewDirection(char[][] tracks, Cart cart)
        {
            var x = cart.X;
            var y = cart.Y;
            var t = tracks[y][x];

            switch (cart.Direction)
            {
                case 0:
                    if (t == '/')
                    {
                        return 90;
                    }
                    else if (t == '\\')
                    {
                        return 270;
                    }
                    else if (t == '+')
                    {
                        if (cart.Last == -1)
                        {
                            cart.Last = 0;
                            return 270;
                        }

                        if (cart.Last == 0)
                        {
                            cart.Last = 1;
                            return 0;
                        }

                        if (cart.Last == 1)
                        {
                            cart.Last = -1;
                            return 90;
                        }
                    }
                    return cart.Direction;
                case 90:
                    if (t == '/')
                    {
                        return 0;
                    }
                    else if (t == '\\')
                    {
                        return 180;
                    }
                    else if (t == '+')
                    {
                        if (cart.Last == -1)
                        {
                            cart.Last = 0;
                            return 0;
                        }

                        if (cart.Last == 0)
                        {
                            cart.Last = 1;
                            return 90;
                        }

                        if (cart.Last == 1)
                        {
                            cart.Last = -1;
                            return 180;
                        }
                    }

                    return cart.Direction;
                case 180:
                    if (t == '/')
                    {
                        return 270;
                    }
                    else if (t == '\\')
                    {
                        return 90;
                    }
                    else if (t == '+')
                    {
                        if (cart.Last == -1)
                        {
                            cart.Last = 0;
                            return 90;
                        }

                        if (cart.Last == 0)
                        {
                            cart.Last = 1;
                            return 180;
                        }

                        if (cart.Last == 1)
                        {
                            cart.Last = -1;
                            return 270;
                        }
                    }
                    return cart.Direction;
                case 270:
                    if (t == '/')
                    {
                        return 180;
                    }
                    else if (t == '\\')
                    {
                        return 0;
                    }
                    else if (t == '+')
                    {
                        if (cart.Last == -1)
                        {
                            cart.Last = 0;
                            return 180;
                        }

                        if (cart.Last == 0)
                        {
                            cart.Last = 1;
                            return 270;
                        }

                        if (cart.Last == 1)
                        {
                            cart.Last = -1;
                            return 0;
                        }
                    }
                    return cart.Direction;
                default:
                    throw new Exception($"Unknown direction '{cart.Direction}'");
            }
        }

        public (char[][] tracks, IEnumerable<Cart> carts) ParseInput(string data)
        {
            var split = data.Split("\r\n");
            var tracks = new char[split.Length][];
            var carts = new List<Cart>();

            for (int y = 0; y < split.Length; y++)
            {
                tracks[y] = split[y].Select((s, i) =>
                {
                    switch (s)
                    {
                        case '>':
                            carts.Add(new Cart(i, y, 90, -1));
                            return '-';
                        case '<':
                            carts.Add(new Cart(i, y, 270, -1));
                            return '-';
                        case '^':
                            carts.Add(new Cart(i, y, 0, -1));
                            return '|';
                        case 'v':
                            carts.Add(new Cart(i, y, 180, -1));
                            return '|';
                        default:
                            return s;
                    }
                }).ToArray();
            }

            return (tracks: tracks, carts: carts);
        }

        public class Cart
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Direction { get; set; }
            public int Last { get; set; }
            public bool IsCrashed { get; set; }

            public Cart(int x, int y, int dir, int last)
            {
                X = x;
                Y = y;
                Direction = dir;
                Last = last;
            }

            public override bool Equals(object obj)
            {
                Cart other = obj as Cart;
                if (other == null)
                {
                    return false;
                }
                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                return (X.GetHashCode() ^ Y.GetHashCode()).GetHashCode();
            }
        }
    }
}