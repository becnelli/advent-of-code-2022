using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC.Lib.Solutions
{
    public class Day11 : ISolution
    {
        public List<Monkey> GetMonkeyTestList()
        {
            List<Monkey> monkeys = new List<Monkey>();
            monkeys.Add(new Monkey(0, new long[] {79, 98}, true));
            monkeys.Add(new Monkey(1, new long[] {54, 65, 75, 74}, true));
            monkeys.Add(new Monkey(2, new long[] {79, 60, 97}, true));
            monkeys.Add(new Monkey(3, new long[] {74}, true));
            return monkeys;
        }
         
        public List<Monkey> GetMonkeyList()
        {
            List<Monkey> monkeys = new List<Monkey>();
            monkeys.Add(new Monkey(0, new long[] {50, 70, 54, 83, 52, 78}, false));
            monkeys.Add(new Monkey(1, new long[] {71, 52, 58, 60, 71}, false));
            monkeys.Add(new Monkey(2, new long[] {66, 56, 56, 94, 60, 86, 73}, false));
            monkeys.Add(new Monkey(3, new long[] {83, 99}, false));
            monkeys.Add(new Monkey(4, new long[] {98, 98, 79}, false));
            monkeys.Add(new Monkey(5, new long[] {76}, false));
            monkeys.Add(new Monkey(6, new long[] {52, 51, 84, 54}, false));
            monkeys.Add(new Monkey(7, new long[] {82, 86, 91, 79, 94, 92, 59, 94}, false));

            return monkeys;
        }

        public object Part1(string filename)
        {
            List<int> inspectCount = new List<int>();
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);

            List<Monkey> monkeys = GetMonkeyTestList();
          
            foreach(Monkey monkey in monkeys)
            {
                monkey.Print();
            }
            Console.WriteLine();
            for(int i =0; i <20; i++)
            {
                //Console.WriteLine($"ROUND: {i}");
                foreach(Monkey monkey in monkeys)
                {
                    inspectCount[monkey.Id] += monkey.Items.Count;

                    foreach(long item in monkey.Items)
                    {
                        long newVal = monkey.OperateOnItem(item);
                        long postWorry = (long)Math.Floor(newVal /3.0);
                        int dest = monkey.ThrowTo(postWorry);
                        monkeys[dest].AddItem(postWorry);
                    }

                    monkey.ClearItems();
                }

                foreach(Monkey monkey in monkeys)
                {
                    //monkey.Print();
                }
                Console.WriteLine();
            }

            inspectCount.Sort();

            return inspectCount[7] * inspectCount[6];
        }

        public object Part2(string filename)
        {
            foreach(string line in Helpers.ReadFile(filename))
            {
            }
            
            return "TBD";
        }
    }

    public class Monkey
    {
        public List<long> Items {get; private set;}
        public int Id {get; private set;}

        private bool TestMonkeys;

        public Monkey(int id,IEnumerable<long> items, bool testMonkeys)
        {
            Id = id;
            Items = new List<long>(items);
            TestMonkeys = testMonkeys;
        }

        public void AddItem(long item)        
        {
            Items.Add(item);
        }

        public void ClearItems()
        {
            Items.Clear();
        }

        public long OperateOnItem(long item)
        {
            if(TestMonkeys)
            {
                switch(Id)
                {
                    case 0:
                        return item *19;
                    case 1:
                        return item +6;
                    case 2:
                        return item * item;
                    case 3:
                        return item+3;
                }
            }

            switch(Id)
            {
                case 0:
                    return item *3;
                case 1:
                    return item *item;
                case 2:
                    return item +1;
                case 3:
                    return item+8;
                case 4:
                    return item+3;
                case 5:
                    return item+4;
                case 6:
                    return item*17;
                case 7:
                    return item+7;
            }

            throw new NotImplementedException();
        }

        public void Print()
        {
            Console.WriteLine($"Monkey {Id}: " +  string.Join(", ", Items));
        }

        public int ThrowTo(long item)
        {
            if(TestMonkeys)
            {

                switch(Id)
                {
                    case 0:
                        return (item % 23 == 0) ? 2 : 3;
                    case 1:
                        return (item % 19 == 0)? 2: 0;
                    case 2:
                        return (item % 13 == 0)? 1: 3;
                    case 3:
                        return (item % 17 == 0)? 0: 1;
                }
            }
            switch(Id)
            {
                case 0:
                    return (item % 11 == 0) ? 2 : 7;
                case 1:
                    return (item % 7 == 0)? 0: 2;
                case 2:
                    return (item % 3 == 0)? 7: 5;
                case 3:
                    return (item % 5 == 0)? 6: 4;
                case 4:
                    return (item % 17 == 0)? 1: 0;
                case 5:
                    return (item % 13 == 0)? 6: 3;
                case 6:
                    return (item % 19 == 0)? 4: 1;
                case 7:
                    return (item % 2 == 0)? 5: 3;
            }


            throw new NotImplementedException();
        }


    }
}
