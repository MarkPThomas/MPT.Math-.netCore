
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;
using NUnit.Framework;
using System;

namespace MPT.Math.Algebra.UnitTests
{
    [TestFixture]
    public static class AlgebraTests
    {
        public static double Tolerance = 0.00001;

        #region Solutions to Formulas
        [TestCase(1, 6, 3, -5.44949, -0.55051)]
        [TestCase(1, -6, 3, 0.55051, 5.44949)]
        [TestCase(1, 0, 0, 0, 0)]
        public static void QuadraticFormula(double a, double b, double c, double expectedPositive, double expectedNegative)
        {
            double[] results = AlgebraLibrary.QuadraticFormula(a, b, c);
            Assert.AreEqual(expectedNegative, results[0], Tolerance);
            Assert.AreEqual(expectedPositive, results[1], Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(0, 6, 1)]
        public static void QuadraticFormula_Throws_Exception_When_a_is_0(double a, double b, double c)
        {
            Assert.Throws<ArgumentException>(()=> { double[] results = AlgebraLibrary.QuadraticFormula(a, b, c); });
        }

        [Test]
        public static void QuadraticFormula_Throws_Exception_When_SQRT_of_Negative_Value()
        {
            Assert.Throws<ArgumentException>(() => { double[] results = AlgebraLibrary.QuadraticFormula(1, 1, 1); });  // operad2Sqrt = -3< 0
        }

        [TestCase(0.7225, -1.7, 284.772087, -5274.720807, 13.440692)]  // B.IsNegative()
        [TestCase(0.7225, 1.7, 284.772087, 5274.720807, -13.440692)]   // tSqrt.IsNegative() with D.IsGreaterThanOrEqualTo(0)
        [TestCase(0.636733, -378.604263, 59057.623334, -1627072.644858, 34.90111)]
        public static void CubicCurveLowestRoot(double a, double b, double c, double d, double expectedResult)
        {
            double result = AlgebraLibrary.CubicCurveLowestRoot(a, b, c, d);
            Assert.AreEqual(expectedResult, result, 0.001);
        }

        [TestCase(0.7225, -1.7, 284.772087, -5274.720807, 13.440692, 13.440692, 13.440692)]  // B.IsNegative()
        [TestCase(0.7225, 1.7, 284.772087, 5274.720807, -13.440692, -13.440692, -13.440692)]   // tSqrt.IsNegative() with D.IsGreaterThanOrEqualTo(0)
        [TestCase(0.636733, -378.604263, 59057.623334, -1627072.644858, 351.26821, 34.90111, 208.435572)]
        public static void CubicCurveRoots(double a, double b, double c, double d, double expectedRoot1, double expectedRoot2, double expectedRoot3)
        {
            double[] result = AlgebraLibrary.CubicCurveRoots(a, b, c, d);

            double result1 = result[0];
            double result2 = result.Length > 1 ? result[1] : result[0];  // Returns lowest root if only one is returned
            double result3 = result.Length > 2 ? result[2] : result[0];  // Returns lowest root if only one is returned

            Assert.AreEqual(expectedRoot1, result1, 0.001);
            Assert.AreEqual(expectedRoot2, result2, 0.001);
            Assert.AreEqual(expectedRoot3, result3, 0.001);
        }
        #endregion

        #region Interpolations
        // public static double InterpolationLinear(double value1, double value2, double point2Weight)

        // public static CartesianCoordinate InterpolationLinear(CartesianCoordinate point1, CartesianCoordinate point2, double point2Weight)


        // Check for each corner
        // Check outside of bounds
        //Check all corners have same value
        // public static double InterpolationLinear2D(CartesianCoordinate Po, CartesianCoordinate3D ii, CartesianCoordinate3D ij, CartesianCoordinate3D ji, CartesianCoordinate3D jj )
        #endregion

        #region Intersections
        // Horizonal line colinear
        // Horizontal line parallel offset
        // public static double IntersectionX(double y, double x1, double y1, double x2, double y2)

        // public static double IntersectionX(double y, CartesianCoordinate I, CartesianCoordinate J)
        #endregion

        #region Calcs
        [TestCase(0, 0, 0)]
        [TestCase(0, 0, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(3, 5.5, 12)]
        [TestCase(3, -5.5, 12)]
        public static void SRSS(double value1, double value2, double value3)
        {
            double expectedResult = NMath.Sqrt(value1.Squared() + value2.Squared() + value3.Squared());
            Assert.AreEqual(expectedResult, AlgebraLibrary.SRSS(value1, value2, value3));
        }
        #endregion
    }    
}
