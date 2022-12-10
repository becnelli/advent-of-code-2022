using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC.Lib.Solutions
{
    public class Day07 : ISolution
    {
        private static List<int> GetDirSizes(string filename)
        {
            List<int> inactiveDirs = new List<int>();
            List<int> activeDirs = new List<int>();

            foreach(string line in Helpers.ReadFile(filename))
            {
                string[] parts = line.Split(" ");

                switch(parts[0])
                {
                    case "$": //cmd (cd|ls)
                        switch(parts[1])
                        {
                            case "cd":
                                if(parts[2] == "..")
                                {
                                    inactiveDirs.Add(activeDirs.Last());
                                    activeDirs.RemoveAt(activeDirs.Count - 1);
                                }
                                else
                                {
                                    activeDirs.Add(0);
                                }
                                break;
                            case "ls":
                                // nop
                                break;
                        }
                        break;
                    case "dir": //dir
                        // nop
                        break;
                    default: // file
                        int size = Int32.Parse(parts[0]);
                        activeDirs = activeDirs.Select(i => i + size).ToList();
                        break;
                }
            }

            inactiveDirs.AddRange(activeDirs);

            return inactiveDirs;
        }

        public object Part1(string filename)
        {
            List<int> sizes = GetDirSizes(filename);
            
            if(sizes.Count == 0)
            {
                return "IDK";
            }

            return sizes.Where(x => x < 100000).Sum();
        }

        public object Part2(string filename)
        {
            List<int> sizes = GetDirSizes(filename);

            if(sizes.Count == 0)
            {
                return "IDK";
            }

            sizes.Sort();

            int availableFreeSpace = 70000000 - sizes[sizes.Count -1];
            for (int i = 0; i < sizes.Count; i++)
            {
                if(availableFreeSpace + sizes[i] >= 30000000)
                    return sizes[i];
            }

            return "IDK";
        }
    }
}
