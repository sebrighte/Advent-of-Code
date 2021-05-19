using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day18: Duet")]
    //[ProblemName("Day18: ...@TEST@")]
    class Day18 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input).First();
        public object PartTwo(string input) => null;

        private static IEnumerable<object> Solver(string inData)
        {
            //List<string> input = inData.Split("\r\n").ToList();
            yield return $"To Do {inData}";
        }
    }
}
