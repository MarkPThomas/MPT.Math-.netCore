
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;
using NUnit.Framework;
using System;
using MPT.Math.Coordinates;

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
        [TestCase(1, 11, 0, 1)]
        [TestCase(1, 11, 1, 11)]
        [TestCase(1, 11, 0.5, 6)]
        [TestCase(1.1, 11.1, 0.5, 6.1)]
        public static void InterpolationLinear_Between_Two_Values(double value1, double value2, double point2Weight, double expectedResult)
        {
            double interpolatedValue = AlgebraLibrary.InterpolationLinear(value1, value2, point2Weight);

            Assert.AreEqual(expectedResult, interpolatedValue);
        }

        [TestCase(1, 11, 2)]
        [TestCase(1, 11, -2)]
        public static void InterpolationLinear_Between_Two_Values_Throws_Exception_if_Weight_Outside_Bounds(double value1, double value2, double point2Weight)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { AlgebraLibrary.InterpolationLinear(value1, value2, point2Weight); });
        }

        [TestCase(1, -2, 11, 6, 0, 1, -2)]
        [TestCase(1, -2, 11, 6, 1, 11, 6)]
        [TestCase(1, -2, 11, 6, 0.5, 6, 2)]
        [TestCase(1.1, -2.2, 11.1, 6.6, 0.5, 6.1, 2.2)]
        public static void InterpolationLinear_Between_Two_Coordinates(double x1, double y1, double x2, double y2, double point2Weight, double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate coordinate2= new CartesianCoordinate(x2, y2);

            CartesianCoordinate interpolatedCoordinate = AlgebraLibrary.InterpolationLinear(coordinate1, coordinate2, point2Weight);

            Assert.AreEqual(expectedX, interpolatedCoordinate.X);
            Assert.AreEqual(expectedY, interpolatedCoordinate.Y);
        }

        [TestCase(1, 2, 11, 5, 2)]
        [TestCase(1, 2, 11, 5, -2)]
        public static void InterpolationLinear_Between_Two_Coordinates_Throws_Exception_if_Weight_Outside_Bounds(
            double x1, double y1, 
            double x2, double y2, double point2Weight)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(x2, y2);

            Assert.Throws<ArgumentOutOfRangeException>(() => { AlgebraLibrary.InterpolationLinear(coordinate1, coordinate2, point2Weight); });
        }

        [TestCase(2.2, 3.3, 1.1, 2.2, 4.4, 6.6, 1.5, 1.5, 1.5, 1.5, 1.5)]  // All Corners have same value
        [TestCase(1.1, 2.2, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 1)]  // Pt ii
        [TestCase(4.4, 6.6, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 4)]  // Pt jj
        [TestCase(1.1, 6.6, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 3)]  // Pt ij
        [TestCase(4.4, 2.2, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 2)]  // Pt ji
        [TestCase(2.75, 2.2, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 1.5)]  // Top Edge
        [TestCase(2.75, 6.6, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 3.5)]  // Bottom Edge
        [TestCase(1.1, 4.4, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 2)]  // Left Edge
        [TestCase(4.4, 4.4, 1.1, 2.2, 4.4, 6.6, 1, 2, 3, 4, 3)]  // Right Edge
        [TestCase(2.75, 4.4, 1.1, 2.2, 4.4, 6.6, 1.5, 1.5, 2.5, 2.5, 2)]  // Center of plane sloped along rows
        [TestCase(2.75, 4.4, 1.1, 2.2, 4.4, 6.6, 1.5, 3.5, 1.5, 3.5, 2.5)]  // Center of plane sloped along columns
        [TestCase(2.2, 3.3, 1.1, 2.2, 4.4, 6.6, 1.1, 2.2, 3.3, 4.4, 2.016667)]  // All corners have different value, point not centered
        public static void InterpolationLinear2D(
            double col0, double row0,
            double iiCol, double iiRow,
            double jjCol, double jjRow,
            double iiValue, double ijValue, double jiValue, double jjValue,
            double expectedValue)
        {
            CartesianCoordinate pO = new CartesianCoordinate(col0, row0);
            CartesianCoordinate ii = new CartesianCoordinate(iiCol, iiRow);
            CartesianCoordinate jj = new CartesianCoordinate(jjCol, jjRow);

            double value = AlgebraLibrary.InterpolationLinear2D(pO, ii, jj, iiValue, ijValue, jiValue, jjValue);

            Assert.AreEqual(expectedValue, value, Tolerance);
        }

        [Test]
        public static void InterpolationLinear2D_Width_0_Throws_ArgumentException()
        {
            CartesianCoordinate pO = new CartesianCoordinate(2.2, 3.3);
            CartesianCoordinate ii = new CartesianCoordinate(1.1, 2.2);
            CartesianCoordinate jj = new CartesianCoordinate(1.1, 6.6);

            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.InterpolationLinear2D(pO, ii, jj, 1, 1, 1, 1); });
        }

        [Test]
        public static void InterpolationLinear2D_Height_0_Throws_ArgumentException()
        {
            CartesianCoordinate pO = new CartesianCoordinate(2.2, 3.3);
            CartesianCoordinate ii = new CartesianCoordinate(1.1, 2.2);
            CartesianCoordinate jj = new CartesianCoordinate(4.4, 2.2);

            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.InterpolationLinear2D(pO, ii, jj, 1, 1, 1, 1); });
        }

        [Test]
        public static void InterpolationLinear2D_OutOfBounds_Throws_ArgumentOutOfRangeException()
        {
            CartesianCoordinate pO = new CartesianCoordinate(1, 2);
            CartesianCoordinate ii = new CartesianCoordinate(1.1, 2.2);
            CartesianCoordinate jj = new CartesianCoordinate(4.4, 6.6);

            Assert.Throws<ArgumentOutOfRangeException>(() => { AlgebraLibrary.InterpolationLinear2D(pO, ii, jj, 1, 1, 1, 1); });
        }
        #endregion

        #region Intersections
        [TestCase(3, 2, 3, 2, 10, 2)]  // vertical line
        [TestCase(4, 2, 3, 5, 6, 3)]  // within points
        [TestCase(7, 2, 3, 5, 6, 6)]  // outside of points
        [TestCase(-1, 2, 3, -2, -3, -0.666667)]  // negative slope
        public static void IntersectionX_Returns_X_Coordinate_Intersection(double y, double x1, double y1, double x2, double y2, double expectedX)
        {
            double result = AlgebraLibrary.IntersectionX(y, x1, y1, x2, y2);

            Assert.AreEqual(expectedX, result, Tolerance);
        }

        [Test]
        public static void IntersectionX_Throws_Exception_for_Line_as_Point()
        {
            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.IntersectionX(3, 2, 3, 2, 3); });
        }

        [Test]
        public static void IntersectionX_Throws_Exception_for_Collinear_Line()
        {
            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.IntersectionX(2, 2, 3, 6, 3); });
        }

        [Test]
        public static void IntersectionX_Throws_Exception_for_Parallel_Line()
        {
            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.IntersectionX(3, 2, 3, 6, 3); });
        }

        [TestCase(3, 2, 3, 2, 10, 2)]  // vertical line
        [TestCase(4, 2, 3, 5, 6, 3)]  // within points
        [TestCase(7, 2, 3, 5, 6, 6)]  // outside of points
        [TestCase(-1, 2, 3, -2, -3, -0.666667)]  // negative slope
        public static void IntersectionX_Between_Two_Coordinates_Returns_X_Coordinate_Intersection_of_Line_Formed(double y, double x1, double y1, double x2, double y2, double expectedX)
        {
            double result = AlgebraLibrary.IntersectionX(y, new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedX, result, Tolerance);
        }

        [Test]
        public static void IntersectionX_Between_Two_Coordinates_Throws_Exception_for_Line_as_Point()
        {
            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.IntersectionX(3, new CartesianCoordinate(2, 3), new CartesianCoordinate(2, 3)); });
        }

        [Test]
        public static void IntersectionX_Between_Two_Coordinates_Throws_Exception_for_Collinear_Line_of_Line_Formed()
        {
            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.IntersectionX(2, new CartesianCoordinate(2, 3), new CartesianCoordinate(6, 3)); });
        }

        [Test]
        public static void IntersectionX_Between_Two_Coordinates_Throws_Exception_for_Parallel_Line_of_Line_Formed()
        {
            Assert.Throws<ArgumentException>(() => { AlgebraLibrary.IntersectionX(3, new CartesianCoordinate(2, 3), new CartesianCoordinate(6, 3)); });
        }
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
