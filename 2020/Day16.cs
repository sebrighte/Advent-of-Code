using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 16: Ticket Translation")]
    class Day16 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        List<ticket> tickets;
        List<ticketRule> ticketRules;

        private IEnumerable<int> Day1(string inData)
        {
            List<string> list = inData.Split("\r\n").ToList();
            List<ticketRule> tr = new List<ticketRule>();

            int result = 0;
            int result2 = 0;
            int section = 1;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == "") section++;

                if (section == 1)
                {
                    ticketRule tic = new ticketRule();
                    tic.name = list[i].Split(':')[0];
                    tic.val1min = int.Parse(list[i].Split(':')[1].Split(' ')[1].Split('-')[0]);
                    tic.val1max = int.Parse(list[i].Split(':')[1].Split(' ')[1].Split('-')[1]);
                    tic.val2min = int.Parse(list[i].Split(':')[1].Split(' ')[3].Split('-')[0]);
                    tic.val2max = int.Parse(list[i].Split(':')[1].Split(' ')[3].Split('-')[1]);
                    tr.Add(tic);
                }

                if (section == 2)
                {
                    if (list[i] != "")
                    {
                        //do nothing for now
                    }
                }

                if (section == 3)
                {
                    if (list[i] != "")
                    {
                        if (!list[i].Contains("nearby tickets:"))
                        {
                            string[] values = list[i].Split(',');
                            foreach (string str in values)
                            {
                                bool isValid = false;

                                foreach (ticketRule ticket in tr)
                                {
                                    if (ticket.checkValues(int.Parse(str)))
                                    {
                                        isValid = true;
                                    }
                                }
                                if (!isValid)
                                {
                                    result += int.Parse(str);
                                    result2++;
                                }
                            }
                        }
                    }
                }
            }
            yield return result;
        }

        private IEnumerable<long> Day2(string inData)
        {
            List<string> list = inData.Split("\r\n").ToList();

            ticket Myticket = new ticket();

            ticketRules = new List<ticketRule>();
            tickets = new List<ticket>();

            long solResult = 1;
            int section = 1;
            int result = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == "") section++;

                //Read in rules
                if (section == 1)
                {
                    readRules(list[i], ticketRules);
                }

                //Read in my ticket
                if (section == 2)
                {
                    Myticket = readMyTicket(list[i], tickets);
                }

                //Read in tickets
                if (section == 3)
                {
                    readTickets(list[i], tickets);
                }
            }

            //Validate tickets (Part1)
            foreach (ticket t in tickets)
            {
                t.isVaid = true;

                foreach (int i in t.ticketValues)
                {
                    bool isValid = false;

                    foreach (ticketRule ticket in ticketRules)
                    {
                        if (ticket.checkValues(i))
                        {
                            isValid = true;
                        }
                    }
                    if (!isValid)
                    {
                        result += i;
                        t.isVaid = false;
                    }
                }
            }

            //Remove invalid tickets
            for (int r = tickets.Count - 1; r > 0; r--)
            {
                if (!tickets[r].isVaid)
                    tickets.Remove(tickets[r]);
            }

            //List per section
            int[] matchedRules = new int[ticketRules.Count];

            GetValidRules(matchedRules);

            //use -1 as flag so replace
            matchedRules[Array.FindIndex(matchedRules, a => a.Equals(-1))] = 0;

            for (int g = 0; g < 6; g++)
            {
                int arrayVal = Array.FindIndex(matchedRules, a => a.Equals(g));
                solResult *= Myticket.ticketValues[arrayVal];
            }
            yield return solResult;
        }

        private void readRules(string listItem, List<ticketRule> tr)
        {
            ticketRule tic = new ticketRule();
            tic.name = listItem.Split(':')[0];
            tic.val1min = int.Parse(listItem.Split(':')[1].Split(' ')[1].Split('-')[0]);
            tic.val1max = int.Parse(listItem.Split(':')[1].Split(' ')[1].Split('-')[1]);
            tic.val2min = int.Parse(listItem.Split(':')[1].Split(' ')[3].Split('-')[0]);
            tic.val2max = int.Parse(listItem.Split(':')[1].Split(' ')[3].Split('-')[1]);
            tr.Add(tic);
        }

        private void readTickets(string listItem, List<ticket> tickets)
        {
            if (!listItem.Contains("nearby tickets:") && !listItem.Contains("your ticket:") && listItem != "")
            {
                string[] values = listItem.Split(',');
                int[] tv = new int[values.Count()];
                for (int v = 0; v < values.Count(); v++)
                {
                    tv[v] = int.Parse(values[v]);
                }
                ticket t = new ticket();
                t.ticketValues = tv;
                t.isVaid = true;
                tickets.Add(t);
            }
        }

        private ticket readMyTicket(string listItem, List<ticket> tickets)
        {
            ticket t = new ticket();

            if (!listItem.Contains("nearby tickets:") && !listItem.Contains("your ticket:") && listItem != "")
            {
                string[] values = listItem.Split(',');
                int[] tv = new int[values.Count()];
                for (int v = 0; v < values.Count(); v++)
                {
                    tv[v] = int.Parse(values[v]);
                }
                t.ticketValues = tv;
                t.isVaid = true;
            }
            return t;
        }

        public int GetValidRules(int[] matchedRules)
        {
            int retval = 0;
            int colCtr = 0;
            int colVal = 0;
            int tickValCtr = 0;

            for (int ruleCtr = 0; ruleCtr < ticketRules.Count; ruleCtr++)
            {
                {
                    retval = 0;
                    for (colCtr = 0; colCtr < ticketRules.Count; colCtr++)
                    {
                        if (matchedRules[colCtr] == 0)
                        {
                            int[] arr = new int[tickets.Count];
                            for (tickValCtr = 0; tickValCtr < tickets.Count; tickValCtr++)
                            {
                                int d = tickets[tickValCtr].ticketValues[colCtr];
                                arr[tickValCtr] = d;
                            }
                            if (ticketRules[ruleCtr].checkValues(arr))
                            {
                                retval++;
                                colVal = colCtr;
                            }
                        }
                    }
                    if (retval == 1)
                    {
                        matchedRules[colVal] = ruleCtr;
                        if (ruleCtr == 0) matchedRules[colVal] = -1;
                        GetValidRules(matchedRules);
                    }
                }
            }
            return retval;
        }

    }

    class ticketRule
    {
        public string name;
        public int val1min;
        public int val1max;
        public int val2min;
        public int val2max;
        //public int totalValidIndex = 0;

        public bool checkValues(int value)
        {
            if ((value >= val1min && value <= val1max) || (value >= val2min && value <= val2max)) return true;
            else return false;
        }

        public bool checkValues(int[] value)
        {
            bool valid = true;
            for (int a = 0; a < value.Count(); a++)
            {
                if (!this.checkValues(value[a]))
                    if (valid == true) valid = false;
            }
            return valid;
        }

        public bool checkValues(string value)
        {
            var vVal = value.Split(',');
            bool valid = true;
            for (int a = 0; a < vVal.Count(); a++)
            {
                if (!this.checkValues(vVal[a]))
                    if (valid == true) valid = false;
            }
            return valid;
        }
    }

    class ticket
    {
        public int[] ticketValues;
        public bool isVaid;
    }

}
