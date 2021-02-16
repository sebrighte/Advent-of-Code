using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 9: Encoding Error")]
    class Day09 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        static List<long> clistLng;
        static int cmultilpes;

        private IEnumerable<long> Day1(string inData, bool part2 = false)
        {
            List<long> listval = inData.Split("\r\n").Select(x => Convert.ToInt64(x)).ToList();

            clistLng = listval;
            cmultilpes = 25;

            long valDay8pt1 = isValidLoop();
            long valDay8pt2 = findContiguous(valDay8pt1);
            
            if(!part2)
                yield return valDay8pt1;
            else
                yield return valDay8pt2;
        }

        public static bool isValid(int start, long val)
        {
            bool isValid = false;
            int st = start - cmultilpes;

            for (int p = st; p < start; p++)
            {
                for (int i = st; i < start; i++)
                {
                    if ((clistLng[i] + clistLng[p] == val) && (clistLng[i] != clistLng[p])) isValid = true;
                }
            }
            return isValid;
        }

        public static long isValidLoop()
        {
            long ans = 0;
            for (int p = cmultilpes; p < clistLng.Count; p++)
            {
                if (!isValid(p, clistLng[p]))
                {
                    ans = clistLng[p];
                }
            }
            return ans;
        }

        public static long findContiguous(long val)
        {
            int start = 0;
            string ansLoc = "";
            string ansAll = "";
            long sumLoc = 0;

            for (int i = start; i < clistLng.Count; i++)
            {
                sumLoc = 0;
                ansLoc = "";
                for (int p = i; p < clistLng.Count; p++)
                {
                    ansLoc += clistLng[p] + ",";
                    sumLoc += clistLng[p];
                    if ((sumLoc == val) && (ansAll == ""))
                    {
                        ansAll = ansLoc;
                    }
                    if (sumLoc > val)
                    {
                        break;
                    }
                }
            }

            var v = ansAll.Split(',');
            long max = 0;
            long min = 1000000000000000000;
            for (int z = 0; z < v.Count() - 1; z++)
            {
                long tmp4 = long.Parse(v[z]);
                if (tmp4 > max) max = tmp4;
                if (tmp4 < min) min = tmp4;
            }
            return min + max;
        }
    }
}
