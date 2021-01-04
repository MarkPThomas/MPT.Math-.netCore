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

        #region Hyperbolics
        [TestCase(0, 0)]
        [TestCase(45, 0.868671)]
        [TestCase(90, 2.301299)]
        [TestCase(135, 5.227972)]
        [TestCase(180, 11.548739)]
        [TestCase(225, 25.367158)]
        [TestCase(270, 55.654398)]
        [TestCase(315, 122.073484)]
        [TestCase(360, 267.744894)]
        [TestCase(30, 0.547853)]
        [TestCase(60, 1.249367)]
        [TestCase(-45, -0.868671)]
        [TestCase(-90, -2.301299)]
        [TestCase(-135, -5.227972)]
        [TestCase(-180, -11.548739)]
        [TestCase(-225, -25.367158)]
        [TestCase(-270, -55.654398)]
        [TestCase(-315, -122.073484)]
        [TestCase(-360, -267.744894)]
        public static void SinH(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.SinH(radians), Tolerance);
        }

        [TestCase(0, 1)]
        [TestCase(45, 1.324609)]
        [TestCase(90, 2.509178)]
        [TestCase(135, 5.322752)]
        [TestCase(180, 11.591953)]
        [TestCase(225, 25.386861)]
        [TestCase(270, 55.663381)]
        [TestCase(315, 122.077579)]
        [TestCase(360, 267.746761)]
        [TestCase(30, 1.140238)]
        [TestCase(60, 1.600287)]
        [TestCase(-45, 1.324609)]
        [TestCase(-90, 2.509178)]
        [TestCase(-135, 5.322752)]
        [TestCase(-180, 11.591953)]
        [TestCase(-225, 25.386861)]
        [TestCase(-270, 55.663381)]
        [TestCase(-315, 122.077579)]
        [TestCase(-360, 267.746761)]
        public static void CosH(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.CosH(radians), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(45, 0.655794)]
        [TestCase(90, 0.917152)]
        [TestCase(135, 0.982193)]
        [TestCase(180, 0.996272)]
        [TestCase(225, 0.999224)]
        [TestCase(270, 0.999839)]
        [TestCase(315, 0.999966)]
        [TestCase(360, 0.999993)]
        [TestCase(30, 0.480473)]
        [TestCase(60, 0.780714)]
        [TestCase(-45, -0.655794)]
        [TestCase(-90, -0.917152)]
        [TestCase(-135, -0.982193)]
        [TestCase(-180, -0.996272)]
        [TestCase(-225, -0.999224)]
        [TestCase(-270, -0.999839)]
        [TestCase(-315, -0.999966)]
        [TestCase(-360, -0.999993)]
        public static void TanH(double degrees, double expectedResult)
        {
            double radians = Angle.DegreesToRadians(degrees);
            Assert.AreEqual(expectedResult, TrigonometryLibrary.TanH(radians), Tolerance);
        }
        #endregion
    }
}
