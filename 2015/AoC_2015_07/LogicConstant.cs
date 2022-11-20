public class LogicConstant : LogicGate
{
    public LogicConstant(string value, string target, Dictionary<string, ushort> logicValues)
        : base(target, logicValues)
    {
        Field = value;
    }

    public string Field { get; }

    public override void CalculateValue()
    {
        LogicValues.Add(Target, TryGetValue(Field).Value);
    }

    public override bool IsValid()
    {
        return TryGetValue(Field).HasValue;
    }
}