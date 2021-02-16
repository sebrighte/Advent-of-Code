using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015.NoFile
{
    [ProblemName("Day 20: ...")]
    class Day20 : BaseLine, Solution
    {
        public Day20() { ReqDataFile = false; }

        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<string> Day1(string inData, bool part2 = false)
        {
            
            //List<string> input = inData.Split("\r\n").ToList();
            yield return $"To Do {inData}";
        }
    }
}
