using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day3: Spiral Memory")]
    class Day03 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(325489).First();
        public object PartTwo(string input) => Day2(325489).First();

        private IEnumerable<object> Day1(int value)
        {
            int mult = 0;
            int index = 1;

            //Get bottom right corner (BRC)
            while (mult < value)
            {
                mult = index * index;
                index += 2;
            }

            //Relate value to BRC
            int btm = mult - ((int)Math.Sqrt(mult)-1);
            while(value < btm)
            {
                value += ((int)Math.Sqrt(mult) - 1);
            }

            //Calc path
            int halfSqrt = (int)(Math.Sqrt(mult) - 1) / 2;
            int valDiff = (int)(mult - halfSqrt);
            int pathDist = (int)(Math.Abs(valDiff - value) + halfSqrt);

            yield return pathDist;
        }

        private IEnumerable<object> Day2(int value)
        {
            Dictionary<string, int> grid = new Dictionary<string, int>();
            int x = 0;
            int y = 0;

            grid.Add($"{x}@{y}", 1); //initial

            int gridSize = 3; //inital spiral size 3x3(9)

            while(true)
            {
                int max = 0 + (gridSize / 2);
                int min = 0 - (gridSize / 2);
                int result = 0;

                //next spiral start
                result = GetValues(grid, x, ++y);
                if (result > value) yield return result;
                grid.Add($"{x}@{y}", result);

                while (x <= max - 1)//up
                {
                    result = GetValues(grid, ++x, y);
                    if (result > value) yield return result;
                    grid.Add($"{x}@{y}", result);
                }
                while (y >= min + 1)//left
                {
                    result = GetValues(grid, x, --y); 
                    if (result > value) yield return result;
                    grid.Add($"{x}@{y}", result);
                }
                while (x >= min + 1)//down
                {
                    result = GetValues(grid, --x, y); 
                    if (result > value) yield return result;
                    grid.Add($"{x}@{y}", result);
                }
                while (y <= max - 1)//right
                {
                    result = GetValues(grid, x, ++y); 
                    if (result > value) yield return result;
                    grid.Add($"{x}@{y}", result);
                }
                gridSize += 2;
            }  
        }

        private int GetValues(Dictionary<string, int> grid, int x, int y)
        {
            int retval = 0;
            if (grid.ContainsKey($"{x    }@{y + 1}")) retval += grid[$"{x    }@{y + 1}"];   //right
            if (grid.ContainsKey($"{x + 1}@{y + 1}")) retval += grid[$"{x + 1}@{y + 1}"];   //top/right
            if (grid.ContainsKey($"{x + 1}@{y    }")) retval += grid[$"{x + 1}@{y    }"];   //top
            if (grid.ContainsKey($"{x + 1}@{y - 1}")) retval += grid[$"{x + 1}@{y - 1}"];   //top/left
            if (grid.ContainsKey($"{x    }@{y - 1}")) retval += grid[$"{x    }@{y - 1}"];   //left
            if (grid.ContainsKey($"{x - 1}@{y - 1}")) retval += grid[$"{x - 1}@{y - 1}"];   //bottom/left
            if (grid.ContainsKey($"{x - 1}@{y    }")) retval += grid[$"{x - 1}@{y    }"];   //bottom
            if (grid.ContainsKey($"{x - 1}@{y + 1}")) retval += grid[$"{x - 1}@{y + 1}"];   //bottom/right
            return retval;
        }
    }
}
