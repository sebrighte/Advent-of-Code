using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    class IPRange { public long Start; public long End; }

    [ProblemName("Day20: Firewall Rules")]
    class Day20 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input, 4294967295, false);
        public object PartTwo(string input) => Solver(input, 4294967295, true);

        private string Solver(string inData, long max, bool part2)
        {
            //inData = "5-8\r\n0-2\r\n4-7";

            List<string> input = inData.Split("\r\n").ToList();
            var blacklist = new List<IPRange>();

            foreach (var ipRangeStr in input)
            {
                var ipRange = new IPRange();
                var match = Regex.Match(ipRangeStr, @"(\d*)-(\d*)");
                ipRange.Start = match.Groups[1].Value.ToInt64();
                ipRange.End = match.Groups[2].Value.ToInt64();
                blacklist.Add(ipRange);
            }

            long result = 0;
            var ipCount = 0;
            var entry = blacklist.FirstOrDefault(a => a.Start <= result && a.End >= result);

            while (result <= max) 
            {
                while (entry != null)
                {
                    result = entry.End + 1;
                    entry = blacklist.FirstOrDefault(b => b.Start <= result && b.End >= result);
                }

                if(!part2) return $"{result}";

                if (result <= max)
                {
                    ipCount++;
                    result++;
                    entry = blacklist.FirstOrDefault(c => c.Start <= result && c.End >= result);
                }
            }
            if(part2) return $"{ipCount}";
            return "";
        }
    }
}
