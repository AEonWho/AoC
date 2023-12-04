var input = File.ReadAllLines("Input.txt");

List<int> level1Points = new List<int>();
List<Level2Class> level2DataHelper = new();
for (int i = 0; i < input.Length; i++)
{
    var line = input[i];
    var splitted = line.Split(':', '|');

    var winningNumbers = splitted[1].Split([" "], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    var tokens = splitted[2].Split([" "], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    var scoredNumbers = tokens.Intersect(winningNumbers).ToList();
    if (scoredNumbers.Any())
    {
        level1Points.Add((int)Math.Pow(2, scoredNumbers.Count - 1));
    }

    level2DataHelper.Add(new Level2Class { Id = i, Winnings = scoredNumbers.Count });
}

Console.WriteLine("Level1: " + level1Points.Sum());

foreach (var k in level2DataHelper)
{
    for (int i = 1; i <= k.Winnings; i++)
    {
        var nextId = k.Id + i;
        if (nextId < level2DataHelper.Count)
        {
            level2DataHelper[nextId].Count += k.Count;
        }
    }
}

Console.WriteLine("Level2: " + level2DataHelper.Sum(d => d.Count));
Console.ReadLine();

public class Level2Class
{
    public int Id { get; init; }
    public int Winnings { get; init; }
    public int Count { get; set; } = 1;
}