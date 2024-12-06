namespace AoC_Common.PathFinding
{
    public static class MapDirectionExtension
    {
        public static MapDirection Rotate90(this MapDirection direction)
        {
            switch (direction)
            {
                case MapDirection.N:
                    return MapDirection.E;
                case MapDirection.E:
                    return MapDirection.S;
                case MapDirection.S:
                    return MapDirection.W;
                case MapDirection.W:
                    return MapDirection.N;
            }

            throw new NotSupportedException();
        }
        public static MapDirection Rotate180(this MapDirection direction)
        {
            switch (direction)
            {
                case MapDirection.N:
                    return MapDirection.S;
                case MapDirection.E:
                    return MapDirection.W;
                case MapDirection.S:
                    return MapDirection.N;
                case MapDirection.W:
                    return MapDirection.E;
            }

            throw new NotSupportedException();
        }
        public static MapDirection Rotate270(this MapDirection direction)
        {
            switch (direction)
            {
                case MapDirection.N:
                    return MapDirection.W;
                case MapDirection.E:
                    return MapDirection.N;
                case MapDirection.S:
                    return MapDirection.E;
                case MapDirection.W:
                    return MapDirection.S;
            }

            throw new NotSupportedException();
        }
    }
}
