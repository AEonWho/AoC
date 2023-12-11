
namespace AoC_Common.PathFinding
{
    public class MapPoint
    {
        public MapCoordinate Coordinates { get; }

        public long Cost { get; }

        public long CostEstimate { get; }

        public long PathCost { get; private set; }

        public long EstimatedPathCost { get; private set; }

        public MapPoint? Parent { get; private set; }

        public MapPoint(MapCoordinate coordinates, long cost, long costEstimate, MapPoint? parent = null)
        {
            Coordinates = coordinates;
            Cost = cost;
            CostEstimate = costEstimate;

            SetPath(parent);
        }

        public void SetPath(MapPoint? parent)
        {
            Parent = parent;

            if (Parent != null)
                PathCost = Parent.PathCost + Cost;
            else
                PathCost += Cost;

            EstimatedPathCost = PathCost + CostEstimate;
        }
    }
}
