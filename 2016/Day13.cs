using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2016
{
    [ProblemName("Day 13: A Maze of Twisty Little Cubicles")]
    class Day13 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(CreateMap(1364)).First();
        public object PartTwo(string input) => Day2(CreateMap(1364)).Take(10000).Max();

        public List<string> CreateMap(int magicNumber)
        {
            List<string> map = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                string line = "";
                for (int j = 0; j < 100; j++)
                {
                    switch (j)
                    {
                        case 1 when i == 1:
                            line += 'A';
                            break;
                        case 31 when i == 39:
                            line += 'B';
                            break;
                        default:
                            line += GetPos(j, i, magicNumber);
                            break;
                    }
                }
                map.Add(line);
            }
            return map;
        }

        private IEnumerable<object> Day1(List<string> map)
        {

            HashSet<string> part2 = new HashSet<string>();

            var start = new TileDay13();
            start.Y = map.FindIndex(x => x.Contains("A"));
            start.X = map[start.Y].IndexOf("A");


            var finish = new TileDay13();
            finish.Y = map.FindIndex(x => x.Contains("B"));
            finish.X = map[finish.Y].IndexOf("B");

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<TileDay13>();
            activeTiles.Add(start);
            var visitedTiles = new List<TileDay13>();


            while (true)
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {

                    if (checkTile.Cost < 50)
                        part2.Add($"{checkTile.X}-{checkTile.Y}");
                    //We found the destination and we can be sure (Because the the OrderBy above)
                    //That it's the most low cost option. 
                    var tile = checkTile;
                    //Console.WriteLine("Retracing steps backwards...");
                    while (true)
                    {
                        //Console.WriteLine($"{tile.X} : {tile.Y}");
                        if (map[tile.Y][tile.X] == '.')
                        {
                            var newMapRow = map[tile.Y].ToCharArray();
                            newMapRow[tile.X] = 'O';
                            map[tile.Y] = new string(newMapRow);
                        }
                        tile = tile.Parent;
                        if (tile == null)
                        {
                            //Console.WriteLine();
                            //map.ForEach(x => Console.WriteLine(x));
                            //Console.WriteLine();
                            yield return checkTile.Cost;
                        }
                    }
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(map, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
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
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
            }

            Console.WriteLine("No Path Found!");
            yield return $"";
        }

        private IEnumerable<object> Day2(List<string> map)
        {

            HashSet<string> part2 = new HashSet<string>();

            //map.ForEach(a => Console.WriteLine(a));

            var start = new TileDay13();
            start.Y = map.FindIndex(x => x.Contains("A"));
            start.X = map[start.Y].IndexOf("A");


            var finish = new TileDay13();
            finish.Y = map.FindIndex(x => x.Contains("B"));
            finish.X = map[finish.Y].IndexOf("B");

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new List<TileDay13>();
            activeTiles.Add(start);
            var visitedTiles = new List<TileDay13>();

            bool MoreTiles = true;

            while (MoreTiles)
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.Cost < 50)
                    part2.Add($"{checkTile.X}-{checkTile.Y}");

                yield return part2.Count() + 1;

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var walkableTiles = GetWalkableTiles(map, checkTile, finish);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Any(x => x.X == walkableTile.X && x.Y == walkableTile.Y))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 
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
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Add(walkableTile);
                    }
                }
                if (activeTiles.Count() == 0) MoreTiles = false;
            }
        }

        private char GetPos(int x, int y, int magicNumber)
        {
            int retVal = ((x * x) + (3 * x) + (2 * x * y) + y + (y * y)) + magicNumber;
            return Convert.ToString(retVal, 2).Where(a => a == '1').Count() % 2 == 0 ? '.' : '#';
        }

        private static List<TileDay13> GetWalkableTiles(List<string> map, TileDay13 currentTile, TileDay13 targetTile)
        {
            var possibleTiles = new List<TileDay13>()
            {
                new TileDay13 { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileDay13 { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1},
                new TileDay13 { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new TileDay13 { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));

            var maxX = map.First().Length - 1;
            var maxY = map.Count - 1;

            return possibleTiles
                    .Where(tile => tile.X >= 0 && tile.X <= maxX)
                    .Where(tile => tile.Y >= 0 && tile.Y <= maxY)
                    .Where(tile => map[tile.Y][tile.X] == '.' || map[tile.Y][tile.X] == 'B')
                    .ToList();
        }
    }

    class TileDay13
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public TileDay13 Parent { get; set; }
 
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }
    }
}

