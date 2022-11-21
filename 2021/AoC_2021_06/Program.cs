Dictionary<int, long> states = CreateEmptyDict();

File.ReadAllText("Input.txt").Split(',').Select(int.Parse).GroupBy(d => d).ToList().ForEach(d => states[d.Key] = d.Count());


for (int i = 1; i <= 256; i++)
{
    states = Advance(states);

    if (i == 80 || i == 256)
    {
        var sum = states.Select(c => c.Value).Sum();

        Console.WriteLine($"Iteration {i}:{sum}");
    }
}


Dictionary<int, long> Advance(Dictionary<int, long> states)
{
    Dictionary<int, long> dict = CreateEmptyDict();
    foreach (var key in states.Keys)
    {
        if (key == 0)
        {
            dict[8] += states[key];
            dict[6] += states[key];
        }
        else
        {
            dict[key - 1] += states[key];
        }
    }

    return dict;
}

static Dictionary<int, long> CreateEmptyDict()
{
    return new Dictionary<int, long>
{
    {0,0 }, {1,0}, {2,0}, {3,0}, {4,0}, {5,0}, {6,0}, {7,0}, {8,0}
};
}