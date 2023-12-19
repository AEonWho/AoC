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
    public GreatherEvaluator(string result, Func<XMas, long> getValue, long checkValue) : base(result)
    {
        CheckValue = checkValue;
        ValueAccessor = getValue;
    }

    public long CheckValue { get; }

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
    public LesserEvaluator(string result, Func<XMas, long> getValue, long checkValue) : base(result)
    {
        CheckValue = checkValue;
        ValueAccessor = getValue;
    }

    public long CheckValue { get; }

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