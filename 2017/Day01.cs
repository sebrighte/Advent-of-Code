using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day1: Inverse Captcha")]
    class Day01 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            //inData = "122344456666788888";
            var matches = Regex.Matches(inData, @"([0-9])\1+");

            int retVal = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                int valOne = matches[i].Value[0].ToInt32();
                int cnt = matches[i].Value.Length;
                retVal += valOne * (cnt - 1);
            }
            retVal += inData[0] == inData[inData.Length - 1]?  inData[0].ToInt32() : 0;
            yield return $"{retVal}";
        }

        private IEnumerable<object> Day2(string inData)
        {
            int offset = inData.Length / 2;
            int retVal = 0;

            for (int i = 0; i < inData.Length; i++)
            {
                int mainVal = inData[0 + i].ToInt32();
                int offVal = 0;

                offVal = i < offset? inData[offset + i].ToInt32() : inData[i - offset].ToInt32();

                if (mainVal == offVal) retVal += mainVal;
            }
            yield return $"{retVal}";
        }
    }
}
