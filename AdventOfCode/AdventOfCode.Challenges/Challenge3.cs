﻿using System;
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

        private int CalculateValueOfSharedItems(List<char> Department1, List<char> Department2)
        {
            var sharedItems = Department1.Intersect(Department2).ToList();
            int value = 0;
            sharedItems.ForEach(x => value += (char.IsUpper(x) ? (26 + (char.ToUpper(x) - 64)) : (char.ToUpper(x) - 64)));
            return value;
        }

    }
}
