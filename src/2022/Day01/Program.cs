var calories = File.ReadAllText("input.txt")
    .Split("\n\n")
    .Select(s => s.Split("\n").Select(int.Parse).Sum());

Console.WriteLine($"{calories.Max()}, {calories.OrderByDescending(i => i).Take(3).Sum()}");