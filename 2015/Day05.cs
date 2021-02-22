using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 5: Doesn't He Have Intern-Elves For This?")]
    class Day05 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private object Day1(string inData)
        {
            int goodCtr = 0;
            foreach (var item in inData.Split("\n"))
            {
                Match match1 = new Regex(@"((\w*[aeuio]\w*){3,})").Match(item); //vowels
                Match match2 = new Regex(@"(\w{1})\1").Match(item); //dup letters
                Match match3 = new Regex(@"(ab|cd|pq|xy)").Match(item); //excluded words (not)
                if (match1.Success && match2.Success && !match3.Success) goodCtr++;
            }
            return goodCtr;
        }

        private object Day2(string inData)
        {
            int goodCtr = 0;
            foreach (var item in inData.Split("\n"))
            {
                Match match1 = new Regex(@"([a-z][a-z]).*\1").Match(item);//repeated words
                Match match2 = new Regex(@"([a-z])[a-z]\1").Match(item); //repeat split
                if (match1.Success && match2.Success) goodCtr++;
            }
            return goodCtr;
        }
    }
}
