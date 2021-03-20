using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 3: Squares With Three Sides")]
    class Day03 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<object> Day2(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            int valid = 0;

            for (int i = 0; i < input.Count; i++)
            {
                int[] sides1 = new int[3];
                int[] sides2 = new int[3];
                int[] sides3 = new int[3];

                for (int j = 0; j < 3; j++)
                {                   
                    var matches = Regex.Match(input[i], @"\s*(\d*)\s*(\d*)\s*(\d*)");
                    sides1[j] = int.Parse(matches.Groups[1].Value);
                    sides2[j] = int.Parse(matches.Groups[2].Value);
                    sides3[j] = int.Parse(matches.Groups[3].Value);
                    if (j < 2) i++;
                }

                if (isvalid(sides1)) valid++;
                if (isvalid(sides2)) valid++;
                if (isvalid(sides3)) valid++;
            }
            yield return valid;
        }

        private IEnumerable<object> Day1(string inData, bool part2 = false)
        {
            List<string> input = inData.Split("\r\n").ToList();
            int valid = 0;

            foreach (var triangle in input)
            {
                var matches = Regex.Match(triangle, @"\s*(\d*)\s*(\d*)\s*(\d*)");

                int[] sides = new int[3];
                sides[0] = int.Parse(matches.Groups[1].Value);
                sides[1] = int.Parse(matches.Groups[2].Value);
                sides[2] = int.Parse(matches.Groups[3].Value);

                if (isvalid(sides)) valid++;
            }
            yield return valid;
        }

        private bool isvalid(int[] triange)
        {
            Array.Sort(triange);
            return triange[0] + triange[1] > triange[2] ? true : false;
        }
    }
}
