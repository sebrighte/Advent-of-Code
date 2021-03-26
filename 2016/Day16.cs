using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 16: Dragon Checksum")]
    class Day16 : BaseLine, Solution
    {
        //public object PartOne(string input) => Day1("10000", 20).First();
        public object PartOne(string input) => Day1("10111011111001111", 272).First();
        public object PartTwo(string input) => Day1("10111011111001111", 35651584).First();

        private IEnumerable<object> Day1(string diskData, int diskSize)
        {
            StringBuilder sb = new StringBuilder();
            while (diskData.Length < diskSize)
            {
                sb.Clear();
                foreach (var digit in diskData.Reverse()) sb.Append(digit == '0' ? "1" : "0");
                diskData = $"{diskData}0{sb.ToString()}";
            }
            string checksum = diskData = diskData.Substring(0, diskSize);
            while (checksum.Length%2==0 || checksum == diskData)
            {
                sb.Clear();
                for (int i = 0; i < checksum.Length-1; i++) sb.Append(checksum[i] == checksum[i++ + 1] ? "1" : "0");
                checksum = sb.ToString(); 
            }
            yield return checksum;
        }
    }
}
