using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 13: Shuttle Search")]
    class Day13 : BaseLine, Solution
    {

        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<int> Day1(string inData)
        {
            List<string> list = inData.Split("\n").ToList();

            int time = int.Parse(list[0]);
            int selectedBusID = 0;
            int selectedWait = -100;

            string[] busses = list[1].Replace(",x", "").Split(',');

            for (int j = 0; j < busses.Count(); j++)
            {
                int busID = int.Parse(busses[j]);
                for (int i = 0; i < time * 2; i += busID)
                {
                    if (i >= time)
                    {
                        if (time - i > selectedWait)
                        {
                            selectedWait = time - i;
                            selectedBusID = busID;
                            break;
                        }
                    }
                }
            }
            //return (string.Format("Day 13/1: Bus ID * Minutes ({0} * {1}) wait {2}", selectedBusID, selectedWait, (Math.Abs(selectedWait) * selectedBusID)));

            yield return Math.Abs(selectedWait) * selectedBusID;
        }

        private IEnumerable<long> Day2(string inData)
        {
            List<string> list = inData.Split("\n").ToList();

            string[] sbusses = list[1].Split(',');
            bus[] myBusses = new bus[list[1].Replace(",x", "").Split(',').Count()];

            int ctr = 0;
            for (int i = 0; i < sbusses.Count(); i++)
            {
                if (sbusses[i] != "x")
                {
                    myBusses[ctr++] = new bus(int.Parse(sbusses[i]), i);
                }
            }

            long stepSize = myBusses[0].busID;
            long time = 0;
            for (int i = 1; i < myBusses.Count(); i++)
            {
                while ((time + myBusses[i].delay) % myBusses[i].busID != 0)
                {
                    time += stepSize;
                }
                stepSize *= myBusses[i].busID; // New Ratio!
            }
            
            //return "Day 13/2: The earliest timestamp for listed busses is " + time;

            yield return time;
        }
    }

    class bus
    {
        public int busID { get; }
        public int delay { get; }

        public bus(int idVal, int delayVal)
        {
            busID = idVal;
            delay = delayVal;
        }
    }
}
