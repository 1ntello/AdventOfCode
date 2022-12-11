using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace AdventOfCode.Challenges
{
    public class Challenge3
    {
        public void Run(string path)
        {
            string[] rucksacks = File.ReadAllLines(path);
            int totalValue = 0;

            foreach (var rucksack in rucksacks)
            {
                int halfPoint = rucksack.Length / 2;
                var Department1 = rucksack.Substring(0, halfPoint).ToList();
                var Department2 = rucksack.Substring(halfPoint, halfPoint).ToList();
                totalValue += CalculateValueOfSharedItems(Department1, Department2);
            }

            Console.WriteLine($"The total value of the shared items between compartments is { totalValue }");
        }

        public void RunPart2(string path)
        {
            string[] rucksacks = File.ReadAllLines(path);
            int totalValue = 0;
            int sillyCounter = 1;

            for (var i = 0; i < rucksacks.Length; i++)
            {
                if (sillyCounter == 3)
                {
                    sillyCounter = 0;
                    totalValue += DealWithTripleGroup(rucksacks[i].ToList(), rucksacks[i - 1].ToList(), rucksacks[i - 2].ToList());
                }
                sillyCounter++;
            }

            Console.WriteLine($"The total value of the shared items between compartments is { totalValue }");
        }

        public int DealWithTripleGroup(List<char> rucksack1, List<char> rucksack2, List<char> rucksack3) 
        {
            // We can argue that if it is the ONLY thing in common between all 3, it is also in the first two. 
            var sharedItems = rucksack1.Intersect(rucksack2).ToList();

            // We just intersect that with the third and single out the badge 
            var comparedToRucksack3 = rucksack3.Intersect(sharedItems).ToList();
            var badge = comparedToRucksack3.Single();

            // And we just calculate the value 
            return (char.IsUpper(badge) ? (26 + (char.ToUpper(badge) - 64)) : (char.ToUpper(badge) - 64));
        }

        private int CalculateValueOfSharedItems(List<char> Department1, List<char> Department2)
        {
            var sharedItems = Department1.Intersect(Department2).ToList();
            int value = 0;
            sharedItems.ForEach(x => value += (char.IsUpper(x) ? (26 + (char.ToUpper(x) - 64)) : (char.ToUpper(x) - 64)));
            return value;
        }

    }
}
