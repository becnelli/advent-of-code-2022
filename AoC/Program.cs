using AoC.Lib;

int day = 14;//DateTime.Now.Day;
string dayStr = day.ToString("D2");


Console.WriteLine("Solving for AoC Day " + day);
Console.WriteLine();

string objectToInstantiate = $"AoC.Lib.Solutions.Day{dayStr}, AoC.Lib";

var objectType = Type.GetType(objectToInstantiate);

if(objectType == null)
{
    Console.WriteLine("Unknown Day: " + day);
    return;
}

ISolution? sln = Activator.CreateInstance(objectType) as ISolution;

if(sln == null)
{
    Console.WriteLine("Problem creating instance of " + objectType);
    return;
}

string file = $"{dayStr}.txt";
string testFile = $"{dayStr}t.txt";
string testFile2 = $"{dayStr}t2.txt";

Console.WriteLine("Part 1 Test: " + sln.Part1(testFile));
//Console.WriteLine("Part 1: " + sln.Part1(file));

Console.WriteLine();

Console.WriteLine("Part 2 Test: " + sln.Part2(testFile));
Console.WriteLine("Part 2 Test2: " + sln.Part2(testFile2));
Console.WriteLine("Part 2: " + sln.Part2(file));

Console.WriteLine();
