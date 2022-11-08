using System.Numerics;

public class Scanner
{
    private readonly List<BeaconPoint> beacons;

    public Dictionary<Scanner, List<(BeaconPoint, BeaconPoint)>> Overlap { get; }

    public Scanner(string name)
    {
        Name = name;
        beacons = new List<BeaconPoint>();
        Overlap = new Dictionary<Scanner, List<(BeaconPoint, BeaconPoint)>>();
    }

    public IReadOnlyList<BeaconPoint> Beacons => beacons;

    public string Name { get; }

    public bool RotationSet { get; set; }

    public Vector3 NormalizedPosition { get; internal set; }
    public Quaternion? Rotation { get; internal set; }

    internal void AddBeacon(string[] coords)
    {
        beacons.Add(new BeaconPoint(coords, this));
    }

    internal void CalculateDistances()
    {
        foreach (var beacon in Beacons)
        {
            foreach (var secondBeacon in Beacons)
            {
                beacon.AddDistance(secondBeacon);
            }
        }
    }
}