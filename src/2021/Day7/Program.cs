var submarines = File.ReadLines("input.txt")
    .First()
    .Split(",")
    .Select(int.Parse)
    .ToArray();

var max = submarines.Max();

TaskOne();
TaskTwo();

void TaskOne()
{
    var positions = new int[max + 1];
    foreach (var submarine in submarines)
        for (var k = 0; k < positions.Length; k++)
            positions[k] += Math.Abs(submarine - k);

    Console.WriteLine(positions.Min());
}

void TaskTwo()
{
    var positions = new int[max + 1];
    foreach (var submarine in submarines)
    {
        for (var k = 0; k < positions.Length; k++)
        {
            var n = Math.Abs(submarine - k);
            positions[k] += (n * (n+1)) / 2;
        }
    }
    
    Console.WriteLine(positions.Min());
}
