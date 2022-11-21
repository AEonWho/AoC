using System.Text;

var data = File.ReadAllLines("Input.txt");

var startTags = new[] { '(', '[', '{', '<' }.ToList();
var endTags = new[] { ')', ']', '}', '>' }.ToList();

List<char> errors = new List<char>();
List<long> autocompleteScores = new List<long>();
foreach (var line in data)
{
    StringBuilder sb = new StringBuilder();

    for (int i = 0; i < line.Length; i++)
    {
        if (startTags.Contains(line[i]))
        {
            sb.Append(line[i]);
        }
        else if (endTags.Contains(line[i]))
        {
            var idx = endTags.IndexOf(line[i]);

            if (sb[sb.Length - 1] == startTags[idx])
            {
                sb.Remove(sb.Length - 1, 1);
            }
            else
            {
                errors.Add(line[i]);
                sb.Clear();
                break;
            }
        }
    }

    if (sb.Length > 0)
    {
        StringBuilder sbAc = new StringBuilder();
        int i = sb.Length;
        while (i > 0)
        {
            i--;

            switch (sb[i])
            {
                case '(':
                    sbAc.Append(')');
                    continue;
                case '[':
                    sbAc.Append(']');
                    continue;
                case '{':
                    sbAc.Append('}');
                    continue;
                case '<':
                    sbAc.Append('>');
                    continue;
            }
            break;
        }

        long score = 0;
        foreach (var c in sbAc.ToString())
        {
            score = score * 5;
            switch (c)
            {
                case ')':
                    score = score + 1;
                    break;
                case ']':
                    score = score + 2;
                    break;
                case '}':
                    score = score + 3;
                    break;
                case '>':
                    score = score + 4;
                    break;
            }
        }
        autocompleteScores.Add(score);
    }
}

var res = errors.Sum(GetErrorValue);

Console.WriteLine(res);


var x = autocompleteScores.OrderBy(d => d).ToList();

var res2 = x[autocompleteScores.Count / 2];

Console.WriteLine(res2);
int GetErrorValue(char arg1)
{
    switch (arg1)
    {
        case ')':
            return 3;
        case ']':
            return 57;
        case '}':
            return 1197;
        case '>':
            return 25137;
        default:
            throw new InvalidOperationException();
    }
}