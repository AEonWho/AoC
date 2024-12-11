using Microsoft.CodeAnalysis.CSharp.Scripting;

var lines = File.ReadAllLines("Input.txt");

ulong sum1 = 0;
ulong sum2 = 0;
foreach (var lin in lines)
{
    var split = lin.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray();

    var result = split[0];
    var checks = split.Skip(1).ToArray();

    List<ulong> tmpLvl1 = [checks[0]];
    List<ulong> tmpLvl2 = [checks[0]];
    foreach (var c in checks.Skip(1))
    {
        tmpLvl1 = tmpLvl1.SelectMany(d => new ulong[] { d * c, d + c }).Distinct().ToList();
        tmpLvl2 = tmpLvl2.SelectMany(d => new ulong[] { d * c, d + c, ulong.Parse(d + "" + c) }).Distinct().ToList();
    }

    if (tmpLvl1.Contains(result))
    {
        sum1 += result;
    }

    if (tmpLvl2.Contains(result))
    {
        sum2 += result;
    }
}

Console.WriteLine("Level1: " + sum1);
Console.WriteLine("Level2: " + sum2);