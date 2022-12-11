using System;
using System.Collections.Generic;
using System.Linq;
using System.IO; 


namespace AdventOfCode.Challenges
{
    public class Challenge1
    {
      
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

            var hardestWorkingElf = ElvesAndCalories.OrderBy(x => x.Value).Last();
            Console.WriteLine($"Elf carrying the most calories is elf number { hardestWorkingElf.Key } carrying { hardestWorkingElf.Value }");

            // Calculate part 2, we can order by descending (to make it easier) then select the top3 with a take
            var hardestWorkingElves = ElvesAndCalories.OrderByDescending(x => x.Value).Take(3);
            // Then we just calculate the total 
            var amountOfCalories = hardestWorkingElves.Sum(x => x.Value);
            Console.WriteLine($"The three hardest working elves are carrying { amountOfCalories } calories");
        }

        // Don't use this its mean
        public void RunDifferent(string path)
        {
            string[] calories = File.ReadAllLines(path);
            int[] result = new int[calories.Length];
            int current, counter;
            current = 0;  counter = 1;

            foreach (var c in calories)
                if (c != string.Empty)
                    current += Int32.Parse(c);
                else
                {
                    result[counter - 1] = current;
                    current = 0;
                    counter++;
                }

            Console.WriteLine($"Elf carrying the most calories is elf number { Array.IndexOf(result, result.Max(), 0, result.Length) + 1 } carrying { result.Max() }");
        }

       

    }
}
