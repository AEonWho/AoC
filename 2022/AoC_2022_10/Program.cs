var lines = File.ReadAllLines("Input.txt").ToList();

Console.SetCursorPosition(0, Console.CursorTop + 1);
Console.SetCursorPosition(0, Console.CursorTop + 1);

Console.WriteLine("Stage2: ");
var value = 1;
List<int> values = new List<int>();
for (int i = 0; i < lines.Count; i++)
{
    values.Add(value);
    Print();
    var line = lines[i];

    if (line == "noop")
        continue;

    var splitted = line.Split(' ');
    var tmpVal = int.Parse(splitted[1]);
    values.Add(value);
    Print();

    value += tmpVal;
}

Console.SetCursorPosition(0, 0);

var result = values[19] * 20 + values[59] * 60 + values[99] * 100 + values[139] * 140 + values[179] * 180 + values[219] * 220;
Console.WriteLine("Stage1: " + result);


Console.SetCursorPosition(0, 10);

void Print()
{
    var test = (values.Count - 1) % 40;
    if (test == 0)
    {
        Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
    }

    if (values[^1] == test || values[^1] == test - 1 || values[^1] == test + 1)
    {
        Console.SetCursorPosition(test, Console.CursorTop);
        Console.Write('#');
    }
}