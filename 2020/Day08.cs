using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 8: Handheld Halting")]
    class Day08 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            List<string> list = inData.Split("\n").ToList();

            //Console.WriteLine("\nDay 8: Handheld Halting\n");

            List<string> history = new List<string>(0);
            runCode(list, history);

            //Console.WriteLine("Day 8/1: Accumulator value at loop is " + history[history.Count - 1].Split(',')[1]);
            if(!part2)
             yield return int.Parse(history[history.Count - 1].Split(',')[1]);

            int retValStored = 0;

            for (int x = 0; x < history.Count; x++)
            {
                List<string> editiedlList = new List<string>(list);
                List<string> history2 = new List<string>(0);

                var v = history[x].Split(',');

                int ipos = int.Parse(v[0]);
                int iacc = int.Parse(v[1]);
                string iins = v[2];

                string ins = iins.Substring(0, 3);

                int retVal = 0;

                switch (ins)
                {
                    case "jmp":
                        editiedlList[ipos] = editiedlList[ipos].Replace("jmp", "nop");
                        retVal = runCode(editiedlList, history2);
                        if (retVal > retValStored) retValStored = retVal;
                        break;
                    case "nop":
                        editiedlList[ipos] = editiedlList[ipos].Replace("nop", "jmp");
                        retVal = runCode(editiedlList, history2);
                        if (retVal > retValStored) retValStored = retVal;
                        break;
                }
            }
            //Console.WriteLine("Day 8/2: Accumulator value at corrected instruction is " + retValStored + "\n");
            if (part2)
                yield return retValStored;
        }

        public static int runCode(List<string> list, List<string> instHistory)
        {
            int acc = 0;
            int pos = 0;
            bool end = false;
            string instruction = "";
            List<int> history = new List<int>(0);
            string lastInst = "";

            while (!end)
            {
                //parse instruction 
                if (pos == list.Count)
                    return acc;

                instruction = list[pos];
                string ins = list[pos].Substring(0, 3);
                string sym = list[pos].Substring(4, 1);
                int num = int.Parse(list[pos].Substring(5));

                lastInst = string.Format("{0},{1},{2}", pos, acc, instruction);

                instHistory.Add(lastInst);

                //lastInst = "\nLast Instruction: '" + instruction + "' at line " + (pos + 1) + "\n";

                switch (ins)
                {
                    case "acc":
                        if (sym == "+") acc = acc + num;
                        else if (sym == "-") acc = acc - num;
                        pos++;
                        break;
                    case "jmp":
                        if (sym == "+") pos = pos + num;
                        else if (sym == "-") pos = pos - num;
                        break;
                    case "nop":
                        pos++;
                        break;
                }

                for (int i = 0; i < history.Count; i++)
                {
                    if (history[i] == pos) end = true;
                    //Console.WriteLine(acc);
                }

                history.Add(pos);

            }

            for (int x = 0; x < history.Count; x++)
            {
                //Console.WriteLine(x + ":" + history[x]);
            }

            return 0;

        }
    }
}
