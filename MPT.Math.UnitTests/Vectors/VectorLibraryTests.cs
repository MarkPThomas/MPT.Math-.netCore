using System;
using MPT.Math.Vectors;
using NUnit.Framework;

namespace MPT.Math.UnitTests.Vectors
{
    [TestFixture]
    public static class VectorLibraryTests
    {
        public static double Tolerance = 0.00001;

        #region 2D Vectors
        [TestCase(0, 0, 1, 0, 1)]
        [TestCase(0, 0, 0, 1, 1)]
        [TestCase(0, 0, 1, 1, 1.414214)]
        [TestCase(1, 1, 2, 2, 1.414214)]
        [TestCase(-1, 1, -2, 2, 1.414214)]
        [TestCase(-1, -1, -2, -2, 1.414214)]
        [TestCase(1, -1, 2, -2, 1.414214)]
        public static void Magnitude(double x1, double y1, double x2, double y2, double expectedResult)
        {
            double xComponent = x2 - x1;
            double yComponent = y2 - y1;
            Assert.AreEqual(expectedResult, VectorLibrary.Magnitude(xComponent, yComponent), Tolerance);
        }

        [Test]
        public static void Magnitude_Throws_ArgumentException_for_Zero_Magnitude()
        {
            Assert.Throws<ArgumentException>(() => VectorLibrary.Magnitude(0, 0));
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 1, 0, 1)]
        [TestCase(1, 1, 1, 1, 2)]
        [TestCase(1, 2, 3, 4, 11)]
        [TestCase(-1, -2, -3, -4, 11)]
        [TestCase(1, -2, 3, 4, -5)]
        [TestCase(1.1, -2.2, 3.3, 4.4, -6.05)]
        public static void DotProduct(double x1, double y1, double x2, double y2, double expectedResult)
        {
            Assert.AreEqual(expectedResult, VectorLibrary.DotProduct(x1, y1, x2, y2), Tolerance); 
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 1, 0, 0)]
        [TestCase(1, 1, 1, 1, 0)]
        [TestCase(1, 2, 3, 4, -2)]
        [TestCase(-1, -2, -3, -4, -2)]
        [TestCase(1, -2, 3, 4, 10)]
        [TestCase(1.1, -2.2, 3.3, 4.4, 12.1)]
        public static void CrossProduct(double x1, double y1, double x2, double y2, double expectedResult)
        {
            Assert.AreEqual(expectedResult, VectorLibrary.CrossProduct(x1, y1, x2, y2), Tolerance);
        }
        #endregion

        #region 3D Vectors
        [TestCase(0, 0, 0, 1, 0, 0, 1)]
        [TestCase(0, 0, 0, 0, 1, 0, 1)]
        [TestCase(0, 0, 0, 1, 1, 0, 1.414214)]
        [TestCase(1, 1, 0, 2, 2, 0, 1.414214)]
        [TestCase(-1, 1, 0, -2, 2, 0, 1.414214)]
        [TestCase(-1, -1, 0, -2, -2, 0, 1.414214)]
        [TestCase(1, -1, 0, 2, -2, 0, 1.414214)]
        [TestCase(1, 0, 1, 2, 0, 2, 1.414214)]
        [TestCase(0, 1, 1, 0, 2, 2, 1.414214)]
        [TestCase(1, 1, 1, 2, 2, 2, 1.732051)]
        public static void Magnitude_3D(double x1, double y1, double z1, double x2, double y2, double z2, double expectedResult)
        {
            double xComponent = x2 - x1;
            double yComponent = y2 - y1;
            double zComponent = z2 - z1;
            Assert.AreEqual(expectedResult, VectorLibrary.Magnitude3D(xComponent, yComponent, zComponent), Tolerance);
        }

        [Test]
        public static void Magnitude_3D_Throws_ArgumentException_for_Zero_Magnitude()
        {
            Assert.Throws<ArgumentException>(() => VectorLibrary.Magnitude3D(0, 0, 0));
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4, 5, 6, 32)]
        [TestCase(1, 2, 3, -4, -5, -6, -32)]
        [TestCase(-1, -2, -3, 4, 5, 6, -32)]
        [TestCase(-1, -2, -3, -4, -5, -6, 32)]
        [TestCase(-1.1, -2.2, -3.3, -4.4, -5.5, -6.6, 38.72)]
        public static void DotProduct_3D(
            double x1, double y1, double z1, 
            double x2, double y2, double z2, double expectedResult)
        {
            Assert.AreEqual(expectedResult, VectorLibrary.DotProduct3D(x1, y1, z1, x2, y2, z2), Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4, 5, 6, -3, 6, -3)]
        [TestCase(1, 2, 3, -4, -5, -6, 3, -6, 3)]
        [TestCase(-1, -2, -3, 4, 5, 6, 3, -6, 3)]
        [TestCase(-1, -2, -3, -4, -5, -6, -3, 6, -3)]
        [TestCase(-1, 2, -3, 4, -5, 6, -3, -6, -3)]
        [TestCase(1, -2, 3, -4, 5, -6, -3, -6, -3)]
        [TestCase(7.7, -2.2, 3.3, -4.4, 5.5, -6.6, -3.63, 36.3, 32.67)]
        public static void CrossProduct_3D(
            double x1, double y1, double z1, 
            double x2, double y2, double z2, 
            double xExpected, double yExpected, double zExpected)
        {
            double[] result = VectorLibrary.CrossProduct3D(x1, y1, z1, x2, y2, z2);
            Assert.AreEqual(xExpected, result[0], Tolerance);
            Assert.AreEqual(yExpected, result[1], Tolerance);
            Assert.AreEqual(zExpected, result[2], Tolerance);
        }
        #endregion
    }
}
