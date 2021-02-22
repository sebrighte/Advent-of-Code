using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 2: I Was Told There Would Be No Math")]
    class Day02 : BaseLine, Solution
    {
        public object PartOne(string input) => ProcessDimensions1(input);
        public object PartTwo(string input) => ProcessDimensions2(input);

        private object ProcessDimensions1(string inData)
        {
            List<string> input = inData.Split("\n").ToList();
            Present tmpPresent;

            int totalSurfaceArea = 0;

            foreach (var (item, index) in input.WithIndex())
            {
                tmpPresent = new Present(item);
                totalSurfaceArea += tmpPresent.GetSurfaceArea();
            }
            return totalSurfaceArea;
        }

        private object ProcessDimensions2(string inData)
        {
            List<string> input = inData.Split("\n").ToList();
            Present tmpPresent;

           int  totalRibbonlength = 0;

            foreach (var (item, index) in input.WithIndex())
            {
                tmpPresent = new Present(item);
                totalRibbonlength += tmpPresent.GetRibbonLength();
            }
            return totalRibbonlength;
        }
    }

    class Present
    {
        int height;
        int width;
        int length;

        public Present(string dimensions)
        {
            int[] pD = dimensions.Split('x').Select(int.Parse).ToArray();
            Array.Sort(pD);
            height = pD[0]; width = pD[1]; length = pD[2];
        }

        public int GetSurfaceArea()
        {
            int surfaceArea = 0;
            int smallest = 100000;

            surfaceArea += 2 * length * width;
            if (length * width < smallest) smallest = length * width;
            surfaceArea += 2 * width * height;
            if (width * height < smallest) smallest = width * height;
            surfaceArea += 2 * length * height;
            if (length * height < smallest) smallest = length * height;
            surfaceArea += smallest;

            return surfaceArea;
        }

        public int GetRibbonLength()
        {
            int ribbonlength = 0;

            ribbonlength += (2 * height) + (2 * width);
            ribbonlength += length * width * height;

            return ribbonlength;
        }
    }
}

