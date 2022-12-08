using static System.Formats.Asn1.AsnWriter;

var lines = File.ReadAllLines("Input.txt").ToList();

var maxX = lines[0].Length;
var maxY = lines.Count;

Dictionary<(int x, int y), int> Map = new Dictionary<(int x, int y), int>();

for (int y = 0; y < lines.Count; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {

        var val = int.Parse(lines[y][x].ToString());

        Map.Add((x, y), val);
    }
}

HashSet<(int x, int y)> visiblePositions = new HashSet<(int x, int y)>();
int maxScore = 0;
foreach (var k in Map.Keys)
{
    if (k.y == 0 || k.x == 0 || k.x == maxX - 1 || k.y == maxY - 1)
    {
        visiblePositions.Add(k);
        continue;
    }

    List<IEnumerable<(int x, int y)>> testList = new List<IEnumerable<(int x, int y)>>
    {
        Enumerable.Range(0, k.x).Reverse().Select(d => (d, k.y)),
        Enumerable.Range(0, k.y).Reverse().Select(d => (k.x, d)),
        Enumerable.Range(k.x + 1, maxX - k.x - 1).Select(d => (d, k.y)),
        Enumerable.Range(k.y + 1, maxY - k.y - 1).Select(d => (k.x, d)),
    };

    if (testList.Any(d => d.All(d => Map[d] < Map[k])))
    {
        visiblePositions.Add(k);
    }

    var currentScore = testList.Select(d =>
    {
        var score1 = 0;
        foreach (var entry in d)
        {
            score1++;
            if (Map[entry] >= Map[k])
                break;
        }
        return score1;
    }).Aggregate((f, s) => f * s);
    
    if (maxScore < currentScore)
    {
        maxScore = currentScore;
    }
}

Console.WriteLine("Stage1: " + visiblePositions.Count);
Console.WriteLine("Stage2: " + maxScore);