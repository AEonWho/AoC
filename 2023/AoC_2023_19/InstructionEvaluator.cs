public abstract class InstructionEvaluator
{
    public InstructionEvaluator(string result)
    {
        Result = result;
    }

    public abstract bool Evaluate(XMas input);

    public string Result { get; }
}

public class GreatherEvaluator : InstructionEvaluator
{
    public GreatherEvaluator(string result, string property, long checkValue) : base(result)
    {
        CheckValue = checkValue;
        Property = property;

        ValueAccessor = GetValueAccessor();
    }

    private Func<XMas, long> GetValueAccessor()
    {
        Func<XMas, long> accessor = d => throw new NotSupportedException();
        switch (Property)
        {
            case "x":
                accessor = d => d.X;
                break;
            case "m":
                accessor = d => d.M;
                break;
            case "a":
                accessor = d => d.A;
                break;
            case "s":
                accessor = d => d.S;
                break;
            default:
                break;
        }

        return accessor;
    }

    public long CheckValue { get; }
    public string Property { get; }
    public Func<XMas, long> ValueAccessor { get; }

    public override bool Evaluate(XMas input)
    {
        if (ValueAccessor(input) > CheckValue)
        {
            return true;
        }

        return false;
    }
}

public class LesserEvaluator : InstructionEvaluator
{
    public LesserEvaluator(string result, string property, long checkValue) : base(result)
    {
        CheckValue = checkValue;
        Property = property;

        ValueAccessor = GetValueAccessor();
    }

    private Func<XMas, long> GetValueAccessor()
    {
        Func<XMas, long> accessor = d => throw new NotSupportedException();
        switch (Property)
        {
            case "x":
                accessor = d => d.X;
                break;
            case "m":
                accessor = d => d.M;
                break;
            case "a":
                accessor = d => d.A;
                break;
            case "s":
                accessor = d => d.S;
                break;
            default:
                break;
        }

        return accessor;
    }

    public long CheckValue { get; }
    public string Property { get; }
    public Func<XMas, long> ValueAccessor { get; }

    public override bool Evaluate(XMas input)
    {
        if (ValueAccessor(input) < CheckValue)
        {
            return true;
        }

        return false;
    }
}

public class FallbackEvaluator : InstructionEvaluator
{
    public FallbackEvaluator(string result) : base(result)
    {
    }

    public override bool Evaluate(XMas input)
    {
        return true;
    }
}