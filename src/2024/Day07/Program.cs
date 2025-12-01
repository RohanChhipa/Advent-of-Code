var lines = File.ReadAllLines("input.txt")
    .Select(x =>
    {
        var indexOf = x.IndexOf(": ");
        return (
            total: long.Parse(x[..indexOf]),
            components: x[(indexOf + 1)..].Trim().Split(" ").Select(long.Parse).ToList()
        );
    }).ToList();

var taskOne = lines
    .Where(tuple => Eval(tuple.total, tuple.components, 1, tuple.components.First()))
    .Sum(tuple => tuple.total);

var taskTwo = lines
    .Where(tuple => EvalTwo(tuple.total, tuple.components, 1, tuple.components.First()))
    .Sum(tuple => tuple.total);

Console.WriteLine(taskOne);
Console.WriteLine(taskTwo);

bool Eval(long total, List<long> components, int currentComponent, long currentTotal)
{
    if (currentComponent >= components.Count)
    {
        return total == currentTotal;
    }

    if (currentTotal > total)
    {
        return false;
    }

    return Eval(total, components, currentComponent + 1, currentTotal + components[currentComponent]) ||
           Eval(total, components, currentComponent + 1, currentTotal * components[currentComponent]);
}

bool EvalTwo(long total, List<long> components, int currentComponent, long currentTotal)
{
    if (currentComponent >= components.Count)
    {
        return total == currentTotal;
    }

    if (currentTotal > total)
    {
        return false;
    }

    return EvalTwo(total, components, currentComponent + 1, currentTotal + components[currentComponent]) ||
           EvalTwo(total, components, currentComponent + 1, currentTotal * components[currentComponent]) ||
           EvalTwo(total, components, currentComponent + 1,
               long.Parse($"{currentTotal}{components[currentComponent]}"));
}