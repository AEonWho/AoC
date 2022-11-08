






public class WayPoint
{
    public List<WayPoint> Path { get; set; } = new List<WayPoint>();

    public WayPoint(int cost)
    {
        while (cost > 9)
            cost = cost - 9;

        Cost = cost;
    }

    public int Cost { get; }

    public int? CurrentPathCost { get; set; }
}