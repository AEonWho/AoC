var lines = File.ReadAllLines("Input.txt");

List<(int X, int Y)> southFacing = new List<(int X, int Y)>();
List<(int X, int Y)> eastFacing = new List<(int X, int Y)>();

var sizeX = lines[0].Length;
var sizeY = lines.Length;

for (int y = 0; y < sizeY; y++)
{
    for (int x = 0; x < sizeX; x++)
    {
        switch (lines[y][x])
        {
            case '>':
                eastFacing.Add((x, y));
                break;

            case 'v':
                southFacing.Add((x, y));
                break;
        }
    }
}

int step = 0;
bool moved = true;
while (moved)
{
    moved = false;
    List<(int X, int Y)> newSouthFacing = new List<(int X, int Y)>();
    List<(int X, int Y)> newEastFacing = new List<(int X, int Y)>();

    foreach (var ef in eastFacing)
    {
        var newX = ef.X + 1;
        if (newX >= sizeX)
            newX = 0;

        var newKey = (newX, ef.Y);
        if (!eastFacing.Contains(newKey) && !southFacing.Contains(newKey))
        {
            moved = true;
            newEastFacing.Add(newKey);
        }
        else
        {
            newEastFacing.Add(ef);
        }
    }

    foreach (var sf in southFacing)
    {
        var newY = sf.Y + 1;
        if (newY >= sizeY)
            newY = 0;

        var newKey = (sf.X, newY);
        if (!newEastFacing.Contains(newKey) && !southFacing.Contains(newKey))
        {
            moved = true;
            newSouthFacing.Add(newKey);
        }
        else
        {
            newSouthFacing.Add(sf);
        }
    }

    southFacing = newSouthFacing;
    eastFacing = newEastFacing;

    step++;
}

Console.WriteLine("Stage 1: " + step);