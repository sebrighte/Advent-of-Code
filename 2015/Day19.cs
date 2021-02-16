using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 19: Medicine for Rudolph")]
    class Day19 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => null;

        List<KeyValuePair<string, string>> dictGrid = new List<KeyValuePair<string, string>>();

        private IEnumerable<string> Day1(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            string word = input[input.Count -1];
            input.RemoveRange(input.Count - 2, 2);

            foreach (var line in input)
            {
                var match = Regex.Match(line, @"(.*) => (.*)");
                dictGrid.Add(new KeyValuePair<string, string>(match.Groups[1].Value, match.Groups[2].Value));
            }
            yield return $"To Do {string.Join("\n",input.ToArray())} {word}";
        }

        private IEnumerable<string> Day2(string inData)
        {
            //List<int> list = inData.Split("\n").Select(x => Int32.Parse(x)).ToList();
            yield return $"To Do {inData}";
        }
    }
}