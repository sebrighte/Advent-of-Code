using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 14: Docking Data")]
    class Day14 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input, true).First();

        private static long[] binValues = { 0, 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024,
            2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576, 2097152,
            4194304, 8388608, 16777216, 33554432, 67108864, 134217728, 268435456, 536870912,
            1073741824, 2147483648, 4294967296, 8589934592, 17179869184, 34359738368 };

        private IEnumerable<long> Day1(string inData, bool part2 = false)
        {
            Dictionary<long, long> mem = new Dictionary<long, long>();

            List<string> list = inData.Split("\n").ToList();

            string mask = "";

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Substring(0, 4) == "mask")
                    mask = list[i].Substring(list[i].IndexOf('=') + 2);
                else
                {
                    int loc = int.Parse(list[i].Substring(4, list[i].IndexOf(']') - 4));
                    long intVal = int.Parse(list[i].Substring(list[i].IndexOf('=') + 1));
                    long retVal = ApplyMask(mask, intVal);

                    if (mem.ContainsKey(loc))
                        mem[loc] = retVal;
                    else
                        mem.Add(loc, retVal);
                }
            }
            long sum = 0;
            foreach (var m in mem)
                sum += m.Value;

            yield return sum;
        }

        private IEnumerable<long> Day2(string inData, bool part2 = false)
        {
            Dictionary<long, long> mem = new Dictionary<long, long>();

            List<string> list = inData.Split("\n").ToList();

            string mask = "";

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Substring(0, 4) == "mask")
                    mask = list[i].Substring(list[i].IndexOf('=') + 2);
                else
                {
                    long loc = int.Parse(list[i].Substring(4, list[i].IndexOf(']') - 4));
                    long intVal = int.Parse(list[i].Substring(list[i].IndexOf('=') + 1));

                    char[] m = mask.ToCharArray();
                    char[] v = Convert.ToString(loc, 2).PadLeft(36, '0').ToCharArray();

                    for (int i2 = 0; i2 < m.Length; i2++)
                    {
                        if (m[i2] == 'X' || m[i2] == '1') v[i2] = m[i2];
                    }

                    var addresses = GetAddresses(new string(v));

                    foreach (var add in addresses)
                    {
                        if (mem.ContainsKey(add))
                            mem[add] = intVal;
                        else
                            mem.Add(add, intVal);
                    }
                }
            }
            long sum = 0;
            foreach (var m in mem)
                sum += m.Value;

            yield return sum;
        }

        private static List<long> GetAddresses(string address)
        {
            List<long> addresses = new List<long>(0);

            address = ReverseStringStr(address);

            int noXs = address.Count(a => a.Equals('X'));

            string mask2 = address;
            long[] xs = new long[noXs + 1];

            int ctr = 1;
            for (int i = 0; i < address.Length; i++)
            {
                if (address.Substring(i, 1) == "X") xs[ctr++] = binValues[i + 1];
            }

            address = address.Replace("X", "0");
            long tempLng = ReverseStringLng(address);

            long[] masks = new long[noXs];


            for (int mask = 0; mask < (1 << noXs); mask++)
            {
                for (int i = 0; i < noXs; i++)
                {
                    {
                        long id = mask & 1 << i;
                        id = Array.IndexOf(binValues, id);
                        masks[i] = id;
                    }
                }

                long h = tempLng;
                for (int y = 0; y < masks.Count(); y++)
                {
                    h |= xs[masks[y]];
                }
                addresses.Add(h);
            }
            return addresses;
        }

        private static long ReverseStringLng(string orig)
        {
            char[] charArray = orig.ToCharArray();
            Array.Reverse(charArray);
            string tmp = new string(charArray);
            return Convert.ToInt64(tmp, 2);
        }

        private static string ReverseStringStr(string orig)
        {
            char[] charArray = orig.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private long ApplyMask(string mask, long val)
        {
            char[] m = mask.ToCharArray();
            char[] v = Convert.ToString(val, 2).PadLeft(36, '0').ToCharArray();
            for (int i = 0; i < m.Length; i++)
            {
                if (m[i] != 'X') v[i] = m[i];
            }
            return Convert.ToInt64(new string(v), 2);
        }
    }
    class memory
    {
        public long address { get; set; }
        public long value { get; set; }

        public memory(long add, long Val)
        {
            this.address = add;
            this.value = Val;
        }
    }
}

