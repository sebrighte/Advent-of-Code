using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode.Y2016
{
    class Hashkey
    {
        public int found;
        public int limit => found + 1001;
        public string val3;
        public string val5 => val3.Substring(0, 1).Repeat(5);
        //public string key;

        public Hashkey(int f, string v3, string k)
        {
            found = f;
            val3 = v3;
            //key = k;
        }
    
    }

    [ProblemName("Day 14: One-Time Pad")]
    class Day14 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).Take(64).Last();
        public object PartTwo(string input) => Day1(input, 2016).Take(64).Last();

        public static Regex regex3 = new Regex(@"(.)\1{2}", RegexOptions.Compiled);
        public static Regex regex5 = new Regex(@"(.)\1{4}", RegexOptions.Compiled);

        private IEnumerable<object> Day1(string inData, int rehash = 0)
        {
            string salt = "abc";
            salt = "zpqevtbw";
            List<Hashkey> ValidKeys = new List<Hashkey>();
            int saltIndex = 0;

            Dictionary<int, string> hash = new Dictionary<int, string>();

            int loop = 100;
            Parallel.For(0, saltIndex + loop, (i) =>
            {
                if (!hash.ContainsKey(i)) hash.Add(i, CreateMD5($"{salt}{i}").Skip(rehash).First());
            });

            while (true)
            {
                if(saltIndex % loop == 0)
                { 
                    Parallel.For(saltIndex, saltIndex + loop, (i) =>
                    {
                        if (!hash.ContainsKey(i)) hash.Add(i, CreateMD5($"{salt}{i}").Skip(rehash).First());
                    });
                }

                while (!hash.ContainsKey(saltIndex))
                {
                    if (!hash.ContainsKey(saltIndex)) hash.Add(saltIndex, CreateMD5($"{salt}{saltIndex}").Skip(rehash).First());
                }

                string MdRes = hash[saltIndex];

                var match = regex3.Match(MdRes);
                if (match.Success)
                {
                    ValidKeys.Add(new Hashkey(saltIndex, match.Groups[0].Value, MdRes));
                }
                match = regex5.Match(MdRes);
                if (match.Success)
                {
                    var resSet = ValidKeys.Select(a => a).Where(a => a.val5 == match.Groups[0].Value && a.limit > saltIndex).OrderBy(a => a.found).ToArray();
                    foreach (var res in resSet)
                    {
                        int p = res.limit - saltIndex;
                        if (p < 1000)
                        {
                            ValidKeys.Remove(res);
                            yield return res.found;
                        }
                    }
                }
                saltIndex++;
            }
        }

        public IEnumerable<string> CreateMD5(string seed)
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
    }
}
