using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC.Lib.Solutions
{
    public class Day06 : ISolution
    {
        private int GetPosition(string input, int size)
        {
            for (int i = 0; i < input.Length - size; i++)
            {
                if (input.Skip(i).Take(size).Distinct().Count() == size)
                    return i + size;
            }

            return -1;
        }

        public object Part1(string filename)
        {
            string input = Helpers.ReadFile(filename).First();

            return GetPosition(input, 4);
        }

        public object Part2(string filename)
        {
            string input = Helpers.ReadFile(filename).First();

            return GetPosition(input, 14);
        }
    }
}
