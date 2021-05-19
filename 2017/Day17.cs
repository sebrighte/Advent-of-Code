using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2017
{
    [ProblemName("Day17: Spinlock")]
    //[ProblemName("Day17: ...@TEST@")]
    class Day17 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver1(2017, 386).Max();
        public object PartTwo(string input) => Solver2(50000000, 386).Max();

        private static IEnumerable<object> Solver1(int loop, int skip)
        {
            LinkedList<int> Spinlock = new();
            int curentPos = 0;
            int curentVal = 0;
            Spinlock.AddFirst(curentVal++);
            var currentNode = Spinlock.NodeAt(curentPos);

            for (int r = 0; r < loop; r++)
            {
                for (int i = 0; i < skip; i++)
                {
                    if (curentPos == Spinlock.Count - 1)
                        curentPos = 0;
                    else
                        curentPos++;
                }
                currentNode = Spinlock.NodeAt(curentPos++);
                currentNode = Spinlock.AddAfter(currentNode, curentVal++);
                yield return Spinlock.NodeAt(Spinlock.IndexOf(2017) + 1).Value;
            }
        }

        private static IEnumerable<object> Solver2(int loop, int skip)
        {
            int pos = 0;
            for (int r = 1; r < loop; r++)
            {
                pos = ((pos + skip) % r) + 1;
                if (pos == 1)
                {
                    yield return r;
                }
            }
        }
    }
}
