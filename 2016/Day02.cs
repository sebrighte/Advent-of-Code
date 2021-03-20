using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 2: Bathroom Security")]
    class Day02 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<object> Day1(string inData)
        {
            //inData = "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD";
            //1985

            List<string> instructions = inData.Split("\r\n").ToList();

            int position = 5;
            string code = "";

            foreach (var instruction in instructions)
            {
                foreach (char dir in instruction)
                {
                    switch (position)
                    {
                        case 1:
                            if (dir == 'D')
                                position = 4;
                            if (dir == 'R')
                                position = 2;
                            break;

                        case 2:
                            if (dir == 'D')
                                position = 5;
                            if (dir == 'L')
                                position = 1;
                            if (dir == 'R')
                                position = 3;
                            break; 
                            
                        case 3:
                            if (dir == 'L')
                                position = 2;
                            if (dir == 'D')
                                position = 6;
                            break;

                        case 4:
                            if (dir == 'U')
                                position = 1;
                            if (dir == 'D')
                                position = 7;
                            if (dir == 'R')
                                position = 5;
                            break;

                        case 5:
                            if (dir == 'U')
                                position = 2;
                            if (dir == 'D')
                                position = 8;
                            if (dir == 'L')
                                position = 4;
                            if (dir == 'R')
                                position = 6;
                            break;

                        case 6:
                            if (dir == 'U')
                                position = 3;
                            if (dir == 'D')
                                position = 9;
                            if (dir == 'L')
                                position = 5;
                            break;

                        case 7:
                            if (dir == 'U')
                                position = 4;
                            if (dir == 'R')
                                position = 8;
                            break;

                        case 8:
                            if (dir == 'U')
                                position = 5;
                            if (dir == 'L')
                                position = 7;
                            if (dir == 'R')
                                position = 9;
                            break;

                        case 9:
                            if (dir == 'U')
                                position = 6;
                            if (dir == 'L')
                                position = 8;
                            break;

                    }

                }
                code += position;

            }
            yield return code;
        }

        private IEnumerable<object> Day2(string inData)
        {
            //inData = "ULL\r\nRRDDD\r\nLURDL\r\nUUUUD";
            //5DB3

            //563B6

            List<string> instructions = inData.Split("\r\n").ToList();

            char position = '5';
            string code = "";

            foreach (var instruction in instructions)
            {
                foreach (char dir in instruction)
                {
                    switch (position)
                    {
                        case '1':
                            if (dir == 'D') position = '3';
                            break;

                        case '2':
                            if (dir == 'D') position = '6';
                            if (dir == 'R') position = '3';
                            break;

                        case '3':
                            if (dir == 'U') position = '1';
                            if (dir == 'D') position = '7';
                            if (dir == 'L') position = '2';
                            if (dir == 'R') position = '4';
                            break;

                        case '4':
                            if (dir == 'L')  position = '3';
                            if (dir == 'D')  position = '8';
                            break;

                        case '5':
                            if (dir == 'R')  position = '6';
                            break;

                        case '6':
                            if (dir == 'U') position = '2';
                            if (dir == 'D') position = 'A';
                            if (dir == 'L') position = '5';
                            if (dir == 'R') position = '7';
                            break;

                        case '7':
                            if (dir == 'U') position = '3';
                            if (dir == 'D') position = 'B';
                            if (dir == 'L') position = '6';
                            if (dir == 'R') position = '8';
                            break;

                        case '8':
                            if (dir == 'U') position = '4';
                            if (dir == 'D') position = 'C';
                            if (dir == 'L') position = '7';
                            if (dir == 'R') position = '9';
                            break;

                        case '9':
                            if (dir == 'L') position = '8';
                            break;

                        case 'A':
                            if (dir == 'U') position = '6';
                            if (dir == 'R') position = 'B';
                            break;

                        case 'B':
                            if (dir == 'U') position = '7';
                            if (dir == 'D') position = 'D';
                            if (dir == 'L') position = 'A';
                            if (dir == 'R') position = 'C';
                            break;

                        case 'C':
                            if (dir == 'L') position = 'B';
                            if (dir == 'U') position = '8';
                            break;

                        case 'D':
                            if (dir == 'U')  position = 'B';
                            break;
                    }
                }
                code += position;
            }
            yield return code;
        }
    }
}
