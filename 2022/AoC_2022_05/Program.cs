var lines = File.ReadAllLines("Input.txt").ToList();

List<List<char>> tmp = new List<List<char>>()
{
    new List<char>(),
    new List<char>(),
    new List<char>(),
    new List<char>(),
    new List<char>(),
    new List<char>(),
    new List<char>(),
    new List<char>(),
    new List<char>(),
};

Stage1(lines, tmp);

foreach (var entry in tmp)
{
    Console.Write(entry[^1]);
}

Console.WriteLine();

tmp.ForEach(d => d.Clear());

Stage2(lines, tmp);

foreach (var entry in tmp)
{
    Console.Write(entry[^1]);
}

static void Stage1(List<string> lines, List<List<char>> tmp)
{
    for (int i = 7; i >= 0; i--)
    {
        var line = lines[i];

        for (int charIdx = 0; charIdx < 9; charIdx++)
        {
            var start = 1 + (charIdx * 4);
            var value = line[start];

            if (value != ' ')
            {
                tmp[charIdx].Add(value);
            }
        }
    }

    for (int i = 10; i < lines.Count; i++)
    {
        var line = lines[i];
        var splitted = line.Split(' ');

        var cnt = int.Parse(splitted[1]);
        var from = int.Parse(splitted[3]) - 1;
        var to = int.Parse(splitted[5]) - 1;

        for (int x = 0; x < cnt; x++)
        {
            var val = tmp[from][^1];
            tmp[from].RemoveAt(tmp[from].Count - 1);
            tmp[to].Add(val);
        }
    }
}

static void Stage2(List<string> lines, List<List<char>> tmp)
{
    for (int i = 7; i >= 0; i--)
    {
        var line = lines[i];

        for (int charIdx = 0; charIdx < 9; charIdx++)
        {
            var start = 1 + (charIdx * 4);
            var value = line[start];

            if (value != ' ')
            {
                tmp[charIdx].Add(value);
            }
        }
    }

    for (int i = 10; i < lines.Count; i++)
    {
        var line = lines[i];
        var splitted = line.Split(' ');

        var cnt = int.Parse(splitted[1]);
        var from = int.Parse(splitted[3]) - 1;
        var to = int.Parse(splitted[5]) - 1;

        var tmpStack = new List<char>();
        for (int x = 0; x < cnt; x++)
        {
            var val = tmp[from][^1];
            tmp[from].RemoveAt(tmp[from].Count - 1);
            tmpStack.Add(val);
        }
        tmpStack.Reverse();
        tmp[to].AddRange(tmpStack);
    }
}