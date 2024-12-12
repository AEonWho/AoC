namespace AoC_Common.PathFinding
{
    public record MapCoordinate(long X, long Y)
    {
        public static implicit operator MapCoordinate((long x, long y) data)
        {
            return new MapCoordinate(data.x, data.y);
        }

        public static implicit operator MapCoordinate((uint x, uint y) data)
        {
            return new MapCoordinate(data.x, data.y);
        }

        public static implicit operator MapCoordinate((ulong x, ulong y) data)
        {
            return new MapCoordinate((long)data.x, (long)data.y);
        }

        public IEnumerable<MapCoordinate> GetNeighbors()
        {
            yield return North();
            yield return South();
            yield return East();
            yield return West();
        }

        public IEnumerable<(MapCoordinate, MapDirection)> GetNeighborsWithDirection()
        {
            yield return (North(), MapDirection.N);
            yield return (South(), MapDirection.S);
            yield return (East(), MapDirection.E);
            yield return (West(), MapDirection.W);
        }

        public IEnumerable<MapCoordinate> GetFullNeighbors()
        {
            yield return North();
            yield return NorthEast();
            yield return East();
            yield return SouthEast();
            yield return South();
            yield return SouthWest();
            yield return West();
            yield return NorthWest();
        }

        public MapCoordinate North() => new MapCoordinate(X, Y + 1);

        public MapCoordinate NorthWest() => new MapCoordinate(X - 1, Y + 1);

        public MapCoordinate East() => new MapCoordinate(X + 1, Y);

        public MapCoordinate SouthEast() => new MapCoordinate(X + 1, Y - 1);

        public MapCoordinate South() => new MapCoordinate(X, Y - 1);

        public MapCoordinate SouthWest() => new MapCoordinate(X - 1, Y - 1);

        public MapCoordinate West() => new MapCoordinate(X - 1, Y);

        public MapCoordinate NorthEast() => new MapCoordinate(X + 1, Y + 1);

        public MapCoordinate MoveDirection(MapDirection direction)
        {
            switch (direction)
            {
                case MapDirection.N:
                    return North();
                case MapDirection.NE:
                    return NorthEast();
                case MapDirection.E:
                    return East();
                case MapDirection.SE:
                    return SouthEast();
                case MapDirection.S:
                    return South();
                case MapDirection.SW:
                    return SouthWest();
                case MapDirection.W:
                    return West();
                case MapDirection.NW:
                    return NorthWest();
            }

            throw new NotImplementedException();
        }
    };
}
