using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day4: High-Entropy Passphrases")]
    class Day04 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).Count();
        public object PartTwo(string input) => Day2(input).Count();

        private IEnumerable<object> Day1(string inData)
        {
            foreach (var passPhrase in inData.Split("\r\n").ToList())
            {
                if (!passPhrase.Split(" ").GroupBy(x => x).Where(g => g.Count() > 1).Any())
                    yield return 1;
            }
        }

        private IEnumerable<object> Day2(string inData)
        {
            foreach (var passPhrase in inData.Split("\r\n").ToList())
            {
                var tmp = new List<string>();
                foreach (var item in passPhrase.Split(" "))
                {
                    tmp.Add(String.Concat(item.OrderBy(c => c)));
                }
                if (!tmp.GroupBy(x => x).Where(g => g.Count() > 1).Any())
                    yield return 1;
            }
        }
    }
}
