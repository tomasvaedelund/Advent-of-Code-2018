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

            // 4104510979 <
            return (result, timer.ElapsedMilliseconds);
        }

        public (string result, long time) PartTwo(string input)
        {
            var timer = Stopwatch.StartNew();
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
            else
            {
                recipes.AddRange(score.ToIntList());
            }

            for (int i = 0; i < elves.Length; i++)
            {
                elves[i] = (elves[i] + recipes[elves[i]] + 1) % recipes.Count;
            }

            return (elves: elves, recipes: recipes);
        }

        public string GetResult(int numRecipes, List<int> recipes)
        {
            return string.Join("", recipes.Skip(numRecipes).Take(10));
        }
    }

    public static class SolutionExtensions
    {
        public static List<int> ToIntList(this int number)
        {
            List<int> listOfInts = new List<int>();

            while (number > 0)
            {
                listOfInts.Add(number % 10);
                number = number / 10;
            }

            listOfInts.Reverse();

            return listOfInts;
        }
    }
}