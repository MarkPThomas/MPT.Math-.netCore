
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;
using NUnit.Framework;
using System;
using MPT.Math.Curves;
using MPT.Math.Vectors;

namespace MPT.Math.UnitTests.Curves
{
    [TestFixture]
    public static class LinearCurveTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization_with_Coordinates_Results_in_Object_with_Immutable_Coordinates_Properties_List()
        {
            LinearCurve lineSegment = new LinearCurve(new CartesianCoordinate(3, 4), new CartesianCoordinate(5, 6));

            Assert.AreEqual(3, lineSegment.ControlPointI.X);
            Assert.AreEqual(4, lineSegment.ControlPointI.Y);
            Assert.AreEqual(5, lineSegment.ControlPointJ.X);
            Assert.AreEqual(6, lineSegment.ControlPointJ.Y);
        }
        #endregion

        #region Methods: Query
        [TestCase(0, 0, 0, 0, true)] // Pt.
        [TestCase(1, 2, 1, 2, true)] // Pt.
        [TestCase(1, 2, 10, 2, true)] // Horizontal positive
        [TestCase(1, -2, 10, -2, true)] // Horizontal negative
        [TestCase(-10, -2, 10, -2, true)] // Horizontal mixed
        [TestCase(1.1, 5.5, 10.5, 5.5, true)] // Horizontal decimal
        [TestCase(2, 1, 2, 10, false)] // Vertical positive
        [TestCase(-2, 1, -2, 10, false)] // Vertical negative
        [TestCase(-2, -10, -2, 10, false)] // Vertical mixed
        [TestCase(2.2, 1.2, 2.2, 10.2, false)] // Vertical decimal
        [TestCase(1, 2, 3, 4, false)] // Sloped
        public static void IsHorizontal(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.IsHorizontal());
        }

        [TestCase(0, 0, 0, 0, true)] // Pt.
        [TestCase(1, 2, 1, 2, true)] // Pt.
        [TestCase(1, 2, 10, 2, false)] // Horizontal positive
        [TestCase(1, -2, 10, -2, false)] // Horizontal negative
        [TestCase(-10, -2, 10, -2, false)] // Horizontal mixed
        [TestCase(1.1, 5.5, 10.5, 5.5, false)] // Horizontal decimal
        [TestCase(2, 1, 2, 10, true)] // Vertical positive
        [TestCase(-2, 1, -2, 10, true)] // Vertical negative
        [TestCase(-2, -10, -2, 10, true)] // Vertical mixed
        [TestCase(2.2, 1.2, 2.2, 10.2, true)] // Vertical decimal
        [TestCase(1, 2, 3, 4, false)] // Sloped
        public static void IsVertical(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.IsVertical());
        }

        [TestCase(-5, 6, -3, -1, 5.2, 6.1, 7.7, -1.3, false)] // slope
        [TestCase(-5, 6, -3, -2, 5, 6, 1, 5, false)] // sloped perpendicular
        [TestCase(-5, -2, -5, 2, -5, 6, 5, 6, false)] // aligned perpendicular + vertical
        [TestCase(-5, 2, -5, -2, -5, 6, 5, 6, false)] // aligned perpendicular - vertical
        [TestCase(1, 2, 3, 4, 2, 3, 4, 5, true)] // + slope parallel
        [TestCase(-5, 6, -3, -1, 5, 6, 7, -1, true)] // - slope parallel
        [TestCase(-1, 2, 1, 2, -2, 3, 2, 3, true)] // horizontal parallel
        [TestCase(-5, -2, -5, 2, 5, 6, 5, 8, true)] // + vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 8, 5, 6, true)] // - vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 6, 5, 8, true)] // +/- vertical parallel
        public static void IsParallel(
            double xi1, double yi1, double xj1, double yj1,
            double xi2, double yi2, double xj2, double yj2, bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(
                new CartesianCoordinate(xi1, yi1),
                new CartesianCoordinate(xj1, yj1));

            LinearCurve curve2 = new LinearCurve(
                new CartesianCoordinate(xi2, yi2),
                new CartesianCoordinate(xj2, yj2));

            Assert.AreEqual(expectedResult, curve1.IsParallel(curve2));
        }

        [TestCase(-5, 6, -3, -1, 5.2, 6.1, 7.7, -1.3, false)] // slope
        [TestCase(-5, 6, -3, -2, 5, 6, 1, 5, true)] // sloped perpendicular
        [TestCase(-5, -2, -5, 2, -5, 6, 5, 6, true)] // aligned perpendicular + vertical
        [TestCase(-5, 2, -5, -2, -5, 6, 5, 6, true)] // aligned perpendicular - vertical
        [TestCase(1, 2, 3, 4, 2, 3, 4, 5, false)] // + slope parallel
        [TestCase(-5, 6, -3, -1, 5, 6, 7, -1, false)] // - slope parallel
        [TestCase(-1, 2, 1, 2, -2, 3, 2, 3, false)] // horizontal parallel
        [TestCase(-5, -2, -5, 2, 5, 6, 5, 8, false)] // + vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 8, 5, 6, false)] // - vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 6, 5, 8, false)] // +/- vertical parallel
        public static void IsPerpendicular(
            double xi1, double yi1, double xj1, double yj1,
            double xi2, double yi2, double xj2, double yj2, bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(
                new CartesianCoordinate(xi1, yi1),
                new CartesianCoordinate(xj1, yj1));

            LinearCurve curve2 = new LinearCurve(
                new CartesianCoordinate(xi2, yi2),
                new CartesianCoordinate(xj2, yj2));

            Assert.AreEqual(expectedResult, curve1.IsPerpendicular(curve2));
        }

        [TestCase(1, 1, 3, 1, 2, 1, true)]  // Horizontal
        [TestCase(1, 2, 1, 4, 1, 3, true)]  // Vertical
        [TestCase(1, 2, 3, 4, 2, 3, true)]  // Sloped
        [TestCase(1.2, 3.4, 6.7, 9.1, 3.95, 6.25, true)]  // Sloped
        public static void IsIntersectingCoordinate(
            double x1, double y1, double x2, double y2,
            double pointX, double pointY, bool expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            CartesianCoordinate coordinate = new CartesianCoordinate(pointX, pointY);

            Assert.AreEqual(expectedResult, curve.IsIntersectingCoordinate(coordinate));
        }

        [TestCase(-5, 6, -3, -1, 5.2, 6.1, 7.7, -1.3, true)] // slope
        [TestCase(-5, 6, -3, -2, 5, 6, 1, 5, true)] // sloped perpendicular
        [TestCase(-5, -2, -5, 2, -5, 6, 5, 6, true)] // aligned perpendicular + vertical
        [TestCase(-5, 2, -5, -2, -5, 6, 5, 6, true)] // aligned perpendicular - vertical
        [TestCase(1, 2, 3, 4, 2, 3, 4, 5, false)] // + slope parallel
        [TestCase(-5, 6, -3, -1, 5, 6, 7, -1, false)] // - slope parallel
        [TestCase(-1, 2, 1, 2, -2, 3, 2, 3, false)] // horizontal parallel
        [TestCase(-5, -2, -5, 2, 5, 6, 5, 8, false)] // + vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 8, 5, 6, false)] // - vertical parallel
        [TestCase(-5, 2, -5, -2, 5, 6, 5, 8, false)] // +/- vertical parallel
        public static void IsIntersectingCurve(
            double xi1, double yi1, double xj1, double yj1,
            double xi2, double yi2, double xj2, double yj2, bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(
                new CartesianCoordinate(xi1, yi1),
                new CartesianCoordinate(xj1, yj1));

            LinearCurve curve2 = new LinearCurve(
                new CartesianCoordinate(xi2, yi2),
                new CartesianCoordinate(xj2, yj2));

            Assert.AreEqual(expectedResult, curve1.IsIntersectingCurve(curve2));
        }
        #endregion

        #region Methods: Properties
        [Test]
        public static void Slope_of_Point_Throws_Argument_Exception()
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(0, 1),
                new CartesianCoordinate(0, 1));

            Assert.Throws<ArgumentException>(() => curve.Slope());
        }

        [TestCase(2, 1, 4, 2, ExpectedResult = 1 / 2d)]
        [TestCase(2, 2, 4, 1, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 1, ExpectedResult = 1 / 2d)]
        [TestCase(4, 1, 2, 2, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 2, ExpectedResult = 0)]
        [TestCase(4, 1, 4, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(4, 2, 4, 1, ExpectedResult = double.NegativeInfinity)]
        public static double Slope(double x1, double y1, double x2, double y2)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            return curve.Slope();
        }


        [TestCase(1, 0, 2, 3, 1)]    // i coordinate at x-intercept
        [TestCase(1, 2, -4, 0, -4)]    // j coordinate at x-intercept
        [TestCase(0, 1, 2, 3, -1)]    // i coordinate at y-intercept
        [TestCase(1, 2, 0, -4, 0.666667)]    // j coordinate at y-intercept
        [TestCase(1, 2, 3, 4, -1)]    // + intercept
        [TestCase(-1, -2, -3, -4, 1)]    // - intercept
        [TestCase(1.5, 2.75, 3.75, 4.5, -2.035714)]    // decimal
        [TestCase(1, 2, 3.75, 2, double.PositiveInfinity)]    // horizontal
        [TestCase(2, 1, 2, 4.5, 2)]    // vertical
        public static void InterceptX(double x1, double y1, double x2, double y2, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            double result = curve.InterceptX();
            Assert.AreEqual(expectedResult, result, Tolerance);
        }

        [TestCase(1, 0, 2, 3, -3)]    // i coordinate at x-intercept
        [TestCase(1, 2, -4, 0, 1.6)]    // j coordinate at x-intercept
        [TestCase(0, 1, 2, 3, 1)]    // i coordinate at y-intercept
        [TestCase(1, 2, 0, -4, -4)]    // j coordinate at y-intercept
        [TestCase(1, 2, 3, 4, 1)]    // + intercept
        [TestCase(-1, -2, -3, -4, -1)]    // - intercept
        [TestCase(1.5, 2.75, 3.75, 4.5, 1.583333)]    // decimal
        [TestCase(1, 2, 3.75, 2, 2)]    // horizontal
        [TestCase(2, 1, 2, 4.5, double.PositiveInfinity)]    // vertical
        public static void InterceptY(double x1, double y1, double x2, double y2, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            double result = curve.InterceptY();
            Assert.AreEqual(expectedResult, result, Tolerance);
        }

        [TestCase(1, 1, 2, 1, 0, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, -1, 0)] // Vertical
        [TestCase(1, 2, 3, 4, -0.707107, 0.707107)] // + slope
        [TestCase(3, 4, 1, 2, 0.707107, -0.707107)] // - slope by reversing + slope coordinates
        [TestCase(-1, -2, -3, -4, 0.707107, -0.707107)] // - slope
        [TestCase(1, 2, -3, -4, 0.832050, -0.554700)] // - slope
        public static void NormalVector(double x1, double y1, double x2, double y2, double expectedXMagnitude, double expectedYMagnitude)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            Vector vector = curve.NormalVector();

            Assert.AreEqual(expectedXMagnitude, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedYMagnitude, vector.Ycomponent, Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1, 0)] // Horizontal
        [TestCase(1, 1, 1, 2, 0, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 0.707107, 0.707107)] // + slope
        [TestCase(3, 4, 1, 2, -0.707107, -0.707107)] // - slope by reversing + slope coordinates
        [TestCase(-1, -2, -3, -4, -0.707107, -0.707107)] // - slope
        [TestCase(1, 2, -3, -4, -0.554700, -0.832050)] // - slope
        public static void TangentVector(double x1, double y1, double x2, double y2, double expectedXMagnitude, double expectedYMagnitude)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            Vector vector = curve.TangentVector();

            Assert.AreEqual(expectedXMagnitude, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedYMagnitude, vector.Ycomponent, Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1, double.PositiveInfinity)] // Horizontal, on line
        [TestCase(1, 1, 2, 1, 2, double.PositiveInfinity)] // Horizontal, off line
        [TestCase(1, 1, 1, 2, 1.25, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2.5, 1.5)] // + slope
        [TestCase(-1, -2, -3, -4, -2.5, -1.5)] // - slope
        [TestCase(1, 2, -3, -4, 0.2, -0.2)] // - slope
        public static void X(double x1, double y1, double x2, double y2, double y, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.X(y), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1.25, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1, double.PositiveInfinity)] // Vertical, on line
        [TestCase(1, 1, 1, 2, 2, double.PositiveInfinity)] // Vertical, off line
        [TestCase(1, 2, 3, 4, 1.5, 2.5)] // + slope
        [TestCase(-1, -2, -3, -4, -1.5, -2.5)] // - slope
        [TestCase(1, 2, -3, -4, -0.2, 0.2)] // - slope
        public static void Y(double x1, double y1, double x2, double y2, double x, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.Y(x), Tolerance);
        }
        #endregion

        #region Methods: Properties Derived with Limits
        [TestCase(1, 1, 2, 1, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2.828427)] // + slope
        [TestCase(-1, -2, -3, -4, 2.828427)] // - slope
        [TestCase(1, 2, -3, -4, 7.2111103)] // - slope
        public static void Length(double x1, double y1, double x2, double y2, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.Length(), Tolerance);
        }
        #endregion

        #region Methods: Public
        [TestCase(-5, 6, -3, -2, 1, 5, -7, 3, -4.411765, 3.647059)] // sloped perpendicular, within points
        [TestCase(-5, 2, -5, 8, -5, 6, 5, 6, -5, 6)] // aligned perpendicular + vertical, within points
        [TestCase(-5, 8, -5, 2, -5, 6, 5, 6, -5, 6)] // aligned perpendicular - vertical, within points
        [TestCase(-5, 6, -3, -2, -5, 6, -1, 7, -5, 6)] // sloped perpendicular, on i-i points
        [TestCase(-5, 6, -3, -2, -1, 7, -5, 6, -5, 6)] // sloped perpendicular, on i-j points
        [TestCase(-5, 6, -3, -2, -3, -2, 1, -1, -3, -2)] // sloped perpendicular, on j-i points
        [TestCase(-5, 6, -3, -2, -5, 6, -1, 7, -5, 6)] // sloped perpendicular, on j-j points
        [TestCase(-5, 2, -5, 8, -5, 2, 1, 2, -5, 2)] // aligned perpendicular + vertical, on i-i points
        [TestCase(-5, 2, -5, 8, -5, 2, 1, 2, -5, 2)] // aligned perpendicular + vertical, on j-j points
        public static void IntersectionCoordinate(
           double xi1, double yi1, double xj1, double yj1,
           double xi2, double yi2, double xj2, double yj2, double expectedX, double expectedY)
        {
            LinearCurve segment1 = new LinearCurve(
                new CartesianCoordinate(xi1, yi1),
                new CartesianCoordinate(xj1, yj1));

            LinearCurve segment2 = new LinearCurve(
                new CartesianCoordinate(xi2, yi2),
                new CartesianCoordinate(xj2, yj2));

            CartesianCoordinate coordinateResult = segment1.IntersectionCoordinate(segment2);

            Assert.AreEqual(expectedX, coordinateResult.X, Tolerance);
            Assert.AreEqual(expectedY, coordinateResult.Y, Tolerance);
        }
        #endregion

        #region Methods: Static

        #region Alignment
        [TestCase(0, 0, 0, 0, true)] // Pt.
        [TestCase(1, 2, 1, 2, true)] // Pt.
        [TestCase(1, 2, 10, 2, true)] // Horizontal positive
        [TestCase(1, -2, 10, -2, true)] // Horizontal negative
        [TestCase(-10, -2, 10, -2, true)] // Horizontal mixed
        [TestCase(1.1, 5.5, 10.5, 5.5, true)] // Horizontal decimal
        [TestCase(2, 1, 2, 10, false)] // Vertical positive
        [TestCase(-2, 1, -2, 10, false)] // Vertical negative
        [TestCase(-2, -10, -2, 10, false)] // Vertical mixed
        [TestCase(2.2, 1.2, 2.2, 10.2, false)] // Vertical decimal
        [TestCase(1, 2, 3, 4, false)] // Sloped
        public static void IsHorizontal_Static(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(x1, y1);
            CartesianCoordinate ptJ = new CartesianCoordinate(x2, y2);

            Assert.AreEqual(expectedResult, LinearCurve.IsHorizontal(ptI, ptJ));
        }

        [TestCase(0, 0, 0, 0, true)] // Pt.
        [TestCase(1, 2, 1, 2, true)] // Pt.
        [TestCase(1, 2, 10, 2, false)] // Horizontal positive
        [TestCase(1, -2, 10, -2, false)] // Horizontal negative
        [TestCase(-10, -2, 10, -2, false)] // Horizontal mixed
        [TestCase(1.1, 5.5, 10.5, 5.5, false)] // Horizontal decimal
        [TestCase(2, 1, 2, 10, true)] // Vertical positive
        [TestCase(-2, 1, -2, 10, true)] // Vertical negative
        [TestCase(-2, -10, -2, 10, true)] // Vertical mixed
        [TestCase(2.2, 1.2, 2.2, 10.2, true)] // Vertical decimal
        [TestCase(1, 2, 3, 4, false)] // Sloped
        public static void IsVertical_Static(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            CartesianCoordinate ptI = new CartesianCoordinate(x1, y1);
            CartesianCoordinate ptJ = new CartesianCoordinate(x2, y2);

            Assert.AreEqual(expectedResult, LinearCurve.IsVertical(ptI, ptJ));
        }

        [TestCase(5.6789d, -1 / 5.6789d, false)] // perpendicular sloped
        [TestCase(2, -1 / 2d, false)] // perpendicular sloped
        [TestCase(1, -1, false)] // perpendicular sloped
        [TestCase(double.NegativeInfinity, 0, false)] // aligned perpendicular - vertical
        [TestCase(double.PositiveInfinity, 0, false)] // aligned perpendicular + vertical
        [TestCase(0, double.NegativeInfinity, false)] // aligned perpendicular - vertical
        [TestCase(0, double.PositiveInfinity, false)] // aligned perpendicular + vertical
        [TestCase(1, 1, true)] // + slope parallel
        [TestCase(5.6789d, 5.6789d, true)] // + slope parallel
        [TestCase(0, 0, true)] // horizontal parallel
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, true)] // + vertical parallel
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, true)] // - vertical parallel
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, true)] // +/- vertical parallel
        [TestCase(5.6789d, -5.6789d, false)] // slope
        public static void IsParallel_Static(double slope1, double slope2, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, LinearCurve.IsParallel(slope1, slope2));
        }

        [TestCase(5.6789d, -1 / 5.6789d, true)] // perpendicular sloped
        [TestCase(2, -1 / 2d, true)] // perpendicular sloped
        [TestCase(1, -1, true)] // perpendicular sloped
        [TestCase(double.NegativeInfinity, 0, true)] // aligned perpendicular - vertical
        [TestCase(double.PositiveInfinity, 0, true)] // aligned perpendicular + vertical
        [TestCase(0, double.NegativeInfinity, true)] // aligned perpendicular - vertical
        [TestCase(0, double.PositiveInfinity, true)] // aligned perpendicular + vertical
        [TestCase(1, 1, false)] // + slope parallel
        [TestCase(5.6789d, 5.6789d, false)] // + slope parallel
        [TestCase(0, 0, false)] // horizontal parallel
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, false)] // + vertical parallel
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, false)] // - vertical parallel
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, false)] // +/- vertical parallel
        [TestCase(5.6789d, -5.6789d, false)] // - slope
        public static void IsPerpendicular_Static(double slope1, double slope2, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, LinearCurve.IsPerpendicular(slope1, slope2));
        }
        #endregion

        #region Slope
        [TestCase(1, 2, ExpectedResult = 1 / 2d)]
        [TestCase(-1, 2, ExpectedResult = -1 / 2d)]
        [TestCase(-1, -2, ExpectedResult = 1 / 2d)]
        [TestCase(1, -2, ExpectedResult = -1 / 2d)]
        [TestCase(0, -2, ExpectedResult = 0)]
        [TestCase(1, 0, ExpectedResult = double.PositiveInfinity)]
        [TestCase(-1, 0, ExpectedResult = double.NegativeInfinity)]
        public static double Slope_Static(double rise, double run)
        {
            return LinearCurve.Slope(rise, run);
        }

        [Test]
        public static void Slope_Static_of_Point_Throws_Argument_Exception()
        {
            Assert.Throws<ArgumentException>(() => LinearCurve.Slope(rise: 0, run: 0));
        }

        [TestCase(2, 1, 4, 2, ExpectedResult = 1 / 2d)]
        [TestCase(2, 2, 4, 1, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 1, ExpectedResult = 1 / 2d)]
        [TestCase(4, 1, 2, 2, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 2, ExpectedResult = 0)]
        [TestCase(4, 1, 4, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(4, 2, 4, 1, ExpectedResult = double.NegativeInfinity)]
        public static double Slope_Static_of_Coordinate_Input(double x1, double y1, double x2, double y2)
        {
            return LinearCurve.Slope(x1, y1, x2, y2);
        }

        [TestCase(2, 1, 4, 2, ExpectedResult = 1 / 2d)]
        [TestCase(2, 2, 4, 1, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 1, ExpectedResult = 1 / 2d)]
        [TestCase(4, 1, 2, 2, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 2, ExpectedResult = 0)]
        [TestCase(4, 1, 4, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(4, 2, 4, 1, ExpectedResult = double.NegativeInfinity)]
        public static double Slope_Static_of_Point_Input(double x1, double y1, double x2, double y2)
        {
            CartesianCoordinate point1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate point2 = new CartesianCoordinate(x2, y2);
            return LinearCurve.Slope(point1, point2);
        }

        [TestCase(2, 1, 4, 2, ExpectedResult = 1 / 2d)]
        [TestCase(2, 2, 4, 1, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 1, ExpectedResult = 1 / 2d)]
        [TestCase(4, 1, 2, 2, ExpectedResult = -1 / 2d)]
        [TestCase(4, 2, 2, 2, ExpectedResult = 0)]
        [TestCase(4, 1, 4, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(4, 2, 4, 1, ExpectedResult = double.NegativeInfinity)]
        public static double Slope_Static_of_Offset_Input(double x1, double y1, double x2, double y2)
        {
            CartesianCoordinate point1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate point2 = new CartesianCoordinate(x2, y2);
            CartesianOffset offset = new CartesianOffset(point1, point2);
            return LinearCurve.Slope(offset);
        }
        #endregion

        #region Intercept
        [TestCase(1, 0, 2, 3, 1)]    // i coordinate at x-intercept
        [TestCase(1, 2, -4, 0, -4)]    // j coordinate at x-intercept
        [TestCase(0, 1, 2, 3, -1)]    // i coordinate at y-intercept
        [TestCase(1, 2, 0, -4, 0.666667)]    // j coordinate at y-intercept
        [TestCase(1, 2, 3, 4, -1)]    // + intercept
        [TestCase(-1, -2, -3, -4, 1)]    // - intercept
        [TestCase(1.5, 2.75, 3.75, 4.5, -2.035714)]    // decimal
        [TestCase(1, 2, 3.75, 2, double.PositiveInfinity)]    // horizontal
        [TestCase(2, 1, 2, 4.5, 2)]    // vertical
        public static void InterceptX_Static(double x1, double y1, double x2, double y2, double expectedResult)
        {
            double result = LinearCurve.InterceptX(x1, y1, x2, y2);
            Assert.AreEqual(expectedResult, result, Tolerance);
        }

        [TestCase(1, 0, 2, 3, 1)]    // i coordinate at x-intercept
        [TestCase(1, 2, -4, 0, -4)]    // j coordinate at x-intercept
        [TestCase(0, 1, 2, 3, -1)]    // i coordinate at y-intercept
        [TestCase(1, 2, 0, -4, 0.666667)]    // j coordinate at y-intercept
        [TestCase(1, 2, 3, 4, -1)]    // + intercept
        [TestCase(-1, -2, -3, -4, 1)]    // - intercept
        [TestCase(1.5, 2.75, 3.75, 4.5, -2.035714)]    // decimal
        [TestCase(1, 2, 3.75, 2, double.PositiveInfinity)]    // horizontal
        [TestCase(2, 1, 2, 4.5, 2)]    // vertical
        public static void InterceptX_Static_Cartesian_Points(double x1, double y1, double x2, double y2, double expectedResult)
        {
            CartesianCoordinate pt1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate pt2 = new CartesianCoordinate(x2, y2);

            double result = LinearCurve.InterceptX(pt1, pt2);
            Assert.AreEqual(expectedResult, result, Tolerance);
        }

        [TestCase(1, 0, 2, 3, -3)]    // i coordinate at x-intercept
        [TestCase(1, 2, -4, 0, 1.6)]    // j coordinate at x-intercept
        [TestCase(0, 1, 2, 3, 1)]    // i coordinate at y-intercept
        [TestCase(1, 2, 0, -4, -4)]    // j coordinate at y-intercept
        [TestCase(1, 2, 3, 4, 1)]    // + intercept
        [TestCase(-1, -2, -3, -4, -1)]    // - intercept
        [TestCase(1.5, 2.75, 3.75, 4.5, 1.583333)]    // decimal
        [TestCase(1, 2, 3.75, 2, 2)]    // horizontal
        [TestCase(2, 1, 2, 4.5, double.PositiveInfinity)]    // vertical
        public static void InterceptY_Static(double x1, double y1, double x2, double y2, double expectedResult)
        {
            double result = LinearCurve.InterceptY(x1, y1, x2, y2);
            Assert.AreEqual(expectedResult, result, Tolerance);
        }

        [TestCase(1, 0, 2, 3, -3)]    // i coordinate at x-intercept
        [TestCase(1, 2, -4, 0, 1.6)]    // j coordinate at x-intercept
        [TestCase(0, 1, 2, 3, 1)]    // i coordinate at y-intercept
        [TestCase(1, 2, 0, -4, -4)]    // j coordinate at y-intercept
        [TestCase(1, 2, 3, 4, 1)]    // + intercept
        [TestCase(-1, -2, -3, -4, -1)]    // - intercept
        [TestCase(1.5, 2.75, 3.75, 4.5, 1.583333)]    // decimal
        [TestCase(1, 2, 3.75, 2, 2)]    // horizontal
        [TestCase(2, 1, 2, 4.5, double.PositiveInfinity)]    // vertical
        public static void InterceptY_Static_Cartesian_Points(double x1, double y1, double x2, double y2, double expectedResult)
        {
            CartesianCoordinate pt1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate pt2 = new CartesianCoordinate(x2, y2);

            double result = LinearCurve.InterceptY(pt1, pt2);
            Assert.AreEqual(expectedResult, result, Tolerance);
        }
        #endregion

        #region Intersect
        [TestCase(0, 0, false)] // || horizontal
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, false)] // || vertical
        [TestCase(double.PositiveInfinity, double.NegativeInfinity, false)] // || vertical
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, false)] // || vertical
        [TestCase(1.1, 1.1, false)] // || +sloped
        [TestCase(-1.1, -1.1, false)] // || -sloped
        [TestCase(1.1, -1.1, true)] // Not ||
        public static void AreLinesIntersecting_Static(double slope1, double slope2, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, LinearCurve.AreLinesIntersecting(slope1, slope2));
        }

        [TestCase(0, 0, 0, 0, double.PositiveInfinity)]  // Parallel
        [TestCase(1, -1, 1, -2, double.PositiveInfinity)]  // Parallel
        [TestCase(0, double.PositiveInfinity, double.PositiveInfinity, 3.3, 3.3)]  // Perpendicular
        [TestCase(double.PositiveInfinity, 3.3, 0, double.PositiveInfinity, 3.3)]  // Perpendicular
        [TestCase(1, -1, -1, 0.9, -0.05)]  // Perpendicular
        [TestCase(1, -1, -2.093023, 2.153333, 1.1338346)]  // Sloped
        public static void LineIntersectX_Static(double slope1, double xIntercept1, double slope2, double xIntercept2, double expectedX)
        {
            double intersectionX = LinearCurve.LineIntersectX(slope1, xIntercept1, slope2, xIntercept2);
            Assert.AreEqual(expectedX, intersectionX, Tolerance);
        }

        [TestCase(0, 0, 0, 0, double.PositiveInfinity)]  // Parallel
        [TestCase(1, 1, 1, 2, double.PositiveInfinity)]  // Parallel
        [TestCase(0, 1, double.PositiveInfinity, double.PositiveInfinity, 1)]  // Perpendicular
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, 0, 1, 1)]  // Perpendicular
        [TestCase(1, 1, -1, 0.9, 0.95)]  // Perpendicular
        [TestCase(1, 1, -2.093023, 4.5069767, 2.1338346)]  // Sloped
        public static void LineIntersectY_Static(double slope1, double yIntercept1, double slope2, double yIntercept2, double expectedY)
        {
            double intersectionY = LinearCurve.LineIntersectY(slope1, yIntercept1, slope2, yIntercept2);
            Assert.AreEqual(expectedY, intersectionY, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, double.PositiveInfinity, double.PositiveInfinity)]  // Parallel
        [TestCase(1, -1, 1, 1, -2, 2, double.PositiveInfinity, double.PositiveInfinity)]  // Parallel
        [TestCase(0, double.PositiveInfinity, 1, double.PositiveInfinity, 3.3, double.PositiveInfinity, 3.3, 1)]  // Perpendicular
        [TestCase(1, -1, 1, -1, 0.9, 0.9, -0.05, 0.95)]  // Perpendicular
        [TestCase(1, -1, 1, -2.093023, 2.153333, 4.5069767, 1.1338346, 2.1338346)]  // Sloped
        public static void LineIntersect_Static(
            double slope1, double xIntercept1, double yIntercept1, double slope2, double xIntercept2, double yIntercept2, double expectedX, double expectedY)
        {
            CartesianCoordinate intersection = LinearCurve.LineIntersect(slope1, xIntercept1, yIntercept1, slope2, xIntercept2, yIntercept2);
            Assert.AreEqual(expectedX, intersection.X, Tolerance);
            Assert.AreEqual(expectedY, intersection.Y, Tolerance);
        }
        #endregion

        #endregion
    }
}
