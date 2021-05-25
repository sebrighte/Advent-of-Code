using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2017
{
    [ProblemName("Day18: Duet")]
    class Day18 : BaseLine, Solution
    {
        public object PartOne(string input) => Solver1(input).First();//4601
        public object PartTwo(string input) => Solver2(input).First();//6858

        private static IEnumerable<object> Solver1(string inData)
        {
            Machine m1 = new Machine(inData.Split("\r\n").ToList(), 0);
            string res = "";
            while (res != "rcv")
            {
                res = m1.Step();
            }
            yield return m1.lastSound;
        }

        private static IEnumerable<object> Solver2(string inData)
        {
            Queue<long> queueM1 = new();
            Queue<long> queueM0 = new();

            //inData = "snd 1\r\nsnd 2\r\nsnd p\r\nrcv a\r\nrcv b\r\nrcv c\r\nrcv d";
            List<string> input = inData.Split("\r\n").ToList();
            Machine m0 = new Machine(input, 0);
            Machine m1 = new Machine(input, 1);
            string res1;
            string res2;
            bool loop = true;

            while (loop)
            {
                res1 = m0.Step(queueM0, queueM1);
                res2 = m1.Step(queueM1, queueM0);
                if (res1 == "waiting..." && res2 == "waiting...") loop = false;
            }
            yield return m1.GetSentCount();
        }
    }

    class Machine
    {
        Dictionary<string, long> Registry = new Dictionary<string, long> { { "a", 0 }, { "b", 0 }, { "f", 0 }, { "i", 0 }, { "p", 0 } };
        List<string> instructions = new();
        int pos = 0;
        int sentVal = 0;
        public long lastSound;

        public Machine(List<string> list, int id)
        {
            instructions = list;
            Registry["p"] = id;
        }

        public int GetSentCount()
        {
            return sentVal;
        }

        public string Step()
        {
            var inst = instructions[pos].Split(" ");
            var ins = inst[0];
            string Regx = inst[1];
            long X = inst[1].IsNumber() ? inst[1].ToInt64() : Registry[inst[1]];
            long Y = 0;
            if (inst.Length == 3) Y = inst[2].IsNumber() ? inst[2].ToInt64() : Registry[inst[2]];

            switch (ins)
            {
                case "snd": lastSound = X; pos++; break;
                case "set": Registry[Regx] = Y; pos++; break;
                case "mul": Registry[Regx] *= Y; pos++; break;
                case "add": Registry[Regx] += Y; pos++; break;
                case "mod": Registry[Regx] %= Y; pos++; break;
                case "rcv": if (Registry[Regx] == 0) { pos++; ins = "rcvWithData"; } break;
                case "jgz": pos += X > 0 ? (int)Y : 1; break;
            }
            return ins;
        }

        public string Step(Queue<long> queueThis, Queue<long> queueOther)
        {
            var inst = instructions[pos].Split(" ");
            var ins = inst[0];
            string Regx = inst[1];
            long X = inst[1].IsNumber() ? inst[1].ToInt64() : Registry[inst[1]];
            long Y = 0;
            if (inst.Length == 3) Y = inst[2].IsNumber() ? inst[2].ToInt64() : Registry[inst[2]];

            switch (ins)
            {
                case "snd": queueOther.Enqueue(X); sentVal++; pos++; break;
                case "set": Registry[Regx] = Y; pos++; break;
                case "mul": Registry[Regx] *= Y; pos++; break;
                case "add": Registry[Regx] += Y; pos++; break;
                case "mod": Registry[Regx] %= Y; pos++; break;
                case "rcv":
                    if (queueThis.Count > 0)
                    {
                        pos++;
                        Registry[Regx] = queueThis.Dequeue();
                    }
                    else
                        ins = "waiting...";
                    break;
                case "jgz": pos += X > 0 ? (int)Y : 1; break;
            }
            return ins;
        }
    }
}
