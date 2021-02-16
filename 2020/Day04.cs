using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 4: Passport Processing")]
    class Day04 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            string passportEntry = "";

            foreach (var line in inData.Split("\r\n").ToArray())
            {
                //List<string> list = new List<string>();
                if (line == "") passportEntry += "$";
                if (line != "" && passportEntry != "") passportEntry += " ";
                passportEntry += line;
            }

            passportEntry = passportEntry.Replace("$ ", "$");

            var b = passportEntry.Split('$');

            int validPassports2 = 0;
            int validPassports1 = 0;

            for (int passportList = 0; passportList < b.Length; ++passportList)
            {
                PassportCls Passports = new PassportCls(b[passportList]);
                if (Passports.isPassportValid()) validPassports2++;
                if (Passports.isPassportValid1()) validPassports1++;
            }

            if (!part2)
                yield return validPassports1;
            else
                yield return validPassports2;
        }
    }

    public class PassportCls
    {
        private string byr; //(Birth Year)
        private string iyr; //(Issue Year)
        private string eyr; //(Expiration Year)
        private string hgt; //(Height)
        private string hcl; //(Hair Color)
        private string ecl; //(Eye Color)
        private string pid; //(Passport ID)
        private string cid; //(Country ID)
        private string isValidstr2;
        private string isValidstr1;

        public string wrtiePassport(int index)
        {
            string outstr = index.ToString() + ",";
            outstr += sortstring(isValidstr2) + ",";
            outstr += byr + ",";
            outstr += iyr + ",";
            outstr += eyr + ",";
            outstr += hgt + ",";
            outstr += hcl + ",";
            outstr += ecl + ",";
            outstr += pid + ",";
            outstr += cid;
            return outstr;
        }

        private string sortstring(string myStr)
        {
            char temp;
            string str = myStr.ToLower();
            char[] charstr = str.ToCharArray();
            for (int i = 1; i < charstr.Length; i++)
            {
                for (int j = 0; j < charstr.Length - 1; j++)
                {
                    if (charstr[j] > charstr[j + 1])
                    {
                        temp = charstr[j];
                        charstr[j] = charstr[j + 1];
                        charstr[j + 1] = temp;
                    }
                }
            }
            string charsStr = new string(charstr);
            return charsStr;
        }

        public PassportCls(string passportString)
        {
            var passportHdgs = passportString.Split(' ');
            Regex regx;

            for (int x = 0; x < passportHdgs.Length; ++x)
            {
                string codeHdr = passportHdgs[x].Substring(0, 3);
                string codeVal = passportHdgs[x].Substring(4, passportHdgs[x].Length - 4);

                switch (codeHdr)
                {
                    //byr(Birth Year) - four digits; at least 1920 and at most 2002.
                    case "byr":
                        byr = codeVal;
                        regx = new Regex(@"^[1][9][2-9][0-9]$|^[2][0][0][0-2]$"); //205
                        isValidstr2 += regx.IsMatch(codeVal) ? "a" : "";
                        isValidstr1 += "a";
                        break;

                    //iyr(Issue Year) - four digits; at least 2010 and at most 2020
                    case "iyr":
                        iyr = codeVal;
                        regx = new Regex(@"^[2][0][1][0-9]$|^[2][0][2][0]$"); //195
                        isValidstr2 += regx.IsMatch(codeVal) ? "b" : "";
                        isValidstr1 += "b";
                        break;

                    //eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
                    case "eyr":
                        eyr = codeVal;
                        regx = new Regex(@"^[2][0][2][0-9]$|^[2][0][3][0]$"); //190
                        isValidstr2 += regx.IsMatch(codeVal) ? "c" : "";
                        isValidstr1 += "c";
                        break;

                    //hgt(Height) - a number followed by either cm or in:
                    //If cm, the number must be at least 150 and at most 193.
                    //If in, the number must be at least 59 and at most 76.
                    case "hgt":
                        hgt = codeVal;
                        regx = new Regex(@"(^[1][5][0][cm]{2}$|^[1][5-8][0-9][cm]{2}$|^[1][9][0-3][cm]{2}$)|(^[5][9][in]{2}$|^[6][0-9][in]{2}$|^[7][0-6][in]{2}$)");
                        isValidstr2 += regx.IsMatch(codeVal) ? "d" : "";
                        isValidstr1 += "d";
                        break;

                    //hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                    case "hcl":
                        hcl = codeVal;
                        regx = new Regex(@"#[0-9a-f]{6}");
                        isValidstr2 += regx.IsMatch(codeVal) ? "e" : "";
                        isValidstr1 += "e";
                        break;

                    //ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                    case "ecl":
                        ecl = codeVal;
                        regx = new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$");
                        isValidstr2 += regx.IsMatch(codeVal) ? "f" : "";
                        isValidstr1 += "f";
                        break;

                    //pid (Passport ID) - a nine-digit number, including leading zeroes.
                    case "pid":
                        pid = codeVal;
                        regx = new Regex(@"^[0-9]{9}$");
                        isValidstr2 += regx.IsMatch(codeVal) ? "g" : "";
                        isValidstr1 += "g";
                        break;

                    //cid (Country ID) - ignored, missing or not.
                    case "cid":
                        cid = codeVal;
                        break;
                }
            }
        }

        public bool isPassportValid()
        {
            Regex regx = new Regex(@"^([a-g]{7,7}$)");
            if (regx.IsMatch(isValidstr2)) return true;
            return false;
        }

        public bool isPassportValid1()
        {
            Regex regx = new Regex(@"^([a-g]{7,7}$)");
            if (regx.IsMatch(isValidstr1)) return true;
            return false;
        }

        public string isPassportValidstr()
        {
            return isValidstr2;
        }
    }
}