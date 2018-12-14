using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day14
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var elves = new int[] { 0, 1 };
            var recipes = new List<int>() { 3, 7 };

            TestCreateNewRecipes(sut, elves, recipes);

            ShouldImproveAfterXRecipes(sut, elves, recipes, 5, "0124515891");

            ShouldImproveAfterXRecipes(sut, elves, recipes, 9, "5158916779");

            ShouldImproveAfterXRecipes(sut, elves, recipes, 18, "9251071085");

            ShouldImproveAfterXRecipes(sut, elves, recipes, 2018, "5941429882");

            Debug.Assert(true == true);
        }

        private void TestCreateNewRecipes(Solution sut, int[] elves, List<int> recipes)
        {
            (elves, recipes) = sut.CreateNewRecipes(elves, recipes);

            Debug.Assert(elves.SequenceEqual(new int[] { 0, 1 }));
            Debug.Assert(recipes.SequenceEqual(new List<int> { 3, 7, 1, 0 }));

            (elves, recipes) = sut.CreateNewRecipes(elves, recipes);

            Debug.Assert(elves.SequenceEqual(new int[] { 4, 3 }));
            Debug.Assert(recipes.SequenceEqual(new List<int> { 3, 7, 1, 0, 1, 0 }));

            (elves, recipes) = sut.CreateNewRecipes(elves, recipes);

            Debug.Assert(elves.SequenceEqual(new int[] { 6, 4 }));
            Debug.Assert(recipes.SequenceEqual(new List<int> { 3, 7, 1, 0, 1, 0, 1 }));

            (elves, recipes) = sut.CreateNewRecipes(elves, recipes);

            Debug.Assert(elves.SequenceEqual(new int[] { 0, 6 }));
            Debug.Assert(recipes.SequenceEqual(new List<int> { 3, 7, 1, 0, 1, 0, 1, 2 }));

            (elves, recipes) = sut.CreateNewRecipes(elves, recipes);

            Debug.Assert(elves.SequenceEqual(new int[] { 4, 8 }));
            Debug.Assert(recipes.SequenceEqual(new List<int> { 3, 7, 1, 0, 1, 0, 1, 2, 4 }));
        }

        private void ShouldImproveAfterXRecipes(Solution sut, int[] elves, List<int> recipes, int numRecipes, string expected)
        {
            var result = sut.CreateNewRecipes(elves, recipes);

            while (result.recipes.Count < numRecipes + 10)
            {
                result = sut.CreateNewRecipes(result.elves, result.recipes);
            }

            var fact = sut.GetResult(numRecipes, recipes);

            Debug.Assert(expected == fact);
        }
    }
}