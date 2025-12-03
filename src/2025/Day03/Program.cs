using System.Diagnostics;
using System.Text;

var readLines = File.ReadLines("input.txt").ToList();

var stopwatch = Stopwatch.StartNew();
// Console.WriteLine($"Part One: {TaskOne(readLines)}");
Console.WriteLine($"Part Two: {TaskTwo(readLines)}");
Console.WriteLine($"Elapsed MS: {stopwatch.ElapsedMilliseconds}");

return;

// long TaskOne(List<string> input)
// {
//     return input.Select(cell => Recurse(cell, 0, -1, new StringBuilder(), 2)).Sum();
// }

long TaskTwo(List<string> input)
{
    var depth = 12;
    var value = 0L;
    foreach (var se in input)
    {
        var valueTuples = se.Select((c, i) => (c, i)).OrderByDescending(x => x.c)
            .Where(x => x.i + depth - 1 < se.Length);

        var maxes = valueTuples.Where(x => x.c == valueTuples.First().c);

        var max = new List<string>();
        foreach (var valueTuple in maxes)
        {
            var s = Recurse(se, 0, valueTuple.i - 1, new StringBuilder(), depth, max);
        }

        // Console.WriteLine(max.Max());
        value += long.Parse(max.Max());
    }

    return value;
}

string Recurse(string cell, int level, int index, StringBuilder current, int maxDepth, List<string> maxes)
{
    if (level == maxDepth)
    {
        maxes.Add(current.ToString());
        return current.ToString();
    }

    var currentMaxValue = 0;

    for (var i = index + 1; i < cell.Length; i++)
    {
        if (cell[i] > currentMaxValue)
        {
            currentMaxValue = cell[i];
            current.Append(cell[i]);
            Recurse(cell, level + 1, i, current, maxDepth, maxes);
            current.Remove(current.Length - 1, 1);
        }
    }

    return "";
}