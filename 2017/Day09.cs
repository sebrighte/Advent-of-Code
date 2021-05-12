using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day9: Stream Processing")]
    class Day09 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input, true).First();
        public object PartTwo(string input) => Solver(input, false).First();

        private IEnumerable<object> Solver(string inData, bool part1)
        {
            int groups = 0;
            int garbage = 0;
            int curlyOpen = 0;
            bool ignoreNext = false;
            bool startGarbage = false;

            foreach (var charVal in inData)
            {
                if (ignoreNext) { ignoreNext = false; continue; }
                if (startGarbage && charVal != '>' && charVal != '!') { garbage++; continue; }
                if (charVal == '{') { curlyOpen++; groups += curlyOpen; }
                if (charVal == '}') if (curlyOpen > 0) curlyOpen--;
                if (charVal == '<') startGarbage = true;
                if (charVal == '>') startGarbage = false;
                if (charVal == '!') ignoreNext = true;
            }

            yield return part1? groups : garbage;
        }
    }
}
