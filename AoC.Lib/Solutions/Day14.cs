using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Numerics;

namespace AoC.Lib.Solutions
{
    public class Day14 : ISolution
    {        
        public object Part1(string filename)
        {  
            List<RockPath> paths = ReadRockPaths(filename);

            Grid grid = new Grid(paths);
            grid.SetSand(500, 0);

            grid.Print();

            bool success = false;
            int drops=0;

            do {
                try
                {
                    success = grid.DropSand();
                    drops++;
                }
                catch (IndexOutOfRangeException)
                {
                    success = false;
                }
            } while(success);

            grid.Print();
            return drops;
        }

        public object Part2(string filename)
        {
            List<RockPath> paths = ReadRockPaths(filename);

            if(!paths.Any())
                return "EMPTY";

            Grid grid = new Grid(paths, true);
            grid.SetSand(500, 0);

            grid.Print();

            bool success = false;
            int drops=0;

            do {
                try
                {
                    success = grid.DropSand();
                    drops++;
                }
                catch (IndexOutOfRangeException)
                {
                    success = false;
                }
            } while(success);

            grid.Print();
            return drops;
        }

        private List<RockPath> ReadRockPaths(string filename)
        {
            List<RockPath> paths = new List<RockPath>();

            foreach(string line in Helpers.ReadFile(filename))
            {
                string[] parts = line.Split(" -> ");

                int pastX = Int32.Parse(parts[0].Split(",")[0]);
                int pastY = Int32.Parse(parts[0].Split(",")[1]); 

                for(int i = 1; i < parts.Length; i++)
                {
                    int currX = Int32.Parse(parts[i].Split(",")[0]);
                    int currY = Int32.Parse(parts[i].Split(",")[1]); 

                    if(currX == pastX)
                    {
                        paths.Add(new VerticalRockPath(currX, pastY, currY));
                    }
                    else                    
                    {
                        paths.Add(new HorizontalRockPath(currY, pastX, currX));
                    }

                    pastX = currX;
                    pastY = currY;
                }
            }

            return paths;
        }

        public class Grid
        {
            private List<List<string>> grid = new List<List<string>>();
            private List<List<string>> leftGrid = new List<List<string>>();
            private List<List<string>> rightGrid = new List<List<string>>();
            private Tuple<int, int> sand = Tuple.Create(-1, -1);
            private bool infiniteBase = false;

            public int xDiff { get; private set; }
            public int xWidth { get; private set; }
            

            public Grid(IEnumerable<RockPath> coordinateLines, bool infiniteBase = false)
            {
                this.infiniteBase = infiniteBase;

                int minX = coordinateLines.Select(x => x is HorizontalRockPath ? ((HorizontalRockPath)x).MinX : ((VerticalRockPath)x).X).Min();
                int maxX = coordinateLines.Select(x => x is HorizontalRockPath ? ((HorizontalRockPath)x).MaxX : ((VerticalRockPath)x).X).Max();

                int height = coordinateLines.Select(y => y is HorizontalRockPath ? ((HorizontalRockPath)y).Y : ((VerticalRockPath)y).MaxY).Max() +1;
                xDiff = minX;
                xWidth = maxX - minX + 1;

                for(int i = 0; i < height; i++)                
                {
                    grid.Add(new List<string>(xWidth));
                    leftGrid.Add(new List<string>());
                    
                    for(int j = 0; j < xWidth; j++)
                        grid[grid.Count - 1].Add(".");
                }

                if(infiniteBase)
                {
                    grid.Add(new List<string>(xWidth));
                    for(int j = 0; j < xWidth; j++)
                        grid[grid.Count - 1].Add(".");

                    grid.Add(new List<string>(xWidth));
                    for(int j = 0; j < xWidth; j++)
                        grid[grid.Count - 1].Add("#");
                        
                    leftGrid.Add(new List<string>());                    
                    leftGrid.Add(new List<string>());
                    
                    height+=2;
                }   

                foreach(RockPath path in coordinateLines)
                {
                    if(path is HorizontalRockPath)
                    {
                        HorizontalRockPath h = (HorizontalRockPath)path;
                        for(int i = h.MinX; i <= h.MaxX; i++)
                        {
                            grid[h.Y][ConvertX(i)] = "#";
                        }
                    }
                    else if(path is VerticalRockPath)
                    {
                        VerticalRockPath h = (VerticalRockPath)path;
                        for(int i = h.MinY; i <= h.MaxY; i++)
                        {
                            grid[i][ConvertX(h.X)] = "#";
                        }
                    }
                }             
            }

            private int ConvertY(int y)
            {
                return y;
            }

            private int ConvertX(int x)
            {
                return x - xDiff;
            }

            public void SetSand(int x, int y)
            {
                sand = Tuple.Create(ConvertX(x), y);
                grid[y][ConvertX(x)] = "+";
            }

