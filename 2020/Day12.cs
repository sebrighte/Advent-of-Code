using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//link
//code

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 12: Rain Risk")]
    class Day12 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            List<string> list = inData.Split("\n").ToList();

            BoatClsPt1 cBoat1 = new BoatClsPt1(list);
            if(!part2)
                yield return cBoat1.GetPositon();

            boatClsPt2 cBoat2 = new boatClsPt2(list);
            if (part2)
                yield return cBoat2.GetPositon();
        }
    }

    class BoatClsPt1
    {
        private int lat;
        private int lon;
        private int dirDeg = 90; //East

        //public string GetPositon()
        public int GetPositon()
        {
            //return string.Format("Lat Long:({0},{1}) Manhattan distance: {2}", lat, lon, Math.Abs(lat) + Math.Abs(lon));
            return Math.Abs(lat) + Math.Abs(lon);
        }

        private string GetStatus()
        {
            return string.Format("Pos :{0},{1} {2} Dir:{3}", lat, lon, Math.Abs(lat) + Math.Abs(lon), dirDeg);
        }

        public BoatClsPt1(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string tmp = list[i];
                char action = tmp.Substring(0, 1).ToCharArray()[0];
                int value = int.Parse(tmp.Substring(1));

                string rawStr = string.Format("Raw: {0}{1} ", action, value);

                switch (action)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                        this.MoveDirection(action, value);
                        break;
                    case 'L':
                    case 'R':
                        this.TurnBoat(action, value);
                        break;
                    case 'F':
                        this.MoveForward(value);
                        break;
                }
            }
        }

        private void MoveForward(int value)
        {
            switch (dirDeg)
            {
                case 0:
                    lat += value;
                    break;
                case 90:
                    lon += value;
                    break;
                case 180:
                    lat -= value;
                    break;
                case 270:
                    lon -= value;
                    break;
            }
        }

        private void TurnBoat(char action, int value)
        {
            if (action == 'L') value = value * -1;
            int tmpValue = dirDeg + value;

            if (tmpValue < 0 || tmpValue > 270)
            {
                if (tmpValue == 360) tmpValue = 0;
                else if (tmpValue > 360) tmpValue -= 360;
                else if (tmpValue < 0) tmpValue += 360;

                if (tmpValue == 360) tmpValue = 0;
            }
            dirDeg = tmpValue;
        }

        private void MoveDirection(char action, int value)
        {
            switch (action)
            {
                case 'N':
                    lat += value;
                    break;
                case 'E':
                    lon += value;
                    break;
                case 'S':
                    lat -= value;
                    break;
                case 'W':
                    lon -= value;
                    break;
            }
        }
    }

    class boatClsPt2
    {
        private int shipLat, shipLon;
        private int wayPointLat = 1, wayPointLon = 10; //Start 1 north 10 west

        //public string GetPositon()
        //{
            //return string.Format("Lat Long:({0},{1}) Manhattan distance: {2}", shipLat, shipLon, Math.Abs(shipLat) + Math.Abs(shipLon));
        //}

        //public string GetPositon()
        public int GetPositon()
        {
            //return string.Format("Lat Long:({0},{1}) Manhattan distance: {2}", lat, lon, Math.Abs(lat) + Math.Abs(lon));
            return Math.Abs(shipLat) + Math.Abs(shipLon);
        }

        public boatClsPt2(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string tmp = list[i];
                char action = tmp.Substring(0, 1).ToCharArray()[0];
                int value = int.Parse(tmp.Substring(1));

                string rawStr = string.Format("Raw: {0}{1} ", action, value);

                switch (action)
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                        this.MoveWaypoint(action, value);
                        break;
                    case 'L':
                    case 'R':
                        this.RotateWayPoint(action, value);
                        break;
                    case 'F':
                        this.MoveByWaypoint(value);
                        break;
                }
            }
        }

        private void MoveByWaypoint(int value)
        {
            shipLat += wayPointLat * value;
            shipLon += wayPointLon * value;
        }

        private void RotateWayPoint(char action, int value)
        {
            int rotation = 0;
            //string tmpStr;
            int tmp;

            if (value != 0)
                rotation = value / 90;

            //tmpStr = string.Format("[{0},{1}]", wayPointLat, wayPointLon);

            for (int i = 0; i < rotation; i++)
            {
                if (action == 'L')
                {
                    tmp = wayPointLat;
                    wayPointLat = wayPointLon;
                    wayPointLon = (tmp * -1);
                    //tmpStr = string.Format("[{0},{1}]", wayPointLat, wayPointLon);
                }

                if (action == 'R')
                {
                    tmp = wayPointLon;
                    wayPointLon = wayPointLat;
                    wayPointLat = (tmp * -1);
                    //tmpStr = string.Format("[{0},{1}]", wayPointLat, wayPointLon);
                }
            }
        }

        private void MoveWaypoint(char action, int value)
        {
            switch (action)
            {
                case 'N':
                    wayPointLat += value;
                    break;
                case 'E':
                    wayPointLon += value;
                    break;
                case 'S':
                    wayPointLat -= value;
                    break;
                case 'W':
                    wayPointLon -= value;
                    break;
            }
        }
    }

    
}