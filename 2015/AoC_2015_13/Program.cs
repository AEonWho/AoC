var lines = File.ReadAllLines("Input.txt");

List<string> persons = new List<string>();

Dictionary<(string, string), int> likeData = new Dictionary<(string, string), int>();

foreach (var line in lines)
{
    var data = line.Replace("gain ", "+").Replace("lose ", "-").Split(' ', '.');

    if (!persons.Contains(data[0]))
        persons.Add(data[0]);

    if (!persons.Contains(data[^2]))
        persons.Add(data[^2]);

    likeData.Add((data[0], data[^2]), int.Parse(data[2]));
}

var tmp = GetSeatings(persons).ToList();

var seating = tmp.Select(d => new { Persons = d, Like = GetLength(d) }).OrderByDescending(c => c.Like).FirstOrDefault();

Console.WriteLine($"Max: {seating.Like} ({string.Join(", ", seating.Persons)})");

persons.Add("@Me");
tmp = GetSeatings(persons).ToList();

seating = tmp.Select(d => new { Persons = d, Like = GetLength(d) }).OrderByDescending(c => c.Like).FirstOrDefault();

Console.WriteLine($"Max: {seating.Like} ({string.Join(", ", seating.Persons)})");

IEnumerable<List<string>> GetSeatings(IEnumerable<string> enumerable)
{
    foreach (var entry in enumerable)
    {
        if (!enumerable.Where(c => c != entry).Any())
        {
            yield return new List<string> { entry };
        }
        else
        {
            var tmp = GetSeatings(enumerable.Where(c => c != entry));

            foreach (var t in tmp)
            {
                t.Insert(0, entry);
                yield return t;
            }
        }
    }
}


int? GetLength(List<string> persons)
{
    int liveValue = 0;
    for (int i = 0; i < persons.Count; i++)
    {
        var person = persons[i];
        var personRight = persons[0];
        var personLeft = persons[^1];

        if (i != persons.Count - 1)
        {
            personRight = persons[i + 1];
        }
        if (i != 0)
        {
            personLeft = persons[i - 1];
        }

        if (likeData.ContainsKey((person, personRight)))
        {
            liveValue += likeData[(person, personRight)];
        }

        if (likeData.ContainsKey((person, personLeft)))
        {
            liveValue += likeData[(person, personLeft)];
        }
    }

    return liveValue;
}