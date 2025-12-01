var grid = File.ReadLines("input.txt")
    .Select(x => x.ToCharArray())
    .ToArray();

var guard = grid.SelectMany((chars, x) => chars.Select((c, y) => (c, x, y)))
    .First(tuple => tuple.c == '^');

var rotateGuard = ((int, int) tuple) => (tuple.Item2, -1 * tuple.Item1);
var isInBounds = ((int x, int y) position) =>
    position.x >= 0 && position.x < grid.Length && position.y >= 0 && position.y < grid[0].Length;

Console.WriteLine($"Task One: {TaskOne()}");
Console.WriteLine($"Task Two: {TaskTwo()}");

int TaskOne()
{
    var guardDirection = (-1, 0);
    var guardPosition = (guard.x, guard.y);
    grid[guardPosition.x][guardPosition.y] = '.';

    var set = new HashSet<(int, int)>();
    while (isInBounds(guardPosition))
    {
        set.Add(guardPosition);
        var newPosition = (guardPosition.x + guardDirection.Item1, guardPosition.y + guardDirection.Item2);
        if (isInBounds(newPosition) && grid[newPosition.Item1][newPosition.Item2] != '.')
        {
            guardDirection = rotateGuard(guardDirection);
            newPosition = (guardPosition.x + guardDirection.Item1, guardPosition.y + guardDirection.Item2);
        }

        guardPosition = newPosition;
    }

    return set.Count;
}

int TaskTwo()
{
    var loops = 0;
    for (var k = 0; k < grid.Length; k++)
    {
        for (var j = 0; j < grid[k].Length; j++)
        {
            if (grid[k][j] != '.')
            {
                continue;
            }

            var guardDirection = (-1, 0);
            var guardPosition = (guard.x, guard.y);
            grid[guardPosition.x][guardPosition.y] = '.';

            grid[k][j] = '#';

            var obstacles = new Dictionary<(int, int, int, int), int>();
            var isLoopDetected = false;

            while (isInBounds(guardPosition))
            {
                var newPosition = (guardPosition.x + guardDirection.Item1, guardPosition.y + guardDirection.Item2);
                if (isInBounds(newPosition) && grid[newPosition.Item1][newPosition.Item2] != '.')
                {
                    var t = (guardPosition.x, guardPosition.y, guardDirection.Item1, guardDirection.Item2);
                    obstacles.TryAdd(t, 0);
                    if (obstacles[t] == 10)
                    {
                        isLoopDetected = true;
                        break;
                    }

                    obstacles[t]++;

                    guardDirection = rotateGuard(guardDirection);
                    newPosition = guardPosition;
                }

                guardPosition = newPosition;
            }

            if (isLoopDetected)
            {
                loops++;
            }

            grid[k][j] = '.';
        }
    }

    return loops;
}