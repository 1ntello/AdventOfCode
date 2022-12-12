using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode.Challenges
{
    public class Challenge5
    {
        Dictionary<int, Stack<char>> Stacks;
        int splitInt;
        public void Run(string path)
        {
            var text = File.ReadAllLines(path);
            Stacks = ParseTheSetup(text);
            HandleInstructions(text);

            foreach (var s in Stacks)
            {
                Console.WriteLine($" Printing stack number { s.Key }");
                foreach (var v in s.Value)
                {
                    if (!Char.IsLetter(v))
                        continue;
                    else;
                        Console.WriteLine($"[{v}]");
                }
            }
        }

        private void HandleInstructions(string[] text)
        {
            for (int i = splitInt + 1; i < text.Length; i++)
            {
                var instructions = text[i].Split(" ");

                // we move instructions[1] from instructions[3] to instructions[5]

                int amountToMove = Int32.Parse(instructions[1]);
                int origStack = Int32.Parse(instructions[3]);
                int destStack = Int32.Parse(instructions[5]);

                for (int j = 0; j < amountToMove; j++)
                {
                    var valueToBeMoved = Stacks.Where(x => x.Key == origStack).Single().Value.Pop();
                    
                    Stacks.Where(x => x.Key == destStack).Single().Value.Push(valueToBeMoved);
                }

            }
        }

        // This will fill the stacks we see at top. Its a list of stacks, we have to make it dynamic. 
        private Dictionary<int, Stack<char>> ParseTheSetup(string[] unParsed)
        {
            Dictionary<int, Stack<char>> returnStacks = new Dictionary<int, Stack<char>>();
            List<string> unParsedList = unParsed.ToList();
            int beforeIndex = unParsedList.IndexOf(unParsedList.First(x => x.StartsWith("move"))) - 1;
            splitInt = beforeIndex; 

            for (var i = beforeIndex; i >= 0; i--)
            {
                if (unParsedList[i] == string.Empty)
                    continue;

                if (unParsedList[i].StartsWith(" 1"))
                {
                    var amountOfStacks = unParsedList[i].Where(x => !char.IsWhiteSpace(x)).Select(y => Int32.Parse(y.ToString())).Max();
                    for (int z = 1; z <= amountOfStacks; z++)
                        returnStacks.Add(z, new Stack<char>());
                }

                else 
                {
                    string[] line = unParsedList[i].Split(" ");
                    int amountOfWhitespaces = 0;
                    int stacksToAdd = 0;

                    for (int x = 0; x < line.Length; x++)
                    {
                        string cValue = line[x];

                        // We very cheekily decide how many stacks to add, 4 whitespaces means an empty space, else its +1
                        if (cValue == string.Empty)
                        {
                            amountOfWhitespaces++;

                            if (amountOfWhitespaces == 4)
                            {
                                amountOfWhitespaces = 0;
                                stacksToAdd++;
                            }
                            continue;
                        }
                        else
                        {
                            stacksToAdd++;
                        }

                        cValue = cValue.Replace("[", "");
                        cValue = cValue.Replace("]", "");

                        var charToPlace = Char.Parse(cValue);
                        returnStacks.Where(s => s.Key == (stacksToAdd)).Single().Value.Push(charToPlace);
                    }
                }
            }
            return returnStacks;
        }
    }
}
