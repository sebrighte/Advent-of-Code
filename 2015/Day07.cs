using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 7: Some Assembly Required")]
    class Day07 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input, false);
        public object PartTwo(string input) => Day1(input, true);

        Dictionary<string, ushort> values = new Dictionary<string, ushort>();

        int storedVal;

        private object Day1(string inData, bool useExisting = false)
        {
            //if (p2 != 0) values.Clear();
            List<string> input = inData.Split("\n").ToList();
            values.Clear();

            while (!values.ContainsKey("a"))
            {
                foreach (var itemList in input)
                {
                    string item = itemList;

                    if (useExisting && item == "1674 -> b")
                        item = $"{storedVal} -> b";

                    string[] str = item.Split(' ');

                    if (item.Contains("AND"))           //x AND y -> d
                        _and(str);
                    else if (item.Contains("OR"))       //x OR y -> e
                        _or(str);
                    else if (item.Contains("LSHIFT"))   //x LSHIFT 2 -> f
                        _lshift(str);
                    else if (item.Contains("RSHIFT"))   //y RSHIFT 2 -> g
                        _rshift(str);
                    else if (item.Contains("NOT"))      //lx = a
                        _not(str);
                    else
                        _other(str);
                }
            }
            storedVal = values["a"];
            return values["a"];
        }

        private void _and(string[] str)
        {
            if (str[0].IsNumber())
            {
                if (values.ContainsKey(str[2]))
                {
                    if (values.ContainsKey(str[4]))
                        values[str[4]] = Convert.ToUInt16((Convert.ToUInt16(str[0])) & values[str[2]]);
                    else
                    {
                        values.Add(str[4], Convert.ToUInt16((Convert.ToUInt16(str[0])) & values[str[2]]));
                    }
                }
            }
            else if (values.ContainsKey(str[0]) && values.ContainsKey(str[2]))
            {
                if (values.ContainsKey(str[4]))
                    values[str[4]] = Convert.ToUInt16(values[str[0]] & values[str[2]]);
                else
                {
                    values.Add(str[4], Convert.ToUInt16(values[str[0]] & values[str[2]]));
                }
            }
        }

        private void _or(string[] str)
        {
            if (values.ContainsKey(str[0]) && values.ContainsKey(str[2]))
            {
                if (values.ContainsKey(str[4]))
                    values[str[4]] = Convert.ToUInt16(values[str[0]] | values[str[2]]);
                else
                {
                    values.Add(str[4], Convert.ToUInt16(values[str[0]] | values[str[2]]));
                }
            }
        }

        private void _lshift(string[] str)
        {
            if (values.ContainsKey(str[0]))
            {
                if (values.ContainsKey(str[4]))
                    values[str[4]] = (UInt16)(values[str[0]] << (UInt16)Convert.ToUInt16(str[2]));
                else
                {
                    values.Add(str[4], (UInt16)(values[str[0]] << (UInt16)Convert.ToUInt16(str[2])));
                }
            }
        }

        private void _rshift(string[] str)
        {
            if (values.ContainsKey(str[0]))
            {
                if (values.ContainsKey(str[4]))
                    values[str[4]] = Convert.ToUInt16(values[str[0]] >> Convert.ToUInt16(str[2]));
                else
                {
                    values.Add(str[4], Convert.ToUInt16(values[str[0]] >> Convert.ToUInt16(str[2])));
                }
            }
        }

        private void _not(string[] str)
        {
            if (values.ContainsKey(str[1]))
            {
                if (values.ContainsKey(str[3]))
                    values[str[3]] = (UInt16)~values[str[1]];
                else
                {
                    values.Add(str[3], (UInt16)~values[str[1]]);
                }
            }
        }

        private void _other(string[] str)
        {
            if (str[0].IsNumber())
            {
                if (values.ContainsKey(str[2]))
                    values[str[2]] = Convert.ToUInt16(str[0]);
                else
                {
                    ushort val = (UInt16)Convert.ToUInt16(str[0]);
                    values.Add(str[2], val);
                    //lx -> a
                }
            }
            else
            {
                if (values.ContainsKey(str[0]))
                    if (!str[0].IsNumber() && !str[2].IsNumber())
                        values.Add(str[2], values[str[0]]);
            }
        }
    }
}
