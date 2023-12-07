using System.Data;

var input = File.ReadAllLines("Input.txt");

{
    List<Hand> tmp = new List<Hand>();
    foreach (var line in input)
    {
        tmp.Add(new Hand(line.Split(' '), false));
    }

    var sum = tmp.Order().Select((d, i) => d.Bid * (i + 1)).Sum();

    Console.WriteLine("Level1: " + sum);
}

{
    List<Hand> tmp = new List<Hand>();
    foreach (var line in input)
    {
        tmp.Add(new Hand(line.Split(' '), true));
    }

    var sum = tmp.Order().Select((d, i) => d.Bid * (i + 1)).Sum();

    Console.WriteLine("Level2: " + sum);
}