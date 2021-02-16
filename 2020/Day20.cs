using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 20: Jurassic Jigsaw")]
    class Day20 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        List<string> input;
        List<SatImage> satImages;

        static int arraySize;
        string[,] posList;
        char[,] combined;

        private IEnumerable<long> Day1(string inData, bool part2 = false)
        {
            LoadImages(inData);

            arraySize = 20;
            posList = new string[arraySize, arraySize];

            int sqrt = Convert.ToInt32(Math.Sqrt(satImages.Count));
            combined = new char[sqrt * 8, sqrt * 8];
            posList = new string[sqrt * 2, sqrt * 2];

            //Part 1
            RunCycle();

            if(!part2)
                yield return GetResult();

            //Part 2
            CollateImages();

            if (part2)
                yield return FindMonster();     
        }

        private void RunCycle()
        {
            //Prepopulte satimages relationship grid
            for (int x = 0; x < posList.GetLength(0); x++)
            {
                for (int y = 0; y < posList.GetLength(1); y++)
                {
                    posList[x, y] = "";

                }
            }

            posList[posList.GetLength(0) / 2, posList.GetLength(1) / 2] = "0";
            satImages[0].locked = true;

            while (GetLockedCount() != satImages.Count)
            {
                for (int a = 0; a < satImages.Count; a++)
                {
                    for (int b = 0; b < satImages.Count; b++)
                    {

                        if (satImages[a].locked)
                            CompareTiles(a, b);
                    }
                    //if (GetLockedCount() % 12 == 0)
                    //DrawTextProgressBar(GetLockedCount(), satImages.Count);

                }
            }

            //Cal reduded borders on correctly mapped images
            for (int a = 0; a < satImages.Count; a++)
            {
                satImages[a].RemoveBorders();
            }
        }

        private void CollateImages()
        {
            //Get exact grid of maped images
            int[,] grid = GetGrid();

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    AddToGrid(grid[x, y], x, y);
                }
            }
        }

        private int[,] GetGrid()
        {
            //get first position of a value in main grid
            int x1 = 0;
            int y1 = 0;

            for (int x = 0; x < posList.GetLength(0); x++)
            {
                for (int y = 0; y < posList.GetLength(1); y++)
                {
                    if (posList[x, y] != "")
                    {
                        if (x1 == 0) x1 = x;
                        if (y1 == 0) y1 = y;
                    }

                }
            }

            int sqrt = Convert.ToInt32(Math.Sqrt(satImages.Count));
            int[,] grid = new int[sqrt, sqrt];
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = int.Parse(posList[x1 + x, y1 + y]);
                }
            }
            return grid;
        }

        private int[][] CreateMonster()
        {
            //                  # 
            //#    ##    ##    ###
            //#  #  #  #  #  #   

            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/jagged-arrays

            int[][] newMonster =
            {
                new int[] { 0, 18 },
                new int[] { 1, 0 },
                new int[] { 1, 5 },
                new int[] { 1, 6 },
                new int[] { 1, 11 },
                new int[] { 1, 12 },
                new int[] { 1, 17 },
                new int[] { 1, 18 },
                new int[] { 1, 19 },
                new int[] { 2, 1 },
                new int[] { 2, 4 },
                new int[] { 2, 7 },
                new int[] { 2, 10 },
                new int[] { 2, 13 },
                new int[] { 2, 16 }
            };

            return newMonster;
        }

        private int FindMonster()
        {
            //cheat as dont try all options...
            combined = RotateImageClockwise(combined);
            combined = RotateImageClockwise(combined);
            combined = RotateImageClockwise(combined);

            return GetHashCount(combined) - (MonsterSearch() * 15);
        }

        private int MonsterSearch()
        {
            int[][] monster = CreateMonster();
            int xExtent = combined.GetLength(0) - 2;
            int yExtent = combined.GetLength(0) - 19;

            int mstrCtr = 0;
            for (int x = 0; x < xExtent; ++x)
            {
                for (int y = 0; y < yExtent; ++y)
                {
                    {
                        int monsterBitsCtr = 0;

                        foreach (int[] monst in monster)
                        {
                            int xOff = monst[0];
                            int yOff = monst[1];
                            if (combined[x + xOff, y + yOff] == '#')
                                monsterBitsCtr++;
                        }
                        if (monsterBitsCtr == 15)
                        {
                            mstrCtr++;
                        }
                        monsterBitsCtr = 0;
                    }
                }
            }
            return mstrCtr;
        }

        public char[,] InvertImageVertical(char[,] arr)
        {
            char[,] ret = new char[arr.GetLength(0), arr.GetLength(1)];

            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(0); ++j)
                {
                    ret[i, j] = arr[arr.GetLength(0) - i - 1, j];
                }
            }
            return ret;
        }

        public char[,] RotateImageClockwise(char[,] arr)
        {
            char[,] ret = new char[arr.GetLength(0), arr.GetLength(1)];

            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(0); ++j)
                {
                    ret[i, j] = arr[arr.GetLength(0) - j - 1, i];
                }
            }
            return ret;
        }

        public int GetHashCount(char[,] arr)
        {
            int ctr = 0;

            for (int i = 0; i < arr.GetLength(0); ++i)
            {
                for (int j = 0; j < arr.GetLength(0); ++j)
                {
                    if (arr[i, j] == '#') ctr++;
                }
            }
            return ctr;
        }

        private void AddToGrid(int id, int xOff, int yOff)
        {
            for (int x = 0; x < satImages[id].satImageReduced.GetLength(0); x++)
            {
                for (int y = 0; y < satImages[id].satImageReduced.GetLength(1); y++)
                {
                    int linrItem = satImages[id].satImageReduced[x, y];
                    int xRelated = x + xOff * 8;
                    int yRelated = y + yOff * 8;
                    combined[xRelated, yRelated] = satImages[id].satImageReduced[x, y];
                }
            }
        }

        private long GetResult()
        {
            int x1 = 0;
            int y1 = 0;

            for (int x = 0; x < posList.GetLength(0); x++)
            {
                for (int y = 0; y < posList.GetLength(1); y++)
                {
                    if (posList[x, y] != "")
                    {
                        if (x1 == 0) x1 = x;
                        if (y1 == 0) y1 = y;
                    }

                }
            }

            int sqrt = Convert.ToInt32(Math.Sqrt(satImages.Count)) - 1;

            long tl = satImages[int.Parse(posList[x1, y1])].imageId;
            long bl = satImages[int.Parse(posList[x1 + sqrt, y1])].imageId;
            long tr = satImages[int.Parse(posList[x1, y1 + sqrt])].imageId;
            long br = satImages[int.Parse(posList[x1 + sqrt, y1 + sqrt])].imageId;

            long result = tl * bl * tr * br;

            return result;
        }

        private int GetLockedCount()
        {
            int ctr = 0;
            for (int a = 0; a < satImages.Count; a++)
            {
                if (satImages[a].locked)
                    ctr++;
            }
            return ctr;
        }

        private void SetPosition(int DatumImage, int SearchImage, int i)
        {
            //int p = Math.Abs(i - j);
            {
                for (int x = 0; x < posList.GetLength(0); x++)
                {
                    for (int y = 0; y < posList.GetLength(1); y++)
                    {
                        if (posList[x, y] == DatumImage.ToString())
                        {
                            if (i == 0)// && posList[x - 1, y] == "")
                            {
                                posList[x - 1, y] = SearchImage.ToString();
                                satImages[DatumImage].edges[0] = SearchImage;
                                satImages[SearchImage].edges[2] = DatumImage;
                            }
                            if (i == 1)// && posList[x, y + 1] == "")
                            {
                                posList[x, y + 1] = SearchImage.ToString();
                                satImages[DatumImage].edges[1] = SearchImage;
                                satImages[SearchImage].edges[3] = DatumImage;
                            }
                            if (i == 2)// && posList[x + 1, y] == "") 
                            {
                                posList[x + 1, y] = SearchImage.ToString();
                                satImages[DatumImage].edges[2] = SearchImage;
                                satImages[SearchImage].edges[0] = DatumImage;
                            }
                            if (i == 3)// && posList[x, y - 1] == "") 
                            {
                                posList[x, y - 1] = SearchImage.ToString();
                                satImages[DatumImage].edges[3] = SearchImage;
                                satImages[SearchImage].edges[1] = DatumImage;
                            }
                        }
                    }
                }
            }
        }

        private void ArrangeTiles(int DatumImage, int SearchImage, int dFace)
        {
            string[] edgesDatum = satImages[DatumImage].GetDatumEdges();
            string[] edgesSearch;

            int[] searchIndex = { 2, 3, 0, 1 };
            if (DatumImage == 5 && SearchImage == 8)
            {
            }

            edgesSearch = satImages[SearchImage].GetSreachEdges(searchIndex[dFace]);
            for (int j = 0; j < edgesSearch.Count(); j++)
            {
                if (edgesDatum[dFace] == edgesSearch[j] && DatumImage != SearchImage && satImages[SearchImage].locked == false)
                {
                    if (j == 0)
                    {
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                    if (j == 1)
                    {
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                    if (j == 2)
                    {
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                    if (j == 3)
                    {
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                    if (j == 4)
                    {
                        satImages[SearchImage].InvertImageHorizontal();
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                    if (j == 5)
                    {
                        satImages[SearchImage].InvertImageHorizontal();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                    if (j == 6)
                    {
                        satImages[SearchImage].InvertImageHorizontal();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                    if (j == 7)
                    {
                        satImages[SearchImage].InvertImageHorizontal();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].RotateImageClockwise();
                        satImages[SearchImage].locked = true;
                        satImages[DatumImage].locked = true;
                        SetPosition(DatumImage, SearchImage, dFace);
                    }
                }
            }
        }

        private void CompareTiles(int DatumImage, int SearchImage)
        {
            if (DatumImage == SearchImage) return;

            string[] edgesDatum = satImages[DatumImage].GetDatumEdges();

            for (int i = 0; i < edgesDatum.Count(); i++)
            {
                ArrangeTiles(DatumImage, SearchImage, i);
            }
        }

        private void LoadImages(string DataFile)
        {
            satImages = new List<SatImage>();
            input = DataFile.Split("\r\n").ToList();

            int x = 0;
            int tilectr = 0;
            char[,] tmpSatImage = new char[10, 10];
            string tmpImgTitle = "";
            SatImage tmpSatImg;

            foreach (string str in input)
            {
                if (str.Contains("Tile"))
                {
                    tmpImgTitle = str;
                    tilectr++;
                }
                else if (str == "")
                {
                    //x = 0;
                }
                else
                {
                    char[] strC = str.ToCharArray();
                    for (int i = 0; i < strC.Length; i++)
                    {
                        tmpSatImage[x, i] = strC[i];
                    }
                    x++;
                    if (x == 10)
                    {
                        tmpSatImg = new SatImage(tmpSatImage, tmpImgTitle);
                        satImages.Add(tmpSatImg);
                        tmpSatImage = new char[10, 10];
                        x = 0;
                    }
                }
            }
        }
    }

    class SatImage
    {
        public char[,] satImage;
        public char[,] satImageReduced;
        public int imageId;
        public bool locked;
        public int[] edges;

        public SatImage(char[,] img, string title)
        {
            satImage = img;
            imageId = int.Parse(title.Split(' ')[1].Split(':')[0]);
            locked = false;
            edges = new int[4];
            edges[0] = edges[1] = edges[2] = edges[3] = -1;
        }

        public string GetTopEdge()
        {
            string retStr = "";
            for (int i = 0; i < 10; ++i)
            {
                retStr += satImage[0, i];
            }
            return retStr;
        }

        public string GetLeftEdge()
        {
            string retStr = "";
            for (int i = 0; i < 10; ++i)
            {
                retStr += satImage[i, 0];
            }
            return retStr;
        }

        public string GetBottomEdge()
        {
            string retStr = "";
            for (int i = 0; i < 10; ++i)
            {
                retStr += satImage[9, i];
            }
            return retStr;
        }

        public string GetRightEdge()
        {
            string retStr = "";
            for (int i = 0; i < 10; ++i)
            {
                retStr += satImage[i, 9];
            }
            return retStr;
        }

        public string[] GetDatumEdges()
        {
            string[] satImageEdges = new string[4];
            {
                satImageEdges[0] = GetTopEdge();
                satImageEdges[1] = GetRightEdge();
                satImageEdges[2] = GetBottomEdge();
                satImageEdges[3] = GetLeftEdge();
            }
            return satImageEdges;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string[] GetSreachEdges(int start)
        {
            string[] satImageEdges = new string[32];
            string[] satFilteredEdges = new string[8];
            int ctr = 0;

            for (int i = 0; i < 4; i++)
            {
                satImageEdges[ctr++] = GetTopEdge();
                satImageEdges[ctr++] = GetRightEdge();
                satImageEdges[ctr++] = GetBottomEdge();
                satImageEdges[ctr++] = GetLeftEdge();
                RotateImageClockwise();
            }

            InvertImageHorizontal();

            for (int i = 0; i < 4; i++)
            {
                satImageEdges[ctr++] = GetTopEdge();
                satImageEdges[ctr++] = GetRightEdge();
                satImageEdges[ctr++] = GetBottomEdge();
                satImageEdges[ctr++] = GetLeftEdge();
                RotateImageClockwise();
            }

            InvertImageHorizontal();

            ctr = 0;
            for (int i = start; i < 32; i += 4)
                satFilteredEdges[ctr++] = satImageEdges[i];

            return satFilteredEdges;
        }

        public string DrawImage()
        {
            string image = "";
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Console.Write(satImage[x, y]);
                    image += satImage[x, y];
                }
                Console.WriteLine();
                image += "\n";
            }
            Console.WriteLine();
            image += "\n";
            return image;
        }

        public void DrawImageReduced()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Console.Write(satImageReduced[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void RemoveBorders()
        {
            char[,] tmpImage = new char[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    tmpImage[x, y] = satImage[x + 1, y + 1];
                }
            }
            satImageReduced = tmpImage;
        }

        public void RotateImageClockwise()
        {
            if (locked) return;
            char[,] ret = new char[10, 10];

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    ret[i, j] = satImage[10 - j - 1, i];
                }
            }
            satImage = ret;
        }

        public void RotateImageAntiClockwise()
        {
            if (locked) return;
            char[,] ret = new char[10, 10];

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    ret[i, j] = satImage[j, 10 - i - 1];
                }
            }
            satImage = ret;
        }

        public void InvertImageHorizontal()
        {
            if (locked) return;
            char[,] ret = new char[10, 10];

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    ret[i, j] = satImage[i, 10 - j - 1];
                }
            }
            satImage = ret;
        }

        public void InvertImageVertical()
        {
            if (locked) return;
            char[,] ret = new char[10, 10];

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    ret[i, j] = satImage[10 - i - 1, j];
                }
            }
            satImage = ret;
        }
    }
}
