using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 8: Two-Factor Authentication")]
    class Day08 : BaseLine, Solution
    {
        //public object PartOne(string input) => Day1(input, 3, 7).First();
        public object PartOne(string input) => Day1(input, 6, 50).First();
        public object PartTwo(string input) => Day1(input, 6, 50, true).First();

        private IEnumerable<object> Day1(string inData, int height, int width, bool part2 = false)
        {
            //inData = "rect 3x2\r\nrotate column x=1 by 1\r\nrotate row y=0 by 4\r\nrotate column x=1 by 1";
            List<string> input = inData.Split("\r\n").ToList();
            char[,] screen = new char[height,width];
            screen.FillCharArray(' ');

            foreach (var item in input)
            {
                if (item.Contains("rect"))
                {
                    var matches = Regex.Match(item, @"rect (\d*)x(\d*)");
                    for (int i = 0; i < int.Parse(matches.Groups[2].Value); i++)
                        for (int j = 0; j < int.Parse(matches.Groups[1].Value); j++)
                            screen[i,j] = '#';
                }
                if (item.Contains("rotate"))
                {
                    var matches = Regex.Match(item, @"rotate (.*) (.*)=(\d*) by (\d*)");
                    int colMax = screen.GetLength(1) - 1;
                    int rowMax = screen.GetLength(0) - 1;
                    int axisNo = int.Parse(matches.Groups[3].Value);
                    int rotations = int.Parse(matches.Groups[4].Value);
                    string axis = matches.Groups[1].Value;

                    for (int i = 0; i < rotations; i++)
                    {
                        if (axis == "column")
                        {
                            char end = screen[rowMax, axisNo];
                            for (int c = rowMax; c != 0; c--)
                                screen[c, axisNo] = screen[c - 1, axisNo];
                            screen[0, axisNo] = end;
                        }
                        else if (axis == "row")
                        {
                            char end = screen[axisNo, colMax];
                            for (int c = colMax; c != 0; c--)
                                screen[axisNo,c] = screen[axisNo, c-1];
                            screen[axisNo,0] = end;
                        }
                    }
                }
            } 
            yield return !part2? screen.CountCharArray('#') : screen.DrawCharArray(false);
        } 
    }
}
