var lines = File.ReadAllLines("Input.txt").ToList();

List<Game> games = new List<Game>();
for (int i = 0; i < lines.Count; i++)
{
    var line = lines[i];

    var split = line.Split(new char[] { ':', ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

    var game = new Game(int.Parse(split[1]));
    for (int x = 2; x < split.Length; x += 2)
    {
        var nmb = split[x];
        var color = split[x + 1];

        game.Tokens.Add((color, int.Parse(nmb)));
    }
    games.Add(game);
}

Console.WriteLine("Level1: " + games.Where(CheckLvl1).Sum(d => d.Id));
Console.WriteLine("Level2: " + games.Sum(GetLvl2));

decimal GetLvl2(Game game)
{
    return game.Tokens.GroupBy(d => d.Color).Select(c => c.Max(c => c.Count)).Aggregate((f, s) => f * s);
}

bool CheckLvl1(Game game)
{
    if (game.Tokens.Any(c => c.Color == "red" && c.Count > 12))
    {
        return false;
    }
    if (game.Tokens.Any(c => c.Color == "green" && c.Count > 13))
    {
        return false;
    }
    if (game.Tokens.Any(c => c.Color == "blue" && c.Count > 14))
    {
        return false;
    }

    return true;
}