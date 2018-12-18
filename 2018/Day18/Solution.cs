using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Y2018.Day18
{
    class Solution : Solver
    {
        public string GetName() => "Day18";

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

            var matrix = PassXMinutes(parsed, 10);

            var wooded = matrix.Cast<char>().Count(c => c == '|');
            var lumberyards = matrix.Cast<char>().Count(c => c == '#');
            var result = wooded * lumberyards;

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            var parsed = ParseInput(input);
            var amounts = new List<int>();
            var minutes = 1000000000;
            var index = 0;
            var length = 0;

            while (minutes-- > 0)
            {
                parsed = PassOneMinute(parsed);
                var wooded = parsed.Cast<char>().Count(c => c == '|');
                var lumberyards = parsed.Cast<char>().Count(c => c == '#');
                var amount = wooded * lumberyards;

                amounts.Add(amount);
                var temp = HasRepeatingPattern(amounts);
                if (temp.repeats)
                {
                    index = temp.pos;
                    length = temp.length;
                    break;
                }
            }

            var pos = (1000000000 - index) % length;

            // - 1 since we want to count from the pos of the last item BEFORE pattern starts repeating
            var result = amounts[index + pos - 1];

            return ($"{result}", timer.ElapsedMilliseconds);
        }

        public char[,] ParseInput(string input)
        {
            var split = input.Split("\r\n");
            var matrix = new char[split.Length, split[0].Length];

            for (int y = 0; y < split.Length; y++)
            {
                for (int x = 0; x < split[y].Length; x++)
                {
                    matrix[y, x] = split[y][x];
                }
            }

            return matrix;
        }

        public char[,] PassXMinutes(char[,] matrix, int minutes)
        {
            while (minutes-- > 0)
            {
                matrix = PassOneMinute(matrix);
                // WriteMatrix(matrix, minutes);
            }

            return matrix;
        }

        public char[,] PassOneMinute(char[,] matrix)
        {
            var tempMatrix = new char[matrix.GetLength(0), matrix.GetLength(1)];
            Array.Copy(matrix, 0, tempMatrix, 0, matrix.Length);

            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    var oldValue = matrix[y, x];
                    var newValue = GetNewValueForAcre(x, y, matrix);
                    tempMatrix[y, x] = newValue;
                }
            }

            return tempMatrix;
        }

        public char GetNewValueForAcre(int x, int y, char[,] matrix)
        {
            switch (matrix[y, x])
            {
                case '.':
                    if (GetAdjacentAcres(x, y, matrix).Count(a => a == '|') >= 3)
                    {
                        return '|';
                    }
                    return '.';
                case '|':
                    if (GetAdjacentAcres(x, y, matrix).Count(a => a == '#') >= 3)
                    {
                        return '#';
                    }
                    return '|';
                case '#':
                    if (GetAdjacentAcres(x, y, matrix).Count(a => a == '#') >= 1 && GetAdjacentAcres(x, y, matrix).Count(a => a == '|') >= 1)
                    {
                        return '#';
                    }
                    return '.';
                default:
                    throw new Exception($"Unknonw value '{matrix[y, x]}'");
            }
        }

        public static IEnumerable<T> GetAdjacentAcres<T>(int x, int y, T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);

            for (int j = y - 1; j <= y + 1; j++)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    if (i >= 0 && j >= 0 && i < columns && j < rows && !(j == y && i == x))
                    {
                        yield return arr[j, i];
                    }
                }
            }
        }

        public void WriteMatrix(char[,] matrix, int minute)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);
            var sb = new StringBuilder();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var c = (matrix[y, x] == '\0') ? '.' : matrix[y, x];
                    sb.Append(c);
                }

                //sb.AppendLine();
            }

            // sb.AppendLine();

            var wooded = matrix.Cast<char>().Count(c => c == '|');
            var lumberyards = matrix.Cast<char>().Count(c => c == '#');
            var result = wooded * lumberyards;

            using (var sw = new StreamWriter(@"c:\temp\day18.txt", true))
            {
                sw.WriteLine($"Minute: {minute}, result: {result}");
                sw.WriteLine(sb.ToString());
            }
        }

        public void PrintMatrix(char[,] matrix)
        {
            var height = matrix.GetLength(0);
            var width = matrix.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var c = (matrix[y, x] == '\0') ? '.' : matrix[y, x];
                    Console.Write(c);
                }

                Console.WriteLine();
            }
        }

        public (bool repeats, int pos, int length) HasRepeatingPattern(List<int> array)
        {
            for (int startPos = 0; startPos < array.Count; startPos++)
            {
                for (int sequenceLength = 1; sequenceLength <= (array.Count - startPos) / 2; sequenceLength++)
                {
                    var equals = true;
                    for (int i = 0; i < sequenceLength; i++)
                    {
                        if (array[startPos + i] != array[startPos + sequenceLength + i])
                        {
                            equals = false;
                        }
                    }
                    if (equals)
                    {
                        return (true, startPos, sequenceLength);
                    }
                }
            }

            return (false, 0, 0);
        }
    }
}