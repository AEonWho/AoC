using AoC_Common.PathFinding;

var lines = File.ReadAllLines("Input.txt");

Dictionary<MapCoordinate, char> map = new Dictionary<MapCoordinate, char>();

for (int y = 0; y < lines.Length; y++)
{
    var line = lines[y];
    for (int x = 0; x < line.Length; x++)
    {
        map.Add((x, y), line[x]);
    }
}

HashSet<(MapCoordinate, string)> finishedBeams = TestBeam((-1, 0), "E");

Console.WriteLine("Level1: " + finishedBeams.Select(c => c.Item1).Distinct().Count());

var longestBeam = 0;
for (int y = 0; y < lines.Length; y++)
{
    finishedBeams = TestBeam((-1, y), "E");
    var count = finishedBeams.Select(c => c.Item1).Distinct().Count();
    if (count > longestBeam)
        longestBeam = count;
}

for (int y = 0; y < lines.Length; y++)
{
    finishedBeams = TestBeam((lines[0].Length, y), "W");
    var count = finishedBeams.Select(c => c.Item1).Distinct().Count();
    if (count > longestBeam)
        longestBeam = count;
}

for (int x = 0; x < lines[0].Length; x++)
{
    finishedBeams = TestBeam((x, -1), "N");
    var count = finishedBeams.Select(c => c.Item1).Distinct().Count();
    if (count > longestBeam)
        longestBeam = count;
}

for (int x = 0; x < lines[0].Length; x++)
{
    finishedBeams = TestBeam((x, lines.Length), "S");
    var count = finishedBeams.Select(c => c.Item1).Distinct().Count();
    if (count > longestBeam)
        longestBeam = count;
}

Console.WriteLine("Level2: " + longestBeam);

HashSet<(MapCoordinate, string)> TestBeam((int, int) start, string dir)
{
    HashSet<(MapCoordinate, string)> currentBeams = new HashSet<(MapCoordinate, string)>()
{
    (start, dir)
};

    var finishedBeams = new HashSet<(MapCoordinate, string)>();
    while (currentBeams.Any())
    {
        var nextBeams = new HashSet<(MapCoordinate, string)>();
        foreach (var currentVector in currentBeams)
        {
            switch (currentVector.Item2)
            {
                case "E":
                    {
                        var next = currentVector.Item1.East();
                        if (map.ContainsKey(next))
                        {
                            switch (map[next])
                            {
                                case '|':
                                    CheckAndAdd(next, "N");
                                    CheckAndAdd(next, "S");
                                    break;
                                case '\\':
                                    CheckAndAdd(next, "N");
                                    break;
                                case '/':
                                    CheckAndAdd(next, "S");
                                    break;
                                case '-':
                                default:
                                    CheckAndAdd(next, "E");
                                    break;
                            }
                        }
                    }
                    break;
                case "W":
                    {
                        var next = currentVector.Item1.West();
                        if (map.ContainsKey(next))
                        {
                            switch (map[next])
                            {
                                case '|':
                                    CheckAndAdd(next, "N");
                                    CheckAndAdd(next, "S");
                                    break;
                                case '\\':
                                    CheckAndAdd(next, "S");
                                    break;
                                case '/':
                                    CheckAndAdd(next, "N");
                                    break;
                                case '-':
                                default:
                                    CheckAndAdd(next, "W");
                                    break;
                            }
                        }
                    }
                    break;
                case "N":
                    {
                        var next = currentVector.Item1.North();
                        if (map.ContainsKey(next))
                        {
                            switch (map[next])
                            {
                                case '-':
                                    CheckAndAdd(next, "E");
                                    CheckAndAdd(next, "W");
                                    break;
                                case '\\':
                                    CheckAndAdd(next, "E");
                                    break;
                                case '/':
                                    CheckAndAdd(next, "W");
                                    break;
                                case '|':
                                default:
                                    CheckAndAdd(next, "N");
                                    break;
                            }
                        }
                    }
                    break;
                case "S":
                    {
                        var next = currentVector.Item1.South();
                        if (map.ContainsKey(next))
                        {
                            switch (map[next])
                            {
                                case '-':
                                    CheckAndAdd(next, "E");
                                    CheckAndAdd(next, "W");
                                    break;
                                case '\\':
                                    CheckAndAdd(next, "W");
                                    break;
                                case '/':
                                    CheckAndAdd(next, "E");
                                    break;
                                case '|':
                                default:
                                    CheckAndAdd(next, "S");
                                    break;
                            }
                        }
                    }
                    break;
            }

            void CheckAndAdd(MapCoordinate next, string direction)
            {
                if (!finishedBeams.Contains((next, direction)))
                {
                    nextBeams.Add((next, direction));
                    finishedBeams.Add((next, direction));
                }

            }
        }

        currentBeams = nextBeams;
    }

    return finishedBeams;
}