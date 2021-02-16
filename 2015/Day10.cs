using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Day10
{
    [ProblemName("Day 10: Elves Look, Elves Say")]
    class Day10 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private object Day1(string inData)
        {
            return LookAndSay("1113122113").Take(40).Last().Length;
        }

        private object Day2(string inData)
        {
            return LookAndSay("1113122113").Take(50).Last().Length;
        }

        IEnumerable<string> LookAndSay(string input)
        {
            List<string> newStringList = new List<string>();
            while (true)
            {
                newStringList.Clear();
                int ctr = 0;
                while (ctr < input.Length)
                {
                    if (ctr < input.Length - 2 && input[ctr] == input[ctr + 1] && input[ctr] == input[ctr + 2])
                    {
                        newStringList.Add(3.ToString());
                        newStringList.Add(input[ctr].ToString());
                        ctr += 3;
                    }
                    else if (ctr < input.Length - 1 && input[ctr] == input[ctr + 1])
                    {
                        newStringList.Add(2.ToString());
                        newStringList.Add(input[ctr].ToString());
                        ctr += 2;
                    }
                    else
                    {
                        newStringList.Add(1.ToString());
                        newStringList.Add(input[ctr].ToString());
                        ctr += 1;
                    }
                }
                input = string.Join("", newStringList);
                yield return input;
            }
        }
    }
}
