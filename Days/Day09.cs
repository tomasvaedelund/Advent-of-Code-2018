using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day09
    {
        public static void GetResult()
        {
            // Init
            var day = "09";

            // Start
            // var testData = Helpers.GetDataFromFile($"day{day}_test.txt");
            var data = "468 players; last marble is worth 71010 points";
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(Solve1("9 players; last marble is worth 25 points") == 32);
            Debug.Assert(Solve1("10 players; last marble is worth 1618 points") == 8317);
            Debug.Assert(Solve1("13 players; last marble is worth 7999 points") == 146373);
            Debug.Assert(Solve1("17 players; last marble is worth 1104 points") == 2764);
            Debug.Assert(Solve1("21 players; last marble is worth 6111 points") == 54718);
            Debug.Assert(Solve1("30 players; last marble is worth 5807 points") == 37305);

            stopWatch = Stopwatch.StartNew();
            result = Solve1(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            // Debug.Assert(Solve2(testData, 0, 2) == 15);

            stopWatch = Stopwatch.StartNew();
            result = Solve2(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static LinkedListNode<int> Rotate(this LinkedList<int> list, LinkedListNode<int> current, int count)
        {
            if (count > 0)
            {
                while (count-- > 0)
                {
                    current = current.Next ?? list.First;
                }
            }
            else if (count < 0)
            {
                while (count++ < 0)
                {
                    current = current.Previous ?? list.Last;
                }
            }

            return current;
        }

        private static uint Solver(string data, int seed)
        {
            var elves = int.Parse(data.Split(' ')[0]);
            var marbles = int.Parse(data.Split(' ')[6]) * seed + 1;

            var players = new uint[elves];
            var board = new LinkedList<int>();
            var current = board.AddFirst(0);

            for (int i = 1; i < marbles; i++)
            {
                if (i % 23 == 0)
                {
                    current = board.Rotate(current, -7);

                    players[i % elves] += (uint)(i + current.Value);

                    current = board.Rotate(current, 1);
                    board.Remove(board.Rotate(current, -1));
                }
                else
                {
                    current = board.Rotate(current, 1);
                    current = board.AddAfter(current, i);
                }
            }

            return players.Max();
        }

        private static uint Solve1(string data)
        {
            return Solver(data, 1);
        }

        private static uint Solve2(string data)
        {
            return Solver(data, 100);
        }
    }
}