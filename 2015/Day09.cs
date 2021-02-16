using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 9: All in a Single Night Routed")]
    class Day09 : BaseLine, Solution
    {
        public object PartOne(string input) => CalcRoutes(input).Min();
        public object PartTwo(string input) => CalcRoutes(input).Max();

        private List<int> CalcRoutes(string inData)
        {
            List<string> input = inData.Split("\n").ToList();
            List<int> Distances = new List<int>();
            HashSet<string> capitals = new HashSet<string>();
            Dictionary<string, int> capitalsDist = new Dictionary<string, int>();

            foreach (var line in input)
            {
                var m = System.Text.RegularExpressions.Regex.Match(line, @"(.*) to (.*) = (.*)");
                var (a, b) = (m.Groups[1].Value, m.Groups[2].Value);
                var d = int.Parse(m.Groups[3].Value);
                //var arr = new[] { (k: (a, b), d), (k: (b, a), d) };

                if (!capitals.Contains(a)) capitals.Add(a);
                if (!capitals.Contains(b)) capitals.Add(b);
                if (!capitalsDist.ContainsKey($"{a}-{b}")) capitalsDist.Add($"{a}-{b}", d);
                if (!capitalsDist.ContainsKey($"{b}-{a}")) capitalsDist.Add($"{b}-{a}", d);
            }

            foreach (string routePerm in GetPermutations(capitals.ToList()))
            {
                string[] city = routePerm.Split(',');
                int dist = 0;
                for (int i = 0; i < city.Count() - 1; i++)
                {
                    string tmp = city[i] + "-" + city[i + 1];
                    dist += capitalsDist[tmp];
                }
                Distances.Add(dist);
            }
            return Distances;
        }
    }
}