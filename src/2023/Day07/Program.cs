using System.Text;

var cardScores = new Dictionary<char, int>
{
    {'J', 1},
    {'2', 2},
    {'3', 3},
    {'4', 4},
    {'5', 5},
    {'6', 6},
    {'7', 7},
    {'8', 8},
    {'9', 9},
    {'T', 10},
    // {'J', 11},
    {'Q', 12},
    {'K', 13},
    {'A', 14}
};

var strategies = new List<Func<string, int>>()
{
    s => s.GroupBy(c => c).Any(chars => chars.Count() == 5) ? 7 : 0,
    s => s.GroupBy(c => c).Any(chars => chars.Count() == 4) ? 6 : 0,
    s =>
    {
        var groupBy = s.GroupBy(c => c);
        return groupBy.Any(chars => chars.Count() == 3)
               && groupBy.Any(chars => chars.Count() == 2) ? 5 : 0;
    },
    s => 
    {
        var groupBy = s.GroupBy(c => c);
        return groupBy.Any(chars => chars.Count() == 3)
               && groupBy.Any(chars => chars.Count() == 1) ? 4 : 0;
    },
    s => 
    {
        var groupBy = s.GroupBy(c => c);
        return groupBy.Count(chars => chars.Count() == 2) == 2
               && groupBy.Any(chars => chars.Count() == 1) ? 3 : 0;
    },
    s => s.GroupBy(c => c).Count(chars => chars.Count() == 2) == 1 ? 2 : 0,
    s => s.GroupBy(c => c).All(chars => chars.Count() == 1) ? 1 : 0
};

Console.WriteLine(strategies.First(func => func("23432") != 0).Invoke("23432"));

var lines = File.ReadLines("input.txt")
    .Select(s =>
    {
        var strings = s.Split(" ");
        return (strings.First(), long.Parse(strings.Last()));
    })
    .OrderByDescending(tuple => strategies.First(func => func(tuple.Item1) != 0).Invoke(tuple.Item1))
    .ThenByDescending(tuple => cardScores[tuple.Item1[0]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[1]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[2]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[3]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[4]])
    .ToList();

var taskOne = Enumerable.Range(0, lines.Count)
    .Sum(i => ((lines.Count) - i) * lines[i].Item2);

Console.WriteLine($"Task One: {taskOne}");

long getScore(string s)
{

    return strategies.First(func => func(s) != 0).Invoke(s);
}

var lines2 = File.ReadLines("input.txt")
    .Select(s =>
    {
        var strings = s.Split(" ");
        return (strings.First(), long.Parse(strings.Last()));
    })
    .OrderByDescending(tuple =>
    {
        Console.WriteLine(tuple.Item1);
        if (tuple.Item1.Contains("J"))
        {
            var stringBuilder = new StringBuilder(tuple.Item1);
            var score = int.MinValue;
            for (var a = 1; a < cardScores.Count; a++)
            {
                if (stringBuilder[0] == 'J') 
                    stringBuilder[0] = cardScores.Keys.ElementAt(a);

                for (var b = 1; b < cardScores.Count; b++)
                {
                    
                    if (tuple.Item1[1] == 'J') 
                        stringBuilder[1] = cardScores.Keys.ElementAt(b);

                    for (var c = 1; c < cardScores.Count; c++)
                    {
                        
                        if (stringBuilder[2] == 'J') 
                            stringBuilder[2] = cardScores.Keys.ElementAt(c);

                        for (var d = 1; d < cardScores.Count; d++)
                        {
                            
                            if (stringBuilder[3] == 'J') 
                                stringBuilder[3] = cardScores.Keys.ElementAt(d);

                            for (var e = 1; e < cardScores.Count; e++)
                            {
                                if (stringBuilder[4] == 'J') 
                                    stringBuilder[4] = cardScores.Keys.ElementAt(e);

                                score = Math.Max(score,
                                    strategies.First(func => func(stringBuilder.ToString()) != 0)
                                        .Invoke(stringBuilder.ToString())
                                    );
                                
                                if (tuple.Item1[4] == 'J')
                                    stringBuilder[4] = 'J';
                            }
                            
                            if (tuple.Item1[3] == 'J')
                                stringBuilder[3] = 'J';
                        }
                        
                        if (tuple.Item1[2] == 'J')
                            stringBuilder[2] = 'J';
                    }
                    
                    if (tuple.Item1[1] == 'J')
                        stringBuilder[1] = 'J';
                }

                if (tuple.Item1[0] == 'J')
                    stringBuilder[0] = 'J';
            }

            return score;
        }

        return strategies.First(func => func(tuple.Item1) != 0).Invoke(tuple.Item1);
    })
    .ThenByDescending(tuple => cardScores[tuple.Item1[0]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[1]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[2]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[3]])
    .ThenByDescending(tuple => cardScores[tuple.Item1[4]])
    .ToList();

var taskTwo = Enumerable.Range(0, lines2.Count)
    .Sum(i => ((lines2.Count) - i) * lines2[i].Item2);

// Console.WriteLine(string.Join(", ", lines2));
Console.WriteLine($"Task Two: {taskTwo}");