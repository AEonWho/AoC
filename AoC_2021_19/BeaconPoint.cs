using System.Numerics;

public class BeaconPoint
{
    private readonly Dictionary<BeaconPoint, Vector3> _distances;

    public BeaconPoint(string[] coords, Scanner scanner)
    {
        _distances = new Dictionary<BeaconPoint, Vector3>();
        Position = new Vector3(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]));
        Scanner = scanner;
    }

    public Vector3 Position { get; }
    public Scanner Scanner { get; }

    public IDictionary<BeaconPoint, Vector3> Distances => _distances;

    public Vector3 NormalizedPosition { get; private set; }

    internal void AddDistance(BeaconPoint secondBeacon)
    {
        if (secondBeacon == this)
            return;

        if (Distances.ContainsKey(secondBeacon))
            return;

        var vector = Position - secondBeacon.Position;
        _distances.Add(secondBeacon, vector);
    }

    internal void SetNormalizedPosition()
    {
        var vectorFromProbe = Position;

        if (Scanner.Rotation != null)
            vectorFromProbe = Vector3.Transform(vectorFromProbe, Scanner.Rotation.Value);
        vectorFromProbe = new Vector3((int)Math.Round(vectorFromProbe.X), (int)Math.Round(vectorFromProbe.Y), (int)Math.Round(vectorFromProbe.Z));

        NormalizedPosition = Vector3.Add(Scanner.NormalizedPosition, vectorFromProbe);
    }
}
