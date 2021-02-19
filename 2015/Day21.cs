using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 21: RPG Simulator 20XX")]
    class Day21 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(103, 9, 2).Take(10000).Min();
        public object PartTwo(string input) => Day1(103, 9, 2, true).Take(10000).Max();

        private IEnumerable<int> Day1(int hitPoints, int damage, int armour, bool part2 = false)
        {
            //set up lists for shop
            #region
            //Weapons:    Cost  Damage  Armor
            List<string> listWeapons = new List<string>();
            //listWeapons.Add("");
            listWeapons.Add("Dagger 8 4 0");
            listWeapons.Add("Shortsword 10 5 0");
            listWeapons.Add("Warhammer 25 6 0");
            listWeapons.Add("Longsword 40 7 0");
            listWeapons.Add("Greataxe 74 8 0");

            //Armor:      Cost  Damage  Armor
            List<string> listArmour = new List<string>();
            listArmour.Add("");
            listArmour.Add("Leather 13 0 1");
            listArmour.Add("Chainmail 31 0 2");
            listArmour.Add("Splintmail 53 0 3");
            listArmour.Add("Bandedmail 75 0 4");
            listArmour.Add("Platemail 102 0 5");

            //Rings:      Cost  Damage  Armor
            List<string> listRings = new List<string>();
            listRings.Add("");
            listRings.Add("");
            listRings.Add("Damage+1 25 1 0");
            listRings.Add("Damage+2 50 2 0");
            listRings.Add("Damage+3 100 3 0");
            listRings.Add("Defense+1 20 0 1");
            listRings.Add("Defense+2 40 0 2");
            listRings.Add("Defense+3 80 0 3");
            #endregion

            while (true)
            {
                int cost = 0;
                int dam = 0;
                int arm = 0;

                //select wapon
                listWeapons = Shuffle(listWeapons).ToList();
                var match = Regex.Match(listWeapons[0], @"(.*) (.*) (.*) (.*)");
                cost += int.Parse(match.Groups[2].Value);
                dam += int.Parse(match.Groups[3].Value);
                arm += int.Parse(match.Groups[4].Value);

                //select armour
                listArmour = Shuffle(listArmour).ToList();
                match = Regex.Match(listArmour[0], @"(.*) (.*) (.*) (.*)");
                if (match.Groups.Count > 1)
                {
                    cost += int.Parse(match.Groups[2].Value);
                    dam += int.Parse(match.Groups[3].Value);
                    arm += int.Parse(match.Groups[4].Value);
                }

                //select ring
                listRings = Shuffle(listRings).ToList();
                match = Regex.Match(listRings[0], @"(.*) (.*) (.*) (.*)");
                if (match.Groups.Count > 1)
                {
                    cost += int.Parse(match.Groups[2].Value);
                    dam += int.Parse(match.Groups[3].Value);
                    arm += int.Parse(match.Groups[4].Value);
                }

                match = Regex.Match(listRings[1], @"(.*) (.*) (.*) (.*)");
                if (match.Groups.Count > 1)
                {
                    cost += int.Parse(match.Groups[2].Value);
                    dam += int.Parse(match.Groups[3].Value);
                    arm += int.Parse(match.Groups[4].Value);
                }

                yield return Fight(true, cost, 100, dam, arm, hitPoints, damage, armour, part2);
            }
        }

        private int Fight(bool player1, int gold, int uhps, int udam, int uarm, int ohps, int odam, int oarm, bool part2)
        {
            if(player1)
                ohps -= udam - oarm < 1 ? 1 : udam - oarm;
            else 
                uhps -= odam - uarm < 1 ? 1 : odam - uarm; 

            player1 = !player1;

            if (ohps <= 0)
                return part2? 0 : gold;

            if (uhps <= 0)
                return part2? gold : 999999;

            return Fight(player1, gold, uhps, udam, uarm, ohps, odam, oarm, part2);
        }
    }
}
