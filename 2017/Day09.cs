using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day9: Stream Processing")]
    class Day09 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => null;// Day1(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            yield return $"To Do {inData}";
        }
    }
}
