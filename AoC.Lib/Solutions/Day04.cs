using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Lib.Solutions
{
    public class Day04 : ISolution
    {
        private int CalcVal(object item)
        {
            return 0;
        }

        public object Part1(string filename)
        {
            int sum = 0;
            foreach (string line in Helpers.ReadFile(filename))
            {
                string[] parts = line.Split(",");
                
                int s1 = Int32.Parse(parts[0].Split("-")[0]);
                int e1 = Int32.Parse(parts[0].Split("-")[1]);

                int s2 = Int32.Parse(parts[1].Split("-")[0]);
                int e2 = Int32.Parse(parts[1].Split("-")[1]);

                if ((s1 <= s2 && e1 >= e2) || (s2 <= s1 && e2 >= e1))
                    sum++;

            }
            return sum;
        }

        public object Part2(string filename)
        {
            int sum = 0;
            foreach (string line in Helpers.ReadFile(filename))
            {
                string[] parts = line.Split(",");

                int s1 = Int32.Parse(parts[0].Split("-")[0]);
                int e1 = Int32.Parse(parts[0].Split("-")[1]);

                int s2 = Int32.Parse(parts[1].Split("-")[0]);
                int e2 = Int32.Parse(parts[1].Split("-")[1]);

                // part 1 starts before part 2 AND part 1 ends after part 2 starts
                if(s1 <= s2 && e1 >= s2)
                {
                    sum++;
                    continue;
                }

                // part 2 starts before part 1 AND part 2 ends after part 1 starts
                if (s2 <= s1 && e2 >= s1)
                {
                    sum++;
                    continue;
                }
            }
            return sum;
        }
    }
}
