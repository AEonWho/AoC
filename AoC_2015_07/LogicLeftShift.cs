internal class LogicLeftShift : LogicGate
{
    public LogicLeftShift(string field1, string field2, string target, Dictionary<string, ushort> logicValues)
        : base(target, logicValues)
    {
        Field1 = field1;
        Field2 = field2;
    }

    public string Field1 { get; }
    public string Field2 { get; }

    public override void CalculateValue()
    {
        var val1 = TryGetValue(Field1).Value;
        var val2 = TryGetValue(Field2).Value;
        var val = val1 << val2;

        LogicValues.Add(Target, (ushort)val);
    }

    public override bool IsValid()
    {
        return TryGetValue(Field1).HasValue && TryGetValue(Field2).HasValue;
    }
}