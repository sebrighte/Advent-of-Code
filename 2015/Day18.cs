using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    class Lights
    {
        int gridSize;
        Dictionary<(int, int), char> dictGrid = new Dictionary<(int, int), char>();

        public int Count()
        {
            return dictGrid.Count(x => x.Value.Equals('#'));
        }

        public void Update(int loop = 1,  bool part2 = false)
        {
            for (int i = 0; i < loop; i++)
            {
                Dictionary<(int, int), char> updates = new Dictionary<(int, int), char>();

                int gridMax = dictGrid.Select(x => int.Parse(x.Key.Item1.ToString())).Max();
                int gridMin = dictGrid.Select(x => int.Parse(x.Key.Item1.ToString())).Min();

                if (part2)
                {
                    dictGrid[(gridMin, gridMin)] = '#';
                    dictGrid[(gridMin, gridMax)] = '#';
                    dictGrid[(gridMax, gridMax)] = '#';
                    dictGrid[(gridMax, gridMin)] = '#';
                }

                foreach (var light in dictGrid)
                {
                    int x = light.Key.Item1;
                    int y = light.Key.Item2;
                    int onctr = 0;
                    int offctr = 0;

                    for (int a = -1; a < 2; a++)
                    {
                        for (int b = -1; b < 2; b++)
                        {
                            if (!(a == 0 && b == 0))
                            {
                                dictGrid.TryGetValue((x + a, y + b), out char p);
                                if (dictGrid[(x, y)] == '#')
                                    if (p == '#') onctr++;
                                if (dictGrid[(x, y)] == '.')
                                    if (p == '#') offctr++;
                            }
                        }
                    }
                    if (!(onctr == 2 || onctr == 3))
                    {
                        if (dictGrid[(x, y)] == '#') updates.Add((x, y), '.');
                    }
                    if (offctr == 3)
                    {
                        if (dictGrid[(x, y)] == '.') updates.Add((x, y), '#');
                    }
                }

                foreach (var update in updates)
                {
                    dictGrid[(update.Key.Item1, update.Key.Item2)] = update.Value;
                }

                if (part2)
                {
                    dictGrid[(gridMin, gridMin)] = '#';
                    dictGrid[(gridMin, gridMax)] = '#';
                    dictGrid[(gridMax, gridMax)] = '#';
                    dictGrid[(gridMax, gridMin)] = '#';
                }
            }
        }

        public void DrawGrid()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    Console.Write(dictGrid[(x, y)]);
                }
                Console.WriteLine();
            }
        }
        
        public Lights(string inputData)
        {
            dictGrid.Clear();
            List<string> input = inputData.Split("\r\n").ToList();
            gridSize = input.Count();

            int x = 0;
            foreach (var item in input)
            {
                char[] chrLine = item.ToCharArray();
                {
                    for (int y = 0; y < gridSize; y++)
                    {
                        dictGrid.Add((x, y), chrLine[y]);
                    }
                }
                x++;
            }
        }
    }

    [ProblemName("Day 18: Like a GIF For Your Yard")]
    class Day18 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
           // string TestData = ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..";

            Lights lights = new Lights(inData);

            lights.Update(100, part2);

            yield return lights.Count();
        }
    }
}