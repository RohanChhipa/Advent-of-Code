using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var line = File.ReadAllText("input.txt");
var find = (int markerLength) => Enumerable.Range(0, line.Length - markerLength - 1)
    .Select(i =>
    {
        var markerFound = line.Substring(i, markerLength)
            .GroupBy(c => c)
            .All(chars => chars.Count() == 1);

        return (markerFound, i + markerLength);
    })
    .First(tuple => tuple.markerFound);

Console.WriteLine($"Task One: {find(4).Item2}");
Console.WriteLine($"Task Two: {find(14).Item2}");
Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");