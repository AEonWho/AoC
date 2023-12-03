// See https://aka.ms/new-console-template for more information

internal class PartNumber
{
    public PartNumber(int xMax, int yMax)
    {
        XMax = xMax;
        YMax = yMax;
    }

    public int Value { get; set; }

    public List<(int x, int y)> Coords { get; set; } = new List<(int x, int y)>();

    public IEnumerable<(int x, int y)> Neighbors => GetNeighbors();

    public int XMax { get; }
    public int YMax { get; }

    internal void Add(int nmb, (int charNumber, int lineNumber) value)
    {
        Value = Value * 10 + nmb;
        Coords.Add(value);
    }

    private IEnumerable<(int x, int y)> GetNeighbors()
    {
        HashSet<(int x, int y)> neighbors = new HashSet<(int, int)>();

        foreach (var coord in Coords)
        {
            neighbors.Add((coord.x + 1, coord.y));
            neighbors.Add((coord.x + 1, coord.y + 1));
            neighbors.Add((coord.x + 1, coord.y - 1));
            neighbors.Add((coord.x, coord.y - 1));
            neighbors.Add((coord.x, coord.y + 1));
            neighbors.Add((coord.x - 1, coord.y));
            neighbors.Add((coord.x - 1, coord.y + 1));
            neighbors.Add((coord.x - 1, coord.y - 1));
        }

        return neighbors.Except(Coords).Where(c => c.x >= 0 && c.y >= 0 && c.x <= XMax && c.y <= YMax).ToList();
    }
}