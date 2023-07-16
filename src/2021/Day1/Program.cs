using static System.Console;

var input = File.ReadLines("input.txt")
    .Select(int.Parse).ToList();

TaskOne();
TaskTwo();

void TaskOne()
{
    var count = Enumerable.Range(0, input.Count - 1)
        .Count(i => input[i + 1] > input[i]);

    WriteLine(count);
}

void TaskTwo()
{
    var groups = Enumerable.Range(1, input.Count - 2)
        .Select(i => input[i - 1] + input[i] + input[i + 1])
        .ToList();
    
    var count = Enumerable.Range(0, groups.Count - 1)
        .Count(i => groups[i + 1] > groups[i]);
    
    WriteLine(count);
}
