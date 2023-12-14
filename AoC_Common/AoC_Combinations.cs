namespace AoC_Common
{
    public static class AoC_Combinations
    {
        public static IEnumerable<List<T>> GetCombinations<T>(this IEnumerable<T> enumerable)
        {
            var index = 0;

            HashSet<T> checkedEntries = new HashSet<T>();

            foreach (var entry in enumerable)
            {
                if (checkedEntries.Contains(entry))
                {
                    continue;
                }
                checkedEntries.Add(entry);

                var remaining = enumerable.Where((_, i) => i != index);
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
                index++;
            }
        }
    }
}
