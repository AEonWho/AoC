var input = File.ReadAllLines("Input.txt");

var numbers = input[0].Split(',').Select(int.Parse).ToArray();

List<Board> boards = new List<Board>();
for (int i = 2; i < input.Length; i = i + 6)
{
    Board brd = new Board(input.Skip(i).Take(5).Select(c => c.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray());
    boards.Add(brd);
}

foreach (var number in numbers)
{
    foreach (var brd in boards.ToList())
    {
        if (brd.MarkNumber(number))
        {
            var res = brd.GetUnmarkedNumbers().Sum() * number;
            Console.WriteLine($"Winning {res}");
            boards.Remove(brd);
        }
    }
}


internal class Board
{
    public Board(int[][] numbers)
    {
        for (int x = 0; x < numbers.Length; x++)
        {
            for (int y = 0; y < numbers[x].Length; y++)
            {
                Numbers.Add(numbers[x][y], (x, y));
            }
        }
    }

    Dictionary<int, (int x, int y)> Numbers = new Dictionary<int, (int x, int y)>();

    List<(int, int)> MarkedNumbers = new List<(int, int)>();

    public IEnumerable<int> GetUnmarkedNumbers()
    {
        foreach (var number in Numbers)
        {
            if (!MarkedNumbers.Contains(number.Value))
                yield return number.Key;
        }
    }

    public bool MarkNumber(int number)
    {
        if (Numbers.ContainsKey(number))
        {
            MarkedNumbers.Add(Numbers[number]);

            return MarkedNumbers.GroupBy(d => d.Item1).Any(c => c.Count() == 5) || MarkedNumbers.GroupBy(d => d.Item2).Any(c => c.Count() == 5);
        }

        return false;
    }
}