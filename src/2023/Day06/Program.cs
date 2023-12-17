var lines = File.ReadLines("input.txt");

var time = lines.First()
    .Replace("Time:", "")
    .Trim()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToList();

var distance = lines.Last()
    .Replace("Distance:", "")
    .Trim()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToList();

Console.WriteLine(string.Join(", ", time));
Console.WriteLine(string.Join(", ", distance));

var taskOne = Enumerable.Range(0, time.Count)
    .Select(i => Enumerable.Range(0, time[i]).Count(i1 => (time[i] - i1) *  i1 > distance[i]))
    .Aggregate(1, (i, i1) => i * i1);

Console.WriteLine($"Task one: {taskOne}");


var singleTime = long.Parse(lines.First()
    .Replace("Time:", "")
    .Replace(" ", "")
    .Trim());

var singleDistance = long.Parse(lines.Last()
    .Replace("Distance:", "")
    .Replace(" ", "")
    .Trim());
    
// Console.WriteLine(singleTime);
// Console.WriteLine(singleDistance);

var taskTwo = 0L;
for (var k = 0L; k < singleTime; k++)
    if ((singleTime - k) * k > singleDistance)
        taskTwo++;

Console.WriteLine($"Task two: {taskTwo}");