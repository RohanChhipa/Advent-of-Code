TaskOne();
TaskTwo();

void TaskOne()
{
    var lines = File.ReadAllLines("input.txt");
    var validGames = 0;
    foreach (var line in lines)
    {
        var sets = line.Split(": ", StringSplitOptions.TrimEntries);
        var game = int.Parse(sets.First().Replace("Game ", ""));
        var validGame = true;

        sets = sets.Last().Split(";", StringSplitOptions.TrimEntries);
        foreach (var set in sets)
        {
            
            var dictionary = new Dictionary<string, int>
            {
                {"blue", 0},
                {"red", 0},
                {"green", 0}
            };
            
            var cubes = set.Split(", ", StringSplitOptions.TrimEntries);
            foreach (var cube in cubes)
            {
                var c = cube.Split(" ");
                dictionary[c.Last()] += int.Parse(c.First());
            }

            if (dictionary["red"] > 12
                || dictionary["green"] > 13
                || dictionary["blue"] > 14)
            {
                validGame = false;
                break;
            }
        }

        if (validGame)
            validGames += game;
    }
    
    Console.WriteLine(validGames);
}

void TaskTwo()
{
    var lines = File.ReadAllLines("input.txt");
    var validGames = 0L;
    foreach (var line in lines)
    {
        var sets = line.Split(": ", StringSplitOptions.TrimEntries);
        var game = int.Parse(sets.First().Replace("Game ", ""));

        sets = sets.Last().Split(";", StringSplitOptions.TrimEntries);
        
        var dictionary = new Dictionary<string, int>
        {
            {"blue", 0},
            {"red", 0},
            {"green", 0}
        };
        
        foreach (var set in sets)
        {
            var cubes = set.Split(", ", StringSplitOptions.TrimEntries);
            foreach (var cube in cubes)
            {
                var c = cube.Split(" ");
                dictionary[c.Last()] = Math.Max(dictionary[c.Last()], int.Parse(c.First()));
            }
        }

        validGames += dictionary["red"] * dictionary["green"] * dictionary["blue"];
    }
    
    Console.WriteLine(validGames);
}