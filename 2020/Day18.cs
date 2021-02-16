using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 18: Operation Order")]
    class Day18 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<long> Day1(string inData, bool part2 = false)
        {
            List<string> input = inData.Split("\r\n").ToList();
            long resultDay1 = 0;
            long resultDay2 = 0;
            foreach (string inputLineS in input)
            {
                SumDay1 sum_Day1 = new SumDay1(inputLineS);
                resultDay1 += sum_Day1.result;

                SumDay2 sum_Day2 = new SumDay2(inputLineS);
                resultDay2 += sum_Day2.result;
            }

            if(!part2)
                yield return resultDay1;
            else
                yield return resultDay2;
        }
    }

    class SumDay1
    {
        public string sumString;
        public long result;

        public SumDay1(string inputStr)
        {
            sumString = inputStr;
            //sumString = sumString.Replace("((", "( ");
            sumString = sumString.Replace("(", "( ");
            sumString = sumString.Replace(")", " )");
            result = CalculateBrackets();
        }

        public long CalculateBrackets()
        {
            if (sumString.ToCharArray().Count(val => val == '(') != 0)
            {
                int firstBracket = -1;
                int secondBracket = -1;
                for (int j = 0; j < sumString.Length; j++)
                {
                    if (sumString[j] == '(') { firstBracket = j; }
                    else if (firstBracket != -1 && sumString[j] == '(') firstBracket = j;
                    else if (firstBracket != -1 && sumString[j] == ')') { secondBracket = j; break; }
                }
                string strToPass = sumString.Substring(firstBracket + 2, secondBracket - 3 - firstBracket);
                string strToReplace = sumString.Substring(firstBracket, secondBracket + 1 - firstBracket);
                sumString = sumString.Replace(strToReplace, Dosum(strToPass).ToString());
                CalculateBrackets();
            }
            return Dosum(sumString);
        }

        public long Dosum(string input)
        {
            string[] sum = input.Split(' ');

            long val1 = -1;
            long val2 = 0;
            string action = "";

            for (int i = 0; i < sum.Length; i++)
            {
                if (long.TryParse(sum[i], out val2))
                {
                    if (val1 == -1)
                    {
                        val1 = val2;
                    }
                    else
                    {
                        if (action == "+") { val1 += long.Parse(sum[i].ToString()); }
                        if (action == "*") { val1 *= long.Parse(sum[i].ToString()); }
                    }
                }
                else action = sum[i];
            }
            return val1;
        }
    }

    class SumDay2
    {
        public string sumString;
        public string sumStringFinal;
        public long result;

        public SumDay2(string inputStr)
        {
            sumString = inputStr;
            //sumString = sumString.Replace("((", "( ");
            //sumString = sumString.Replace("))", ") ");
            sumString = sumString.Replace("(", "( ");
            sumString = sumString.Replace(")", " )");
            sumString = sumString.Replace("  ", " ");

            result = CalculateBrackets();
        }

        public long CalculateBrackets()
        {
            if (sumString.ToCharArray().Count(val => val == '(') != 0)
            {
                int firstBracket = -1;
                int secondBracket = -1;
                for (int j = 0; j < sumString.Length; j++)
                {
                    if (sumString[j] == '(') { firstBracket = j; }
                    else if (firstBracket != -1 && sumString[j] == '(') firstBracket = j;
                    else if (firstBracket != -1 && sumString[j] == ')') { secondBracket = j; break; }
                }
                string strToPass = sumString.Substring(firstBracket + 2, secondBracket - 3 - firstBracket);
                string strToReplace = sumString.Substring(firstBracket, secondBracket + 1 - firstBracket);
                sumString = sumString.Replace(strToReplace, Dosum(strToPass).ToString());
                CalculateBrackets();
            }
            return Dosum(sumString);
        }

        private string DoAdditions2(string input)
        {
            string output = "";
            long val1; long val2;

            if (input.Contains("+"))
            {
                string[] sum = input.Split(' ');

                for (int i = 0; i < sum.Length; i++)
                {
                    if (sum[i] == "+" && Int64.TryParse(sum[i - 1], out val1) && Int64.TryParse(sum[i + 1], out val2))
                    {
                        long value = long.Parse(sum[i - 1]) + long.Parse(sum[i + 1]);
                        string toReplace = string.Format("{0} + {1}", sum[i - 1], sum[i + 1]);
                        string replaceWith = string.Format("{0}", value);

                        input = input.ReplaceFirst(toReplace, replaceWith);

                        //input = input.Replace(toReplace, replaceWith);
                        break;// i = sum.Length;
                    }
                }
                output = input;
            }
            return output;
        }

        private string DoAdditions(string input)
        {
            string output = "";

            if (input.Contains("+"))
            {
                string[] sum = input.Split(' ');

                for (int i = 0; i < sum.Length; i++)
                {
                    if (sum[i] == "+")
                    {
                        long value = long.Parse(sum[i - 1]) + long.Parse(sum[i + 1]);
                        string toReplace = string.Format("{0} + {1}", sum[i - 1], sum[i + 1]);
                        string replaceWith = string.Format("{0}", value);

                        //public static string ReplaceFirst(this string text, string search, string replace)

                        input = input.ReplaceFirst(toReplace, replaceWith);

                        //input = input.Replace(toReplace, replaceWith);

                        break;// i = sum.Length;
                    }
                }
                output = input;
            }
            return output;
        }

        public long Dosum(string input)
        {
            while (input.Contains("+"))
                input = DoAdditions(input);

            sumStringFinal = input;

            string[] sum = input.Split(' ');

            long val1 = -1;
            long val2 = 0;
            string action = "";

            for (int i = 0; i < sum.Length; i++)
            {
                if (long.TryParse(sum[i], out val2))
                {
                    if (val1 == -1)
                    {
                        val1 = val2;
                    }
                    else
                    {
                        //if (action == "+") { val1 += int.Parse(sum[i].ToString()); }
                        if (action == "*") { val1 *= long.Parse(sum[i].ToString()); }
                    }
                }
                else action = sum[i];
            }
            return val1;
        }
    }

}
