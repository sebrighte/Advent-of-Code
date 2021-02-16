using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 3: Toboggan Trajectory")]
    class Day03 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private object Day1(string inData)
        {
            List<string> list = inData.Split("\n").ToList();

            int posY = 1, trees = 0;
            int[] array1 = new int[] { 1, 3, 5, 7, 1 };
            int[] array2 = new int[] { 0, 0, 0, 0, 1 };

            for (int x = 0; x < array1.Length; ++x)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    string line = list[i];
                    string firstLetter = line.Substring(posY - 1, 1);
                    if (firstLetter == "#") trees++;
                    posY += array1[x];
                    i += array2[x];
                    if (posY > 31) posY -= 31;
                }
                if (array1[x] == 3) return trees;

                trees = 0;
                posY = 1;
            }
            return 0;
        }

        private object Day2(string inData)
        {
            List<string> list = inData.Split("\n").ToList();

            int posY = 1, trees = 0;
            int[] array1 = new int[] { 1, 3, 5, 7, 1 };
            int[] array2 = new int[] { 0, 0, 0, 0, 1 };
            int total = 0;

            for (int x = 0; x < array1.Length; ++x)
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    string line = list[i];
                    string firstLetter = line.Substring(posY - 1, 1);
                    if (firstLetter == "#") trees++;
                    posY += array1[x];
                    i += array2[x];
                    if (posY > 31) posY -= 31;
                }
                if (total == 0) total = trees;
                else total *= trees;

                trees = 0;
                posY = 1;
            }
            return total;
        }
    }
}
