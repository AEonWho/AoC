using System.Collections.Concurrent;

var input = File.ReadAllLines("Input.txt");

var template = input[0];

Dictionary<string, string> polymerToElementMap = input.Skip(2).Select(c => c.Split(" -> ")).ToDictionary(d => d[0], d => d[1]);

ConcurrentDictionary<string, long> polymerCount = new ConcurrentDictionary<string, long>();
ConcurrentDictionary<string, long> elmentCount = new ConcurrentDictionary<string, long>();
template.GroupBy(d => d).ToList().ForEach(c => elmentCount.TryAdd(c.Key.ToString(), c.Count()));

for (int i = 1; i < template.Length; i++)
{
    var idxStart = i - 1;
    var idxEnd = i + 1;
    polymerCount.AddOrUpdate(template[idxStart..idxEnd], 1, (str, i) => i + 1);
}

for (int i = 0; i < 40; i++)
{
    ConcurrentDictionary<string, long> newPolymerCount = new ConcurrentDictionary<string, long>();
    foreach (var pair in polymerCount.Select(c => new { Key = c.Key, Value = c.Value }).ToList())
    {
        var first = pair.Key[0] + polymerToElementMap[pair.Key];
        var second = polymerToElementMap[pair.Key] + pair.Key[1];

        newPolymerCount.AddOrUpdate(first, pair.Value, (str, i) => i + pair.Value);
        newPolymerCount.AddOrUpdate(second, pair.Value, (str, i) => i + pair.Value);
        elmentCount.AddOrUpdate(polymerToElementMap[pair.Key], pair.Value, (str, i) => i + pair.Value);
    }
    polymerCount = newPolymerCount;

    var max = elmentCount.Max(d => d.Value);
    var min = elmentCount.Min(d => d.Value);

    Console.WriteLine($"Result a step {i+1}: {max - min}");
}
