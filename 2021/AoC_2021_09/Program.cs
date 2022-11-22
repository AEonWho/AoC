var data = File.ReadAllLines("Input.txt");

var xMax = data.Length;
var yMax = data[0].Length;

var dict = new Dictionary<(int, int), int>();

for (int y = 0; y < yMax; y++)
{
    for (int x = 0; x < xMax; x++)
    {
        dict.Add((x, y), data[x][y] - 48);
    }
}

var points = new List<(int x, int y)>();

for (int y = 0; y < yMax; y++)
{
    for (int x = 0; x < xMax; x++)
    {
        var cValue = dict[(x, y)];
        if (dict.ContainsKey((x - 1, y)) && cValue >= dict[(x - 1, y)])
            continue;

        if (dict.ContainsKey((x + 1, y)) && cValue >= dict[(x + 1, y)])
            continue;

        if (dict.ContainsKey((x, y - 1)) && cValue >= dict[(x, y - 1)])
            continue;

        if (dict.ContainsKey((x, y + 1)) && cValue >= dict[(x, y + 1)])
            continue;

        points.Add((x, y));
    }
}

var risk = points.Sum(d => dict[d] + 1);
Console.WriteLine(risk);

Dictionary<(int x, int y), HashSet<(int, int)>> basins = new Dictionary<(int x, int y), HashSet<(int, int)>>();
foreach (var point in points)
{
    HashSet<(int, int)> locations = new HashSet<(int, int)>();

    CheckBasins(point, locations);

    basins.Add(point, locations);
}

var tmp = basins.OrderByDescending(c => c.Value.Count).Select(c => c.Value.Count).Take(3).ToList();

var res = tmp.Aggregate((d, e) => d * e);

Console.WriteLine(res);

void CheckBasins((int x, int y) point, HashSet<(int, int)> locations)
{
    if (dict[point] == 9)
        return;

    locations.Add(point);

    var cValue = dict[point];

    var points = new[] { (point.x - 1, point.y), (point.x + 1, point.y), (point.x, point.y - 1), (point.x, point.y + 1) };

    foreach (var p in points)
    {
        if (dict.ContainsKey(p) && cValue <= dict[p] && !locations.Contains(p))
        {
            CheckBasins(p, locations);
        }
    }
}