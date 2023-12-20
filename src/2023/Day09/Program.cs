var lines = File.ReadAllLines("input.txt")
    .Select(s => s.Split(" "))
    .Select(s => s.Select(int.Parse).ToList())
    .ToList();

var taskOne = 0;
var taskTwo = 0;
foreach (var line in lines)
{
    var list = line;
    var stackNext = new Stack<int>();
    var stackPrev = new Stack<int>();
    
    while (list.Any(i => i != 0))
    {
        stackNext.Push(list.Last());
        stackPrev.Push(list.First());
        
        var tmp = new List<int>();
        for (var k = 1; k < list.Count; k++)
            tmp.Add(list[k] - list[k-1]);
                
        list = tmp;
    }

    var next = 0;
    while (stackNext.Count > 0)
        next += stackNext.Pop();

    var prev = 0;
    while (stackPrev.Count > 0)
        prev = stackPrev.Pop() - prev;

    taskOne += next;
    taskTwo += prev;
}

Console.WriteLine($"Task One: {taskOne}");
Console.WriteLine($"Task Two: {taskTwo}");