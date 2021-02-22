using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AdventOfCode.Y2015
{
    [ProblemName("Day 12: JSAbacusFramework.io")]
    class Day12 : BaseLine, Solution
    {
        public object PartOne(string input) => Traverse(JsonDocument.Parse(input).RootElement, false);
        public object PartTwo(string input) => Traverse(JsonDocument.Parse(input).RootElement, true);

        int Traverse(JsonElement t, bool part2 = false)
        {
            int sum = 0;
            if (t.ValueKind == JsonValueKind.Object)
            {
                var p = t.EnumerateObject();
                foreach (var item in p)
                {
                    if (item.Value.ValueKind != JsonValueKind.Object)
                        sum += Traverse(item.Value, part2);
                    if (item.Value.ValueKind == JsonValueKind.Object)
                        if (!part2 || !item.Value.EnumerateObject().Any(p => p.Value.ValueKind == JsonValueKind.String && p.Value.GetString() == "red"))
                            sum += Traverse(item.Value, part2);
                }
            }
            if (t.ValueKind == JsonValueKind.Array)
            {
                var p = t.EnumerateArray();
                foreach (var item in p)
                {
                    if (item.ValueKind != JsonValueKind.Object)
                        sum += Traverse(item, part2);
                    if (item.ValueKind == JsonValueKind.Object)
                        if (!part2 || !item.EnumerateObject().Any(p => p.Value.ValueKind == JsonValueKind.String && p.Value.GetString() == "red"))
                            sum += Traverse(item, part2);
                }
            }
            if (t.ValueKind == JsonValueKind.Number) sum += t.GetInt32();
            return sum;
        }
    }
}
