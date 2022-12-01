var lines = File.ReadAllLines("Input.txt").ToList();


List<List<int>> elves = new List<List<int>>();


elves.Add(new List<int>());
foreach (var line in lines)
{
    if (int.TryParse(line, out var val))
    {
        elves[^1].Add(val);
    }
    else
    {
        elves.Add(new List<int>());
    }
}
Console.WriteLine("Stage1: " + elves.Select(c => c.Sum()).Max());

Console.WriteLine("Stage2: " + elves.Select(c => c.Sum()).OrderByDescending(d => d).Take(3).Sum());