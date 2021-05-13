using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day10: Knot Hash")]
    class Day10 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver1().First();
        public object PartTwo(string input) => Solver2().First();

        private IEnumerable<object> Solver1()
        {
            string inData = "120,93,0,90,5,80,129,74,1,165,204,255,254,2,50,113";
            int ropeLen = 256;
            
            List<int> rope = Enumerable.Range(0, ropeLen).ToList();
            int pos = 0;
            int skip = 0;
            rope = Hash(rope, inData, ref pos, ref skip);
            yield return $"{rope[0] * rope[1]}";
        }

        private IEnumerable<object> Solver2()
        {
            string inData = "120,93,0,90,5,80,129,74,1,165,204,255,254,2,50,113";
            int ropeLen = 256;
            int offset = 0;
            int skip = 0;
            string hexValue = "";
            List<int> rope = Enumerable.Range(0, ropeLen).ToList();

            inData = string.Join(",", inData.Select(a => (int)a).ToArray()) + ",17,31,73,47,23";

            for (int i = 0; i < 64; i++)
                rope = Hash(rope, inData, ref offset, ref skip);

            for (int i = 0; i < 16; i++){
                int value = 0;
                var tmpRope = rope.Skip(i*16).Take(16);
                foreach (var (val, index) in tmpRope.WithIndex())
                    value = index == 0 ? val : value ^ val;
                hexValue += value.ToString("X").PadLeft(2, '0');
            }
            yield return $"{hexValue.ToLower()}";
        }

        private List<int> Hash (List<int> rope, string inData, ref int pos, ref int skip)
        {
            List<int> input = inData.Split(",").Select(a => int.Parse(a)).ToList();

            foreach (var length in input)
            {
                if (pos >= rope.Count) pos = pos % rope.Count();
                if (skip >= rope.Count) skip = skip % rope.Count();

                var tmp = rope.Take(pos).ToArray();
                rope.RemoveRange(0, pos);
                rope.AddRange(tmp);

                var tmp2 = rope.Take(length).ToArray().Reverse();
                rope.RemoveRange(0, length);
                rope.InsertRange(0, tmp2);

                for (int i = 0; i < pos; i++)
                {
                    var tmp3 = rope[rope.Count - 1];
                    rope.RemoveAt(rope.Count - 1);
                    rope.Insert(0, tmp3);
                }
                pos += length + skip++;
            }
            return rope;
        }
    }
}
