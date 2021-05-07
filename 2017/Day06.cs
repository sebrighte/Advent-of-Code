using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day6: Memory Reallocation")]
    class Day06 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input, false).First();
        public object PartTwo(string input) => Solver(input, true).First();

        private IEnumerable<object> Solver(string inData, bool part2)
        {
            //inData = "0\t2\t7\t0";
            HashSet<string> history = new HashSet<string>();
            List<int> register = inData.Split("\t").Select(a => int.Parse(a)).ToList();
            string historyStr = "";
            int cnt = 0;

            while (!history.Contains(historyStr))
            {
                history.Add(historyStr);
                var highest = register.Select((val, index) => (val, index)).OrderByDescending(a => a.val).ThenBy(a => a.index).First();
                register[highest.index] = 0;
                int regIndex = highest.index;

                for (int i = 0; i < highest.val; i++)
                {
                    regIndex = regIndex + 1 == register.Count() ? 0 : regIndex + 1;
                    register[regIndex]++;
                }
                historyStr = string.Join("~", register);
                cnt++;
            }

            if(part2)
            {
                cnt = 0;
                string stop = historyStr;
                historyStr = "";
                while (stop != historyStr)
                {
                    var highest = register.Select((val, index) => (val, index)).OrderByDescending(a => a.val).ThenBy(a => a.index).First();
                    register[highest.index] = 0;
                    int regIndex = highest.index;

                    for (int i = 0; i < highest.val; i++)
                    {
                        regIndex = regIndex + 1 == register.Count() ? 0 : regIndex + 1;
                        register[regIndex]++;
                    }
                    historyStr = string.Join("~", register);
                    cnt++;
                }
            }
            yield return $"{cnt}";
        }
    }
}
