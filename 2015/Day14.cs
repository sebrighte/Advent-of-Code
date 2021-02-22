using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    //2696
    //1084
    [ProblemName("Day 14: Reindeer Olympics")]
    class Day14 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input, false).Take(2503).Max();
        public object PartTwo(string input) => Day1(input, true).Take(2503).Max();

        IEnumerable<int> Day1(string input, bool part2)
        {
            List<Reimdeer> reindeers = new List<Reimdeer>();

            //reindeers.Add(new Reimdeer("Comet", 14, 10, 127));
            //reindeers.Add(new Reimdeer("Dancer", 16, 11, 162));

            foreach (string line in input.Split("\n"))
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, @"(.*) can fly (.*) km/s for (.*) seconds, but then must rest for (.*) seconds\.");
                var name = match.Groups[1].Value;
                var (dist, flight, rest) = (int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

                reindeers.Add(new Reimdeer(name, dist, flight, rest));
            }

            int reindeerWnnerIndex = 0;
            int[] reindeerRaceCtr = new int[reindeers.Count];

            while (true)
            {
                int max = 0;

                foreach (var (rd, index) in reindeers.WithIndex())
                {
                    int tmp = rd.Tick();
                    if (tmp > max)
                    {
                        max = tmp;
                        reindeerWnnerIndex = index;
                    }
                }
                reindeerRaceCtr[reindeerWnnerIndex]++;
                if(!part2)
                    //2696
                    yield return max;
                else
                    //1084
                    yield return reindeerRaceCtr.Max();
            }
        }
    }

    class Reimdeer
    {
        private string name;
        private int rest;
        private int dist;
        private int flight;
        private int seconds;
        private int totDist;
        private int secondTotals;

        public Reimdeer(string n, int d, int f, int r)
        {
            name = n;
            rest = r;
            dist = d;
            flight = f;
            seconds = 0;
            totDist = 0;
        }

        public int Tick()
        {
            int distloc = 0;

            seconds++;
            secondTotals++;

            if (seconds <= flight) distloc = dist * seconds;
            if (seconds == flight + 1) totDist += flight * dist;
            if (seconds >= rest + flight)
                seconds = 0;
           
            return totDist + distloc;
        }
    }
}
