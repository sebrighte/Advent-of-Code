using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day24: Air Duct Spelunking")]
    class Day24 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver(input);
        public object PartTwo(string input) => Solver(input,true);

        private Dictionary<string, int> savedRoutes = new Dictionary<string, int>();
        private int minVal = int.MaxValue;

        private int Solver(string inData,bool part2 = false)
        {
            //inData = "###########\r\n#0.1.....2#\r\n#.#######.#\r\n#4.......3#\r\n###########\r\n";

            minVal = int.MaxValue;
            //savedRoutes.Clear();
            List<string> map = inData.Split("\r\n").ToList();
            var positions = int.Parse(inData.Where(a => char.IsDigit(a)).Max().ToString());
            string nodes = string.Join("", Enumerable.Range(1, positions).Select(x => x).OrderBy(x => x));
            TileDay24BestRoute start = new TileDay24BestRoute(0, 0, nodes,null);
            return CalculateBestRoute(map, start, part2);
        }

        public int CalculateBestRoute(List<string> map, TileDay24BestRoute node, bool part2)
        {
            int park = node.distanceTot + (part2? CalcRouteTryStored(map, node.id, 0) : 0);

            if (park > minVal) return 0;

            if (node.permitted.Length == 0)
                if (park < minVal) minVal = park;

            foreach (var move in BestRouteGetNextMoves(map, node))
                CalculateBestRoute(map, move, part2);

            return minVal;
        }

        private List<TileDay24BestRoute> BestRouteGetNextMoves(List<string> map, TileDay24BestRoute current)
        {
            List<TileDay24BestRoute> moves = new List<TileDay24BestRoute>();

            foreach (var item in current.permitted)
            {
                int target = int.Parse(item.ToString());
                string permitted = current.permitted.ReplaceFirst(item.ToString(), "");
                int route = CalcRouteTryStored(map, current.id, target);
                var tmp = new TileDay24BestRoute(target, route , permitted, current);
                moves.Add(tmp);
            }
            return moves.OrderBy(x=>x.distance).ToList();
        }

        private int CalcRouteTryStored(List<string> map, int Start, int End)
        {
            int retVal = 0;
            string routeSearch = $"{Math.Max(Start, End)}-{Math.Min(Start, End)}";
            if (savedRoutes.ContainsKey(routeSearch))
                retVal = savedRoutes[routeSearch];
            else 
            {
                retVal = CalcRoute(map, Math.Max(Start, End), Math.Min(Start, End));
                savedRoutes.Add(routeSearch, retVal);
            }
            return retVal;
        }

        private int CalcRoute(List<string> map, int StartVal, int EndVal)
        {
            var start = new TileDay24Route();
            start.Y = map.FindIndex(x => x.Contains(StartVal.ToString()));
            start.X = map[start.Y].IndexOf(StartVal.ToString());
            start.Val = StartVal;

            var finish = new TileDay24Route();
            finish.Y = map.FindIndex(x => x.Contains(EndVal.ToString()));
            finish.X = map[finish.Y].IndexOf(EndVal.ToString());
            finish.Val = EndVal;

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<TileDay24Route>();
            activeTiles.Add(start);
            var visitedTiles = new List<TileDay24Route>();

            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    return checkTile.CostDistance;
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetValidTiles(map, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    if (activeTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }
                    }
                    else
                    {
                        activeTiles.Add(walkableTile);
                    }
                }
            }
            return 0;
        }

        private static List<TileDay24Route> GetValidTiles(List<string> map, TileDay24Route currentTile, TileDay24Route targetTile)
        {
            var possibleTiles = new List<TileDay24Route>()
            {
                new TileDay24Route { X = currentTile.X, Y =     currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileDay24Route { X = currentTile.X, Y =     currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new TileDay24Route { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost =     currentTile.Cost + 1 },
                new TileDay24Route { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost =     currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .Where(tile => char.IsDigit(map[tile.Y][tile.X]) || map[tile.Y][tile.X] == '.')
                    .ToList();
        }
    }

    class TileDay24Route
    {
        public int Val { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public TileDay24Route Parent { get; set; }
 
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }

    class TileDay24BestRoute
    {
        public int distance;
        public int id;
        public int value;
        public int distanceTot;
        public string history;
        public string permitted;
        public TileDay24BestRoute parent;

        public TileDay24BestRoute(int i, int d, string p, TileDay24BestRoute p2)
        {
            distance = d;
            history = "";
            if (p2 != null)
            {
                distanceTot = distance + p2.distanceTot;
                history = p2.history + p2.id;
            }
            id = i;
            permitted = p;
            parent = p2;
            value = distance + permitted.Length;
            if (id == 0) value = 1000;

            //var tmp = $"{i} {p2.id} {distance} {distanceTot}";
        }
    }
}

