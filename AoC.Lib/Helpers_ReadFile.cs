using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Lib
{
    public partial class Helpers
    {        
        public static IEnumerable<string> ReadFile(string filename)
        {
            Directory.GetCurrentDirectory();
            
            if (File.Exists(@"..\AoC.Lib\Inputs\" + filename))
            {
                return System.IO.File.ReadLines(@"..\AoC.Lib\Inputs\" + filename);
            }
            return new List<string>();
        }

        public static IEnumerable<int?> ReadFileAsInts(string filename)
        {
            int temp;
            foreach (string line in ReadFile(filename))
            {
                if (int.TryParse(line, out temp))
                {
                    yield return temp;
                }
                else
                {
                    yield return null;
                }
            }
        }

        public static List<List<int>> ReadFileAs2DArrayOfInts(string filename)
        {
            List<List<int>> outer = new List<List<int>>();

            foreach (string line in ReadFile(filename))
            {
                outer.Add(ReadCharsAsInt(line).ToList());
            }

            return outer;
        }

        public static List<List<char>> ReadFileAs2DArray(string filename)
        {
            List<List<char>> outer = new List<List<char>>();

            foreach (string line in ReadFile(filename))
            {
                outer.Add(new List<char>(line.ToCharArray()));
            }

            return outer;
        }

        private static IEnumerable<int> ReadCharsAsInt(string line)
        {
            foreach (char c in line)
            {
                yield return int.Parse(c.ToString());
            }
        }
    }
}
