using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
    class Preparer
    {
        public static string Run(int year, int day)
        {
            try
            {
                var path = Assembly.GetExecutingAssembly().CodeBase.Split(new string[] { "file:///", "/bin/" }, StringSplitOptions.RemoveEmptyEntries)[0];
                var pathYear = Path.Combine(path, year.ToString());
                var pathDay = Path.Combine(pathYear, $"Day{day.ToString("00")}");

                if (!Directory.Exists(pathYear))
                {
                    Directory.CreateDirectory(pathYear);
                }

                if (Directory.Exists(pathDay))
                {
                    return $"Day{day.ToString("00")} already exists";
                }

                if (!Directory.Exists(pathDay))
                {
                    Directory.CreateDirectory(pathDay);
                }

                File.Create(Path.Combine(pathDay, "input.in"));
                File.Create(Path.Combine(pathDay, "input_test.in"));

                CreateSolutionFile(pathDay, year, day);
                CreateTestFile(pathDay, year, day);

                return "Path and files created";
            }
            catch (Exception e)
            {
                return $"Error creating Path and Files. Message '{e.Message}'";
            }
        }

        private static void CreateTestFile(string pathDay, int year, int day)
        {
            var contents = $@"using System;
                            |using System.Collections;
                            |using System.Collections.Generic;
                            |using System.Diagnostics;
                            |using System.Linq;
                            |
                            |namespace AdventOfCode.Y2018.Day{day.ToString("00")}
                            |{{
                            |    class Solution : Solver
                            |    {{
                            |        public string GetName() => ""Day{day.ToString("00")}"";
                            |
                            |        public void Test(string input) => new Test().Run(input);
                            |
                            |        public IEnumerable<(string, long)> Solve(string input)
                            |        {{
                            |            yield return PartOne(input);
                            |            yield return PartTwo(input);
                            |        }}
                            |
                            |        public (string, long) PartOne(string input)
                            |        {{
                            |            var timer = Stopwatch.StartNew();
                            |            return ($""result"", timer.ElapsedMilliseconds);
                            |        }}
                            |
                            |        public (string, long) PartTwo(string input)
                            |        {{
                            |            var timer = Stopwatch.StartNew();
                            |            return ($""result"", timer.ElapsedMilliseconds);
                            |        }}
                            |    }}
                            |}}".StripCode();

            File.WriteAllText(Path.Combine(pathDay, "Solution.cs"), contents);
        }

        private static void CreateSolutionFile(string pathDay, int year, int day)
        {
            var contents = $@"using System;
                            |using System.Collections;
                            |using System.Collections.Generic;
                            |using System.Diagnostics;
                            |using System.Linq;
                            |
                            |namespace AdventOfCode.Y2018.Day{day.ToString("00")}
                            |{{
                            |    public class Test : Tester
                            |    {{
                            |        public void Run(string input)
                            |        {{
                            |            Debug.Assert(true == true);
                            |        }}
                            |    }}
                            |}}".StripCode();

            File.WriteAllText(Path.Combine(pathDay, "Test.cs"), contents);
        }
    }

    static class PreparerExtensions
    {
        public static string StripCode(this string data)
        {
            return string.Join("\r\n", data.Split("\r\n").Select(d => d.Trim().Replace("|", "")));
        }
    }
}