using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 2: Password Philosophy")]
    class Day02 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private object Day1(string inData)
        {
            List<string> list = inData.Split("\n").ToList();

            string minVal;
            string maxVal;
            string letter;
            string pswd;
            int countLetter = 0;
            int goodPasswords = 0;

            for (int a = 0; a < list.Count; a++)
            {
                var b = list[a].Split(' ');
                var c = b[0].Split('-');

                minVal = c[0];
                maxVal = c[1];
                letter = b[1].Replace(":", "");
                pswd = b[2];

                for (int i = 0; i < pswd.Length; ++i)
                {
                    if (letter == pswd.Substring(i, 1))
                    {
                        countLetter += 1;
                    }
                }
                if ((countLetter >= int.Parse(minVal)) && (countLetter <= int.Parse(maxVal))) goodPasswords += 1;
                countLetter = 0;
            }
            return goodPasswords;
        }

        private object Day2(string inData)
        {
            List<string> list = inData.Split("\n").ToList();

            string minVal;
            string maxVal;
            string letter;
            string pswd;
            int countLetter = 0;
            int goodPasswords = 0;

            for (int a = 0; a < list.Count; a++)
            {
                var b = list[a].Split(' ');
                var c = b[0].Split('-');

                minVal = c[0];
                maxVal = c[1];
                letter = b[1].Replace(":", "");
                pswd = b[2];

                for (int i = 0; i < pswd.Length; ++i)
                {
                    if (letter == pswd.Substring(i, 1))
                    {
                        countLetter += 1;
                    }
                }
                //if ((countLetter >= int.Parse(minVal)) && (countLetter <= int.Parse(maxVal))) goodPasswords += 1;
                //countLetter = 0;

                string firstLetter = pswd.Substring(int.Parse(minVal) - 1, 1);
                string secondLetter = pswd.Substring(int.Parse(maxVal) - 1, 1);

                if (firstLetter == letter || secondLetter == letter)
                {
                    if (firstLetter != secondLetter) goodPasswords += 1;
                }
            }
            return goodPasswords;
        }
    }
}
