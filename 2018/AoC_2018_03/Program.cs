using System.Linq;

var lines = File.ReadAllLines("Input.txt").ToList();

Dictionary<(int, int), List<string>> claims = new Dictionary<(int, int), List<string>>();
List<string> claimIds = new List<string>();
for (int i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    var splitted = line.Split(' ', ',', ':', 'x');

    claimIds.Add(splitted[0]);

    var xStart = int.Parse(splitted[2]);
    var yStart = int.Parse(splitted[3]);

    var lenX = int.Parse(splitted[^2]);
    var lenY = int.Parse(splitted[^1]);

    for (int y = 0; y < lenY; y++)
    {
        for (int x = 0; x < lenX; x++)
        {
            var key = (x + xStart, y + yStart);
            if (!claims.ContainsKey(key))
                claims.Add(key, new List<string>());

            claims[key].Add(splitted[0]);
        }
    }
}

var count = claims.Count(d => d.Value.Count > 1);

Console.WriteLine("Stage1: " + count);

var search = claimIds.Where(d => claims.Where(c => c.Value.Contains(d)).All(c => c.Value.Count == 1)).ToList();

Console.WriteLine("Stage2: " + search.First());