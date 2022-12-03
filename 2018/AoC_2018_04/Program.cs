using System;

var lines = File.ReadAllLines("Input.txt").ToList();

List<(DateTime, string)> guardData = new List<(DateTime, string)>();

for (int i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    var splitted = line.Split(new string[] { "[", "] " }, StringSplitOptions.RemoveEmptyEntries);

    guardData.Add((DateTime.Parse(splitted[0]), splitted[1]));
}

guardData = guardData.OrderBy(c => c.Item1).ToList();

Dictionary<int, Guard> guards = new Dictionary<int, Guard>();
Guard currentGuard = null;
foreach (var guardEntry in guardData)
{
    if (guardEntry.Item2.StartsWith("Guard"))
    {
        if (currentGuard != null)
        {
            currentGuard.Cycle(null, guardEntry.Item1);
        }

        var id = int.Parse(guardEntry.Item2.Split(' ')[1][1..]);

        if (!guards.ContainsKey(id))
        {
            guards.Add(id, new Guard(id));
        }
        currentGuard = guards[id];
    }

    currentGuard.Cycle(guardEntry.Item2, guardEntry.Item1);
}

var sleepyGuard = guards.Select(d=>d.Value).OrderByDescending(c => c.SleepMinutes).First();

Console.WriteLine("Stage1: " + sleepyGuard.Id * sleepyGuard.SleepMinute.Item1);

var consistentSleepyGuard = guards.Select(d => d.Value).OrderByDescending(c => c.SleepMinute.Item2).First();

Console.WriteLine("Stage2: " + consistentSleepyGuard.Id * consistentSleepyGuard.SleepMinute.Item1);


public class Guard
{
    public int Id { get; }

    public List<(DateTime, DateTime?)> AwakeTimes { get; set; } = new List<(DateTime, DateTime?)>();

    public List<(DateTime, DateTime?)> SleepTimes { get; set; } = new List<(DateTime, DateTime?)>();

    public double SleepMinutes => SleepTimes.Sum(d => (d.Item2.Value - d.Item1).TotalMinutes);

    public (int, int) SleepMinute => GetSleepIntersections();

    private (int, int) GetSleepIntersections()
    {
        List<(int, int)> tmp = new List<(int, int)>();
        for (int i = 0; i < 60; i++)
        {
            var cnt = SleepTimes.Where(d => d.Item1.Minute <= i && d.Item2.Value.Minute > i).Count();

            tmp.Add((i, cnt));
        }

        return tmp.OrderByDescending(d => d.Item2).First();
    }

    public Guard(int id)
    {
        Id = id;
    }

    public void Cycle(string data, DateTime item1)
    {
        if (data == null)
        {
            if (AwakeTimes.Any() && AwakeTimes[^1].Item2 == null)
                AwakeTimes[^1] = (AwakeTimes[^1].Item1, item1);
            if (SleepTimes.Any() && SleepTimes[^1].Item2 == null)
                SleepTimes[^1] = (SleepTimes[^1].Item1, item1);
        }
        else if (data.StartsWith("Guard"))
        {
            AwakeTimes.Add((item1, null));
        }
        else if (data == "falls asleep")
        {
            AwakeTimes[^1] = (AwakeTimes[^1].Item1, item1);
            SleepTimes.Add((item1, null));
        }
        else
        {
            AwakeTimes.Add((item1, null));
            SleepTimes[^1] = (SleepTimes[^1].Item1, item1);
        }
    }
}