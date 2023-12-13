var input = File.ReadAllLines("Input.txt");

List<Map> _maps = [new Map()];
{
    var y = 0;
    foreach (var line in input)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            y = 0;
            _maps.Add(new Map());
            continue;
        }
        for (var x = 0; x < line.Length; x++)
        {
            _maps[^1].Add((x, y), line[x]);
        }
        y++;
    }
}

int sumLevel1 = 0;
int sumLevel2 = 0;
foreach (var map in _maps)
{
    int XSplitLevel1 = -1;
    int XSplitLevel2 = -1;
    for (int i = 0; i < map.X_Max - 1; i++)
    {
        int errorCount = 0;
        var iFirst = i;
        var iSecond = i + 1;
        while (iFirst >= 0 && iSecond < map.X_Max)
        {
            for (int y = 0; y < map.Y_Max; y++)
            {
                if (map.Data[(iFirst, y)] != map.Data[(iSecond, y)])
                {
                    errorCount++;
                }
            }

            iFirst--;
            iSecond++;
        }

        if (errorCount == 0)
        {
            XSplitLevel1 = i;
        }

        if (errorCount == 1)
        {
            XSplitLevel2 = i;
        }
    }

    if (XSplitLevel1 != -1)
    {
        sumLevel1 += (XSplitLevel1 + 1);
    }

    if (XSplitLevel2 != -1)
    {
        sumLevel2 += (XSplitLevel2 + 1);
    }

    int YSplitLevel1 = -1;
    int YSplitLevel2 = -1;
    for (int i = 0; i < map.Y_Max - 1; i++)
    {
        int errorCount = 0;
        var iFirst = i;
        var iSecond = i + 1;
        while (iFirst >= 0 && iSecond < map.Y_Max)
        {
            for (int x = 0; x < map.X_Max; x++)
            {
                if (map.Data[(x, iFirst)] != map.Data[(x, iSecond)])
                {
                    errorCount++;
                }
            }

            iFirst--;
            iSecond++;
        }

        if (errorCount == 0)
        {
            YSplitLevel1 = i;
        }

        if (errorCount == 1)
        {
            YSplitLevel2 = i;
        }
    }

    if (YSplitLevel1 != -1)
    {
        sumLevel1 += 100 * (YSplitLevel1 + 1);
    }

    if (YSplitLevel2 != -1)
    {
        sumLevel2 += 100 * (YSplitLevel2 + 1);
    }
}

Console.WriteLine("Level1: " + sumLevel1);
Console.WriteLine("Level2: " + sumLevel2);