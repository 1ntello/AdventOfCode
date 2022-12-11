using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Challenges
{
    public enum Options
    {
        Rock = 1, 
        Paper = 2,
        Scissors = 3
    }

    public class Challenge2
    {
        public void Run(string path)
        {
            //moet van mike
            string[] strategies = File.ReadAllLines(path);
            int totalPoints = 0;
            foreach (var s in strategies)
            {
                string[] moves = s.Split(" ");
                totalPoints += CalculatePoints(StringToOptions(moves[0]), StringToOptions(moves[1]));
            }
            Console.WriteLine($"Amount of points: { totalPoints }");
        }

        private Options StringToOptions(string s) {
            if (s == "A" || s == "X") return Options.Rock;
            else if (s == "B" || s == "Y") return Options.Paper;
            else if (s == "C" || s == "Z") return Options.Scissors;
            else throw new Exception("No valid option stop cheating nerd");
        }
        private int CalculatePoints(Options option1, Options option2)
        {
            // We only need to know when the player wins 
            if ((option1 == Options.Paper && option2 == Options.Rock) || (option1 == Options.Rock && option2 == Options.Scissors) || (option1 == Options.Scissors && option2 == Options.Paper))
                return 6 + (int)option1;
            if (option1 == option2) //Draw
                return 3 + (int)option1;
            else // Only other result is a loss
                return (int)option1;
        }
    }
}
