using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day2: Corruption Checksum")]
    class Day02 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            List<string> spreadsheet = inData.Split("\r\n").ToList();

            int retVal = 0;
            foreach (var row in spreadsheet)
            {
                var sp = row.Split("\t").Select(n => n.ToInt32());
                retVal += sp.Max() - sp.Min();
            }
            yield return $"{retVal}";
        }

        private IEnumerable<object> Day2(string inData)
        {
            //inData = "5\t9\t2\t8\r\n9\t4\t7\t3\r\n3\t8\t6\t5";
            List<string> spreadsheet = inData.Split("\r\n").ToList();
            int retVal = 0;
            foreach (var row in spreadsheet)
            {
                var sp = row.Split("\t").Select(n => n.ToInt32());
                retVal += (from a in sp
                             from b in sp
                             where a != b
                             where a % b == 0 || b % a == 0
                             select a / b).Max();
            }
            yield return $"{retVal}";
        }
    }
}
