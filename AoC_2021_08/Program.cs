using System.Text;

var splits = File.ReadAllLines("Input.txt");

var resultNumber = 0;
foreach (var split in splits)
{
    var data = split.Split(new string[] { " ", "|" }, StringSplitOptions.RemoveEmptyEntries).Select(c => new String(c.OrderBy(d => d).ToArray())).Distinct().ToList();
    List<string> numbers = new List<string>(new string[10]);

    numbers[1] = data.FirstOrDefault(d => d.Length == 2);
    numbers[4] = data.FirstOrDefault(d => d.Length == 4);
    numbers[7] = data.FirstOrDefault(d => d.Length == 3);
    numbers[8] = data.FirstOrDefault(d => d.Length == 7);

    numbers[5] = data.Single(c => c.Count() == 5 && !numbers.Contains(c) && c.Intersect(numbers[4]).Count() == 3 && c.Intersect(numbers[1]).Count() == 1);
    numbers[2] = data.Single(c => c.Count() == 5 && !numbers.Contains(c) && c.Intersect(numbers[4]).Count() == 2 && c.Intersect(numbers[1]).Count() == 1);
    numbers[3] = data.Single(c => c.Count() == 5 && !numbers.Contains(c));
    numbers[9] = data.Single(c => c.Count() == 6 && !numbers.Contains(c) && numbers[4].All(c.Contains));
    numbers[6] = data.Single(c => c.Count() == 6 && !numbers.Contains(c) && numbers[5].All(c.Contains));
    numbers[0] = data.Single(c => c.Count() == 6 && !numbers.Contains(c));

    var idx = split.IndexOf("|");
    var res = split.Substring(idx + 1).Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(c => new String(c.OrderBy(d => d).ToArray())).ToList();

    StringBuilder sbNumber = new StringBuilder(res.Count);
    foreach (var resField in res)
    {
        sbNumber.Append(numbers.IndexOf(resField).ToString());
    }

    resultNumber += int.Parse(sbNumber.ToString());
}

Console.WriteLine(resultNumber);