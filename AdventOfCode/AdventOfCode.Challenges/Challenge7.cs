using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode.Challenges
{
    public class Challenge7
    {
        // We thought to easy and forgot nesting sad.  lets do structs!
        Dictionary<AdventObject, int> _masterData = new Dictionary<AdventObject, int>();
        private class AdventObject
        {
            public string Name;
            public AdventObject ParentDirectory;
            public int Size;
            public bool IsDir; 

            public void UpdateParentDir(AdventObject s)
            {
                ParentDirectory = s;
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false; 

                var castObj = (AdventObject)obj;
                bool parentDirMatches = false;
                if (castObj.ParentDirectory == null && ParentDirectory == null)
                    parentDirMatches = true;
                else if (castObj.ParentDirectory == null && ParentDirectory != null)
                    parentDirMatches = false;
                else if (castObj.ParentDirectory.Equals(ParentDirectory))
                    parentDirMatches = true;


                return castObj.Name == Name && parentDirMatches && castObj.Size == Size && castObj.IsDir == IsDir;
            }
        }

        public void Run(string path)
        {
            // read all lines again since order matters
            var fileTree = File.ReadAllLines(path);

            // vars we need for the routine
            List<AdventObject> listOfRelevantFiles = new List<AdventObject>();
          
            AdventObject dirAbove = new AdventObject() { Name = "" };
            AdventObject currentDir = new AdventObject() { Name = "/" };
            listOfRelevantFiles.Add(currentDir);
            int currentSize;
            // ignore the first two rules its  cd and ls
            for (int i = 1; i < fileTree.Length; i++)
            {
                // existing folder just add to list. 
                if (fileTree[i].StartsWith("dir"))
                {
                    string dirname = fileTree[i].Split(" ")[1];
                    // forgot that we can enter the same folder twice, so we only add if non existing 
                    if (!listOfRelevantFiles.Where(t => t.Name == dirname && t.ParentDirectory == currentDir).Any())
                        listOfRelevantFiles.Add(new AdventObject() { Name = dirname, IsDir = true, ParentDirectory = currentDir});
                }
                else if (fileTree[i].StartsWith("$ cd"))
                {

                    //if filetree[i] contains .. we move in the parent folder of the current folder 
                    if (fileTree[i].Contains(".."))
                    {
                        //Now we just need to find which one it is that we need to have. 
                        currentDir = dirAbove;
                        dirAbove = dirAbove.ParentDirectory;

                    }
                    else
                    {
                        var intoFolder = listOfRelevantFiles.Single(x => x.Name == fileTree[i].Split(" cd ")[1] && x.ParentDirectory == currentDir);
                        if (listOfRelevantFiles.Any(x => x == intoFolder))
                        {
                            //if it already exists in this configuration , e-e we have been in it again,. 
                            if (!listOfRelevantFiles.Any(x => x == intoFolder && x.ParentDirectory == currentDir && x.IsDir))
                                listOfRelevantFiles.Single(x => x == intoFolder && x.ParentDirectory == currentDir && x.IsDir).UpdateParentDir(currentDir);

                            dirAbove = currentDir;
                            currentDir = intoFolder;
                        }
                    }
                }
                if (Char.IsDigit(fileTree[i].First()) && !fileTree[i].Contains("$"))
                {
                    // its a file from a dir of current name
                    var size = fileTree[i].Split(" ")[0];
                    var file = fileTree[i].Split(" ")[1];
                    listOfRelevantFiles.Add(new AdventObject() { Name = file, ParentDirectory = currentDir, Size = Int32.Parse(size) });
                }
            }

            var directories = listOfRelevantFiles.Where(x => x.IsDir);
            var startingDir = listOfRelevantFiles[0];
            FillDictionary(startingDir, listOfRelevantFiles);

            //Now the actual challenge 
            _masterData.Where(x => x.Value < 100000).ToList().ForEach(x => Console.WriteLine($"We can delete directory {x.Key} with size {x.Value}"));
            var totalSum = _masterData.Where(x => x.Value <= 100000).ToList().Sum(x => x.Value);
            Console.WriteLine($"Total sum is {totalSum}");


            //PART 2
            const int totalSize = 70000000;
            const int requiredSize = 30000000;

            int usedSize = _masterData.Where(x => x.Key.Name == "/").Single().Value;
            int missingSize = requiredSize - (totalSize - usedSize);

            var possibleDirs = _masterData.Where(x => x.Value >= missingSize).ToList();
            var smallest = possibleDirs.OrderByDescending(x => x.Value).Last();
            Console.WriteLine($"Smallest dir available = {smallest.Value} and dir is {smallest.Key.Name}");

        }

        private int FillDictionary(AdventObject dir, List<AdventObject> all)
        {
            bool dirStillHasSubdirectories = all.Where(x => x.ParentDirectory != null && x.ParentDirectory.Equals(dir)).Any();
            List<AdventObject> subDirs = all.Where(x => x.ParentDirectory != null && x.ParentDirectory.Equals(dir) && x.IsDir == true).ToList();
            int size = all.Where(x => x.ParentDirectory != null && x.ParentDirectory.Equals(dir) && x.IsDir == false).Sum(x => x.Size);

            if (dirStillHasSubdirectories)
            {
                foreach (var sdir in subDirs)
                {
                    size += FillDictionary(sdir, all);
                }
            }
            else
            {
                size = 0;
            }
            _masterData.Add(dir, size);
            return size; 
        }
    }
}
