var lines = File.ReadAllLines("input.txt")
    .Where(s => !string.IsNullOrWhiteSpace(s))
    .ToList();

var seeds = lines.First()
    .Replace("seeds: ", "")
    .Split(" ")
    .Select(long.Parse)
    .ToList();

lines.RemoveAt(0);

var maps = new List<List<(long, long, long)>>();
foreach (var line in lines)
{
    if (line.Contains("map"))
    {
        maps.Add(new List<(long, long, long)>());
    }
    else
    {
        var array = line.Split(" ").Select(long.Parse).ToArray();
        maps.Last().Add((array[0], array[1], array[2]));
    }
}

Console.WriteLine($"Task One: {getLocations(seeds).Min()}");

// var locations = new List<long>();
// for (var k = 0; k < seeds.Count; k += 2)
// {
//     for (var j = seeds[k]; j <= seeds[k] + seeds[k + 1]; j++)
//     {
//         Console.WriteLine(j);
//         getLocation(j);
//     }
//     
//     Console.WriteLine(k);
//
//     // count += seeds[k + 1];
//     // var seed = Enumerable.Range(0, (int)seeds[k + 1]).Select((l, i) => seeds[k] + i);
//     // seed.ForEach(l => dic.Add(l, 0));
//
//     // locations.AddRange(getLocations(seed.Select((l, i) => seeds[k] + i).ToList()));
//     // Console.WriteLine(k);
// }

// Console.WriteLine($"Task Two: {locations.Min()}");
// Console.WriteLine($"Task Two: {getReverseLocation(46, maps)}");

var tasktwo = 0L;
var range = Enumerable.Range(0, seeds.Count / 2).Select(x => x * 2).ToList();
while (true)
{
    var reverseLocation = getReverseLocation(tasktwo, maps);
    var any = range.Any(k => reverseLocation >= seeds[k] && reverseLocation <= seeds[k] + seeds[k + 1]);
    
    if (tasktwo % 1000000 == 0)
        Console.WriteLine(tasktwo);
    
    if (any)
    {
        Console.WriteLine(tasktwo);
        Console.WriteLine(reverseLocation);
        Console.WriteLine(string.Join(", ", getLocations(new List<long>() {reverseLocation})));
        break;
    }

    tasktwo++;
}

List<long> getLocations(List<long> seeds)
{
    var locations = new List<long>();
    foreach (var seed in seeds)
    {
        var value = seed;
        foreach (var map in maps)
        {
            var range = map.FirstOrDefault(tuple => value >= tuple.Item2 && value <= tuple.Item2 + tuple.Item3);
            if (range != default)
            {
                value = range.Item1 + (value - range.Item2);
            }
        }

        locations.Add(value);
    }

    return locations;
}

long getReverseLocation(long seed, List<List<(long, long, long)>> currentMap)
{
    var value = seed;
    
    for (var k = maps.Count - 1; k >= 0; k--)
    {
        var map = maps[k];
        var range = map.FirstOrDefault(tuple => value >= tuple.Item1 && value <= tuple.Item1 + tuple.Item3);
        if (range != default)
        {
            value = range.Item2 + (value - range.Item1);
        }
    }

    return value;
}








