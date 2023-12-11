using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using AoC_Common.PathFinding;

namespace AoC_Common
{
    public static class AoC_PathFinding
    {
        public static PathResult FindPath_Directed(MapCoordinate start, MapCoordinate end, Func<MapCoordinate, long>? costFunction = null, Func<MapCoordinate, bool>? validateCoordinate = null, Action<IReadOnlyDictionary<MapCoordinate, MapPoint>>? debug = null)
        {
            costFunction ??= d => d == start ? 0 : 1;
            validateCoordinate ??= d => true;

            return InternalFindPath(start, end, costFunction, d => d.GetNeighbors(), validateCoordinate, (coord1, coord2) => coord1.Distance(coord2), debug);
        }

        public static PathResult FindPath_Diagonal(MapCoordinate start, MapCoordinate end, Func<MapCoordinate, long>? costFunction = null, Func<MapCoordinate, bool>? validateCoordinate = null, Action<IReadOnlyDictionary<MapCoordinate, MapPoint>>? debug = null)
        {
            costFunction ??= d => d == start ? 0 : 1;

            validateCoordinate ??= d => true;

            return InternalFindPath(start, end, costFunction, d => d.GetFullNeighbors(), validateCoordinate, (coord1, coord2) => coord1.DistanceDiagonal(coord2), debug);
        }

        public static PathResult FindPath(MapCoordinate start, MapCoordinate end, Func<MapCoordinate, IEnumerable<MapCoordinate>> neighborFunction, Func<MapCoordinate, MapCoordinate, long> costEstimate, Func<MapCoordinate, long>? costFunction = null, Func<MapCoordinate, bool>? validateCoordinate = null, Action<IReadOnlyDictionary<MapCoordinate, MapPoint>>? debug = null)
        {
            costFunction ??= d => d == start ? 0 : 1;
            validateCoordinate ??= d => true;

            return InternalFindPath(start, end, costFunction, neighborFunction, validateCoordinate, costEstimate, debug);
        }

        private static PathResult InternalFindPath(MapCoordinate start, MapCoordinate end, Func<MapCoordinate, long> getCost, Func<MapCoordinate, IEnumerable<MapCoordinate>> getNeighbors, Func<MapCoordinate, bool> validateCoordinate, Func<MapCoordinate, MapCoordinate, long> costEstimate, Action<IReadOnlyDictionary<MapCoordinate, MapPoint>>? debug)
        {
            Dictionary<MapCoordinate, MapPoint> map = new Dictionary<MapCoordinate, MapPoint>()
            {
                {start, new MapPoint(start, getCost(start), 0) }
            };

            PriorityQueue<MapPoint, long> knownPoints = new PriorityQueue<MapPoint, long>();
            knownPoints.Enqueue(map[start], 0);

            while (!map.ContainsKey(end) && knownPoints.Count > 0)
            {
                var current = knownPoints.Dequeue();
                var nextPoints = getNeighbors(current.Coordinates).Where(validateCoordinate);

                foreach (var point in nextPoints)
                {
                    if (map.TryGetValue(point, out MapPoint? mapPoint))
                    {
                        if (mapPoint.PathCost <= current.PathCost + mapPoint.Cost)
                        {
                            continue;
                        }
                        mapPoint.SetPath(current);
                    }
                    else
                    {
                        mapPoint = new MapPoint(point, getCost(point), costEstimate(point, end), current);
                        map.Add(point, mapPoint);
                    }

                    knownPoints.Enqueue(mapPoint, mapPoint.EstimatedPathCost);

                    if (debug != null)
                        debug(map);
                }
            }

            return new PathResult(map, end);
        }
    }
}
