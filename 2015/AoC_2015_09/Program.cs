var lines = File.ReadAllLines("Input.txt");

List<string> locations = new List<string>();

Dictionary<(string, string), int> paths = new Dictionary<(string, string), int>();

foreach (var line in lines)
{
    var data = line.Split(new string[] { " to ", " = " }, StringSplitOptions.None);

    if (!locations.Contains(data[0]))
        locations.Add(data[0]);

    if (!locations.Contains(data[1]))
        locations.Add(data[1]);

    paths.Add((data[0], data[1]), int.Parse(data[2]));
    paths.Add((data[1], data[0]), int.Parse(data[2]));
}

var tmp = GetLocations(locations).ToList();

IEnumerable<List<string>> GetLocations(IEnumerable<string> enumerable)
{
    foreach (var entry in enumerable)
    {
        if (!enumerable.Where(c => c != entry).Any())
        {
            yield return new List<string> { entry };
        }
        else
        {
            var tmp = GetLocations(enumerable.Where(c => c != entry));

            foreach (var t in tmp)
            {
                t.Insert(0, entry);
                yield return t;
            }
        }
    }
}

var lengths = tmp.Select(GetLength).ToList();

int? GetLength(List<string> locations)
{
    int distance = 0;
    for (int i = 1; i < locations.Count; i++)
    {
        var key = (locations[i - 1], locations[i]);
        if (paths.ContainsKey(key))
        {
            distance += paths[key];
        }
        else
        {
            return null;
        }
    }

    return distance;
}

Console.WriteLine($"Kürzeste Distanz: {lengths.Min()}");
Console.WriteLine($"Längste Distanz: {lengths.Max()}");