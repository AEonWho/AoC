var lines = File.ReadAllLines("Input.txt").ToList();

var data = lines.Select(c => c.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToList();

var valid = 0;
foreach (var line in data)
{
    if (CheckValidLine(line))
        valid++;
}

Console.WriteLine("Level1: " + valid);


valid = 0;
foreach (var line in data)
{
    if (CheckValidLine(line))
    {
        valid++;
    }
    else
    {
        for (int i = 0; i < line.Length; i++)
        {
            var newLine = line.Take(i).Concat(line.Skip(i + 1)).ToArray();
            if (CheckValidLine(newLine))
            {
                valid++;
                break;
            }
        }
    }
}

Console.WriteLine("Level2: " + valid);

static bool CheckValidLine(int[] line)
{
    var previous = line[0];
    bool increase = line[0] < line[1];
    foreach (var entry in line.Skip(1))
    {
        var diff = 0;
        if (increase)
        {
            diff = entry - previous;
        }
        else
        {
            diff = previous - entry;
        }

        if (diff < 1 || diff > 3)
        {
            return false;
        }

        previous = entry;
    }
    return true;
}