var lines = File.ReadAllLines("Input.txt");

var points = new HashSet<(int X, int Y)>();

List<(string Axis, int Value)> folds = new List<(string, int)>();

foreach (var line in lines)
{
    var splitted = line.Split(',', '=');

    if (line.Contains(","))
    {
        var x = int.Parse(splitted[0]);
        var y = int.Parse(splitted[1]);

        points.Add((x, y));
    }
    else if (line.Contains("="))
    {
        if (splitted[0][^1] == 'x')
        {
            folds.Add(("x", int.Parse(splitted[1])));
        }
        else
        {
            folds.Add(("y", int.Parse(splitted[1])));
        }
    }
}

Console.WriteLine($"Points before Fold: {points.Count}");
int i = 0;
foreach (var fold in folds)
{
    i++;
    if (fold.Axis == "x")
    {
        var newPoints = points.Where(d => d.X <= fold.Value).ToHashSet();

        foreach (var foldedPoint in points.Where(d => d.X > fold.Value))
        {
            var mirrorPoint = (fold.Value * 2 - foldedPoint.X, foldedPoint.Y);
            newPoints.Add(mirrorPoint);
        }

        points = newPoints;
    }
    else
    {
        var newPoints = points.Where(d => d.Y <= fold.Value).ToHashSet();

        foreach (var foldedPoint in points.Where(d => d.Y > fold.Value))
        {
            var mirrorPoint = (foldedPoint.X, fold.Value * 2 - foldedPoint.Y);
            newPoints.Add(mirrorPoint);
        }

        points = newPoints;
    }

    Console.WriteLine($"Points on Fold {i}: {points.Count}");
}

for (int y = 0; y <= points.Select(c=>c.Y).Max(); y++)
{
    for (int x = 0; x <= points.Select(c => c.X).Max(); x++)
    {
        Console.Write(points.Contains((x, y)) ? "#" : ".");
    }
    Console.WriteLine();
}
Console.WriteLine();