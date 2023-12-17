using System.Diagnostics;
using System.Xml.XPath;
using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt");

Dictionary<MapCoordinate, int> map = new Dictionary<MapCoordinate, int>();

for (int y = 0; y < lines.Length; y++)
{
    var line = lines[y];

    for (int x = 0; x < line.Length; x++)
    {
        map.Add((x, y), line[x] - 48);
    }
}

{
    MapCoordinate start = (0, 0);
    MapCoordinate end = (lines[0].Length - 1, lines.Length - 1);

    PriorityQueue<TmpData, int> queue = new();
    queue.Enqueue(new TmpData(start, "N", -1), 0);
    queue.Enqueue(new TmpData(start, "E", -1), 0);

    HashSet<TmpData> tmp = new();

    long result = 0;
    while (queue.TryDequeue(out var data, out var cost))
    {
        if (data.Coordinate == end)
        {
            result = cost;
            break;
        }

        TryAdd(data.Direction);
        switch (data.Direction)
        {
            case "N":
            case "S":
                TryAdd("E");
                TryAdd("W");
                break;
            case "E":
            case "W":
                TryAdd("S");
                TryAdd("N");
                break;
        }

        void TryAdd(string direction)
        {
            int length = data.Direction == direction ? data.DirectionLength + 1 : 0;
            if (length >= 3)
                return;

            MapCoordinate coord = data.Coordinate;
            switch (direction)
            {
                case "N":
                    coord = coord.North();
                    break;
                case "E":
                    coord = coord.East();
                    break;
                case "S":
                    coord = coord.South();
                    break;
                case "W":
                    coord = coord.West();
                    break;
            }

            if (coord.X < start.X || coord.Y < start.Y || coord.X > end.X || coord.Y > end.Y)
                return;

            var tmpEntry = new TmpData(coord, direction, length);
            if (tmp.Contains(tmpEntry))
                return;

            tmp.Add(tmpEntry);

            var c = cost + map[coord];
            queue.Enqueue(tmpEntry, c);

        }
    }

    Console.WriteLine("Level1: " + result);
}

{
    MapCoordinate start = (0, 0);
    MapCoordinate end = (lines[0].Length - 1, lines.Length - 1);

    PriorityQueue<TmpData, int> queue = new();
    queue.Enqueue(new TmpData(start, "N", -1), 0);
    queue.Enqueue(new TmpData(start, "E", -1), 0);

    HashSet<TmpData> tmp = new();

    long result = 0;
    while (queue.TryDequeue(out var data, out var cost))
    {
        if (data.Coordinate == end && data.DirectionLength >= 3)
        {
            result = cost;
            break;
        }

        TryAdd(data.Direction, data.DirectionLength + 1);
        if (data.DirectionLength >= 3)
        {
            switch (data.Direction)
            {
                case "N":
                case "S":
                    TryAdd("E", 0);
                    TryAdd("W", 0);
                    break;
                case "E":
                case "W":
                    TryAdd("S", 0);
                    TryAdd("N", 0);
                    break;
            }
        }

        void TryAdd(string direction, int length)
        {
            if (length >= 10)
                return;

            MapCoordinate coord = data.Coordinate;
            switch (direction)
            {
                case "N":
                    coord = coord.North();
                    break;
                case "E":
                    coord = coord.East();
                    break;
                case "S":
                    coord = coord.South();
                    break;
                case "W":
                    coord = coord.West();
                    break;
            }

            if (coord.X < start.X || coord.Y < start.Y || coord.X > end.X || coord.Y > end.Y)
                return;

            var tmpEntry = new TmpData(coord, direction, length);
            if (tmp.Contains(tmpEntry))
                return;

            tmp.Add(tmpEntry);

            var c = cost + map[coord];
            queue.Enqueue(tmpEntry, c);

        }
    }


    Console.WriteLine("Level2: " + result);
}

public record TmpData(MapCoordinate Coordinate, string Direction, int DirectionLength);
