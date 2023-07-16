var rawInput = File.ReadLines("input.txt")
    .Select(s => s.Split(" -> "))
    .Select(strings =>
    {
        var (fromX, fromY) = ParseInput(strings.First());
        var (toX, toY) = ParseInput(strings.Last());

        if (fromX == toX || fromY == toY)
        {
            if (toX < fromX)
                (fromX, toX) = Swap(fromX, toX);

            if (toY < fromY)
                (fromY, toY) = Swap(fromY, toY);
        }

        return (fromX, fromY, toX, toY);
    })
    .ToList();

TaskOne();
TaskTwo();

void TaskOne()
{
    var input = rawInput
        .Where(tuple => tuple.fromX == tuple.toX || tuple.fromY == tuple.toY)
        .ToList();

    var visited = new Dictionary<(int, int), int>();
    foreach (var pair in input)
    {
        for (var k = pair.fromX; k <= pair.toX; k++)
        {
            for (var j = pair.fromY; j <= pair.toY; j++)
            {
                var point = (k, j);
                if (!visited.ContainsKey(point))
                    visited.Add(point, 0);

                visited[point]++;
            }
        }
    }

    Console.WriteLine(visited.Values.Count(i => i > 1));
}

void TaskTwo()
{
    var straightLines = rawInput
        .Where(tuple => tuple.fromX == tuple.toX || tuple.fromY == tuple.toY)
        .ToList();

    var diagonals = rawInput
        .Where(tuple => tuple.fromX != tuple.toX && tuple.fromY != tuple.toY)
        .ToList();

    var visited = new Dictionary<(int, int), int>();

    var valueTuples = diagonals.SelectMany(pair =>
    {
        var minX = Math.Min(pair.fromX, pair.toX);
        var maxX = Math.Max(pair.fromX, pair.toX);
        
        var minY = Math.Min(pair.fromY, pair.toY);
        var maxY = Math.Max(pair.fromY, pair.toY);

        var x = Enumerable.Range(minX, maxX - minX + 1)
            .OrderBy(i => i * ((pair.fromX - pair.toX) / Math.Abs(pair.fromX - pair.toX)))
            .ToList();

        var y = Enumerable.Range(minY, maxY - minY + 1)
            .OrderBy(i => i * ((pair.fromY - pair.toY) / Math.Abs(pair.fromY - pair.toY)))
            .ToList();

        return x.Zip(y, (a, b) => (a, b));
    });
    
    foreach (var tuple in valueTuples)
    {
        if (!visited.ContainsKey(tuple))
            visited.Add(tuple, 0);

        visited[tuple]++;
    }

    foreach (var pair in straightLines)
    {
        for (var k = pair.fromX; k <= pair.toX; k++)
        {
            for (var j = pair.fromY; j <= pair.toY; j++)
            {
                var point = (k, j);
                if (!visited.ContainsKey(point))
                    visited.Add(point, 0);

                visited[point]++;
            }
        }
    }

    Console.WriteLine(visited.Values.Count(i => i > 1));
}

(int, int) Swap(int a, int b)
{
    a ^= b;
    b ^= a;
    a ^= b;

    return (a, b);
}

(int, int) ParseInput(string s)
{
    var input = s.Split(",").Select(int.Parse);

    var x = input.First();
    var y = input.Last();

    return (x, y);
}
