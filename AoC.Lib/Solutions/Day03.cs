using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Lib.Solutions
{
    public class Day03 : ISolution
    {
        private int CalcValue(char c)
        {
            if ((int)c >= 97)
            {
                return 1 + c - (int)'a';
            }
            else
            {
                return 27 + c - (int)'A';
            }
        }

        public object Part1(string filename)
        {
            int sum = 0;
            foreach (string line in Helpers.ReadFile(filename))
            {
                char[] parts = line.ToCharArray();

                int half = parts.Length / 2;

                IEnumerable<char> first = parts.Take(half).ToList().Distinct();
                IEnumerable<char> second = parts.Skip(half).Take(half).ToList().Distinct();

                foreach (char c in first)
                {
                    if(second.Contains(c))
                    {
                        sum += CalcValue(c);
                    }
                }
            }
            return sum;
        }

        public object Part2(string filename)
        {
            int sum = 0;

            List<char[]> lines = Helpers.ReadFile(filename).Select(s => s.Distinct().ToArray()).ToList();

            for (int i = 0; i < lines.Count; i += 3)
            {
                foreach(char c in lines[i])
                {
                    if(lines[i+1].Contains(c) && lines[i+2].Contains(c))
                    {
                        sum += CalcValue(c);
                        break;
                    }
                }
            }

            return sum;
        }
    }
}
