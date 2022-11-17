var replacements = File.ReadAllLines("Replacements.txt").Select(d => d.Split(" => ")).Select(c => (c[0], c[1])).ToList();

var molecule = File.ReadAllText("Input.txt");

var uniqueReplacements = replacements.Where(d => d.Item2.Contains("Rn") && d.Item2.EndsWith("Ar"));

List<string> results = Calibrate(molecule);

Console.WriteLine($"UniqueCombinations: {results.Count}");

replacements.Remove(("Ca", "SiTh"));

(molecule, int iteration) = SplitGroupJoin();

while (molecule.Contains("Ar"))
{
    var firstAr = molecule.IndexOf("Ar");
    var rightAfterFirstAr = molecule.Substring(firstAr + 2);
    var test = molecule.Substring(0, firstAr);
    var lastRn = test.LastIndexOf("Rn");
    var leftBeforeLastRn = molecule.Substring(0, lastRn);
    var center = test.Substring(lastRn + 2);

    (center, iteration) = CheckReverse(center, iteration);

    var moleculeToReplace = leftBeforeLastRn + "Rn" + center + "Ar";

    var replacement = replacements.First(d => moleculeToReplace.EndsWith(d.Item2));

    moleculeToReplace = moleculeToReplace.Replace(replacement.Item2, replacement.Item1);
    molecule = moleculeToReplace + rightAfterFirstAr;
    iteration++;
}

while (molecule != "e")
{
    foreach (var replacement in replacements)
    {
        int idx = 0;
        while ((idx = molecule.IndexOf(replacement.Item2, idx)) != -1)
        {
            molecule = molecule.Substring(0, idx) + replacement.Item1 + molecule.Substring(idx + replacement.Item2.Length);

            iteration++;
        }
    }
}

Console.WriteLine($"Found after iteration {iteration}!");

List<string> Calibrate(string molecule)
{
    List<string> results = new List<string>();

    foreach (var replacement in replacements)
    {
        int idx = 0;
        while ((idx = molecule.IndexOf(replacement.Item1, idx)) != -1)
        {
            var str = molecule.Substring(0, idx) + replacement.Item2 + molecule.Substring(idx + replacement.Item1.Length);

            if (!results.Contains(str))
                results.Add(str);

            idx++;
        }
    }

    return results;
}

(string, int) CheckReverse(string data, int iteration)
{
    var list = new List<string> { data };
    while (list.Any())
    {
        list = list.SelectMany(c => ReverseEngineer(c)).Distinct().ToList();
        if (list.Any())
        {
            iteration++;

            if (list.Count == 1)
            {
                data = list[0];
            }
        }
    }

    return (data, iteration);
}

List<string> ReverseEngineer(string input)
{
    List<string> results = new List<string>();

    foreach (var replacement in replacements)
    {
        int idx = 0;
        while ((idx = input.IndexOf(replacement.Item2, idx)) != -1)
        {
            var str = input.Substring(0, idx) + replacement.Item1 + input.Substring(idx + replacement.Item2.Length);

            if (!results.Contains(str))
                results.Add(str);

            idx++;
        }
    }

    return results;
}

(string, int) SplitGroupJoin()
{
    List<string> groups = new List<string>();
    string tmpChars = string.Empty;
    for (int i = 0; i < molecule.Length; i++)
    {
        tmpChars += molecule[i];

        if (tmpChars.EndsWith("Ar") || tmpChars.EndsWith("Rn"))
        {
            if (tmpChars[..^2].Length > 0)
                groups.Add(tmpChars[..^2]);
            groups.Add(tmpChars[^2..]);
            tmpChars = string.Empty;
        }
    }

    groups.Add(tmpChars);

    int iteration = 0;
    for (int i = 0; i < groups.Count; i++)
    {
        (groups[i], iteration) = CheckReverse(groups[i], iteration);
    }

    molecule = string.Join("", groups);
    return (molecule, iteration);
}