
internal class ClawMachine
{

    public ClawMachine(string resultX, string resultY, string buttonAX, string buttonAY, string buttonBX, string buttonBY)
    {
        ResultX = long.Parse(resultX);
        ResultY = long.Parse(resultY);
        ButtonAX = long.Parse(buttonAX);
        ButtonAY = long.Parse(buttonAY);
        ButtonBX = long.Parse(buttonBX);
        ButtonBY = long.Parse(buttonBY);
    }

    public void InitLvl2()
    {
        ResultX += 10000000000000;
        ResultY += 10000000000000;
    }

    public long ResultX { get; private set; }
    public long ResultY { get; private set; }
    public long ButtonAX { get; }
    public long ButtonAY { get; }
    public long ButtonBX { get; }
    public long ButtonBY { get; }

    internal IEnumerable<(long, long)> SimulateGameLevel1()
    {
        var maxBCountX = ResultX / ButtonBX;
        var maxBCountY = ResultY / ButtonBY;

        var bMax = Math.Min(maxBCountX, maxBCountY);

        for (var bCount = bMax; bCount > 0; bCount--)
        {
            var offsetX = ResultX - (bCount * ButtonBX);
            var offsetY = ResultY - (bCount * ButtonBY);
            var res1 = offsetX % ButtonAX;
            if (res1 != 0)
                continue;

            var res2 = offsetY % ButtonAY;

            if (res1 == res2)
            {
                var acnt1 = offsetX / ButtonAX;
                var acnt2 = offsetY / ButtonAY;

                if (acnt1 == acnt2)
                {
                    yield return (acnt2, bCount);
                }
            }
        }
    }

    internal IEnumerable<(long, long)> SimulateGameLevel2()
    {
        //Whatever Logic ausn Internet...
        var b = (ResultY * ButtonAX - ResultX * ButtonAY) / ((ButtonAY * -1L) * ButtonBX + ButtonBY * ButtonAX);
        var a = (ResultX - ButtonBX * b) / ButtonAX;

        if (a * ButtonAX + b * ButtonBX == ResultX && a * ButtonAY + b * ButtonBY == ResultY)
            yield return (a, b);
    }
}