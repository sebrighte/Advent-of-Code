using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day10: ...")]
    //[ProblemName("Day10: ...@TEST@")]
    class Day10 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input).First();
        public object PartTwo(string input) => null;

        private IEnumerable<object> Solver(string inData)
        {
            //List<string> input = inData.Split("\r\n").ToList();
            yield return $"To Do {inData}";
        }
    }
}
