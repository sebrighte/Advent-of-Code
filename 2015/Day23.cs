using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 23: Opening the Turing Lock")]
    class Day23 : BaseLine, Solution
    {
        public object PartOne(string input) => Solve(input, 0).Max();
        public object PartTwo(string input) => Solve(input, 1).Max();

        private IEnumerable<object> Solve(string inData, long a)
        {
            int b = 0;
            int ctr = 0;
            string[] instArr;

            //Test Data - Make return a
            //inData = "inc a\r\njio a, +2\r\ntpl a\r\ninc a";

            List<string> input = inData.Split("\r\n").ToList();

            while (ctr < input.Count)
            {
                instArr = input[ctr].Replace(",", "").Split(' ').ToArray();

                switch (instArr[0])
                {
                    case "hlf":
                        if (instArr[1] == "a") { a /= 2; }
                        ctr++;
                        break;
                    case "tpl":
                        if (instArr[1] == "a") { a *= 3; }
                        ctr++;
                        break;
                    case "inc":
                        if (instArr[1] == "a") { a++; }
                        else b++;
                        ctr++;
                        break;
                    case "jmp":
                        ctr += int.Parse(instArr[1]);
                        break;
                    case "jie":
                        ctr+= a%2 == 0? int.Parse(instArr[2]) : 1;
                        break;
                    case "jio":
                        ctr+= a==1? int.Parse(instArr[2]) : 1;
                        break;
                }
                yield return b;
            }
        }
    }
}

