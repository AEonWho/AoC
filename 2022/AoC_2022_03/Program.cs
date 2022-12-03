using System.Linq;

var lines = File.ReadAllLines("Input.txt").ToList();

List<int> tmp = new List<int>();
foreach (var line in lines)
{
    var length = line.Length / 2;
    var val = line[..length].Intersect(line[length..]).Sum(GetValue);
    tmp.Add(val);
}

Console.WriteLine("Stage1: " + tmp.Sum());

List<char> tmp2 = new List<char>();
for (int i = 0; i < lines.Count; i = i + 3)
{
    var c = lines[i].Intersect(lines[i + 1].Intersect(lines[i + 2])).First();
    tmp2.Add(c);
}

Console.WriteLine("Stage2: " + tmp2.Select(GetValue).Sum());

int GetValue(char arg1)
{
    if (Char.IsLower(arg1))
    {
        return arg1 - 'a' + 1;
    }
    else
    {
        return arg1 - 'A' + 27;
    }
}