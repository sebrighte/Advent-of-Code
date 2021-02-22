using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 1: Not Quite Lisp")]
    class Day01 : BaseLine, Solution
    {
        public object PartOne(string input) => ProcessDay1(input);
        public object PartTwo(string input) => ProcessDay2(input);

        private object ProcessDay1(string inData)
        {
            List<string> input = inData.Split("\n").ToList();

            int floor = 0;
            foreach (var (item, index) in input.WithIndex())
            {
                floor += item.CountFindChar('(');
                floor -= item.CountFindChar(')');
            }
            return floor;
        }

        private object ProcessDay2(string inData)
        {
            List<string> input = inData.Split("\n").ToList();

            int floor = 0;
            int indexFloor = 0;

            foreach (string str in input)
            {
                foreach (var (item, index) in str.ToArray().WithIndex())
                {
                    if (item == '(') floor++;
                    if (item == ')') floor--;
                    if (floor == -1)
                    {
                        indexFloor = index + 1;
                        break;
                    }
                }
            }
            return indexFloor;
        }
    }
}
