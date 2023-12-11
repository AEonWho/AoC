using AoC_Common;

namespace AoC_Tests
{
    public class Test_AoC_Math
    {
        [Theory]
        [InlineData(8, new[] { 2, 2, 2 })]
        [InlineData(9, new[] { 3, 3 })]
        [InlineData(10, new[] { 2, 5 })]
        [InlineData(18, new[] { 2, 3, 3 })]
        [InlineData(420, new[] { 2, 2, 3, 5, 7 })]
        [InlineData(26880, new[] { 2, 2, 2, 2, 2, 2, 2, 2, 3, 5, 7 })]
        public void PrimeFactors(int number, int[] primes)
        {
            var res = AoC_Math.GetPrimeFactors((ulong)number);

            Assert.Equal(primes.Length, res.Length);
        }

        [Theory]
        [InlineData(new ulong[] { 120, 310 }, 3720)]
        [InlineData(new ulong[] { 20777, 18673, 13939, 17621, 19199, 12361 }, 17972669116327)]
        public void SmallestCommonmultiple_ulong(ulong[] primes, ulong number)
        {
            var res = AoC_Math.GetSmallestCommonMultiple(primes);

            Assert.Equal(number, res);
        }

        [Theory]
        [InlineData(new long[] { 120, 310 }, 3720)]
        [InlineData(new long[] { 20777, 18673, 13939, 17621, 19199, 12361 }, (ulong)17972669116327)]
        public void SmallestCommonmultiple_long(long[] primes, ulong number)
        {
            var res = AoC_Math.GetSmallestCommonMultiple(primes);

            Assert.Equal(number, res);
        }

        [Theory]
        [InlineData(new int[] { 120, 310 }, 3720)]
        [InlineData(new int[] { 20777, 18673, 13939, 17621, 19199, 12361 }, (ulong)17972669116327)]
        public void SmallestCommonmultiple_int(int[] primes, ulong number)
        {
            var res = AoC_Math.GetSmallestCommonMultiple(primes);

            Assert.Equal(number, res);
        }

        [Theory]
        [InlineData(new uint[] { 120, 310 }, 3720)]
        [InlineData(new uint[] { 20777, 18673, 13939, 17621, 19199, 12361 }, (ulong)17972669116327)]
        public void SmallestCommonmultiple_uint(uint[] primes, ulong number)
        {
            var res = AoC_Math.GetSmallestCommonMultiple(primes);

            Assert.Equal(number, res);
        }

        [Theory]
        [InlineData(new ulong[] { 120, 310 }, 10)]
        [InlineData(new ulong[] { 20777, 18673, 13939, 17621, 19199, 12361 }, 263)]
        public void GreatestCommonDivisor_ulong(ulong[] primes, ulong number)
        {
            var res = AoC_Math.GetGreatestCommonDivisor(primes);

            Assert.Equal(number, res);
        }

        [Theory]
        [InlineData(new long[] { 120, 310 }, 10)]
        [InlineData(new long[] { 20777, 18673, 13939, 17621, 19199, 12361 }, 263)]
        public void GreatestCommonDivisor_long(long[] primes, long number)
        {
            var res = AoC_Math.GetGreatestCommonDivisor(primes);

            Assert.Equal(number, res);
        }

        [Theory]
        [InlineData(new int[] { 120, 310 }, 10)]
        [InlineData(new int[] { 20777, 18673, 13939, 17621, 19199, 12361 }, 263)]
        public void GreatestCommonDivisor_int(int[] primes, int number)
        {
            var res = AoC_Math.GetGreatestCommonDivisor(primes);

            Assert.Equal(number, res);
        }

        [Theory]
        [InlineData(new uint[] { 120, 310 }, 10)]
        [InlineData(new uint[] { 20777, 18673, 13939, 17621, 19199, 12361 }, 263)]
        public void GreatestCommonDivisor_uint(uint[] primes, uint number)
        {
            var res = AoC_Math.GetGreatestCommonDivisor(primes);

            Assert.Equal(number, res);
        }
    }
}