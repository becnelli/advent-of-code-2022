using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Numerics;

namespace AoC.Lib.Solutions
{
    public class Day12 : ISolution
    {
        public Tuple<Tuple<int, int>, List<List<char>>> ParseStartingData(string filename)
        {
            List<List<char>> map = Helpers.ReadFileAs2DArray(filename);
            
            int startX = -1;
            int startY = -1;

            for(int i =0; i < map.Count;i++)
            {
                for(int j=0; j < map[i].Count; j++)
                {
                    if(map[i][j] == 'S')
                    {
                        startX = i;
                        startY = j;
                        break;
                    }
                }

                if(startX != -1 && startY != -1)
                    break;
            }
           
            return new Tuple<Tuple<int, int>, List<List<char>>>(new Tuple<int, int>(startX, startY), map);
        }

        public static List<List<T>> Make2D<T>(T initValue, int height, int width)
        {
            List<List<T>> result = new List<List<T>>();

            for(int i =0; i < height; i++)
            {
                result.Add(new List<T>());

                for(int j=0; j<width; j++)
                {
                    result[i].Add(initValue);
                }
            }

            return result;
        }

        public static IEnumerable<Tuple<int, int>> GetNeighbors(Tuple<int, int> focusElement, int maxX, int maxY)
        {
            if(focusElement.Item1 != 0)
            {
                yield return new Tuple<int, int>(focusElement.Item1 - 1, focusElement.Item2);
            }

            if(focusElement.Item1 != maxX)
            {
                yield return new Tuple<int, int>(focusElement.Item1 + 1, focusElement.Item2);
            }

            if(focusElement.Item2 != 0)
            {
                yield return new Tuple<int, int>(focusElement.Item1, focusElement.Item2 - 1);
            }

            if(focusElement.Item2 != maxY)
            {
                yield return new Tuple<int, int>(focusElement.Item1, focusElement.Item2 + 1);
            }
        }

        public static void PrintMap(List<List<char>> map, List<List<int>> dist, List<List<Tuple<int, int>>> prev)
        {
            for(int i = 0; i<map.Count; i++)
            {
                Console.Write(i + ": ");
                for(int j = 0; j<map.Count; j++)
                {
                    if(map[i][j] == 'S')
                        Console.Write('S');
                    else if(map[i][j] == 'E')
                        Console.Write('E');
                    else if(dist[i][j] == Int32.MaxValue)
                        Console.Write(".");
                    else if(prev[i][j].Item1 < i)
                        Console.Write("^");
                    else if(prev[i][j].Item1 > i)
                        Console.Write("v");
                    else if(prev[i][j].Item2 < j)
                        Console.Write("<");
                    else if(prev[i][j].Item2 > j)
                        Console.Write(">");
                }
                Console.WriteLine();
            }
        }


        public object Part1(string filename)
        {  
            Tuple<Tuple<int, int>, List<List<char>>> parsedData = ParseStartingData(filename);

            List<List<char>> map = parsedData.Item2;

            if(map.Count == 0)
                return "EMPTY MAP";


            List<List<int>> dist = Make2D<int>(Int32.MaxValue, map.Count, map[0].Count);            
            List<List<Tuple<int, int>>> prev = Make2D<Tuple<int, int>>(new Tuple<int, int>(-1, -1), map.Count, map[0].Count);

            List<Tuple<int, int>> myQueue = new List<Tuple<int, int>>();
            List<Tuple<int, int>> visited = new List<Tuple<int, int>>();

            myQueue.Add(parsedData.Item1);

            dist[parsedData.Item1.Item1][parsedData.Item1.Item2] = 0;

