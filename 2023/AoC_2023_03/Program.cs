// See https://aka.ms/new-console-template for more information

var lines = File.ReadAllLines("input.txt");


HashSet<PartNumber> partnumbers = new HashSet<PartNumber>();
HashSet<(int, int)> symbols = new HashSet<(int, int)>();
HashSet<(int, int)> gears = new HashSet<(int, int)>();
for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
{
    PartNumber? currentPartNumber = null;
    for (int charNumber = 0; charNumber < lines[lineNumber].Length; charNumber++)
    {
        var character = lines[lineNumber][charNumber];
        var nmb = character - 48;
        if (nmb >= 0 && nmb <= 9)
        {
            currentPartNumber ??= new PartNumber(lines[lineNumber].Length - 1, lines.Length - 1);
            partnumbers.Add(currentPartNumber);
            currentPartNumber.Add(nmb, (charNumber, lineNumber));
        }
        else
        {
            currentPartNumber = null;
            if (character != '.')
            {
                symbols.Add((charNumber, lineNumber));
                if (character == '*')
                {
                    gears.Add((charNumber, lineNumber));
                }
            }
        }
    }
}

var Level1 = partnumbers.Where(d => d.Neighbors.Any(symbols.Contains)).Sum(c => c.Value);
Console.WriteLine("Level1: " + Level1);

var level2List = gears.Select(c => partnumbers.Where(d => d.Neighbors.Contains(c)).ToList()).Where(c => c.Count() > 1).ToList();
var level2 = level2List.Select(c => c.Aggregate<PartNumber, int>(1, (first, second) => first * second.Value));
Console.WriteLine("Level2: " + level2.Sum());