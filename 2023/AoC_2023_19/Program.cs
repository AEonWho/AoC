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

Console.WriteLine("Level1: " + sum);

List<InstructionRange> acceptedRanges = [];
Queue<(InstructionRange Range, string Workflow)> Data = new Queue<(InstructionRange, string)>();
Data.Enqueue((new InstructionRange(1, 4000, 1, 4000, 1, 4000, 1, 4000), "in"));

while (Data.TryDequeue(out var dataset))
{
    if (dataset.Workflow == "A")
    {
        acceptedRanges.Add(dataset.Range);
        continue;
    }
    else if (dataset.Workflow == "R")
    {
        continue;
    }

    var range = dataset.Item1;
    var instruction = Instructions[dataset.Workflow];


    foreach (var evaluator in instruction.Evaluators)
    {
        if (evaluator is GreatherEvaluator gEval)
        {
            switch (gEval.Property)
            {
                case "x":
                    if (range.XEnd <= gEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.XStart > gEval.CheckValue)
                    {
                        Data.Enqueue((range, gEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { XStart = gEval.CheckValue + 1 }, gEval.Result));
                        range = range with { XEnd = gEval.CheckValue };
                    }
                    break;

                case "m":
                    if (range.MEnd <= gEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.MStart > gEval.CheckValue)
                    {
                        Data.Enqueue((range, gEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { MStart = gEval.CheckValue + 1 }, gEval.Result));
                        range = range with { MEnd = gEval.CheckValue };
                    }
                    break;

                case "a":
                    if (range.AEnd <= gEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.AStart > gEval.CheckValue)
                    {
                        Data.Enqueue((range, gEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { AStart = gEval.CheckValue + 1 }, gEval.Result));
                        range = range with { AEnd = gEval.CheckValue };
                    }
                    break;

                case "s":
                    if (range.SEnd <= gEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.SStart > gEval.CheckValue)
                    {
                        Data.Enqueue((range, gEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { SStart = gEval.CheckValue + 1 }, gEval.Result));
                        range = range with { SEnd = gEval.CheckValue };
                    }
                    break;
            }
        }
        else if (evaluator is LesserEvaluator lEval)
        {
            switch (lEval.Property)
            {
                case "x":
                    if (range.XStart >= lEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.XEnd < lEval.CheckValue)
                    {
                        Data.Enqueue((range, lEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { XEnd = lEval.CheckValue - 1 }, lEval.Result));
                        range = range with { XStart = lEval.CheckValue };
                    }
                    break;

                case "m":
                    if (range.MStart >= lEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.MEnd < lEval.CheckValue)
                    {
                        Data.Enqueue((range, lEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { MEnd = lEval.CheckValue - 1 }, lEval.Result));
                        range = range with { MStart = lEval.CheckValue };
                    }
                    break;

                case "a":
                    if (range.AStart >= lEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.AEnd < lEval.CheckValue)
                    {
                        Data.Enqueue((range, lEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { AEnd = lEval.CheckValue - 1 }, lEval.Result));
                        range = range with { AStart = lEval.CheckValue };
                    }
                    break;

                case "s":
                    if (range.SStart >= lEval.CheckValue)
                    {
                        continue;
                    }
                    else if (range.SEnd < lEval.CheckValue)
                    {
                        Data.Enqueue((range, lEval.Result));
                        break;
                    }
                    else
                    {
                        Data.Enqueue((range with { SEnd = lEval.CheckValue - 1 }, lEval.Result));
                        range = range with { SStart = lEval.CheckValue };
                    }
                    break;
            }
        }
        else if (evaluator is FallbackEvaluator fb)
        {
            Data.Enqueue((range, fb.Result));
            break;
        }
    }
}

sum = 0;
foreach (var range in acceptedRanges)
{
    var x = (range.XEnd - range.XStart) + 1;
    var m = (range.MEnd - range.MStart) + 1;
    var a = (range.AEnd - range.AStart) + 1;
    var s = (range.SEnd - range.SStart) + 1;

    sum += x * m * a * s;
}

Console.WriteLine(sum);

record InstructionRange(long XStart, long XEnd, long MStart, long MEnd, long AStart, long AEnd, long SStart, long SEnd);