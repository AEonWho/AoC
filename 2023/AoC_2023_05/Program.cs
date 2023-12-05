using System.Diagnostics;

var input = File.ReadAllLines("Input.txt");

var Maps = new Dictionary<string, (string Target, List<(long startRange, long endRange, long offset, bool? tmp)>)>();
var MapsReverse = new Dictionary<string, (string Target, List<(long startRange, long endRange, long offset, bool? tmp)>)>();

var seeds = input[0].Split(' ').Skip(1).Select(long.Parse).ToList();

var currentEntry = "";
var currentTarget = "";
for (int i = 2; i < input.Length; i++)
{
    var line = input[i];
    var splitted = line.Split(new[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);

    if (splitted.Length == 0)
        continue;
    if (splitted[^1] == "map:")
    {
        currentEntry = splitted[0];
        currentTarget = splitted[2];
        continue;
    }

    var parsed = splitted.Select(long.Parse).ToList();
    var cnt = parsed[2];
    var start = parsed[0];
    var mapped = parsed[1];

    if (!Maps.ContainsKey(currentEntry))
    {
        Maps.Add(currentEntry, (currentTarget, new List<(long startRange, long endRange, long offset, bool? tmp)>()));
        MapsReverse.Add(currentTarget, (currentEntry, new List<(long startRange, long endRange, long offset, bool? tmp)>()));
    }

    var tmp = Maps[currentEntry].Item2;
    var tmpReverse = MapsReverse[currentTarget].Item2;

    tmp.Add((mapped, mapped + cnt, start - mapped, true));
    tmpReverse.Add((start, start + cnt, mapped - start, true));
}

var sw = new Stopwatch();
sw.Start();
{
    var values = new List<long>();
    foreach (var s in seeds)
    {
        var value = s;

        var start = "seed";
        while (start != "location")
        {
            var entry = Maps[start];
            start = entry.Item1;

            var data = entry.Item2.FirstOrDefault(d => d.startRange <= value && d.endRange > value);
            if (data.tmp != null)
                value = value + data.offset;

        }

        values.Add(value);
    }

    Console.WriteLine("Level1: " + values.Min());
}
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds + " ms");

sw.Restart();
{
    var ranges = new List<(long, long)>();
    for (int si = 0; si < seeds.Count; si = si + 2)
    {
        ranges.Add((seeds[si], seeds[si] + seeds[si + 1]));
    }

    for (long l = 1; l < long.MaxValue; l++)
    {
        var value = l;

        bool found = false;
        var start = "location";
        while (start != "seed")
        {
            var entry = MapsReverse[start];
            start = entry.Item1;

            var data = entry.Item2.FirstOrDefault(d => d.startRange <= value && d.endRange > value);
            if (data.tmp != null)
            {
                value = value + data.offset;
                found = true;
            }
            else
            {
                found = false;
            }
        }

        if (found && ranges.Any(c => c.Item1 <= value && c.Item2 > value))
        {
            Console.WriteLine("Level2: " + l);
            break;
        }
    }
}
sw.Stop();
Console.WriteLine(sw.ElapsedMilliseconds + " ms");