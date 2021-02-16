using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 19: Monster Messages")]
    class Day19 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        List<string> inputList;
        List<string> receivedMessages = new List<string>();
        Rule[] rules;

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            inputList = inData.Split("\r\n").ToList();

            receivedMessages.Clear();

            //Create rule array
            int ruleCtr = 0;
            foreach (string str in inputList)
            {
                if (str.Contains(":"))
                    ruleCtr++;
            }
            rules = new Rule[ruleCtr];

            //load data into classes
            foreach (string str in inputList)
            {
                if (str.Contains(":"))
                {
                    int id = int.Parse(str.Split(':')[0]);
                    Rule tmpRule = new Rule(str);
                    rules[id] = tmpRule;
                }
                else if (str.Contains('a') || (str.Contains('b')))
                    receivedMessages.Add(str);
            }

            //Get Regex for rules
            string regexRule = CreateRegEx();

            //Calculate matches
            int ctr2 = 0;
            foreach (string msg in receivedMessages)
            {
                if (Regex.IsMatch(msg, regexRule))
                {
                    ctr2++;
                }
            }
            yield return ctr2;
        }

        private IEnumerable<int> Day2(string inData, bool part2 = false)
        {
            inputList = inData.Split("\r\n").ToList();

            receivedMessages.Clear();

            //Create rule array
            int ruleCtr2 = 0;
            foreach (string str in inputList)
            {
                if (str.Contains(":"))
                    ruleCtr2++;
            }
            rules = new Rule[500];

            //load data into classes
            foreach (string str in inputList)
            {
                if (str.Contains(":"))
                {
                    int id = int.Parse(str.Split(':')[0]);
                    Rule tmpRule = new Rule(str);
                    rules[id] = tmpRule;
                }
                else if (str.Contains('a') || (str.Contains('b')))
                    receivedMessages.Add(str);
            }

            int totalctr = 0;

            for (int p = 1; p < 10; p++)
            {
                string rule8 = "8: ( 42 ) +";
                Rule tmp1 = new Rule(rule8);
                rules[8] = tmp1;

                string rule11 = "11: 42 { x } 31 { x }";
                Rule tmp2 = new Rule(rule11);
                rules[11] = tmp2;

                string regexRule = CreateRegEx();

                regexRule = regexRule.Replace("x", p.ToString());

                int ctr = 0;
                foreach (string msg in receivedMessages)
                {
                    if (Regex.IsMatch(msg, regexRule)) { ctr++; }
                }
                totalctr += ctr;
                //Console.WriteLine(ctr);
                if (ctr == 0) break;

            }
            yield return totalctr;
        }

        public string CreateRegEx(int start = 0)
        {
            Rule thisRule = rules[start];
            string ruleString = thisRule.GetMessage();
            string[] split;
            bool finish = false;

            while (!finish)
            {
                split = ruleString.Split(' ');

                for (int i = 0; i < split.Length; i++)
                {
                    if (split[i].IsNumber())
                    {
                        if (rules[int.Parse(split[i])].GetRuleType() == Rule.ruletype.terminal)
                            ruleString = ruleString.ReplaceFirst(split[i], string.Format("{0}", rules[int.Parse(split[i])].GetMessage()));
                        else
                            ruleString = ruleString.ReplaceFirst(split[i], string.Format("( {0} )", rules[int.Parse(split[i])].GetMessage()));
                        break;
                    }
                }
                finish = !ruleString.Any(c => char.IsDigit(c));
            }
            ruleString = "(?:^REGEX$)".Replace("REGEX", ruleString).Replace(" ", "");
            return ruleString;
        }
    }

    class Rule
    {
        public enum ruletype { rule, terminal };
        string ruleStr;
        ruletype type;
        string ruleMessage;

        public string GetMessage()
        {
            return ruleMessage;
        }

        public ruletype GetRuleType()
        {
            return type;
        }

        public Rule(string ruleStrIn)
        {
            ruleStr = ruleStrIn;
            type = ruletype.rule;

            //Terminal e.g. a|b
            if (Regex.IsMatch(ruleStrIn, @"^(\d+: ""[ab]"")$"))
            {
                type = ruletype.terminal;
                ruleMessage = ruleStrIn.Split(':')[1].Replace("\"", "").Replace(" ", "").Trim();
            }
            else
                ruleMessage = ruleStrIn.Split(':')[1].Trim();
        }
    }

}
