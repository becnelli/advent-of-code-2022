using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Lib
{
    public partial class Helpers
    {        
        public static void Print2DArray(IEnumerable<IEnumerable<int>> array)
        {
            foreach(var item in array)
            {
                Console.Write("[");
                foreach(var item2 in item)
                {
                    Console.Write(item2.ToString() + ",\t");
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }
    }
}
