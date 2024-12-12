public class Field
{
    public required char FieldId { get; init; }

    public HashSet<MapCoordinate> Area { get; } = new HashSet<MapCoordinate>();
}