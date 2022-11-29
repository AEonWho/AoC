var values = File.ReadAllLines("Input.txt").Select(int.Parse).ToList();

Console.WriteLine("Stage1: " + values.Sum());

HashSet<long> Frequencies = new HashSet<long>();

var value = 0;
int i = 0;
while (!Frequencies.Contains(value))
{
    Frequencies.Add(value);

    value += values[i];

    i++;
    if (i >= values.Count)
        i = 0;
}

Console.WriteLine("Stage1: " + value);