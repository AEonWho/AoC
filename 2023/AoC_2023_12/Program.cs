var input = File.ReadAllLines("Input.txt");

{
    var data = new List<HotSpringLine>();
    foreach (var line in input)
    {
        var splitted = line.Split([" ", ","], StringSplitOptions.RemoveEmptyEntries);
        var template = splitted[0];
        var sizes = splitted.Skip(1).Select(int.Parse);

        data.Add(new HotSpringLine(template, sizes));
    }

    Console.WriteLine("Level1: " + data.Sum(d => d.PossibleConfigurations));
}

{
    var data = new List<HotSpringLine>();

    foreach (var line in input)
    {
        var splitted = line.Split([" ", ","], StringSplitOptions.RemoveEmptyEntries);
        var template = splitted[0] + "?" + splitted[0] + "?" + splitted[0] + "?" + splitted[0] + "?" + splitted[0];
        var sizes = splitted.Skip(1).Select(int.Parse);

        data.Add(new HotSpringLine(template, [.. sizes, .. sizes, .. sizes, .. sizes, .. sizes]));
    }

    Console.WriteLine("Level2: " + data.Sum(d => d.PossibleConfigurations));
}