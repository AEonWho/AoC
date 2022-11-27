using System.Data;
using System.Runtime.CompilerServices;

var lines = File.ReadAllLines("Input.txt");

List<List<InstructionBase>> instructions = new List<List<InstructionBase>>();


foreach (var line in lines)
{
    var split = line.Split(' ');

    switch (split[0])
    {
        case "inp":
            instructions.Add(new List<InstructionBase>());
            instructions[^1].Add(new Input(split[1]));
            break;
        case "add":
            instructions[^1].Add(new AddInstruction(split[1], split[2]));
            break;
        case "mul":
            instructions[^1].Add(new MulInstruction(split[1], split[2]));
            break;
        case "div":
            instructions[^1].Add(new DivInstruction(split[1], split[2]));
            break;
        case "mod":
            instructions[^1].Add(new ModInstruction(split[1], split[2]));
            break;
        case "eql":
            instructions[^1].Add(new EqualInstruction(split[1], split[2]));
            break;
        default:
            throw new NotImplementedException();
    }
}

HashSet<(int, long)> invalidStates = new HashSet<(int, long)>();


//CheckInstructionsStage1(0, new Dictionary<string, long>());
CheckInstructionsStage2(0, new Dictionary<string, long>());

bool CheckInstructionsStage1(int depth, Dictionary<string, long> dict, string number = "")
{
    for (int i = 9; i > 0; i--)
    {
        var newDict = dict.ToDictionary(d => d.Key, d => d.Value);

        instructions[depth].ForEach(d => d.Evaluate(newDict, i));

        if (invalidStates.Contains((depth, newDict["z"])))
            continue;

        if (depth == 13)
        {
            if (newDict["z"] == 0)
            {
                Console.WriteLine("Stage1: " + number + i);
                return true;
            }
        }
        else
        {
            var res = CheckInstructionsStage1(depth + 1, newDict, number + i);

            if (!res)
            {
                invalidStates.Add((depth, newDict["z"]));
            }
            else
            {
                return true;
            }
        }
    }

    return false;
}
bool CheckInstructionsStage2(int depth, Dictionary<string, long> dict, string number = "")
{
    for (int i = 1; i < 10; i++)
    {
        var newDict = dict.ToDictionary(d => d.Key, d => d.Value);

        instructions[depth].ForEach(d => d.Evaluate(newDict, i));

        if (invalidStates.Contains((depth, newDict["z"])))
            continue;

        if (depth == 13)
        {
            if (newDict["z"] == 0)
            {
                Console.WriteLine("Stage2: " + number + i);
                return true;
            }
        }
        else
        {
            var res = CheckInstructionsStage2(depth + 1, newDict, number + i);

            if (!res)
            {
                invalidStates.Add((depth, newDict["z"]));
            }
            else
            {
                return true;
            }
        }
    }

    return false;
}

internal abstract class InstructionBase
{
    public InstructionBase(string field)
    {
        TargetField = field;

    }

    public string TargetField { get; }

    public abstract void Evaluate(Dictionary<string, long> dict, int number);
}

class Input : InstructionBase
{
    public Input(string field) : base(field)
    {
    }

    public override void Evaluate(Dictionary<string, long> dict, int number)
    {
        dict[TargetField] = number;
    }
}

abstract class InstructionOperationBase : InstructionBase
{
    private long? _value;
    private string _paramField;

    public InstructionOperationBase(string field, string param) : base(field)
    {
        if (long.TryParse(param, out var value))
        {
            _value = value;
        }
        else
        {
            _paramField = param;
        }

    }

    protected long GetValue(Dictionary<string, long> dict)
    {
        return _value ?? (dict.ContainsKey(_paramField) ? dict[_paramField] : 0);
    }
}

class AddInstruction : InstructionOperationBase
{
    public AddInstruction(string field, string param) : base(field, param)
    {
    }

    public override void Evaluate(Dictionary<string, long> dict, int number)
    {
        dict[TargetField] = dict[TargetField] + GetValue(dict);
    }

}

class MulInstruction : InstructionOperationBase
{
    public MulInstruction(string field, string param) : base(field, param)
    {
    }

    public override void Evaluate(Dictionary<string, long> dict, int number)
    {
        dict[TargetField] = (dict.ContainsKey(TargetField) ? dict[TargetField] : 0) * GetValue(dict);
    }
}

class DivInstruction : InstructionOperationBase
{
    public DivInstruction(string field, string param) : base(field, param)
    {
    }

    public override void Evaluate(Dictionary<string, long> dict, int number)
    {
        dict[TargetField] = (dict.ContainsKey(TargetField) ? dict[TargetField] : 0) / GetValue(dict);
    }
}

class ModInstruction : InstructionOperationBase
{
    public ModInstruction(string field, string param) : base(field, param)
    {
    }

    public override void Evaluate(Dictionary<string, long> dict, int number)
    {
        dict[TargetField] = dict[TargetField] % GetValue(dict);
    }
}

class EqualInstruction : InstructionOperationBase
{
    public EqualInstruction(string field, string param) : base(field, param)
    {
    }

    public override void Evaluate(Dictionary<string, long> dict, int number)
    {
        dict[TargetField] = dict[TargetField] == GetValue(dict) ? 1 : 0;
    }
}