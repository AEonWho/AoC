using System.Runtime.CompilerServices;

namespace AoC_Common.PathFinding
{
    public static class MapCoordinateExtensions
    {
        public static long Distance(this MapCoordinate a, MapCoordinate b)
        {
            var distanceX = Math.Abs(a.X - b.X);
            var distanceY = Math.Abs(a.Y - b.Y);

            return distanceX + distanceY;
        }

        public static long DistanceDiagonal(this MapCoordinate a, MapCoordinate b)
        {
            return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
        }
    }
}
