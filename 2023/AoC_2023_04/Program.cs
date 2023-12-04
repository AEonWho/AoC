var input = File.ReadAllLines("Input.txt");

List<int> level1Points = new List<int>();
Dictionary<int, int> level2winningCountDict = new();
for (int i = 0; i < input.Length; i++)
{
    var line = input[i];
    var splitted = line.Split(':', '|');

    var winningNumbers = splitted[1].Split([" "], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    var tokens = splitted[2].Split([" "], StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    var scoredNumbers = tokens.Intersect(winningNumbers).ToList();
    if (scoredNumbers.Any())
    {
        level1Points.Add((int)Math.Pow(2, scoredNumbers.Count - 1));
    }

    level2winningCountDict.Add(i, scoredNumbers.Count);
}

Console.WriteLine("Level1: " + level1Points.Sum());

int cnt = 0;
Queue<int> queue = new Queue<int>(Enumerable.Range(0, input.Length));
while (queue.TryDequeue(out var idx))
{
    if (level2winningCountDict.TryGetValue(idx, out var winningCount))
    {
        cnt++;

        for (int i = 1; i <= winningCount; i++)
        {
            queue.Enqueue(idx + i);
        }
    }
}

Console.WriteLine("Level2: " + cnt);

Console.ReadLine();