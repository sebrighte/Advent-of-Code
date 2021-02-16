using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015.Day03
{
    [ProblemName("Day 3: Perfectly Spherical Houses in a Vacuum")]
    class Day03 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private object Day1(string inData)
        {
            string input = inData;
            SortedList<string, int> houses = new SortedList<string, int>();

            int x = 0;
            int y = 0;

            //string test = "^";
            //string test = ">v<";
            //string test = "^v^v^v^v^v";
            //foreach (var item in test.ToArray())
            foreach (var item in input.ToArray())
            {
                switch (item)
                {
                    case '^': x = x - 1; break;
                    case 'v': x = x + 1; break;
                    case '<': y = y - 1; break;
                    case '>': y = y + 1; break;
                }

                string Location = $"{x}-{y}";

                if (houses.ContainsKey(Location))
                    houses[Location] += 1;
                else
                    houses.Add(Location, 1);
            }
            return houses.Count;
            //return "";
        }

        private object Day2(string inData)
        {
            string input = inData;
            SortedList<string, int> houses = new SortedList<string, int>();

            int x = 0;
            int y = 0;
            int xRS = 0;
            int yRS = 0;
            string location = "";

            string initLocation = $"{x}-{y}";
            houses.Add(initLocation, 1);

            //string test = "^v";
            //string test = "^>v<";
            //string test = "^v^v^v^v^v";
            //foreach (var (item, index) in test.ToArray().WithIndex())
            foreach (var (item, index) in input.ToArray().WithIndex())
            {
                if (index % 2 == 0)
                {
                    switch (item)
                    {
                        case '^': x = x - 1; break;
                        case 'v': x = x + 1; break;
                        case '<': y = y - 1; break;
                        case '>': y = y + 1; break;
                    }

                    location = $"{x}-{y}";

                    if (houses.ContainsKey(location))
                        houses[location] += 1;
                    else
                        houses.Add(location, 1);
                }
                else
                {
                    switch (item)
                    {
                        case '^': xRS = xRS - 1; break;
                        case 'v': xRS = xRS + 1; break;
                        case '<': yRS = yRS - 1; break;
                        case '>': yRS = yRS + 1; break;
                    }

                    location = $"{xRS}-{yRS}";

                    if (houses.ContainsKey(location))
                        houses[location] += 1;
                    else
                        houses.Add(location, 1);
                }
            }
            return houses.Count;
        }
    }
}


