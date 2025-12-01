var readLines = File.ReadLines("input.txt");
var firstList = readLines.Select(x => long.Parse(x.Split("   ")[0])).Order().ToList();
var secondList = readLines.Select(x => long.Parse(x.Split("   ")[1])).Order().ToList();

Console.WriteLine($"Task One: {TaskOne()}");
Console.WriteLine($"Task Two: {TaskTwo()}");

return;

long TaskOne()
{
    return firstList.Select((l, i) => Math.Abs(firstList[i] - secondList[i])).Sum();
}

long TaskTwo()
{
    return firstList.Select(l => l * secondList.Count(l1 => l1 == l)).Sum();
}