using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2020
{
    [ProblemName("Day 21: Allergen Assessment")]
    class Day21 : BaseLine, Solution
    {
        public object PartOne(string input) => Day1(input).First();
        public object PartTwo(string input) => Day1(input, true).First();

        private IEnumerable<string> Day1(string inData, bool part2 = false)
        {
            //Console.WriteLine("\n\nDay 21: Allergen Assessment\n");

            List<string> inputList = inData.Split("\n").ToList();
            List<food> ingredientsList = new List<food>();
            List<string> allergensList = new List<string>();
            List<string> allIngedientsList = new List<string>();
            List<string> allergensMapList;

            //Load calsses
            foreach (string ingStr in inputList)
            {
                food tmpIng = new food(ingStr);
                ingredientsList.Add(tmpIng);
                foreach (string ing in tmpIng.ingredients)
                    allIngedientsList.Add(ing);
            }

            //create list of all ingerdients
            foreach (food ingCls in ingredientsList)
            {
                foreach (string tmpStr in ingCls.allergens)
                {
                    if (!allergensList.Contains(tmpStr))
                    {
                        allergensList.Add(tmpStr);
                    }
                }
            }

            //Do checks
            allergensMapList = new List<string>();
            foreach (string tmp in allergensList) allergensMapList.Add("");

            for (int w = 0; w < 20; w++) //Guessed loop..
            {
                for (int i = 0; i < allergensList.Count; i++)
                {
                    List<string> lastIngredients = new List<string>(0);
                    List<string> foundIngredients = new List<string>(0);

                    foreach (food ingCls in ingredientsList)
                    {
                        if (ingCls.allergens.Contains(allergensList[i]))
                        {
                            if (foundIngredients.Count == 0)
                            {
                                foundIngredients = ingCls.ingredients.ToList();

                                if (ingredientsList.Where(x => x.allergens.Contains(allergensList[i])).Count() == 1)
                                {
                                    List<string> itemsFound = new List<string>();
                                    //Loop for each set of ingredients of the list (for test)
                                    foreach (string fndIngredients in foundIngredients)
                                    {
                                        foreach (string searchIngredients in ingCls.ingredients)
                                        {
                                            if (fndIngredients == searchIngredients && !allergensMapList.Contains(searchIngredients))
                                                itemsFound.Add(searchIngredients);
                                        }
                                    }
                                    foundIngredients = itemsFound;
                                }
                            }
                            else
                            {
                                List<string> itemsFound = new List<string>();
                                foreach (string fndIngredients in foundIngredients)
                                {
                                    foreach (string searchIngredients in ingCls.ingredients)
                                    {
                                        if (fndIngredients == searchIngredients && !allergensMapList.Contains(searchIngredients))
                                            itemsFound.Add(searchIngredients);
                                    }
                                }
                                foundIngredients = itemsFound;
                            }
                        }
                    }
                    if (foundIngredients.Count == 1)
                    {
                        allergensMapList[i] = foundIngredients[0];
                    }
                }
            }

            //Part 2
            foreach (string all in allergensMapList)
                allIngedientsList.RemoveAll(str => str == all);

            //string a = "";
            List<string> tmpListForSort = new List<string>();
            for (int f = 0; f < allergensList.Count; f++)
            {
                tmpListForSort.Add(allergensList[f] + " " + allergensMapList[f]);
            }

            tmpListForSort.Sort();

            string tmpStringForSort = "";
            foreach (string s in tmpListForSort)
            {
                tmpStringForSort += s.Split(' ')[1] + ",";
            }

            //Console.WriteLine("Day 21/1: How many times do any of the safe ingredients appear? " + allIngedientsList.Count);
            //Console.WriteLine("Day 21/2: Dangerous ingredient list? " + tmpStringForSort.Substring(0, tmpStringForSort.Length - 1));
            yield return !part2 ? allIngedientsList.Count.ToString() : tmpStringForSort.Substring(0, tmpStringForSort.Length - 1);

        }
    }

    class food
    {
        public string[] ingredients;
        public string[] allergens;
        public food(string input)
        {
            input = input.Replace(" (", "(");
            string[] tmp = input.Split('(');
            ingredients = tmp[0].Split(' ');
            allergens = tmp[1].Replace("contains ", "").Replace(" ", "").Split(')')[0].Split(',');
        }
    }

}
