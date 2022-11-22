var initMap = File.ReadAllLines("Input.txt");

Dictionary<(int, int), int> Map = new Dictionary<(int, int), int>();

for (int y = 0; y < initMap.Length; y++)
{
    for (int x = 0; x < initMap[y].Length; x++)
    {
        Map.Add((x, y), initMap[y][x] - 48);
    }
}

int flashes = 0;
int i = 0;
while (true)
{
    i++;
    flashes += IterateMap(Map);

    Console.WriteLine($"Iteration {i}: {flashes}");

    if (Map.All(d => d.Value == 0))
    {
        Console.WriteLine($"Synced at Iteration {i}");
        break;
    }
}



static int IterateMap(Dictionary<(int, int), int> Map)
{
    foreach (var key in Map.Keys)
        Map[key]++;

    HashSet<(int, int)> values = new HashSet<(int, int)>();

    var mappedLights = Map.Where(d => d.Value > 9).Select(d => d.Key).ToList();
    while (mappedLights.Any())
    {
        foreach (var mappedLight in mappedLights)
        {
            for (int offsetX = -1; offsetX <= 1; offsetX++)
            {
                for (int offsetY = -1; offsetY <= 1; offsetY++)
                {
                    var point = (mappedLight.Item1 + offsetX, mappedLight.Item2 + offsetY);
                    if (Map.ContainsKey(point))
                    {
                        Map[point]++;
                    }
                }
            }
        }

        mappedLights.ForEach(d => values.Add(d));
        mappedLights = Map.Where(d => d.Value > 9).Select(d => d.Key).Except(values).ToList();
    }

    foreach (var key in values)
        Map[key] = 0;

    return values.Count;
}