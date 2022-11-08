using System.Text;

internal class FloorMap
{
    public char[] Logic { get; }

    public char DefaultChar { get; }

    public char[,] Data { get; }

    private HashSet<(int x, int y)> _points = new HashSet<(int x, int y)>();

    public FloorMap(string[] data, char[] logic)
    {
        this.Logic = logic;
        DefaultChar = logic[0];
        Data = new char[data.Length, data[0].Length];

        for (int row = 0; row < Data.GetLength(0); row++)
        {
            for (int column = 0; column < Data.GetLength(1); column++)
            {
                Data[column, row] = data[row][column];
                if (data[row][column] == '#')
                {
                    _points.Add((column, row));
                }
            }
        }
    }

    public FloorMap(char[,] data, char[] logic, char defaultChar)
    {
        this.Logic = logic;
        DefaultChar = defaultChar;
        Data = data;

        for (int x = 0; x < Data.GetLength(0); x++)
        {
            for (int y = 0; y < Data.GetLength(1); y++)
            {
                if (Data[x, y] == '#')
                {
                    _points.Add((x, y));
                }
            }
        }
    }

    public FloorMap NextIteration()
    {
        var newSizeX = Data.GetLength(0) + 2;
        var newSizeY = Data.GetLength(1) + 2;

        var newData = new char[newSizeX, newSizeY];

        for (int x = 0; x < newSizeX; x++)
        {
            for (int y = 0; y < newSizeY; y++)
            {
                newData[x, y] = GetNextValueForPosition(x - 1, y - 1);
            }
        }

        var nextDefaultChar = DefaultChar;
        if (nextDefaultChar == '.' && Logic[0] == '#')
        {
            nextDefaultChar = '#';
        }
        else if (nextDefaultChar == '#' && Logic[511] == '.')
        {
            nextDefaultChar = '.';
        }

        return new FloorMap(newData, Logic, nextDefaultChar);
    }

    private char GetNextValueForPosition(int x, int y)
    {
        var idx = 0;
        idx += IsHighlight((x + 1, y + 1)) ? 1 : 0;
        idx += IsHighlight((x + 0, y + 1)) ? 2 : 0;
        idx += IsHighlight((x - 1, y + 1)) ? 4 : 0;
        idx += IsHighlight((x + 1, y + 0)) ? 8 : 0;
        idx += IsHighlight((x + 0, y + 0)) ? 16 : 0;
        idx += IsHighlight((x - 1, y + 0)) ? 32 : 0;
        idx += IsHighlight((x + 1, y - 1)) ? 64 : 0;
        idx += IsHighlight((x + 0, y - 1)) ? 128 : 0;
        idx += IsHighlight((x - 1, y - 1)) ? 256 : 0;

        return Logic[idx];
    }

    private bool IsHighlight((int, int) value)
    {
        if (_points.Contains(value))
            return true;

        if (value.Item1 < 0 || value.Item2 < 0)
            return DefaultChar != Logic[0];

        if (value.Item1 >= Data.GetLength(0) || value.Item2 >= Data.GetLength(1))
            return DefaultChar != Logic[0];

        return false;
    }

    internal string GetString()
    {
        StringBuilder sb = new StringBuilder();

        for (int y = 0; y < Data.GetLength(1); y++)
        {
            if (y != 0)
                sb.AppendLine();

            for (int x = 0; x < Data.GetLength(0); x++)
            {
                if (x != 0)
                    sb.Append(' ');
                sb.Append(_points.Contains((x, y)) ? '#' : '.');
            }
        }

        return sb.ToString();
    }
}