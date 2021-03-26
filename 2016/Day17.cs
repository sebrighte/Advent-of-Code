using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day17: Two Steps Forward")]
    class Day17 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver();
        public object PartTwo(string input) => Solver(true);

        int minLen = int.MaxValue;
        int maxLen = 0;
        Status endStatus;

        private object Solver(bool part2 = false)
        {
            string seedIn = "yjjvjgan"; //RLDRUDRDDR / 498 steps
            //seedIn = "ihgpwlah"; //DDRRRD / 370 steps
            //seedIn = "kglvqrro"; //DDUDRLRRUDRD / 492 steps
            //seedIn = "ulqzkmiv"; //DRURDRUDDLLDLUURRDULRLDUUDDDRR / 830 steps
            
            if (!part2)
                return iterateRoute(new Status(seedIn)).history;
            else
                return iterateRoute(new Status(seedIn), part2).history.Length;
        }

        private Status iterateRoute(Status st, bool part2 = false)
        {
            if (st.AtVault())
            {
                if (st.history.Length <= minLen && !part2)
                {
                    minLen = st.history.Length;
                    endStatus = st;
                }

                if (st.history.Length >= maxLen && part2)
                {
                    maxLen = st.history.Length;
                    endStatus = st;
                }
            }
            else
            {
                foreach (var move in GetVaidMoves(st))
                {
                    Status stDup = st.Clone();
                    stDup.SetLocation(move);
                    stDup.AppendHistory(move);
                    iterateRoute(stDup, part2);
                }
            }
            return endStatus;
        }

        private List<string> GetVaidMoves(Status st)
        {
            var moves = new List<string>();

            foreach (var (item, index) in CreateMD5($"{st.seededHistory}").First().Substring(0, 4).WithIndex())
            {
                if("bcdef".ToCharArray().Contains(item))
                {
                    switch (index)
                    {
                        case 0: if (st.X > 0) moves.Add("U"); break;
                        case 1: if (st.X < 3) moves.Add("D"); break;
                        case 2: if (st.Y > 0) moves.Add("L"); break;
                        case 3: if (st.Y < 3) moves.Add("R"); break;
                    }
                }   
            }
            return moves.ToList();
        }
    }

    class Status 
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string history { get; set; }
        public string seed { get; set; }
        public string seededHistory => $"{seed}{history}";

        public void AppendHistory(string move)
        {
            history += move;
        }

        public void SetLocation(string move)
        {
            if (move == "U") X -= 1;
            if (move == "D") X += 1;
            if (move == "L") Y -= 1;
            if (move == "R") Y += 1;
        }

        public Status(string seedIn, int XIn=0, int YIn=0, string historyIn="")
        {
            seed = seedIn;
            X = XIn;
            Y = YIn;
            history = historyIn;
        }

        public Status Clone(){ return new Status(this.seed, this.X, this.Y, this.history); }

        public bool AtVault() { return X == 3 && Y == 3 ? true : false; }
    }
}
