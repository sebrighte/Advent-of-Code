using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 1: No Time for a Taxicab")]
    class Day01 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            //inData = "R2, L3";// 5 blocks away.
            //inData = "R2, R2, R2"; // 2 blocks away.
            //inData = "R5, L5, R5, R3"; // 12 blocks away.
            List<string> input = inData.Split(", ").ToList();
            int x = 0;
            int y = 0;
            int dir = 0;
            foreach (var inst in input)
            {
                string turn = inst.Substring(0, 1);
                int dist = int.Parse(inst.Substring(1));

                switch(turn)
                {
                    case "R":
                        if (dir < 3) dir++;
                        else if (dir == 3) dir = 0;
                        break;
                    case "L":
                        if (dir > 0) dir--;
                        else if (dir == 0) dir = 3;
                        break;
                }

                switch(dir)
                {
                    case 0: x += dist; break;
                    case 1: y += dist; break;
                    case 2: x -= dist; break;
                    case 3: y -= dist; break;
                }
            }
            yield return Math.Abs(x) + Math.Abs(y);
        }

        private IEnumerable<object> Day2(string inData)
        {
            //inData = "R8, R4, R4, R8";
            List<string> input = inData.Split(", ").ToList();
            Dictionary<string, int> visits = new Dictionary<string, int>();
            int x = 0;
            int y = 0;
            int dir = 0;
            foreach (var inst in input)
            {
                string turn = inst.Substring(0, 1);
                int dist = int.Parse(inst.Substring(1));

                switch (turn)
                {
                    case "R":
                        if (dir < 3) dir++;
                        else if (dir == 3) dir = 0;
                        break;
                    case "L":
                        if (dir > 0) dir--;
                        else if (dir == 0) dir = 3;
                        break;
                }

                switch (dir)
                {
                    case 0:
                        for (int i = 0; i < dist; i++)
                        {
                            if (!visits.ContainsKey($"{++x}-{y}"))
                                visits.Add($"{x}-{y}", 0);
                            else
                                yield return Math.Abs(x) + Math.Abs(y);
                        }
                        break;
                    case 1:
                        for (int i = 0; i < dist; i++)
                        {
                            if (!visits.ContainsKey($"{x}-{++y}"))
                                visits.Add($"{x}-{y}", 0);
                            else
                                yield return Math.Abs(x) + Math.Abs(y);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < dist; i++)
                        {
                            if (!visits.ContainsKey($"{--x}-{y}"))
                                visits.Add($"{x}-{y}", 0);
                            else
                                yield return Math.Abs(x) + Math.Abs(y);
                        }
                        break;
                    case 3:
                        for (int i = 0; i < dist; i++)
                        {
                            if (!visits.ContainsKey($"{x}-{--y}"))
                                visits.Add($"{x}-{y}", 0);
                            else
                                yield return Math.Abs(x) + Math.Abs(y);
                        }
                        break;
                }
            }
        }
    }
}
