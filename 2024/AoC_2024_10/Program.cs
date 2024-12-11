using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt").ToArray();

var map = new Dictionary<MapCoordinate, int>();

var sizeY = lines.Length;
var sizeX = lines[0].Length;

List<MapCoordinate> startCoordinates = new List<MapCoordinate>();
for (int y = 0; y < sizeY; y++)
{
    for (int x = 0; x < sizeX; x++)
    {
        if (lines[y][x] == '0')
        {
            startCoordinates.Add((x, y));
        }

        map[(x, y)] = int.Parse(lines[y][x] + "");
    }
}


var c1 = 0;
var c2 = 0;
foreach (var start in startCoordinates)
{
    List<MapCoordinate> routes1 = [start];
    List<MapCoordinate> routes2 = [start];

    for (int i = 1; i < 10; i++)
    {
        routes1 = routes1.SelectMany(c => c.GetNeighbors()).Where(c => map.TryGetValue(c, out var t) && t == i).Distinct().ToList();
        routes2 = routes2.SelectMany(c => c.GetNeighbors()).Where(c => map.TryGetValue(c, out var t) && t == i).ToList();
    }

    c1 += routes1.Count;
    c2 += routes2.Count;
}

Console.WriteLine("Level 1: " + c1);
Console.WriteLine("Level 2: " + c2);