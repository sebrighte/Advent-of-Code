using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 21: Scrambled Letters and Hash")]
    class Day21 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input,false).First();
        public object PartTwo(string input) => Solver(input,true).First();

        /*private IEnumerable<object> Day1(string inData)
        {
            //inData =
            //    "swap position 4 with position 0\r\n" +
            //    "swap letter d with letter b\r\n" +
            //    "reverse positions 0 through 4\r\n" +
            //    "rotate left 1 step\r\n" +
            //    "move position 1 to position 4\r\n" +
            //    "move position 3 to position 0\r\n" +
            //    "rotate based on position of letter b\r\n" +
            //    "rotate based on position of letter d";

            string password = "abcde";
            password = "abcdefgh";

            Match match;

            List<string> input = inData.Split("\r\n").ToList();
            foreach (var lineItem in input)
            {
                if (lineItem.Contains("swap position"))
                {
                    match = Regex.Match(lineItem, @"swap position (.*) with position (.*)");
                    password = password.SwapCharPos(match.Groups[1].Value.ToInt32(), match.Groups[2].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("swap letter"))
                {
                    match = Regex.Match(lineItem, @"swap letter (.*) with letter (.*)");
                    password = password.SwapChar(match.Groups[1].Value, match.Groups[2].Value);
                    continue;
                }

                if (lineItem.Contains("reverse positions"))
                {
                    match = Regex.Match(lineItem, @"reverse positions (.*) through (.*)");
                    password = password.ReversePos(match.Groups[1].Value.ToInt32(), match.Groups[2].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("rotate left"))
                {
                    match = Regex.Match(lineItem, @"rotate left (.*) step");
                    password = password.RotateStepLeft(match.Groups[1].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("rotate right"))
                {
                    match = Regex.Match(lineItem, @"rotate right (.*) step");
                    password = password.RotateStepRight(match.Groups[1].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("move position"))
                {
                    match = Regex.Match(lineItem, @"move position (.*) to position (.*)");
                    password = password.Move(match.Groups[1].Value.ToInt32(), match.Groups[2].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("rotate based on position of letter"))
                {
                    match = Regex.Match(lineItem, @"rotate based on position of letter (.*)");
                    int tmp = password.IndexOf(match.Groups[1].Value);
                    tmp += tmp >= 4 ? 1 : 0;
                    password = password.RotateStepRight(++tmp);
                    continue;
                }
            }
            yield return password;
        }*/

        private IEnumerable<object> Solver(string inData, bool part2 = true)
        {
            string password = part2 ? "fbgdceah" : "abcdefgh";

            Match match;

            List<string> input = inData.Split("\r\n").ToList();

            if (part2) input.Reverse();

            foreach (var lineItem in input)
            {
                if (lineItem.Contains("swap position"))
                {
                    match = Regex.Match(lineItem, @"swap position (.*) with position (.*)");
                    password = password.SwapCharPos(match.Groups[1].Value.ToInt32(), match.Groups[2].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("swap letter"))
                {
                    match = Regex.Match(lineItem, @"swap letter (.*) with letter (.*)");
                    password = password.SwapChar(match.Groups[1].Value, match.Groups[2].Value);
                    continue;
                }

                if (lineItem.Contains("reverse positions"))
                {
                    match = Regex.Match(lineItem, @"reverse positions (.*) through (.*)");
                    password = password.ReversePos(match.Groups[1].Value.ToInt32(), match.Groups[2].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("rotate left"))
                {
                    match = Regex.Match(lineItem, @"rotate left (.*) step");
                    if (part2) password = password.RotateStepRight(match.Groups[1].Value.ToInt32());
                    if (!part2) password = password.RotateStepLeft(match.Groups[1].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("rotate right"))
                {
                    match = Regex.Match(lineItem, @"rotate right (.*) step");
                    if (part2) password = password.RotateStepLeft(match.Groups[1].Value.ToInt32());
                    if (!part2) password = password.RotateStepRight(match.Groups[1].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("move position"))
                {
                    match = Regex.Match(lineItem, @"move position (.*) to position (.*)");
                    if (part2) password = password.Move(match.Groups[2].Value.ToInt32(), match.Groups[1].Value.ToInt32());
                    if (!part2) password = password.Move(match.Groups[1].Value.ToInt32(), match.Groups[2].Value.ToInt32());
                    continue;
                }

                if (lineItem.Contains("rotate based on position of letter"))
                {
                    match = Regex.Match(lineItem, @"rotate based on position of letter (.*)");
                    int tmp = password.IndexOf(match.Groups[1].Value);

                    if (part2)
                    {
                        int[] mapping = { 1, 1, 6, 2, 7, 3, 0, 4 };
                        password = password.RotateStepLeft(mapping[tmp]);
                    }
                    else
                    {
                        tmp += tmp >= 4 ? 1 : 0;
                        password = password.RotateStepRight(++tmp);
                    }
                    continue;
                }
            }
            yield return password;
        }
    }
}
