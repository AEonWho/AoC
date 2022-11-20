var lines = File.ReadAllLines("Input.txt").Select(int.Parse).ToList();

int increments = 0;

for (int i = 1; i < lines.Count; i++)
{
    if (lines[i - 1] < lines[i])
        increments++;
}

Console.WriteLine($"Increments: {increments}");

increments = 0;

for (int i = 3; i < lines.Count; i++)
{
    var val1 = lines[i - 3] + lines[i - 2] + lines[i - 1];
    var val2 = lines[i - 2] + lines[i - 1] + lines[i];

    if (val1< val2)
        increments++;
}

Console.WriteLine($"Increments: {increments}");