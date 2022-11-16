using System.Collections.Concurrent;

var lines = File.ReadAllLines("Input.txt");

ConcurrentDictionary<(string, int), List<string>> dataset = new ConcurrentDictionary<(string, int), List<string>>();

Dictionary<string, Dictionary<string, int>> aunts = new Dictionary<string, Dictionary<string, int>>();

foreach (var line in lines)
{
    var splitted = line.Split(new string[] { ", ", ": " }, StringSplitOptions.RemoveEmptyEntries);

    var name = splitted[0];

    var values = splitted.Skip(1).Chunk(2).ToDictionary(d => d[0], d => int.Parse(d[1]));

    aunts.Add(name, values);

    foreach (var value in values)
    {
        var list = dataset.GetOrAdd((value.Key, value.Value), d => new List<string>());
        list.Add(name);
    }
}

{
    var measurements = new List<(string, int)>
    {
        ("children", 3),
        ("cats", 7),
        ("samoyeds", 2),
        ("pomeranians", 3),
        ("akitas", 0),
        ("vizslas", 0),
        ("goldfish", 5),
        ("trees", 3),
        ("cars", 2),
        ("perfumes", 1),
    };

    var validDataSets = measurements.Where(dataset.ContainsKey).Select(d => dataset[d]).ToList();
    var result = validDataSets.SelectMany(c => c).GroupBy(c => c).Where(c => c.Count() == 3).First().Key;

    Console.WriteLine($"Level1 Aunt Sue: {result}");
}

{
    var measurements = new List<(string, int)>
    {
        ("children", 3),
        ("samoyeds", 2),
        ("akitas", 0),
        ("vizslas", 0),
        ("cars", 2),
        ("perfumes", 1),
    };

    var t1 = measurements.Where(dataset.ContainsKey).Select(d => dataset[d]).ToList();

    var measurementsMore = new List<(string, int)>
    {
        ("cats", 7),
        ("trees", 3),
    };

    var t2 = measurementsMore.SelectMany(d => dataset.Where(c => c.Key.Item1 == d.Item1 && c.Key.Item2 > d.Item2)).Select(c=>c.Value).ToList();

    var measurementsFewer = new List<(string, int)>
    {
        ("goldfish", 5),
        ("pomeranians", 3),
    };

    var t3 = measurementsFewer.SelectMany(d => dataset.Where(c => c.Key.Item1 == d.Item1 && c.Key.Item2 < d.Item2)).Select(c => c.Value).ToList();

    var result = t1.Union(t2).Union(t3).SelectMany(c => c).GroupBy(c => c).Where(c => c.Count() == 3).First().Key;

    Console.WriteLine($"Level2 Aunt Sue: {result}");

}