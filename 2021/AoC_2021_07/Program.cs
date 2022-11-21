var values = File.ReadAllText("Input.txt").Split(',').Select(int.Parse).OrderBy(c => c).ToList();

var lowest = values.Min();
var highest = values.Max();

Dictionary<int, int> sumStage1 = new Dictionary<int, int>();
Dictionary<int, int> sumStage2 = new Dictionary<int, int>();
for (int i = lowest; i <= highest; i++)
{
    sumStage1.Add(i, values.Sum(d => Math.Abs(i - d)));
    sumStage2.Add(i, values.Sum(d => SumOverN(Math.Abs(i - d))));
}

var lowestvalue1 = sumStage1.Min(c => c.Value);
Console.WriteLine(lowestvalue1);

var lowestvalue2 = sumStage2.Min(c => c.Value);
Console.WriteLine(lowestvalue2);

int SumOverN(int val)
{
    return (val * (val + 1)) / 2;
}