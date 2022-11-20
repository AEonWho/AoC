var packages = File.ReadAllLines("Input.txt").Select(long.Parse).ToList();

var distributionWeight = packages.Sum() / 3;

Console.WriteLine("3 - " + distributionWeight);

HashSet<string> validPackageCombinations = new HashSet<string>();

SetPackageCombinations(new List<long>(), packages);

var firstOfThree = GetFirstGroupOfThreeValue();

Console.WriteLine("quantum entanglement: " + firstOfThree);

distributionWeight = packages.Sum() / 4;

Console.WriteLine("4 - " + distributionWeight);

validPackageCombinations = new HashSet<string>();

SetPackageCombinations(new List<long>(), packages);

var firstOfFour = GetFirstGroupOfFourValue();

Console.WriteLine("quantum entanglement: " + firstOfFour);

void SetPackageCombinations(List<long> list, IEnumerable<long> packages)
{
    var currentSum = list.Sum();
    var dif = distributionWeight - currentSum;
    int i = 1;
    foreach (var package in packages.Where(c => c <= dif))
    {
        var value = currentSum + package;
        if (value == distributionWeight)
        {
            var str = string.Join(" ", list.Union(new[] { package }).OrderBy(c => c));

            validPackageCombinations.Add(str);
        }
        else
        {
            var tmp = list.Union(new[] { package }).ToList();
            SetPackageCombinations(tmp, packages.Skip(i));
        }

        i++;
    }
}

long GetFirstGroupOfThreeValue()
{
    var lists = validPackageCombinations.Select(c => c.Split(' ').Select(long.Parse).ToHashSet())
        .OrderBy(c => c.Count)
        .ThenBy(c => c.Aggregate((d, e) => d * e))
        .ToList();

    int i = 1;
    foreach (var entry1 in lists)
    {
        int i2 = 1;
        foreach (var entry2 in lists.Skip(i))
        {
            if (entry1.Any(entry2.Contains))
                continue;

            foreach (var entry3 in lists.Skip(i).Skip(i2))
            {
                if (entry1.Any(entry3.Contains))
                    continue;

                if (entry2.Any(entry3.Contains))
                    continue;

                return entry1.Aggregate((d, e) => d * e);

            }
            i2++;
        }
        i++;
    }

    return -1;
}

long GetFirstGroupOfFourValue()
{
    var lists = validPackageCombinations.Select(c => c.Split(' ').Select(long.Parse).ToHashSet())
        .OrderBy(c => c.Count)
        .ThenBy(c => c.Aggregate((d, e) => d * e))
        .ToList();

    int i = 1;
    foreach (var entry1 in lists)
    {
        int i2 = 1;
        foreach (var entry2 in lists.Skip(i))
        {
            if (entry1.Any(entry2.Contains))
                continue;

            int i3 = 1;
            foreach (var entry3 in lists.Skip(i).Skip(i2))
            {
                if (entry1.Any(entry3.Contains))
                    continue;

                if (entry2.Any(entry3.Contains))
                    continue;

                foreach (var entry4 in lists.Skip(i).Skip(i2).Skip(i3))
                {
                    if (entry1.Any(entry4.Contains))
                        continue;

                    if (entry2.Any(entry4.Contains))
                        continue;

                    if (entry3.Any(entry4.Contains))
                        continue;

                    return entry1.Aggregate((d, e) => d * e);

                }
                i3++;

            }
            i2++;
        }
        i++;
    }

    return -1;
}