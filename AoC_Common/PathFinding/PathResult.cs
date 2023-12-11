

namespace AoC_Common.PathFinding
{
    public class PathResult : List<MapCoordinate>
    {
        public IReadOnlyDictionary<MapCoordinate, MapPoint> Map { get; }

        public MapCoordinate[]? Path { get; }

        public long Cost { get; }

        internal PathResult(Dictionary<MapCoordinate, MapPoint> map, MapCoordinate end)
        {
            Map = map.AsReadOnly();
            if (map.TryGetValue(end, out var result))
            {
                Path = GetPath(result).Reverse().ToArray();
                Cost = result.PathCost;
            }
        }

        private IEnumerable<MapCoordinate> GetPath(MapPoint result)
        {
            var entry = result;
            while(entry != null)
            {
                yield return entry.Coordinates;
                entry = entry.Parent;
            }
        }
    }
}