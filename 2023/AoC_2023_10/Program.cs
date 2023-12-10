var input = File.ReadAllLines("Input.txt");

(int x, int y) startPoint = default;

Dictionary<(int x, int y), char> Map = new Dictionary<(int, int), char>();

for (int y = 0; y < input.Length; y++)
{
    var line = input[y];

    for (int x = 0; x < line.Length; x++)
    {
        switch (line[x])
        {
            case 'S':
                startPoint = (x, y);
                break;
        }

        Map.Add((x, y), line[x]);

        if (line[x] == 'S')
            startPoint = (x, y);
    }
}

var testPoints = GetNeighbors(startPoint);

var validNeighbors = testPoints.Where(d => Map.TryGetValue(d, out var bla) && GetRoute(bla, d).Contains(startPoint)).OrderBy(d => d.Item1).ThenBy(d => d.Item2).ToList();

var length = 1;

var firstNeighbor = validNeighbors[0];
var secondNeighbor = validNeighbors[1];

if (GetRoute('-', startPoint).All(validNeighbors.Contains))
    Map[startPoint] = '-';
else if (GetRoute('|', startPoint).All(validNeighbors.Contains))
    Map[startPoint] = '|';
else if (GetRoute('J', startPoint).All(validNeighbors.Contains))
    Map[startPoint] = 'J';
else if (GetRoute('L', startPoint).All(validNeighbors.Contains))
    Map[startPoint] = 'L';
else if (GetRoute('F', startPoint).All(validNeighbors.Contains))
    Map[startPoint] = 'F';
else if (GetRoute('7', startPoint).All(validNeighbors.Contains))
    Map[startPoint] = '7';

HashSet<(int, int)> loop = new HashSet<(int, int)>() { startPoint, firstNeighbor, secondNeighbor };
while (firstNeighbor != secondNeighbor)
{
    firstNeighbor = GetRoute(Map[firstNeighbor], firstNeighbor).First(d => !loop.Contains(d));
    secondNeighbor = GetRoute(Map[secondNeighbor], secondNeighbor).First(d => !loop.Contains(d));

    loop.Add(firstNeighbor);
    loop.Add(secondNeighbor);

    length++;
}
Console.WriteLine("Level1: " + length);

HashSet<(int x, int y)> inside = new HashSet<(int x, int y)>();
HashSet<(int x, int y)> outside = new HashSet<(int x, int y)>();
for (int y = 0; y < input.Length; y++)
{
    var line = input[y];

    for (int x = 0; x < line.Length; x++)
    {
        var m = (x, y);
        if (loop.Contains(m))
            continue;

        var current = m;
        double wallcounter = 0;
        int cross = 0;
        while (true)
        {
            if (loop.Contains(current))
            {
                if (Map[current] == '-')
                {
                    wallcounter++;
                }
                else if (Map[current] == 'J' || Map[current] == 'F')
                {
                    cross++;
                }
                else if (Map[current] == 'L' || Map[current] == '7')
                {
                    cross--;
                }
            }

            if (cross == -2 || cross == 2)
            {
                wallcounter++;
                cross = 0;
            }
            current = (current.x, current.y - 1);

            if (outside.Contains(current) || !Map.ContainsKey(current))
            {
                if (wallcounter % 2 == 0)
                {
                    outside.Add(m);
                }
                else
                {
                    inside.Add(m);
                }
                break;
            }
            else if (inside.Contains(current))
            {
                if (wallcounter % 2 == 0)
                {
                    inside.Add(m);
                }
                else
                {
                    outside.Add(m);
                }
                break;
            }
        }
    }
}

Console.WriteLine("Level2: " + inside.Count);

for (int y = 0; y < input.Length; y++)
{
    var line = input[y];

    for (int x = 0; x < line.Length; x++)
    {
        if (loop.Contains((x, y)))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if((x,y) == startPoint)
                Console.ForegroundColor = ConsoleColor.Red;
            
            switch (line[x])
            {
                case 'F':
                    Console.Write("┌");
                    break;
                case '7':
                    Console.Write("┐");
                    break;
                case 'J':
                    Console.Write("┘");
                    break;
                case 'L':
                    Console.Write("└");
                    break;
                case '-':
                    Console.Write("─");
                    break;
                case '|':
                    Console.Write("│");
                    break;
                case 'S':
                    Console.Write("S");
                    break;
            }
        }
        else
        {
            if (inside.Contains((x, y)))
                Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("#");
        }

        Console.ForegroundColor = ConsoleColor.Gray;
    }
    Console.WriteLine();
}

static IEnumerable<(int x, int y)> GetRoute(char c, (int x, int y) point)
{
    switch (c)
    {
        case '|':
            yield return (point.x, point.y + 1);
            yield return (point.x, point.y - 1);
            break;
        case '-':
            yield return (point.x + 1, point.y);
            yield return (point.x - 1, point.y);
            break;
        case 'F':
            yield return (point.x, point.y + 1);
            yield return (point.x + 1, point.y);
            break;
        case 'L':
            yield return (point.x + 1, point.y);
            yield return (point.x, point.y - 1);
            break;
        case 'J':
            yield return (point.x - 1, point.y);
            yield return (point.x, point.y - 1);
            break;
        case '7':
            yield return (point.x - 1, point.y);
            yield return (point.x, point.y + 1);
            break;

        default:
            yield break;
    }
}

static IEnumerable<(int, int)> GetNeighbors((int x, int y) startPoint)
{
    yield return (startPoint.x + 1, startPoint.y);
    yield return (startPoint.x - 1, startPoint.y);
    yield return (startPoint.x, startPoint.y + 1);
    yield return (startPoint.x, startPoint.y - 1);
}