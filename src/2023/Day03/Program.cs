var lines = File.ReadLines("input.txt")
    .ToList();

var symbols = lines.SelectMany((s, row) =>
        s.Select((c, col) => (c, row, col))
            .Where(tuple => !char.IsDigit(tuple.c) && tuple.c != '.')
            .ToList()
    )
    .ToList();

var numberRanges = new List<(int, int, int, int)>();
for (var k = 0; k < lines.Count; k++)
{
    var start = -1;
    for (var j = 0; j < lines[k].Length; j++)
    {
        if (char.IsDigit(lines[k][j]) && start < 0)
        {
            start = j;
        }

        if (!char.IsDigit(lines[k][j]) && start != -1)
        {
            numberRanges.Add((k, start, j - 1, int.Parse(lines[k].Substring(start, j - start))));
            start = -1;
        }
    }

    if (start != -1)
    {
        numberRanges.Add((k, start, lines[k].Length - 1,
            int.Parse(lines[k].Substring(start, lines[k].Length - start))));
    }
}

var l = new List<int>();
foreach (var numberRange in numberRanges)
{
    var valueTuples = Enumerable.Range(numberRange.Item1 - 1, 3)
        .SelectMany(row => Enumerable.Range(numberRange.Item2 - 1, numberRange.Item3 - numberRange.Item2 + 3)
            .Select(col => (row, col)))
        .Where(tuple => (tuple.row >= 0 && tuple.row < lines.Count)
                        && (tuple.col >= 0 && tuple.col < lines[0].Length))
        .ToList();

    if (!valueTuples.Any(tuple =>
            symbols.Any(valueTuple => valueTuple.row == tuple.row && valueTuple.col == tuple.col)))
    {
        l.Add(numberRange.Item4);
    }
}

Console.WriteLine($"Task One: {numberRanges.Sum(tuple => tuple.Item4) - l.Sum()}");

var enumerable = symbols.Where(tuple => tuple.c == '*')
    .Select(tuple =>
    {
        var valueTuples = Enumerable.Range(tuple.row - 1, 3)
            .SelectMany(row => Enumerable.Range(tuple.col - 1, 3)
                .Select(col => (row, col)))
            .Where(tuple => (tuple.row >= 0 && tuple.row < lines.Count)
                            && (tuple.col >= 0 && tuple.col < lines[0].Length))
            .ToList();

        return valueTuples.Select(valueTuple =>
            numberRanges.FirstOrDefault(tuple1 => valueTuple.row == tuple1.Item1
                                         && valueTuple.col >= tuple1.Item2 &&
                                         valueTuple.col <= tuple1.Item3)
        )
            .Select(valueTuple => valueTuple.Item4)
            .Where(valueTuple => valueTuple != default)
            .Distinct()
            .ToList();
    }).Where(list => list.Count > 1).ToList();

Console.WriteLine($"Task Two: {enumerable.Sum(list => list.Aggregate((tuple, valueTuple) => tuple * valueTuple))}");