using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt").Reverse().ToArray();

var map = new Dictionary<MapCoordinate, char>();

var sizeY = lines.Length;
var sizeX = lines[0].Length;

MapCoordinate startCoordinates = (0, 0);
for (int y = 0; y < sizeY; y++)
{
    for (int x = 0; x < sizeX; x++)
    {
        if (lines[y][x] == '^')
        {
            startCoordinates = new MapCoordinate(x, y);
        }

        map[(x, y)] = lines[y][x];
    }
}

var result = Run(map);

Console.WriteLine("Level1: " + result.Distinct().Count());

var bla = result.Where(d => d != startCoordinates).Distinct().Where(path =>
{
    var test = map.ToDictionary(d => d.Key, d => d.Value);
    test[path] = '#';

    var t = Run(test);

    return t == null;
}).ToList();

Console.WriteLine("Level2: " + bla.Count);




IEnumerable<MapCoordinate> Run(Dictionary<MapCoordinate, char> _map)
{
    var direction = MapDirection.N;
    HashSet<(MapDirection, MapCoordinate)> path = new HashSet<(MapDirection, MapCoordinate)>();
    path.Add((direction, startCoordinates));

    var currentCoord = startCoordinates;
    while (true)
    {
        var next = currentCoord.MoveDirection(direction);

        if (!_map.ContainsKey(next))
            break;

        if (_map[next] == '#')
        {
            direction = direction.Rotate90();
        }
        else
        {
            if (path.Contains((direction, next)))
                return null;
            currentCoord = next;
            path.Add((direction, next));
        }
    }

    return path.Select(c => c.Item2);
}