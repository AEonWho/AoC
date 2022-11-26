using System.Reflection.Metadata.Ecma335;

var lines = File.ReadAllLines("Input.txt");
var entries = lines.Select(c => new SnailFishMath(c)).ToList();

var math = entries[0].Base;

for (int i = 1; i < entries.Count; i++)
{
    var x = new SnailFishMath(math, entries[i].Base);

    x.Reduce();
    math = x.Base;
}

Console.WriteLine("Stage1: " + math.GetMagnitude());

var combinations = from a in lines
                   from b in lines
                   where a != b
                   select new
                   {
                       A = a,
                       B = b
                   };

long value = 0;
foreach (var combination in combinations)
{
    var A = new SnailFishMath($"[{combination.A},{combination.B}]");
    A.Reduce();
    var newVal = A.Base.GetMagnitude();
    if (value < newVal)
    {
        value = newVal;
    }
    var B = new SnailFishMath($"[{combination.B},{combination.A}]");
    B.Reduce();
    newVal = B.Base.GetMagnitude();
    if (value < newVal)
    {
        value = newVal;
    }
}

Console.WriteLine("Stage2: " + value);

internal class SnailFishMath
{
    public SnailFishPair Base { get; }

    public SnailFishMath(string c)
    {
        (var pair, _) = ParseLine(c, null);
        Base = pair as SnailFishPair;
    }

    public SnailFishMath(SnailFishPair left, SnailFishPair right)
    {
        Base = new SnailFishPair();
        Base.Left = left;
        left.Parent = Base;
        Base.Right = right;
        right.Parent = Base;
    }

    private (SnailFishBase, int) ParseLine(string c, SnailFishPair parent)
    {
        if (c.StartsWith("["))
        {
            var idx = 1;
            var pair = new SnailFishPair() { Parent = parent };
            (pair.Left, var idx1) = ParseLine(c[idx..], pair);
            idx += idx1 + 1;
            (pair.Right, var idx2) = ParseLine(c[idx..], pair);
            idx += idx2 + 1;

            return (pair, idx);
        }
        else
        {
            var value = new SnailFishValue() { Parent = parent, Value = int.Parse(c[0..1]) };

            return (value, 1);
        }
    }

    internal void Reduce()
    {
        while (true)
        {
            (SnailFishPair entryToExplode, _) = Base.GetEntryToExplode(0).FirstOrDefault(d => d.Item2 >= 4);
            while (entryToExplode != null)
            {
                var valueLeft = entryToExplode.Left as SnailFishValue;
                var valueRight = entryToExplode.Right as SnailFishValue;

                if (entryToExplode.Parent.Left == entryToExplode)
                {
                    var parentLeft = entryToExplode.Parent;
                    var entryToCheck = entryToExplode;
                    while (parentLeft?.Left == entryToCheck)
                    {
                        entryToCheck = parentLeft;
                        parentLeft = entryToCheck.Parent;
                    }

                    if (parentLeft != null)
                    {
                        var leftPath = parentLeft.Left;
                        while (leftPath is SnailFishPair pair)
                        {
                            leftPath = pair.Right;
                        }
                        (leftPath as SnailFishValue).Value += valueLeft.Value;
                    }

                    entryToExplode.Parent.Left = new SnailFishValue { Parent = entryToExplode.Parent, Value = 0 };

                    var rightPath = entryToExplode.Parent.Right;
                    while (rightPath is SnailFishPair pair)
                    {
                        rightPath = pair.Left;
                    }

                    (rightPath as SnailFishValue).Value += valueRight.Value;
                }
                else
                {
                    var parentRight = entryToExplode.Parent;
                    var entryToCheck = entryToExplode;
                    while (parentRight?.Right == entryToCheck)
                    {
                        entryToCheck = parentRight;
                        parentRight = entryToCheck.Parent;
                    }

                    if (parentRight != null)
                    {
                        var rightPath = parentRight.Right;
                        while (rightPath is SnailFishPair pair)
                        {
                            rightPath = pair.Left;
                        }
                        (rightPath as SnailFishValue).Value += valueRight.Value;
                    }

                    entryToExplode.Parent.Right = new SnailFishValue { Parent = entryToExplode.Parent, Value = 0 };



                    var leftPath = entryToExplode.Parent.Left;
                    while (leftPath is SnailFishPair pair)
                    {
                        leftPath = pair.Right;
                    }

                    (leftPath as SnailFishValue).Value += valueLeft.Value;
                }

                (entryToExplode, _) = Base.GetEntryToExplode(0).FirstOrDefault(d => d.Item2 >= 4);
            }

            var entryToReduce = Base.GetEntryToReduce().FirstOrDefault();

            if (entryToReduce != null)
            {
                var leftPart = entryToReduce.Value / 2;
                var rightPart = entryToReduce.Value - leftPart;

                var newPair = new SnailFishPair
                {
                    Parent = entryToReduce.Parent
                };

                newPair.Left = new SnailFishValue
                {
                    Parent = newPair,
                    Value = leftPart,
                };
                newPair.Right = new SnailFishValue
                {
                    Parent = newPair,
                    Value = rightPart,
                };

                if (entryToReduce.Parent.Left == entryToReduce)
                {
                    entryToReduce.Parent.Left = newPair;
                }
                else
                {
                    entryToReduce.Parent.Right = newPair;
                }

                continue;
            }

            break;
        }
    }
}

public abstract class SnailFishBase
{
    public SnailFishPair Parent { get; set; }

    public abstract long GetMagnitude();
}

public class SnailFishValue : SnailFishBase
{
    public int Value { get; set; }

    public override long GetMagnitude()
    {
        return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

public class SnailFishPair : SnailFishBase
{
    public SnailFishBase Left { get; set; }
    public SnailFishBase Right { get; set; }

    public IEnumerable<(SnailFishPair, int)> GetEntryToExplode(int currentDepth)
    {
        if (Left is SnailFishPair lPair)
        {
            foreach (var entry in lPair.GetEntryToExplode(currentDepth + 1))
                yield return entry;
        }

        if (Right is SnailFishPair rPair)
        {
            foreach (var entry in rPair.GetEntryToExplode(currentDepth + 1))
                yield return entry;
        }

        yield return (this, currentDepth);
    }

    public IEnumerable<SnailFishValue> GetEntryToReduce()
    {
        if (Left is SnailFishPair lPair)
        {
            foreach (var entry in lPair.GetEntryToReduce())
                yield return entry;
        }
        else if (Left is SnailFishValue vLeft && vLeft.Value >= 10)
        {
            yield return vLeft;
        }

        if (Right is SnailFishPair rPair)
        {
            foreach (var entry in rPair.GetEntryToReduce())
                yield return entry;
        }
        if (Right is SnailFishValue vRight && vRight.Value >= 10)
        {
            yield return vRight;
        }
    }

    public override long GetMagnitude()
    {
        return (Left.GetMagnitude() * 3) + (Right.GetMagnitude() * 2);
    }

    public override string ToString()
    {
        return $"[{Left}, {Right}]";
    }
}