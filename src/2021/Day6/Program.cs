using System.Numerics;
using Microsoft.VisualBasic;

var input = File.ReadAllLines("input.txt")
    .First()
    .Split(",")
    .Select(int.Parse)
    .ToList();

var days = new BigInteger[9];
foreach (var k in input)
    days[k]++;

for (var t = 0; t < 256; t++)
{
    var next = new BigInteger[9];
    for (var k = 1; k < days.Length; k++)
        next[k - 1] = days[k];

    if (days[0] > 0)
    {
        next[6] += days[0];
        next[8] += days[0];
    }

    days = next;
}

// Console.WriteLine(string.Join(", ", days));
Console.WriteLine(days.Aggregate((integer, bigInteger) => integer + bigInteger));
