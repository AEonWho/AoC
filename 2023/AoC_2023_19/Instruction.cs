public record XMas(long X, long M, long A, long S);

public class Instruction
{

    public List<InstructionEvaluator> Evaluators { get; } = new List<InstructionEvaluator>();

    public Instruction(string[] instructions)
    {
        foreach (var instruction in instructions)
        {
            var splitted = instruction.Split([":", "<", ">"], StringSplitOptions.RemoveEmptyEntries);
            if (splitted.Length == 1)
            {
                Evaluators.Add(new FallbackEvaluator(splitted[0]));
            }
            else
            {
                if (instruction.Contains(">"))
                {
                    Evaluators.Add(new GreatherEvaluator(splitted[2], splitted[0], long.Parse(splitted[1])));
                }
                else
                {
                    Evaluators.Add(new LesserEvaluator(splitted[2], splitted[0], long.Parse(splitted[1])));
                }
            }
        }
    }

    public string Evaluate(XMas entry)
    {
        foreach(var evaluator in Evaluators)
        {
            if (evaluator.Evaluate(entry))
                return evaluator.Result;
        }

        throw new NotSupportedException();
    }
}