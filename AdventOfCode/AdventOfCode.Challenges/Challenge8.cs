using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Challenges
{
    public class Tree
    {
        public int Row;
        public int Column;
        public int Height; 
     }
    public class Challenge8
    {
        public void Run(string path)
        {
            // we can use a graph, add all the trees as nodes, and if the adjacent nodes are smaller that means its visible
            // sort off, but since we dont have a graph class we are going to do a quick variation. 
            var trees = File.ReadAllLines(path); // we need the lines these are our rows. 
            List<Tree> calculationData = new List<Tree>();
            int currentRow = 1;
            foreach (var row in trees)
            {
                int column = 1;
                foreach (var tree in row)
                {
                    calculationData.Add(new Tree() { Height = Int32.Parse(tree.ToString()), Column = column, Row = currentRow });
                    column++;
                }
                currentRow++;
            }

            // good now we just select the ones that matter, ergo the ones that are not on the outside runs. 
            var outsideTrees = calculationData.Where(x => x.Row == 1 || x.Row == trees.Length || x.Column == 1 || x.Column == trees[0].Length).Distinct().ToList();
            var importantTrees = calculationData.Except(outsideTrees); // ooh i got to use except again. 
            
            List<Tree> visibleTrees = new List<Tree>();
            foreach (var importantTree in importantTrees)
            {
                // Now we just run code to get all trees to the left, all trees to the right, all trees to the top, and run a query 
                // YES WE COULD HAVE DONE A GRAPH BUT ITS 22:00 ON A WEDNESDAY LEAVE ME ALONE 
                var leftTrees = calculationData.Where(x => x.Column < importantTree.Column && x.Row == importantTree.Row).ToList();
                var rightTrees = calculationData.Where(x => x.Column > importantTree.Column && x.Row == importantTree.Row).ToList();
                var upTrees = calculationData.Where(x => x.Column == importantTree.Column && x.Row < importantTree.Row).ToList(); //haha uptrees
                var downTrees = calculationData.Where(x => x.Column == importantTree.Column && x.Row > importantTree.Row).ToList();

                if (leftTrees.All(x => x.Height < importantTree.Height)
                    || rightTrees.All(x => x.Height < importantTree.Height)
                    || upTrees.All(x => x.Height < importantTree.Height)
                    || downTrees.All(x => x.Height < importantTree.Height)) {
                    visibleTrees.Add(importantTree);
                }
            }

            Console.WriteLine($"Total visible trees on the outside is { outsideTrees.Count() } plus inside visible is " +
                $"{visibleTrees.Count() } so in total { outsideTrees.Count() + visibleTrees.Count() }");
        }
    }
}
