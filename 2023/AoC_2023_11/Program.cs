var input = File.ReadAllLines("Input.txt");

List<(long X, long Y)> GalaxiesLevel1 = ObserveGalaxies(input, 2).ToList();
List<(long X, long Y)> GalaxiesLevel2 = ObserveGalaxies(input, 1_000_000).ToList();

Console.WriteLine("Level1: " + GetResult(GalaxiesLevel1));
Console.WriteLine("Level2: " + GetResult(GalaxiesLevel2));

static IEnumerable<(long X, long Y)> ObserveGalaxies(string[] input, int voidLength)
{
    var voidY = Enumerable.Range(0, input.Length).Where(d => input[d].All(c => c == '.')).ToList();
    var voidX = Enumerable.Range(0, input[0].Length).Where(d => input.Select(c => c[d]).All(c => c == '.')).ToList();

    var offsetY = 0;
    for (int y = 0; y < input.Length; y++)
    {
        var line = input[y];

        if (voidY.Contains(y))
            offsetY += voidLength - 1;

        var offsetX = 0;
        for (int x = 0; x < line.Length; x++)
        {
            if (voidX.Contains(x))
                offsetX += voidLength-1;

            if (line[x] == '#')
                yield return (x + offsetX, y + offsetY);
        }
    }
}

static long GetResult(List<(long X, long Y)> galaxies)
{
    long result = 0;
    for (int i = 0; i < galaxies.Count - 1; i++)
    {
        for (int i2 = i + 1; i2 < galaxies.Count; i2++)
        {
            var distanceX = Math.Abs(galaxies[i].X - galaxies[i2].X);
            var distanceY = Math.Abs(galaxies[i].Y - galaxies[i2].Y);

            result += distanceX + distanceY;
        }
    }

    return result;
}