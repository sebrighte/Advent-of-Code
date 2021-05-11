using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{ 
    [ProblemName("Day7: Recursive Circus")]
    class Day07 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input,false).First();
        public object PartTwo(string input) => Solver(input,true).First();

        private IEnumerable<object> Solver(string inData, bool part2)
        {
            yield return !part2? GetRootNode(inData).GetProgName() : FindTowerImbalance(GetRootNode(inData));
        }

        private int FindTowerImbalance(Day17Program prog)
        {
            var tot = prog.GetChildren().Select(a => a.balanceWeight).Max() % prog.GetChildren().Select(a => a.balanceWeight).Min() ==0;

            if (tot == true)
            {
                var heaviest1 = prog.GetParent().GetChildren().Select(a => a).OrderByDescending(a => a.balanceWeight).First();
                var lightest1 = prog.GetParent().GetChildren().Select(a => a).OrderBy(a => a.balanceWeight).First();
                return heaviest1.progWeight - (heaviest1.balanceWeight - lightest1.balanceWeight);
            }
            return FindTowerImbalance(prog.GetChildren().Select(a => a).OrderByDescending(a => a.balanceWeight).FirstOrDefault());
        }

        private int GetTowerWeight(Day17Program root, ref int weight)
        {
            weight += root.progWeight;
            foreach (var child in root.GetChildren())
            {
                GetTowerWeight(child, ref weight);
            }
            return weight;
        }

        private Day17Program GetRootNode(string inData)
        {
            List<string> input = inData.Split("\r\n").ToList();
            
            List<Day17Program> tower = (
                from prog in input
                let tmp = new Day17Program(prog)
                select tmp)
                .ToList();

            foreach (var prog in input)
            {
                if (prog.Contains("->"))
                {
                    var childrenStr = prog.Split("->")[1].Replace(" ", "").Split(",");
                    var children = from c in childrenStr select tower.Where(a => a.GetProgName() == c);
                    var thisProg = tower.Where(a => a.GetProgName() == prog.Split(" ")[0]).Select(a => a).First();
                    foreach (var child in from childList in children
                                          from item in childList
                                          select item)
                    {
                        thisProg.AssociateChild(child);
                        child.AssociateParent(thisProg);
                    }
                }
            }

            foreach (var prog in tower)
            {
                int q = 0;
                GetTowerWeight(prog, ref q);
                prog.balanceWeight = q;
                q = 0;
            }

            return tower.Where(a => a.HasNoParent()).First();
        }
    }

    class Day17Program
    {
        List<Day17Program> progChildren = new List<Day17Program>();
        Day17Program progParent;
        string progName;
        public int progWeight = 0;
        public int balanceWeight = 0;

        public void AssociateChild(Day17Program child)
        {
            this.progChildren.Add(child);
        }

        public bool HasNoParent()
        {
            return progParent == null;
        }

        public Day17Program GetParent()
        {
            return progParent;
        }

        public string GetProgName()
        { 
            return progName; 
        }

        public List<Day17Program> GetChildren()
        {
            return progChildren;
        }

        public void AssociateParent(Day17Program parent)
        {
            this.progParent = parent;
        }
        
        public Day17Program(string input)
        {
            progName = input.Split(" ")[0];
            progWeight = input.Split(" ")[1].Replace("(", "").Replace(")","").ToInt32();
        }
    }
} 
