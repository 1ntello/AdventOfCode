using System;
using System.Collections.Generic;
using System.Linq;
using System.IO; 

namespace AdventOfCode.Challenges
{
    public class Challenge6
    {
        public void Run(string path)
        {
            // we are just going to cheat this one in. In this case it is one string so we use read all text
            var text = File.ReadAllText(path);
            // Why don't we use an array? because we are going to use the linq distinct ...
            Queue<char> last4values = new Queue<char>();
            int counter = 0;
            foreach(char c in text)
            {
                counter++;
                last4values.Enqueue(c);
                //This will go off once we start counting
                if (last4values.Count == 4)
                {
                    if (last4values.Distinct().Count() == 4)
                        Console.WriteLine($"4 different characters found, so it started at  { counter  }");
                    else
                    {
                        // we dequeue the first 
                        last4values.Dequeue();
                    }
                }
            }
        }
    }
}
