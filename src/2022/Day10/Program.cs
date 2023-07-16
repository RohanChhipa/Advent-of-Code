using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var lines = File.ReadAllLines("input.txt");

var x = new List<int> { };
foreach (var line in lines.Select(s => s.Split(" ")))
{
    var last = x.Count == 0 ? 1 : x.Last();
    if (line[0] == "noop")
    {
        x.Add(last);
        continue;
    }
    
    if (x.Count == 0)
        x.Add(last);

    x.AddRange(Enumerable.Repeat(last, 1));
    x.Add(x.Last() + int.Parse(line[1]));
}

Console.WriteLine(string.Join(", ", x));

var taskOne = (20 * x[19]) +
              (60 * x[59]) +
              (100 * x[99]) +
              (140 * x[139]) +
              (180 * x[179]) +
              (220 * x[219]);

var taskTwo = Enumerable.Repeat(".", 240).ToArray();
for (var k = 0; k < Math.Min(taskTwo.Length, x.Count); k++)
{
    if (k % 40 >= x[k] - 1 && k % 40 <= x[k] + 1)
        taskTwo[k] = "#";
}

Console.WriteLine(taskOne);
Console.WriteLine(string.Join("\n", Enumerable.Range(0, 6)
        .Select(i => string.Join("", taskTwo.Skip(40 * i).Take(40)))
    )
);
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");