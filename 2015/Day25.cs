using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 25: Let It Snow")]
    class Day25 : BaseLine, Solution
    {
        //To continue, please consult the code grid in the manual.  Enter the code at row 2981, column 3075.

        public object PartOne(string input) => Day3(3075, 2981).Last(); //9132360
        public object PartTwo(string input) => null; // Day1(input, true).First();


        public IEnumerable<long> Day3(int col, int row)
        {
            int colVal = 0;
            int rowVal = col;

            colVal = ((col * (col + 1)) / 2);

            for (int i = 0; i < row-1; i++)
            {
                colVal += rowVal+i;
            }

            long acc = 20151125;

            for (long i = 1; i < colVal; i++)
            {
                acc = (acc * 252533) % 33554393;
                yield return acc;
            }
        }

        public IEnumerable<long> Day3_2(int icolDst, int irowDst)
        {
            var m = 20151125L;
            var (irow, icol) = (1, 1);
            while (irow != irowDst || icol != icolDst)
            {
                irow--;
                icol++;
                if (irow == 0)
                {
                    irow = icol;
                    icol = 1;
                }
                m = (m * 252533L) % 33554393L;
            }
            yield return m;
        }
    }
}
