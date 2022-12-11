using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq; 

namespace AdventOfCode.Challenges
{
    public class Challenge4
    {
        public void Run(string path)
        {
            Dictionary<int, int> Pairs = new Dictionary<int, int>();
            string text = File.ReadAllText(path);
            var p = text.Split("\r\n");
            int counter = 0;
            foreach (var sp in p)
            {
                if (sp == String.Empty)
                    continue; 

                var elves = sp.Split(",").ToList();

                int firstElfMinimal = Int32.Parse(elves[0].Split("-")[0]);
                int firstElfMaximal = Int32.Parse(elves[0].Split("-")[1]);
                int lastElfMinimal = Int32.Parse(elves[1].Split("-")[0]);
                int lastElfMaximal = Int32.Parse(elves[1].Split("-")[1]);

                if ((firstElfMaximal <= lastElfMaximal && firstElfMinimal >= lastElfMinimal) 
                    || lastElfMaximal <= firstElfMaximal && lastElfMinimal >= firstElfMinimal)
                    counter++;

            }

         
            Console.WriteLine($"Amount of pairs { counter } ");
        }
    }
}
