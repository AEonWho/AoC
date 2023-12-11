using System.Diagnostics;
using AoC_Common;
using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt");

{
    Dictionary<MapCoordinate, int> map = new Dictionary<MapCoordinate, int>();

    for (int y = 0; y < lines.Length; y++)
    {
        var line = lines[y];

        for (int x = 0; x < line.Length; x++)
        {
            map.Add((x, y), line[x] - 48);
        }
    }

    MapCoordinate start = (0, 0);
    MapCoordinate end = (lines[0].Length - 1, lines.Length - 1);

    PathResult result = AoC_PathFinding.FindPath_Directed(start, end, costFunction: d => d == start ? 0 : map[d], validateCoordinate: map.ContainsKey);

    PrintMap(result);
    Console.WriteLine("Level1: " + result.Cost!);
}

{
    Dictionary<MapCoordinate, int> map = new Dictionary<MapCoordinate, int>();

    for (int yMul = 0; yMul < 5; yMul++)
    {
        var offsetY = yMul * lines.Length;
        for (int xMul = 0; xMul < 5; xMul++)
        {
            var offsetX = xMul * lines[0].Length;
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    var number = line[x] - 48;
                    number += (1 * yMul) + (1 * xMul);
                    while (number > 9)
                    {
                        number -= 9;
                    }
                    map.Add((x + offsetX, y + offsetY), number);
                }
            }
        }
    }

    MapCoordinate start = (0, 0);
    MapCoordinate end = (lines[0].Length*5 - 1, lines.Length*5 - 1);

    PathResult result = AoC_PathFinding.FindPath_Directed(start, end, costFunction: d => d == start ? 0 : map[d], validateCoordinate: map.ContainsKey);

    //PrintMap(result);
    Console.WriteLine("Level2: " + result.Cost!);
}

static void PrintMap(PathResult result)
{
    Console.Clear();
    for (var y = result.Path![0].Y; y <= result.Path![^1].Y; y++)
    {
        for (var x = result.Path![0].X; x <= result.Path![^1].X; x++)
        {
            if (result?.Path?.Contains((x, y)) ?? false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write(result.Map.TryGetValue((x, y), out var bla) ? bla.Cost : " ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        Console.WriteLine();
    }
}