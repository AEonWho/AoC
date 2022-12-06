var line = File.ReadAllText("Input.txt");

for (int i = 4; i < line.Length; i++)
{
    var start = i - 4;
    var end = i;

    var cnt = line[start..end].Distinct().Count();

    if (cnt == 4)
    {
        Console.WriteLine("Stage1: " + i);
        break;
    }
}

for (int i = 14; i < line.Length; i++)
{
    var start = i - 14;
    var end = i;

    var cnt = line[start..end].Distinct().Count();

    if (cnt == 14)
    {
        Console.WriteLine("Stage2: " + i);
        break;
    }
}