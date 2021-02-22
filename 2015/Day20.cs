using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 20: Infinite Elves and Infinite Houses")]
    class Day20 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(34000000, 1000000, 10).First();
        public object PartTwo(string input) => Day1(34000000, 50, 11).First();

        private IEnumerable<int> Day1(int max, int steps, int multiple)
        {
            var presents = new int[1000000];
            for (var i = 1; i < presents.Length; i++)
            {
                var j = i;
                var step = 0;
                while (j < presents.Length && step < steps)
                {
                    presents[j] += multiple * i;

                    if (presents[j] >= max)
                        yield return j;

                    j += i;
                    step++;
                }
            }
            yield return -1;
        }
    }
}
