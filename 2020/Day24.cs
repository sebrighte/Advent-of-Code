using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 24: Lobby Layout")]
    class Day24 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        Floor myFloor;

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            myFloor = new Floor(200);

            List<string> input = inData.Split("\n").ToList(); //FileToArrayStr(@"..\..\Data\Day24InputTest.txt");

            ProcessInputString(input);

            //Console.WriteLine("\nDay 24/1: (Test) Number of black tiles? " + myFloor.GetTileCount(Tile.etileColour.black));

            //for (int loop = 0; loop < 100; loop++)
            //{
            //    myFloor.flip();
            //}
            //Console.WriteLine("Day 24/2: (Test) Number of black tiles? " + myFloor.GetTileCount(Tile.etileColour.black));

            yield return myFloor.GetTileCount(Tile.etileColour.black);
        }

        private IEnumerable<int> Day2(string inData, bool part2 = false)
        {
            myFloor = new Floor(300);

            List<string> input = inData.Split("\n").ToList();

            ProcessInputString(input);

           // Console.WriteLine("\nDay 24/1: (Test) Number of black tiles? " + myFloor.GetTileCount(Tile.etileColour.black));

            for (int loop = 0; loop < 100; loop++)
            {
                myFloor.flip();
            }
            //Console.WriteLine("Day 24/2: (Test) Number of black tiles? " + myFloor.GetTileCount(Tile.etileColour.black));

            yield return myFloor.GetTileCount(Tile.etileColour.black);
        }

        public void ProcessInputString(List<string> input)
        {
            foreach (string val2 in input)
            {
                int strCtr = 0;
                int pos = 0;

                while (strCtr != val2.Length)
                {
                    if (val2.Substring(strCtr, 1) == "e")
                    {
                        strCtr++;
                        myFloor.MoveTile(pos++, Floor.eDirection.E, strCtr == val2.Length); ;
                    }
                    else if (val2.Substring(strCtr, 1) == "w")
                    {
                        strCtr++;
                        myFloor.MoveTile(pos++, Floor.eDirection.W, strCtr == val2.Length);
                    }
                    else if (val2.Substring(strCtr, 2) == "se")
                    {
                        strCtr += 2;
                        myFloor.MoveTile(pos++, Floor.eDirection.SE, strCtr == val2.Length);
                    }
                    else if (val2.Substring(strCtr, 2) == "sw")
                    {
                        strCtr += 2;
                        myFloor.MoveTile(pos++, Floor.eDirection.SW, strCtr == val2.Length);
                    }
                    else if (val2.Substring(strCtr, 2) == "ne")
                    {
                        strCtr += 2;
                        myFloor.MoveTile(pos++, Floor.eDirection.NE, strCtr == val2.Length);
                    }
                    else if (val2.Substring(strCtr, 2) == "nw")
                    {
                        strCtr += 2;
                        myFloor.MoveTile(pos++, Floor.eDirection.NW, strCtr == val2.Length);
                    }
                }
            }
        }
    }

    class Floor
    {
        Tile[,] floorTiles;
        List<int[]> ListFlip = new List<int[]>();
        int[] startTile;
        int[] linePos;
        int floorSize;
        public enum eDirection { NE, E, SE, SW, W, NW }

        public Floor(int pfloorSize)
        {
            floorSize = pfloorSize;
            floorTiles = new Tile[pfloorSize, pfloorSize];
            startTile = new int[] { (pfloorSize / 2), (pfloorSize / 2) };

            for (int x = 0; x < pfloorSize; x++)
            {
                for (int y = 0; y < pfloorSize; y++)
                {
                    if (x % 2 == 0)
                        if (y % 2 == 0)
                            floorTiles[x, y] = new Tile(Tile.etileColour.white);

                    if (x % 2 == 1)
                        if (y % 2 == 1)
                            floorTiles[x, y] = new Tile(Tile.etileColour.white);
                }
            }
        }

        public void DrawFloor()
        {
            int offset = 5;
            for (int i = offset; i < floorSize - offset; i++)
            {
                for (int j = 0; j < floorSize; j++)
                {
                    //if (i == startTile[0] && j == startTile[1]) Console.Write("S");
                    //else
                    {
                        if (floorTiles[i, j] is null)
                            Console.Write("|");
                        else
                        {
                            if (floorTiles[i, j].GetTileColour() == Tile.etileColour.white) Console.Write(" ");
                            if (floorTiles[i, j].GetTileColour() == Tile.etileColour.black) Console.Write("X");
                        }

                    }

                }
                if (i % 2 == 0)
                {
                    Console.WriteLine();
                    for (int k = 0; k < floorSize; k += 2)
                        Console.Write("\\/");
                }

                if (i % 2 != 0)
                {
                    Console.WriteLine();
                    for (int k = 0; k < floorSize; k += 2)
                        Console.Write("/\\");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void flip()
        {
            for (int i = 2; i < floorSize - 2; i++)
            {
                for (int j = 2; j < floorSize - 2; j++)
                {
                    if (floorTiles[i, j] is null)
                    { }//Console.Write("");
                    else
                    {
                        if (floorTiles[i, j].GetTileColour() == Tile.etileColour.black)
                        {
                            int ctr1 = 0;
                            if (floorTiles[i - 1, j + 1].GetTileColour() == Tile.etileColour.black) ctr1++;
                            if (floorTiles[i, j + 2].GetTileColour() == Tile.etileColour.black) ctr1++;
                            if (floorTiles[i + 1, j + 1].GetTileColour() == Tile.etileColour.black) ctr1++;
                            if (floorTiles[i + 1, j - 1].GetTileColour() == Tile.etileColour.black) ctr1++;
                            if (floorTiles[i, j - 2].GetTileColour() == Tile.etileColour.black) ctr1++;
                            if (floorTiles[i - 1, j - 1].GetTileColour() == Tile.etileColour.black) ctr1++;

                            if (ctr1 == 0 || ctr1 > 2)
                            {
                                ListFlip.Add(new int[] { i, j });
                            }
                        }

                        else if (floorTiles[i, j].GetTileColour() == Tile.etileColour.white)
                        {
                            int ctr2 = 0;
                            if (floorTiles[i - 1, j + 1].GetTileColour() == Tile.etileColour.black) ctr2++;
                            if (floorTiles[i, j + 2].GetTileColour() == Tile.etileColour.black) ctr2++;
                            if (floorTiles[i + 1, j + 1].GetTileColour() == Tile.etileColour.black) ctr2++;
                            if (floorTiles[i + 1, j - 1].GetTileColour() == Tile.etileColour.black) ctr2++;
                            if (floorTiles[i, j - 2].GetTileColour() == Tile.etileColour.black) ctr2++;
                            if (floorTiles[i - 1, j - 1].GetTileColour() == Tile.etileColour.black) ctr2++;

                            if (ctr2 == 2)
                            {
                                ListFlip.Add(new int[] { i, j });
                            }
                        }
                    }
                }
            }
            foreach (int[] flip in ListFlip)
                floorTiles[flip[0], flip[1]].FlipColourTile();

            ListFlip.Clear();
        }

        public int GetTileCount(Tile.etileColour colour)
        {
            int ctr = 0;
            foreach (Tile t in floorTiles)
                if (t is null)
                { }
                else if (t.GetTileColour() == colour)
                    ctr++;
            return ctr;
        }

        public void MoveTile(int pos, eDirection dir, bool end)
        {
            if (pos == 0) linePos = (int[])startTile.Clone();

            switch (dir)
            {
                case eDirection.NE:
                    linePos[0] = linePos[0] - 1;
                    linePos[1] = linePos[1] + 1;
                    break;
                case eDirection.E:
                    linePos[1] = linePos[1] + 2;
                    break;
                case eDirection.SE:
                    linePos[0] = linePos[0] + 1;
                    linePos[1] = linePos[1] + 1;
                    break;
                case eDirection.SW:
                    linePos[0] = linePos[0] + 1;
                    linePos[1] = linePos[1] - 1;
                    break;
                case eDirection.W:
                    linePos[1] = linePos[1] - 2;
                    break;
                case eDirection.NW:
                    linePos[0] = linePos[0] - 1;
                    linePos[1] = linePos[1] - 1;
                    break;
            }
            if (end)
            {
                floorTiles[linePos[0], linePos[1]].FlipColourTile();
            }
        }

    }

    class Tile
    {
        public enum etileColour { black, white }
        public etileColour colour;

        public Tile(etileColour pcolour)
        {
            colour = pcolour;
        }

        public void FlipColourTile()
        {
            if (this.colour == etileColour.black) this.colour = etileColour.white;
            else this.colour = etileColour.black;
        }

        public etileColour GetTileColour()
        {
            return colour;
        }
    }
}
