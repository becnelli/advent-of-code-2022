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
        private static MyDir ParseDirs(string filename)
        {
            Regex cmd = new Regex("\\$ (cd|ls) ?(.*)?");
            Regex lsFile = new Regex("^(\\d+) (.*)$");
            Regex lsDir = new Regex("^dir (.*)$");

            MyDir currentDir = new MyDir("/");
            MyDir? rootDir = null;

            List<String> lines = Helpers.ReadFile(filename).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                if (!cmd.IsMatch(line))
                {
                    Console.WriteLine("IDK: " + line);
                    break;
                }

                Match m = cmd.Match(line);

                List<Group> vals = m.Groups.Values.ToList();


                switch (vals[1].Value.ToLower())
                {
                    case "cd":
                        switch (vals[2].Value)
                        {
                            case "/":
                                if (rootDir == null)
                                {
                                    rootDir = new MyDir("/");
                                }
                                currentDir = rootDir;
                                break;
                            case "..":
                                currentDir = currentDir.Parent;
                                break;
                            default:
                                if (currentDir.ChildDirs.Any(x => x.Name == vals[2].Value))
                                {
                                    currentDir = currentDir.ChildDirs.First(x => x.Name == vals[2].Value);
                                }
                                else
                                {
                                    MyDir myDir = new MyDir(vals[2].Value, currentDir);
                                    currentDir.ChildDirs.Add(myDir);
                                    currentDir = myDir;
                                }
                                break;
                        }
                        break;
                    case "ls":
                        while (i + 1 < lines.Count && !cmd.IsMatch(lines[i + 1]))
                        {
                            i++;
                            line = lines[i];

                            if (lsDir.IsMatch(line))
                            {
                                string dirName = lsDir.Match(line).Groups.Values.ToList()[1].Value;

                                if (currentDir.ChildDirs.Any(x => x.Name == dirName))
                                {
                                    currentDir = currentDir.ChildDirs.First(x => x.Name == dirName);
                                }
                                else
                                {
                                    MyDir myDir = new MyDir(dirName, currentDir);
                                    currentDir.ChildDirs.Add(myDir);
                                }
                            }
                            else if (lsFile.IsMatch(line))
                            {
                                string fileName = lsFile.Match(line).Groups.Values.ToList()[2].Value;
                                int fileSize = Int32.Parse(lsFile.Match(line).Groups.Values.ToList()[1].Value);

                                MyFile file = new MyFile(fileName, fileSize);

                                currentDir.ChildFiles.Add(file);
                            }
                            else
                            {
                                Console.WriteLine("IDK2: " + line);
                            }
                        }
                        break;
                }

            }
            return rootDir;

        }

        public object Part1(string filename)
        {
            MyDir rootDir = ParseDirs(filename);
            
            return GetSumLessThan(rootDir, 100000);
        }

        public object Part2(string filename)
        {
            MyDir rootDir = ParseDirs(filename);

            IEnumerable<MyDir> allDirs = GetAllDirs(rootDir);

            IEnumerable<MyDir> sorted = allDirs.Where(x => x.GetSizeRecursive() < 30000000).OrderBy(x => x.GetSizeRecursive());

            int curr = 70000000 - rootDir.GetSizeRecursive();

            foreach (MyDir d in sorted)
            {
                if(d.GetSizeRecursive() + curr  >= 30000000)
                {
                    return d.GetSizeRecursive();
                }
            }

            return "IDK"; ;
        }

        public static int GetSumLessThan(MyDir d, int size)
        {
            int total = 0;
            int r = d.GetSizeRecursive();

            if (r < size)
            {
                total += r;
            }

            foreach (MyDir c in d.ChildDirs)
            {
                total += GetSumLessThan(c, size);
            }
            return total;
        }

        public static IEnumerable<MyDir> GetAllDirs(MyDir d)
        {
            List<MyDir> temp = d.ChildDirs.ToList();

            d.ChildDirs.ForEach(x => temp.AddRange(GetAllDirs(x)));

            return temp;
        }
    }



    public class MyDir
    {
        public string Name { get; set; }    

        public MyDir? Parent { get; set; }

        public List<MyDir> ChildDirs { get; set; }

        public List<MyFile> ChildFiles { get; set; }

        public MyDir(String name)
        {
            Name = name;
            Parent = null;
            ChildDirs = new List<MyDir>();
            ChildFiles = new List<MyFile>();
        }

        public MyDir(String name, MyDir parent)
        {
            Name = name;
            Parent = parent;
            ChildDirs = new List<MyDir>();
            ChildFiles = new List<MyFile>();
        }

        public int GetSizeRecursive()
        {
            return ChildFiles.Sum(x => x.Size) + ChildDirs.Sum(x => x.GetSizeRecursive());
        }
    }

    public class MyFile
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public MyFile(String name, int size)
        {
            Name = name;
            Size = size;
        }

    }
}
