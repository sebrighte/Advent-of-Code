using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 5: How About a Nice Game of Chess?")]
    class Day05 : BaseLine, Solution
    {
        public object PartOne(string input) => SearchForHashPt1("uqwqemis");
        public object PartTwo(string input) => SearchForHashPt2("uqwqemis");

        private object SearchForHashPt1(string input)
        {
            //return "1a3099aa";

            long ctr = 4500000;
            string password = "";
            string lastPassword = "";

            while (password.Length < 8)
            {
                string md5 = returnCreateMD5(input + ctr.ToString());
                if (md5.Substring(0, 5) == "00000")
                {
                    password += md5.Substring(5, 1);
                    //Console.WriteLine(password.PadRight(8));
                    if (lastPassword != password) 
                        Console.Write("\r" + password.PadRight(8,'#'));
                    lastPassword = password;
                }
                ctr++;
            }
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine();
            return password;
        }

        private object SearchForHashPt2(string input)
        {
            //return "694190cd";

            long ctr = 4500000;
            string password = "########";
            //input = "abc";
            string lastPassword = "";
            //Console.WriteLine();

            while (password.Contains("#"))
            {
                string md5 = returnCreateMD5(input + ctr.ToString());
                if (md5.Substring(0, 5) == "00000")
                {
                    string pos5 = md5.Substring(5, 1);
                    string pos6 = md5.Substring(6, 1);

                    if (pos5.IsNumber())
                        if(int.Parse(pos5)<8)
                            if (password.Substring(int.Parse(pos5), 1) == "#")
                                password = password.Remove(int.Parse(pos5),1).Insert(int.Parse(pos5), pos6);
                    //password += md5.Substring(5, 1);
                    if(lastPassword != password) Console.Write("\r" + password);
                    lastPassword = password;
                }
                ctr++;
            }
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine();
            return password;
        }

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
                return sb.ToString().ToLower();
            }
        }
    }


}
