using System.Linq;
using System.Transactions;

var lines = File.ReadAllLines("Input.txt").ToList();

Dictionary<(int, int), WayPoint> Map = new Dictionary<(int, int), WayPoint>();

var start = (0, 0);
var end = (0, 0);

for (int i = 0; i < lines.Count; i++)
{
    var line = lines[i];

    var x = 0;
    foreach (var c in line)
    {
        if (c == 'S')
        {
            Map.Add((x, i), new WayPoint(1));
            start = (x, i);
        }
        else if (c == 'E')
        {
            Map.Add((x, i), new WayPoint(26));
            end = (x, i);
        }
        else
        {
            Map.Add((x, i), new WayPoint(c - 96));
        }

        x++;
    }
}

List<(int X, int Y)> waypoints = new List<(int x, int y)>();
waypoints.Add(start);

while (true)
{
    if (waypoints[0] == end)
        break;

    var current = waypoints[0];
    waypoints.RemoveAt(0);

    InsertWaypoint(current, current with { X = current.X + 1 });
    InsertWaypoint(current, current with { X = current.X - 1 });
    InsertWaypoint(current, current with { Y = current.Y + 1 });
    InsertWaypoint(current, current with { Y = current.Y - 1 });
}

Console.WriteLine("Stage1: " + Map[waypoints[0]].CurrentPathCost);


var test = Map.Where(d => d.Value.Height == 1).Select(d => d.Key).ToList();

List<int> bestWays = new List<int>();

foreach (var entry in test)
{
    Map.Values.ToList().ForEach(d => d.Path.Clear());

    waypoints = new List<(int X, int Y)> { entry };

    while (true)
    {
        if (!waypoints.Any() || waypoints[0] == end)
            break;

        var current = waypoints[0];
        waypoints.RemoveAt(0);

        InsertWaypoint(current, current with { X = current.X + 1 });
        InsertWaypoint(current, current with { X = current.X - 1 });
        InsertWaypoint(current, current with { Y = current.Y + 1 });
        InsertWaypoint(current, current with { Y = current.Y - 1 });
    }

    if (waypoints.Any())
    {
        bestWays.Add(Map[waypoints[0]].CurrentPathCost);
    }
}

Console.WriteLine("Stage2: " + bestWays.Min());

void InsertWaypoint((int, int Y) currentPoint, (int X, int Y) nextPoint)
{
    if (!Map.ContainsKey(nextPoint))
        return;

    var currentWaypoint = Map[currentPoint];
    var next = Map[nextPoint];

    if (next.Height > currentWaypoint.Height + 1)
        return;

    if (next.CurrentPathCost != 0 && next.CurrentPathCost < currentWaypoint.CurrentPathCost)
        return;

    next.Path = currentWaypoint.Path.ToList();
    next.Path.Add(currentWaypoint);

    waypoints.Remove(nextPoint);

    for (int i = 0; i <= waypoints.Count; i++)
    {
        if (i == waypoints.Count)
        {
            waypoints.Add(nextPoint);
            break;
        }
        else if (next.CurrentPathCost < Map[waypoints[i]].CurrentPathCost)
        {
            waypoints.Insert(i, nextPoint);
            break;
        }
    }
}