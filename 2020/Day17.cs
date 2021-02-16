using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 17: Conway Cubes")]
    class Day17 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private int Day1(string inputStr)
        {
            //Console.WriteLine("\n\nDay 17: Conway Cubes\n");

            //List<string> input = FileToArrayStr(@"..\..\Data\Day17Input.txt");
            List<string> input = inputStr.Split("\r\n").ToList();

            //int cubeSize = Convert.ToInt32(Math.Pow(input[0].Length,3))
            int cubeSize = 50;

            ConwayCube3D cubleCls = new ConwayCube3D(cubeSize);

            int startPoint = Convert.ToInt32(Math.Sqrt(cubeSize));

            int x = startPoint;
            int y = startPoint;
            int z = startPoint;
            int w = startPoint;

            //Console.WriteLine("Offset = " + startPoint);

            foreach (string strS in input)
            {
                char[] strC = strS.ToCharArray();
                foreach (char chr in strC)
                {
                    cubleCls.cube[x, y, z] = chr;
                    y++;
                }
                y = startPoint;
                x++;
            }

            cubleCls.cube = cubleCls.ApplyCycle(6);

            return cubleCls.CountActivated();

            //Console.WriteLine("Day 17/1: " + cubleCls.CountActivated() + " Cells activated");
        }

        private int Day2(string inputStr)
        {
            //Console.WriteLine("\n\nDay 17: Conway Cubes\n");

            //List<string> input = FileToArrayStr(@"..\..\Data\Day17Input.txt");

            List<string> input = inputStr.Split("\r\n").ToList();

            //int cubeSize = Convert.ToInt32(Math.Pow(input[0].Length,3));

            int cubeSize = 35;

            ConwayCube4D cubleCls = new ConwayCube4D(cubeSize);

            int startPoint = Convert.ToInt32(Math.Sqrt(cubeSize));

            int x = startPoint;
            int y = startPoint;
            int z = startPoint;
            int w = startPoint;

            //Console.WriteLine("Offset = " + startPoint);

            foreach (string strS in input)
            {
                char[] strC = strS.ToCharArray();
                foreach (char chr in strC)
                {
                    cubleCls.cube[x, y, z, w] = chr;
                    y++;
                }
                y = startPoint;
                x++;
            }

            cubleCls.cube = cubleCls.ApplyCycle(6);

            return cubleCls.CountActivated();
            //Console.WriteLine("Day 17/2: " + cubleCls.CountActivated() + " Cells activated");
        }
    }

    class ConwayCube3D
    {
        public char[,,] cube { get; set; }
        private int size { get; set; }

        public ConwayCube3D(int cubeSize)
        {
            cube = new char[cubeSize, cubeSize, cubeSize];
            size = cubeSize;

            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        cube[x, y, z] = '.';
                    }
                }
            }
        }

        public void DrawCube()
        {
            Console.Write("\nDrawCube#1\n\n");
            for (int z = 0; z < size; z++)
            {
                Console.WriteLine("z" + (z + 1));
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        Console.Write(cube[x, y, z]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public void DrawCube(int layer)
        {
            Console.Write("\nDrawCube#1\n\n");
            Console.WriteLine("z" + (layer + 1));
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Console.Write(cube[x, y, layer]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int CountActivated()
        {
            int ctr = 0;
            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        if (cube[x, y, z] == '#')
                        {
                            ctr++;
                        }
                    }
                }
            }
            return ctr;
        }

        public char[,,] SetRoundPoint(int xIn, int yIn, int zIn)
        {
            char[,,] tmpCube = this.CopyCube();

            for (int z = -1; z < 2; z++)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if (!(x == 0 && y == 0 && z == 0))
                            tmpCube[xIn + x, yIn + y, zIn + z] = 'X';
                    }
                }
            }
            return tmpCube;
        }

        public char[,,] ApplyCycle(int recurse)
        {
            int CRPRes = 0;
            char[,,] tmpCube = this.CopyCube();
            for (int r = 0; r < recurse; r++)
            {
                //Console.WriteLine("Cycle " + (r + 1) + " of " + recurse);
                tmpCube = this.CopyCube();
                for (int z = 0; z < size; z++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            if (cube[x, y, z] == '#' || cube[x, y, z] == '.') CRPRes = CountRoundPoint(x, y, z);

                            if (cube[x, y, z] == '#')
                            {
                                if ((CRPRes == 2) || (CRPRes == 3)) { }
                                else tmpCube[x, y, z] = '.';
                            }
                            if (cube[x, y, z] == '.')
                            {
                                if (CRPRes == 3) tmpCube[x, y, z] = '#';
                            }
                        }
                    }
                }
                cube = tmpCube;
            }
            return tmpCube;
        }

        public char[,,] ApplyCycle()
        {
            int CRPRes = 0;
            char[,,] tmpCube = this.CopyCube();
            Console.WriteLine("Cycle");
            tmpCube = this.CopyCube();
            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        if (cube[x, y, z] == '#' || cube[x, y, z] == '.') CRPRes = CountRoundPoint(x, y, z);

                        if (cube[x, y, z] == '#')
                        {
                            if ((CRPRes == 2) || (CRPRes == 3)) { }
                            else tmpCube[x, y, z] = '.';
                        }
                        if (cube[x, y, z] == '.')
                        {
                            if (CRPRes == 3) tmpCube[x, y, z] = '#';
                        }
                    }
                }
            }
            return tmpCube;
        }

        private char[,,] CopyCube()
        {
            char[,,] tmpCube = new char[size, size, size];
            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        tmpCube[x, y, z] = cube[x, y, z];
                    }
                }
            }
            return tmpCube;
        }

        private int CountRoundPoint(int xIn, int yIn, int zIn)
        {
            int ctr = 0;
            for (int z = -1; z < 2; z++)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        if ((xIn + x > 0) && (yIn + y > 0) && (zIn + z > 0))
                        {
                            if ((xIn + x < size) && (yIn + y < size) && (zIn + z < size))
                            {
                                if (cube[xIn + x, yIn + y, zIn + z] == '#' && !(x == 0 && y == 0 && z == 0))
                                {
                                    //Console.WriteLine(string.Format("#{0} {1} {2}", xIn + x, yIn + y, zIn + z));
                                    ctr++;
                                }
                                else
                                {
                                    //Console.WriteLine(string.Format("{0} {1} {2}", xIn + x, yIn + y, zIn + z));
                                }
                            }
                        }
                        //catch
                        {
                            //if at edge of cube will error
                        }
                    }
                }
            }
            return ctr;
        }

        private int CountActivatedLayer(int layer)
        {
            int ctr = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (cube[x, y, layer] == '#')
                    {
                        ctr++;
                    }
                }
            }
            return ctr;
        }

        public void Check()
        {
            //tmpCube[x, y, z] = CountRoundPoint(x, y, z).ToString().ToCharArray()[0];
            char[,,] tmpCube = cube;
            Console.Write("\nCheckCube#1\n\n");
            for (int z = 0; z < size; z++)
            {
                Console.WriteLine("z" + (z + 1));
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        Console.Write(tmpCube[x, y, z] = CountRoundPoint(x, y, z).ToString().ToCharArray()[0]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }

    class ConwayCube4D : BaseLine
    {
        public char[,,,] cube { get; set; }
        private int size { get; set; }

        public ConwayCube4D(int cubeSize)
        {
            cube = new char[cubeSize, cubeSize, cubeSize, cubeSize];
            size = cubeSize;

            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int w = 0; w < size; w++)
                            cube[x, y, z, w] = '.';
                    }
                }
            }
        }

        public void DrawCube()
        {
            Console.Write("\nDrawCube#1\n\n");
            for (int z = 0; z < size; z++)
            {
                Console.WriteLine("z" + (z + 1));
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int w = 0; w < size; w++)
                            Console.Write(cube[x, y, z, w]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public void DrawCube(int layer)
        {
            Console.Write("\nDrawCube#1\n\n");
            Console.WriteLine("z" + (layer + 1));
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int w = 0; w < size; w++)
                        Console.Write(cube[x, y, layer, w]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int CountActivated()
        {
            int ctr = 0;
            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int w = 0; w < size; w++)
                            if (cube[x, y, z, w] == '#')
                            {
                                ctr++;
                            }
                    }
                }
            }
            return ctr;
        }

        public char[,,,] SetRoundPoint(int xIn, int yIn, int zIn, int wIn)
        {
            char[,,,] tmpCube = this.CopyCube();

            for (int z = -1; z < 2; z++)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        for (int w = 0; w < size; w++)
                        {
                            if (!(x == 0 && y == 0 && z == 0))
                                tmpCube[xIn + x, yIn + y, zIn + z, wIn + w] = 'X';
                        }
                    }
                }
            }
            return tmpCube;
        }

        public char[,,,] ApplyCycle(int recurse)
        {
            int CRPRes = 0;
            char[,,,] tmpCube = this.CopyCube();
            for (int r = 0; r < recurse; r++)
            {
                //Console.WriteLine("Cycle " + (r + 1) + " of " + recurse);
                //DrawTextProgressBar(r, recurse-1);
                tmpCube = this.CopyCube();
                for (int z = 0; z < size; z++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            for (int w = 0; w < size; w++)
                            {
                                if (cube[x, y, z, w] == '#' || cube[x, y, z, w] == '.') CRPRes = CountRoundPoint(x, y, z, w);

                                if (cube[x, y, z, w] == '#')
                                {
                                    if ((CRPRes == 2) || (CRPRes == 3)) { }
                                    else tmpCube[x, y, z, w] = '.';
                                }
                                if (cube[x, y, z, w] == '.')
                                {
                                    if (CRPRes == 3) tmpCube[x, y, z, w] = '#';
                                }
                            }
                        }
                    }
                }
                cube = tmpCube;
            }
            return tmpCube;
        }

        public char[,,,] ApplyCycle()
        {
            int CRPRes = 0;
            char[,,,] tmpCube = this.CopyCube();
            Console.WriteLine("Cycle");
            tmpCube = this.CopyCube();
            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int w = 0; w < size; w++)
                        {
                            if (cube[x, y, z, w] == '#' || cube[x, y, z, w] == '.') CRPRes = CountRoundPoint(x, y, z, w);

                            if (cube[x, y, z, w] == '#')
                            {
                                if ((CRPRes == 2) || (CRPRes == 3)) { }
                                else tmpCube[x, y, z, w] = '.';
                            }
                            if (cube[x, y, z, w] == '.')
                            {
                                if (CRPRes == 3) tmpCube[x, y, z, w] = '#';
                            }
                        }
                    }
                }
            }
            return tmpCube;
        }

        private char[,,,] CopyCube()
        {
            char[,,,] tmpCube = new char[size, size, size, size];
            for (int z = 0; z < size; z++)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int w = 0; w < size; w++)
                            tmpCube[x, y, z, w] = cube[x, y, z, w];
                    }
                }
            }
            return tmpCube;
        }

        private int CountRoundPoint(int xIn, int yIn, int zIn, int wIn)
        {
            int ctr = 0;
            for (int z = -1; z < 2; z++)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        for (int w = -1; w < 2; w++)
                        {
                            if ((xIn + x > 0) && (yIn + y > 0) && (zIn + z > 0) && (wIn + w > 0))
                            {
                                if ((xIn + x < size) && (yIn + y < size) && (zIn + z < size) && (wIn + w < size))
                                {
                                    if (cube[xIn + x, yIn + y, zIn + z, wIn + w] == '#' && !(x == 0 && y == 0 && z == 0 && w == 0))
                                    {
                                        //Console.WriteLine(string.Format("#{0} {1} {2}", xIn + x, yIn + y, zIn + z));
                                        ctr++;
                                    }
                                    else
                                    {
                                        //Console.WriteLine(string.Format("{0} {1} {2}", xIn + x, yIn + y, zIn + z));
                                    }
                                }
                            }
                        }
                        //catch
                        {
                            //if at edge of cube will error
                        }
                    }
                }
            }
            return ctr;
        }

        private int CountActivatedLayer(int layer)
        {
            int ctr = 0;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int w = 0; w < size; w++)
                    {
                        if (cube[x, y, layer, w] == '#')
                        {
                            ctr++;
                        }
                    }
                }
            }
            return ctr;
        }

        public void Check()
        {
            //tmpCube[x, y, z] = CountRoundPoint(x, y, z).ToString().ToCharArray()[0];
            char[,,,] tmpCube = cube;
            Console.Write("\nCheckCube#1\n\n");
            for (int z = 0; z < size; z++)
            {
                Console.WriteLine("z" + (z + 1));
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int w = 0; w < size; w++)
                        {
                            Console.Write(tmpCube[x, y, z, w] = CountRoundPoint(x, y, z, w).ToString().ToCharArray()[0]);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}