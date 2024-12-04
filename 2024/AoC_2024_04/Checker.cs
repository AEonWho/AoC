using AoC_Common.PathFinding;

public class Checker
{
    public Checker(MapCoordinate start, MapDirection dir, string check = "XMAS")
    {
        Start = start;
        Direction = dir;
        Check = check;
    }

    public MapCoordinate Start { get; }

    public MapDirection Direction { get; }

    public string Check { get; }

    public IEnumerable<MapCoordinate> GetCoordinates()
    {
        yield return Start; 
        var next = Start.MoveDirection(Direction);
        yield return next;
        for (int i = 0; i < Check.Length - 2; i++)
        {
            next = next.MoveDirection(Direction);
            yield return next;
        }
    }

    public bool IsValid(Dictionary<MapCoordinate, char> map)
    {
        var coords = GetCoordinates().ToArray();

        if (coords.All(map.ContainsKey))
        {
            return new string(coords.Select(c => map[c]).ToArray()) == Check;
        }

        return false;
    }
}
