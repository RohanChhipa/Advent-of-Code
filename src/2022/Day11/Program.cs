using System.Diagnostics;
using System.Numerics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var lines = File.ReadAllText("input.txt")
    .Split("\n\n");

var monkeys = new List<List<BigInteger>>();
var operation = new List<Func<BigInteger, BigInteger>>();
var test = new List<long>();
var throwA = new List<int>();
var throwB = new List<int>();

foreach (var t in lines)
{
    var input = t.Split("\n");
    monkeys.Add(input[1].Replace("Starting items: ", "")
        .Split(", ")
        .Select(s => new BigInteger(long.Parse(s)))
        .ToList()
    );

    var parsed = input[2].Replace("Operation: new = old ", "").Split(" ");
    Func<BigInteger, BigInteger> op = i =>
    {
        var operand = parsed[1] switch
        {
            "old" => i,
            _ => new BigInteger(long.Parse(parsed[1]))
        };

        return parsed[0] switch
        {
            "*" => i * operand,
            "+" => i + operand,
            _ => 0
        };
    };
    operation.Add(op);

    test.Add(int.Parse(input[3].Replace("Test: divisible by ", "")));
    throwA.Add(int.Parse(input[4].Replace("If true: throw to monkey ", "")));
    throwB.Add(int.Parse(input[5].Replace("If false: throw to monkey ", "")));
}

var mod = test.Aggregate(1L, (l, l1) => l * l1);

var totalTasks = new BigInteger[monkeys.Count];
for (var round = 0; round < 10000; round++)
{
    for (var k = 0; k < monkeys.Count; k++)
    {
        totalTasks[k] += monkeys[k].Count;
        
        while (monkeys[k].Any())
        {
            var worry = monkeys[k].First();
            monkeys[k].RemoveAt(0);
            
            worry = operation[k](worry);
            // worry /= 3;
            
            monkeys[worry % test[k] == 0 ? throwA[k] : throwB[k]].Add(worry % mod);
        }
    }
    
}

var taskOne = totalTasks.OrderByDescending(i => i)
    .Take(2)
    .Aggregate(new BigInteger(1), (i, i1) => i * i1);

Console.WriteLine(taskOne);
Console.WriteLine(string.Join(", ", totalTasks));
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

