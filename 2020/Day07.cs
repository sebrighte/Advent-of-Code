using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 7: Handy Haversacks")]
    class Day07 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            List<string> list = inData.Split("\r\n").ToList();
            List<string> holdingBagsDict = new List<string>(0);
            List<string> holdingBagsFnd = new List<string>(0);
            List<string> tree = new List<string>(0);

            holdingBagsFnd.Add("shiny gold bags");
            getBagsRecurse(holdingBagsFnd, holdingBagsDict, list);
            holdingBagsDict = holdingBagsDict.Distinct().ToList();
            //Console.WriteLine("Day 7/1: No of shiny gold bag combos is " + holdingBagsDict.Count);

            int total = 0;
            getBagCountRecursive(tree, list, "shiny gold bags contain", 1, 0, 0, ref total);
            //Console.WriteLine("Day 7/2: No of individual bags required is " + total + "\n");
            //List<string> input = inData.Split("\n").ToList();
            if(!part2)
                yield return holdingBagsDict.Count;
            else
                yield return total;
        }

        private static bool isemptyBag(List<string> list, string bagname)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Contains(bagname)) && (list[i].LastIndexOf("no other bags") > -1))
                {
                    return true;
                }
            }
            return false;
        }

        private static void getBagCountRecursive(List<string> tree, List<string> list, string seed, int multiple, int prevMultiple, int bags, ref int total)
        {
            //list for all entries in file
            for (int i = 0; i < list.Count; i++)
            {
                //search for containing bag
                if (list[i].Contains(seed))
                {
                    //format the returned row and split into string array
                    int q = list[i].LastIndexOf("contain");
                    string sub = list[i].Substring(q + 8);
                    string[] contentsArray = sub.Split(',');

                    //Loop through all the containing bags
                    for (int p = 0; p < contentsArray.Count(); p++)
                    {
                        //If the first loop save the original multiple
                        string line = contentsArray[p];
                        if (p > 0) multiple = prevMultiple;

                        if (!isemptyBag(list, line))
                        {
                            //check as sometimes an extra space, if so remove
                            int pi = line.IndexOf(' ');
                            if (pi == 0)
                            {
                                line = line.Substring(1);
                                pi = line.IndexOf(' ');
                            }
                            //Get no of containe bags
                            bags = int.Parse(line.Substring(0, pi));
                            //save orig multiple inside loop
                            prevMultiple = multiple;
                            //Calc the new multiple
                            multiple = bags * multiple;

                            //format search string
                            line = line.Replace(".", "");
                            line += " contain";
                            line = line.Replace("bag contain", "bags contain");
                            int a = line.IndexOf(' ');
                            line = line.Substring(a + 1);

                            //add to count
                            total += multiple;

                            getBagCountRecursive(tree, list, line, multiple, prevMultiple, bags, ref total);
                        }
                    }
                }
            }
        }

        private static void getBagsRecurse(List<string> fnd, List<string> dict, List<string> list)
        {
            List<string> holdingBagsFnd = new List<string>(0);

            for (int i = 0; i < fnd.Count; i++)
            {
                for (int p = 0; p < list.Count; p++)
                {
                    string findStr = fnd[i].Substring(0, fnd[i].Length - 1);
                    string listStr = list[p];
                    if (listStr.Contains(findStr))
                    {
                        if (listStr.LastIndexOf(findStr) != 0)
                        {
                            dict.Add(listStr.Substring(0, list[p].LastIndexOf("contain") - 1));
                            holdingBagsFnd.Add(listStr.Substring(0, listStr.LastIndexOf("contain") - 1));
                        }
                    }
                }
            }
            if (holdingBagsFnd.Count > 0) getBagsRecurse(holdingBagsFnd, dict, list);
        }
    }
}