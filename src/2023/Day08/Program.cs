var lines = File.ReadAllLines("input.txt");

var instructions = lines[0];

var graph = lines.Skip(2).Select(s => 
    s.Replace(" = (", ", ")
        .Replace(")", "")
        .Split(", ")
).ToDictionary(strings => strings.First(), strings => strings.Skip(1).Take(2).ToArray());

// TaskOne();
TaskTwo();

void TaskOne()
{
    var curr = "AAA";
    var k = 0;
    var taskOne = 0;
    while (curr != "ZZZ")
    {
        curr = instructions[k] switch
        {
            'L' => graph[curr][0],
            'R' => graph[curr][1],
            _ => curr
        };
    
        k = (k + 1) % instructions.Length;
        taskOne++;
    }

    Console.WriteLine($"Task One: {taskOne}");
}

void TaskTwo()
{
    var curr = graph.Keys.Where(s => s[2] == 'A').ToArray();
    var scores = new List<long>();
    
    for (var i = 0; i < curr.Length; i++)
    {
        var k = 0;
        var taskOne = 0;
        while (curr[i][2] != 'Z')
        {
            curr[i] = instructions[k] switch
            {
                'L' => graph[curr[i]][0],
                'R' => graph[curr[i]][1],
                _ => curr[i]
            };
    
            k = (k + 1) % instructions.Length;
            taskOne++;
        }
        
        scores.Add(taskOne);
    }

    var aggregate = scores.Skip(1).Aggregate(scores.First(), (l, l1) => Lcm(l, l1));
    Console.WriteLine($"Task Two: {aggregate}");
}

long Gcd(long a, long b)
{
    if (a == 0)
        return b;
 
    return Gcd(b % a, a);
}

long Lcm(long a, long b)
{
    return (a * b) / Gcd(a, b);
}