var lines = File.ReadAllLines("Input.txt");

var reindeers = lines.Select(c => new Reindeer(c)).ToList();

for (int i = 0; i < 2503; i++)
{
    reindeers.ForEach(d => d.Tick());

    reindeers.GroupBy(d => d.DistanceTraveled).OrderByDescending(c => c.Key).First().ToList().ForEach(c => c.AddPoint());
}

reindeers.ForEach(d => Console.WriteLine($"{d.Name} traveled {d.DistanceTraveled} km with {d.Points} Points."));


class Reindeer
{
    public int TimeElapsed { get; private set; } = 0;
    public int Points { get; private set; } = 0;

    public int DistanceTraveled { get; private set; } = 0;

    public Reindeer(string line)
    {
        var x = line.Split(' ');
        Name = x[0];

        Speed = int.Parse(x[3]);

        SprintTime = int.Parse(x[6]);

        RestTime = int.Parse(x[^2]);
    }

    public string Name { get; }

    public int Speed { get; }

    public int SprintTime { get; }

    public int RestTime { get; }

    public void Tick()
    {
        var duration = SprintTime + RestTime;

        var timeIndex = TimeElapsed % duration;

        if (timeIndex < SprintTime)
        {
            DistanceTraveled += Speed;
        }

        TimeElapsed++;
    }

    internal void AddPoint()
    {
        Points++;
    }
}