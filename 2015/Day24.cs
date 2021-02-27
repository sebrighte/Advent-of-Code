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
            bestSoFarCnt = 1 + presents.Length / groups;

            return Distribute(new List<long>(), presents.Reverse().ToList(), (int)weightPerSet).Min();
        }

        int bestSoFarCnt = Int32.MaxValue;
        long bestSoFarEQ = Int64.MaxValue;
        IEnumerable<long> Distribute(List<long> used, List<long> pool, int amount)
        {
            if (used.Count >= bestSoFarCnt) yield break;
            var remaining = amount - used.Sum();

            for (int n = 0; n < pool.Count; n++)
            {
                var s = pool[n];
                if (pool[n] > remaining) continue;
                var x = used.ToList();
                x.Add(s);
                if (s == remaining)
                {
                    if (x.Aggregate((a, b) => a * b) < bestSoFarEQ)
                    {
                        bestSoFarEQ = x.Aggregate((a, b) => a * b);
                        yield return bestSoFarEQ;
                        //$"Present Count: {x.Count} Presents: {string.Join(",", x)} EQ: {x.Aggregate((a, b) => a * b)}".Dump();
                    }
                    else if (x.Count < bestSoFarCnt)
                    {
                        bestSoFarCnt = x.Count;
                        yield return x.Aggregate((a, b) => a * b);
                        //$"Present Count: {x.Count} Presents: {string.Join(",", x)} EQ: {x.Aggregate((a, b) => a * b)}".Dump();
                    }
                }
                else
                {
                    var y = pool.Skip(n + 1).ToList();
                    foreach (var d in Distribute(x, y, amount))
                    {
                        yield return d;
                    }
                }
            }
        }
    }
}
