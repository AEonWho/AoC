var lines = File.ReadAllLines("Input.txt");

Dictionary<string, Instruction> Instructions = new Dictionary<string, Instruction>();

long sum = 0;
bool instructions = true;
foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        instructions = false;
        continue;
    }
    if (instructions)
    {
        var bla = line.Split(["{", "}", ","], StringSplitOptions.RemoveEmptyEntries);
        Instructions.Add(bla[0], new Instruction(bla[1..]));
    }
    else
    {
        var bla = line.Split(["{", "}", "=", ","], StringSplitOptions.RemoveEmptyEntries);

        var entry = new XMas(int.Parse(bla[1]), int.Parse(bla[3]), int.Parse(bla[5]), int.Parse(bla[7]));

        var workflow = "in";
        while (workflow != "R" && workflow != "A")
        {
            workflow = Instructions[workflow].Evaluate(entry);
        }

        if (workflow == "A")
        {
            sum += entry.X;
            sum += entry.M;
            sum += entry.A;
            sum += entry.S;
        }
    }
}

Console.WriteLine(sum);

long cnt = 0;
for (int x = 1; x <= 4000; x++)
{
    for (int m = 1; m <= 4000; m++)
    {
        for (int a = 1; a <= 4000; a++)
        {
            for (int s = 1; s <= 4000; s++)
            {
                var entry = new XMas(x, m, a, s);

                var workflow = "in";
                while (workflow != "R" && workflow != "A")
                {
                    workflow = Instructions[workflow].Evaluate(entry);
                }

                if (workflow == "A")
                {
                    cnt++;
                }
            }
        }

        Console.WriteLine(x * m * 4000 * 4000);
    }
}