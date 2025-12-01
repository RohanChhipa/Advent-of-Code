var lines = File.ReadAllLines("input.txt");

var antennas = lines.SelectMany((line, x) => line
    .Select((c, y) => (c, x, y))
    .Where(tuple => tuple.c != '.')
).ToList();

var list = antennas.GroupBy(tuple => tuple.c)
    .Select(
        group => group
            .SelectMany(_ => group, (tuple1, tuple2) => (tuple1, tuple2))
            .Where(tuple => tuple.tuple1 != tuple.tuple2)
            .ToList()
    ).ToList();

var taskOne = list.SelectMany(group => group
        .Select(item => (item.tuple1.x + (item.tuple1.x - item.tuple2.x),
            item.tuple1.y + (item.tuple1.y - item.tuple2.y)))
    )
    .Where(x => x.Item1 >= 0 && x.Item2 >= 0 && x.Item1 < lines.Length && x.Item2 < lines[0].Length)
    .Distinct()
    .ToList();

var taskTwo = list.SelectMany(group => group
        .SelectMany(item =>
        {
            var step = (item.tuple1.x - item.tuple2.x, item.tuple1.y - item.tuple2.y);
            var prev = (item.tuple1.x, item.tuple1.y);
            var nodes = new List<(int x, int y)>();

            while (prev.Item1 >= 0 && prev.Item1 < lines.Length && prev.Item2 >= 0 && prev.Item2 < lines[0].Length)
            {
                nodes.Add(prev);
                prev = (prev.Item1 + step.Item1, prev.Item2 + step.Item2);
            }

            return nodes;
        })
    )
    .Distinct()
    .ToList();

Console.WriteLine($"Task One: {taskOne.Count}");
Console.WriteLine($"Task Two: {taskTwo.Count}");