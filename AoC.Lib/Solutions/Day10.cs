using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC.Lib.Solutions
{
    public class Day10 : ISolution
    {
        int[] specials= {20, 60, 100, 140, 180, 220};

        public object Part1(string filename)
        {
            int opCount = 1;
            int val = 1;
            int res = 0;
            foreach(string line in Helpers.ReadFile(filename).ToList())
            {
                string[] parts = line.Split(" ");
                int opMov = 1;
                int movVal = 0;

                if(parts[0] == "addx")
                {
                    opMov++;
                    movVal += Int32.Parse(parts[1]);
                }
                
                if(specials.Contains(opCount+1) && opMov ==2)
                {
                    res += val * (opCount+1);
                }
                
                val += movVal;

                opCount+=opMov;

                if(specials.Contains(opCount))
                    res += val * opCount;
            }

            return res;
        }

        public object Part2(string filename)
        {
            int val = 1;

            List<int> cycleVals= new List<int>();
           // cycleVals.Add(val);

            foreach(string line in Helpers.ReadFile(filename).ToList())
            {
                string[] parts = line.Split(" ");

                if(parts[0] == "addx")
                {
                    cycleVals.Add(val);
                    cycleVals.Add(val);
                    val += Int32.Parse(parts[1]);
                }
                else
                {
                    cycleVals.Add(val);
                }
            }

            if(cycleVals.Count == 0)
                return "NO DATA";

            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 40;j++)
                {
                    int currCycle = i*40 + j;

                    List<int> spritePos = GetSpritePositions(cycleVals[currCycle]).ToList();

                    if(spritePos.Contains(j))
                    {
                        Console.Write("##"); // extra thick so I can read
                    }
                    else
                    {                     
                        Console.Write(".."); // extra thick so I can read
                    }

                }
                Console.WriteLine();
            }
            
            return "^^^^^ LOOK AT THE LETTERS ABOVE ^^^^^";
        }

        public IEnumerable<int> GetSpritePositions(int val)
        {
            yield return val-1;
            yield return val;
            yield return val+1;
        }
    }
}
