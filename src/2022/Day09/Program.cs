using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var lines = File.ReadAllLines("input.txt")
    .SelectMany(line =>
    {
        var s = line.Split(" ");
        var x = s[0] switch
        {
            "R" => (1, 0),
            "L" => (-1, 0),
            "U" => (0, 1),
            "D" => (0, -1),
            _ => (0, 0)
        };
        
        return Enumerable.Repeat(x, int.Parse(s[1]));
    });

var isTouching = ((int, int) head, (int, int) tail) => 
    Math.Abs(head.Item1 - tail.Item1) <= 1 && Math.Abs(head.Item2 - tail.Item2) <= 1;

var safeDiv = (int a, int b) => b == 0 ? 0 : a / b;

    var knots = new (int, int)[10];
var knotLocations = new HashSet<(int, int)>[knots.Length];

foreach (var line in lines)
{
    var prev = knots[0];
    knots[0] = (knots[0].Item1 + line.Item1, knots[0].Item2 + line.Item2);

    for (var k = 1; k < knots.Length; k++)
    {
        if (!isTouching(knots[k - 1], knots[k]))
        {
            var i = (knots[k - 1].Item1 - knots[k].Item1, knots[k - 1].Item2 - knots[k].Item2);
            i = (safeDiv(i.Item1, Math.Abs(i.Item1)), safeDiv(i.Item2, Math.Abs(i.Item2)));
            
            knots[k] = (knots[k].Item1 + i.Item1, knots[k].Item2 + i.Item2);
        }
    }
    
    for (var k = 0; k < knots.Length; k++)
    {
        if (knotLocations[k] is null)
            knotLocations[k] = new HashSet<(int, int)>();

        knotLocations[k].Add(knots[k]);
    }
}

Console.WriteLine(knotLocations[1].Count);
Console.WriteLine(knotLocations.Last().Count);
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");