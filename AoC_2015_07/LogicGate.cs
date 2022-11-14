public abstract class LogicGate
{
    public LogicGate(string target, Dictionary<string, ushort> logicValues)
    {
        Target = target;

        LogicValues = logicValues;
    }

    public abstract void CalculateValue();

    public abstract bool IsValid();

    protected ushort? TryGetValue(string data)
    {
        if (ushort.TryParse(data, out var val))
        {
            return val;
        }
        else if (LogicValues.ContainsKey(data))
        {
            return LogicValues[data];
        }
        return null;
    }

    public bool IsSet => LogicValues.ContainsKey(Target);

    public string Target { get; }

    public Dictionary<string, ushort> LogicValues { get; }
}