using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    static class Day03
    {
        public static void GetResult()
        {
            // Init
            var day = "03";

            // Start
            var data = Helpers.GetDataFromFile($"day{day}.txt");
            var result = "";
            var stopWatch = new Stopwatch();

            // First star
            Debug.Assert(GetHowManySquareInchesOverlap("#1 @ 1,3: 4x4; #2 @ 3,1: 4x4; #3 @ 5,5: 2x2", ";") == 4);

            stopWatch = Stopwatch.StartNew();
            result = GetHowManySquareInchesOverlap(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 1", result, stopWatch.ElapsedMilliseconds);

            // Second star
            Debug.Assert(GetNoneOverlappingClaim("#1 @ 1,3: 4x4; #2 @ 3,1: 4x4; #3 @ 5,5: 2x2", ";") == 3);

            stopWatch = Stopwatch.StartNew();
            result = GetNoneOverlappingClaim(data).ToString();
            Helpers.DisplayDailyResult($"{day} - 2", result, stopWatch.ElapsedMilliseconds);

            // End
            stopWatch.Stop();
        }

        private static int GetHowManySquareInchesOverlap(string data, string splitter = "\r\n")
        {
            var sheetSize = 1000;
            var sheet = new int[sheetSize, sheetSize];
            var claims = data.Split(splitter).Select(x => new Claim(x)).ToList();

            claims.ForEach(claim =>
            {
                for (int y = claim.Top; y < claim.Top + claim.Height; y++)
                {
                    for (int x = claim.Left; x < claim.Left + claim.Width; x++)
                    {
                        sheet[y, x]++;
                    }
                }
            });

            return sheet.Cast<int>().Count(x => x > 1);
        }

        private static int GetNoneOverlappingClaim(string data, string splitter = "\r\n")
        {
            var sheetSize = 1000;
            var sheet = new int[sheetSize, sheetSize];
            var claims = data.Split(splitter).Select(x => new Claim(x)).ToList();

            claims.ForEach(claim =>
            {
                for (int y = claim.Top; y < claim.Top + claim.Height; y++)
                {
                    for (int x = claim.Left; x < claim.Left + claim.Width; x++)
                    {
                        if (sheet[y, x] > 0)
                        {
                            claim.isOverlapped = true;
                            claims.FirstOrDefault(c => c.ID == sheet[y, x]).isOverlapped = true;
                        }
                        sheet[y, x] = claim.ID;
                    }
                }
            });

            return claims.Single(x => x.isOverlapped == false).ID;
        }
    }

    class Claim
    {
        public int ID { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isOverlapped { get; set; }

        public Claim(string claim)
        {
            var claimArray = claim.Trim().Split(new string[] { "#", " @ ", ",", ": ", "x" }, StringSplitOptions.RemoveEmptyEntries);

            ID = Int32.Parse(claimArray[0]);
            Left = Int32.Parse(claimArray[1]);
            Top = Int32.Parse(claimArray[2]);
            Width = Int32.Parse(claimArray[3]);
            Height = Int32.Parse(claimArray[4]);
        }
    }
}
