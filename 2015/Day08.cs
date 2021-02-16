using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Day08
{
    [ProblemName("Day 8: Matchsticks")]
    class Day08 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private int Day1(string inData)
        {
            List<string> input = inData.Split("\n").ToList();

            int sum = 0;
            foreach (var s in input)
            {
                var Unescaped = s.Substring(1, s.Length - 2).Replace("\\\"", "\"").Replace("\\\\", "@");
                Unescaped = Regex.Replace(Unescaped, @"\\x[0-9a-f]{2}", "?");
                sum += s.Length - Unescaped.Length;
            }
            return sum;
        }

        private int Day2(string inData)
        {
            List<string> input = inData.Split("\n").ToList();

            int sum = 0;
            foreach (var s in input)
            {
                var Escaped = "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
                sum += Escaped.Length - s.Length;
            }
            return sum;
        }
    }
}
