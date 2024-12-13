var lines = await File.ReadAllLinesAsync("Input.txt");

List<ClawMachine> machines = new List<ClawMachine>();
for (int i = 0; i < lines.Length; i += 4)
{
    var split = lines[i].Split(['+', ',', ' '], StringSplitOptions.RemoveEmptyEntries);
    var split2 = lines[i + 1].Split(['+', ',', ' '], StringSplitOptions.RemoveEmptyEntries);
    var split3 = lines[i + 2].Split(['=', ',', ' '], StringSplitOptions.RemoveEmptyEntries);


    machines.Add(new ClawMachine(split3[^3], split3[^1], split[^3], split[^1], split2[^3], split2[^1]));
}

long sum = 0;
foreach (var m in machines)
{
    var score = m.SimulateGameLevel1().ToList();

    if (score.Any())
    {
        sum += score.Select(d => d.Item1 * 3 + d.Item2 * 1).OrderBy(d => d).FirstOrDefault();
    }
}

Console.WriteLine("Level 1: " + sum);



sum = 0;
foreach (var m in machines)
{
    m.InitLvl2();

    var score = m.SimulateGameLevel2().ToList();

    if (score.Any())
    {
        sum += score.Select(d => d.Item1 * 3 + d.Item2 * 1).OrderBy(d => d).FirstOrDefault();
    }
}

Console.WriteLine("Level 2: " + sum);