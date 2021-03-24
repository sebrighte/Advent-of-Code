using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 12: Leonardo's Monorail")]
    class Day12 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input,0,0,0,0).First();
        public object PartTwo(string input) => Day1(input,0,0,1,0).First();

        private IEnumerable<object> Day1(string inData, int a, int b, int c, int d)
        {
            //inData = "cpy 41 a\r\ninc a\r\ninc a\r\ndec a\r\njnz a 2\r\ndec a";

            List<string> input = inData.Split("\r\n").ToList();

            var Registry = new Dictionary<string, int> { {"a", a}, {"b", b}, {"c", c},{"d", d} };         
                
            for (int i = 0; i < input.Count(); i++)
            {
                var fields = input[i].Split(" ");
                switch (fields[0])
                {
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
            }
            yield return $"{Registry["a"]}";
        }
    }
}
