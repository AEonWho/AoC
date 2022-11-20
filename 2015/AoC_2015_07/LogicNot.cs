public class LogicNot : LogicGate
{
    public LogicNot(string field, string target, Dictionary<string, ushort> logicValues)
        : base(target, logicValues)
    {
        Field = field;
    }

    public string Field { get; }

    public override void CalculateValue()
    {
        var value = LogicValues[Field];
        var invert = (ushort)~value;
        LogicValues.Add(Target, invert);
    }

    public override bool IsValid()
    {
        if (LogicValues.ContainsKey(Field))
        {
            return true;
        }

        return false;
    }
}