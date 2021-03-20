using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 10: Balance Bots")]
    class Day10 : BaseLine, Solution
    {
        public object PartOne(string input) => solve(input, 61, 17).First();
        public object PartTwo(string input) => solve(input, 0, 0, true).First();

        private IEnumerable<object> solve(string inData, int maxChip, int minChip, bool part2 = false)
        {
            Bot[] Bots = new Bot[350];
            bool loopFirst = true;

            while (true)
            {
                foreach (var inst in inData.Split("\r\n").ToList())
                {
                    if (inst.Substring(0, 3) == "val" && loopFirst)
                    {
                        var match = Regex.Match(inst, @"value (\d*) goes to bot (\d*)");
                        var (botName, procvalue) = (match.Groups[2].Value.ToInt32(), match.Groups[1].Value.ToInt32());

                        if (Bots[botName] is null)
                            Bots[botName] = new Bot(botName, procvalue);
                        else
                            Bots[botName].AddValue(procvalue);
                    }

                    else if (inst.Substring(0, 3) == "bot" && !loopFirst)
                    {
                        var match = Regex.Match(inst, @"bot (\d*) gives low to (.*) (\d*) and high to (.*) (\d*)");
                        int botFrom = match.Groups[1].Value.ToInt32();
                        string outLo = match.Groups[2].Value;
                        string outHi = match.Groups[4].Value;
                        int botLow = match.Groups[3].Value.ToInt32();
                        int botHigh = match.Groups[5].Value.ToInt32();

                        if (Bots[300] is not null && Bots[301] is not null && Bots[302] is not null)
                            if (part2) yield return Bots[300].lowerValue * Bots[301].lowerValue * Bots[302].lowerValue;

                        if (outLo == "output") botLow += 300;
                        if (outHi == "output") botHigh += 300;

                        if (Bots[botFrom] is null)
                        {
                            Bots[botFrom] = new Bot(botFrom, 0);
                        }
                        else
                        {
                            if (Bots[botFrom].Has2Chips() == true)
                            {
                                if (Bots[botLow] is null)
                                    Bots[botLow] = new Bot(botLow, Bots[botFrom].GetLoValue());
                                else
                                    Bots[botLow].AddValue(Bots[botFrom].GetLoValue());

                                if (Bots[botHigh] is null)
                                    Bots[botHigh] = new Bot(botHigh, Bots[botFrom].GetHiValue());
                                else
                                    Bots[botHigh].AddValue(Bots[botFrom].GetHiValue());
                            }
                        }
                    }
                }
                loopFirst = false;
                foreach (var item in Bots)
                {
                    if (item is not null)
                        if (item.lowerValue == minChip && item.higherValue == maxChip)
                        {
                            if (!part2) yield return item.arrNo;
                        }
                }
            }
        }
    }

    class Bot
    {
        public int arrNo;
        public int higherValue;
        public int lowerValue;
        public bool has2Values;

        public Bot(int arn, int lv)
        {
            arrNo = arn;
            higherValue = 0;
            lowerValue = lv;
            has2Values = false;
        }

        public bool Has2Chips()
        {
            return (higherValue > 0 && lowerValue > 0) ? true : false;
        }

        public int GetHiValue(bool delete = true)
        {
            int tmp = higherValue;
            if (delete) higherValue = 0;
            return tmp;
        }

        public int GetLoValue(bool delete = true)
        {
            int tmp = lowerValue;
            if (delete) lowerValue = 0;
            return tmp;
        }

        public void AddValue(int value)
        {
            if (higherValue == 0 && lowerValue == 0)
                lowerValue = value;
            else if (higherValue != 0 && lowerValue == 0)
            {
                if (value > higherValue)
                {
                    lowerValue = higherValue;
                    higherValue = value;
                }
                else
                    lowerValue = value;
            }
            else if (higherValue == 0 && lowerValue != 0)
            {
                if (value < lowerValue)
                {
                    higherValue = lowerValue;
                    lowerValue = value;
                }
                else
                    higherValue = value;
            }
        }
    }
}
