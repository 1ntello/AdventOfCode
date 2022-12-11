using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode.Challenges
{
    /*
        [D]                     - 
        [N] [C]    
        [Z] [M] [P]         - its a stack so we just build up. First goes in the stack of the same line . 
         1   2   3          - then we do a max here then we know our stacks

        move 1 from 2 to 1  - find this line, go two back. 
        move 3 from 1 to 3
        move 2 from 2 to 1
        move 1 from 1 to 2 */
  
    class Challenge5
    {
        QuickStackImplementation<char>[] stacks;
        public void Run(string path)
        {
            var text = File.ReadAllLines(path); 
            // write a parser to parse the first set
            

            // Then we just take the instructions, take the numbers, ansd then use pop and push if we go. 

        }

        // This will fill the stacks we see at top. Its a list of stacks, we have to make it dynamic. 
        private QuickStackImplementation<char>[] ParseTheInstructions(string[] unParsed)
        {
            // allright we start by making it a list because that opens up more of the .net library for me to use
            List<string> unParsedList = new List<string>();


            return null;
        };
    }

    class QuickStackImplementation<T>
    {
        T[] data;
        int size;

        public QuickStackImplementation()
        {
            data = new T[24]; //alphabet
        }

        public T Pop()
        {
            return data[size];
            size--;
        }

        public void Push(T item)
        {
            data[size + 1] = item;
            size++;
        }

    }
}
