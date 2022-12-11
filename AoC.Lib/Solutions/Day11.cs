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
        public List<Monkey> GetMonkeyTestList()
        {
            List<Monkey> monkeys = new List<Monkey>();
            monkeys.Add(new Monkey(0, new long[] {79, 98}, true));
            monkeys.Add(new Monkey(1, new long[] {54, 65, 75, 74}, true));
            monkeys.Add(new Monkey(2, new long[] {79, 60, 97}, true));
            monkeys.Add(new Monkey(3, new long[] {74}, true));
            return monkeys;
        }

        static long LCM(long[] numbers)
        {
            return numbers.Aggregate(lcm);
        }
        
        static long lcm(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
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
            List<long> inspectCount = new List<long>();
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);
            inspectCount.Add(0);

            bool testData = false;

            List<Monkey> monkeys = testData ? GetMonkeyTestList(): GetMonkeyList();

            long myLCM =testData ? LCM(new long[]{19, 23, 13, 17}) : LCM(new long[]{11, 7, 3, 5, 17, 13, 19, 2});


          
            foreach(Monkey monkey in monkeys)
            {
                monkey.Print(0);
            }

            Console.WriteLine();
            for(int i =0; i <10000; i++)
            {
                //Console.WriteLine($"ROUND: {i}");
                foreach(Monkey monkey in monkeys)
                {
                    inspectCount[monkey.Id] += monkey.Items.Count;

                    foreach(Item item in monkey.Items)
                    {
                        long newVal = monkey.OperateOnItem(item.Value);
                        long postWorry = newVal % myLCM; //newval > myLCM ? (long)Math.Floor(newVal / (double)myLCM) : newVal;
                        int dest = monkey.ThrowTo(postWorry);
                        monkeys[dest].AddItem(new Item(item.Id, postWorry));
                    }

                    monkey.ClearItems();
                }
/*
                foreach(Monkey monkey in monkeys)
                {
                    //monkey.Print(inspectCount[monkey.Id]);
                }*/

                if( i == 0 || i == 19 || i == 999 )
                {
                Console.WriteLine(string.Join(",", inspectCount));
                Console.WriteLine();
                }
                
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

    public class Item 
    {
        public long Value {get;set;}
        public string Id {get;set;}

        public Item(string id, long val)
        {
            this.Id = id;
            this.Value = val;
        }
    }

    public class Monkey
    {
        public List<Item> Items {get; private set;}
        public int Id {get; private set;}

        private bool TestMonkeys;

        public Monkey(int id,long[]  items, bool testMonkeys)
        {
            Id = id;

            Items = new List<Item>();
            for(int i = 0; i < items.Count(); i++)
            {
                Items.Add(new Item(id + "-" + i, items[i]));
            }

            TestMonkeys = testMonkeys;
        }

        public void AddItem(Item item)        
        {
            Items.Add(item);
/*
            if(TestMonkeys)
            {
                switch(Id)
                {
                    case 0:
                        return item * 19;
                    case 1:
                        return item + 6;
                    case 2:
                        return item * item;
                    case 3:
                        return item + 3;
                }
                switch(Id)
                {
                    case 0:
                        return (item % 23 == 0) ? 2 : 3;
                    case 1:
                        return (item % 19 == 0) ? 2 : 0;
                    case 2:
                        return (item % 13 == 0) ? 1 : 3;
                    case 3:
                        return (item % 17 == 0) ? 0 : 1;
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
            }*/
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


        public void Print(int inspectCount)
        {
            Console.WriteLine($"Monkey {Id} ({inspectCount}): " +  string.Join(", ", Items.Select(x=>x.Value + "/"+ x.Id)));
        }

    }
}
