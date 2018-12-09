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

            // stopWatch = Stopwatch.StartNew();
            // result = Solve2(data, 60, 5).ToString();
            // Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int Solve1(string data)
        {
            var elves = int.Parse(data.Split(' ')[0]);
            var marbles = int.Parse(data.Split(' ')[6]);

            var players = new int[elves];
            var board = new List<int>(marbles);

            var currentPos = 0;
            var currentPlayer = 0;

            board.Insert(0, 0);

            for (int i = 1; i < marbles + 1; i++)
            {
                currentPlayer = i % elves;

                if (i % 23 == 0)
                {
                    currentPos = (currentPos - 7) % board.Count;
                    currentPos = (currentPos < 0) ? board.Count + currentPos : currentPos;
                    players[currentPlayer] += i;
                    players[currentPlayer] += board[currentPos];
                    board.RemoveAt(currentPos);
                }
                else
                {
                    currentPos = ((currentPos + 1) % board.Count) + 1;
                    board.Insert(currentPos, i);
                }
            }

            return players.Max();
        }
    }
}