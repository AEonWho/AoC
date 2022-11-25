var lines = File.ReadAllLines("Input.txt");

var Cubes = new List<Cube>();

foreach (var line in lines)
{
    var data = line.Split(new string[] { " x=", ",y=", ",z=", ".." }, StringSplitOptions.RemoveEmptyEntries);

    var num = data.Skip(1).Select(int.Parse).ToArray();

    Cubes.Add(new Cube(num[0], num[1], num[2], num[3], num[4], num[5], data[0] == "on"));
}

var xValuesEnds = Cubes.SelectMany(c => new[] { c.X1 - 1, c.X2 }).Distinct().OrderBy(c => c).ToArray();
var yValuesEnds = Cubes.SelectMany(c => new[] { c.Y1 - 1, c.Y2 }).Distinct().OrderBy(c => c).ToArray();
var zValuesEnds = Cubes.SelectMany(c => new[] { c.Z1 - 1, c.Z2 }).Distinct().OrderBy(c => c).ToArray();

HashSet<((int, int) X, (int, int) Y, (int, int) Z)> areas = new HashSet<((int, int), (int, int), (int, int))>();

int i = 0;
foreach (var cube in Cubes)
{
    i++;
    List<(int, int)> xValues = new List<(int, int)>();
    List<(int, int)> yValues = new List<(int, int)>();
    List<(int, int)> zValues = new List<(int, int)>();

    splitCube(xValuesEnds, cube.X1, cube.X2, xValues);
    splitCube(yValuesEnds, cube.Y1, cube.Y2, yValues);
    splitCube(zValuesEnds, cube.Z1, cube.Z2, zValues);

    foreach (var xEntry in xValues)
    {
        foreach (var yEntry in yValues)
        {
            foreach (var zEntry in zValues)
            {
                if (cube.State)
                {
                    areas.Add((xEntry, yEntry, zEntry));
                }
                else
                {
                    areas.Remove((xEntry, yEntry, zEntry));
                }
            }
        }
    }

    if(i==20)
    {
        var volumeStage1 = areas.Sum(GetVolume);

        Console.WriteLine("Stage1: "+volumeStage1);
    }
}

var volumeStage2 = areas.Sum(GetVolume);

Console.WriteLine("Stage2: " + volumeStage2);

long GetVolume(((int, int) X, (int, int) Y, (int, int) Z) arg)
{
    var bla = Math.Abs((long)arg.X.Item2 - arg.X.Item1) + 1;
    var bla1 = Math.Abs((long)arg.Y.Item2 - arg.Y.Item1) + 1;
    var bla2 = Math.Abs((long)arg.Z.Item2 - arg.Z.Item1) + 1;

    return bla * bla1 * bla2;
}

Console.WriteLine();

void splitCube(int[] xValuesEnds, int x1, int x2, List<(int, int)> xValues)
{
    var startX = x1;
    while (startX <= x2)
    {
        var endX = xValuesEnds.First(d => d >= startX);
        xValues.Add((startX, endX));
        startX = endX + 1;
    }
}

class Cube
{
    public Cube(int x1, int x2, int y1, int y2, int z1, int z2, bool state)
    {
        X1 = x1;
        X2 = x2;
        Y1 = y1;
        Y2 = y2;
        Z1 = z1;
        Z2 = z2;
        State = state;
    }

    public bool State { get; private set; }

    public int X1 { get; private set; }
    public int X2 { get; private set; }
    public int Y1 { get; private set; }
    public int Y2 { get; private set; }
    public int Z1 { get; private set; }
    public int Z2 { get; private set; }
}