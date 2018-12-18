using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day16
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var commands = new string[]
            {
                "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti",
                "gtir", "gtri", "gtrr", "eqir", "eqri", "eqrr"
            };

            ShouldReturnCorrectAmountOfValidCommands(
                sut,
                commands,
                new int[] { 3, 2, 1, 1 },
                new int[] { 9, 2, 1, 2 },
                new int[] { 3, 2, 2, 1 },
                3);

            Debug.Assert(true == true);
        }

        private void ShouldReturnCorrectAmountOfValidCommands(Solution sut, string[] commands, int[] v1, int[] v2, int[] v3, int expected)
        {
            var fact = sut.GetMatchingMethods(commands, (v1, v2, v3)).Sum();

            Debug.Assert(fact == expected);
        }
    }
}