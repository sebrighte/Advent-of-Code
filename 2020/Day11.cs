using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 11: Seating System")]
    class Day11 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private static char[,] cSeatArray;
        private static int cListRow;
        private static int cListCol;
        enum DayPart { Part1, Part2 };


        private IEnumerable<int> Day1(string inData, bool part2 = false)
        {
            List<string> list = inData.Split("\n").ToList();

            int ctrLatest;
            int ctrLast;
            int ctr;

            //Part 1
            SetSeatArray(list);
            ctrLatest = -1;
            ctrLast = 0;
            ctr = 0;
            while (ctrLatest != ctrLast)
            {
                ctrLast = ctrLatest;
                BookCheckSeats(DayPart.Part1);
                ctrLatest = CountBookedSeats();
                ctr++;
                //ShowSeats();
            }

            if (!part2)
                yield return ctrLast;

            //Part 2
            SetSeatArray(list);
            ctrLatest = -1;
            ctrLast = 0;
            ctr = 0;
            while (ctrLatest != ctrLast)
            {
                ctrLast = ctrLatest;
                BookCheckSeats(DayPart.Part2);
                ctrLatest = CountBookedSeats();
                //DrawTextProgressBar(ctr, 85);
                ctr++;
            }

            if (part2)
                yield return ctrLast;
        }

        private static void BookCheckSeats(DayPart part)
        {
            List<string> checkBook = new List<string>(0);
            for (int i = 1; i < cListRow - 1; ++i)
            {
                for (int j = 1; j < cListCol - 1; ++j)
                {
                    if (part == DayPart.Part1)
                    {
                        string str = BookAndCheckSeatDay1(i, j);
                        if (str != "") checkBook.Add(str);
                    }
                    if (part == DayPart.Part2)
                    {
                        string str = BookAndCheckSeatDay2(i, j);
                        if (str != "") checkBook.Add(str);
                    }
                }
            }

            for (int t = 0; t < checkBook.Count; t++)
            {
                char a = checkBook[t].Split(',')[0].ToCharArray()[0];
                int r = int.Parse(checkBook[t].Split(',')[1]);
                int c = int.Parse(checkBook[t].Split(',')[2]);
                cSeatArray[r, c] = a;
            }
        }

        private static string BookAndCheckSeatDay2(int row, int col)
        {
            int loopVal = Math.Max(cListRow, cListCol);
            int[] bookedSeats = { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int p = 1; p < loopVal; p++)
            {
                int rowValU = row - p;
                int rowValD = row + p;
                int colValL = col - p;
                int colValR = col + p;

                if (rowValU > 0)
                {
                    if (cSeatArray[rowValU, col] == '#') if (bookedSeats[0] == 0) bookedSeats[0] = 1;
                    if (cSeatArray[rowValU, col] == 'L') if (bookedSeats[0] == 0) bookedSeats[0] = 2;
                }
                if (rowValD < cListRow - 1)
                {
                    if (cSeatArray[rowValD, col] == '#') if (bookedSeats[4] == 0) bookedSeats[4] = 1;
                    if (cSeatArray[rowValD, col] == 'L') if (bookedSeats[4] == 0) bookedSeats[4] = 2;
                }
                if (colValL > 0)
                {
                    if (cSeatArray[row, colValL] == '#') if (bookedSeats[6] == 0) bookedSeats[6] = 1;
                    if (cSeatArray[row, colValL] == 'L') if (bookedSeats[6] == 0) bookedSeats[6] = 2;
                }
                if (colValR < cListCol - 1)
                {
                    if (cSeatArray[row, colValR] == '#') if (bookedSeats[2] == 0) bookedSeats[2] = 1;
                    if (cSeatArray[row, colValR] == 'L') if (bookedSeats[2] == 0) bookedSeats[2] = 2;
                }
                if ((rowValU > 0) && (colValR < cListCol - 1))
                {
                    if (cSeatArray[rowValU, colValR] == '#') if (bookedSeats[1] == 0) bookedSeats[1] = 1;
                    if (cSeatArray[rowValU, colValR] == 'L') if (bookedSeats[1] == 0) bookedSeats[1] = 2;
                }
                if ((rowValD < cListRow - 1) && (colValR < cListCol - 1))
                {
                    if (cSeatArray[rowValD, colValR] == '#') if (bookedSeats[3] == 0) bookedSeats[3] = 1;
                    if (cSeatArray[rowValD, colValR] == 'L') if (bookedSeats[3] == 0) bookedSeats[3] = 2;
                }
                if ((rowValD < cListRow - 1) && (colValL > 0))
                {
                    if (cSeatArray[rowValD, colValL] == '#') if (bookedSeats[5] == 0) bookedSeats[5] = 1;
                    if (cSeatArray[rowValD, colValL] == 'L') if (bookedSeats[5] == 0) bookedSeats[5] = 2;
                }
                if ((rowValU > 0) && (colValL > 0))
                {
                    if (cSeatArray[rowValU, colValL] == '#') if (bookedSeats[7] == 0) bookedSeats[7] = 1;
                    if (cSeatArray[rowValU, colValL] == 'L') if (bookedSeats[7] == 0) bookedSeats[7] = 2;
                }
            }

            int BookedSeats = 0;

            for (int x = 0; x < 8; x++)
            {
                if (bookedSeats[x] == 1) BookedSeats++;
            }

            if (cSeatArray[row, col] != '.')
            {
                if (BookedSeats == 0)
                {
                    return "#," + row + "," + col;
                }
                if (BookedSeats >= 5)
                {
                    return "L," + row + "," + col;
                }
            }
            return "";
        }

        private static string BookAndCheckSeatDay1(int row, int col)
        {
            int ctrFree = 0;
            int ctrBooked = 0;

            if (cSeatArray[row, col] != '.')
            {
                for (int i = -1; i < 2; ++i)
                {
                    for (int j = -1; j < 2; ++j)
                    {
                        if (!((i == 0) && (j == 0)))
                        {
                            if (cSeatArray[row, col] == 'L')
                            {
                                if (cSeatArray[row + i, col + j] == '#')
                                    ctrFree++;
                            }

                            if (cSeatArray[row, col] == '#')
                            {
                                if (cSeatArray[row + i, col + j] == '#')
                                    ctrBooked++;
                            }
                        }
                    }
                }
                if (ctrFree == 0 && ctrBooked < 4)
                {
                    return "#," + row + "," + col;
                }
                if (ctrBooked > 3)
                {
                    return "L," + row + "," + col;
                }
            }
            return "";
        }

        private static void SetSeatArray(List<string> clistStr)
        {
            cListRow = clistStr.Count() + 2;
            cListCol = clistStr[0].Length + 2;

            cSeatArray = new char[cListRow, cListCol];

            for (int i = 0; i < cListRow; ++i)
            {
                for (int j = 0; j < cListCol; ++j)
                {
                    cSeatArray[i, j] = '.';
                }
            }

            for (int i = 1; i < cListRow - 1; ++i)
            {
                for (int j = 1; j < cListCol - 1; ++j)
                {
                    char[] lineArray = clistStr[i - 1].ToCharArray();
                    cSeatArray[i, j] = lineArray[j - 1];
                }
            }
        }

        private static int CountBookedSeats()
        {
            int ctr = 0;
            for (int i = 1; i < cListRow - 1; ++i)
            {
                for (int j = 1; j < cListCol - 1; ++j)
                {
                    if (cSeatArray[i, j] == '#')
                        ctr++;
                }
            }
            return ctr;
        }

        private static void ShowSeats(string run = "")
        {
            Console.WriteLine("\nShowSeats\n");

            if (run != "") Console.WriteLine(run);
            for (int i = 1; i < cListRow - 1; ++i)
            {
                for (int j = 1; j < cListCol - 1; ++j)
                {
                    Console.Write(cSeatArray[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            CountBookedSeats();
        }
    }
}