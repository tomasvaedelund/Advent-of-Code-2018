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
            var result = "";

            while (true)
            {
                var status = MoveCarts(parsed.tracks, parsed.carts);

                if (status.isCrash)
                {
                    result = $"{status.x},{status.y}";
                    break;
                }
            }

            return (result, timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            return ($"result", timer.ElapsedMilliseconds);
        }

        public (bool isCrash, int x, int y) MoveCarts(char[][] tracks, IEnumerable<Cart> carts)
        {
            carts = carts.OrderBy(c => c.Y).ThenBy(c => c.X);

            foreach (var cart in carts)
            {
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

                var crashes = carts
                    .GroupBy(c => new { c.X, c.Y })
                    .Select(g => new { cart = g.Key, count = g.Count() })
                    .Where(g => g.count > 1);

                if (crashes.Any())
                {
                    return (isCrash: true, x: crashes.First().cart.X, y: crashes.First().cart.Y);
                }
            }

            return (isCrash: false, x: 0, y: 0);
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

            public Cart(int x, int y, int dir, int last)
            {
                X = x;
                Y = y;
                Direction = dir;
                Last = last;
            }
        }
    }
}