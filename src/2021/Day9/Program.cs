using static System.Console;

var grid = File.ReadLines("input.txt")
    .Select(s => s.ToCharArray()
        .Select(c => c - '0')
        .ToList()
    )
    .ToList();

foreach (var line in grid)
{
    line.Add(9);
    line.Insert(0, 9);
}

grid.Add(Enumerable.Repeat(9, grid[0].Count).ToList());
grid.Insert(0, Enumerable.Repeat(9, grid[0].Count).ToList());

var lowestPoints = new List<(int, int)>();
for (var k = 1; k < grid.Count - 1; k++)
{
    for (var j = 1; j < grid[k].Count - 1; j++)
    {
        if (grid[k][j] < grid[k][j - 1]
            && grid[k][j] < grid[k][j + 1]
            && grid[k][j] < grid[k - 1][j]
            && grid[k][j] < grid[k + 1][j])
        {
            lowestPoints.Add((k, j));
        }
    }
}

TaskOne();
TaskTwo();

void TaskOne()
{
    var sum = lowestPoints.Select(tuple => grid[tuple.Item1][tuple.Item2] + 1).Sum();
    WriteLine(sum);
}

void TaskTwo()
{
    var score = lowestPoints.Select(GetBasin)
        .Select(basin => basin.Count)
        .OrderByDescending(x => x)
        .Take(3)
        .Aggregate(1, (i, i1) => i * i1);
    
    WriteLine(score);
}

List<(int, int)> GetBasin((int, int) startingPoint)
{
    var basin = new List<(int, int)>();
    var queue = new Queue<(int, int)>();
    var visited = new bool[grid.Count][];
    for (var k = 0; k < visited.Length; k++)
        visited[k] = new bool[grid[k].Count];

    queue.Enqueue(startingPoint);
    while (queue.TryDequeue(out var element))
    {
        if (visited[element.Item1][element.Item2])
            continue;
        
        basin.Add(element);
        visited[element.Item1][element.Item2] = true;
        var newPositions = new[]
        {
            (element.Item1 + 1, element.Item2),
            (element.Item1 - 1, element.Item2),
            (element.Item1, element.Item2 + 1),
            (element.Item1, element.Item2 - 1),
        };

        foreach (var newPosition in newPositions)
        {
            if (grid[newPosition.Item1][newPosition.Item2] != 9
                && !visited[newPosition.Item1][newPosition.Item2])
            {
                queue.Enqueue(newPosition);
            }
        }
    }

    return basin;
}