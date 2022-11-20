var lines = File.ReadAllLines("Input.txt").Select(c => c.Split(' ')).ToList();

int x = 0;
int y = 0;

foreach (var line in lines)
{
    var value = int.Parse(line[1]);

    switch (line[0])
    {
        case "up":
            y -= value;
            break;
        case "down":
            y += value;
            break;
        case "forward":
            x += value;
            break;
    }
}

Console.WriteLine($"Horizontal: {x} Vertical: {y}, Sum: {x * y}");

x = 0;
y = 0;
int aim = 0;
foreach (var line in lines)
{
    var value = int.Parse(line[1]);

    switch (line[0])
    {
        case "up":
            aim -= value;
            break;
        case "down":
            aim += value;
            break;
        case "forward":
            x += value;
            y += aim*value;
            break;
    }
}

Console.WriteLine($"Horizontal: {x} Vertical: {y}, Sum: {x * y}");