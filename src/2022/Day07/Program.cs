using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var lines = File.ReadAllLines("input.txt");

var dir = new Stack<string>();
var dirStructure = new Dictionary<string, List<string>>();

foreach (var line in lines)
{
    var input = line;

    if (input.StartsWith("$ cd .."))
    {
        dir.Pop();
    }
    else
    if (input.StartsWith("$ cd "))
    {
        input = input.Replace("$ cd ", "");


        var path = dir.Any() ? $"{dir.Peek()}/{input}" : input;
        dir.Push(path);

        if (!dirStructure.ContainsKey(path))
        {
            dirStructure.Add(path, new List<string>());
        }
    }
    else
    if (!input.StartsWith("$ "))
    {
        dirStructure[dir.Peek()].Add(input);
    }
}

var scores = new Dictionary<string, int>();
Recur("/", dirStructure["/"], scores);

Console.WriteLine(scores.Where(pair => pair.Value <=100000).Sum(pair => pair.Value));

var left = 70000000 - scores["/"];
Console.WriteLine(scores.Values.Where(i => left + i >= 30000000).OrderBy(i => i).First());

Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

int Recur(string key, List<string> value, Dictionary<string, int> scores)
{
    if (scores.ContainsKey(key))
        return scores[key];

    var children = value.Select(s => s.Split(" "));
    var dirs = children
        .Where(strings => strings[0].StartsWith("dir"))
        .Select(strings => $"{key}/{strings[1]}");

    var sum = children
        .Where(strings => !strings[0].StartsWith("dir"))
        .Select(strings => int.Parse(strings[0]))
        .Sum();

    foreach (var dir in dirs)
    {
        sum += Recur(dir, dirStructure[dir], scores);
    }
    
    scores.Add(key, sum);

    return sum;
}