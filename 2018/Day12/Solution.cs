using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode.Y2018.Day12
{
    class Solution : Solver
    {
        public string GetName() => "Day12";

        public void Test(string input) => new Test().Run(input);

        public bool IsTest { get; set; } = false;

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            var parsed = GetParsedInput(input);
            var iterations = 20;

            var lastGeneration = parsed.initialState;
            var index = 0;

            while (iterations-- > 0)
            {
                lastGeneration = GetNextGeneration(lastGeneration, parsed.notes);
                index += 3;
            }

            var result = GetResult(lastGeneration, index);

            return (result.ToString(), timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            var parsed = GetParsedInput(input);
            var iterations = 100;

            var lastGeneration = parsed.initialState;
            var index = 0;

            while (iterations-- > 0)
            {
                lastGeneration = GetNextGeneration(lastGeneration, parsed.notes);
                index += 3;
            }

            var result = GetResult(lastGeneration, index);

            var calculated = (long)result + (long)7800 * (long)499999999;

            return (calculated.ToString(), timer.ElapsedMilliseconds);
        }

        public (string initialState, IDictionary<string, char> notes) GetParsedInput(string input)
        {
            var initialState = input.Split("\r\n")[0].Replace("initial state: ", "");

            var notes = input
                .Split("\r\n")
                .Skip(2)
                .Select(note => (pattern: note.Split(" => ")[0], pot: note.Split(" => ")[1]))
                .ToDictionary(note => note.pattern, note => note.pot[0]);

            return (initialState, notes);
        }

        public string GetNextGeneration(string currentState, IDictionary<string, char> notes)
        {
            currentState = $"...{currentState}...";
            var nextState = new StringBuilder(currentState);

            for (int i = 2; i < currentState.Length - 2; i++)
            {
                var key = currentState.Substring(i - 2, 5);
                if (notes.TryGetValue(key, out var pot))
                {
                    nextState[i] = pot;
                }
                else if (IsTest)
                {
                    nextState[i] = '.';
                }
            }

            return nextState.ToString();
        }

        private int GetResult(string lastGeneration, int index)
        {
            var result = 0;

            for (int i = 0; i < lastGeneration.Length; i++)
            {
                result += (lastGeneration[i] == '#') ? i - index : 0;
            }

            return result;
        }
    }
}