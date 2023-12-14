using AoC_Common.PathFinding;

var input = File.ReadAllLines("Input.txt");

var maxY = 0;

List<string> pattern = new List<string>();
Dictionary<MapCoordinate, char> map = new();
{
    foreach (var line in input)
    {
        for (var x = 0; x < line.Length; x++)
        {
            MapCoordinate coord = (x, maxY);
            var newChar = line[x];
            map[coord] = newChar;
        }
        maxY++;
    }
    Tilt("S", d => d.South);
    var sum = map.ToList().Where(d => d.Value == 'O').Sum(d => maxY - d.Key.Y);

    Console.WriteLine("Level1: " + sum);

    var patternStartIdx = -1;
    var patternIdx = 0;
    for (int i = 0; i < 1000000000; i++)
    {
        if (patternStartIdx == -1)
        {
            if (i != 0)
            {
                Tilt("S", d => d.South);
            }
            Tilt("W", d => d.West);
            Tilt("N", d => d.North);
            Tilt("E", d => d.East);
            patternStartIdx = CheckPattern();
            patternIdx = i;
        }
        else
        {
            patternIdx++;
            if (patternIdx >= pattern.Count)
            {
                patternIdx = patternStartIdx;
            }
        }
    }

    Console.WriteLine(patternIdx);

    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxY; x++)
        {
            map[(x, y)] = pattern[patternIdx + 1][x + (y * maxY)];
        }
    }

    sum = map.ToList().Where(d => d.Value == 'O').Sum(d => maxY - d.Key.Y);

    Console.WriteLine("Level2: " + sum);

}

int CheckPattern()
{
    var newPattern = string.Join("", map.OrderBy(d => d.Key.Y).ThenBy(d => d.Key.X).Select(c => c.Value));
    var idx = pattern.IndexOf(newPattern);
    if (idx == -1)
    {
        pattern.Add(newPattern);
    }

    return idx;
}

void Tilt(string direction, Func<MapCoordinate, MapCoordinate> rollCoordinate)
{
    for (int y = direction == "N" ? (maxY - 1) : 0; (direction == "N" ? y >= 0 : y < maxY); y += (direction == "N" ? -1 : +1))
    {
        for (int x = direction == "E" ? (maxY - 1) : 0; (direction == "E" ? x >= 0 : x < maxY); x += (direction == "E" ? -1 : +1))
        {
            MapCoordinate coord = (x, y);
            if (map[coord] == 'O')
            {
                var nextCoord = rollCoordinate(coord);
                while (map.TryGetValue(nextCoord, out var c) && c == '.')
                {
                    map[coord] = '.';
                    map[nextCoord] = 'O';

                    coord = nextCoord;
                    nextCoord = rollCoordinate(coord);
                }
            }
        }

    }
}