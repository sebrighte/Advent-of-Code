using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 9: Explosives in Cyberspace")]
    class Day09 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).Max();
        public object PartTwo(string input) => Day2(input).Max();

        private IEnumerable<object> Day1(string inData)
        {
            int decryptedStrLen = 0;

            while (inData.Length > 0)
            {
                string marker = inData.Substring(0, inData.IndexOf(')') - inData.IndexOf('(') + 1);
                var matches = Regex.Match(marker, @"(\d*)x(\d*)");
                var (rep, mult) = (matches.Groups[1].Value.ToInt32(), matches.Groups[2].Value.ToInt32());
                decryptedStrLen += inData.Substring(marker.Length, rep).Repeat(mult).Length;
                inData = inData.Remove(0, marker.Length + rep);

                yield return decryptedStrLen;
            }
        }

        private IEnumerable<object> Day2(string inData)
        {
            yield return Expand(inData, 0, inData.Length);
        }

        long Expand(string input, int i, int lim)
        {
            long res = 0;
            while (i < lim)
            {
                if (input[i] == '(')
                {
                    var j = input.IndexOf(')', i + 1);
                    var matches = Regex.Match(input.Substring(i + 1, j - i - 1), @"(\d*)x(\d*)");
                    var (length, mult) = (matches.Groups[1].Value.ToInt32(), matches.Groups[2].Value.ToInt32());
                    res += Expand(input, j + 1, j + length + 1) * mult;
                    i = j + length + 1;
                }
                else
                {
                    res++;
                    i++;
                }
            }
            return res;
        }
    }
}
