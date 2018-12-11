using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Helper
    {
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
            Console.WriteLine($"Day {day} Result: {result}");
            Console.WriteLine($"Day {day} Execution time: {timeElapsed}ms");
        }

        /// <summary>
        /// Converts a string to an array with char values
        ///
        /// "abcd".StringToIntArray() = [49, 50, 51, 52]
        /// </summary>
        /// <param name="data">string to convert</param>
        /// <returns>int[]</returns>
        public static int[] StringToIntArray(this string data)
        {
            return data.Select(c => (int)(c - '0')).ToArray();
        }

        /// <summary>
        /// Convert a string that represents an int array to an int array
        ///
        /// "1, 2, 3, 4".ToIntArray(",") = [1, 2, 3, 4]
        /// </summary>
        /// <param name="data">string to convert</param>
        /// <param name="splitter">optional splitter, default is "\r\n"</param>
        /// <returns>int[]</returns>
        public static int[] ToIntArray(this string data, string splitter = "\r\n")
        {
            return data.Split(splitter).Select(Int32.Parse).ToArray();
        }

        public static IEnumerable<int> Last(this IEnumerable<int> data, int count)
        {
            return data.Reverse().Take(count).Reverse();
        }
    }
}