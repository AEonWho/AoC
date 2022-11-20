var logicText = File.ReadAllLines("Input.txt"); 
//var logicText = File.ReadAllLines("Sample.txt");

Dictionary<string, ushort> logicValues = new Dictionary<string, ushort>();

var logic = logicText.Select(Parse).ToList();

while (logic.Any(c => !c.IsSet))
{
    var entries = logic.Where(c => !c.IsSet && c.IsValid()).ToList();

    entries.ForEach(c => c.CalculateValue());
}

var value = logicValues["a"];

Console.WriteLine(value);

logicValues.Clear();
logicValues["b"] = value;

while (logic.Any(c => !c.IsSet))
{
    var entries = logic.Where(c => !c.IsSet && c.IsValid()).ToList();

    entries.ForEach(c => c.CalculateValue());
}

Console.WriteLine(logicValues["a"]);

LogicGate Parse(string line)
{
    var arg = line.Split(" -> ").ToList();

    var target = arg[1];

    var logicData = arg[0].Split(' ').ToList();

    if (logicData.Count == 1)
    {
        return new LogicConstant(logicData[0], target, logicValues);
    }
    else if (logicData[0] == "NOT")
    {
        return new LogicNot(logicData[1], target, logicValues);
    }
    else if (logicData[1] == "AND")
    {
        return new LogicAnd(logicData[0], logicData[2], target, logicValues);
    }
    else if (logicData[1] == "OR")
    {
        return new LogicOr(logicData[0], logicData[2], target, logicValues);
    }
    else if (logicData[1] == "LSHIFT")
    {
        return new LogicLeftShift(logicData[0], logicData[2], target, logicValues);
    }
    else if (logicData[1] == "RSHIFT")
    {
        return new LogicRightShift(logicData[0], logicData[2], target, logicValues);
    }

    throw new InvalidOperationException();
}