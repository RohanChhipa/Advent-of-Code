using System.Diagnostics;

var input = File.ReadAllText("./input.txt")
    .Split(",")
    .Select(s => s.Split("-").Select(long.Parse).ToArray())
    .ToArray();

var stopwatch = Stopwatch.StartNew();
Console.WriteLine($"Task One: {TaskOne()}");
Console.WriteLine($"Task One: {TaskTwo()}");
Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}");

return;

long TaskOne()
{
    long total = 0;
    foreach (var range in input)
    {
        var list = new List<long>();
        for (var k = range[0]; k <= range[1]; k++)
        {
            if (FindPatternOne(k.ToString()))
            {
                list.Add(k);
            }
        }

        total += list.Sum();
    }

    return total;
}


long TaskTwo()
{
    long total = 0;
    foreach (var range in input)
    {
        var list = new List<long>();
        for (var k = range[0]; k <= range[1]; k++)
        {
            if (FindPatternTwo(k.ToString()))
            {
                list.Add(k);
            }
        }

        total += list.Sum();
    }

    return total;
}

bool FindPatternOne(string pattern)
{
    if (pattern.Length % 2 != 0)
        return false;

    return pattern.Substring(0, pattern.Length / 2) == pattern.Substring(pattern.Length / 2);
}


bool FindPatternTwo(string pattern)
{
    if (pattern.IsWhiteSpace())
        return false;

    var setSize = Enumerable.Range(1, pattern.Length).First(x => pattern.Length % x == 0);
    while (setSize <= pattern.Length / 2)
    {
        var sets = Enumerable.Range(0, pattern.Length / setSize)
            .Select(x => pattern.Substring(x * setSize, setSize))
            .ToList();

        if (sets.All(x => x == sets.First()))
            return true;

        setSize = Enumerable.Range(setSize + 1, pattern.Length).First(x => pattern.Length % x == 0);
    }

    return false;
}