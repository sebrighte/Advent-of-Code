﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class ProblemName : Attribute
    {
        public readonly string Name;
        public ProblemName(string name)
        {
            this.Name = name;
        }
    }

    interface Solution
    {
        public object PartOne(string input);
        public object PartTwo(string input);
    }

    static class SolutionExtensions
    {
        public static string GetName(this Solution solver)
        {
            return (
                solver
                    .GetType()
                    .GetCustomAttribute(typeof(ProblemName)) as ProblemName
            ).Name;
        }

        public static string Year(Type t)
        {
            string[] split = t.FullName.Split('.');
            string title = Regex.Replace(split[0], "([a-z])([A-Z])", "$1 $2");
            string year = split[1].Substring(1);
            //return $"{title} {year}";
            return $"{year}";
        }

        public static string Day(Type t)
        {
            string[] split = t.FullName.Split('.');
            string title = Regex.Replace(split[0], "([a-z])([A-Z])", "$1 $2");
            string day = split[2].Substring(1);
            //return $"{title} {year}";
            return $"{day}";
        }

        public static string Year(this Solution solver)
        {
            return Year(solver.GetType());
        }

        public static string Day(this Solution solver)
        {
            return Year(solver.GetType());
        }
    }

    public static class StringExtensionMethods
    {

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static int CountFindChar(this string text, char c)
        {
            return text.Split(c).Length - 1;
        }

        public static int CountCharInstances(this string text, string search)
        {
            int vowels = 0;
            char[] splitVal = search.ToCharArray();
            foreach (char c in splitVal)
            {
                vowels += text.CountFindChar(c);
            }
            return vowels;
        }

        /// <summary>
        /// Replace the first instance of the substring only
        /// </summary>
        /// <param name="text"></param>
        /// <param name="search"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        /// <summary>
        /// Check if the string is an integer
        /// </summary>
        /// <param name="text"></param>
        /// <returns>True if sring can be parsed as an integer</returns>
        public static bool IsNumber(this string text)
        {
            return int.TryParse(text, out int i);
        }

        public static string ReverseAll(this string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    class BaseLine
    {

        protected static bool ReqDataFile = true;

        protected List<int[]> GetFindPermutationsIntArrToList(int[] set)
        {
            var output = new List<int[]>();

            if (set.Count() == 1)
                output.Add(set.ToArray());
            else
            {
                foreach (var (c, ind) in set.WithIndex())
                {
                    int[] tail = set;
                    tail = tail.Where((source, index) => index != ind).ToArray();
                    foreach (var tailPerms in GetFindPermutationsIntArrToList(tail))
                    {
                        int[] copy = tailPerms.ToArray();
                        Array.Resize(ref copy, copy.Length + 1);
                        copy[copy.GetUpperBound(0)] = c;
                        output.Add(copy.ToArray());
                    }
                }
            }
            return output;
        }

        protected IEnumerable<int[]> GetCombinations(List<int> numbers, int target)
        {
            int combinations = (int)(Math.Pow(2, numbers.Count) - 1);

            for (int i = 0; i < combinations; i++)
            {
                List<int> subset = new List<int>();
                List<int> subindexes = new List<int>();

                for (int j = 0; j < numbers.Count; j++)
                {
                    if (((i & (1 << j)) >> j) == 1)
                    {
                        subset.Add(numbers[j]);
                        subindexes.Add(j);
                    }
                }

                if (subset.Sum() == target)
                {
                    yield return subset.ToArray();
                    //Console.WriteLine(string.Join(",", subset.ToArray()));
                    //yield return subindexes.ToArray();
                }
            }
        }

        protected List<string> GetPermutations(List<string> set)
        {
            var output = new List<string>();
            if (set.Count() == 1)
                output.Add(string.Join(",", set));
            else
            {
                foreach (var c in set)
                {
                    List<string> tail = new List<string>(set);
                    tail.Remove(c);
                    foreach (var tailPerms in GetPermutations(tail))
                        output.Add(c + "," + string.Join(",", tailPerms));
                }
            }
            return output;
        }

        protected IEnumerable<T[]> Permutations<T>(T[] rgt)
        {
            IEnumerable<T[]> PermutationsRec(int i)
            {
                if (i == rgt.Length)
                {
                    yield return rgt.ToArray();
                }

                for (var j = i; j < rgt.Length; j++)
                {
                    (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
                    foreach (var perm in PermutationsRec(i + 1))
                    {
                        yield return perm;
                    }
                    (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
                }
            }
            return PermutationsRec(0);
        }

        protected static List<int> ParseInputStrtoListInt(string filename)
        {
            List<int> list = new List<int>();
            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    list.Add(int.Parse(line));
                }
            }
            return list;
        }

        protected static List<long> ParseInputStrtoListLng(string filename)
        {
            List<long> list = new List<long>();
            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    list.Add(Convert.ToInt64(line));
                }
            }
            return list;
        }

        protected static string ParseInputStrtoStr(string filename)
        {
            string retVal = "";
            using (var reader = new StreamReader(filename))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    retVal += line;
                }
            }
            return retVal;
        }

        protected static byte[] ParseInputStrtoByte(string filename)
        {
            byte[] p = System.IO.File.ReadAllBytes(filename);
            return p;
        }

        protected static List<string> ParseInputStrtoListStr(string filename)
        {
            List<string> list = new List<string>();

            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    list.Add(line);
                }
            }
            return list;
        }
    }
}

