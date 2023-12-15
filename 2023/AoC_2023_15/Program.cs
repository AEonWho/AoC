using System.Collections.Concurrent;

var text = File.ReadAllText("Input.txt");

ConcurrentDictionary<int, int> lookups = new ConcurrentDictionary<int, int>();
List<int> tmp = new List<int>();

var splitted = text.Split(",");
foreach (var entry in splitted)
{
    tmp.Add(GenerateHash(entry));
}

Console.WriteLine("Level1: " + tmp.Sum());

ConcurrentDictionary<int, List<(string, int)>> boxes = new ConcurrentDictionary<int, List<(string, int)>>();

ConcurrentDictionary<string, int> BoxLookup = new ConcurrentDictionary<string, int>();
foreach (var entry in splitted)
{
    var split = entry.Split(["=", "-"], StringSplitOptions.RemoveEmptyEntries);
    var boxId = BoxLookup.GetOrAdd(split[0], GenerateHash);

    var list = boxes.GetOrAdd(boxId, _ => new List<(string, int)>());

    if (split.Length == 1)
    {
        list.RemoveAll(d => d.Item1 == split[0]);
    }
    else
    {
        var existing = list.FindIndex(d => d.Item1 == split[0]);

        if (existing == -1)
        {
            list.Add((split[0], int.Parse(split[1])));
        }
        else
        {
            list[existing] = ((split[0], int.Parse(split[1])));
        }

    }
}


var sum = 0;
foreach (var box in boxes)
{
    for (int i = 0; i < box.Value.Count; i++)
    {
        sum += (box.Key + 1) * (i + 1) * box.Value[i].Item2;
    }
}

Console.WriteLine("Level2: " + sum);

int GenerateHash(string entry)
{
    int value = 0;

    foreach (var c in entry)
    {
        value = lookups.GetOrAdd(value + c, val => val * 17 % 256);
    }

    return value;
}