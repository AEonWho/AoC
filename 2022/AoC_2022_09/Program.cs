var lines = File.ReadAllLines("Input.txt").ToList();

Console.WriteLine("Stage1: " + SimulateKnots(lines, 2));

Console.WriteLine("Stage2: " + SimulateKnots(lines, 10));

static int SimulateKnots(List<string> lines, int count)
{
    List<(int X, int Y)> Knots = new List<(int X, int Y)>(count);

    for (int tmp = 0; tmp < Knots.Capacity; tmp++)
    {
        Knots.Add(default);
    }

    HashSet<(int, int)> TailPositions = new HashSet<(int, int)>();

    TailPositions.Add(Knots[^1]);

    for (int i = 0; i < lines.Count; i++)
    {
        var line = lines[i];

        var split = line.Split(' ');

        var value = int.Parse(split[1]);

        for (int t = 0; t < value; t++)
        {
            switch (split[0])
            {
                case "D":
                    Knots[0] = (Knots[0].X, Knots[0].Y - 1);
                    break;
                case "U":
                    Knots[0] = (Knots[0].X, Knots[0].Y + 1);
                    break;
                case "L":
                    Knots[0] = (Knots[0].X - 1, Knots[0].Y);
                    break;
                case "R":
                    Knots[0] = (Knots[0].X + 1, Knots[0].Y);
                    break;
            }

            for (int tmp = 1; tmp < count; tmp++)
            {
                Knots[tmp] = (GetNextKnotPosition(Knots[tmp], Knots[tmp - 1]));
            }

            TailPositions.Add(Knots[^1]);
        }
    }

    return TailPositions.Count;
}

static (int, int) GetNextKnotPosition((int X, int Y) Tail, (int X, int Y) Head)
{
    var diffY = Math.Abs(Tail.Y - Head.Y);
    var diffX = Math.Abs(Tail.X - Head.X);
    if (diffY <= 1 && diffX <= 1)
        return (Tail.X, Tail.Y);

    var newTailY = Tail.Y;
    if (Tail.Y > Head.Y)
    {
        newTailY--;
    }
    else if (Tail.Y < Head.Y)
    {
        newTailY++;
    }

    var newTailX = Tail.X;
    if (Tail.X > Head.X)
    {
        newTailX--;
    }
    else if (Tail.X < Head.X)
    {
        newTailX++;
    }

    return (newTailX, newTailY);
}