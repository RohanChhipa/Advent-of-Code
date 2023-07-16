using System.Diagnostics;

var stopwatch = new Stopwatch();
stopwatch.Start();

var lines = File.ReadAllLines("input.txt");

var taskOne = 0;
var taskTwo = 0;
foreach (var line in lines)
{
    var input = line.Split(",")
        .Select(s => s.Split("-")
            .Select(int.Parse)
            .ToArray())
        .ToArray();

    if (input[0][0] <= input[1][0] && input[0][1] >= input[1][1]
        || input[1][0] <= input[0][0] && input[1][1] >= input[0][1])
        taskOne++;

    if (input[0][0] <= input[1][0] && input[0][1] >= input[1][0]
        || input[1][0] <= input[0][0] && input[1][1] >= input[0][0])
        taskTwo++;
}

Console.WriteLine($"Task One: {taskOne} Task Two: {taskTwo} Time: {stopwatch.ElapsedMilliseconds}");