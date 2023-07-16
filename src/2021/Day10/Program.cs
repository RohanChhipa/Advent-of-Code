using System.Text;
using static System.Console;

var input = File.ReadAllLines("./input.txt")
    .Select(s => s.Trim().ToCharArray())
    .ToList();

var opening = new HashSet<char> {'(', '[', '{', '<'};
var closing = new HashSet<char> {')', ']', '}', '>'};

var pairs = new Dictionary<char, char>
{
    {'(', ')'},
    {'[', ']'},
    {'{', '}'},
    {'<', '>'},
};

var taskOneScores = new Dictionary<char, int>()
{
    {')', 3},
    {']', 57},
    {'}', 1197},
    {'>', 25137},
};

var taskTwoScores = new Dictionary<char, int>()
{
    {')', 1},
    {']', 2},
    {'}', 3},
    {'>', 4},
};

var corruptedChars = new List<char>();
var completedLines = new List<string>();

foreach (var line in input)
{
    var openingChars = new Stack<char>();
    var hasCorruptedChars = false;
    
    foreach (var character in line)
    {
        if (opening.Contains(character))
            openingChars.Push(character);
        else
        {
            var pop = openingChars.Pop();
            if (pairs[pop] != character)
            {
                hasCorruptedChars = true;
                corruptedChars.Add(character);
            }
        }
    }
    
    if (!hasCorruptedChars)
    {
        var builder = new StringBuilder();
        while (openingChars.Any())
            builder.Append(pairs[openingChars.Pop()]);
            
        if (!string.IsNullOrEmpty(builder.ToString()))
            completedLines.Add(builder.ToString());
    }
}

WriteLine($"Task one: {corruptedChars.Sum(c => taskOneScores[c])}");

var scores = completedLines.Select(s => s.ToCharArray())
    .Select(chars => chars.Aggregate(0L, (i, c) => i * 5L + taskTwoScores[c]))
    .OrderBy(i => i)
    .ToList();

WriteLine($"Task two: {scores[scores.Count / 2]}");
