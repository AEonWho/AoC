public class WayPoint
{
    public List<WayPoint> Path { get; set; } = new List<WayPoint>();

    public WayPoint(int height)
    {
        Height = height;
    }

    public int Height { get; }

    public int CurrentPathCost => Path.Count;
}