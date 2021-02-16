using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 15: Science for Hungry People")]


    class Day15 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).Max();
        public object PartTwo(string input) => Day1(input, 500).Max();

        Cookie CalcCookie = new Cookie();
        HashSet<string> reciepeHash = new HashSet<string>();

        private IEnumerable<long> Day1(string input, int calories = -1)
        {
            foreach (string line in input.Split("\n"))
            {
                var match = Regex.Match(line, @"(.*): capacity (.*), durability (.*), flavor (.*), texture (.*), calories (.*)");
                var a = match.Groups[1].Value;
                var (b, c, d, e, f) = (int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value));

                CalcCookie.AddIngredients(a, new int[] { b, c, d, e});
                CalcCookie.AddCaloties(new int[] { f });
            }

            //Calc possible permutations (bit messy but works....)
            int tota = 0;
            int totb = 0;
            int totc = 0;
            int totd = 0;
            int loop = 100;

            for (int a = 0; a <= loop; a++)
            {
                tota = a;
                for (int b = 0; b <= loop; b++)
                {
                    totb = tota + b < loop ? b : loop - tota;
                    for (int c = 0; c <= loop; c++)
                    {
                        totc = tota + totb + c < loop ? c : loop - tota - totb;
                        totd = tota + totb + totc < loop ? loop - (tota + totb + totc) : 0;
                        int total = tota + totb + totc + totd;

                        reciepeHash.Add($"{tota},{totb},{totc},{totd}");  
                    }
                }
            }

            foreach (var hash in reciepeHash)
            {
                //Get the ratios from HashSet
                int[] rec = hash.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                yield return CalcCookie.SetRatio(rec, calories);
            }
        }
    }

    class Cookie
    {
        List<int[]> ingredients = new List<int[]>();
        List<int[]> calories = new List<int[]>();

        public void AddIngredients(string name, int[] i) { ingredients.Add(i);}

        public void AddCaloties(int[] c) { calories.Add(c); }

        public long SetRatio(int[] ratios, int cals)
        {
            int caloriesTot = 0;
            int[] results = new int[ingredients[0].Count()];
            
            //loop for ratios
            foreach (var (ratio,rindex) in ratios.WithIndex())
            {
                //loop for ings
                for (int i = 0; i < ingredients[rindex].Count(); i++)
                {
                    //Get ing metrics for each ratio
                    results[i] += ingredients[rindex][i] * ratio;
                }
                //Calc calories
                caloriesTot += calories[rindex][0] * ratio;
            }

            //Look for negative vals
            for (int i = 0; i < results.Length; i++) if (results[i] < 0) results[i] = 0;

            //Get multiple
            long result = results.Aggregate(1, (a, b) => a * b);

            //Part 2 chack if 500 calories
            if (caloriesTot != cals && cals != -1) result = 0;

            return result;
        }
    }
}
