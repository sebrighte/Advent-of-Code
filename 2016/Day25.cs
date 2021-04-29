using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day25: Clock Signal")]
    class Day25 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => "Complete";



        private IEnumerable<object> Day1(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            int valA = 0;
            while(true)
            {
                if (Process(valA, input).First()) yield return valA;
                valA++;
            }
        }

        private IEnumerable<bool> Process(int a, List<string> input)
        {
            var Registry = new Dictionary<string, int> { { "a", a }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
            string result = "";

            for (int i = 0; i < input.Count(); i++)
            {
                var fields = input[i].Split(" ");
                switch (fields[0])
                {
                    case "out":
                        result += Registry[fields[1]];
                        break;
                    case "cpy":
                        Registry[fields[2]] = fields[1].IsNumber() ? fields[1].ToInt32() : Registry[fields[1]];
                        break;
                    case "inc":
                        Registry[fields[1]] += 1;
                        break;
                    case "dec":
                        Registry[fields[1]] -= 1;
                        break;
                    case "jnz":
                        int val = fields[1].IsNumber() ? fields[1].ToInt32() : Registry[fields[1]];
                        i += val != 0 ? fields[2].ToInt32() - 1 : 0;
                        break;
                }
                if (result.Length > 10) break;
            }
            yield return Regex.IsMatch(result, @"^(01)*0?$");
        }
    }
}
