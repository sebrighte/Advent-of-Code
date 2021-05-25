using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017
{
    [ProblemName("Day20: Particle Swarm")]
    class Day20 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input).First();
        public object PartTwo(string input) => Solver2(input).First();

        private static IEnumerable<object> Solver(string inData)
        {
            //inData = "p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>\r\n" +
            //           "p=<4,0,0>, v=<0,0,0>, a=<-2,0,0>";// + "\r\n" +
            //            "p=<4,0,0>, v=<1,0,0>, a=<-1,0,0>" + "\r\n" +
            //            "p=<2,0,0>, v=<-2,0,0>, a=<-2,0,0>" + "\r\n" +
            //            "p=<4,0,0>, v=<0,0,0>, a=<-1,0,0>" + "\r\n" +
            //            "p=<-2,0,0>, v=<-4,0,0>, a=<-2,0,0>" + "\r\n" +
            //            "p=<3,0,0>, v=<-1,0,0>, a=<-1,0,0>" + "\r\n" +
            //            "p=<-8,0,0>, v=<-6,0,0>, a=<-2,0,0>";

            List<Partical> Particals = new();
            int ctr = 0;
            foreach (var partical in inData.Split("\r\n").ToList())
            {
                Particals.Add(new Partical(ctr++, partical));
            }
            ctr = 0;
            while (ctr < 1000)
            {
                ctr++;
                foreach (var item in Particals)
                {
                    item.Transform();
                }
            }
            yield return Particals.Select(a => a).OrderBy(a => a.Distance).First().ID;
        }

        private static IEnumerable<object> Solver2(string inData)
        {
            List<Partical> Particals = new();
            int ctr = 0;

            foreach (var partical in inData.Split("\r\n").ToList())
            {
                Particals.Add(new Partical(ctr++, partical));
            }
            ctr = 0;
            while (ctr < 1000)
            {
                Dictionary<string, int> test = new();
                ctr++;
                foreach (var item in Particals)
                {
                    item.Transform();
                    string pos = item.posStr;
                    if (test.ContainsKey(pos))
                        test[pos]++;
                    else
                        test.Add(pos, 1);
                }

                var delctr = (from a in test.Where(a => a.Value > 1)
                              from b in Particals
                              where b.posStr == a.Key
                              select b.ID).ToArray();

                Particals.RemoveAll(a=> delctr.Contains(a.ID));
            }
            yield return Particals.Count();
        }
    }
    class Partical
    {
        long posX, posY, posZ;
        long volX, volY, volZ;
        long accX, accY, accZ;
        public string posStr => $"{posX} {posY} {posZ}";
        public long Distance => Math.Abs(posX) + Math.Abs(posY) + Math.Abs(posZ);
        public int ID;
        public Partical(int idIn, string input)
        {
            ID = idIn;
            var m = Regex.Match(input, @"^p=<(\d+|-\d+),(\d+|-\d+),(\d+|-\d+)>, v=<(\d+|-\d+),(\d+|-\d+),(\d+|-\d+)>, a=<(\d+|-\d+),(\d+|-\d+),(\d+|-\d+)>$");

            (posX, posY, posZ) = (m.Groups[1].Value.ToInt64(), m.Groups[2].Value.ToInt64(), m.Groups[3].Value.ToInt64());
            (volX, volY, volZ) = (m.Groups[4].Value.ToInt64(), m.Groups[5].Value.ToInt64(), m.Groups[6].Value.ToInt64());
            (accX, accY, accZ) = (m.Groups[7].Value.ToInt64(), m.Groups[8].Value.ToInt64(), m.Groups[9].Value.ToInt64());
        }

        public void Transform()
        {
            posX += volX += accX;
            posY += volY += accY;
            posZ += volZ += accZ;
        }
            
    }
}
