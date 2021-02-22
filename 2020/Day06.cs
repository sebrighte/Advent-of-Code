using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 6: Custom Customs")]
    class Day06 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            List<string> list = inData.Split("\n").ToList();

            string answers = "";
            string answers2 = "";
            int count = 0;
            int count2 = 0;
            list.Add("");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != "")
                {
                    answers += list[i];
                    answers2 += list[i] + ",";
                }
                else
                {
                    answers = RemoveDuplicates(answers);
                    count += answers.Length;
                    answers = "";
                    count2 += getAmswers(answers2);
                    answers2 = "";
                }
            }
            if (!part2)
                yield return count;
            else
                yield return count2;
        }

        public static string RemoveDuplicates(string input)
        {
            return new string(input.ToCharArray().Distinct().ToArray());
        }

        public static int getAmswers(string grpAns)
        {
            //remove last comma
            grpAns = grpAns.Substring(0, grpAns.Length - 1);
            var grpSplit = grpAns.Split(',');
            int grp = grpSplit.Count();

            string grpAns2 = grpAns.Replace(",", "");
            grpAns2 = RemoveDuplicates(grpAns2);

            int score = 0;

            for (int i = 0; i < grpAns2.Length; i++)
            {
                int count = grpAns.Split(grpAns2[i]).Length - 1;
                if (count == grp) score++;
            }
            return score;
        }
    }
}