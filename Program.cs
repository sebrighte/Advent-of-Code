using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string day = "";
            string year = "";

            foreach (var item in args)
            {
                if (item.Contains("day")) day = item.Replace("day=", "");
                if (item.Contains("year")) year = item.Replace("year=", "");
            }
            //if (year == "") year = "2015";

            Console.WriteLine("\n     Advent of Code\n=========================\nhttps://adventofcode.com/\n");

            if (day == "")
            {
                var tSolutions3 = Assembly.GetEntryAssembly()!.GetTypes()
                   .Where(t => t.GetTypeInfo().IsClass && typeof(Solution).IsAssignableFrom(t))
                   .OrderBy(t => t.FullName)
                   .ToArray();

                var tsolversSelected = from t in tSolutions3
                                       orderby SolutionExtensions.Year(t), SolutionExtensions.Day(t)
                                           where year.Contains(SolutionExtensions.Year(t))
                                           select t;
                if (year == "")
                {
                    tsolversSelected = from t in tSolutions3
                                            orderby SolutionExtensions.Year(t), SolutionExtensions.Day(t)
                                            select t;
                }

                if (tsolversSelected.Count() == 0)
                {
                    var colour = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Sorry no Solutions for year {year} cannot be found");
                    Console.ForegroundColor = colour;
                    return;
                }

                Solution[] Days = new Solution[tsolversSelected.Count()];
                foreach (var (t,index) in tsolversSelected.WithIndex())
                {
                    Days[index] = Activator.CreateInstance(t) as Solution;
                }
                AdventOfCode.Runner.ProcessDays(Days);
            }
            else
            {
                var tSolutionsArgs = Assembly.GetEntryAssembly()!.GetTypes()
                    .Where(t => t.GetTypeInfo().IsClass && typeof(Solution).IsAssignableFrom(t) && t.Name.Contains(day))
                    .OrderBy(t => t.FullName)
                    .ToArray();

                var tsolversSelectedArgs = from t in tSolutionsArgs
                                       orderby SolutionExtensions.Year(t), SolutionExtensions.Day(t)
                                       where year.Contains(SolutionExtensions.Year(t))
                                       select t;

                if (tsolversSelectedArgs.Count() != 0)
                {
                    Solution[] Days = new Solution[tsolversSelectedArgs.Count()];
                    foreach (var (t, index) in tsolversSelectedArgs.WithIndex())
                    {
                        Days[index] = Activator.CreateInstance(t) as Solution;
                    }
                    AdventOfCode.Runner.ProcessDays(Days);
                }
                else
                {
                    var colour = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Sorry Class {day} cannot be found");
                    Console.ForegroundColor = colour;
                }
            }
            Console.WriteLine("\n\nPress as Key to exit");
            Console.ReadKey();
        }
    }
}