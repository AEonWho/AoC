using System.Collections.Concurrent;

var instructions = File.ReadAllLines("Input.txt");

Dictionary<string, int> variables = new Dictionary<string, int>();

variables["a"] = 0;
variables["b"] = 0;

Execute();

Console.WriteLine("Stage1");
foreach (var tmp in variables)
{
    Console.WriteLine($"Variable {tmp.Key} has Value {tmp.Value}");
}

variables["a"] = 1;
variables["b"] = 0;

Execute();

Console.WriteLine();
Console.WriteLine("Stage2");
foreach (var tmp in variables)
{
    Console.WriteLine($"Variable {tmp.Key} has Value {tmp.Value}");
}

void Execute()
{
    int i = 0;
    while (true)
    {
        if (i >= instructions.Length || i < 0)
            break;

        var instruction = instructions[i];

        if (instruction.StartsWith("hlf"))
        {
            var variable = instruction.Substring(4);

            variables[variable] = variables[variable] / 2;
        }
        else if (instruction.StartsWith("tpl"))
        {
            var variable = instruction.Substring(4);

            variables[variable] = variables[variable] * 3;
        }
        else if (instruction.StartsWith("inc"))
        {
            var variable = instruction.Substring(4);

            variables[variable] = variables[variable] + 1;
        }
        else if (instruction.StartsWith("jmp"))
        {
            var offset = int.Parse(instruction.Substring(4));

            i = i + offset;
            continue;
        }
        else if (instruction.StartsWith("jie"))
        {
            var split = instruction.Substring(4).Split(',');
            var value = variables[split[0]];
            if (value % 2 == 0)
            {
                var offset = int.Parse(split[1]);

                i = i + offset;
                continue;
            }
        }
        else if (instruction.StartsWith("jio"))
        {
            var split = instruction.Substring(4).Split(',');
            var value = variables[split[0]];

            if (value == 1)
            {
                var offset = int.Parse(split[1]);

                i = i + offset;
                continue;
            }
        }


        i++;
    }
}