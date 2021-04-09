using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 22: Grid Computing")]
    class Day22 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(ParseData(input)).First();
        public object PartTwo(string input) => Solver(ParseData(input)).Last();

        private List<Node> ParseData(string inData)
        {
            var nodes = new List<Node>();
            foreach (var item in inData.Split("\r\n").ToList().Skip(2))
            {
                nodes.Add(new Node(item));
            }
            return nodes;
        } 

        private IEnumerable<object> Solver(List<Node> nodes)
        {
            yield return (from A in nodes
                          from B in nodes
                          where A.used > 0 && A != B && A.used < B.avail
                          select A).Count();

            //Get the data node
            Node dataNode = nodes.Select(a => a).Where(a => a.y == 0 && a.x == nodes.Select(a => a.x).Max()).First();
            //Get the home node
            Node homeNode = nodes.Select(a => a).Where(a => a.y == 0 && a.x == 0).First();
            //find the empty node
            var emptyNode = nodes.Where(a => a.size == a.avail).First();

            //find route to goal
            var steps = findRouteToNode(nodes, emptyNode, dataNode).Count();
            
            //find route to node
            //each move to node takes 5 moves to move data (last move not needed)
            steps += (findRouteToNode(nodes, dataNode, homeNode).Count() - 1) * 5;

            yield return steps;
        }

        //Credit to https://dotnetcoretutorials.com/2020/07/25/a-search-pathfinding-algorithm-in-c/
        public List<TileNode> findRouteToNode(List<Node> nodes, Node startNode, Node endNode)
        {
            var start = new TileNode();
            start.X = startNode.x;
            start.Y = startNode.y;

            var finish = new TileNode();
            finish.X = endNode.x;
            finish.Y = endNode.y;

            var activeNodes = new List<TileNode>();
            activeNodes.Add(start);
            var visitedNodes = new List<TileNode>();

            while (activeNodes.Any())
            {
                var checkTile = activeNodes.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    var route = new List<TileNode>();

                    var tile = checkTile;
                    while (true)
                    {
                        route.Add(tile);
                        tile = tile.Parent;
                        if (tile == null)
                        {
                            route.RemoveAt(0);
                            return route;
                        }
                    }
                }
                visitedNodes.Add(checkTile);
                activeNodes.Remove(checkTile);

                var ConnectedNodes = GetConnectedNodes(nodes, checkTile, finish);

                foreach (var walkableTile in ConnectedNodes)
                {
                    if (visitedNodes.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    if (activeNodes.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeNodes.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeNodes.Remove(existingTile);
                            activeNodes.Add(walkableTile);
                        }
                    }
                    else
                    {
                        //We've never seen this node before so add it to the list. 
                        activeNodes.Add(walkableTile);
                    }
                }
            }
            Console.WriteLine("No Path Found!");
            return null;
        }

        private static Node GetNodeFromTag(List<Node> nodes, string nodeTag)
        {
            return nodes.Select(a=>a).Where(node => node.tag == nodeTag).First();
        }

        private static List<TileNode> GetConnectedNodes(List<Node> nodes, TileNode currentTile, TileNode targetTile)
        {
            var possibleTiles = new List<TileNode>()
            {
                new TileNode { X = currentTile.X,     Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileNode { X = currentTile.X,     Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileNode { X = currentTile.X - 1, Y = currentTile.Y    , Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileNode { X = currentTile.X + 1, Y = currentTile.Y    , Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            return (from tileVar in possibleTiles
                    where tileVar.X >= 0 && tileVar.X <= nodes.Select(a => a.x).Max()
                    where tileVar.Y >= 0 && tileVar.Y <= nodes.Select(a => a.y).Max()
                    where GetNodeFromTag(nodes, tileVar.tag).used < GetNodeFromTag(nodes, currentTile.tag).size
                    select tileVar).ToList();
        }

        public void DrawNetwork(List<Node> nodes, Node endNode, List<TileNode> route = null)
        {
            Console.WriteLine();

            List<Node> tmpListNode = new List<Node>();
            int dataSize = endNode.used;

            for (int i = 0; i < nodes.Select(a => a.y).Max() + 1; i++)
            {
                for (int j = 0; j < nodes.Select(a => a.x).Max() + 1; j++)
                {
                    Node testNode = nodes.Select(a => a).Where(a => a.x == j && a.y == i).First();

                        if (testNode.used==0)
                        {
                            Console.Write("_");
                        }
                        else if (testNode.used >= dataSize)
                        {
                            Console.Write("#");
                        }
                        else
                            Console.Write(".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    class TileNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public TileNode Parent { get; set; }
        public string tag => $"{X}-{Y}";

        //The distance is essentially the estimated distance, ignoring walls to our target.  
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }

    class Node
    {
        public int x;
        public int y;
        public string tag => $"{x}-{y}";
        public int size;
        public int used;
        public int avail => size - used;

        public Node(string item)
        {
            while (item.Contains("  "))
            {
                item = item.Replace("  ", " ");
            }

            var s = item.Split(" ");
            var s2 = s[0].Split("-");
            x = s2[1].Substring(1, s2[1].Length - 1).ToInt32();
            y = s2[2].Substring(1, s2[2].Length - 1).ToInt32();
            size = s[1].Substring(0, s[1].Length - 1).ToInt32();
            used = s[2].Substring(0, s[2].Length - 1).ToInt32();
        }
    }
}
