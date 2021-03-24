using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 15: Timing is Everything")]
    class Day15 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, 11, 0).First();

        private IEnumerable<object> Day1(string inData, int pstns = 0, int pstn = 0)
        {
            List<disc> sculpture = new List<disc>();
            var input = inData.Split("\r\n").ToList();

            foreach (var item in input)
            {
                var match = Regex.Match(item, @"Disc #(\d*) has (\d*) positions; at time=0, it is at position (\d*).").Groups;
                sculpture.Add(new disc(match[1].Value.ToInt32(), match[2].Value.ToInt32(), match[3].Value.ToInt32()));
            }
            
            if(pstns + pstn != 0) sculpture.Add(new disc(sculpture.Count()+1, pstns, pstn));

            int i = 0;
            while (true)
            {
               if(sculpture.Where(a => a.Tick() != 0).Count() == 0)
                    yield return i;
                i++;
            }
        }
    }

    class disc
    {
        int positions;
        int position;

        public disc(int no, int poss, int pos)
        {
            positions = poss;
            position = pos;
            this.Tick(no-1);
        }

        public int Tick(int cnt = 1)
        {
            for (int i = 0; i < cnt; i++)
            {
                position = position == positions - 1 ? 0 : position + 1;
            }
            return position;
        }
    }
}
