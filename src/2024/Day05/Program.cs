var lines = File.ReadLines("input.txt").ToList();

var rules = lines.Where(x => x.Contains('|'))
    .Select(x => x.Split("|"))
    .Select(x => new[] { int.Parse(x[0]), int.Parse(x[1]) })
    .ToList();

var manuals = lines.Where(x => x.Contains(','))
    .Select(x => x.Split(",")
        .Select(int.Parse).ToList()
    ).ToList();

Console.WriteLine($"Task One: {TaskOne()}");
Console.WriteLine($"Task Two: {TaskTwo()}");


int TaskOne()
{
    return manuals.Sum(manual =>
    {
        return rules.Where(x => x.All(manual.Contains))
            .Any(x => manual.IndexOf(x.First()) >= manual.IndexOf(x.Last()))
            ? 0
            : manual[manual.Count / 2];
    });
}

int TaskTwo()
{
    return manuals.Sum(manual =>
    {
        var applicableRules = rules.Where(x => x.All(manual.Contains)).ToList();

        var violatedRule = applicableRules.FirstOrDefault(x => manual.IndexOf(x.First()) >= manual.IndexOf(x.Last()));
        if (violatedRule != null)
        {
            while (violatedRule != null)
            {
                manual.RemoveAt(manual.IndexOf(violatedRule.First()));
                manual.Insert(manual.IndexOf(violatedRule.Last()), violatedRule.First());

                violatedRule =
                    applicableRules.FirstOrDefault(x => manual.IndexOf(x.First()) >= manual.IndexOf(x.Last()));
            }

            return manual[manual.Count / 2];
        }

        return 0;
    });
}