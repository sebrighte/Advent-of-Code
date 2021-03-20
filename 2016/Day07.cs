using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 7: Internet Protocol Version 7")]
    class Day07 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            int ctr = 0;

            foreach (var item in input)
            {
                string add = item.ReplaceAndExport('[', ']', out string export);
                ctr += Regex.Match(add, @"(.)(.)\2\1").Success ? 1 : 0; //check 'abba' in address
                ctr += Regex.Match(export, @"(.)(.)\2\1").Success ? -1 : 0; //rule out 'abba' in brackets
                ctr += Regex.Match(add, @"([a-z])\1{3}").Success ? -1 : 0; //rule out 'aaaa'
            }
            yield return ctr;
        }

        private IEnumerable<object> Day2(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            int ctr = 0;

            foreach (var item in input)
                ctr += CheckPattern(item) ? 1 : 0;

            yield return ctr;
        }

        bool CheckPattern(string item)
        {
            List<string> ret = new List<string>();
            string add = item;
            string bkt = "";

            while (add.Contains("["))
            {
                add = add.ReplaceAndExport('[', ']', out string export);
                bkt += export;
            }
            for (int i = 0; i < add.Length - 2; i++)
                if (add[i] == add[i + 2])
                    ret.Add(add[i + 1].ToString() + add[i].ToString() + add[i + 1].ToString());

            return ret.Any(a => bkt.Contains(a));
        }
    }
}
