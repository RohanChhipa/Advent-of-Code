using System.Text;

TaskOne();
TaskTwo();

void TaskOne()
{
    var strings = File.ReadAllLines("input.txt")
        .Select(s => s.ToCharArray().Where(char.IsDigit).ToList())
        .Select(list => int.Parse($"{list.First()}{(list.Count == 1 ? list.First() : list.Last())}"));
    
    Console.WriteLine(strings.Sum());
}

void TaskTwo()
{
    var lookups = new Dictionary<string, string>
    {
        { "zero", "0" },
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four", "4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" },
    };

    var strings = File.ReadAllLines("input.txt")
        .Select(s =>
        {
            var front = new StringBuilder();
            foreach (var character in s.ToCharArray())
            {
                front.Append(character);

                foreach (var (key, value) in lookups)
                    front = front.Replace(key, value);
            }

            var back = new StringBuilder();
            foreach (var character in s.ToCharArray().Reverse())
            {
                back.Insert(0, character);

                foreach (var (key, value) in lookups)
                    back = back.Replace(key, value);
            }

            var first = front.ToString().ToCharArray().Where(char.IsDigit).First();
            var last = back.ToString().ToCharArray().Where(char.IsDigit).Last();

            Console.WriteLine(first);
            Console.WriteLine(last);

            return int.Parse($"{first}{last}");
        });
    
    Console.WriteLine(strings.Sum());
}