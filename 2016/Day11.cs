﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 11: Radioisotope Thermoelectric Generators")]
    class Day11 : BaseLine, Solution
    {
        //public object PartOne(string input) => Greedy(ParseInput(input, 0));
        //public object PartTwo(string input) => Greedy(ParseInput(input, 2));

        //public object PartOne(string input) => Simple();
        //public object PartTwo(string input) => Simple(true);

        public object PartOne(string input) => aStarAlgorithm(input);
        public object PartTwo(string input) => aStarAlgorithm(input, 2);

        private object aStarAlgorithm(string inData, int add = 0)
        {
            AAlgo algo = new AAlgo();
            return algo.start(ParseInput(inData, add)) + " (A*)";
        }

        private object Simple(bool day2 = false)
        {
            simpleMethod sm = new simpleMethod();
            return sm.Run(day2) + " (Math)";
        }

        private object Greedy(List<string[]> RTGIn)
        {
            Greedy gr = new Greedy();
            GState gs = new GState(RTGIn, null, 0, 0);
            return gr.iterate(gs) + " (Greedy)";
        }

        private List<string[]> ParseInput(string inData, int additional)
        {
            //inData = "The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.\r\nThe second floor contains a hydrogen generator.\r\nThe third floor contains a lithium generator.\r\nThe fourth floor contains nothing relevant.";

            List<string[]> RTG = new List<string[]>();
            List<string> input = inData.Split("\r\n").ToList();

            foreach (var (line, index) in input.WithIndex())
            {
                var match = Regex.Matches(line, @"((\w*) generator)|((\w*)-compatible microchip)");

                string[] floor = new string[match.Count];

                for (int i = 0; i < match.Count; i++)
                {
                    //if (fullfat)
                       // floor[i] = (match[i].Value.Split(" ")[0].Substring(0, 1).ToUpper() + match[i].Value.Split(" ")[1].Substring(0, 1).ToUpper());
                    //else
                        floor[i] = match[i].Value.Split(" ")[1].Substring(0, 1);
                }

                floor = floor.Select(a => a).OrderBy(a => a).ToArray();
                RTG.Add(floor);
            }

            List<string> a = RTG[0].ToList();
            for (int i = 0; i < additional; i++)
            {
                a.Add("g");
                a.Add("m");
            }
            RTG[0] = a.ToArray();

            return RTG;
        }
    }

    /// <summary>
    /// Greedy, check all routes/////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    #region Greedy Solution

    class GState
    {
        public List<string[]> RTG;
        public GState Parent;
        public int Moves;
        public string value;
        public int currentFloor;

        public GState(List<string[]> RTGIn, GState parent, int moves, int floor)
        {
            RTG = RTGIn.ToList();
            Parent = parent;
            Moves = moves;
            currentFloor = floor;
        }
    }

    class Greedy
    {
        HashSet<string> history = new HashSet<string>();
        int maxIteration = 1000;

        public int iterate(GState gs)
        {
            if (gs.Moves > maxIteration)
            {
                return 0;
            }
            if (gs.RTG[3].Count() == gs.RTG.SelectMany(a => a).Count())
            {
                if (gs.Moves <= maxIteration)
                {
                    maxIteration = gs.Moves;
                }
                return 0;
            }
            else
            {
                var perms = GetPermutations(gs);

                gs.Moves++;

                foreach (var item in perms)
                {
                    var items = item.Split(",");

                    if (gs.currentFloor == 0 || gs.currentFloor == 1 || gs.currentFloor == 2)
                    {
                        var dupGs = new GState(gs.RTG, gs, gs.Moves, gs.currentFloor + 1);
                        List<string> listThis = new List<string>(dupGs.RTG[gs.currentFloor]);
                        List<string> listUp = new List<string>(dupGs.RTG[gs.currentFloor + 1]);

                        foreach (var q in items)
                        {
                            listThis.Remove(q);
                            listUp.Add(q);
                        }

                        dupGs.RTG[gs.currentFloor] = listThis.ToArray();
                        dupGs.RTG[gs.currentFloor + 1] = listUp.ToArray();

                        if (CheckIfValid(dupGs))
                        {
                            iterate(dupGs);
                        }
                    }

                    if (gs.currentFloor == 3 || gs.currentFloor == 1 || gs.currentFloor == 2)
                    {
                        var dupGs2 = new GState(gs.RTG, gs, gs.Moves, gs.currentFloor - 1);
                        List<string> listThis = new List<string>(gs.RTG[gs.currentFloor]);
                        List<string> listDown = new List<string>(gs.RTG[gs.currentFloor - 1]);

                        foreach (var q in items)
                        {
                            listThis.Remove(q);
                            listDown.Add(q);
                        }

                        dupGs2.RTG[gs.currentFloor] = listThis.ToArray();
                        dupGs2.RTG[gs.currentFloor - 1] = listDown.ToArray();

                        if (CheckIfValid(dupGs2))
                        {
                            iterate(dupGs2);
                        }
                    }
                }
            }
            return maxIteration;
        }

        private List<string> GetPermutations(GState gs)
        {
            HashSet<string> tmp = new HashSet<string>();

            foreach (var item1 in gs.RTG[gs.currentFloor])
            {
                tmp.Add(item1);
                foreach (var item2 in gs.RTG[gs.currentFloor])
                {
                    if (item1 != item2)
                        tmp.Add(string.Compare(item1, item2) >= 0 ? $"{item1},{item2}" : $"{item2},{item1}");
                }
            }
            return tmp.Select(a => a).OrderBy(a => a.Length).Reverse().ToList(); ;
        }

        public bool CheckIfValid(GState gs)
        {
            string result = "";
            foreach (var (floor, index) in gs.RTG.WithIndex())
            {
                result += $"";
                result += string.Join($"", floor.OrderBy(a => a).ToArray());
                result += $"|";
            }

            gs.value = result;

            if (history.Contains(result))
            {
                return false;
            }

            history.Add(result);
            return true;
        }

        private void Draw(GState gs)
        {
            Console.WriteLine($"\nMove: {gs.Moves}");
            //System.IO.File.AppendAllText(@"C:\out.txt", $"Move: {moves}/{ctr++}\n");

            for (int i = gs.RTG.Count - 1; i > -1; i--)
            {
                string a = i == gs.currentFloor ? "e" : "";
                Console.WriteLine($"F{i + 1}{a}\t {string.Join(" ", gs.RTG[i])}");
                //System.IO.File.AppendAllText(@"C:\out.txt", $"F{i + 1}{a}\t {string.Join("\t", RTG[i])}\n");
            }
            Console.WriteLine();
            //System.IO.File.AppendAllText(@"C:\out.txt", $"\n");
        }
    }
    #endregion Greedy Solution

    /// <summary>
    /// Work out with equation///////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    #region Simple Maths Solution

    struct TComps //Day1Simple
    {
        public string element;
        public int gen_floor;
        public int mic_floor;

        public void set(string e, int g, int m)
        {
            element = e;
            gen_floor = g;
            mic_floor = m;
        }
    }

    class simpleMethod
    {
        public int Run(bool part2 = false)
        {
            TComps[] comps = new TComps[part2 ? 7 : 5];

            comps[0].set("T", 2, 3);
            comps[1].set("S", 1, 1);
            comps[2].set("P", 1, 1);
            comps[3].set("R", 2, 2);
            comps[4].set("C", 2, 2);
            if (part2)
            {
                comps[5].set("X", 1, 1);
                comps[6].set("Y", 1, 1);
            }

            int[] items_per_floor = { 0, 0, 0, 0, 0 };
            int item_count = comps.Length * 2;

            foreach (var item in comps)
            {
                items_per_floor[item.gen_floor] += 1;
                items_per_floor[item.mic_floor] += 1;
            }

            int moves = 0;
            int floor = 1;

            while (items_per_floor[4] != item_count)
            {
                moves += 2 * items_per_floor[floor] - 3;
                items_per_floor[floor + 1] += items_per_floor[floor];
                floor += 1;
            }
            return moves;
        }
    }
    #endregion

    /// <summary>
    /// Find optimal path using A*///////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    #region A* Agorithm Solution

    class State
    {
        public List<string[]> RTG { get; set; }
        public int Cost { get; set; }
        public int ToTCost_h => Cost + Value_g;
        public int Value_g { get; set; }
        public int Floor { get; set; }
        public string Serialized { get; set; }
        public State Parent { get; set; }

        public State(State parentIn, List<string[]> RTGIn, int floorIn)
        {
            RTG = RTGIn.ToList();
            Floor = floorIn;
            int max = 4 * RTG.SelectMany(a => a).Count();

            Parent = parentIn;
            Cost = Parent is null? 0 : parentIn.Cost + 1;
            UpdateState();
        }

        public void UpdateState()
        {
            GetValue();
            SerializeState();
        }

        public bool CheckValid()
        {
            GetValue();
            SerializeState();

            foreach (var floor in RTG)
            {
                int m = floor.Select(a => a).Where(a => a.Equals("m")).Count();
                int g = floor.Select(a => a).Where(a => a.Equals("g")).Count();

                if (g > 0 && m > g + 1)
                {
                    return false;
                }
            }
            return true;
        }

        private void SerializeState()
        {
            string result = $"{Floor} ";
            //result = "";
            foreach (var (floor, index) in RTG.WithIndex())
            {
                result += $"";
                result += string.Join($"", floor.OrderBy(a => a).ToArray());
                result += $"|";

            }
            Serialized = result;
        }

        private void GetValue()
        {
            int retVal = 0;
            retVal += RTG[0].Select(a => a).Count() * 3;
            retVal += RTG[1].Select(a => a).Count() * 2;
            retVal += RTG[2].Select(a => a).Count() * 1;
            retVal += RTG[3].Select(a => a).Count() * 0;
            Value_g = retVal;
        }
    }

    class AAlgo
    {
        public int start(List<string[]> RTG)
        {
            var startState = new State(null, RTG, 0);

            List<string[]> RTGDup = RTG.DupList().ToList();
            string[] tmp = RTG.SelectMany(a => a).ToArray();
            RTGDup[0] = RTGDup[1] = RTGDup[2] = new string[0];
            RTGDup[3] = tmp;

            var finishState = new State(null, RTGDup, 3);

            var activeStates = new List<State>();
            activeStates.Add(startState);
            var visitedStates = new List<State>();

            while (activeStates.Any())
            {
                var checkState = activeStates.OrderBy(x => x.Value_g).First();

                if (checkState.Value_g == finishState.Cost)
                {
                    return checkState.Cost;
                }

                visitedStates.Add(checkState);
                activeStates.Remove(checkState);

                var walkableStates = GetWalkableStates(checkState, finishState);

                foreach (var walkableState in walkableStates)
                {
                    //We have already visited this tile so we don't need to do so again!
                   if (visitedStates.Any(x => x.Serialized == walkableState.Serialized))
                       continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
                    if (activeStates.Any(x => x.Serialized == walkableState.Serialized))
                    {
                        var existingState = activeStates.First(x => x.Serialized == walkableState.Serialized);
                        if (existingState.ToTCost_h > checkState.ToTCost_h)
                        {
                            activeStates.Remove(existingState);
                            activeStates.Add(walkableState);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeStates.Add(walkableState);
                    }
                }
            }
            Console.WriteLine("No Path Found!");
            return 0;
        }

        private static List<State> GetWalkableStates(State currentState, State targetState)
        {
            HashSet<string> tmp = new HashSet<string>();

            foreach (var item1 in currentState.RTG[currentState.Floor])
            {
                tmp.Add(item1);
                foreach (var item2 in currentState.RTG[currentState.Floor])
                {
                    if (item1 != item2)
                        tmp.Add(string.Compare(item1, item2) >= 0 ? $"{item1},{item2}" : $"{item2},{item1}");
                }
            }
            tmp.Reverse();
            List<State> validStates = GetValidStates(currentState, tmp.ToList());
            return validStates;
        }

        public static List<State> GetValidStates(State currentState, List<string> perms)
        {
            List<State> validStates = new List<State>();

            foreach (var item in perms)
            {
                var items = item.Split(",");

                if (currentState.Floor == 0 || currentState.Floor == 1 || currentState.Floor == 2)
                {
                    var dupRTG = new State(currentState, currentState.RTG, currentState.Floor + 1);
                    List<string> listThis = new List<string>(dupRTG.RTG[currentState.Floor]);
                    List<string> listUp = new List<string>(dupRTG.RTG[currentState.Floor + 1]);

                    foreach (var q in items)
                    {
                        listThis.Remove(q);
                        listUp.Add(q);
                    }

                    dupRTG.RTG[currentState.Floor] = listThis.ToArray();
                    dupRTG.RTG[currentState.Floor + 1] = listUp.ToArray();

                    if (dupRTG.CheckValid())
                    {
                        dupRTG.UpdateState();
                        validStates.Add(dupRTG);
                    }
                }

                if (currentState.Floor == 3 || currentState.Floor == 1 || currentState.Floor == 2)
                {
                    var dupRTG = new State(currentState, currentState.RTG, currentState.Floor - 1);
                    List<string> listThis = new List<string>(dupRTG.RTG[currentState.Floor]);
                    List<string> listDown = new List<string>(dupRTG.RTG[currentState.Floor - 1]);

                    foreach (var q in items)
                    {
                        listThis.Remove(q);
                        listDown.Add(q);
                    }

                    dupRTG.RTG[currentState.Floor] = listThis.ToArray();
                    dupRTG.RTG[currentState.Floor - 1] = listDown.ToArray();

                    if (dupRTG.CheckValid())
                    {
                        dupRTG.UpdateState();
                        validStates.Add(dupRTG);
                    }
                }
            }
            return validStates;
        }
    }
    #endregion a
}
