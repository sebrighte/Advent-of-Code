using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2015.Day04
{
    [ProblemName("Day 4: The Ideal Stocking Stuffer")]
    class Day04 : BaseLine, Solution
    {
        public object PartOne(string input) => SearchForHash5("ckczppom");
        public object PartTwo(string input) => SearchForHash6("ckczppom");

        public static string returnCreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private object SearchForHash6(string input)
        {
            long ctr = 0;
            ctr = 3900000;

            bool found = false;
            //string testkey = "abcdef";
            //string testkey = "pqrstuv";
            string key = input;
            while (!found)
            {
                if (returnCreateMD5(key + ctr).Substring(0, 6) == "000000")
                {
                    found = true;
                    break;
                }
                ctr++;
            }
            return ctr;
        }

        private string SearchForHash5(string input)
        {
            long ctr = 0;

            //if (leading == 6)
            //    ctr = 3900000;
            //if (leading == 5)
            //    ctr = 100000;

            bool found = false;
            //string testkey = "abcdef";
            //string testkey = "pqrstuv";
            string key = input;
            while (!found)
            {
                if (returnCreateMD5(key + ctr).Substring(0, 5) == "00000")
                {
                    found = true;
                    break;
                }
                ctr++;
            }
            return ctr.ToString();
        }
    }
}


