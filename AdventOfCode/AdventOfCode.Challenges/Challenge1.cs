using System;
using System.Collections.Generic;
using System.Linq;
using System.IO; 


namespace AdventOfCode.Challenges
{
    public class Challenge1
    {
       
        public Challenge1()
        {

        }

        public void Run()
        {
            string[] calories = File.ReadAllLines("Inputs/challenge1.txt");
            Dictionary<int, int> ElvesAndCalories = new Dictionary<int, int>();

            int elf = 1;
            int current = 0;

            foreach (var c in calories)
            {
                if (c != String.Empty)
                    current += Int32.Parse(c);
                else
                {
                    ElvesAndCalories.Add(elf, current);
                    elf++;
                    current = 0; 
                }
            }

            var fatso = ElvesAndCalories.OrderBy(x => x.Value).Last();
            Console.WriteLine($"Elf carrying the most calories is elf number { fatso.Key } carrying { fatso.Value }");

        }


    }
}
