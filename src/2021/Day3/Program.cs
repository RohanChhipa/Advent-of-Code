using System.Text;
using static System.Console;

var input = File.ReadLines("input.txt")
    .Select(s => s.ToCharArray())
    .ToArray();

var columnExtract = (int k, char[][] matrix) => Enumerable.Range(0, matrix.Length)
    .Select(i => matrix[i][k])
    .ToArray();

TaskOne();
TaskTwo();

void TaskOne()
{
    var groups = Enumerable.Range(0, input[0].Length)
        .Select(i => columnExtract(i, input))
        .Select(strings => strings.GroupBy(s => s))
        .ToList();

    var gammaBitstring = string.Join("", groups.Select(enumerable => enumerable
        .OrderByDescending(grouping => grouping.Count())
        .First().Key));

    var gamma = Convert.ToInt32(gammaBitstring, 2);

    var epsilonBitstring = string.Join("", groups.Select(enumerable => enumerable
        .OrderBy(grouping => grouping.Count())
        .First().Key));

    var epsilon = Convert.ToInt32(epsilonBitstring, 2);

    WriteLine(gamma * epsilon);
}

void TaskTwo()
{
    var oxygenBits = GetRating(false);
    var co2Bits = GetRating(true);

    var oxygen = Convert.ToInt16(oxygenBits, 2);
    var co2 = Convert.ToInt16(co2Bits, 2);

    WriteLine(oxygen * co2);
}

string GetRating(bool asc)
{
    var clone = input.ToList();

    var idx = 0;
    while (clone.Count() > 1)
    {
        var keys = columnExtract(idx, clone.ToArray())
            .GroupBy(c => c)
            .OrderBy(chars => (asc ? 1 : -1) * chars.Count());

        var key = keys
            .First()
            .Key;

        var equal = keys.Select(chars => chars.Count()).Distinct().Count() == 1;
        if (equal)
        {
            if (asc)
                key = '0';
            else
                key = '1';
        }

        clone = clone.Where(chars => chars[idx] == key).ToList();

        idx++;
    }

    return string.Join("", clone.First());
}
