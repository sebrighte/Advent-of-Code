using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 22: Crab Combat")]
    class Day22 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).Max();
        public object PartTwo(string input) => Day2(input).Max();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            CrabCombatGameV1 game = new CrabCombatGameV1();

            //Player 1:
            game.LoadPlayer1(new int[] { 6, 25, 8, 24, 30, 46, 42, 32, 27, 48, 5, 2, 14, 28, 37, 17, 9, 22, 40, 33, 3, 50, 47, 19, 41 });
            //Player 2:
            game.LoadPlayer2(new int[] { 1, 18, 31, 39, 16, 10, 35, 29, 26, 44, 21, 7, 45, 4, 20, 38, 15, 11, 34, 36, 49, 13, 23, 43, 12 });

            while (!game.Play()) { }
            //Console.WriteLine("Day 22/1: What is the winning player's score? " + game.GetScore());
            yield return game.GetScore();
        }

        private IEnumerable<int> Day2(string inData, bool part2 = false)
        {
            CrabCombatGameV2 game = new CrabCombatGameV2(1);

            //Player 1:
            game.LoadPlayer1(new int[] { 6, 25, 8, 24, 30, 46, 42, 32, 27, 48, 5, 2, 14, 28, 37, 17, 9, 22, 40, 33, 3, 50, 47, 19, 41 });
            //Player 2:
            game.LoadPlayer2(new int[] { 1, 18, 31, 39, 16, 10, 35, 29, 26, 44, 21, 7, 45, 4, 20, 38, 15, 11, 34, 36, 49, 13, 23, 43, 12 });

            while (!game.Play()) { }
            //Console.WriteLine("Day 22/2: What is the winning player's score? " + game.GetScore());
            yield return game.GetScore();
        }

    }

    class CrabCombatGameV1
    {
        Queue<int> Player1Queue = new Queue<int>();
        Queue<int> Player2Queue = new Queue<int>();

        public void LoadPlayer1(int[] cards)
        {
            Player1Queue.Clear();
            foreach (int card in cards)
                Player1Queue.Enqueue(card);
        }

        public void LoadPlayer2(int[] cards)
        {
            Player2Queue.Clear();
            foreach (int card in cards)
                Player2Queue.Enqueue(card);
        }

        public bool Play()
        {
            int p1 = Player1Queue.Peek();
            int p2 = Player2Queue.Peek();

            if (p1 > p2)
            {
                Player1Queue.Dequeue();
                Player2Queue.Dequeue();
                Player1Queue.Enqueue(p1);
                Player1Queue.Enqueue(p2);
            }

            if (p1 < p2)
            {
                Player1Queue.Dequeue();
                Player2Queue.Dequeue();
                Player2Queue.Enqueue(p2);
                Player2Queue.Enqueue(p1);
            }

            if (p1 == p2)
            {
                Player1Queue.Dequeue();
                Player2Queue.Dequeue();
                Player1Queue.Enqueue(p1);
                Player2Queue.Enqueue(p2);
            }

            return (Player1Queue.Count == 0 || Player2Queue.Count == 0);
        }

        public int GetScore()
        {
            int res1 = 0;
            int ctr = 1;

            for (int v = Player1Queue.Count - 1; v > -1; v--)
            {
                res1 += ctr++ * Player1Queue.ElementAt(v);
            }

            int res2 = 0;
            ctr = 1;
            for (int v = Player2Queue.Count - 1; v > -1; v--)
            {
                res2 += ctr++ * Player2Queue.ElementAt(v);
            }

            return res1 > res2 ? res1 : res2;
        }
    }

    class CrabCombatGameV2
    {
        Queue<int> Player1Queue = new Queue<int>();
        Queue<int> Player2Queue = new Queue<int>();
        HashSet<string> pastValues = new HashSet<string>();

        HashSet<int> testint = new HashSet<int>();

        int gameNumber;
        int subGameNumber;
        int roundCtr;
        public int winner;

        private string EnmQueues()
        {
            string retVal = "P1";
            foreach (int card in Player1Queue)
                retVal += card + ",";

            retVal += "P2";
            foreach (int card in Player2Queue)
                retVal += card + ",";

            return retVal;
        }

        public CrabCombatGameV2(int gameNo)
        {
            gameNumber = gameNo;
            subGameNumber = gameNumber + 1;
            roundCtr = 1;
            winner = 0;
        }

        public void LoadPlayer1(int[] cards)
        {
            Player1Queue.Clear();
            foreach (int card in cards)
                Player1Queue.Enqueue(card);
        }

        public void LoadPlayer2(int[] cards)
        {
            Player2Queue.Clear();
            foreach (int card in cards)
                Player2Queue.Enqueue(card);
        }

        public bool Play()
        {
            int p1 = Player1Queue.Peek();
            int p2 = Player2Queue.Peek();

            if (pastValues.Contains(EnmQueues()))
            {
                Player1Queue.Dequeue();
                Player2Queue.Dequeue();
                Player1Queue.Enqueue(p1);
                Player1Queue.Enqueue(p2);
                winner = 1;

                pastValues.Add(EnmQueues());

                return (Player1Queue.Count == 0 || Player2Queue.Count == 0); ;
            }

            pastValues.Add(EnmQueues());

            if (Player1Queue.Count - 1 >= p1 && Player2Queue.Count - 1 >= p2)
            {
                int[] arr1 = Player1Queue.Take(p1 + 1).ToArray().Skip(1).ToArray();
                int[] arr2 = Player2Queue.Take(p2 + 1).ToArray().Skip(1).ToArray();
                CrabCombatGameV2 subGame = new CrabCombatGameV2(subGameNumber++);
                subGame.LoadPlayer1(arr1);
                subGame.LoadPlayer2(arr2);

                bool stopGame = false;
                while (!stopGame)
                {
                    stopGame = subGame.Play();
                }

                if (subGame.winner == 1)
                {
                    Player1Queue.Dequeue();
                    Player2Queue.Dequeue();
                    Player1Queue.Enqueue(p1);
                    Player1Queue.Enqueue(p2);
                    winner = 1;
                }

                if (subGame.winner == 2)
                {
                    Player1Queue.Dequeue();
                    Player2Queue.Dequeue();
                    Player2Queue.Enqueue(p2);
                    Player2Queue.Enqueue(p1);
                    winner = 2;
                }

                roundCtr++;

                return (Player1Queue.Count == 0 || Player2Queue.Count == 0); ;
            }

            if (p1 > p2)
            {
                Player1Queue.Dequeue();
                Player2Queue.Dequeue();
                Player1Queue.Enqueue(p1);
                Player1Queue.Enqueue(p2);
                winner = 1;
            }

            if (p1 < p2)
            {
                Player1Queue.Dequeue();
                Player2Queue.Dequeue();
                Player2Queue.Enqueue(p2);
                Player2Queue.Enqueue(p1);
                winner = 2;
            }

            roundCtr++;

            return (Player1Queue.Count == 0 || Player2Queue.Count == 0);
        }

        public int GetScore()
        {
            int res1 = 0;
            int ctr = 1;

            for (int v = Player1Queue.Count - 1; v > -1; v--)
            {
                res1 += ctr++ * Player1Queue.ElementAt(v);
            }

            int res2 = 0;
            ctr = 1;
            for (int v = Player2Queue.Count - 1; v > -1; v--)
            {
                res2 += ctr++ * Player2Queue.ElementAt(v);
            }

            return res1 > res2 ? res1 : res2;
        }
    }

}
