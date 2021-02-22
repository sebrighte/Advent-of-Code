using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 19: Medicine for Rudolph")]
    class Day19 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).Max();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<int> Day1(string inData)
        {
            HashSet<string> elements = new HashSet<string>();

            //Test
            //inData = "H => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH";
           
            var input = inData.Split("\n").ToList();
            string startingpoint = input[input.Count -1];
            input.RemoveRange(input.Count - 2, 2);

            foreach (var line in input)
            {
                var match = Regex.Match(line, @"(.*) => (.*)");
                string valA = match.Groups[1].Value;
                string valB= match.Groups[2].Value;

                int find = 0;
                int ctr = 0;
                while (ctr != -1)
                {
                    string tmp = startingpoint;
                    find = startingpoint.IndexOf(valA, ctr);
                    if (find != -1)
                    {
                        ctr = find+1;
                        string tmp2 = startingpoint;
                        tmp2.ReplaceOccurrence(find, valA, valB);
                        elements.Add(tmp2.ReplaceOccurrence(find, valA, valB));
                    }
                    else
                        ctr = -1;
                }
                yield return elements.Count();
            }
           
        }

        private IEnumerable<int> Day2(string inData)
        {
            List<KeyValuePair<string, string>> replacements = new List<KeyValuePair<string, string>>();

            var input = inData.Split("\n").ToList();
            string medicineMolecule = input[input.Count - 1];
            input.RemoveRange(input.Count - 2, 2);

            foreach (var line in input)
            {
                var match = Regex.Match(line, @"(.*) => (.*)");
                replacements.Add(new KeyValuePair<string, string>(match.Groups[1].Value, match.Groups[2].Value));
            }
            yield return Search(medicineMolecule, replacements).Min();
        }

        public IEnumerable<int> Search(string molecule, List<KeyValuePair<string, string>> replacements)
        {
            var target = molecule;
            var mutations = 0;

            while (target != "e")
            {
                var tmp = target;
                foreach (var rep in replacements)
                {
                    var index = target.IndexOf(rep.Value);
                    if (index >= 0)
                    {
                        target = target.ReplaceOccurrence(index, rep.Value, rep.Key);
                        mutations++;
                    }
                }

                if (tmp == target)
                {
                    target = molecule;
                    mutations = 0;
                    replacements = Shuffle(replacements).ToList();
                }
            }
            yield return mutations;
        }
    }
}