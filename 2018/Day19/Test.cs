using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day19
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();
            var parsed = sut.ParseInput(input);

            ShouldReturnCorrectRegistry(sut, parsed, 6);

            Debug.Assert(true == true);
        }

        private void ShouldReturnCorrectRegistry(Solution sut, (int pointer, IEnumerable<(string method, int[] instruction)> commands) parsed, int expected)
        {
            var fact = sut.RunProgram(parsed.pointer, parsed.commands, new int[] { 0, 0, 0, 0, 0, 0 });

            Debug.Assert(fact[0] == expected);
        }
    }
}