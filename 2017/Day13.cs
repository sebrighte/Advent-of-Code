using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2017
{
    [ProblemName("Day13: Packet Scanners")]
    class Day13 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver1(input).First();
        public object PartTwo(string input) => Solver2(input).First();

        private List<Port> ConfigureFirewall(string inData)
        {
            List<Port> firewall = new();
            //inData = "0: 3\r\n1: 2\r\n4: 4\r\n6: 4";

            List<string> input = inData.Split("\r\n").ToList();
            foreach (var item in input)
            {
                var tmp = item.RemoveWhitespace().Split(":");
                firewall.Add(new Port(tmp[0].ToInt32(), tmp[1].ToInt32()));
            }

            for (int i = 0; i < firewall.Select(a => a.id).Max(); i++)
            {
                var retPort = firewall.Select(a => a).Where(a => a.id == i).FirstOrDefault();
                if (retPort == null) firewall.Add(new Port(i, 0));
            }
            return firewall.OrderBy(x => x.id).ToList();
        }

        private IEnumerable<object> Solver1(string inData)
        {
            var firewall = ConfigureFirewall(inData);
            int retval = 0;
            for (int i = 0; i < firewall.Count; i++)
            {
                retval += firewall[i].GetStatus();
                foreach (var item in firewall) item.Step();
            }
            yield return $"{retval}";
        }

        private IEnumerable<object> Solver2(string inData)
        {
            var firewall = ConfigureFirewall(inData);
            int cnt = 0;
            bool found = true;

            while(true)
            {
                found = true;
                foreach (var (item,index) in firewall.WithIndex())
                {
                    if (item.depth!=0 && (cnt + index) % ((item.depth * 2) - 2) == 0)
                    {
                        found = false;
                        break;
                    }
                }
                if (found) yield return cnt;
                cnt++;
            }
        }
    }

    class Port
    {
        int status;
        public int depth;
        bool down; //0 down, 1 up
        public int id;

        public Port (int i, int d)
        {
            id = i;
            depth = d;
            status = 0;
            down = true;
        }

        public int GetStatus()
        {
            if (depth > 0 && status == 0)
                return id * depth;
            return 0;
        }

        public void Step(int cnt = 1)
        {
            if (depth == 0) return;
            if (depth > 0)
                cnt = cnt % ((2 * depth) - 2);
            for (int i = 0; i < cnt; i++)
            {
                if (down) //down
                {
                    if (status != depth - 1)
                        status++;
                    else
                    {
                        status--;
                        down = !down;
                    }
                }
                else //up
                {
                    if (status != 0)
                        status--;
                    else
                    {
                        status++;
                        down = !down;
                    }
                }
            }
        }
    }
}
