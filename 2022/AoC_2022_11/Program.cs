var lines = File.ReadAllLines("Input.txt").ToList();

Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>();

for (int i = 0; i < lines.Count; i += 7)
{
    var monkeyId = int.Parse(lines[i].Split(' ', ':')[1]);

    var Items = lines[i + 1].Split(new char[] { ' ', ':', ',' }, StringSplitOptions.RemoveEmptyEntries).Skip(2).Select(long.Parse).ToList();

    var operation = lines[i + 2].Split('=')[1].Trim();

    var check = long.Parse(lines[i + 3].Split(' ')[^1]);

    var trueCond = int.Parse(lines[i + 4].Split(' ')[^1]);
    var falseCond = int.Parse(lines[i + 5].Split(' ')[^1]);

    monkeys.Add(monkeyId, new Monkey(Items, operation, check, trueCond, falseCond));
}

var kgv = monkeys.Select(c => c.Value.CheckCondition).Aggregate((d, e) => d * e);
monkeys.Values.ToList().ForEach(d => d.Helper = kgv);

int round = 0;
while (true)
{
    round++;
    foreach (var monkey in monkeys.Values)
    {
        foreach (var data in monkey.ThrowItems())
        {
            monkeys[data.monkeyId].Items.Add(data.item);
        }
    }

    if (round == 20)
    {
        var stage1monkeys = monkeys.Select(d => d.Value).OrderByDescending(d => d.CheckCount).Take(2).ToList();

        var stage1Result = stage1monkeys[0].CheckCount * stage1monkeys[1].CheckCount;
        Console.WriteLine("Stage1: " + stage1Result);
        break;
    }
}

monkeys.Values.ToList().ForEach(d => d.ResetForStage2());

round = 0;
while (true)
{
    round++;
    foreach (var monkey in monkeys.Values)
    {
        foreach (var data in monkey.ThrowItems())
        {
            monkeys[data.monkeyId].Items.Add(data.item);
        }
    }

    if (round % 1000 == 0)
    {
        var stage1monkeys = monkeys.Select(d => d.Value).OrderByDescending(d => d.CheckCount).Take(2).ToList();

        var stage1Result = stage1monkeys[0].CheckCount * stage1monkeys[1].CheckCount;
        Console.WriteLine($"Stage2 Round({round}): {stage1Result}");

        if (round == 10000)
            break;
    }
}



internal class Monkey
{
    private List<long> _initItems;

    public Monkey(List<long> items, string operation, long check, int trueMonkeyId, int falseMonkeyId)
    {
        _initItems = items.ToList();
        Items = items;
        CheckCondition = check;
        TrueMonkeyId = trueMonkeyId;
        FalseMonkeyId = falseMonkeyId;

        var parts = operation.Split(' ');

        Value1 = parts[0] == "old" ? null : long.Parse(parts[0]);
        Value2 = parts[2] == "old" ? null : long.Parse(parts[2]);

        Operation = parts[1];
    }

    public long Helper { get; set; }

    public bool Stage1 { get; set; } = true;

    public List<long> Items { get; private set; }

    public string Operation { get; }

    public long? Value1 { get; }
    public long? Value2 { get; }

    public long CheckCondition { get; }

    public int TrueMonkeyId { get; }

    public int FalseMonkeyId { get; }

    public long CheckCount { get; set; }

    public void ResetForStage2()
    {
        Stage1 = false;
        CheckCount = 0;
        Items = _initItems;
    }

    public IEnumerable<(int monkeyId, long item)> ThrowItems()
    {
        foreach (var item in Items)
        {
            CheckCount++;

            long newItem;
            if (Operation == "*")
            {
                newItem = (Value1 ?? item) * (Value2 ?? item);
            }
            else
            {
                newItem = (Value1 ?? item) + (Value2 ?? item);
            }

            if (Stage1)
                newItem = newItem / 3;

            if (newItem % CheckCondition == 0)
            {
                yield return (TrueMonkeyId, newItem % Helper);
            }
            else
            {
                yield return (FalseMonkeyId, newItem % Helper);
            }
        }

        Items.Clear();
    }
}