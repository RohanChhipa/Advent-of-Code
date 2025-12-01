var lines = File.ReadAllLines("input.txt")
    .Select(x => x.ToCharArray()
        .Select(c => c - '0')
        .ToArray())
    .ToArray();

var starting = lines.SelectMany((chars, x) => chars.Select((c, y) => (c, x, y)))
    .Where(tuple => tuple.c == 0)
    .Select(tuple => (tuple.x, tuple.y))
    .ToList();

var taskOne = starting.Sum(x =>
{
    var pos = new List<(int, int)>();
    Traverse(x, 0, pos, true);
    return pos.Distinct().Count();
});

var taskTwo = starting.Sum(x => Traverse(x, 0, new List<(int, int)>(), true));

Console.WriteLine($"Task One: {taskOne}");
Console.WriteLine($"Task One: {taskTwo}");

int Traverse((int x, int y) currentPosition, int prevHeight, List<(int, int)> pos, bool init = false)
{
    if (currentPosition.x < 0
        || currentPosition.y < 0
        || currentPosition.x >= lines.Length
        || currentPosition.y >= lines[0].Length
       )
    {
        return 0;
    }

    if (lines[currentPosition.x][currentPosition.y] - prevHeight != 1 && !init)
    {
        return 0;
    }

    if (lines[currentPosition.x][currentPosition.y] == 9)
    {
        pos.Add(currentPosition);
        return 1;
    }

    return Traverse((currentPosition.x + 1, currentPosition.y), lines[currentPosition.x][currentPosition.y], pos)
           + Traverse((currentPosition.x - 1, currentPosition.y), lines[currentPosition.x][currentPosition.y], pos)
           + Traverse((currentPosition.x, currentPosition.y + 1), lines[currentPosition.x][currentPosition.y], pos)
           + Traverse((currentPosition.x, currentPosition.y - 1), lines[currentPosition.x][currentPosition.y], pos);
}