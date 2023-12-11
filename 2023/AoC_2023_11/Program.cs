using System.Collections.Generic;

var input = File.ReadAllLines("Input.txt");

var GalaxiesLevel1 = new List<(long X, long Y)>();
var GalaxiesLevel2 = new List<(long X, long Y)>();

var offsetYLevel1 = 0;
var offsetYLevel2 = 0;

for (int y = 0; y < input.Length; y++)
{
    var line = input[y];

    if (line.All(d => d == '.'))
    {
        offsetYLevel1++;
        offsetYLevel2 += 999999;
    }

    var offsetXLevel1 = 0;
    var offsetXLevel2 = 0;
    for (int x = 0; x < line.Length; x++)
    {
        if (input.Select(d => d[x]).All(d => d == '.'))
        {
            offsetXLevel1++;
            offsetXLevel2 += 999999;
        }

        if (line[x] == '#')
        {
            GalaxiesLevel1.Add((x + offsetXLevel1, y + offsetYLevel1));
            GalaxiesLevel2.Add((x + offsetXLevel2, y + offsetYLevel2));
        }
    }
}

long Level1Res = 0;
long Level2Res = 0;
for (int i = 0; i < GalaxiesLevel1.Count - 1; i++)
{
    for (int i2 = i + 1; i2 < GalaxiesLevel1.Count; i2++)
    {
        var distanceXLevel1 = Math.Abs(GalaxiesLevel1[i].X - GalaxiesLevel1[i2].X);
        var distanceYLevel1 = Math.Abs(GalaxiesLevel1[i].Y - GalaxiesLevel1[i2].Y);

        var distanceXLevel2 = Math.Abs(GalaxiesLevel2[i].X - GalaxiesLevel2[i2].X);
        var distanceYLevel2 = Math.Abs(GalaxiesLevel2[i].Y - GalaxiesLevel2[i2].Y);

        Level1Res += distanceXLevel1 + distanceYLevel1;
        Level2Res += distanceXLevel2 + distanceYLevel2;
    }
}

Console.WriteLine("Level1: " + Level1Res);
Console.WriteLine("Level1: " + Level2Res);