            public void Print()
            {
                Console.WriteLine("My Grid:");
                
                for(int i = 0; i < leftGrid.Count; i++)
                {
                    Console.Write(i + ":\t" + string.Join("", leftGrid[i].ToArray().Reverse()));
                    Console.WriteLine(string.Join("", grid[i].ToArray()));
                }

                Console.WriteLine("--------------");
            }
            
            public bool DropSand()
            {
                return DropSand(sand.Item1, sand.Item2);
            }

            public bool DropSand(int x, int y)
            {
                if(CanGoDown(x, y))
                {
                    return DropSand(x, y+1);
                }
                else if(CanGoDownLeft(x, y))
                {
                    return DropSand(x-1, y+1);
                }
                else if(CanGoDownRight(x, y))
                {
                    return DropSand(x+1, y+1);
                }
                /*else if (infiniteBase && IsLeftEdge(x) && CanGoLeft(x, y))
                {
                    return DropSand(x-1, y);
                }
                else if (infiniteBase && IsRightEdge(x) && CanGoRight(x, y))
                {
                    return DropSand(x+1, y);
                }*/
                else 
                {
                    if(x >= 0)
                    {
                        bool abandonShip = grid[y][x] == "+";

                        grid[y][x] = "o";
                        //Print();
                        
                        if(abandonShip)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        leftGrid[y][Math.Abs(x + 1)] = "o";
                        //Print();
                        return true;
                    }
                }

                return false;
            }

            private bool CanGoDown(int x, int y)
            {
                if(y+1 >= grid.Count)
                {
                    return false;
                }
                else if(x < 0 && infiniteBase)
                {
                    return leftGrid[y+1][Math.Abs(x+1)] == ".";
                }
                
                return grid[y+1][x] == ".";
            }

            private bool CanGoDownLeft(int x, int y)
            {
                if(y+1 >= grid.Count || (x == 0 && !infiniteBase)) // at bottom == no go
                {
                    return false;
                }
                else if(x-1 < 0 && infiniteBase)
                {
                    if(Math.Abs(x-1) > leftGrid[y].Count)
                    {
                        AddColumn(leftGrid);
                    }
                    
                    return leftGrid[y+1][Math.Abs(x)] == ".";
                }
                
                return grid[y+1][x-1] == ".";
            }

            private bool CanGoLeft(int x, int y)
            {
                if(y >= grid.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                else if(x-1 < 0)
                {
                    if(!infiniteBase)
                        throw new IndexOutOfRangeException();
                    else
                    {
                        if(Math.Abs(x-1) > leftGrid[y].Count)
                        {
                            AddColumn(leftGrid);
                        }
                        
                        return leftGrid[y][Math.Abs(x)] == ".";
                    }
                }
                
                return grid[y][x] == ".";
            }

            private bool CanGoDownRight(int x, int y)
            {
                if (y+1 >= grid.Count || (!infiniteBase && x+1 >= grid[0].Count))
                {
                    return false;
                }
                else if(x < -1)
                {
                    return leftGrid[y+1][Math.Abs(x + 1)] == ".";
                }
                else if (infiniteBase && x+1 >= grid[0].Count)
                {
                    if(Math.Abs(x) < grid[y].Count)
                    {
                        AddColumn(grid);
                    }
                }
                
                return grid[y+1][x+1] == ".";
            }

            private bool CanGoRight(int x, int y)
            {
                if(y >= grid.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                else if(x+1 >= grid[0].Count)
                {
                    if(!infiniteBase)
                        throw new IndexOutOfRangeException();
                    else
                    {
                        if(Math.Abs(x) < leftGrid[y].Count)
                        {
                            AddColumn(grid);
                        }
                    }
                }
                
                return grid[y][x+1] == ".";
            }

            private void AddColumn(List<List<string>> grid)
            {
                foreach(List<string> row in grid)
                {
                    row.Add(".");
                }

                grid[grid.Count - 1][grid[0].Count -1] = "#";
            }
        }

        public abstract class RockPath
        {

        }

        public class VerticalRockPath : RockPath
        {
            
            public int X { get; set; }
            public int MinY { get; set; }
            public int MaxY { get; set; }

            public VerticalRockPath(int x, int y1, int y2)
            {
                X = x;
                MinY = y1 < y2 ? y1 : y2;
                MaxY = y1 > y2 ? y1 : y2;
            }
        }

        public class HorizontalRockPath : RockPath
        {
            public int Y { get; set; }
            public int MinX { get; set; }
            public int MaxX { get; set; }

            public HorizontalRockPath(int y, int x1, int x2)
            { 
                Y = y;
                MinX = x1 < x2 ? x1 : x2;
                MaxX = x1 > x2 ? x1 : x2;               
            }
        }
    }
}