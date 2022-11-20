using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;

var names = File.ReadAllLines("Input.txt");

var vowels = "aeiou".ToArray();
var invalidChars = new string[] { "ab", "cd", "pq", "xy" };

var niceNames1 = names.Where(IsNice1).ToList();
var niceNames2 = names.Where(IsNice2).ToList();

Console.WriteLine($"Nice names1: {niceNames1.Count}");
Console.WriteLine($"Nice names2: {niceNames2.Count}");

bool IsNice1(string name)
{
    int vowelCount = 0;
    bool hasDoubleChar = false;
    for (int i = 0; i < name.Length; i++)
    {
        if (vowels.Contains(name[i]))
        {
            vowelCount++;
        }

        if (i != 0)
        {
            if (name[i] == name[i - 1])
            {
                hasDoubleChar = true;
            }

            if (invalidChars.Any(c => c[0] == name[i - 1] && c[1] == name[i]))
                return false;
        }
    }

    if (vowelCount < 3 || !hasDoubleChar)
    {
        return false;
    }

    return true;
}

bool IsNice2(string name)
{
    var pairs = new ConcurrentDictionary<(char, char), List<int>>();
    bool repeatFlag = false;
    for (int i = 0; i < name.Length; i++)
    {
        if (i >= 1)
        {
            var list = pairs.GetOrAdd((name[i - 1], name[i]), new List<int>());

            list.Add(i - 1);
        }

        if (i >= 2)
        {
            if (name[i] == name[i - 2])
            {
                repeatFlag = true;
            }
        }
    }

    if (repeatFlag == false)
        return false;

    if (!pairs.Any(c => c.Value.Max() - c.Value.Min() > 1))
        return false;

    return true;
}