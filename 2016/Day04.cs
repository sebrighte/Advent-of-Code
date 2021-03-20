using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 4: Security Through Obscurity")]
    class Day04 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            int valid = 0;

            foreach (var item in input)
            {
                string code = item.Split("[")[0];
                int id = int.Parse(code.Substring(code.LastIndexOf("-")+1));
                string check = item.Split("[")[1].Split("]")[0];
                Dictionary<string, int> counter = new Dictionary<string, int>();
                    
                for (char c = 'a'; c <= 'z'; c++)
                {
                    int ctr = (code.CountStringInstances(c.ToString()));
                    if (ctr > 0) counter.Add(c.ToString(), ctr);
                }

                string r = string.Join("", counter.OrderByDescending(x => x.Value).ThenBy(x=>x.Key).Take(5).Select(x => x.Key).ToArray());
                valid += check == r? id : 0;
            }
            yield return valid;
        }

        private IEnumerable<object> Day2(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            int valid = 0;

            foreach (var item in input)
            {
                string code = item.Split("[")[0];
                //string code = "qzmt - zixmtkozy - ivhz - 343";
                int id = int.Parse(code.Substring(code.LastIndexOf("-") + 1));
                string check = item.Split("[")[1].Split("]")[0];
                Dictionary<string, int> counter = new Dictionary<string, int>();

                for (char c = 'a'; c <= 'z'; c++)
                {
                    int ctr = (code.CountStringInstances(c.ToString()));
                    if (ctr > 0) counter.Add(c.ToString(), ctr);
                }

                string checkCalc = string.Join("", counter.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(5).Select(x => x.Key).ToArray());
                if (check == checkCalc)
                {
                    //string decoded = Decode(code, id % 26);
                    if (Decode(code, id % 26).Contains("northpole-object-storage"))
                        valid = id;
                }
            }
            yield return valid;
        }

        string Decode(string str, int loop)
        {
            char[] retVal = str.ToArray();
            for (int i = 0; i < retVal.Length; i++)
            {;
                for (int j = 0; j < loop; j++)
                {
                    if (char.IsLetter(retVal[i]))
                    retVal[i] = retVal[i] == 'z' ? 'a' : (char)(retVal[i] + 1);
                }
            }
            return string.Join("", retVal);
        }
    }
}
