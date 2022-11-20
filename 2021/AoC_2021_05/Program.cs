using System.Collections.Concurrent;

var lines = File.ReadAllLines("Input.txt");

ConcurrentDictionary<(int x, int y), int> coordinatesStage1 = new ConcurrentDictionary<(int x, int y), int>();
ConcurrentDictionary<(int x, int y), int> coordinatesStage2 = new ConcurrentDictionary<(int x, int y), int>();

foreach (var line in lines)
{
    var splitted = line.Split(new string[] { ",", " -> " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    var x1 = splitted[0];
    var y1 = splitted[1];
    var x2 = splitted[2];
    var y2 = splitted[3];


    if (x1 == x2)
    {
        coordinatesStage1.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        coordinatesStage2.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        while (y1 != y2)
        {
            if (y1 < y2)
            {
                y1++;
            }
            else
            {
                y1--;
            }

            coordinatesStage1.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
            coordinatesStage2.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        }
    }
    else if (y1 == y2)
    {
        coordinatesStage1.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        coordinatesStage2.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        while (x1 != x2)
        {
            if (x1 < x2)
            {
                x1++;
            }
            else
            {
                x1--;
            }

            coordinatesStage1.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
            coordinatesStage2.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        }
    }
    else
    {
        coordinatesStage2.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        while (x1 != x2)
        {
            if (x1 < x2)
            {
                x1++;
            }
            else
            {
                x1--;
            }

            if (y1 < y2)
            {
                y1++;
            }
            else
            {
                y1--;
            }

            coordinatesStage2.AddOrUpdate((x1, y1), 1, (d, e) => e + 1);
        }
    }
}

var count1 = coordinatesStage1.Count(c => c.Value > 1);
var count2 = coordinatesStage2.Count(c => c.Value > 1);

Console.WriteLine(count1);
Console.WriteLine(count2);