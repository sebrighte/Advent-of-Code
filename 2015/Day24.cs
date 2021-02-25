using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 24: ...")]
    class Day24 : BaseLine, Solution
    {
        public object PartOne(string input) => Solve(input, 3);
        public object PartTwo(string input) => Solve(input, 4);

        long Solve(string input, int groups)
        {
            var presents = input.Split("\n").Select(a=>Convert.ToInt64(a)).ToArray();
            var totalWeight = presents.Sum();
            var weightPerSet = totalWeight / groups;
            bestSoFar = 1 + presents.Length / groups;
            var bestSet = Distribute(new List<long>(), presents.Reverse().ToList(), (int)weightPerSet)
                .Select(g => new { g.Count, QE = g.Aggregate((a, b) => a * b) })
                .OrderBy(g => g.Count)
                .ThenBy(g => g.QE)
                .First();
            return bestSet.QE;
        }

        int bestSoFar = Int32.MaxValue;
        long bestSoFarEQ = Int64.MaxValue;
        IEnumerable<List<long>> Distribute(List<long> used, List<long> pool, int amount)
        {
            //if not the lowest number of presents
            if (used.Count >= bestSoFar) yield break;

            //Whats the gap
            var remaining = amount - used.Sum();

            //loop through all the unused presents
            for (int n = 0; n < pool.Count; n++)
            {
                //get each present
                var s = pool[n];
                //if bigger than gap then exit
                if (pool[n] > remaining) continue;
                //get all the used presents
                var x = used.ToList();
                //add seleced present to used list
                x.Add(s);
                //if equal to sum then yield (if lowest)
                if (s == remaining)
                {
                    //set best EQ
                    if (x.Aggregate((a, b) => a * b) < bestSoFarEQ)
                    {
                        bestSoFarEQ = x.Aggregate((a, b) => a * b);
                        yield return x;
                        //$"Present Count: {x.Count} Presents: {string.Join(",", x)} EQ: {x.Aggregate((a, b) => a * b)}".Dump();
                    }
                    //set best present count
                    else if (x.Count < bestSoFar)
                    {
                        bestSoFar = x.Count;
                        yield return x;
                        //$"Present Count: {x.Count} Presents: {string.Join(",", x)} EQ: {x.Aggregate((a, b) => a * b)}".Dump();
                    }
                }
                else
                {
                    //skip that value and go to next
                    var y = pool.Skip(n + 1).ToList();
                    //recused with new present and exiating used list
                    foreach (var d in Distribute(x, y, amount))
                    {
                        yield return d;
                    }
                }
            }
        }
    }
}
