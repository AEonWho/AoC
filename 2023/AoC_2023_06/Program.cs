var input = File.ReadAllLines("Input.txt");

{
    var times = input[0].Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
    var distance = input[1].Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();

    var sum = new List<int>();
    for (var t = 0; t < times.Count; t++)
    {

        var wins = 0;
        var time = times[t];
        var checkDistance = distance[t];
        for (int i = 1; i < time; i++)
        {
            var remaining = time - i;

            var travel = i * remaining;
            if (travel > checkDistance)
                wins++;
        }
        sum.Add(wins);
    }

    Console.WriteLine("Level1: " + sum.Aggregate((f, s) => f * s));
}

{
    var time = long.Parse(string.Join("", input[0].Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1)));
    var checkDistance = long.Parse(string.Join("", input[1].Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1)));

    var wins = 0;
    for (int i = 1; i < time; i++)
    {
        var remaining = time - i;

        var travel = i * remaining;
        if (travel > checkDistance)
            wins++;
    }

    Console.WriteLine("Level2: " + wins);
}