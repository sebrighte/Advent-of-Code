using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//URL: https://adventofcode.com/2020/day/5
//Data: C:\Advent\Data\Day5Input.txt

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 10: Adapter Array")]
    class Day10 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        static List<int> clistInt;

        private IEnumerable<long> Day1(string inData, bool part2 = false)
        {
            List<int> list = inData.Split("\n").Select(x => Convert.ToInt32(x)).ToList();

            clistInt = list;
            clistInt.Sort();

            int adaptor = 0;
            int adaptor1 = 0;
            int adaptor3 = 0;
            int diff;
            string diffval = "s,";
            long count = 1;

            for (int i = 0; i < clistInt.Count(); i++)
            {
                diff = clistInt[i] - adaptor;
                diffval += diff;
                if (diff == 1) adaptor1++;
                if (diff == 3) adaptor3++;
                adaptor = clistInt[i];
                if (i != clistInt.Count() - 1) diffval += ",";
                else diffval += ",e";
            }

            if(!part2)
                yield return adaptor1 * ++adaptor3;

            if (Regex.Matches(diffval, "3,1,1,1,1,3").Count > 0) //A7
                count *= Convert.ToInt64(Math.Pow(7, Regex.Matches(diffval, @"(?=3,1,1,1,1,3)").Count));
            
            if (Regex.Matches(diffval, "3,1,1,1,3").Count > 0) //B4
                count *= Convert.ToInt64(Math.Pow(4, Regex.Matches(diffval, @"(?=3,1,1,1,3)").Count));

            if (Regex.Matches(diffval, "3,1,1,3").Count > 0) //C2
                count *= Convert.ToInt64(Math.Pow(2, Regex.Matches(diffval, @"(?=3,1,1,3)").Count));

            if (Regex.Matches(diffval, "3,1,1,1,1,e").Count > 0) //D7
                count *= Convert.ToInt64(Math.Pow(7, Regex.Matches(diffval, @"(?=3,1,1,1,1,e)").Count));

            if (Regex.Matches(diffval, "3,1,1,1,e").Count > 0) //E7
                count *= Convert.ToInt64(Math.Pow(7, Regex.Matches(diffval, @"(?=3,1,1,1,e)").Count));

            if (Regex.Matches(diffval, "s,1,1,1,3").Count > 0) //F4
                count *= Convert.ToInt64(Math.Pow(4, Regex.Matches(diffval, @"(?=s,1,1,1,3)").Count));

            if (Regex.Matches(diffval, "s,1,1,1,1,3").Count > 0) //G7
                count *= Convert.ToInt64(Math.Pow(7, Regex.Matches(diffval, @"(?=s,1,1,1,1,3)").Count));

            if (part2)
                yield return count;
        }
    }
}