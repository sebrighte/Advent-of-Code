using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day14: Disk Defragmentation ---")]
    //[ProblemName("Day14: ...@TEST@")]
    class Day14 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver().First();
        public object PartTwo(string input) => Solver().Skip(1).First();

        private IEnumerable<object> Solver()
        {
            HashSet<string> oF = new();
            string seed = "amgozmfv";
            //seed = "flqrgnkx";
            List<string> hardDrive = new();
            for (int i = 0; i < 128; i++)
            {
                string binVal = "";
                foreach (var c in Day10.KnotEncrypt($"{seed}-{i}"))
                {
                    binVal += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
                }
                hardDrive.Add(binVal.Replace("1", "#").Replace("0", "-"));
            }

            yield return $"{string.Join("", hardDrive).Where(a => a == '#').Count()}";

            int groups = 0;
            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    if (hardDrive[x][y] == '#' && !oF.Contains($"{x}@{y}"))
                    {
                        findUsed(oF, hardDrive, x, y, '#');
                        groups++;
                    }
                }
            }
            yield return $"{groups}";
        }

        private void findUsed(HashSet<string> oF, List<string> hd, int x, int y, char c)
        {
            List<string> found = new();
            if (!oF.Contains($"{x}@{y}")) oF.Add($"{x}@{y}");

            if (x + 1 < 128 && hd[x + 1][y] == c) { found.Add($"{x + 1}@{y}"); }
            if (y + 1 < 128 && hd[x][y + 1] == c) { found.Add($"{x}@{y + 1}"); }
            if (x - 1 >= 0 && hd[x - 1][y] == c) { found.Add($"{x - 1}@{y}"); }
            if (y - 1 >= 0 && hd[x][y - 1] == c) { found.Add($"{x}@{y - 1}"); }

            foreach (var item in found)
            {
                int xPass = item.Split("@")[0].ToInt32();
                int yPass = item.Split("@")[1].ToInt32();
                if (!oF.Contains($"{xPass}@{yPass}"))
                    findUsed(oF, hd, xPass, yPass, c);
            }
        }
    }
}
