using System.Diagnostics;

var readLines = File.ReadLines("input.txt").ToList();

var stopwatch = Stopwatch.StartNew();
Console.WriteLine($"Part One: {TaskOne(readLines)}");
Console.WriteLine($"Part Two: {TaskTwo(readLines)}");
Console.WriteLine($"Elapsed MS: {stopwatch.ElapsedMilliseconds}");

return;

int TaskOne(List<string> input)
{
    // Implement Part One logic here
    return 0;
}

int TaskTwo(List<string> input)
{
    // Implement Part Two logic here
    return 0;
}
