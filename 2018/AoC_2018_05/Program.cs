using System.Text;

var chars = File.ReadAllText("Input.txt").ToList();

var result = React(chars.ToList());

Console.WriteLine("Stage1: " + result.Count);

var letters = chars.Where(Char.IsUpper).Distinct().ToList();

int shortest = chars.Count;
foreach (var letter in letters)
{
    var tmp = chars.Where(d => d != letter && d != letter + 32).ToList();
    var result2 = React(tmp);
    if (result2.Count < shortest)
    {
        shortest = result2.Count;
    }
}

Console.WriteLine("Stage2: " + shortest);

List<char> React(List<char> chars)
{
    var idx = 0;
    while (true)
    {
        if (idx >= chars.Count - 1)
            break;

        if (chars[idx] != chars[idx + 1] && (chars[idx] + 32 == chars[idx + 1] || chars[idx] == chars[idx + 1] + 32))
        {
            chars.RemoveAt(idx);
            chars.RemoveAt(idx);

            if (idx > 0)
                idx--;
        }
        else
        {
            idx++;
        }
    }

    return chars;
}