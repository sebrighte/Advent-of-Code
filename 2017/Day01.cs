using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day01: ...")]
    class Day01 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            //List<string> input = inData.Split("\r\n").ToList();
            yield return $"To Do {inData}";
        }
    }
}
