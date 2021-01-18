
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
            LinearCurve linearCurve = new LinearCurve(new CartesianCoordinate(3, 4), new CartesianCoordinate(5, 6));

            Assert.AreEqual(3, linearCurve.ControlPointI.X);
            Assert.AreEqual(4, linearCurve.ControlPointI.Y);
            Assert.AreEqual(5, linearCurve.ControlPointJ.X);
            Assert.AreEqual(6, linearCurve.ControlPointJ.Y);
        }

        [Test]
        public static void Changing_Tolerance_Cascades_to_Properties()
        {
            double defaultTolerance = 10E-6; 
            LinearCurve linearCurve = new LinearCurve(new CartesianCoordinate(3, 4), new CartesianCoordinate(5, 6));

            Assert.AreEqual(defaultTolerance, linearCurve.Tolerance);
            Assert.AreEqual(defaultTolerance, linearCurve.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, linearCurve.ControlPointJ.Tolerance);

            double newTolerance = 10E-3;
            linearCurve.Tolerance = newTolerance;

            Assert.AreEqual(newTolerance, linearCurve.Tolerance);
            Assert.AreEqual(newTolerance, linearCurve.ControlPointI.Tolerance);
            Assert.AreEqual(newTolerance, linearCurve.ControlPointJ.Tolerance);
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

        [Test]
        public static void Curvature_Returns_Zero()
        {
            LinearCurve curve = new LinearCurve(new CartesianCoordinate(1, 2), new CartesianCoordinate(3, 4));
            Assert.AreEqual(0, curve.Curvature());
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
        #endregion

        #region ICurvePositionPolar
        [TestCase(1, 2, 3, 4, Numbers.PiOver2, 1)] // 45 deg line
        [TestCase(1, 2, 3, 4, Numbers.Pi, 1)] // 45 deg line
        [TestCase(1, 2, 3, 4, 1.107149, 2.236068)] // Intersects starting coord of 45 deg line
        [TestCase(1, 2, 3, 4, 0.927295, 5)] // Intersects ending coord of 45 deg line
        [TestCase(0, 1, -1, -2, Numbers.PiOver2, 1)]
        [TestCase(0, 1, -1, -2, 1.32581766, 4.123106)]
        [TestCase(0, 1, -1, -2, 1.107149, 2.236068)]
        public static void RadiusAboutOrigin(double x1, double y1, double x2, double y2, double angleRadians, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            curve.Tolerance = Tolerance;

            double radius = curve.RadiusAboutOrigin(angleRadians);

            Assert.AreEqual(expectedResult, radius, Tolerance);
        }

        [TestCase(1, 2, 3, 4, Numbers.PiOver2, 1)] // 45 deg line
        [TestCase(1, 2, 3, 4, Numbers.Pi, 1)] // 45 deg line
        [TestCase(1, 2, 3, 4, 1.107149, 2.236068)] // Intersects starting coord of 45 deg line
        [TestCase(1, 2, 3, 4, 0.927295, 5)] // Intersects ending coord of 45 deg line
        [TestCase(0, 1, -1, -2, Numbers.PiOver2, 1)]
        [TestCase(0, 1, -1, -2, 1.32581766, 4.123106)]
        [TestCase(0, 1, -1, -2, 1.107149, 2.236068)]
        public static void RadiiAboutOrigin(double x1, double y1, double x2, double y2, double angleRadians, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            curve.Tolerance = Tolerance;

            double[] radii = curve.RadiiAboutOrigin(angleRadians);

            Assert.AreEqual(1, radii.Length);
            Assert.AreEqual(expectedResult, radii[0], Tolerance);
        }

        [TestCase(1, 2, 3, 4, 0)] // Ray never intersects
        [TestCase(1, 2, 3, 4, Numbers.PiOver4)] // Parallel to line. Ray never intersects
        [TestCase(1, 2, 3, 4, 5 * Numbers.PiOver4)] // Parallel to line. Ray never intersects
        [TestCase(1, 2, 3, 4, 6 * Numbers.PiOver4)] // Ray never intersects
        [TestCase(1, 2, 3, 4, 7 * Numbers.PiOver4)] // Ray never intersects
        public static void RadiiAboutOrigin_Throws_ArgumentOutOfRangeException_for_Rotations_that_Never_Intersect_Line(
            double x1, double y1, double x2, double y2, 
            double angleRadians)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));
            curve.Tolerance = Tolerance;

            Assert.Throws<ArgumentOutOfRangeException>(() => curve.RadiiAboutOrigin(angleRadians));
        }
        #endregion

        #region ICurvePositionCartesian
        [TestCase(1, 1, 2, 1, 1, double.PositiveInfinity)] // Horizontal, on line
        [TestCase(1, 1, 1, 2, 1.25, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2.5, 1.5)] // + slope
        [TestCase(-1, -2, -3, -4, -2.5, -1.5)] // - slope
        [TestCase(1, 2, -3, -4, 0.2, -0.2)] // - slope
        public static void XatY(double x1, double y1, double x2, double y2, double y, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.XatY(y), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1.25, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1, double.PositiveInfinity)] // Vertical, on line
        [TestCase(1, 1, 1, 2, 2, double.PositiveInfinity)] // Vertical, off line
        [TestCase(1, 2, 3, 4, 1.5, 2.5)] // + slope
        [TestCase(-1, -2, -3, -4, -1.5, -2.5)] // - slope
        [TestCase(1, 2, -3, -4, -0.2, 0.2)] // - slope
        public static void YatX(double x1, double y1, double x2, double y2, double x, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.YatX(x), Tolerance);
        }

        [TestCase(1, 1, 2, 1, 1, double.PositiveInfinity)] // Horizontal, on line
        [TestCase(1, 1, 1, 2, 1.25, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2.5, 1.5)] // + slope
        [TestCase(-1, -2, -3, -4, -2.5, -1.5)] // - slope
        [TestCase(1, 2, -3, -4, 0.2, -0.2)] // - slope
        public static void XsAtY_Returns_Array_of_All_X_Coordinates_at_Position_Y(double x1, double y1, double x2, double y2, double y, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            double[] coordinates = curve.XsAtY(y);

            Assert.AreEqual(1, coordinates.Length);
            Assert.AreEqual(expectedResult, coordinates[0], Tolerance);
        }

        [TestCase(1, 1, 2, 1, 2)] // Horizontal, off line
        public static void XsAtY_Throws_ArgumentOutOfRangeException_for_Y_Not_On_Curve(double x1, double y1, double x2, double y2, double y)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.Throws<ArgumentOutOfRangeException>(() => curve.XsAtY(y));
        }

        [TestCase(1, 1, 2, 1, 1.25, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1, double.PositiveInfinity)] // Vertical, on line
        [TestCase(1, 1, 1, 2, 2, double.PositiveInfinity)] // Vertical, off line
        [TestCase(1, 2, 3, 4, 1.5, 2.5)] // + slope
        [TestCase(-1, -2, -3, -4, -1.5, -2.5)] // - slope
        [TestCase(1, 2, -3, -4, -0.2, 0.2)] // - slope
        public static void YsAtX_Returns_Array_of_All_Y_Coordinates_at_Position_X(double x1, double y1, double x2, double y2, double x, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            double[] coordinates = curve.YsAtX(x);

            Assert.AreEqual(1, coordinates.Length);
            Assert.AreEqual(expectedResult, coordinates[0], Tolerance);
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

        #endregion

        #region ICurveLimits
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

        public static void LengthBetween_Returns_Length_Between_Relative_Positions(double relativePositionStart, double relativePositionEnd)
        {

        }

        [TestCase(1, 1, 2, 1, 1)] // Horizontal
        [TestCase(1, 1, 1, 2, 1)] // Vertical
        [TestCase(1, 2, 3, 4, 2.828427)] // + slope
        [TestCase(-1, -2, -3, -4, 2.828427)] // - slope
        [TestCase(1, 2, -3, -4, 7.2111103)] // - slope
        public static void ChordLength(double x1, double y1, double x2, double y2, double expectedResult)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedResult, curve.ChordLength(), Tolerance);
        }

        //[TestCase(0, 0)]
        //[TestCase(0, 1)]
        //[TestCase(0, 0)]
        public static void ChordLengthBetween_Returns_Chord_Length_Between_Relative_Positions(double relativePositionStart, double relativePositionEnd)
        {
            //LinearCurve curve = new LinearCurve(
            //    new CartesianCoordinate(1, 2),
            //    new CartesianCoordinate(3, 4));

            //Assert.AreEqual(2.828427, curve.ChordLengthBetween(relativePositionStart, relativePositionEnd), Tolerance);
        }

        [Test]
        public static void Chord_Returns_Line_Segment()
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4));
            curve.Tolerance = Tolerance;

            LinearCurve chord = curve.Chord();

            Assert.AreEqual(curve.ControlPointI, chord.ControlPointI);
            Assert.AreEqual(curve.ControlPointJ, chord.ControlPointJ);
        }


        public static void ChordBetween_Returns_Chord_Between_Relative_Positions(double relativePositionStart, double relativePositionEnd)
        {
            LinearCurve curve;
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(0.5)]
        [TestCase(1)]
        [TestCase(2)]
        public static void TangentVector_Returns_Vector_Based_On_Relative_Position(double relativePosition)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4));
            Vector vector = curve.TangentVector(relativePosition);

            Assert.AreEqual(0.707107, vector.Xcomponent, Tolerance);
            Assert.AreEqual(0.707107, vector.Ycomponent, Tolerance);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(0.5)]
        [TestCase(1)]
        [TestCase(2)]
        public static void NormalVector_Returns_Vector_Based_On_Relative_Position(double relativePosition)
        {
            LinearCurve curve = new LinearCurve(
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(3, 4));
            Vector vector = curve.NormalVector(relativePosition);

            Assert.AreEqual(-0.707107, vector.Xcomponent, Tolerance);
            Assert.AreEqual(0.707107, vector.Ycomponent, Tolerance);
        }

        public static void CoordinateCartesian(double relativePosition)
        {
            CartesianCoordinate coordinate;
        }


        public static void CoordinatePolar(double relativePosition)
        {
            PolarCoordinate coordinate;
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            CartesianCoordinate coordinateI = new CartesianCoordinate(2, 2);
            CartesianCoordinate coordinateJ = new CartesianCoordinate(4, 3);
            LinearCurve linearCurve = new LinearCurve(coordinateI, coordinateJ);

            Assert.AreEqual("MPT.Math.Curves.LinearCurve - X-Intercept: -2, Y-Intercept: 1, Slope: 0.5", linearCurve.ToString());
        }

        [TestCase(-5, 6, -3, -2, 1, 5, -7, 3, -4.411765, 3.647059)] // sloped perpendicular, within points
        [TestCase(-5, 2, -5, 8, -5, 6, 5, 6, -5, 6)] // aligned perpendicular + vertical, within points
        [TestCase(-5, 8, -5, 2, -5, 6, 5, 6, -5, 6)] // aligned perpendicular - vertical, within points
        [TestCase(-5, 6, -3, -2, -5, 6, -1, 7, -5, 6)] // sloped perpendicular, on i-i points
        [TestCase(-5, 6, -3, -2, -1, 7, -5, 6, -5, 6)] // sloped perpendicular, on i-j points
        [TestCase(-5, 6, -3, -2, -3, -2, 1, -1, -3, -2)] // sloped perpendicular, on j-i points
        [TestCase(-5, 2, -5, 8, -5, 2, 1, 2, -5, 2)] // aligned perpendicular + vertical, on i-i points
        [TestCase(-5, 6, 5, 6, -5, 2, -5, 8, -5, 6)] // aligned perpendicular + vertical, within points, flipped order
        [TestCase(1, 3, 4, 6, 2, 1, 2, 8, 2, 4)] // sloped to vertical
        [TestCase(2, 1, 2, 8, 1, 3, 4, 6, 2, 4)] // vertical to sloped
        [TestCase(1, 3, 4, 6, 1, 4, 5, 4, 2, 4)] // sloped to horizontal
        [TestCase(1, 4, 5, 4, 1, 3, 4, 6, 2, 4)] // horizontal to sloped
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

        [TestCase(-7, 7, -6, 4)]
        [TestCase(-5, 1, -6, 4)]
        [TestCase(-4, 8, -3, 5)]
        [TestCase(-2, 2, -3, 5)]
        [TestCase(-1, 9, 0, 6)]
        [TestCase(1, 3, 0, 6)]
        [TestCase(5, 11, 6, 8)]
        [TestCase(7, 5, 6, 8)]
        [TestCase(8, 12, 9, 9)]
        [TestCase(10, 6, 9, 9)]
        public static void CoordinateOfPerpendicularProjection_Sloped_Line(double x, double y, double xExpected, double yExpected)
        {
            LinearCurve lineSegment = new LinearCurve(new CartesianCoordinate(-3, 5), new CartesianCoordinate(6, 8));
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedPerpendicularIntersection = new CartesianCoordinate(xExpected, yExpected);

            CartesianCoordinate actualPerpendicularIntersection = lineSegment.CoordinateOfPerpendicularProjection(coordinate);

            expectedPerpendicularIntersection.Tolerance = Tolerance;
            actualPerpendicularIntersection.Tolerance = Tolerance;

            Assert.AreEqual(expectedPerpendicularIntersection, actualPerpendicularIntersection);
        }

        [TestCase(2, 10, 5, 10)]
        [TestCase(8, 10, 5, 10)]
        [TestCase(2, 8, 5, 8)]
        [TestCase(8, 8, 5, 8)]
        [TestCase(2, 6, 5, 6)]
        [TestCase(8, 6, 5, 6)]
        [TestCase(2, 4, 5, 4)]
        [TestCase(8, 4, 5, 4)]
        [TestCase(2, 2, 5, 2)]
        [TestCase(8, 2, 5, 2)]
        public static void CoordinateOfPerpendicularProjection_Vertical_Line(double x, double y, double xExpected, double yExpected)
        {
            LinearCurve lineSegment = new LinearCurve(new CartesianCoordinate(5, 4), new CartesianCoordinate(5, 8));
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedPerpendicularIntersection = new CartesianCoordinate(xExpected, yExpected);

            CartesianCoordinate actualPerpendicularIntersection = lineSegment.CoordinateOfPerpendicularProjection(coordinate);

            expectedPerpendicularIntersection.Tolerance = Tolerance;
            actualPerpendicularIntersection.Tolerance = Tolerance;

            Assert.AreEqual(expectedPerpendicularIntersection, actualPerpendicularIntersection);
        }

        [TestCase(3, 2, 3, 4)]
        [TestCase(3, 6, 3, 4)]
        [TestCase(6, 2, 6, 4)]
        [TestCase(6, 6, 6, 4)]
        [TestCase(9, 2, 9, 4)]
        [TestCase(9, 6, 9, 4)]
        [TestCase(12, 2, 12, 4)]
        [TestCase(12, 6, 12, 4)]
        [TestCase(15, 2, 15, 4)]
        [TestCase(15, 6, 15, 4)]
        public static void CoordinateOfPerpendicularProjection_Horizontal_Line(double x, double y, double xExpected, double yExpected)
        {
            LinearCurve lineSegment = new LinearCurve(new CartesianCoordinate(6, 4), new CartesianCoordinate(12, 4));
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedPerpendicularIntersection = new CartesianCoordinate(xExpected, yExpected);

            CartesianCoordinate actualPerpendicularIntersection = lineSegment.CoordinateOfPerpendicularProjection(coordinate);

            expectedPerpendicularIntersection.Tolerance = Tolerance;
            actualPerpendicularIntersection.Tolerance = Tolerance;

            Assert.AreEqual(expectedPerpendicularIntersection, actualPerpendicularIntersection);
        }
        #endregion

        #region ICloneable
        [Test]
        public static void Clone()
        {
            CartesianCoordinate coordI = new CartesianCoordinate(-3, 5);
            CartesianCoordinate coordJ = new CartesianCoordinate(7, -1);
            LinearCurve curve1 = new LinearCurve(coordI, coordJ);

            LinearCurve curve2 = curve1.Clone() as LinearCurve;

            Assert.AreEqual(curve1.ControlPointI, curve2.ControlPointI);
            Assert.AreEqual(curve1.ControlPointJ, curve2.ControlPointJ);
        }

        [Test]
        public static void CloneCurve()
        {
            CartesianCoordinate coordI = new CartesianCoordinate(-3, 5);
            CartesianCoordinate coordJ = new CartesianCoordinate(7, -1);
            LinearCurve curve1 = new LinearCurve(coordI, coordJ);

            LinearCurve curve2 = curve1.CloneCurve();

            Assert.AreEqual(curve1.ControlPointI, curve2.ControlPointI);
            Assert.AreEqual(curve1.ControlPointJ, curve2.ControlPointJ);
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

        [Test]
        public static void CurveByYIntercept()
        {
            double yIntercept = 2;
            double slope = -2d / 5;
            LinearCurve linearCurve = LinearCurve.CurveByYIntercept(slope, yIntercept);

            Assert.AreEqual(0, linearCurve.ControlPointI.X);
            Assert.AreEqual(yIntercept, linearCurve.ControlPointI.Y);

            Assert.AreEqual(1, linearCurve.ControlPointJ.X);
            Assert.AreEqual(8d/ 5, linearCurve.ControlPointJ.Y);
        }

        [Test]
        public static void CurveByXIntercept()
        {
            double xIntercept = 5;
            double slope = -2d / 5;
            LinearCurve linearCurve = LinearCurve.CurveByXIntercept(slope, xIntercept);

            Assert.AreEqual(xIntercept, linearCurve.ControlPointI.X);
            Assert.AreEqual(0, linearCurve.ControlPointI.Y);

            Assert.AreEqual(xIntercept + 1, linearCurve.ControlPointJ.X);
            Assert.AreEqual(-2d / 5, linearCurve.ControlPointJ.Y);
        }
        #endregion

        #region Project
        [TestCase(-7, 7, -6, 4)]
        [TestCase(-5, 1, -6, 4)]
        [TestCase(-4, 8, -3, 5)]
        [TestCase(-2, 2, -3, 5)]
        [TestCase(-1, 9, 0, 6)]
        [TestCase(1, 3, 0, 6)]
        [TestCase(5, 11, 6, 8)]
        [TestCase(7, 5, 6, 8)]
        [TestCase(8, 12, 9, 9)]
        [TestCase(10, 6, 9, 9)]
        public static void CoordinateOfPerpendicularProjection_Sloped_Line_Static(double x, double y, double xExpected, double yExpected)
        {
            LinearCurve lineSegment = new LinearCurve(new CartesianCoordinate(-3, 5), new CartesianCoordinate(6, 8));
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedPerpendicularIntersection = new CartesianCoordinate(xExpected, yExpected);

            CartesianCoordinate actualPerpendicularIntersection = LinearCurve.CoordinateOfPerpendicularProjection(coordinate, lineSegment);

            expectedPerpendicularIntersection.Tolerance = Tolerance;
            actualPerpendicularIntersection.Tolerance = Tolerance;

            Assert.AreEqual(expectedPerpendicularIntersection, actualPerpendicularIntersection);
        }

        [TestCase(2, 10, 5, 10)]
        [TestCase(8, 10, 5, 10)]
        [TestCase(2, 8, 5, 8)]
        [TestCase(8, 8, 5, 8)]
        [TestCase(2, 6, 5, 6)]
        [TestCase(8, 6, 5, 6)]
        [TestCase(2, 4, 5, 4)]
        [TestCase(8, 4, 5, 4)]
        [TestCase(2, 2, 5, 2)]
        [TestCase(8, 2, 5, 2)]
        public static void CoordinateOfPerpendicularProjection_Vertical_Line_Static(double x, double y, double xExpected, double yExpected)
        {
            LinearCurve lineSegment = new LinearCurve(new CartesianCoordinate(5, 4), new CartesianCoordinate(5, 8));
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedPerpendicularIntersection = new CartesianCoordinate(xExpected, yExpected);

            CartesianCoordinate actualPerpendicularIntersection = LinearCurve.CoordinateOfPerpendicularProjection(coordinate, lineSegment);

            expectedPerpendicularIntersection.Tolerance = Tolerance;
            actualPerpendicularIntersection.Tolerance = Tolerance;

            Assert.AreEqual(expectedPerpendicularIntersection, actualPerpendicularIntersection);
        }

        [TestCase(3, 2, 3, 4)]
        [TestCase(3, 6, 3, 4)]
        [TestCase(6, 2, 6, 4)]
        [TestCase(6, 6, 6, 4)]
        [TestCase(9, 2, 9, 4)]
        [TestCase(9, 6, 9, 4)]
        [TestCase(12, 2, 12, 4)]
        [TestCase(12, 6, 12, 4)]
        [TestCase(15, 2, 15, 4)]
        [TestCase(15, 6, 15, 4)]
        public static void CoordinateOfPerpendicularProjection_Horizontal_Line_Static(double x, double y, double xExpected, double yExpected)
        {
            LinearCurve lineSegment = new LinearCurve(new CartesianCoordinate(6, 4), new CartesianCoordinate(12, 4));
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedPerpendicularIntersection = new CartesianCoordinate(xExpected, yExpected);

            CartesianCoordinate actualPerpendicularIntersection = LinearCurve.CoordinateOfPerpendicularProjection(coordinate, lineSegment);

            expectedPerpendicularIntersection.Tolerance = Tolerance;
            actualPerpendicularIntersection.Tolerance = Tolerance;

            Assert.AreEqual(expectedPerpendicularIntersection, actualPerpendicularIntersection);
        }
        #endregion
        #endregion
    }
}
