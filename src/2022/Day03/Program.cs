var lines = File.ReadAllLines("input.txt");

var taskOne = lines.Select(s => (s.Substring(0, s.Length / 2), s.Substring(s.Length / 2)))
    .Select(t => t.Item1
        .Intersect(t.Item2)
        .First())
    .Select(c => char.ToLower(c) - 'a' + (char.IsUpper(c) ? 26 : 0) + 1)
    .Sum();

var taskTwo = Enumerable.Range(0, lines.Length / 3)
    .Select(i =>
    {
        var range = (i * 3, i * 3 + 3);
        return lines[range.Item1..range.Item2].SelectMany(s => s.Distinct());
    })
    .Select(chars => chars.GroupBy(c => c)
        .Where(grouping => grouping.Count() == 3)
        .Select(grouping => grouping.Key))
    .Select(chars => chars.First())
    .Select(c => char.ToLower(c) - 'a' + (char.IsUpper(c) ? 26 : 0) + 1)
    .Sum();

Console.WriteLine($"{taskOne}, {taskTwo}");