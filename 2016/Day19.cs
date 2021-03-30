using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 19: An Elephant Named Joseph")]
    class Day19 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(3012210).First();
        public object PartTwo(string input) => Day2(3012210).First();

        Dictionary<int, int> game = new Dictionary<int, int>();

        private IEnumerable<object> Day1(int ctr)
        {
            long check = 1;
            long lastcheck = 0;
            while (check < ctr)
            {
                lastcheck = check;
                check *= 2;
            }
            yield return ((ctr - lastcheck) * 2) + 1;
        }

        private IEnumerable<object> Day2(int ctr)
        {
            long check = 10;
            long lastcheck = 0;
            while (check < ctr)
            {
                lastcheck = check;
                check = (check * 3) - 2;
            }
            yield return ctr - lastcheck + 1;
        }
    }
}
