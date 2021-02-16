using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 15: Rambunctious Recitation")]
    class Day15 : BaseLine, Solution
    {
        public object PartOne(string input) => Play("input", 2020).First();
        public object PartTwo(string input) => Play("input", 30000000).First();

        struct shout
        {
            public int lastTurn;
            public bool exists;
        }

        private IEnumerable<int> Play(string inData, int numberSpoken, bool part2 = false)
        {
            int[] input = { 20, 0, 1, 11, 6, 3 };
            int ctr = 0;

            shout[] arr = new shout[numberSpoken];

            for (int y = 0; y < input.Count() - 1; y++)
            {
                arr[input[y]].lastTurn = ctr++;
                arr[input[y]].exists = true;
            }

            int nextNumber = input[input.Count() - 1];
            int lastNumber = -1;

            while (ctr < numberSpoken)
            {
                lastNumber = nextNumber;

                if (arr[nextNumber].exists == false)
                {
                    arr[nextNumber].lastTurn = ctr;
                    arr[nextNumber].exists = true;
                    nextNumber = 0;
                }
                else
                {
                    int tmp = ctr - arr[nextNumber].lastTurn;
                    arr[nextNumber].lastTurn = ctr;
                    nextNumber = tmp;
                }
                ctr++;

                //if (ctr % 1000000 == 0 || ctr == 10 || ctr == numberSpoken) DrawTextProgressBar(ctr+2, numberSpoken, true);
            }

            //System.Threading.Thread.Sleep(500);
            //stopwatch.Stop();
            //Console.WriteLine("(" + stopwatch.ElapsedMilliseconds + "ms)\n");

            //return lastNumber;
            //List<string> input = inData.Split("\r\n").ToList();
            yield return lastNumber;
        }
    }
}
