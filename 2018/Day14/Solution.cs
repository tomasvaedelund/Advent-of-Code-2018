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
            var needle = new int[] { 1, 5, 7, 9, 0, 1 };

            var temp = CreateNewRecipesTwo(elves, recipes, needle);

            while (true)
            {
                temp = CreateNewRecipesTwo(temp.elves, temp.recipes, needle);

                if (temp.index > 0)
                {
                    break;
                }
            }

            return ($"{temp.index}", timer.ElapsedMilliseconds);
        }

        public (int[] elves, List<int> recipes) CreateNewRecipes(int[] elves, List<int> recipes)
        {
            var score = 0;

            for (int i = 0; i < elves.Length; i++)
            {
                score += recipes[elves[i]];
            }

            if (score > 9)
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

        public (int[] elves, List<int> recipes, int[] needle, int index) CreateNewRecipesTwo(int[] elves, List<int> recipes, int[] needle)
        {
            var score = 0;
            var index = 0;

            for (int i = 0; i < elves.Length; i++)
            {
                score += recipes[elves[i]];
            }

            if (score > 9)
            {
                recipes.Add(1);
                index = IsNeedleInHaystack(recipes, needle) ? recipes.Count : 0;
                if (index == 0)
                {
                    recipes.Add(score % 10);
                    index = IsNeedleInHaystack(recipes, needle) ? recipes.Count : 0;
                }
            }
            else
            {
                recipes.Add(score);
                index = IsNeedleInHaystack(recipes, needle) ? recipes.Count : 0;
            }

            for (int i = 0; i < elves.Length; i++)
            {
                elves[i] = (elves[i] + recipes[elves[i]] + 1) % recipes.Count;
            }

            return (elves: elves, recipes: recipes, needle: needle, index: index - needle.Length);
        }

        public bool IsNeedleInHaystack(IEnumerable<int> recipes, int[] needle)
        {
            var r = recipes.Skip(recipes.Count() - needle.Length).ToArray();

            for (int i = 0; i < needle.Length; i++)
            {
                if (r[i] != needle[i])
                {
                    return false;
                }
            }

            return true;
        }

        public string GetResult(int numRecipes, List<int> recipes)
        {
            return string.Join("", recipes.Skip(numRecipes).Take(10));
        }
    }
}