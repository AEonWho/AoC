internal class Package
{
    public Package(string input)
    {
        var data = input.Split('x').Select(int.Parse).ToArray();

        LengthA = data[0];
        LengthB = data[1];
        LengthC = data[2];

        SideA = LengthA * LengthB;
        SideB = LengthA * LengthC;
        SideC = LengthB * LengthC;

        var ribbon = new[] { LengthA, LengthB, LengthC }.OrderBy(c => c).Take(2).Sum() * 2;
        Ribbon = ribbon + (LengthA * LengthB * LengthC);

        WrappingPaperNeeded = (SideA + SideB + SideC) * 2 + new[] { SideA, SideB, SideC }.Min();
    }

    public int LengthA { get; }
    public int LengthB { get; }
    public int LengthC { get; }
    public int SideA { get; }
    public int SideB { get; }
    public int SideC { get; }
    public int Ribbon { get; }
    public int WrappingPaperNeeded { get; }
}