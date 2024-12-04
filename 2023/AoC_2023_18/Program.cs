using System.Linq;
using System.Xml.Serialization;
using AoC_Common;
using AoC_Common.PathFinding;

var text = File.ReadAllLines("Input.txt");

{
    HashSet<MapCoordinate> map = ParseLevel1(text);

    var minX = map.Min(d => d.X);
    var minY = map.Min(d => d.Y);
    var maxX = map.Max(d => d.X);
    var maxY = map.Max(d => d.Y);

    long insideElementCount = CountInside(map, minX, minY, maxX, maxY);

    Console.WriteLine(insideElementCount + map.Count);
}

{
    var map = ParseLevel2(text, out var borderLength);

    var inner = AoC_Math.Shoelace(map);

    //shoeLace -> 0,2 2,2 2,0 0,0 -> liefert 4
    //halben umfang dazunehmen da der schon teilweise inkludiert war, +1
    Console.WriteLine(inner + (borderLength / 2) + 1);
}
static long CountInside(HashSet<MapCoordinate> map, long minX, long minY, long maxX, long maxY)
{
    long insideElementCount = 0;
    {
        MapCoordinate? start = null;
        for (long y = minY; y <= maxY; y++)
        {
            bool inside = false;
            start = null;
            for (long x = minX; x <= maxX; x++)
            {
                if (map.Contains((x, y)))
                {
                    if (map.Contains((x, y - 1)) && map.Contains((x, y + 1)))
                    {
                        inside = !inside;
                    }
                    else
                    {
                        start ??= (x, y);
                    }
                }
                else
                {
                    if (start != null && map.Contains((x - 1, y)))
                    {
                        if (map.Contains((start.X, start.Y - 1)) && map.Contains((x - 1, start.Y + 1)))
                        {
                            inside = !inside;
                        }
                        else if (map.Contains((start.X, start.Y + 1)) && map.Contains((x - 1, start.Y - 1)))
                        {
                            inside = !inside;
                        }
                    }

                    if (inside)
                    {
                        insideElementCount++;
                    }
                    start = null;
                }

            }
        }
    }

    return insideElementCount;
}

HashSet<MapCoordinate> ParseLevel1(string[] text)
{
    HashSet<MapCoordinate> map = new HashSet<MapCoordinate>();
    int y = 0;
    int x = 0;

    foreach (var line in text)
    {
        var splitted = line.Split([" ", "(", ")"], StringSplitOptions.RemoveEmptyEntries);
        var amount = int.Parse(splitted[1]);

        for (int d = 0; d < amount; d++)
        {
            switch (splitted[0])
            {
                case "R":
                    x++;
                    break;
                case "L":
                    x--;
                    break;
                case "U":
                    y--;
                    break;
                case "D":
                    y++;
                    break;
            }
            map.Add((x, y));
        }
    }

    return map;
}

List<MapCoordinate> ParseLevel2(string[] text, out long length)
{
    List<MapCoordinate> map = new List<MapCoordinate>();
    int y = 0;
    int x = 0;
    length = 0;
    foreach (var line in text)
    {
        var splitted = line.Split([" ", "(", ")"], StringSplitOptions.RemoveEmptyEntries);
        var amount = int.Parse(splitted[2][1..^1], System.Globalization.NumberStyles.HexNumber);

        length += amount;
        switch (splitted[2][^1..])
        {
            case "0":
                x += amount;
                break;
            case "2":
                x -= amount;
                break;
            case "3":
                y -= amount;
                break;
            case "1":
                y += amount;
                break;
        }
        map.Add((x, y));
    }

    return map;
}