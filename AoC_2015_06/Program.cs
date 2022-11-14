
var input = File.ReadAllLines("Input.txt");

Stage1();

Stage2();

void Stage1()
{
    Dictionary<(int x, int y), bool> map = new Dictionary<(int x, int y), bool>();

    foreach (var entry in input)
    {
        var split = entry.Split(' ', ',');

        var startX = int.Parse(split[^5]);
        var startY = int.Parse(split[^4]);
        var endX = int.Parse(split[^2]);
        var endY = int.Parse(split[^1]);

        bool toggle = entry.StartsWith("toggle");
        bool turnOn = entry.StartsWith("turn on");
        bool turnOff = entry.StartsWith("turn off");

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (toggle)
                {
                    if (!map.ContainsKey((x, y)))
                        map[(x, y)] = true;
                    else
                        map[(x, y)] = !map[(x, y)];
                }
                else if (turnOn)
                {
                    map[(x, y)] = true;
                }
                else if (turnOff)
                {
                    map[(x, y)] = false;
                }
            }
        }
    }

    var litLights = map.Values.Where(c => c).Count();
    Console.WriteLine($"lights lit: {litLights}");
}

void Stage2()
{
    Dictionary<(int x, int y), int> map = new Dictionary<(int x, int y), int>();

    foreach (var entry in input)
    {
        var split = entry.Split(' ', ',');

        var startX = int.Parse(split[^5]);
        var startY = int.Parse(split[^4]);
        var endX = int.Parse(split[^2]);
        var endY = int.Parse(split[^1]);

        bool toggle = entry.StartsWith("toggle");
        bool turnOn = entry.StartsWith("turn on");
        bool turnOff = entry.StartsWith("turn off");

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (toggle)
                {
                    if (!map.ContainsKey((x, y)))
                        map[(x, y)] = 2;
                    else
                        map[(x, y)] += 2;
                }
                else if (turnOn)
                {
                    if (!map.ContainsKey((x, y)))
                        map[(x, y)] = 1;
                    else
                        map[(x, y)] += 1;
                }
                else if (turnOff)
                {
                    if (map.ContainsKey((x, y)) && map[(x, y)] >= 1)
                        map[(x, y)] -= 1;
                }
            }
        }
    }

    var litLights = map.Values.Sum(c => c);
    Console.WriteLine($"lights lit Value: {litLights}");
}