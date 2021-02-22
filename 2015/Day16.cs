using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 16: Aunt Sue")]
    class Day16 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string input, bool part2 = false)
        {
            List<string[]> sues = new List<string[]>();
            foreach (string line in input.Split("\n"))
            {               
                var match = Regex.Match(line, @"Sue \d*: (.*), (.*), (.*)");
                sues.Add(new string[] {
                    match.Groups[1].Value,
                    match.Groups[2].Value,
                    match.Groups[3].Value});   
            }
            List<string> results = new List<string>();
            results.Add("children: 3");
            results.Add("cats: 7");
            results.Add("samoyeds: 2");
            results.Add("pomeranians: 3");
            results.Add("akitas: 0") ;
            results.Add("vizslas: 0");
            results.Add("goldfish: 5");
            results.Add("trees: 3");
            results.Add("cars: 2");
            results.Add("perfumes: 1");

            foreach (var (sue, index) in sues.WithIndex())
            {
                int found = 0;
                foreach (var result in results)
                {
                    if (!part2 && sue.Contains(result)) 
                        found++;
                  
                    foreach (var sueLine in sue)
                    {
                        if (part2)
                        {
                            //(a,b) = result.Split(": ").Select((a,b)=>)
                            string[] resSplit = result.Split(": ");
                            string resStr = resSplit[0];
                            int resInt = int.Parse(resSplit[1]);
                            string[] sueSplit = sueLine.Split(": ");
                            string sueStr = sueSplit[0];
                            int sueInt = int.Parse(sueSplit[1]);

                            if (resStr == "cats" && sueStr == "cats") { if (resInt < sueInt) found++; }
                            if (resStr == "trees" && sueStr == "trees") { if (resInt < sueInt) found++; }
                            if (resStr == "pomeranians" && sueStr == "pomeranians") { if (resInt > sueInt) found++; }
                            if (resStr == "goldfish" && sueStr == "goldfish") { if (resInt > sueInt) found++; }
                            if (resStr == "children" && sueStr == "children") { if (resInt == sueInt) found++; }
                            if (resStr == "samoyeds" && sueStr == "samoyeds") { if (resInt == sueInt) found++; }
                            if (resStr == "akitas" && sueStr == "akitas") { if (resInt == sueInt) found++; }
                            if (resStr == "vizslas" && sueStr == "vizslas") { if (resInt == sueInt) found++; }
                            if (resStr == "cars" && sueStr == "cars") { if (resInt == sueInt) found++; }
                            if (resStr == "perfumes" && sueStr == "perfumes") { if (resInt == sueInt) found++; }
                        }
                    }
                }
                if (found == 3)
                {
                    yield return index + 1;
                }
            }
        }
    }
}