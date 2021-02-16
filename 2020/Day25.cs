using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 25: Combo Breaker")]
    class Day25 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => null;

        private IEnumerable<long> Day1(string inData, bool part2 = false)
        {
            yield return GetEncryptionKey(2959251, 4542595);
        }

        private long GetEncryptionKey(int doorCode, int keyCode)
        {
            long subjectNmuber = 7;
            long key1Loop = 0;
            long key2Loop = 0;
            long pKey1 = doorCode;
            long pKey2 = keyCode;

            bool found = false;
            int loop = 1;
            long loopVal = 1;

            while (!found)
            {
                loopVal *= subjectNmuber;
                loopVal %= 20201227;
                if (loopVal == pKey1)
                {
                    key1Loop = loop;
                    found = true;
                }
                loop++;
            }

            loopVal = 1;
            loop = 1;
            found = false;

            while (!found)
            {
                loopVal *= subjectNmuber;
                loopVal %= 20201227;
                if (loopVal == pKey2)
                {
                    key2Loop = loop;
                    found = true;
                }
                loop++;
            }

            subjectNmuber = pKey2;
            long result1 = 1;

            for (long i = 0; i < key1Loop; i++)
            {
                result1 *= subjectNmuber;
                result1 %= 20201227;
            }

            subjectNmuber = pKey1;
            long result2 = 1;

            for (long i = 0; i < key2Loop; i++)
            {
                result2 *= subjectNmuber;
                result2 %= 20201227;
            }

            if (result1 == result2) return result1;
            else return 0;
        }
    }
}
