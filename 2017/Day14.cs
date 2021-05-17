using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day14: Disk Defragmentation ---")]
    class Day14 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver().First();
        public object PartTwo(string input) => Solver().Skip(1).First();

        List<string> hardDrive = new();

        private IEnumerable<object> Solver()
        {
            HashSet<string> inspected = new();
            string seed = "amgozmfv";
            //seed = "flqrgnkx";
            if (hardDrive.Count == 0)
            {
                for (int i = 0; i < 128; i++)
                {
                    string binVal = "";
                    foreach (var c in Day10.KnotEncrypt($"{seed}-{i}"))
                    {
                        binVal += Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0');
                    }
                    hardDrive.Add(binVal);//.Replace("1", "#").Replace("0", "-"));
                }
            }

            yield return $"{string.Join("", hardDrive).Where(a => a == '1').Count()}";

            int groups = 0;
            for (int x = 0; x < hardDrive.Count(); x++)
            {
                for (int y = 0; y < hardDrive[0].Length; y++)
                {
                    if (hardDrive[x][y] == '1' && !inspected.Contains($"{x}@{y}"))
                    {
                        findGroup(inspected, hardDrive, x, y, '1');
                        groups++;
                    }
                }
            }
            yield return $"{groups}";
        }

        private void findGroup(HashSet<string> oF, List<string> hd, int x, int y, char c)
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
                    findGroup(oF, hd, xPass, yPass, c);
            }
        }
    }
}
