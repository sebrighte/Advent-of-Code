using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 6: Signals and Noise")]
    class Day06 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input,false).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<object> Day1(string inData, bool part2 = false)
        {
            List<string> input = inData.Split("\r\n").ToList();
            string retVal = "";

            string[] code = new string[8];

            foreach (var item in input)
                for (int i = 0; i < 8; i++)
                    code[i] += item[i];

            for (int i = 0; i < 8; i++)
                retVal += !part2? GetMostFrequentChar(code[i]).ToString(): GetLeastFrequentChar(code[i]).ToString();

            yield return retVal;
        }

        static char GetMostFrequentChar(string str)
        {
            Dictionary<char, int> result = str.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            return result.Where(x => x.Value == result.Values.Max()).Select(x => x.Key).First();
        }

        static char GetLeastFrequentChar(string str)
        {
            Dictionary<char, int> result = str.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            return result.Where(x => x.Value == result.Values.Min()).Select(x => x.Key).First();
        }
    }
}
