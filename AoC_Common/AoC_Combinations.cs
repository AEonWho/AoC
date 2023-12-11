namespace AoC_Common
{
    public static class AoC_Combinations
    {
        public static IEnumerable<List<T>> GetCombinations<T>(this IEnumerable<T> enumerable)
        {
            foreach (var entry in enumerable)
            {
                var remaining = enumerable.Where(c => !c!.Equals(entry));
                if (!remaining.Any())
                {
                    yield return new List<T> { entry };
                }
                else
                {
                    var tmp = GetCombinations(remaining);

                    foreach (var t in tmp)
                    {
                        t.Insert(0, entry);
                        yield return t;
                    }
                }
            }
        }
    }
}
