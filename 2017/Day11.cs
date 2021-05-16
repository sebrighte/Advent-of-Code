using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day11: Hex Ed")]
    class Day11 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input, true).First();
        public object PartTwo(string input) => Solver(input, false).First();

        private static IEnumerable<object> Solver(string inData, bool part1)
        {
            int maxDist = 0;
            int x = 0;
            int y = 0;
            int pathDist = 0;

            foreach (var dir in inData.Split(",").ToList())
            {
                switch (dir)
                {
                    case "n" : y += 2;   break;
                    case "ne": y++; x++; break;
                    case "se": y--; x++; break;
                    case "s" : y -= 2;   break;
                    case "sw": y--; x--; break;
                    case "nw": y++; x--; break;
                };
                pathDist = (Math.Abs(x) + Math.Abs(y)) / 2;
                if (pathDist > maxDist) maxDist = pathDist;
            }
            yield return part1? pathDist : maxDist;
        }
    }
}
