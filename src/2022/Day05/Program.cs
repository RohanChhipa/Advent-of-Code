using System.Text;
using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var stacks = new[]
{
     new StringBuilder("QGPRLCTF"),
     new StringBuilder("JSFRWHQN"),
     new StringBuilder("QMPWHBF"),
     new StringBuilder("FDTSV"),
     new StringBuilder("ZFVWDLQ"),
     new StringBuilder("SLCZ"),
     new StringBuilder("FDVMBZ"),
     new StringBuilder("BJT"),
     new StringBuilder("HPSLGBNQ")
};

var lines = File.ReadAllLines("input.txt");

var inputs = lines.Select(line => line.Replace("move ", "")
    .Replace("from ", "")
    .Replace("to ", "")
    .Split(" ")
    .Select(int.Parse)
    .ToArray()
).ToArray();

foreach (var input in inputs)
{
   
    var move = input[0];
    var from = input[1] - 1;
    var to = input[2] - 1;

    //Task One
    // var strMove = new string(stacks[from].ToString().Substring(0, move).Reverse().ToArray());
    
    //Task Two
    var strMove = new string(stacks[from].ToString().Substring(0, move).ToArray());
    
    stacks[to].Insert(0, strMove.ToCharArray());
    stacks[from].Remove(0, move);
}

foreach (var stack in stacks)
{
    Console.Write(stack[0]);
}

Console.WriteLine();
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");