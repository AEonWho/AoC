var lines = File.ReadAllLines("Input.txt");


var orderingRules = lines.TakeWhile(d => d != string.Empty).ToList();
var unrefinedPages = lines.Skip(orderingRules.Count() + 1).ToList();


var groupedRules = orderingRules.Select(c => c.Split('|')).GroupBy(d => d[0]).ToDictionary(d => int.Parse(d.Key), d => d.Select(c => int.Parse(c[1])).ToList());
var pages = unrefinedPages.Select(c => c.Split(',').Select(int.Parse).ToList()).ToList();

var validPages = new List<List<int>>();
var invalidPages = new List<List<int>>();
foreach (var page in pages)
{
    bool fail = false;
    for (int i = 0; i < page.Count; i++)
    {
        var checkBefore = page.Take(i).ToArray();
        var checkkAfter = page.Skip(i + 1).ToList();

        if (groupedRules.ContainsKey(page[i]))
        {
            foreach (var c in checkBefore)
            {
                if (groupedRules[page[i]].Contains(c))
                {
                    fail = true;
                    break;
                }
            }

            foreach (var c in checkkAfter)
            {
                if (!groupedRules[page[i]].Contains(c))
                {
                    fail = true;
                    break;
                }
            }
        }

        if (fail)
            break;
    }

    if (!fail)
    {
        validPages.Add(page);
    }
    else
    {
        invalidPages.Add(page);
    }
}

var sum = 0;
foreach (var p in validPages)
{
    sum += p[p.Count / 2];
}

Console.WriteLine("Level1: " + sum);


List<List<int>> tmpRes = new List<List<int>>();
foreach (var invalidPage in invalidPages)
{
    var bla = new List<int>();
    while (invalidPage.Any())
    {
        var invalid = groupedRules.Where(d => invalidPage.Contains(d.Key)).SelectMany(c => c.Value);
        var next = invalidPage.First(d => !invalid.Contains(d));
        invalidPage.Remove(next);
        bla.Add(next);
    }
    tmpRes.Add(bla);
}

sum = 0;
foreach (var p in tmpRes)
{
    sum += p[p.Count / 2];
}

Console.WriteLine("Level2: " + sum);
