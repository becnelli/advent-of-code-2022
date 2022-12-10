using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC.Lib.Solutions
{
    public class Day09 : ISolution
    {
        public object Part1(string filename)
        {
            HashSet<string> cords = new HashSet<string>();

            int hx = 0;
            int hy = 0;
            int tx = 0;
            int ty = 0;

            cords.Add("[0,0]");

            foreach (string line in Helpers.ReadFile(filename))
            {
                char dir = line[0];
                int steps = Int32.Parse(line.Split(" ")[1]);

                for (int i = 0; i < steps; i++)
                {
                    switch (dir)
                    {
                        case 'U':
                            hy += -1;
                            break;
                        case 'D':
                            hy += 1;
                            break;
                        case 'L':
                            hx += -1;
                            break;
                        case 'R':
                            hx += 1;
                            break;
                    }

                    int yDiff = Math.Abs(hy - ty);
                    int xDiff = Math.Abs(hx - tx);

                    if (Math.Abs(xDiff) == 2 && yDiff == 0) // horiz
                    {
                        if (hx > tx)
                        {
                            tx++;
                        }
                        else
                        {
                            tx--;
                        }
                        cords.Add($"[{tx},{ty}]");
                    }
                    else if (Math.Abs(yDiff) == 2 && xDiff == 0) // vert
                    {
                        if (hy > ty)
                        {
                            ty++;
                        }
                        else
                        {
                            ty--;
                        }
                        cords.Add($"[{tx},{ty}]");
                    }
                    else if (yDiff + xDiff > 2)
                    {
                        if (hy > ty)
                        {
                            ty++;
                        }
                        else
                        {
                            ty--;
                        }
                        if (hx > tx)
                        {
                            tx++;
                        }
                        else
                        {
                            tx--;
                        }
                        cords.Add($"[{tx},{ty}]");
                    }

                }
            }

            return cords.Count;
        }

        public object Part2(string filename)
        {
            HashSet<string> cords = new HashSet<string>();

            int hx = 0;
            int hy = 0;

            List<Tuple<int, int>> tails = new List<Tuple<int, int>>();

            for (int i = 0; i < 9; i++)
            {
                tails.Add(new Tuple<int, int>(0, 0));
            }

            cords.Add("[0,0]");

            foreach (string line in Helpers.ReadFile(filename))
            {
                char dir = line[0];
                int steps = Int32.Parse(line.Split(" ")[1]);

                for (int i = 0; i < steps; i++)
                {
                    switch (dir)
                    {
                        case 'U':
                            hy += -1;
                            break;
                        case 'D':
                            hy += 1;
                            break;
                        case 'L':
                            hx += -1;
                            break;
                        case 'R':
                            hx += 1;
                            break;
                    }

                    tails[0] = MoveTail(hx, hy, tails[0].Item1, tails[0].Item2);

                    for(int t = 1; t<tails.Count; t++)
                    {
                        tails[t] = MoveTail(tails[t-1].Item1, tails[t-1].Item2, tails[t].Item1, tails[t].Item2);
                    }
                    cords.Add($"[{tails[8].Item1},{tails[8].Item2}]");
                }
            }

            return cords.Distinct().Count();
        }

        private static Tuple<int, int> MoveTail(int hx, int hy, int tx, int ty)
        {
            int yDiff = Math.Abs(hy - ty);
            int xDiff = Math.Abs(hx - tx);

            if (Math.Abs(xDiff) == 2 && yDiff == 0) // horiz
            {
                if (hx > tx)
                {
                    tx++;
                }
                else
                {
                    tx--;
                }
            }
            else if (Math.Abs(yDiff) == 2 && xDiff == 0) // vert
            {
                if (hy > ty)
                {
                    ty++;
                }
                else
                {
                    ty--;
                }
            }
            else if (yDiff + xDiff > 2)
            {
                if (hy > ty)
                {
                    ty++;
                }
                else
                {
                    ty--;
                }
                if (hx > tx)
                {
                    tx++;
                }
                else
                {
                    tx--;
                }
            }
            return new Tuple<int, int>(tx, ty);
        }
    }
}
