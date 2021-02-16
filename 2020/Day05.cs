using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 5: Binary Boarding")]
    class Day05 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            List<string> list = inData.Split("\n").ToList();

            int current = 0;
            int max = 0;
            int a, b;
            int seatNo = 0;

            List<int> seatArray = new List<int>(0);
            for (a = 0; a < list.Count; a++)
            {
                current = getSeat(list[a]);
                seatArray.Add(current);
                if (current > max) max = current;
            }

            seatArray.Sort();

            for (b = 0; b < seatArray.Count - 1; b++)
            {
                if (seatArray[b] != seatArray[b + 1] - 1) seatNo = seatArray[b + 1] - 1; //or seatNo = seatArray[b] + 1; 
            }
            //Console.WriteLine("Day 5/1: The max seat ID is " + max);
            //Console.WriteLine("Day 5/2: My seat ID is " + seatNo);
            if(!part2)
                yield return max;
            else
                yield return seatNo;
        }

        private static int getSeat(string seatRef)
        {
            double valrup = 127;
            double valrdn = 0;
            double valcup = 7;
            double valcdn = 0;

            int[] binarArray = { 64, 32, 16, 8, 4, 2, 1 };

            /*
            Start by considering the whole range, rows 0 through 127.
            F means to take the lower half, keeping rows 0 through 63.
            B means to take the upper half, keeping rows 32 through 63.
            F means to take the lower half, keeping rows 32 through 47.
            B means to take the upper half, keeping rows 40 through 47.
            B keeps rows 44 through 47.
            F keeps rows 44 through 45.
            The final F keeps the lower of the two, row 44.

            FBFBBFFRLR 0101100
            0-127
            F 0-63 (64)
            B 32-63 (32)
            F 32-47 (16)
            B 40-47 (8)
            B 44-47 (4) 
            F 44-45 (2)
            F 44-44 (1)
            R 7-4
            L 5-4
            R 5-5

            BBFBFBBLRL = 858
            B 127-64
            B 127-96
            F 111-96
            B 111-104
            F 107-104
            B 107-106
            B 107-107
            L 3-0
            R 3-2
            L 2-2
            */

            for (int ctr = 0; ctr < 7; ++ctr)
            {
                if (seatRef.Substring(ctr, 1) == "F")
                {
                    valrup -= binarArray[ctr];
                }
                else if (seatRef.Substring(ctr, 1) == "B")
                {
                    valrdn += binarArray[ctr];
                }
            }

            for (int ctr = 7; ctr < 10; ++ctr)
            {
                if (seatRef.Substring(ctr, 1) == "L")
                {
                    valcup -= binarArray[ctr - 3];
                }
                else if (seatRef.Substring(ctr, 1) == "R")
                {
                    valcdn += binarArray[ctr - 3];
                }
            }
            return (int)((valrdn * 8) + valcdn);
        }
    }
}