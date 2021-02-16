using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 20: Infinite Elves and Infinite Houses")]
    class Day20 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(34000000).First();
        public object PartTwo(string input) => null;

        private IEnumerable<string> Day1(int input)
        {
            //List<string> input = inData.Split("\r\n").ToList();
            yield return $"To Do {input}";
        }
    }
}
