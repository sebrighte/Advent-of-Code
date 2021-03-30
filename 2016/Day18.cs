using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 18: Like a Rogue")]
    class Day18 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input, 40).First();
        public object PartTwo(string input) => Day1(input, 400000).First();

        StringBuilder sb = new StringBuilder();

        private IEnumerable<object> Day1(string nextrow, int rows)
        {
            //nextrow = ".^^.^.^^^^";
            //rows = 10;

            yield return RowCalc(nextrow).Take(rows).Sum();
        }

        private IEnumerable<int> RowCalc(string currentRow)
        {
            yield return currentRow.Where(a => a == '.').Count();

            while (true)
            {
                sb.Clear();
                string rowPart;

                for (int i = 0; i < currentRow.Length; i++)
                {
                    if (i == 0)
                        rowPart = $".{currentRow[i]}{currentRow[i + 1]}";
                    else if (i == currentRow.Length - 1)
                        rowPart = $"{currentRow[i - 1]}{currentRow[i]}.";
                    else
                        rowPart = $"{currentRow[i - 1]}{currentRow[i]}{currentRow[i + 1]}";

                    if (rowPart.Equals(@"^^.") || rowPart.Equals(@".^^") || rowPart.Equals(@"^..") || rowPart.Equals(@"..^"))
                        sb.Append('^');
                    else
                        sb.Append('.');
                }
                currentRow = sb.ToString();
                yield return currentRow.Where(a => a == '.').Count();
            }
        }
    }
}
