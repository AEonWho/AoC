
using AoC_Common.PathFinding;

internal class Map
{
    public long X_Max { get; set; }

    public long Y_Max { get; set; }

    public Dictionary<(long X, long Y), char> Data { get; set; } = new Dictionary<(long, long), char>();

    internal void Add((int x, int y) point, char v)
    {
        if (point.x + 1 > X_Max)
            X_Max = point.x + 1;
        if (point.y + 1 > Y_Max)
            Y_Max = point.y + 1;

        Data[point] = v;
    }

    public void Print()
    {
        for (int y = 0; y < Y_Max; y++)
        {
            for (int x = 0; x < X_Max; x++)
            {
                Console.Write(Data[(x, y)]);
            }

            Console.WriteLine();
        }
    }
}