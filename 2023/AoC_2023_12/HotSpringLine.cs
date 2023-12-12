internal class HotSpringLine
{
    private string template;

    private List<int> sizes;

    public long PossibleConfigurations { get; private set; }

    public HotSpringLine(string template, IEnumerable<int> sizes)
    {
        this.template = template;
        this.sizes = sizes.ToList();

        CalculatePossibleConfigurations();
    }

    private void CalculatePossibleConfigurations()
    {
        var bla = GetPossibilitiesWithValidationInfo(template, GetChoices, Validate).Select(d => (new string(d.Item1.Select(c => c == 0 ? '.' : '#').ToArray()), d.Item2));

        foreach (var entry in bla)
        {
            var data = entry.Item1.Split(["."], StringSplitOptions.RemoveEmptyEntries).Select(d => d.Length).ToList();

            if (data.SequenceEqual(sizes))
            {
                PossibleConfigurations += entry.Item2;
            }
        }
    }




    public IEnumerable<(IEnumerable<T2>, long)> GetPossibilitiesWithValidationInfo<T, T2>(IEnumerable<T> enumerable, Func<T, IEnumerable<T2>> getSubset, Func<IEnumerable<T2>, (int, int, int), (bool, (int, int, int)?)> validateFunc)
    {
        List<(IEnumerable<T2>, long, (int, int, int))> data = [(new T2[0], 1, (0, 0, 0))];

        int i = 0;
        foreach (var entry in enumerable)
        {
            i++;
            var subsets = getSubset(entry);

            var newData = new List<(IEnumerable<T2>, long, (int, int, int))>();

            foreach (var subset in subsets)
            {
                if (subset is byte b && b == 0)
                {
                    foreach (var d in data.GroupBy(d => d.Item3))
                    {
                        var tmpData = Append(d.First().Item1, subset);
                        var res = validateFunc(tmpData, d.Key);
                        if (res.Item1)
                        {
                            newData.Add((tmpData, d.Sum(d => d.Item2), res.Item2!.Value));
                        }
                    }
                }
                else
                {
                    foreach (var d in data)
                    {
                        var tmpData = Append(d.Item1, subset);
                        var res = validateFunc(tmpData, d.Item3);
                        if (res.Item1)
                        {
                            newData.Add((tmpData, d.Item2, res.Item2!.Value));
                        }
                    }
                }
            }

            data = newData;
        }

        return data.Select(d => (d.Item1, d.Item2));
    }

    private static IEnumerable<T> Append<T>(IEnumerable<T> d, T subset)
    {
        return [.. d, subset];
    }

    private (bool, (int, int, int)?) Validate(IEnumerable<byte> enumerable, (int, int, int) lookupInfo)
    {
        var currentIdx = lookupInfo.Item1;
        var currentCount = lookupInfo.Item2;
        var i = lookupInfo.Item3;
        foreach (var entry in enumerable.Skip(i))
        {
            i++;
            if (currentIdx >= sizes.Count)
            {
                if (entry == 0)
                    continue;
                return (false, null);
            }

            if (entry == 0)
            {
                if (currentCount > 0)
                {
                    if (currentCount != sizes[currentIdx])
                        return (false, null);

                    currentIdx++; ;
                }
                currentCount = 0;
            }
            else
            {
                currentCount++;

                if (currentCount > sizes[currentIdx])
                    return (false, null);
            }
        }

        return (true, (currentIdx, currentCount, i));
    }

    private IEnumerable<byte> GetChoices(char d)
    {
        if (d == '.')
            yield return 0;
        else if (d == '#')
            yield return 1;
        else
        {
            yield return 0;
            yield return 1;
        }
    }
}