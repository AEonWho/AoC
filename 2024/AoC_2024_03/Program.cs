using System.Text.RegularExpressions;

var text = File.ReadAllText("Input.txt");

var regex = Regex.Matches(text, "mul\\(([1-9][0-9]*),([1-9][0-9]*)\\)");

var result = 0;
foreach (Match match in regex)
{
    var first = match.Groups[1].Value;
    var second = match.Groups[2].Value;

    result += int.Parse(first) * int.Parse(second);
}

Console.WriteLine("Level1: " + result);

result = 0;
foreach (Match match in regex)
{
    var textBefore = text.Substring(0, match.Index);

    if (textBefore.LastIndexOf("don't()") > textBefore.LastIndexOf("do()"))
        continue;

    var first = match.Groups[1].Value;
    var second = match.Groups[2].Value;

    result += int.Parse(first) * int.Parse(second);
}

Console.WriteLine("Level2: " + result);