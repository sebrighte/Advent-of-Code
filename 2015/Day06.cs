using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 6: Probably a Fire Hazard")]
    class Day06 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input);
        public object PartTwo(string input) => Day2(input);

        private object Day1(string inData)
        {
            HashSet<string> lights = new HashSet<string>();

            string[] test = { "turn on 0,0 through 999,999", "toggle 0,0 through 999,0", "turn off 499,499 through 500,500" };

            List<string> input = inData.Split("\n").ToList();

            //foreach (string item in test)
            foreach (string item in input)
            {
                string str = item.Replace("turn off", "off");
                str = str.Replace("turn on", "on");

                string[] splitStr = str.Split(' ');
                string action = splitStr[0];
                int[] startArr = splitStr[1].Split(',').Select(x => int.Parse(x)).ToArray();
                int[] endArr = splitStr[3].Split(',').Select(x => int.Parse(x)).ToArray();

                for (int x = startArr[0]; x <= endArr[0]; x++)
                {
                    for (int y = startArr[1]; y <= endArr[1]; y++)
                    {
                        string index = $"{x}-{y}";
                        if (action == "on")
                        {
                            if (!lights.Contains(index)) lights.Add(index);
                        }
                        if (action == "off")
                        {
                            if (lights.Contains(index)) lights.Remove(index);
                        }
                        if (action == "toggle")
                        {
                            if (lights.Contains(index)) lights.Remove(index);
                            else lights.Add(index);
                        }
                    }
                }
            }
            return lights.Count;
        }

        private object Day2(string inData)
        {
            Dictionary<string, int> lights = new Dictionary<string, int>();

            List<string> input = inData.Split("\n").ToList();

            string[] test1 = { "turn on 0,0 through 999,999", "toggle 0,0 through 999,0", "turn off 499,499 through 500,500" };
            string[] test2 = { "turn on 0,0 through 0,0", "toggle 0,0 through 999,999" };

            //foreach (string item in test1)
            //foreach (string item in test2)
            foreach (string item in input)
            {
                string str = item.Replace("turn off", "off");
                str = str.Replace("turn on", "on");

                string[] splitStr = str.Split(' ');
                string action = splitStr[0];
                int[] startArr = splitStr[1].Split(',').Select(x => int.Parse(x)).ToArray();
                int[] endArr = splitStr[3].Split(',').Select(x => int.Parse(x)).ToArray();

                for (int x = startArr[0]; x <= endArr[0]; x++)
                {
                    for (int y = startArr[1]; y <= endArr[1]; y++)
                    {
                        string index = $"{x}-{y}";
                        if (action == "on")
                        {
                            if (!lights.ContainsKey(index)) lights.Add(index, 1);
                            else lights[index] += 1;
                        }
                        if (action == "off")
                        {
                            if (!lights.ContainsKey(index)) lights.Add(index, 0);
                            else if (lights[index] > 0) lights[index] -= 1;
                        }
                        if (action == "toggle")
                        {
                            if (!lights.ContainsKey(index)) lights.Add(index, 2);
                            else lights[index] += 2;
                        }
                    }
                }
            }
            return lights.Values.Sum();
        }
    }
}