            while(myQueue.Any())
            {
                Tuple<int, int> focusElement = myQueue.OrderBy(x=> dist[x.Item1][x.Item2]).First();  
                myQueue.Remove(focusElement); 

                visited.Add(focusElement);
                
                char locChar = map[focusElement.Item1][focusElement.Item2];

                if(locChar == 'E')
                {
                    return dist[focusElement.Item1][focusElement.Item2];
                }

                foreach(Tuple<int, int> neighbor in GetNeighbors(focusElement, map.Count - 1, map[0].Count - 1))
                {
                    char mapChar = map[neighbor.Item1][neighbor.Item2];
                    
                    if(!(locChar == 'S' && mapChar == 'a') && !(locChar == 'z' && mapChar == 'E'))
                    {
                        if(mapChar - locChar > 1 || mapChar == 'E' || mapChar == 'S')
                            continue; // too hard to reach
                    }

                    int tempDist = dist[focusElement.Item1][focusElement.Item2] + 1;
                    int currDist = dist[neighbor.Item1][neighbor.Item2];

                    if(tempDist < currDist) 
                    {
                        dist[neighbor.Item1][neighbor.Item2] = tempDist;
                        prev[neighbor.Item1][neighbor.Item2] = focusElement;
                    }

                    if(!visited.Contains(neighbor) && !myQueue.Contains(neighbor))
                    {
                        myQueue.Add(neighbor);
                    }
                }
            }

            for(int i = 0; i < map.Count; i++)
            {
                for(int j = 0; j < map[i].Count; j++)
                {
                    if(visited.Contains(new Tuple<int, int>(i, j)))
                    {
                        Console.Write("-");
                    }
                    else
                    {
                        Console.Write(map[i][j]);
                    }
                }
                Console.WriteLine();
            }

            return "Ran out of friends to look at";
        }

        public object Part2(string filename)
        {
            Tuple<Tuple<int, int>, List<List<char>>> parsedData = ParseStartingData(filename);

            List<List<char>> map = parsedData.Item2;

            if(map.Count == 0)
                return "EMPTY MAP";


            List<List<int>> dist = Make2D<int>(Int32.MaxValue, map.Count, map[0].Count);            
            List<List<Tuple<int, int>>> prev = Make2D<Tuple<int, int>>(new Tuple<int, int>(-1, -1), map.Count, map[0].Count);

            List<Tuple<int, int>> myQueue = new List<Tuple<int, int>>();
            List<Tuple<int, int>> visited = new List<Tuple<int, int>>();

            for(int i = 0; i < map.Count; i++)
            {
                for(int j = 0; j < map[i].Count; j++)
                {
                    if(map[i][j] == 'S')
                    {
                        map[i][j] = 'a';
                    }

                    if(map[i][j] == 'a')
                    {
                        myQueue.Add(new Tuple<int, int>(i,j));
                        dist[i][j] = 0;
                    }

                }
                Console.WriteLine();
            }

            while(myQueue.Any())
            {
                Tuple<int, int> focusElement = myQueue.OrderBy(x=> dist[x.Item1][x.Item2]).First();  
                myQueue.Remove(focusElement); 

                visited.Add(focusElement);
                
                char locChar = map[focusElement.Item1][focusElement.Item2];

                if(locChar == 'E')
                {
                    return dist[focusElement.Item1][focusElement.Item2];
                }

                foreach(Tuple<int, int> neighbor in GetNeighbors(focusElement, map.Count - 1, map[0].Count - 1))
                {
                    char mapChar = map[neighbor.Item1][neighbor.Item2];
                    
                    if(!(locChar == 'S' && mapChar == 'a') && !(locChar == 'z' && mapChar == 'E'))
                    {
                        if(mapChar - locChar > 1 || mapChar == 'E' || mapChar == 'S')
                            continue; // too hard to reach
                    }

                    int tempDist = dist[focusElement.Item1][focusElement.Item2] + 1;
                    int currDist = dist[neighbor.Item1][neighbor.Item2];

                    if(tempDist < currDist) 
                    {
                        dist[neighbor.Item1][neighbor.Item2] = tempDist;
                        prev[neighbor.Item1][neighbor.Item2] = focusElement;
                    }

                    if(!visited.Contains(neighbor) && !myQueue.Contains(neighbor))
                    {
                        myQueue.Add(neighbor);
                    }
                }
            }

            return "Ran out of friends to look at";
        }
    }
}