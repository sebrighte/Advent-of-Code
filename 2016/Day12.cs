using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 12: Leonardo's Monorail")]
    class Day12 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            //similar to Day8 2020


            inData = "cpy 41 a\r\n" +
            "inc a\r\n" +
            "inc a\r\n" +
            "dec a\r\n" +
            "jnz a 2\r\n" +
            "dec a";


            List<string> input = inData.Split("\r\n").ToList();
            Dictionary<string, int> Registry = new Dictionary<string, int>();

            for (int i = 0; i < input.Count(); i++)
            {
                string inst = input[i];
                
                string text = inst.Substring(0, 3);
                switch (text)
                {
                    case "cpy":
                        var match = Regex.Match(inst, @"cpy (\S*) (\S*)");

                        if (match.Groups[1].Value.IsNumber())
                        {
                            if (!Registry.ContainsKey(match.Groups[2].Value))
                                Registry.Add(match.Groups[2].Value, match.Groups[1].Value.ToInt32());
                            else
                                Registry[match.Groups[2].Value] = match.Groups[1].Value.ToInt32();
                        }
                        else
                        {
                            Registry[match.Groups[2].Value] = Registry[match.Groups[1].Value];
                        }
                        break;
                    case "inc":
                        match = Regex.Match(inst, @"inc (\D*)");
                        Registry[match.Groups[1].Value] += 1;
                        break;
                    case "dec":
                        match = Regex.Match(inst, @"dec (\D*)");
                        Registry[match.Groups[1].Value] -= 1;
                        break;
                    case "jnz":
                        match = Regex.Match(inst, @"jnz (\S*) (\S*)");

                        if(match.Groups[1].Value.IsNumber())
                        {
                            if (match.Groups[1].Value.ToInt32() != 0) i += match.Groups[2].Value.ToInt32();
                        }
                        else 
                        {
                            if (!Registry.ContainsKey(match.Groups[1].Value))
                                Registry.Add(match.Groups[1].Value, 0);

                            if (Registry[match.Groups[1].Value] != 0) i += match.Groups[2].Value.ToInt32();
                        }
                        
                        break;
                }
            }

            yield return $"{Registry["a"]}";
        }
    }
}
