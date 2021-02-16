using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 1: Report Repair")]
    class Day01 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private object Day1(string inData)
        {
            List<int> list = inData.Split("\n").Select(x => Int32.Parse(x)).ToList();

            int answer = 0;
        
            for (int a = 0; a < list.Count; a++)
            {
                for (int b = 0; b < list.Count; b++)
                {
                    if ((answer == 0) && (list[a] + list[b] == 2020))
                    {
                        answer = list[a] * list[b];
                       return answer;
                    }
                }
            }
            return "Error!";
        }

        private object Day2(string inData)
        {
            List<int> list = inData.Split("\n").Select(x => Int32.Parse(x)).ToList();

            int answer = 0;

            for (int a = 0; a < list.Count; a++)
            {
                for (int b = 0; b < list.Count; b++)
                {
                    for (int c = 0; c < list.Count; c++)
                    {
                        if ((answer == 0) && (list[a] + list[b] + list[c] == 2020))
                        {
                            answer = list[a] * list[b] * list[c];
                            return answer;
                        }
                    }
                }
            }
            return "Error!";
        }
    }
}
