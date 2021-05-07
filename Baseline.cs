using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;

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

    public static class ArrayExtensions
    {
        public static char[,] FillCharArray(this char[,] arr, char c)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = c;
                }
            }
            return arr;
        }

        public static void FillIntArray(this int[] arr, int c)
        {
            for (int i = 0; i < arr.Count(); i++)
            { 
                    arr[i] = c;
            }
        }

        public static string DrawCharArray(this char[,] arr, bool display = true)
        {
            if(display) Console.WriteLine();
            string tmp = "\r\n";
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (display) Console.Write(arr[i, j]);
                    tmp += arr[i, j];
                }
                if (display) Console.WriteLine();
                tmp += "\r\n";
            }
            if (display) Console.WriteLine();
            return tmp;
        }

        public static int CountCharArray(this char[,] arr, char c)
        {
            int ctr = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == c) ctr++;
                }
            }
            return ctr;
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

        public static int CountFindString(this string text, string c)
        {
            return text.Split(c).Length - 1;
        }

        public static string ReplaceAndExport(this string text, char c1, char c2, out string export)
        {
            //export = new {indexStart = text.IndexOf(c1), indexEnd = text.IndexOf(c2), value = text.Substring(text.IndexOf(c1) + 1, text.IndexOf(c2) - text.IndexOf(c1) -1) };
            if(!text.Contains(c1) || !text.Contains(c2))
            {
                export = "";
                return text;
            }


            export = text.Substring(text.IndexOf(c1) + 1, text.IndexOf(c2) - text.IndexOf(c1) - 1);
            return text.Remove(text.IndexOf(c1), text.IndexOf(c2) - text.IndexOf(c1) + 1);
        }

        public static Int32 ToInt32(this string number)
        {
            return Int32.Parse(
                number,
                NumberStyles.Integer,
                CultureInfo.CurrentCulture.NumberFormat);
        }

        public static Int32 ToInt32(this char number)
        {
            return Int32.Parse(
                number.ToString(),
                NumberStyles.Integer,
                CultureInfo.CurrentCulture.NumberFormat);
        }

        public static Int64 ToInt64(this string number)
        {
            return Int64.Parse(
                number,
                NumberStyles.Integer,
                CultureInfo.CurrentCulture.NumberFormat);
        }

        public static string Repeat(this string text, int num)
        {
            string retVal = "";
            for (int i = 0; i < num; i++)
            {
                retVal += text;
            }
            return retVal;
        }

        public static string SwapCharPos(this string text, int pos1, int pos2)
        {
            string retVal = text;
            string char1 = text[pos1].ToString();
            string char2 = text[pos2].ToString();
            retVal = retVal.Remove(pos1, 1).Insert(pos1, char2);
            retVal = retVal.Remove(pos2, 1).Insert(pos2, char1);
            return retVal;
        }

        public static string SwapChar(this string text, string char1, string char2)
        {
            string retVal = text;
            int pos1 = text.IndexOf(char1);
            int pos2 = text.IndexOf(char2);
            retVal = retVal.Remove(pos1, 1).Insert(pos1, char2);
            retVal = retVal.Remove(pos2, 1).Insert(pos2, char1);
            return retVal;
        }

        public static string ReversePos(this string text, int pos1, int pos2)
        {
            string retVal = text;
            string tmp = retVal.Substring(pos1, pos2 - pos1 + 1);
            retVal = retVal.Remove(pos1, pos2 - pos1 + 1);
            retVal = retVal.Insert(pos1, tmp.ReverseAll());
            return retVal;
        }

        public static string Move(this string text, int pos1, int pos2)
        {
            string retVal = text;
            string val = retVal[pos1].ToString();
            retVal = retVal.Remove(pos1, 1).Insert(pos2, val);
            return retVal;
        }

        public static string RotateStepLeft(this string text, int pos)
        {
            string retVal = text;
            for (int i = 0; i < pos; i++)
            {
                string val = retVal[0].ToString();
                retVal = retVal.Remove(0, 1).Insert(retVal.Length - 1, val);
            }
            return retVal;
        }

        public static string RotateStepRight(this string text, int pos)
        {
            string retVal = text;
            for (int i = pos - 1; i >= 0; i--)
            {
                string val = retVal[retVal.Length - 1].ToString();
                retVal = retVal.Remove(retVal.Length - 1, 1).Insert(0, val);
            }
            return retVal;
        }

        public static int CountStringInstances(this string text, string search)
        {
            return Regex.Matches(text, search).Count;
        }

        public static string ReplaceOccurrence(this string text, int Place, string Find, string Replace)
        {
            string result = text.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
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

        public static string ReplaceLast(this string text, string search, string replace)
        {
            int pos = text.LastIndexOf(search);
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="prefix">Text to show before dumpted value</param>
        /// <param name="suffix">Text to show after dumpted value</param>
        /// <returns></returns>
        public static void Dump<T>(this T value, object prefix = null, object suffix = null)
        {
            Console.WriteLine($"{prefix}{value}{suffix}");
        }

        /// <summary>
        /// Makes a copy from the object.
        /// Doesn't copy the reference memory, only data.
        /// </summary>
        /// <typeparam name="T">Type of the return object.</typeparam>
        /// <param name="item">Object to be copied.</param>
        /// <returns>Returns the copied object.</returns>
        //public static T Clone<T>(this object item)
        //{
        //    if (item != null)
        //    {
        //        BinaryFormatter formatter = new BinaryFormatter();
        //        MemoryStream stream = new MemoryStream();

        //        formatter.Serialize(stream, item);
        //        stream.Seek(0, SeekOrigin.Begin);

        //        T result = (T)formatter.Deserialize(stream);

        //        stream.Close();

        //        return result;
        //    }
        //    else
        //        return default(T);
        //}

        public static IList<T> DupList<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static List<List<string>> Dup(this List<List<string>> listToClone)
        {
            return new List<List<string>>(listToClone.ToArray());
        }

        public static HashSet<string> Dup(this HashSet<string> setToClone)
        {
            HashSet<string> copy = new HashSet<string>();

            foreach (var item in setToClone)
            {
                copy.Add(item);
            }
            return copy;
        }
    }

    class BaseLine
    {
        protected IEnumerable<string> CreateMD5(string seed)
        {
            MD5 md5 = MD5.Create();
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                sb.Clear();
                byte[] inputBytes = Encoding.ASCII.GetBytes(seed);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                foreach (byte v in hashBytes) sb.Append(v.ToString("X2"));
                seed = sb.ToString().ToLower();
                yield return seed;
            }
        }

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

        protected IEnumerable<(int, long)> GetCombinationsMult(List<int> numbers, int target)
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

                //long result = subset.Aggregate(1, (a, b) => a * b);
                long result = 1;
                foreach (var item in subset)
                {
                    result *= item;
                }
                 
                if (subset.Sum() == target && result > 0)
                    yield return (subset.Count, result);

            }
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

        protected static IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        protected List<List<int>> GetIntPermutations(int[] elements)
        {
            var tmp = new List<int>();
            List<List<int>> res = new List<List<int>>();
            CollectAll(elements.ToList(), tmp, res);
            //return res;
            return res.GroupBy(x => String.Join(",", x))
                         .Select(x => x.First().ToList())
                         .ToList();
        }

        protected List<List<string>> GetIntPermutations(string[] elements)
        {
            var tmp = new List<string>();
            List<List<string>> res = new List<List<string>>();
            CollectAll(elements.ToList(), tmp, res);
            return res;
        }

        protected List<List<string>> GetIntPermutations(List<string> elements)
        {
            var tmp = new List<string>();
            List<List<string>> res = new List<List<string>>();
            CollectAll(elements, tmp, res);
            return res;
        }

        private void CollectAll<T> (List<T> remaining, IList<T> soFar, List<List<T>> all)
        {
            if (soFar.Count != 0)
            {
                all.Add(soFar.ToList());
            }
            foreach (var item in remaining.ToList())
            {
                remaining.Remove(item);
                soFar.Add(item);
                CollectAll(remaining, soFar, all);
                soFar.Remove(item);
                remaining.Add(item);
            }
        }
    }
}

