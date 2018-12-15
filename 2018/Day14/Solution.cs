using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day14
{
    class Solution : Solver
    {
        public string GetName() => "Day14";

        public void Test(string input) => new Test().Run(input);

        public IEnumerable<(string result, long time)> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        public (string result, long time) PartOne(string input)
        {
            var timer = Stopwatch.StartNew();
            var elves = new int[] { 0, 1 };
            var recipes = new List<int>() { 3, 7 };
            var numRecipes = 157901;

            var temp = CreateNewRecipes(elves, recipes);

            while (temp.recipes.Count < numRecipes + 10)
            {
                temp = CreateNewRecipes(temp.elves, temp.recipes);
            }

            var result = GetResult(numRecipes, recipes);

            return (result, timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
            var elves = new int[] { 0, 1 };
            var recipes = new List<int>() { 3, 7 };
            var needle = "157901";
            var result = 0;

            var temp = CreateNewRecipes(elves, recipes);

            while (true)
            {
                temp = CreateNewRecipes(temp.elves, temp.recipes);

                result = GetIndexOfValue(recipes, needle);

                if (result >= 0)
                {
                    break;
                }
            }

            return ($"result", timer.ElapsedMilliseconds);
        }

        public (int[] elves, List<int> recipes) CreateNewRecipes(int[] elves, List<int> recipes)
        {
            var score = 0;

            for (int i = 0; i < elves.Length; i++)
            {
                score += recipes[elves[i]];
            }

            if (score == 0)
            {
                recipes.Add(0);
            }
            else if (score > 9)
            {
                recipes.Add(1);
                recipes.Add(score % 10);
            }
            else
            {
                recipes.Add(score);
            }

            for (int i = 0; i < elves.Length; i++)
            {
                elves[i] = (elves[i] + recipes[elves[i]] + 1) % recipes.Count;
            }

            return (elves: elves, recipes: recipes);
        }

        public int GetIndexOfValue(List<int> recipes, string needle)
        {
            var t1 = string.Concat(recipes.Last(needle.Length)).Equals(needle);
            var t2 = string.Concat(recipes.Last(needle.Length + 1).Take(needle.Length)).Equals(needle);

            if (t1)
            {
                return recipes.Count - needle.Length;
            }

            if (t2)
            {
                return recipes.Count - needle.Length - 1;
            }

            return -1;
        }

        public string GetResult(int numRecipes, List<int> recipes)
        {
            return string.Join("", recipes.Skip(numRecipes).Take(10));
        }
    }
}