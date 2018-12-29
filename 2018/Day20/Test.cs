using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2018.Day20
{
    public class Test : Tester
    {
        public void Run(string input)
        {
            var sut = new Solution();

            ShouldCalculateCorrectMaxDistance(sut, "^WNE$", 3);
            ShouldCalculateCorrectMaxDistance(sut, "^ENWWW(NEEE|SSE(EE|N))$", 10);
            ShouldCalculateCorrectMaxDistance(sut, "^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$", 18);
            ShouldCalculateCorrectMaxDistance(sut, "^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$", 23);
            ShouldCalculateCorrectMaxDistance(sut, "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$", 31);

            Debug.Assert(true == true);
        }

        private void ShouldCalculateCorrectMaxDistance(Solution sut, string input, int expected)
        {
            var test = sut.GenerateDoors(input);
            var fact = test.Select(x => x.to.d).Max();

            Debug.Assert(fact == expected);
        }
    }
}