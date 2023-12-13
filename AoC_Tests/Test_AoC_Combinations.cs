using AoC_Common;

namespace AoC_Tests
{
    public class Test_AoC_Combinations
    {
        [Fact]
        public void Combinations()
        {
            List<int> start = [1, 2, 3];

            var result = start.GetCombinations().ToList();

            Assert.Equal(6, result.Count);

            Assert.Equal(1, result[0][0]);
            Assert.Equal(2, result[0][1]);
            Assert.Equal(3, result[0][2]);

            Assert.Equal(1, result[1][0]);
            Assert.Equal(3, result[1][1]);
            Assert.Equal(2, result[1][2]);

            Assert.Equal(2, result[2][0]);
            Assert.Equal(1, result[2][1]);
            Assert.Equal(3, result[2][2]);

            Assert.Equal(2, result[3][0]);
            Assert.Equal(3, result[3][1]);
            Assert.Equal(1, result[3][2]);

            Assert.Equal(3, result[4][0]);
            Assert.Equal(1, result[4][1]);
            Assert.Equal(2, result[4][2]);

            Assert.Equal(3, result[5][0]);
            Assert.Equal(2, result[5][1]);
            Assert.Equal(1, result[5][2]);
        }

        [Fact]
        public void Combinations_Sub2()
        {
            List<int> start = [1, 2, 1];

            var result = start.GetCombinations().ToList();

            Assert.Equal(3, result.Count);

            Assert.Equal(1, result[0][0]);
            Assert.Equal(2, result[0][1]);
            Assert.Equal(1, result[0][2]);

            Assert.Equal(1, result[1][0]);
            Assert.Equal(1, result[1][1]);
            Assert.Equal(2, result[1][2]);

            Assert.Equal(2, result[2][0]);
            Assert.Equal(1, result[2][1]);
            Assert.Equal(1, result[2][2]);
        }
    }
}