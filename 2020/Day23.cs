using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 23: Crab Cups")]
    class Day23 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day2(input).First();

        private IEnumerable<string> Day1(string inData, bool part2 = false)
        {
            int[] Input = { 2, 1, 9, 3, 4, 7, 8, 6, 5 };

            CupGameV1 gameV1; // = new CupGameV1(test);
            gameV1 = new CupGameV1(Input);

            yield return gameV1.GetResult();
        }

        private IEnumerable<long> Day2(string inData, bool part2 = false)
        {
            int[] Input = { 2, 1, 9, 3, 4, 7, 8, 6, 5 };

            CupGameV2 gameV2; 
            gameV2 = new CupGameV2(Input, 1000000, 10000000);

            yield return gameV2.GetResult();
        }
    }

    class CupGameV1
    {
        List<int> cups;
        int currentCup;
        int destinationCup;
        int move;

        public CupGameV1(int[] arr)
        {
            cups = new List<int>(arr.ToList());
            currentCup = cups[0];
            move = 1;

            for (int i = 0; i < 100; i++)
                PlayRound();
        }

        void PlayRound()
        {
            int[] tmpArr;

            //The crab picks up the three cups that are immediately clockwise of the current cup.
            //They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
            tmpArr = PickUpCups();

            //The crab selects a destination cup: the cup with a label equal to the current cup's label minus one. 
            //If this would select one of the cups that was just picked up, the crab will keep subtracting one until it finds a cup that wasn't just picked up. 
            //If at any point in this process the value goes below the lowest value on any cup's label, it wraps around to the highest value on any cup's label instead.
            GetNextDestimation(currentCup);

            //The crab places the cups it just picked up so that they are immediately clockwise of the destination cup.
            //They keep the same order as when they were picked up.
            InsertCups(tmpArr);

            //The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
            GetNextSelectedCup(currentCup);

            move++;
        }

        //1. The crab picks up the three cups that are immediately clockwise of the current cup.
        //They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
        public int[] PickUpCups()
        {
            int cupLoc = cups.IndexOf(currentCup);
            int[] tmpArr = { cups[cupLoc + 1], cups[cupLoc + 2], cups[cupLoc + 3] };
            cups.RemoveRange(cupLoc + 1, 3);
            return tmpArr;
        }

        //2. The crab selects a destination cup: the cup with a label equal to the current cup's label minus one. 
        //If this would select one of the cups that was just picked up, the crab will keep subtracting one until it finds a cup that wasn't just picked up. 
        //If at any point in this process the value goes below the lowest value on any cup's label, it wraps around to the highest value on any cup's label instead.
        private void GetNextDestimation(int cup)
        {
            int origCup = cup;
            bool nextCupFound = false;

            while (!nextCupFound)
            {
                if (cup == 1) cup = 9;
                else cup = cup - 1;
                if (cups.Contains(cup))
                {
                    destinationCup = cup;
                    nextCupFound = true;
                    if (cup == origCup)
                        nextCupFound = false;
                }
            }
        }

        //3. The crab places the cups it just picked up so that they are immediately clockwise of the destination cup.
        //They keep the same order as when they were picked up.
        public void InsertCups(int[] tmpArr)
        {
            int cupLoc = cups.IndexOf(destinationCup);
            cups.InsertRange(cupLoc + 1, tmpArr);
        }

        //4. The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
        private void GetNextSelectedCup(int cup)
        {
            int cupLoc = cups.IndexOf(currentCup);
            if (cupLoc == 8) cupLoc = 0;
            else cupLoc = cupLoc + 1;
            currentCup = cups[cupLoc];
            cupLoc = cups.IndexOf(currentCup);
            //shuffle cups
            if (cupLoc == 0) return;
            else
            {
                for (int i = 0; i < cupLoc; i++)
                {
                    int tmp = cups[0];
                    cups.RemoveAt(0);
                    cups.Add(tmp);
                }
            }
        }

        //Sort cups for final result
        public string GetResult()
        {
            int cupLoc = cups.IndexOf(1);
            if (cupLoc == 0)
            {
                cups.RemoveAt(0);
                return string.Join("", cups);
            }
            else
            {
                for (int i = 0; i < cupLoc; i++)
                {
                    int tmp = cups[0];
                    cups.RemoveAt(0);
                    cups.Add(tmp);
                }
            }
            cups.RemoveAt(0);
            return string.Join("", cups);
        }
    }

    class CupGameV2 : BaseLine
    {
        LinkedList<int> cupsLinkedList; //Cups list
        LinkedListNode<int>[] cupsHashTable; //Cups hastable lookup
        LinkedListNode<int> currentCup; // Current Cup
        long resultVal;

        /// <summary>
        /// Get Result
        /// </summary>
        /// <returns>Result as long</returns>
        public long GetResult()
        {
            return resultVal;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="initialCups">Starting Cups</param>
        /// <param name="noCups"></param>
        /// <param name="noloop"></param>
        public CupGameV2(int[] initialCups, int noCups, int noloop)
        {
            //Create LinkedList from values given
            cupsLinkedList = new LinkedList<int>(initialCups);

            //set current cup as first cup
            currentCup = cupsLinkedList.First;

            //add additional cups up to newCups
            for (int r = initialCups.Count(); r < noCups; r++)
                cupsLinkedList.AddLast(r + 1);

            //Create nodeArray. Start from 1 not 0 for hashtable
            cupsHashTable = new LinkedListNode<int>[cupsLinkedList.Count + 1];

            //Create arrray of LinkedNodes as hashtable
            for (var Node = cupsLinkedList.First; Node != null; Node = Node.Next)
                cupsHashTable[Node.Value] = Node;

            //Play the game
            for (int r = 0; r < noloop; r++)
            {
               PlayRound();
            //    if (r % (noloop / 15) == 0) DrawTextProgressBar(r, noloop - 1);
            }

            //Get values of 1 + 1 + 2
            LinkedListNode<int> result = cupsHashTable[1];
            long a = result.Next.Value;
            long b = result.Next.Next.Value;
            resultVal = a * b;
        }

        /// <summary>
        /// Get Next destination Cup
        /// </summary>
        /// <param name="cup">Current Cup</param>
        /// <param name="vals">Cups removed</param>
        /// <returns></returns>
        private int GetNextDestimation(int cup, int[] cupsRemoved)
        {
            bool found = false;
            cup = cup - 1;
            if (cup == 0) cup = cupsLinkedList.Max();
            while (!found)
            {
                if (cup == 0) cup = cupsLinkedList.Max();
                if (cupsRemoved.Contains(cup))
                    cup = cup - 1;
                else
                    found = true;
            }
            return cup;
        }

        /// <summary>
        /// Play one round
        /// </summary>
        void PlayRound()
        {
            //The crab picks up the three cups that are immediately clockwise of the current cup.
            //They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
            LinkedListNode<int> cup1 = GetNextNode(currentCup);
            LinkedListNode<int> cup2 = GetNextNode(cup1);
            LinkedListNode<int> cup3 = GetNextNode(cup2);

            int[] remNodesVal = new int[] { cup1.Value, cup2.Value, cup3.Value };

            cupsLinkedList.Remove(cup1);
            cupsLinkedList.Remove(cup2);
            cupsLinkedList.Remove(cup3);

            //The crab selects a destination cup: the cup with a label equal to the current cup's label minus one. 
            //If this would select one of the cups that was just picked up, the crab will keep subtracting one until it finds a cup that wasn't just picked up. 
            //If at any point in this process the value goes below the lowest value on any cup's label, it wraps around to the highest value on any cup's label instead.
            int destinationCupInt = GetNextDestimation(currentCup.Value, remNodesVal);
            var destinationCup = cupsHashTable[destinationCupInt];

            //The crab places the cups it just picked up so that they are immediately clockwise of the destination cup.
            //They keep the same order as when they were picked up.
            cupsLinkedList.AddAfter(destinationCup, cup3);
            cupsLinkedList.AddAfter(destinationCup, cup2);
            cupsLinkedList.AddAfter(destinationCup, cup1);

            //The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
            currentCup = GetNextNode(currentCup);
        }

        /// <summary>
        /// Make list circular
        /// </summary>
        /// <param name="node">Current node</param>
        /// <returns>Next node</returns>
        private LinkedListNode<int> GetNextNode(LinkedListNode<int> node)
        {
            LinkedListNode<int> retVal;
            retVal = node.Next;
            if (retVal == null) //end of list
                retVal = cupsLinkedList.First; // link to start of list
            return retVal;
        }
    }
}
