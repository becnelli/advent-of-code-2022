using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC.Lib.Solutions
{
    public class Day08 : ISolution
    {

        public object Part1(string filename)
        {
            List<List<int>> grid = Helpers.ReadFileAs2DArray(filename);

            int maxX = grid.Count - 1;
            int maxY = grid[0].Count - 1;

            int visibleCount = 0;

            for(int x = 0; x <= maxX; x++)
            {
                if(x == 0 || x == maxX)
                {
                    visibleCount += grid[x].Count;
                    continue;
                }

                for(int y = 0; y <= maxY; y++)
                {
                    if(y == 0 || y == maxY)
                    {
                        visibleCount++;
                        continue;
                    }

                    int currTree = grid[x][y];

                    if(!grid[x].Take(y).Any(v => v >= currTree))
                    {
                        visibleCount++;
                    }
                    else if(!grid[x].Skip(y + 1).Any(v=> v >= currTree))
                    {
                        visibleCount++;
                    }
                    else if (!grid.Take(x).Any(c => c[y] >= currTree))
                    {
                        visibleCount++;
                    }
                    else if (!grid.Skip(x + 1).Any(c => c[y] >= currTree))
                    {
                        visibleCount++;
                    }
                }
            }

            return visibleCount;
        }

        public object Part2(string filename)
        {
            List<List<int>> grid = Helpers.ReadFileAs2DArray(filename);

            int maxX = grid.Count - 1;
            int maxY = grid[0].Count - 1;

            int maxScenic = 0;

            for (int x = 0; x <= maxX-1; x++)
            {

                for (int y = 0; y <= maxY-1; y++)
                {
                    int currTree = grid[x][y];

                    int l = 0;
                    foreach (int t in grid[x].Take(y).Reverse())
                    {
                        l++;

                        if (t >= currTree) break;
                    }

                    int r = 0;
                    foreach (int t in grid[x].Skip(y + 1))
                    {
                        r++;
                        if (t >= currTree) break;
                    }

                    int u = 0;
                    foreach (int t in grid.Take(x).Select(c => c[y]).Reverse())
                    {
                        u++;
                        if (t >= currTree) break;
                    }

                    int d = 0;
                    foreach (int t in grid.Skip(x + 1).Select(c => c[y]))
                    {
                        d++;
                        if (t >= currTree) break;
                    }

                    int score = l * r * u * d;

                    if (score > maxScenic)
                    {
                        maxScenic = score;
                    }

                }
            }

            return maxScenic;
        }
    }
}
