using System.Diagnostics;
using System.Text;

var readLines = File.ReadLines("input.txt").ToList();

var stopwatch = Stopwatch.StartNew();
Console.WriteLine($"Part One: {TaskOne(readLines)}");
Console.WriteLine($"Part Two: {TaskTwo(readLines)}");
Console.WriteLine($"Elapsed MS: {stopwatch.ElapsedMilliseconds}");

return;

int TaskOne(List<string> input)
{
    var map = Enumerable.Range(0, input.Count)
        .Select(_ => Enumerable.Repeat(0, input.Count).ToArray())
        .ToArray();

    for (var k = 0; k < map.Length; k++)
    {
        for (var j = 0; j < map[j].Length - 1; j++)
            map[k][j] += input[k][j + 1] == '@' && input[k][j] == '@' ? 1 : 0;

        for (var j = map[k].Length - 1; j > 0; j--)
            map[k][j] += input[k][j - 1] == '@' && input[k][j] == '@' ? 1 : 0;

        for (var j = 0; j < map.Length - 1; j++)
            map[j][k] += input[j + 1][k] == '@' && input[j][k] == '@' ? 1 : 0;

        for (var j = map.Length - 1; j > 0; j--)
            map[j][k] += input[j - 1][k] == '@' && input[j][k] == '@' ? 1 : 0;

        if (k != map.Length - 1)
        {
            for (var j = 0; j < map.Length - 1; j++)
                map[k][j] += input[k + 1][j + 1] == '@' && input[k][j] == '@' ? 1 : 0;

            for (var j = 1; j < map.Length; j++)
                map[k][j] += input[k + 1][j - 1] == '@' && input[k][j] == '@' ? 1 : 0;
        }

        if (k != 0)
        {
            for (var j = 0; j < map.Length - 1; j++)
                map[k][j] += input[k - 1][j + 1] == '@' && input[k][j] == '@' ? 1 : 0;

            for (var j = 1; j < map.Length; j++)
                map[k][j] += input[k - 1][j - 1] == '@' && input[k][j] == '@' ? 1 : 0;
        }
    }

    var list = readLines
        .SelectMany((s, k) => s.Select((c, j) => (c, k, j)))
        .Where(tuple => tuple.c == '@').ToList();

    return list
        .Count(tuple => map[tuple.k][tuple.j] < 4);
}

int TaskTwo(List<string> input)
{
    var lines = input.Select(x => new StringBuilder(x)).ToList();

    var hadItemsRemoved = true;
    var currentCount = 0;
    while (hadItemsRemoved)
    {
        var map = Enumerable.Range(0, lines.Count)
            .Select(_ => Enumerable.Repeat(0, lines.Count).ToArray())
            .ToArray();

        for (var k = 0; k < map.Length; k++)
        {
            for (var j = 0; j < map[j].Length - 1; j++)
                map[k][j] += lines[k][j + 1] == '@' && lines[k][j] == '@' ? 1 : 0;

            for (var j = map[k].Length - 1; j > 0; j--)
                map[k][j] += lines[k][j - 1] == '@' && lines[k][j] == '@' ? 1 : 0;

            for (var j = 0; j < map.Length - 1; j++)
                map[j][k] += lines[j + 1][k] == '@' && lines[j][k] == '@' ? 1 : 0;

            for (var j = map.Length - 1; j > 0; j--)
                map[j][k] += lines[j - 1][k] == '@' && lines[j][k] == '@' ? 1 : 0;

            if (k != map.Length - 1)
            {
                for (var j = 0; j < map.Length - 1; j++)
                    map[k][j] += lines[k + 1][j + 1] == '@' && lines[k][j] == '@' ? 1 : 0;

                for (var j = 1; j < map.Length; j++)
                    map[k][j] += lines[k + 1][j - 1] == '@' && lines[k][j] == '@' ? 1 : 0;
            }

            if (k != 0)
            {
                for (var j = 0; j < map.Length - 1; j++)
                    map[k][j] += lines[k - 1][j + 1] == '@' && lines[k][j] == '@' ? 1 : 0;

                for (var j = 1; j < map.Length; j++)
                    map[k][j] += lines[k - 1][j - 1] == '@' && lines[k][j] == '@' ? 1 : 0;
            }
        }

        var list = lines
            .SelectMany((s, k) => s.ToString().Select((c, j) => (c, k, j)))
            .Where(tuple => tuple.c == '@' && map[tuple.k][tuple.j] < 4).ToList();

        foreach (var valueTuple in list)
            lines[valueTuple.k][valueTuple.j] = '.';

        hadItemsRemoved = list.Any();
        currentCount += list.Count;
    }

    return currentCount;
}