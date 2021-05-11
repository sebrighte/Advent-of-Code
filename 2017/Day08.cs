using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2017
{
    //[ProblemName("Day8: I Heard You Like Registers@TEST@")]
    [ProblemName("Day8: I Heard You Like Registers")]
    class Day08 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input,false).First();
        public object PartTwo(string input) => Solver(input, true).First();

        private static IEnumerable<object> Solver(string inData, bool part2)
        {
            Dictionary<string, int> registry = new();
            int maxReg = 0;

            foreach (var inst in inData.Split("\r\n").ToList())
            {
                bool actionInst = false;
                var match = Regex.Match(inst, @"^(\w*) (inc|dec) (\d+|-\d+) if (\w*) (\D*) (\d+|-\d+)$");
                string register = match.Groups[1].Value;
                string action = match.Groups[2].Value;
                int value = match.Groups[3].Value.ToInt32();
                string compareReg = match.Groups[4].Value;
                string symbol = match.Groups[5].Value;
                int compareVal = match.Groups[6].Value.ToInt32();
 
                if (!registry.ContainsKey(register)) registry.Add(register, 0);
                if (!registry.ContainsKey(compareReg)) registry.Add(compareReg, 0);

                switch(symbol)
                {
                    case ">": if (registry[compareReg] >  compareVal) actionInst = true; break;
                    case "<": if (registry[compareReg] <  compareVal) actionInst = true; break;
                    case "==":if (registry[compareReg] == compareVal) actionInst = true; break;
                    case ">=":if (registry[compareReg] >= compareVal) actionInst = true; break;
                    case "<=":if (registry[compareReg] <= compareVal) actionInst = true; break;
                    case "!=":if (registry[compareReg] != compareVal) actionInst = true; break;
                }
                if(actionInst)
                {
                    if (action == "inc")
                        registry[register] += value;
                    else
                        registry[register] -= value;
                }
                if (registry.Select(a => a.Value).Max() > maxReg) maxReg = registry.Select(a => a.Value).Max();
            }
            yield return part2 ? maxReg : registry.Select(a => a.Value).Max();
        }
    }
}
