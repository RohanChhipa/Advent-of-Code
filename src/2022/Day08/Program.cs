using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var lines = File.ReadAllLines("input.txt");

var input = lines.Select(s => s.ToCharArray()
    .Select(c => c - '0')
    .ToArray()
    ).ToArray();

var taskOne = input.Length * input[0].Length;
var taskTwo = 0;

for (var k = 1; k < input.Length - 1; k++)
{
    for (var j = 1; j < input[k].Length - 1; j++)
    {
        var a = Enumerable.Range(0, j).Select(i => input[k][i]).ToArray();        
        var b = Enumerable.Range(j + 1, input[k].Length - j - 1).Select(i => input[k][i]).ToArray();
        var c = Enumerable.Range(0, k).Select(i => input[i][j]).ToArray();
        var d = Enumerable.Range(k + 1, input.Length - k - 1).Select(i => input[i][j]).ToArray();
        
        if (a.Any(i => i >= input[k][j])
            && b.Any(i => i >= input[k][j]) 
            && c.Any(i => i >= input[k][j]) 
            && d.Any(i => i >= input[k][j]))
        {
            taskOne--;
        }

        var sA = a.Length - (a.Select((i, idx) => new {i, idx})
            .LastOrDefault(tuple => tuple.i >= input[k][j])
            ?.idx ?? 0);

        var sB = b.Select((i, idx) => new {i, idx})
            .FirstOrDefault(tuple => tuple.i >= input[k][j])
            ?.idx + 1?? b.Length;

        var sC = c.Length - (c.Select((i, idx) => new {i, idx})
            .LastOrDefault(tuple => tuple.i >= input[k][j])
            ?.idx ?? 0);

        var sD = d.Select((i, idx) => new {i, idx})
            .FirstOrDefault(tuple => tuple.i >= input[k][j])
            ?.idx + 1 ?? d.Length;

        taskTwo = Math.Max(taskTwo, sA * sB * sC * sD);
    }
}

Console.WriteLine(taskOne);
Console.WriteLine(taskTwo);
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");
