using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 22: Wizard Simulator 20XX")]
    class Day22 : BaseLine, Solution
    {
        public object PartOne(string input) => PlayGame(input, 1000);
        public object PartTwo(string input) => PlayGame(input, 1000, true);

        private object PlayGame(string inData, int iterations, bool part2 = false)
        {
            Game NewGame = new Game();
            return NewGame.Play(50, 500, 55, 8, part2).Take(iterations).Min();
        }
    }

    struct spell
    {
        public string Name;
        public int cost;
        public int damage;
        public int heal;
        public int duration;
        public int armour;
        public int mana;

        public void setspell(string nm, int cst, int dam, int hl, int mr, int dur, int ar)
        {
            Name = nm;
            cost = cst;
            damage = dam;
            heal = hl;
            duration = dur;
            armour = ar;
            mana = mr;
        }
    }

    class Game
    {
        spell[] Spells = new spell[5];
        //Spell id, duration
        List<Tuple<int, int>> gameStatus = new List<Tuple<int, int>>();

        bool player;
        int my_mana;
        int my_hitPoints;
        int my_armour;
        int op_hitPoints;
        int op_damage;
        int turn;
        int manaSpent;
        string spellsUsed;

        Random rnd = new Random();

        private void SetSpells()
        {
                              //Name        Cost    Damage  Heal    Mana    Duration    armour, 
            Spells[0].setspell( "Missile",  53,     4,      0,      0,      0,          0);
            Spells[1].setspell( "Drain",    73,     2,      2,      0,      0,          0);
            Spells[2].setspell( "Shield",   113,    0,      0,      0,      6,          7);
            Spells[3].setspell( "Poison",   173,    3,      0,      0,      6,          0);
            Spells[4].setspell( "Recharge", 229,    0,      0,      101,    5,          0);
        }

        public Game()
        {
            SetSpells();
        }

        public IEnumerable<int> Play(int WizHp, int WizMana, int BossHp, int BossDamage, bool part2 = false)
        {
            int PlayerWins = 0;

            while (true)
            {
                my_mana = WizMana;
                my_hitPoints = WizHp;
                my_armour = 0;
                op_hitPoints = BossHp;
                manaSpent = 0;
                op_damage = BossDamage;
                player = true;
                turn = 0;
                PlayerWins = 0;
                spellsUsed = "";
                gameStatus.Clear();

                while (PlayerWins == 0)
                {
                    if (player)
                    {
                        PlayerWins = Player1(ref manaSpent, part2);
                    }
                    else
                    {
                        PlayerWins = Player2(part2); 
                    }
                    player = !player;
                }
                if (PlayerWins == 1) yield return manaSpent;
                //yield return PlayerWins == 1 ? manaSpent : 9999;
            }
        }

        public int Player1(ref int ms,bool part2)
        {
            turn++;

            //Part2
            if (part2) my_hitPoints -= 1;
            if (my_hitPoints <= 0) 
                return -1;

            int spell = -1;
            bool spellUsed = true;

            foreach (var item in gameStatus)
            {
                op_hitPoints -= Spells[item.Item1].damage;
                my_mana += Spells[item.Item1].mana;
            }

            for (int i = 0; i < gameStatus.Count; i++)
            {
                if (gameStatus[i].Item2 == turn)
                {
                    if (gameStatus[i].Item1 == 2) 
                        my_armour -= Spells[2].armour;
                    gameStatus.Remove(gameStatus[i]);
                }
            }

            if (my_mana >= 53)
            {
                while (spellUsed)
                {
                    spell = rnd.Next(0, 5);

                    spellUsed = false;
                    if (Spells[spell].cost > my_mana)
                    {
                        spellUsed = true;
                    }
                    foreach (var item in gameStatus)
                    {
                        if (item.Item1 == spell)
                        {
                            spellUsed = true;
                        }
                    }
                }
            }

            if (spell == 0)
            {
                //Console.WriteLine($"Player casts Magic Missile"); 
                my_mana -= Spells[spell].cost;
                op_hitPoints -= Spells[spell].damage;
                ms += Spells[spell].cost;
                spellsUsed += "0 ";
            }
            if (spell == 1)
            {
                //Console.WriteLine($"Player casts Drain"); 
                my_mana -= Spells[spell].cost; 
                op_hitPoints -= Spells[spell].damage;  
                my_hitPoints += Spells[spell].heal; 
                ms += Spells[spell].cost;
                spellsUsed += "1 ";
            }
            if (spell == 2)
            {
                //Console.WriteLine($"Player casts Shield"); 
                my_mana -= Spells[spell].cost; 
                my_armour += Spells[spell].armour;
                ms += Spells[spell].cost;
                gameStatus.Add(new Tuple<int, int>(spell, turn + Spells[spell].duration));
                spellsUsed += "2 ";
            }
            if (spell == 3)
            {
                //Console.WriteLine($"Player casts Poison"); 
                my_mana -= Spells[spell].cost; 
                ms += Spells[spell].cost;
                gameStatus.Add(new Tuple<int, int>(spell, turn + Spells[spell].duration));
                spellsUsed += "3 ";
            }
            if (spell == 4)
            {
                //Console.WriteLine($"Player casts Recharge"); 
                my_mana -= Spells[spell].cost; 
                ms += Spells[spell].cost;
                gameStatus.Add(new Tuple<int, int>(spell, turn + Spells[spell].duration));
                spellsUsed += "4 ";
            }

            if (op_hitPoints <= 0)
            {
                //Console.WriteLine($"This kills the Boss, and the Wizard wins");
                return 1;
            }
            return 0;
        }

        public int Player2(bool part2)
        {
            turn++;

            foreach (var item in gameStatus)
            {
                op_hitPoints -= Spells[item.Item1].damage;
                my_mana += Spells[item.Item1].mana;
            }

            for (int i = 0; i < gameStatus.Count; i++)
            {
                if (gameStatus[i].Item2 == turn)
                {
                    if (gameStatus[i].Item1 == 2) 
                        my_armour -= Spells[2].armour;
                    gameStatus.Remove(gameStatus[i]);
                }
            }

            if (op_hitPoints <= 0)
            {
                //Console.WriteLine($"This kills the Boss, and the Wizard wins");
                return 1;
            }
            else
            {
                // Console.WriteLine($"Boss attacks for {op_damage - my_armour} damage"); 
                my_hitPoints -= op_damage - my_armour <= 1? 1 : op_damage - my_armour;

                if (my_hitPoints <= 0)
                {
                    //Console.WriteLine($"This kills the Wizard, and the Boss wins");
                    return -1;
                }
                return 0;
            }
        }
    }
}
