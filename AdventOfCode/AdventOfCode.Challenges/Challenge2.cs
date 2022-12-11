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

    public enum Outcome 
    {
        Loss,
        Win,
        Draw
    }

    public class Challenge2
    {
        public void Run(string path)
        {
            //moet van mike
            string[] strategies = File.ReadAllLines(path);
            Console.WriteLine($"Total strategies { strategies.Length }");
            int totalPoints = 0;
            foreach (var s in strategies)
            {
                string[] moves = s.Split(" ");

                var playerShape = GetBestOption(StringToOptions(moves[0]), StringToOutcome(moves[1]));
                totalPoints += CalculatePoints(playerShape, StringToOptions(moves[0]));
            }
            Console.WriteLine($"Amount of points: { totalPoints }");
        }
        private Options StringToOptions(string s) {
            if (s == "A") return Options.Rock;
            else if (s == "B") return Options.Paper;
            else if (s == "C" ) return Options.Scissors;
            else throw new Exception("No valid option stop cheating nerd");
        }
        private Outcome StringToOutcome(string s)
        {
            if (s == "X") return Outcome.Loss;
            else if (s == "Y") return Outcome.Draw;
            else if (s == "Z") return Outcome.Win;
            else throw new Exception("No valid option stop cheating nerd");
        }


        private int CalculatePoints(Options option1, Options option2)
        {
            // We only need to know when the player wins 
            if ((option1 == Options.Paper && option2 == Options.Rock) || (option1 == Options.Rock && option2 == Options.Scissors) || (option1 == Options.Scissors && option2 == Options.Paper))
                return 6 + (int)option1;
            if (option1 == option2)
                return 3 + (int)option1;
            else
                return (int)option1;
        }

        private Options GetBestOption(Options option1, Outcome outcome) 
        {
            if (outcome == Outcome.Draw) return option1;

            else if (outcome == Outcome.Win && option1 == Options.Rock) return Options.Paper;
            else if (outcome == Outcome.Win && option1 == Options.Paper) return Options.Scissors;
            else if (outcome == Outcome.Win && option1 == Options.Scissors) return Options.Rock;

            else if (outcome == Outcome.Loss && option1 == Options.Rock) return Options.Scissors;
            else if (outcome == Outcome.Loss && option1 == Options.Scissors) return Options.Paper;
            else if (outcome == Outcome.Loss && option1 == Options.Paper) return Options.Rock;

            throw new Exception("Invalid setup");
        }
    }
}
