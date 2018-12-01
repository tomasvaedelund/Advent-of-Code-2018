using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2018.Days
{
    public static class Helpers
    {
        public static int[] StringToIntArray(string data)
        {
            return data.Select(c => (int)(c - '0')).ToArray();
        }

        public static string GetDataFromFile(string fileName)
        {
            var contents = "";
            var directory = System.IO.Directory.GetCurrentDirectory();
            var fullPathToFile = $"{directory}\\Data\\{fileName}";

            if (!File.Exists(fullPathToFile))
            {
                throw new FileNotFoundException($"Cannot find {fileName}");
            }

            contents = File.ReadAllText(fullPathToFile);

            return contents;
        }

        public static void DisplayDailyResult(string day, string result, long timeElapsed)
        {
            Console.WriteLine($"Day {day}: {result} - in {timeElapsed}ms");
        }

        public static int[] ToIntArray(this string data, string splitter = "\r\n")
        {
            return data.Split(splitter).Select(x => Convert.ToInt32(x)).ToArray();
        }
    }
}