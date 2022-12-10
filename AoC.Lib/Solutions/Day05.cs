using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC.Lib.Solutions
{
    public class Day05 : ISolution
    {
        private int CalcVal(object item)
        {
            return 0;
        }

        private Tuple<List<List<string>>, List<Operations>> ReadData(string filename)
        {

            List<List<string>> stacks = new List<List<string>>();
            List<Operations > moves = new List<Operations>();

            Regex r = new Regex("move (\\d+) from (\\d+) to (\\d+)");

            foreach (string line in Helpers.ReadFile(filename))
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                if(line.StartsWith("move"))
                {
                    Match m = r.Match(line);
                    List<Group> vals = m.Groups.Values.ToList();

                    Operations obj = new Operations();
                    obj.NumToMove = Int32.Parse(vals[1].Value);
                    obj.Source = Int32.Parse(vals[2].Value);
                    obj.Dest = Int32.Parse(vals[3].Value);

                    moves.Add(obj);
                }
                else
                {
                    for (int i = 0; i <= line.Length / 4; i++)
                    {
                        if (stacks.Count <= i+1)
                        {
                            stacks.Add(new List<string>());
                        }

                        if (line.Substring(i * 4, 1) == "[")
                        {
                            string c = line.Substring(i * 4 + 1, 1);

                            if (!string.IsNullOrWhiteSpace(c))
                            {
                                stacks[i].Add(c);
                            }
                        }
                    }
                }
            }

            stacks.ForEach(x => x.Reverse());

            return new Tuple<List<List<string>>, List<Operations>>(stacks, moves);
        }

        public object Part1(string filename)
        {
            Tuple<List<List<string>>, List<Operations>> data = ReadData(filename);

            List<List<string>> stacks = data.Item1;

            foreach (Operations op in data.Item2)
            {
                for (int i = 0; i < op.NumToMove; i++)
                {
                    stacks[op.Dest - 1].Add(stacks[op.Source - 1][stacks[op.Source -1].Count - 1]);
                    stacks[op.Source - 1].RemoveAt(stacks[op.Source - 1].Count - 1);
                }

            }
            
            
            string result = "";

            foreach(List<string> stack in stacks)
            {
                if(stack.Any())
                    result += stack.Last();
            }

            return result;
        }

        public object Part2(string filename)
        {
            Tuple<List<List<string>>, List<Operations>> data = ReadData(filename);

            List<List<string>> stacks = data.Item1;

            foreach (Operations op in data.Item2)
            {
                stacks[op.Dest - 1].AddRange(stacks[op.Source - 1].TakeLast(op.NumToMove));

                for (int i = 0; i < op.NumToMove; i++)
                {
                    stacks[op.Source - 1].RemoveAt(stacks[op.Source - 1].Count - 1);
                }
            }

            string result = "";

            foreach (List<string> stack in stacks)
            {
                if (stack.Any())
                    result += stack.Last();
            }

            return result;
        }
    }

    class Operations
    {
        public int NumToMove;
        public int Source;
        public int Dest;
    }
}
