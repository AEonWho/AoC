var data = File.ReadAllLines("Input.txt");

HashSet<(int, int)> lightMap = new HashSet<(int, int)>();

Init(data, lightMap);

for (int i = 0; i < 100; i++)
{
    var newLightMap = new HashSet<(int, int)>();

    GameOfLife(lightMap, newLightMap);

    lightMap = newLightMap;
}

Console.WriteLine("Stage1: " + lightMap.Count());

lightMap = new HashSet<(int, int)>();

Init(data, lightMap);

StuckLights(lightMap);

for (int i = 0; i < 100; i++)
{
    var newLightMap = new HashSet<(int, int)>();

    GameOfLife(lightMap, newLightMap);

    lightMap = newLightMap;

    StuckLights(lightMap);
}

Console.WriteLine("Stage2: " + lightMap.Count());

static void GameOfLife(HashSet<(int, int)> lightMap, HashSet<(int, int)> newLightMap)
{
    for (int y = 0; y < 100; y++)
    {
        for (int x = 0; x < 100; x++)
        {
            int lightsOn = 0;

            if (lightMap.Contains((x - 1, y - 1)))
                lightsOn++;

            if (lightMap.Contains((x, y - 1)))
                lightsOn++;

            if (lightMap.Contains((x + 1, y - 1)))
                lightsOn++;

            if (lightMap.Contains((x - 1, y)))
                lightsOn++;

            if (lightMap.Contains((x + 1, y)))
                lightsOn++;

            if (lightMap.Contains((x - 1, y + 1)))
                lightsOn++;

            if (lightMap.Contains((x, y + 1)))
                lightsOn++;

            if (lightMap.Contains((x + 1, y + 1)))
                lightsOn++;

            if (lightsOn == 2 && lightMap.Contains((x, y)))
            {
                newLightMap.Add((x, y));
            }
            else if (lightsOn == 3)
            {
                newLightMap.Add((x, y));
            }
        }
    }
}

static void StuckLights(HashSet<(int, int)> lightMap)
{
    if (!lightMap.Contains((0, 0)))
        lightMap.Add((0, 0));

    if (!lightMap.Contains((0, 99)))
        lightMap.Add((0, 99));

    if (!lightMap.Contains((99, 0)))
        lightMap.Add((99, 0));

    if (!lightMap.Contains((99, 99)))
        lightMap.Add((99, 99));
}

static void Init(string[] data, HashSet<(int, int)> lightMap)
{
    for (int y = 0; y < 100; y++)
    {
        for (int x = 0; x < 100; x++)
        {
            if (data[y][x] == '#')
            {
                lightMap.Add((x, y));
            }
        }
    }
}