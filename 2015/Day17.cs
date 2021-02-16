using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 17: No Such Thing as Too Much")]
    class Day17 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input, 150).Count();
        public object PartTwo(string input) => Day2(input, 150).Count();

        List<Int32> numbers = new List<Int32>();// { 20, 15, 10, 5, 5 };// with volume of 25l
        List<int> arrayCtr = new List<int>();

        private IEnumerable<int> Day1(string inData, int target)
        {
            foreach (var item in inData.Split("\n"))
            {
                numbers.Add(int.Parse(item));
            }           
            foreach (var combo in GetCombinations(numbers, target))
            {
                yield return 1;
            }
        }

        private IEnumerable<int> Day2(string inData, int target)
        {
            numbers.Clear();
            arrayCtr.Clear();

            foreach (var item in inData.Split("\n"))
            {
                numbers.Add(int.Parse(item));
            }

            int min = 100000;
            foreach (var combo in GetCombinations(numbers, target))
            {
                    arrayCtr.Add(combo.Count());
                    if (combo.Count() < min) min = combo.Count();
            }
            foreach (var item in arrayCtr) if (item == min) yield return item;
        }
    }
}