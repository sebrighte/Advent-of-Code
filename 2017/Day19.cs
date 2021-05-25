using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    [ProblemName("Day19: A Series of Tubes")]
    //[ProblemName("Day19: ...@TEST@")]
    class Day19 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input).First();
        public object PartTwo(string input) => Solver(input).Skip(1).First();

        public enum Direction { up, down, left, right, end}

        private static IEnumerable<object> Solver(string inData)
        {
            int posX = 0;
            int posY = 0;
            int ctr = 1;
            char nextMove = '|';
            string route = "";
            Direction dir = Direction.down;

            var input = inData.Split("\r\n");
            posY = input[0].IndexOf('|');

            while(dir != Direction.end)
            {
                ctr++;
                switch (dir)
                {
                    case Direction.up:    nextMove = input[--posX][posY]; break;
                    case Direction.down:  nextMove = input[++posX][posY]; break;
                    case Direction.left:  nextMove = input[posX][--posY]; break;
                    case Direction.right: nextMove = input[posX][++posY]; break; 
                }

                if (nextMove == '+')
                {
                    switch (dir)
                    {
                        case Direction.up:
                        case Direction.down:
                            {
                                if (posY < input[0].Count() - 1) if (input[posX][posY + 1] != ' ') { dir = Direction.right; }
                                if (posY >= 0) if (input[posX][posY - 1] != ' ') { dir = Direction.left; }
                                break;
                            }
                        case Direction.left:
                        case Direction.right:
                            {
                                if (posX < input.Count() - 1) if (input[posX + 1][posY] != ' ') { dir = Direction.down; }
                                if (posX >= 0) if (input[posX - 1][posY] != ' ') { dir = Direction.up; }
                                break;
                            }
                    }
                }
                if (nextMove != '|' && nextMove != '+' && nextMove != '-') route += nextMove;
                if (nextMove == 'K') dir = Direction.end;
            }
            yield return route;
            yield return ctr;
        }
    }
}
