using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Numerics;

namespace AoC.Lib.Solutions
{
    public class Day11 : ISolution
    {
        public IEnumerable<Monkey> ParseMonkeyList(string filename)
        {
            List<string> lines = Helpers.ReadFile(filename).ToList();

            Regex monkeyLine = new Regex("^Monkey (\\d+):$");
            Regex itemsLine = new Regex("^\\s+Starting items: ([\\d, ]+)$");
            Regex operationLine = new Regex("^\\s+Operation: new = ([^\\s]+) (.) ([^\\s]+)$");
            Regex testLine = new Regex("^\\s+Test: divisible by (\\d+)$");
            Regex ifTrue = new Regex("^\\s+If true: throw to monkey (\\d+)$");
            Regex ifFalse = new Regex("^\\s+If false: throw to monkey (\\d+)$");

            Monkey monkey = null;

            foreach(string line in Helpers.ReadFile(filename))
            {
                if(monkeyLine.IsMatch(line))
                {
                    if(monkey != null) yield return monkey;
                
                    Match match = monkeyLine.Match(line);

                    monkey = new Monkey(Int32.Parse(match.Groups[1].Value));
                    continue;
                }

                if(monkey == null)
                    continue;

                if(itemsLine.IsMatch(line))
                {
                    Match match = itemsLine.Match(line);
                    monkey.InitializeItems(match.Groups[1].Value);
                }
                else if(operationLine.IsMatch(line))
                {
                    Match match = operationLine.Match(line);

                    string op = match.Groups[2].Value;

                    if(match.Groups[2].Value == "*" && match.Groups[3].Value == "old")
                    {
                        monkey.AddOperator((x) => x * x);
                        continue;
                    }
                    
                    int val = Int32.Parse(match.Groups[3].Value);

                    switch(match.Groups[2].Value)
                    {
                        case "*":
                            monkey.AddOperator((x) => x * val);
                            break;

                        case "+":
                            monkey.AddOperator((x) => x + val);
                            break;

                        case "-":
                            monkey.AddOperator((x) => x - val);
                            break;
                    }
                }
                else if(testLine.IsMatch(line))
                {
                    Match match = testLine.Match(line);
                    monkey.AddDivisor(Int32.Parse(match.Groups[1].Value));
                }
                else if(ifTrue.IsMatch(line))
                {
                    Match match = ifTrue.Match(line);
                    monkey.AddTrueDest(Int32.Parse(match.Groups[1].Value));
                }
                else if(ifFalse.IsMatch(line))
                {
                    Match match = ifFalse.Match(line);
                    monkey.AddFalseDest(Int32.Parse(match.Groups[1].Value));
                }
            }
            
            if(monkey != null)
                yield return monkey;
        }

        static int LCM(int[] numbers)
        {
            return numbers.Aggregate(lcm);
        }
        
        static int lcm(int a, int b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        public object Part1(string filename)
        {
            List<Monkey> monkeys = ParseMonkeyList(filename).ToList();

            List<long> inspectCount = new List<long>();
            for(int i = 0; i< monkeys.Count; i++)
            {
                inspectCount.Add(0);
            }

            for(int i =0; i < 20; i++)
            {
                foreach(Monkey monkey in monkeys)
                {
                    inspectCount[monkey.Id] += monkey.Items.Count;

                    foreach(long item in monkey.Items)
                    {
                        long newVal = monkey.OperateOn(item);
                        long postWorry = (long)Math.Floor(newVal / 3.0);
                        int dest = monkey.GetDestination(postWorry);
                        monkeys[dest].AddItem(postWorry);
                    }

                    monkey.ClearItems();
                }
            }

            inspectCount.Sort();

            return inspectCount[monkeys.Count - 1] * inspectCount[monkeys.Count -2];
        }

        public object Part2(string filename)
        {
            List<Monkey> monkeys = ParseMonkeyList(filename).ToList();

            if(!monkeys.Any())
            {
                return "IDK";
            }

            List<long> inspectCount = new List<long>();
            for(int i = 0; i< monkeys.Count; i++)
            {
                inspectCount.Add(0);
            }

            long myLCM = LCM(monkeys.Select(x => x.Divisor).ToArray());

            for(int i =0; i < 10000; i++)
            {
                foreach(Monkey monkey in monkeys)
                {
                    inspectCount[monkey.Id] += monkey.Items.Count;

                    foreach(long item in monkey.Items)
                    {
                        long newVal = monkey.OperateOn(item);
                        long postWorry = newVal % myLCM;
                        int dest = monkey.GetDestination(postWorry);
                        monkeys[dest].AddItem(postWorry);
                    }

                    monkey.ClearItems();
                }
            }

            inspectCount.Sort();

            return inspectCount[monkeys.Count - 1] * inspectCount[monkeys.Count -2];
        }
    }

    public class Monkey
    {
        public List<long> Items { get; private set; }

        public int Id {get; private set;}

        public int Divisor {get;private set;}
        
        public int TrueDest {get;private set;}
        
        public int FalseDest {get;private set;}
        
        private Func<long, long> Operation {  get; set; }

        public Monkey(int id)
        {
            this.Id = id;
            this.Items = new List<long>();
            Operation = (x) => x; // default to returning item (aka x)
        }

        public void InitializeItems(string items)
        {
            this.Items.Clear();
            foreach(string item in items.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                Items.Add(long.Parse(item));
            }
        }

        public void AddItem(long item)
        {
            Items.Add(item);
        }

        public void ClearItems()
        {
            Items.Clear();
        }

        public void AddDivisor(int divisor)
        {
            this.Divisor = divisor;
        }

        public void AddTrueDest(int dest)
        {
            this.TrueDest = dest;
        }

        public void AddFalseDest(int dest)
        {
            this.FalseDest = dest;
        }

        public void AddOperator(Func<long, long> operation)
        {
            Operation = operation;
        }

        public long OperateOn(long item)
        {
            return Operation.Invoke(item);
        }

        public int GetDestination(long item)
        {
            return item % Divisor == 0 ? TrueDest : FalseDest;
        }

    }
}
