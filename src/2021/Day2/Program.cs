using static System.Console;

var input = File.ReadLines("input.txt")
    .Select(s => s.Split(" "))
    .ToList();

TaskOne();
TaskTwo();

void TaskOne()
{
    var forward = input.Where(strings => strings[0] == "forward")
        .Sum(strings => int.Parse(strings[1]));

    var up = input.Where(strings => strings[0] == "up").Sum(strings => int.Parse(strings[1]));
    var down = input.Where(strings => strings[0] == "down").Sum(strings => int.Parse(strings[1]));

    var depth = down - up;

    WriteLine(forward * depth);
}

void TaskTwo()
{
    var aim = 0;
    var horizontal = 0;
    var depth = 0;

    foreach (var line in input)
    {
        var value = int.Parse(line[1]);
        switch (line[0])
        {
            case "forward":
            {
                horizontal += value;
                depth += aim * value;
                break;
            }
            case "down":
                aim += value;
                break;
            case "up":
                aim -= value;
                break;
        }
    }

    WriteLine(horizontal * depth);
}
