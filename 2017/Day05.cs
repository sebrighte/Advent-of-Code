using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day5: A Maze of Twisty Trampolines, All Alike")]
    class Day05 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input, false).Count();
        public object PartTwo(string input) => Day1(input, true).Count();

        private IEnumerable<object> Day1(string inData, bool part2)
        {
            //inData = "0\r\n3\r\n0\r\n1\r\n-3";
            List<int> input = inData.Split("\r\n").Select(a => int.Parse(a)).ToList();
            int pos = 0;

            while (pos < input.Count() && !part2)
            {
                pos += input[pos]++;
                yield return 1;
            }

            while (pos < input.Count() && part2)
            {
                int value = input[pos];
                input[pos] += (value >= 3) ? -1 : 1;
                pos += value;
                yield return 1;
            }
        }
    }
}
