using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Runner
    {
        private static void WriteLine(ConsoleColor color = ConsoleColor.Gray, string text = "")
        {
            Write(color, text + "\n");
        }
        private static void Write(ConsoleColor color = ConsoleColor.Gray, string text = "")
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = c;
        }

        public static void ProcessDay(Solution day)
        {
            Solution[] sol = new Solution[1];
            sol[0] = day;
            ProcessDays(sol);
        }

        private static string GetNormalizedInput(string file)
        {
            var input = File.ReadAllText(file);
            if (input.EndsWith("\n"))
            {
                input = input.Substring(0, input.Length - 1);
            }
            return input;
        }

        public static void ProcessDays(Solution[] solutions)
        {
            Console.WriteLine($"Advent of Code - {solutions[0].Year()}");
            string currentyear = solutions[0].Year();
            foreach (Solution solution in solutions)
            {
                if (solution != null)
                {
                    var year = "";
                    if (solution.Year() != currentyear)
                    {
                        //currentyear = solution.Year();
                        currentyear = year = solution.Year();
                        Console.WriteLine($"\nAdvent of Code - {year}");

                    }
                    else
                        year = solution.Year();

                    var indent = "\t";
                    var status = "✓";
                    Console.WriteLine();
                    Console.WriteLine($"{year} - {solution.GetName()}");
                    var answers = new List<string>();
                    var stopwatch = Stopwatch.StartNew();

                    answers.Add(solution.ToString());

                    var input = "";
                    var dayName = solution.GetName().Split(':')[0].Replace(" ", "");
                    string fileName = $@"..\..\..\{year}\data\{dayName}input.txt";
                    if (File.Exists(fileName))
                    {
                        input = GetNormalizedInput(fileName);
                    }
                    else
                    {
                        var colour = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{indent}? No Input File {dayName}");
                        Console.WriteLine();
                        Console.ForegroundColor = colour;
                        //Console.WriteLine($"Input File {fileName} not found");
                        //continue;
                    }


                    for (int part = 1; part < 3; part++)
                    {
                        var solutionResult = part == 1 ? solution.PartOne(input) : solution.PartTwo(input);

                        answers.Add($"Part {part}: {solutionResult}");
                        var ticks = stopwatch.ElapsedTicks;
                        Write(ConsoleColor.DarkGreen, $"{indent}{status}");
                        Console.Write($" {solutionResult} ");
                        var diff = ticks * 1000.0 / Stopwatch.Frequency;

                        WriteLine(
                            diff > 15000 ? ConsoleColor.Red :
                            diff > 10000 ? ConsoleColor.Yellow :
                            ConsoleColor.DarkGreen,
                            diff > 1000 ? $"({(diff / 1000).ToString("F1")} s)" : $"({diff.ToString("F3")} ms)"
                        );
                        stopwatch.Restart();
                        //Console.Beep();
                    }

                    string answerfileName = $@"..\..\..\{year}\answers\{dayName}Output.txt";
                    System.IO.File.WriteAllLines(answerfileName, answers);
                }
            }
        }
    }
}
