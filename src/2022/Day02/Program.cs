var scores = new Dictionary<int, int>
{
    {0, 3},
    {1, 6},
    {2, 0},
    {-1, 0},
    {-2, 6}
};

var input = File.ReadAllLines("input.txt")
    .Select(line => line
        .Replace(" ", ""))
    .ToList();

TaskOne();
TaskTwo();

void TaskOne()
{


    var total = input
        .Select(line => line
            .Replace("X", "A")
            .Replace("Y", "B")
            .Replace("Z", "C"))
        .Select(line => scores[line.Last() - line.First()] + (line.Last() - 'A' + 1))
        .Sum();

    Console.WriteLine($"{total}");
}

void TaskTwo()
{
    var resultMap = new Dictionary<char, int>
    {
        {'X', -1},
        {'Y', 0},
        {'Z', 1}
    };

    var total = input
        .Select(line =>
        {
            var goal = line.First() + resultMap[line.Last()];
            goal = goal switch
            {
                < 'A' => 'C',
                > 'C' => 'A',
                _ => goal
            };

            return scores[goal - line.First()] + (goal - 'A' + 1);
        })
        .Sum();

    Console.WriteLine($"{total}");
}