var text = File.ReadAllText("input.txt");

var list = new List<int>();
for (var k = 0; k < text.Length; k++)
{
    list.AddRange(Enumerable.Repeat(k % 2 == 0 ? k / 2 : -1, text[k] - '0'));
}

var lastElement = list.Count - 1;
for (var k = 0; k < list.Count && lastElement > k; k++)
{
    if (list[k] == -1)
    {
        list[k] = list[lastElement];
        list[lastElement] = -1;

        lastElement = list.LastIndexOf(list.Last(x => x != -1));
    }
}

var taskOne = Enumerable.Range(0, list.Count).Sum(i => (long)i * Math.Max(list[i], 0));

var valueTuples = text.Select((c, i) => (blocks: c - '0', id: i % 2 == 0 ? i / 2 : -1))
    .ToList();

var newList = new List<(int, int)>();
for (var k = 0; k < valueTuples.Count; k++)
{
    if (valueTuples[k].id < 0 && valueTuples[k].blocks > 0)
    {
        var lastEl = valueTuples.Last(x => x.id > 0 && x.blocks <= valueTuples[k].blocks);
        var indexOf = valueTuples.IndexOf(lastEl);
        var remainingSpace = valueTuples[k].blocks - lastEl.blocks;

        if (indexOf < k)
            continue;

        valueTuples[indexOf] = (lastEl.blocks, -1);
        valueTuples[k] = lastEl;

        if (remainingSpace > 0)
        {
            valueTuples.Insert(k + 1, (remainingSpace, -1));
        }
    }
}

var taskTwo = 0L;
var offset = 0L;
for (var k = 0; k < valueTuples.Count; k++)
{
    taskTwo += Enumerable.Range(0, valueTuples[k].blocks).Sum(i => (i + offset) * Math.Max(valueTuples[k].id, 0));
    offset += valueTuples[k].blocks;
}

Console.WriteLine($"Task One: {taskOne}");
Console.WriteLine($"Task Two: {taskTwo}");