using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day11
{
    class Test : Tester
    {
        public void Run(string input)
        {
            var solver = new Solution();

            Debug.Assert(solver.GetCellPowerLevel(122, 79, 57) == -5);
            Debug.Assert(solver.GetCellPowerLevel(217, 196, 39) == 0);
            Debug.Assert(solver.GetCellPowerLevel(101, 153, 71) == 4);

            Debug.Assert(solver.GetGridPowerLevel(33, 45, 18, 3) == 29);
            Debug.Assert(solver.GetGridPowerLevel(21, 61, 42, 3) == 30);
        }
    }
}