using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 13: Knights of the Dinner Table")]
    class Day13 : BaseLine, Solution
    {
        public object PartOne(string input) => Happiness(input).Max();
        public object PartTwo(string input) => Happiness(input, true).Max();

        IEnumerable<int> Happiness(string inData, bool day2 = false)
        {
            List<string> input = inData.Split("\n").ToList();
            var preferences = new Dictionary<(string, string), int>();

            string[] people = null;

            foreach (string line in input)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, @"(.*) would (.*) (.*) happiness units by sitting next to (.*)\.");
                var (a, b, c) = (match.Groups[1].Value, match.Groups[2].Value, match.Groups[4].Value);
                var d = int.Parse(match.Groups[3].Value);
                preferences.Add(key: (a, c), b == "lose"? -d : d);
            }

            people = preferences.Keys.Select(k => k.Item1).Distinct().ToArray();
            if (day2) 
                people = preferences.Keys.Select(k => k.Item1).Append("Me").Distinct().ToArray();

            var prefs = GetPermutations(people.ToList());

            foreach (string preference in prefs)
            {
                int happinessVal = 0;
                string[] prefSplit = preference.Split(",").Append(preference.Split(",")[0]).ToArray();

                for (int rev = 0; rev < 2; rev++)
                {
                    for (int i = 0; i < prefSplit.Length - 1; i++)
                    {
                        happinessVal += preferences.TryGetValue((prefSplit[i], prefSplit[i + 1]), out int v) ? v : 0;
                    }
                    Array.Reverse(prefSplit);
                }
                yield return happinessVal;
            } 
        }
    }
}
