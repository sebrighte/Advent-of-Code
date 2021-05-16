using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2017
{
    [ProblemName("Day12: Digital Plumber")]
    //[ProblemName("Day12: Digital Plumber@TEST@")]
    class Day12 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input,true).First();
        public object PartTwo(string input) => Solver(input,false).First();

        private IEnumerable<object> Solver(string inData, bool part1)
        {
            HashSet<Day12Prog> progFound = new();
            List<Day12Prog> progList = new();
            Day12Prog prog0 = null;

            foreach (var progInfo in inData.Split("\r\n").ToList())
                progList.Add(new Day12Prog(progInfo.Split(" ")[0].ToInt32()));

            prog0 = progList.Select(a => a).Where(a => a.ID == 0).First();

            foreach (var childProgInfo in inData.Split("\r\n").ToList())
            {
                var childProgID = childProgInfo.Replace(" ", "").Split("<->")[0].ToInt32();
                var childProg = progList.Select(a => a).Where(a => a.ID == childProgID).First();
                foreach (var child in childProgInfo.Replace(" ", "").Split("<->")[1].Split(","))
                    childProg.children.Add(progList.Select(a => a).Where(a => a.ID == child.ToInt32()).First());
            }

            int pt1 = CountChildren(prog0, progFound);
            var notInList = progList.Except(progFound);
            int pt2 = 1; //0 network is the first

            while(notInList.Count() != 0)
            {
                pt2++;
                CountChildren(notInList.First(), progFound);
                notInList = progList.Except(progFound);
            }
            yield return part1? pt1 : pt2;
        }

        private int CountChildren(Day12Prog start, HashSet<Day12Prog> progFound)
        {
            foreach (var child in from child in start.children
                                  where !progFound.Contains(child)
                                  select child)
            {
                progFound.Add(child);
                CountChildren(child, progFound);
            }
            return progFound.Count;
        }
    }

    class Day12Prog
    {
        public int ID;
        public List<Day12Prog> children;

        public Day12Prog(int id)
        {
            children = new();
            ID = id;
        }
    }

}
