using MPT.Math.Coordinates;
using MPT.Math.Trigonometry;
using NUnit.Framework;
namespace MPT.Math.UnitTests.Trigonometry
{
    [TestFixture]
    public static class TrigonometryLibraryTests
    {
        public static double Tolerance = 0.00001;

        // TODO: Test multiples of n*pi

        #region Angles
        
        #endregion

        #region Angle/Ratio Methods (Sin, Cos, Tan, etc.)
        [TestCase(0, 0)]
        [TestCase(45, 0.707107)]
        [TestCase(90, 1)]
        [TestCase(135, 0.707107)]
        [TestCase(180, 0)]
        [TestCase(225, -0.707107)]
        [TestCase(270, -1)]
        [TestCase(315, -0.707107)]
        [TestCase(360, 0)]
        [TestCase(30, 0.5)]
        [TestCase(60, 0.866025)]
        [TestCase(-45, -0.707107)]
        [TestCase(-90, -1)]
        [TestCase(-135, -0.707107)]
        [TestCase(-180, 0)]
        [TestCase(-225, 0.707107)]
        [TestCase(-270, 1)]
        [TestCase(-315, 0.707107)]
        [TestCase(-360, 0)]
        public static void Sin(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.Sin(radians), Tolerance); 
        }

        [TestCase(0, 1)]
        [TestCase(45, 0.707107)]
        [TestCase(90, 0)]
        [TestCase(135, -0.707107)]
        [TestCase(180, -1)]
        [TestCase(225, -0.707107)]
        [TestCase(270, 0)]
        [TestCase(315, 0.707107)]
        [TestCase(360, 1)]
        [TestCase(30, 0.866025)]
        [TestCase(60, 0.5)]
        [TestCase(-45, 0.707107)]
        [TestCase(-90, 0)]
        [TestCase(-135, -0.707107)]
        [TestCase(-180, -1)]
        [TestCase(-225, -0.707107)]
        [TestCase(-270, 0)]
        [TestCase(-315, 0.707107)]
        [TestCase(-360, 1)]
        public static void Cos(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.Cos(radians), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(45, 1)]
        [TestCase(90, double.PositiveInfinity)]
        [TestCase(135, -1)]
        [TestCase(180, 0)]
        [TestCase(225, 1)]
        [TestCase(270, double.PositiveInfinity)]
        [TestCase(315, -1)]
        [TestCase(360, 0)]
        [TestCase(30, 0.57735)]
        [TestCase(60, 1.732051)]
        [TestCase(-45, -1)]
        [TestCase(-90, double.NegativeInfinity)]
        [TestCase(-135, 1)]
        [TestCase(-180, 0)]
        [TestCase(-225, -1)]
        [TestCase(-270, double.NegativeInfinity)]
        [TestCase(-315, 1)]
        [TestCase(-360, 0)]
        public static void Tan(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.Tan(radians), Tolerance);
        }

        [TestCase(1, 1.570796)]
        [TestCase(0.707107, 0.785398)]
        [TestCase(0, 0)]
        [TestCase(-0.707107, -0.785398)]
        [TestCase(-1, -1.570796)]
        [TestCase(0.447214, 0.463648)]
        [TestCase(0.894427, 1.107149)]
        public static void ArcSin(double ratio, double expectedResult)
        {
            Assert.AreEqual(expectedResult, TrigonometryLibrary.ArcSin(ratio), Tolerance);
        }

        [TestCase(1, 0)]
        [TestCase(-0.707107, 2.356194)]
        [TestCase(-1, 3.141593)]
        [TestCase(0, 1.570796)]
        [TestCase(0.707107, 0.785398)]
        [TestCase(0.894427, 0.463648)]
        [TestCase(0.447214, 1.107149)]
        public static void ArcCos(double ratio, double expectedResult)
        {
            Assert.AreEqual(expectedResult, TrigonometryLibrary.ArcCos(ratio), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(1, 0.785398)]
        [TestCase(-1, -0.785398)]
        [TestCase(0.5, 0.463648)]
        [TestCase(2, 1.107149)]
        public static void ArcTan(double ratio, double expectedResult)
        {
            Assert.AreEqual(expectedResult, TrigonometryLibrary.ArcTan(ratio), Tolerance);
        }

        [TestCase(0, 1)]
        [TestCase(45, 1.414214)]
        [TestCase(90, double.PositiveInfinity)]
        [TestCase(135, -1.414214)]
        [TestCase(180, -1)]
        [TestCase(225, -1.414214)]
        [TestCase(270, double.NegativeInfinity)]
        [TestCase(315, 1.414214)]
        [TestCase(360, 1)]
        [TestCase(30, 1.154701)]
        [TestCase(60, 2)]
        [TestCase(-45, 1.414214)]
        [TestCase(-90, double.PositiveInfinity)]
        [TestCase(-135, -1.414214)]
        [TestCase(-180, -1)]
        [TestCase(-225, -1.414214)]
        [TestCase(-270, double.NegativeInfinity)]
        [TestCase(-315, 1.414214)]
        [TestCase(-360, 1)]
        public static void Sec(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.Sec(radians), Tolerance);
        }

        [TestCase(0, double.PositiveInfinity)]
        [TestCase(45, 1.414214)]
        [TestCase(90, 1)]
        [TestCase(135, 1.414214)]
        [TestCase(180, double.PositiveInfinity)]
        [TestCase(225, -1.414214)]
        [TestCase(270, -1)]
        [TestCase(315, -1.414214)]
        [TestCase(360, double.NegativeInfinity)]
        [TestCase(30, 2)]
        [TestCase(60, 1.154701)]
        [TestCase(-45, -1.414214)]
        [TestCase(-90, -1)]
        [TestCase(-135, -1.414214)]
        [TestCase(-180, double.NegativeInfinity)]
        [TestCase(-225, 1.414214)]
        [TestCase(-270, 1)]
        [TestCase(-315, 1.414214)]
        [TestCase(-360, double.PositiveInfinity)]
        public static void Csc(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.Csc(radians), Tolerance);
        }

        [TestCase(0, double.PositiveInfinity)]
        [TestCase(45, 1)]
        [TestCase(90, 0)]
        [TestCase(135, -1)]
        [TestCase(180, double.NegativeInfinity)]
        [TestCase(225, 1)]
        [TestCase(270, 0)]
        [TestCase(315, -1)]
        [TestCase(360, double.NegativeInfinity)]
        [TestCase(30, 1.732051)]
        [TestCase(60, 0.57735)]
        [TestCase(-45, -1)]
        [TestCase(-90, 0)]
        [TestCase(-135, 1)]
        [TestCase(-180, double.PositiveInfinity)]
        [TestCase(-225, -1)]
        [TestCase(-270, 0)]
        [TestCase(-315, 1)]
        [TestCase(-360, double.PositiveInfinity)]
        public static void Cot(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.Cot(radians), Tolerance);
        }
        #endregion
    }
}
