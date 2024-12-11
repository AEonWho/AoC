using System.Collections.Concurrent;
using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt").ToArray();

var map = new Dictionary<MapCoordinate, char>();

var sizeY = lines.Length;
var sizeX = lines[0].Length;

ConcurrentDictionary<char, List<MapCoordinate>> antennas = new();

MapCoordinate startCoordinates = (0, 0);
for (int y = 0; y < sizeY; y++)
{
    for (int x = 0; x < sizeX; x++)
    {
        MapCoordinate coord = (x, y);
        if (lines[y][x] != '.')
        {
            antennas.GetOrAdd(lines[y][x], []).Add(coord);
        }

        map[coord] = lines[y][x];
    }
}

HashSet<MapCoordinate> mapCoords = new HashSet<MapCoordinate>();
HashSet<MapCoordinate> mapCoords2 = new HashSet<MapCoordinate>();

foreach ((var antenna, var coords) in antennas.Where(d => d.Value.Count > 1))
{
    coords.ForEach(d => mapCoords2.Add(d));

    for (int i = 0; i < coords.Count; i++)
    {
        for (int i2 = i + 1; i2 < coords.Count; i2++)
        {
            var first = coords[i];
            var second = coords[i2];

            var offsetX = first.X - second.X;
            var offsetY = first.Y - second.Y;

            MapCoordinate firstAntinode = (first.X + offsetX, first.Y + offsetY);
            MapCoordinate secondAntinode = (second.X - offsetX, second.Y - offsetY);
            mapCoords.Add(firstAntinode);
            mapCoords.Add(secondAntinode);

            while (map.ContainsKey(firstAntinode))
            {
                mapCoords2.Add(firstAntinode);
                firstAntinode = (firstAntinode.X + offsetX, firstAntinode.Y + offsetY);
            }
            while (map.ContainsKey(secondAntinode))
            {
                mapCoords2.Add(secondAntinode);
                secondAntinode = (secondAntinode.X - offsetX, secondAntinode.Y - offsetY);
            }
        }
    }
}

var cnt = mapCoords.Where(map.ContainsKey).Count();

Console.WriteLine("Level 1: " + cnt);

var cnt2 = mapCoords2.Where(map.ContainsKey).Count();

Console.WriteLine("Level 2: " + cnt2);