var lines = File.ReadAllLines("input.txt")
    .Select(line =>
    {
        var s = line.Split(": ");
        var card = int.Parse(s.First().Replace("Card ", ""));
        var numbers = s.Last().Split(" | ");
        var winningNumbers = numbers.First()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
        
        var currentNumbers = numbers.Last()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        return (card, winningNumbers, currentNumbers);
    }).ToList();

var taskOne = lines.Sum(tuple =>
{
    var count = tuple.currentNumbers.Intersect(tuple.winningNumbers).Count();
    return count == 0 ? count : Math.Pow(2, count - 1);
});

Console.WriteLine($"Task One: {taskOne}");

var cards = Enumerable.Repeat(1, lines.Count + 1).ToArray();
foreach (var line in lines)
{
    var count = line.currentNumbers.Intersect(line.winningNumbers).Count();
    for (var k = line.card + 1; k <= line.card + count; k++)
        cards[k] += cards[line.card];
}    

Console.WriteLine($"Task Two: {cards.Skip(1).Sum()}");