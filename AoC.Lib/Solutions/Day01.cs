using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Lib.Solutions
{
    public class Day01 : ISolution
    {
        public object Part1(string filename)
        {
            List<int> mylist = new List<int>();
            mylist.Add(0);

            foreach (int? line in Helpers.ReadFileAsInts(filename))
            {
                if (line.HasValue)
                {
                    mylist[mylist.Count - 1] += line.Value;
                }
                else
                {
                    mylist.Add(0);
                }
            }

            return mylist.Max();
        }

        public object Part2(string filename)
        {
            List<int> mylist = new List<int>();
            mylist.Add(0);

            foreach (int? line in Helpers.ReadFileAsInts(filename))
            {
                if (line.HasValue)
                {
                    mylist[mylist.Count - 1] += line.Value;
                }
                else
                {
                    mylist.Add(0);
                }
            }

            mylist.Sort();

            int sum = mylist[mylist.Count - 1] + mylist[mylist.Count - 2] + mylist[mylist.Count - 3];

            return sum;
        }
    }
}
