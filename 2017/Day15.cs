using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day15: Dueling Generators")]
    class Day15 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver1(40000000, 634, 301).Count();
        //Note: Result found using 40m calls as problem states 5m (incorrct as only 51 matches vs 294)
        public object PartTwo(string input) => Solver2(40000000, 634, 301).Count();

        const long DIV = 2147483647;
        const int MASK = 65535; //1111 1111 1111 1111

        private IEnumerable<object> Solver1(int cycle, long genA, long genB)
        {
            for (int i = 0; i < cycle; i++)
            {
                genA = ((genA * 16807) % DIV);
                genB = ((genB * 48271) % DIV);

                if ((genA & MASK) == (genB & MASK))
                    yield return 1;
            }
        }

        private IEnumerable<object> Solver2(int cycle, long genA, long genB)
        {
            Queue<long> qGenA = new();
            Queue<long> qGenB = new();
            for (int i = 0; i < cycle; i++)
            {
                genA = ((genA * 16807) % DIV);
                genB = ((genB * 48271) % DIV);

                if (genA % 4 == 0) qGenA.Enqueue(genA & MASK);
                if (genB % 8 == 0) qGenB.Enqueue(genB & MASK);  
            }

            while (qGenA.Count > 0 && qGenB.Count > 0)
            {
                if (qGenA.Dequeue() == qGenB.Dequeue())
                    yield return 1;
            }
        }
    }
}
