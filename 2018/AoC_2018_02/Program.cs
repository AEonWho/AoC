var lines = File.ReadAllLines("Input.txt");

int twoCount = 0;
int threeCount = 0;
foreach (var line in lines)
{
    var data = line.GroupBy(d => d).ToList();
    twoCount += data.Any(d => d.Count() == 2) ? 1 : 0;
    threeCount += data.Any(d => d.Count() == 3) ? 1 : 0;
}

Console.WriteLine("Stage1: " + twoCount * threeCount);

var hashSets = lines.Select(l => l.Select((d, e) => (d, e)).ToHashSet<(char letter, int idx)>()).ToList();

var maxIntersectionCount = 0;
string intersectionText = string.Empty;
for (int i = 0; i < hashSets.Count - 1; i++)
{
    var maxFoundIntersection = hashSets.Skip(i + 1).OrderByDescending(d => d.Intersect(hashSets[i]).Count()).FirstOrDefault();
    var currentintersection = maxFoundIntersection.Intersect(hashSets[i]).ToList();
    if (maxIntersectionCount <= currentintersection.Count)
    {
        maxIntersectionCount = currentintersection.Count;
        intersectionText = new string(currentintersection.OrderBy(d => d.idx).Select(c => c.letter).ToArray());
    }
}

Console.WriteLine("Stage2: " + intersectionText);