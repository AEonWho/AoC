using System.Runtime.CompilerServices;

var input = 36000000;

int i = 1;
while (true)
{
    var sum = GetDivisors(i).Sum() * 10;
    if (sum >= input)
    {
        break;
    }

    i++;
}

Console.WriteLine("Stage1 - House number: " + i);

HashSet<int> burnedout = new HashSet<int>();
Dictionary<int, int> elves = new Dictionary<int, int>();

i = 1;
while (true)
{
    var divisors = GetDivisors(i).ToList();

    int sum = 0;
    foreach (var entry in divisors)
    {
        if (burnedout.Contains(entry))
            continue;

        if (elves.ContainsKey(entry))
        {
            if (elves[entry] == 50)
            {
                burnedout.Add(entry);
                elves.Remove(entry);
                continue;
            }
            elves[entry]++;
        }
        else
        {
            elves.Add(entry, 1);
        }

        sum += entry * 11;
    }

    if (sum >= input)
    {
        break;
    }

    i++;
}

Console.WriteLine("Stage1 - House number: " + i);

IEnumerable<int> GetDivisors(int number)
{
    for (int i = 1; i <= Math.Sqrt(number); i++)
    {
        if (number % i == 0)
        {
            yield return i;
            if (i != number / i)
            {
                yield return number / i;
            }
        }
    }
}