using System.Collections.Concurrent;

namespace AoC_Common
{
    public class AoC_Math
    {
        public static ulong GetSmallestCommonMultiple(params long[] numbers)
        {
            if (numbers.Any(d => d < 0))
                throw new NotSupportedException("Numbers smaller 0 not supported");

            return Internal_GetSmallestCommonMultiple(numbers.Select(d => (ulong)d).ToArray());
        }

        public static ulong GetSmallestCommonMultiple(params uint[] numbers)
        {
            return Internal_GetSmallestCommonMultiple(numbers.Select(d => (ulong)d).ToArray());
        }

        public static ulong GetSmallestCommonMultiple(params int[] numbers)
        {
            if (numbers.Any(d => d < 0))
                throw new NotSupportedException("Numbers smaller 0 not supported");

            return Internal_GetSmallestCommonMultiple(numbers.Select(d => (ulong)d).ToArray());
        }

        public static ulong GetSmallestCommonMultiple(params ulong[] numbers)
        {
            if (numbers.Contains(ulong.MinValue))
                throw new NotSupportedException("0 not supported");

            return Internal_GetSmallestCommonMultiple(numbers);
        }

        private static ulong Internal_GetSmallestCommonMultiple(params ulong[] numbers)
        {
            if (numbers.Contains(ulong.MinValue))
                throw new NotSupportedException("0 not supported");

            ConcurrentDictionary<ulong, uint> dict = new ConcurrentDictionary<ulong, uint>();
            foreach (ulong l in numbers.Where(x => x != 1))
            {
                var primeFactors = GetPrimeFactors(l).GroupBy(x => x).ToDictionary(d => d.Key, d => (uint)d.Count());

                foreach (var prime in primeFactors)
                {
                    dict.AddOrUpdate(prime.Key, prime.Value, (_, count) => Math.Max(count, prime.Value));
                }
            }

            return dict.Select(d => Pow(d.Key, d.Value)).Aggregate((f, s) => f * s);
        }


        public static long GetGreatestCommonDivisor(params long[] numbers)
        {
            if (numbers.Any(d => d < 0))
                throw new NotSupportedException("Numbers smaller 0 not supported");

            return (long)InternalGetGreatestCommonDivisor(numbers.Select(d => (ulong)d).ToArray());
        }

        public static uint GetGreatestCommonDivisor(params uint[] numbers)
        {
            return (uint)InternalGetGreatestCommonDivisor(numbers.Select(d => (ulong)d).ToArray());
        }

        public static int GetGreatestCommonDivisor(params int[] numbers)
        {
            if (numbers.Any(d => d < 0))
                throw new NotSupportedException("Numbers smaller 0 not supported");

            return (int)InternalGetGreatestCommonDivisor(numbers.Select(d => (ulong)d).ToArray());
        }

        public static ulong GetGreatestCommonDivisor(params ulong[] numbers)
        {
            if (numbers.Any(d => d < 0))
                throw new NotSupportedException("Numbers smaller 0 not supported");

            return InternalGetGreatestCommonDivisor(numbers);
        }

        public static ulong InternalGetGreatestCommonDivisor(params ulong[] numbers)
        {
            if (numbers.Contains(ulong.MinValue))
                throw new NotSupportedException("0 not supported");

            if (numbers.Any(c => c == 1))
                return 1;

            Dictionary<ulong, uint>? dict = null;
            foreach (ulong l in numbers)
            {
                var primeFactors = GetPrimeFactors(l).GroupBy(x => x).ToDictionary(d => d.Key, d => (uint)d.Count());

                if (dict == null)
                    dict = primeFactors;
                else
                    dict = primeFactors.Keys.Intersect(dict.Keys).ToDictionary(d => d, d => Math.Min(primeFactors[d], dict[d]));

                if (!dict.Any())
                {
                    return 1;
                }
            }

            return dict.Select(d => Pow(d.Key, d.Value)).Aggregate((f, s) => f * s);
        }

        public static ulong Pow(ulong b, uint e)
        {
            ulong result = 1;
            for (uint i = 0; i < e; ++i)
                result *= b;
            return result;
        }

        public static ulong[] GetPrimeFactors(ulong x)
        {
            if (x <= 1)
                throw new ArgumentOutOfRangeException("x", "x >= 2");

            List<ulong> list = new List<ulong>();

            while (x % 2 == 0)
            {
                list.Add(2);
                x /= 2;
            }

            for (ulong i = 3; i <= x && x != 1;)
            {
                if (x % i == 0)
                {
                    list.Add(i);
                    x /= i;
                }
                else
                {
                    i += 2;
                }
            }
            return list.ToArray(); // Als Array zurück geben
        }
    }
}
