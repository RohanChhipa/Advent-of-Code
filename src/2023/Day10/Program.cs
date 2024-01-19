var lines = File.ReadAllLines("input.txt")
    .Select(s => s.ToCharArray())
    .ToArray();

var starting = lines.SelectMany((chars, i) => chars.Select((c, i1) => (c, i, i1)))
    .First(tuple => tuple.c == 'S');

var dictionary = new Dictionary<Point, List<Point>>();
var paths = new Dictionary<char, Func<Point, List<Point>>>
{
    {'-', point => new List<Point> { point with { col = point.col + 1 }, point with { col = point.col - 1 } }},
    {'|', point => new List<Point> { point with { row = point.row + 1 }, point with { row = point.row - 1 } }},
    {'L', point => new List<Point> { point with { row = point.row - 1 }, point with { col = point.col + 1 } }},
    {'J', point => new List<Point> { point with { row = point.row - 1 }, point with { col = point.col - 1 } }},
    {'7', point => new List<Point> { point with { row = point.row + 1 }, point with { col = point.col - 1 } }},
    {'F', point => new List<Point> { point with { row = point.row + 1 }, point with { col = point.col + 1 } }},
    {'S', point => new List<Point> { point with { row = point.row + 1 } }},
};

var stack = new Stack<Point>(new []{ new Point(starting.i + 1, starting.i1) });
var prev = new Point(starting.i, starting.i1);
while (stack.Count != 0)
{
    var point = stack.Pop();
    var points = paths[lines[point.row][point.col]]
        .Invoke(point)
        .Where(next => next != prev)
        .ToList();
    
    dictionary.Add(point, points);

    if (point != new Point(starting.i, starting.i1))
    {
        stack.Push(points.First());
    }

    prev = point;
}

Console.WriteLine($"Task One: {dictionary.Count / 2}");

var maxRow = dictionary.Keys.Max(point => point.row);
var minRow = dictionary.Keys.Min(point => point.row);
var maxCol = dictionary.Keys.Max(point => point.col);
var minCol = dictionary.Keys.Min(point => point.col);

Console.WriteLine($"({minRow} {minCol}) ({maxRow} {maxCol})");

var count = 0;
for (var k = minRow; k <= maxRow; k++)
{
    for (int j = minCol; j <= maxCol; j++)
    {
        var vert = Enumerable.Range(k, lines.Length - k).Count(i => lines[i][j] != '.');
        var hor = Enumerable.Range(j, lines[k].Length - j).Count(i => lines[k][i] != '.');

        count += vert % 2 == 0 && hor % 2 == 0 ? 0 : 1;
    }
}

Console.WriteLine($"Task Two: {count}");

record Point(int row, int col);