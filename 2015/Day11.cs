using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 11: Corporate Policy")]
    class Day11 : BaseLine, Solution
    {
        public object PartOne(string input) => Passwords("hepxxxzz").First();
        public object PartTwo(string input) => Passwords("heqaaaca").First();

        //public object PartOne(string input) => Passwords(input).First();
        //public object PartTwo(string input) => Passwords(input).Skip(1).First();

        //The next password after abcdefgh is abcdffaa.
        //The next password after ghijklmn is ghjaabcc, 

        IEnumerable<string> Passwords(string input)
        {
            var inDatatmp = input;

            while (true)
            {
                //Console.WriteLine(inDatatmp);
                inDatatmp = IncrementPassword(inDatatmp);
                Match match2 = new Regex(@"(abc|bcd|cde|def|efg|fgh|ghi|hij|ijk|jkl|klm|lmn|mno|nop|opq|pqr|qrs|rsr|stu|tuv|uvw|vwx|wxy|xyz)").Match(inDatatmp);
                if (!match2.Success) continue;
                MatchCollection match3 = new Regex(@"([a-z])\1+").Matches(inDatatmp);
                if (match3.Count != 2) continue;

                yield return inDatatmp;
            }
        }

        private string IncrementPassword(string pswd, int inc = 7)
        {
            StringBuilder sb = new StringBuilder(pswd);

            if (sb[inc] != 'z')
            {
                char p = sb[inc];
                sb.Remove(inc, 1);
                sb.Insert(inc,NextLetter(p).First());
            }
            else if (sb[inc] == 'z' && sb[inc - 1] != 'z')
            {
                char p = sb[inc];
                sb.Remove(inc, 1);
                sb.Insert(inc, NextLetter(p).First());
                sb = new StringBuilder(IncrementPassword(sb.ToString(), inc - 1));
            }
            else if (sb[inc] == 'z' && sb[inc - 1] == 'z')
            {
                char p = sb[inc];
                sb.Remove(inc, 1);
                sb.Insert(inc, NextLetter(p).First());
                sb = new StringBuilder(IncrementPassword(sb.ToString(), inc - 1));
            }
            return sb.ToString();
        }

        IEnumerable<char> NextLetter(char start)
        {
            var letters = "abcdefghjkmnpqrstuvwxyz";

            int startIndex = letters.IndexOf(start) +1;
            if ((char)letters[startIndex -1] == 'z')
            {
                startIndex = 0;
            }

            for (int i = startIndex; i < letters.Count(); i++)
            {
                yield return letters[i];
            }
        }
    }
}