Dictionary<(int X, int Y), WayPoint> data = new Dictionary<(int X, int Y), WayPoint>();

var rows = File.ReadAllLines("Input.txt");

var maxColumns = rows[0].Length * 5;
var maxRows = rows.Length * 5;
int[,] arr = new int[maxColumns, maxRows];

for (int yMul = 0; yMul < 5; yMul++)
{
    for (int xMul = 0; xMul < 5; xMul++)
    {
        for (int y = 0; y < maxRows / 5; y++)
        {
            for (int x = 0; x < maxColumns / 5; x++)
            {
                data[(x + (maxColumns / 5 * xMul), y + (maxRows / 5 * yMul))] = new WayPoint(int.Parse(rows[y][x].ToString()) + yMul + xMul);
            }
        }
    }
}

List<(int X, int Y)> waypoints = new List<(int x, int y)>();
waypoints.Add((0, 0));
data[(0, 0)].CurrentPathCost = 0;

while (true)
{
    if (waypoints[0].X == maxColumns - 1 && waypoints[0].Y == maxRows - 1)
        break;

    var current = waypoints[0];
    waypoints.RemoveAt(0);

    //checkRight
    if (current.X != maxColumns - 1)
    {
        var nextPoint = (current.X + 1, current.Y);
        var currentPoint = (current.X, current.Y);
        InsertWaypoint(data, waypoints, nextPoint, currentPoint);
    }

    //checkDown
    if (current.Y != maxRows - 1)
    {
        var nextPoint = (current.X, current.Y + 1);
        var currentPoint = (current.X, current.Y);
        InsertWaypoint(data, waypoints, nextPoint, currentPoint);
    }

    //checkLeft
    if (current.X != 0)
    {
        var nextPoint = (current.X + -1, current.Y);
        var currentPoint = (current.X, current.Y);
        InsertWaypoint(data, waypoints, nextPoint, currentPoint);
    }

    //checkUp
    if (current.Y != 0)
    {
        var nextPoint = (current.X, current.Y - 1);
        var currentPoint = (current.X, current.Y);
        InsertWaypoint(data, waypoints, nextPoint, currentPoint);
    }
}

var finishedPath = data[(maxColumns - 1, maxRows - 1)];
Console.WriteLine(finishedPath.CurrentPathCost);

static void InsertWaypoint(Dictionary<(int X, int Y), WayPoint> data, List<(int X, int Y)> waypoints, (int, int Y) nextPoint, (int X, int Y) currentPoint)
{
    var currentWaypoint = data[currentPoint];
    var next = data[nextPoint];
    if (next.CurrentPathCost == null || currentWaypoint.CurrentPathCost + next.Cost < next.CurrentPathCost)
    {
        next.Path = currentWaypoint.Path.ToList();
        next.Path.Add(currentWaypoint);
        if (next.CurrentPathCost != null)
        {
            waypoints.Remove(nextPoint);
        }
        next.CurrentPathCost = currentWaypoint.CurrentPathCost + next.Cost;

        for (int i = 0; i <= waypoints.Count; i++)
        {
            if (i == waypoints.Count)
            {
                waypoints.Add(nextPoint);
                break;
            }
            else if (next.CurrentPathCost < data[waypoints[i]].CurrentPathCost)
            {
                waypoints.Insert(i, nextPoint);
                break;
            }
        }
    }
}