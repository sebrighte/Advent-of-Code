using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day23: Safe Cracking")]
    class Day23 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input, 7).First();
        public object PartTwo(string input) => Solver(input, 12).First();

        private IEnumerable<object> Solver(string inData, int a)
        {
            //inData ="cpy 2 a\r\ntgl a\r\ntgl a\r\ntgl a\r\ncpy 1 a\r\ndec a\r\ndec a";

            List<string> input = inData.Split("\r\n").ToList();
            var Registry = new Dictionary<string, int> { { "a", a }, { "b", 0 }, { "c", 0 }, { "d", 0 } };

            for (int i = 0; i < input.Count(); i++)
            {
                var fields = input[i].Split(" ");
                switch (fields[0])
                {
                    case "tgl":
                        int tglCnt = Registry[fields[1]];

                        if(i + tglCnt > input.Count()-1) continue;

                        var fieldsTgl = input[i + tglCnt].Split(" ");

                        switch (fieldsTgl[0])
                        {
                            case "inc":
                                input[i + tglCnt] = $"dec {fieldsTgl[1]}";
                                break;
                            case "dec":
                                input[i + tglCnt] = $"inc {fieldsTgl[1]}";
                                break;
                            case "jnz":
                                input[i + tglCnt] = $"cpy {fieldsTgl[1]} {fieldsTgl[2]}";
                                break;
                            case "cpy":
                                input[i + tglCnt] = $"jnz {fieldsTgl[1]} {fieldsTgl[2]}";
                                break;
                            case "tgl":
                                input[i + tglCnt] = $"inc a";
                                break;
                        }
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
                        if (i == 9 && Registry["d"] != 0)
                        {
                            //Multiply....Like Rabbits... a = a + (d * b)
                            Registry["a"] += Registry["d"] * Registry["b"];
                            Registry["d"] = 0;
                        }
                        int val1 = fields[1].IsNumber() ? fields[1].ToInt32() : Registry[fields[1]];
                        int val2 = fields[2].IsNumber() ? fields[2].ToInt32() : Registry[fields[2]];
                        i += val1 != 0 ? val2 - 1 : 0;
                        break;
                }
            }
            yield return $"{Registry["a"]}";
        }
    }
}
