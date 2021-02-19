using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 22: Wizard Simulator 20XX")]
    class Day22 : BaseLine, Solution
    {
        //BOSS
        //Hit Points: 55
        //Damage: 8

        //Wizard
        //Hit Points: 50
        //Mana Points: 500

        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => null; // Day1(input, true).First();

        private IEnumerable<string> Day1(string inData, bool part2 = false)
        {
            Game NewGame = new Game();

            int mana = NewGame.Play();

            //List<string> input = inData.Split("\r\n").ToList();
            yield return $"Mana = {mana}";
        }
    }

    struct spell
    {
        public int number;
        public string Name;
        public int cost;
        public int damage;
        public int heal;
        public int duration;
        public string type;
        public int armour;
        public int mana;

        public void setspell(int num, string nm, int cst, int dam, int hl, int mr, int dur, string t, int ar)
        {
            number = num;
            Name = nm;
            cost = cst;
            damage = dam;
            heal = hl;
            duration = dur;
            type = t;
            armour = ar;
            mana = mr;
        }
    }

    class Game
    {
        spell[] Spells = new spell[5];
        List<Tuple<int, int>> gameStatus = new List<Tuple<int, int>>();

        int my_mana;
        int my_hitPoints;
        int my_armour;
        int op_hitPoints;
        int op_damage;
        bool player;
        int turn;

        private void SetSpells()
        {
            //Number - Cost - Damage - Heal - Mana -Duration - Type - armour?, 
            Spells[0].setspell(0, "Missile",  53, 4, 0,   0, 0, "A", 0);
            Spells[1].setspell(1, "Drain",    73, 2, 2,   0, 0, "A", 0);
            Spells[2].setspell(2, "Shield",  113, 0, 0,   0, 6, "E", 7);
            Spells[3].setspell(3, "Poison",  173, 3, 0,   0, 6, "E", 0);
            Spells[4].setspell(4, "Recharge",229, 0, 0, 101, 5, "E", 0);
        }

        public Game()
        {
            SetSpells();

            my_mana = 250;
            my_hitPoints = 10;
            my_armour = 0;
            op_hitPoints = 14;
            op_damage = 8;
            player = true;
            turn = 0;
        }

        public int Player1PlayTest1()
        {
            turn++;

            Console.WriteLine($"\n-- Player turn --");
            Console.WriteLine($"- Player has {my_hitPoints} hit points, {my_armour} armor, {my_mana} mana");
            Console.WriteLine($"- Boss has {op_hitPoints} hit points");

            for (int i = 0; i < gameStatus.Count; i++)
            {
                if (gameStatus[i].Item2 == turn)
                {
                    if (gameStatus[i].Item1 == 2) my_armour -= 7;
                    gameStatus.Remove(gameStatus[i]);
                }
            }

            foreach (var item in gameStatus)
            {
                op_hitPoints -= Spells[item.Item1].damage;
                my_hitPoints += Spells[item.Item1].heal;
                my_mana += Spells[item.Item1].mana;
            }

            foreach (var item in gameStatus)
            {
                Console.WriteLine($"{Spells[item.Item1].Name} deals {Spells[item.Item1].damage} damage; its timer is now {item.Item2 - turn}");
            }

            if (turn == 1)
            {
                //Console.WriteLine($"Player casts Poison"); my_mana -= 173;
                //gameStatus.Add(new Tuple<int, int>(5, turn + 6));
                Console.WriteLine($"Player casts Poison"); my_mana -= 173;
                gameStatus.Add(new Tuple<int, int>(3, turn + 6));
            }

            if (turn == 3)
            {
                //Console.WriteLine($"Player casts Magic Missile"); my_mana -= 53; op_hitPoints -= 4;
                Console.WriteLine($"Player casts Magic Missile"); my_mana -= 53; op_hitPoints -= 4;
            }

            if (op_hitPoints <= 0)
            {
                Console.WriteLine($"This kills the Boss, and the Wizard wins");
                return 1;
            }
            else if (my_hitPoints <= 0)
            {
                Console.WriteLine($"This kills the Wizard, and the Boss wins");
                return 1;
            }
            else return 0;
        }

        public int Player1PlayTest2()
        {
            turn++;

            Console.WriteLine($"\n-- Player turn --");
            Console.WriteLine($"- Player has {my_hitPoints} hit points, {my_armour} armor, {my_mana} mana");
            Console.WriteLine($"- Boss has {op_hitPoints} hit points");

            for (int i = 0; i < gameStatus.Count; i++)
            {
                if (gameStatus[i].Item2 == turn)
                {
                    if (gameStatus[i].Item1 == 2) my_armour -= 7;
                    gameStatus.Remove(gameStatus[i]);
                }
            }

            foreach (var item in gameStatus)
            {
                op_hitPoints -= Spells[item.Item1].damage;
                my_hitPoints += Spells[item.Item1].heal;
                my_mana += Spells[item.Item1].mana;
            }

            foreach (var item in gameStatus)
            {
                Console.WriteLine($"{Spells[item.Item1].Name} deals {Spells[item.Item1].damage} damage; its timer is now {item.Item2 - turn}");
            }

            if (turn == 1)
            {
                Console.WriteLine($"Player casts Recharge"); my_mana -= 229;
                gameStatus.Add(new Tuple<int, int>(4, turn + 5 + 1));
            }

            if (turn == 3)
            {
                Console.WriteLine($"Player castsShield"); my_mana -= 113; my_armour += 7;
                gameStatus.Add(new Tuple<int, int>(2, turn + 6));
            }

            if (turn == 5)
            {
                Console.WriteLine($"Player casts Drain"); my_mana -= 73; op_hitPoints -= 2; my_hitPoints += 2;
            }
            if (turn == 7)
            {
                Console.WriteLine($"Player casts Poison"); my_mana -= 173;
                gameStatus.Add(new Tuple<int, int>(3, turn + 6));
            }
            if (turn == 9)
            {
                Console.WriteLine($"Player casts Magic Missile"); my_mana -= 53; op_hitPoints -= 4;
            }

            if (op_hitPoints <= 0)
            {
                Console.WriteLine($"This kills the Boss, and the Wizard wins");
                return 1;
            }
            else if (my_hitPoints <= 0)
            {
                Console.WriteLine($"This kills the Wizard, and the Boss wins");
                return 1;
            }
            else return 0;
        }

        public int Player2PlayTest2()
        {
            turn++;

            for (int i = 0; i < gameStatus.Count; i++)
            {
                if (gameStatus[i].Item2 == turn)
                {
                    if (gameStatus[i].Item1 == 2) my_armour -= 7;
                    gameStatus.Remove(gameStatus[i]);
                }
            }

            Console.WriteLine($"\n-- Boss turn --");
            Console.WriteLine($"- Player has {my_hitPoints} hit points, {my_armour} armor, {my_mana} mana");
            Console.WriteLine($"- Boss has {op_hitPoints} hit points");

            foreach (var item in gameStatus)
            {
                op_hitPoints -= Spells[item.Item1].damage;
                my_hitPoints += Spells[item.Item1].heal;
                my_mana += Spells[item.Item1].mana;
            }

            foreach (var item in gameStatus)
            {
                Console.WriteLine($"{Spells[item.Item1].Name} deals {Spells[item.Item1].damage} damage; its timer is now {item.Item2 - turn}");
            }

            if (my_hitPoints <= 0)
            {
                Console.WriteLine($"This kills the Wizard, and the Boss wins");
                return -1;
            }
            else if (op_hitPoints <= 0)
            {
                Console.WriteLine($"This kills the Boss, and the Wizard wins");
                return 1;
            }
            else
            {
                Console.WriteLine($"Boss attacks for {op_damage - my_armour} damage"); my_hitPoints -= (op_damage - my_armour);
                if (my_hitPoints <= 0)
                {
                    Console.WriteLine($"This kills the Wizard, and the Boss wins");
                    return -1;
                }
                return 0;
            }
        }

        public int Play()
        {
            int PlayerWins = 0;

            //For test 1 //
            //op_hitPoints = 13;

            while (PlayerWins == 0)
            {
                if (player)
                {
                    PlayerWins = Player1PlayTest2();
                    //PlayerWins = Player1PlayTest1();
                    player = !player;
                }
                else
                {
                    PlayerWins = Player2PlayTest2();
                    player = !player;
                }
            }
            //"End".Dump(PlayerWins);

            return PlayerWins == 1 ? my_mana : 9999999;
        }
    }

 

}
