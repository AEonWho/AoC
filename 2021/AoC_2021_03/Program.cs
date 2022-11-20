var lines = File.ReadAllLines("Input.txt");

string gamma = string.Empty;
string epsilon = string.Empty;

var length = lines[0].Length;
for (int i = 0; i < length; i++)
{
    var group = lines.Select(c => c[i]).GroupBy(d => d);
    gamma += group.OrderByDescending(c => c.Count()).First().Key;
    epsilon += group.OrderBy(c => c.Count()).First().Key;
}

Console.WriteLine($"Gamma: {gamma}");
Console.WriteLine($"Epsilon: {epsilon}");

Console.WriteLine($"Gamma: {Convert.ToInt32(gamma, 2)}");
Console.WriteLine($"Epsilon: {Convert.ToInt32(epsilon, 2)}");

Console.WriteLine($"Power: {Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)}");

List<string> oxygen = lines.ToList();
List<string> carbondioxid = lines.ToList();

for (int i = 0; i < length; i++)
{
    if (oxygen.Count != 1)
    {
        var groupOxygen = oxygen.Select(c => c[i]).GroupBy(d => d).ToList();

        char filterOxygen;
        if (groupOxygen.Count > 1 && groupOxygen[0].Count() == groupOxygen[1].Count())
        {
            filterOxygen = '1';
        }
        else
        {
            filterOxygen = groupOxygen.OrderByDescending(c => c.Count()).First().Key;
        }

        oxygen = oxygen.Where(c => c[i] == filterOxygen).ToList();
    }

    if (carbondioxid.Count != 1)
    {
        var groupCarbondioxid = carbondioxid.Select(c => c[i]).GroupBy(d => d).ToList();
        char filterCarbondioxid;
        if (groupCarbondioxid.Count > 1 && groupCarbondioxid[0].Count() == groupCarbondioxid[1].Count())
        {
            filterCarbondioxid = '0';
        }
        else
        {
            filterCarbondioxid = groupCarbondioxid.OrderBy(c => c.Count()).First().Key;
        }

        carbondioxid = carbondioxid.Where(c => c[i] == filterCarbondioxid).ToList();
    }
}

Console.WriteLine($"Oxygen: {oxygen[0]}");
Console.WriteLine($"Carbondioxid: {carbondioxid[0]}");

Console.WriteLine($"Oxygen: {Convert.ToInt32(oxygen[0], 2)}");
Console.WriteLine($"Carbondioxid: {Convert.ToInt32(carbondioxid[0], 2)}");

Console.WriteLine($"Life support rating: {Convert.ToInt32(oxygen[0], 2) * Convert.ToInt32(carbondioxid[0], 2)}");