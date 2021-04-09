using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 19: An Elephant Named Joseph")]
    class Day19 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(3012210,false).First();
        public object PartTwo(string input) => Solver(3012210, true).First();

        private IEnumerable<object> Solver(int ctr, bool part2)
        {
            long check = 2;
            long lastcheck = 0;
            long ans = 0;

            if (!part2)
            {
                while (check < ctr)
                {
                    lastcheck = check;
                    check *= 2;
                }
                ans = (((ctr - lastcheck) * 2) + 1);
            }
            else
            {
                while (check < ctr)
                {
                    lastcheck = check;
                    check = ((check * 3) - 2);
                }
                ans = ((ctr - lastcheck) + 1);
            }
            yield return ans;
        }
    }
}
