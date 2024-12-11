var input = File.ReadAllText("Input.txt");

var stones = new ConcurrentDictionary<ulong, ulong>(input.Split(" ").Select(ulong.Parse).ToDictionary(d => d, d => (ulong)1));

for (int i = 0; i < 75; i++)
{
    var newDict = new ConcurrentDictionary<ulong, ulong>();
    foreach ((var stone, var bla) in stones.SelectMany(stone => OperateStone(stone.Key).Select(bla => (stone, bla))))
    {
        newDict.AddOrUpdate(bla, stone.Value, (d, e) => e + stone.Value);
    }
    stones = newDict;

    if (i == 24)
        Console.WriteLine("Level 1: " + stones.Select(c => c.Value).Aggregate((f, s) => f + s));
}

Console.WriteLine("Level 2: " + stones.Select(c => c.Value).Aggregate((f, s) => f + s));

ulong[] OperateStone(ulong stone)
{
    string str = stone.ToString();
    if (stone == 0)
    {
        return [1];
    }
    else if (str.Length % 2 == 0)
    {
        int half = (str.Length / 2);
        return [ulong.Parse(str[..half]), ulong.Parse(str[half..])];
    }
    else
    {
        return [stone * 2024];
    }
}