var input = File.ReadAllLines("Input.txt");

Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

foreach (var line in input)
{
    var split = line.Split('-');

    if (split[0] != "end" && split[1] != "start")
    {
        if (!map.ContainsKey(split[0]))
            map.Add(split[0], new List<string>());

        map[split[0]].Add(split[1]);
    }

    if (split[0] != "start" && split[1] != "end")
    {
        if (!map.ContainsKey(split[1]))
            map.Add(split[1], new List<string>());

        map[split[1]].Add(split[0]);
    }
}

var smallCaves = map.Keys.Where(d => d != "start" && d != "end" && d.All(char.IsLower)).ToHashSet();

List<CavePath> list = new List<CavePath> { new CavePath("start", null) };
List<CavePath> finished = new List<CavePath>();

while (list.Any())
{
    var newlist = list.SelectMany(d => GetOptions(d, false)).ToList();

    finished.AddRange(newlist.Where(d => d.CurrentPosition == "end"));
    list = newlist.Where(d => d.CurrentPosition != "end").ToList();
}

Console.WriteLine($"Stage1: {finished.Count}");

list = new List<CavePath> { new CavePath("start", null) };
finished = new List<CavePath>();

while (list.Any())
{
    var newlist = list.SelectMany(d => GetOptions(d, true)).ToList();

    finished.AddRange(newlist.Where(d => d.CurrentPosition == "end"));
    list = newlist.Where(d => d.CurrentPosition != "end").ToList();
}
Console.WriteLine($"Stage2: {finished.Count}");

IEnumerable<CavePath> GetOptions(CavePath arg, bool stage2 = false)
{
    var options = map[arg.CurrentPosition];
    foreach (var option in options)
    {
        if (smallCaves.Contains(option) && arg.Path.Contains(option))
        {
            if (stage2)
            {
                if (arg.Path.Where(smallCaves.Contains).GroupBy(d => d).Any(c => c.Count() > 1))
                    continue;
            }
            else
            {
                continue;
            }

        }

        yield return new CavePath(option, arg);
    }
}

internal class CavePath
{
    private List<string> path;

    public CavePath(string v, CavePath origin)
    {
        path = origin?.path.ToList() ?? new List<string>();
        path.Add(v);
    }

    public string CurrentPosition => path[^1];

    public IEnumerable<string> Path { get { return path; } }
}