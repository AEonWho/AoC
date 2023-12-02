
var lines = File.ReadAllLines("Input.txt").ToList();

List<int> nmbLevel1 = new List<int>();
List<int> nmbLevel2 = new List<int>();
for (int i = 0; i < lines.Count; i++)
{
    var firstLevel1 = findFirstLevel1(lines[i]);
    var lastLevel1 = findlastLevel1(lines[i]);

    var firstLevel2 = findFirstLevel2(lines[i]);
    var lastLevel2 = findlastLevel2(lines[i]);

    nmbLevel1.Add(firstLevel1 * 10 + lastLevel1);
    nmbLevel2.Add(firstLevel2 * 10 + lastLevel2);
}

Console.WriteLine("Level1: " + nmbLevel1.Sum());
Console.WriteLine("Level2: " + nmbLevel2.Sum());

int findFirstLevel1(string line)
{
    for (int i = 0; i < line.Length; i++)
    {
        var nmb = line[i] - 48;
        if (nmb >= 0 && nmb <= 9)
            return nmb;
    }

    throw new NotSupportedException();
}
int findlastLevel1(string line)
{
    for (int i = line.Length - 1; i >= 0; i--)
    {
        var nmb = line[i] - 48;
        if (nmb >= 0 && nmb <= 9)
            return nmb;
    }

    throw new NotSupportedException();
}
int findFirstLevel2(string line)
{
    for (int i = 0; i < line.Length; i++)
    {
        var nmb = line[i] - 48;
        if (nmb >= 0 && nmb <= 9)
            return nmb;

        var subs = line.Substring(i);

        switch (subs)
        {
            case string s when s.StartsWith("one"):
                return 1;
            case string s when s.StartsWith("two"):
                return 2;
            case string s when s.StartsWith("three"):
                return 3;
            case string s when s.StartsWith("four"):
                return 4;
            case string s when s.StartsWith("five"):
                return 5;
            case string s when s.StartsWith("six"):
                return 6;
            case string s when s.StartsWith("seven"):
                return 7;
            case string s when s.StartsWith("eight"):
                return 8;
            case string s when s.StartsWith("nine"):
                return 9;
            case string s when s.StartsWith("zero"):
                return 0;
        }
    }

    throw new NotSupportedException();
}

int findlastLevel2(string line)
{
    for (int i = line.Length - 1; i >= 0; i--)
    {
        var nmb = line[i] - 48;
        if (nmb >= 0 && nmb <= 9)
            return nmb;

        var subs = line.Substring(i);

        switch(subs)
        {
            case string s when s.StartsWith("one"):
                return 1;
            case string s when s.StartsWith("two"):
                return 2;
            case string s when s.StartsWith("three"):
                return 3;
            case string s when s.StartsWith("four"):
                return 4;
            case string s when s.StartsWith("five"):
                return 5;
            case string s when s.StartsWith("six"):
                return 6;
            case string s when s.StartsWith("seven"):
                return 7;
            case string s when s.StartsWith("eight"):
                return 8;
            case string s when s.StartsWith("nine"):
                return 9;
            case string s when s.StartsWith("zero"):
                return 0;
        }
    }

    throw new NotSupportedException();
}

