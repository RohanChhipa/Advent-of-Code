var readLines = File.ReadLines("input.txt").ToList();

Console.WriteLine($"Part One: {TaskOne(readLines)}");
Console.WriteLine($"Part One: {TaskTwo(readLines)}");

return;

int TaskOne(List<string> input)
{
    var startingPoint = 50;
    var passwordCount = 0;
    foreach (var valueTuple in input.Select(x => (x.Substring(0, 1), int.Parse(x.Substring(1)))))
    {
        var value = valueTuple.Item2 * (valueTuple.Item1 == "L" ? -1 : 1);
        startingPoint += value;

        if (startingPoint >= 100)
            startingPoint %= 100;
        else if (startingPoint < 0)
            startingPoint += (int)Math.Ceiling(Math.Abs(startingPoint / 100.0)) * 100;

        if (startingPoint == 0)
            passwordCount++;
    }

    return passwordCount;
}

int TaskTwo(List<string> input)
{
    var startingPoint = 50;
    var passwordCount = 0;
    foreach (var valueTuple in input.Select(x => (x.Substring(0, 1), int.Parse(x.Substring(1)))))
    {
        for (var i = 1; i <= valueTuple.Item2; i++)
        {
            startingPoint += valueTuple.Item1 == "L" ? -1 : 1;
            if (startingPoint == 0 || startingPoint % 100 == 0)
                passwordCount++;
        }

        if (startingPoint >= 100)
        {
            startingPoint %= 100;
        }
        else if (startingPoint < 0)
        {
            startingPoint += (int)Math.Ceiling(Math.Abs(startingPoint / 100.0)) * 100;
        }
    }

    return passwordCount;
}