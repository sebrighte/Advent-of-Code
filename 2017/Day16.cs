using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day16: Permutation Promenade")]
    //[ProblemName("Day16: ...@TEST@")]
    class Day16 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver1(input).First();
        public object PartTwo(string input) => Solver2(input, 1000000000).First();

        private IEnumerable<object> Solver1(string inData)
        {
            LinkedList<char> dance = new();
            for (char c = 'a'; c <= 'p'; c++) dance.AddLast(c);
            while (true)
            {
                foreach (var item in inData.Split(",").ToList())
                {
                    switch (item[0])
                    {
                        case 's':
                            Spin(dance, item[1..].ToInt32());
                            break;
                        case 'x':
                            SwapNumber(dance, item[1..].Split('/')[0].ToInt32(), item[1..].Split('/')[1].ToInt32());
                            break;
                        case 'p':
                            SwapLetter(dance, item[1..2][0], item[3..4][0]);
                            break; ;
                    }
                }
                yield return string.Join("", dance);
            }
        }

        private IEnumerable<object> Solver2(string inData, long loop)
        {
            int skipVal = (int) loop % 30;
            foreach (var item in Solver1(inData).Skip(--skipVal))
            {
                yield return item.ToString();
            }
        }

        private void Spin(LinkedList<char> dance, int spins)
        {
            for (int i = 0; i < spins; i++)
            {
                var tmp = dance.Last.Value;
                dance.RemoveLast();
                dance.AddFirst(tmp);
            }
        }

        private void SwapLetter(LinkedList<char> dance, char a, char b) 
        {
            SwapNumber(dance, dance.IndexOf(a), dance.IndexOf(b));
        }

        private void SwapNumber(LinkedList<char> dance, int a, int b) 
        {
            var swap1 = dance.NodeAt(a);
            var swap2 = dance.NodeAt(b);
            var fill = new LinkedListNode<char>('x');

            dance.AddAfter(swap2, fill);
            dance.Remove(swap2);
            dance.AddAfter(swap1, swap2);
            dance.Remove(swap1);
            dance.AddAfter(fill, swap1);
            dance.Remove(fill);
        }
    }
}
