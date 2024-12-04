using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt");

var map = new Dictionary<MapCoordinate, char>();

var sizeY = lines.Length;
var sizeX = lines[0].Length;

for (int y = 0; y < sizeY; y++)
{
    for (int x = 0; x < sizeX; x++)
    {
        map[(x, y)] = lines[y][x];
    }
}

var success = new List<Checker>();
foreach (var coord in map.Keys)
{
    var checkers = new List<Checker>()
    {
        new Checker(coord, MapDirection.N),
        new Checker(coord, MapDirection.NE),
        new Checker(coord, MapDirection.E),
        new Checker(coord, MapDirection.SE),
        new Checker(coord, MapDirection.S),
        new Checker(coord, MapDirection.SW),
        new Checker(coord, MapDirection.W),
        new Checker(coord, MapDirection.NW)
    };

    success.AddRange(checkers.Where(c => c.IsValid(map)));
}

Console.WriteLine("Level1: " + success.Count());

success = new List<Checker>();
foreach (var coord in map.Keys)
{
    var checkers = new List<Checker>()
    {
        new Checker(coord, MapDirection.NE, "MAS"),
        new Checker(coord, MapDirection.SE, "MAS"),
        new Checker(coord, MapDirection.SW, "MAS"),
        new Checker(coord, MapDirection.NW, "MAS")
    };

    success.AddRange(checkers.Where(c => c.IsValid(map)));
}

var test = success.GroupBy(d => d.GetCoordinates().ToArray()[1]).Where(d => d.Count() > 1).ToList();
Console.WriteLine("Level2: " + test.Count());