
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;
using Xunit;

namespace MPT.Math.xUnitTests
{
    public class UnitTest1
    {

        [Fact]
        public void GoldenRatio()
        {
            Assert.Equal(1.61803398875, NMath.Round(Numbers.GoldenRatio(), 11));
        }

        [Theory]
        [InlineData(-0.001, 0.1, false)]
        [InlineData(0, 0.1, false)]
        [InlineData(0.001, 0.1, false)]
        [InlineData(0.001, 0.0001, true)]
        [InlineData(0.001, -0.0001, true)]
        public void IsPositiveSign_Double_Custom_Tolerance(double number, double tolerance, bool expected)
        {
            Assert.Equal(expected, Numbers.IsPositiveSign(number, tolerance));
        }
    }
}
