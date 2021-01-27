using MPT.Math.Coordinates;
using NUnit.Framework;
using System;
using MPT.Math.Curves;
using MPT.Math.Vectors;

namespace MPT.Math.UnitTests.Curves
{
    [TestFixture]
    public static class CircularCurveTests
    {
        public static double Tolerance = 0.00001;

        public static CircularCurve curve;
        public const double curveRadius = 6;

        [SetUp]
        public static void SetUp()
        {
            CartesianCoordinate center = new CartesianCoordinate(4, 5);
            curve = new CircularCurve(curveRadius, center, Tolerance);
        }

        #region Initialization
        [Test]
        public static void Initialization_at_Local_Origin_with_Coordinates()
        {
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            double radius = 6;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(radius, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-radius, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, radius);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -radius);

            CartesianCoordinate directrix1 = new CartesianCoordinate(double.PositiveInfinity, 0);
            CartesianCoordinate directrix2 = new CartesianCoordinate(double.PositiveInfinity, 1);

            // Initialization
            CircularCurve curve = new CircularCurve(vertexMajor1, center, Tolerance);

            // Properties unique to circle
            Assert.AreEqual(center, curve.Center);
            Assert.AreEqual(1d / radius, curve.Curvature, Tolerance);
            Assert.AreEqual(radius, curve.Radius, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(0, curve.Eccentricity, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(radius, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(center, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);
            Assert.AreEqual(Angle.Origin(), curve.Rotation);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(directrix1, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix2, curve.Directrix.ControlPointJ);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
        }

        [Test]
        public static void Initialization_at_Local_Origin_with_Radius()
        {
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            double radius = 6;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(radius, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-radius, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, radius);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -radius);

            CartesianCoordinate directrix1 = new CartesianCoordinate(double.PositiveInfinity, 0);
            CartesianCoordinate directrix2 = new CartesianCoordinate(double.PositiveInfinity, 1);

            // Initialization
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            // Properties unique to circle
            Assert.AreEqual(center, curve.Center);
            Assert.AreEqual(1d / radius, curve.Curvature, Tolerance);
            Assert.AreEqual(radius, curve.Radius, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(0, curve.Eccentricity, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(radius, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(center, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);
            Assert.AreEqual(Angle.Origin(), curve.Rotation);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(directrix1, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix2, curve.Directrix.ControlPointJ);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Coordinates(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            double radius = 6;
            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(radius, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-radius, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, radius);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -radius);

            CartesianCoordinate directrix1 = new CartesianCoordinate(double.PositiveInfinity, 0);
            CartesianCoordinate directrix2 = new CartesianCoordinate(double.PositiveInfinity, 1);

            // Initialization
            CircularCurve curve = new CircularCurve(vertexMajor1, center, Tolerance);

            // Properties unique to circle
            Assert.AreEqual(center, curve.Center);
            Assert.AreEqual(1d / radius, curve.Curvature, Tolerance);
            Assert.AreEqual(radius, curve.Radius, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(0, curve.Eccentricity, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(radius, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(center, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);
            Assert.AreEqual(Angle.Origin(), curve.Rotation);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
            Assert.AreEqual(directrix1, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix2, curve.Directrix.ControlPointJ);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Radius(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            double radius = 6;
            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(radius, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-radius, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, radius);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -radius);

            CartesianCoordinate directrix1 = new CartesianCoordinate(double.PositiveInfinity, 0);
            CartesianCoordinate directrix2 = new CartesianCoordinate(double.PositiveInfinity, 1);

            // Initialization
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            // Properties unique to circle
            Assert.AreEqual(center, curve.Center);
            Assert.AreEqual(1d / radius, curve.Curvature, Tolerance);
            Assert.AreEqual(radius, curve.Radius, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(0, curve.Eccentricity, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(radius, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(radius, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(double.PositiveInfinity, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(center, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);
            Assert.AreEqual(Angle.Origin(), curve.Rotation);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(directrix1, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix2, curve.Directrix.ControlPointJ);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
        }

        [Test]
        public static void Changing_Tolerance_Cascades_to_Properties()
        {
            double defaultTolerance = 10E-6;

            Assert.AreEqual(defaultTolerance, curve.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Focus.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Center.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Rotation.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMajor.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMajor.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointJ.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.LocalOrigin.Tolerance);

            double newTolerance = 10E-3;
            curve.Tolerance = newTolerance;

            Assert.AreEqual(newTolerance, curve.Tolerance);
            Assert.AreEqual(newTolerance, curve.Center.Tolerance);
            Assert.AreEqual(newTolerance, curve.Focus.Tolerance);
            Assert.AreEqual(newTolerance, curve.Rotation.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMajor.Item1.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMajor.Item2.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMinor.Item1.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMinor.Item2.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix.ControlPointI.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix.ControlPointJ.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix.Tolerance);
            Assert.AreEqual(newTolerance, curve.LocalOrigin.Tolerance);
        }
        #endregion

        #region Curve Position
        [TestCase(4, 0, 0, 3)] // Out of range +
        [TestCase(-4, 0, 0, 3)] // Out of range -
        [TestCase(9, 4, 5, 3)] // Offset out of range +
        [TestCase(1, 4, 5, 3)] // Offset out of range -
        public static void XatY_Out_of_Range_Return_Infinity(double yCoord, double x, double y, double radius)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(double.PositiveInfinity, xCoord);
        }

        [TestCase(0, 0, 0, 3, 3)] //Aligned with origin
        [TestCase(1.5, 0, 0, 3, 2.598076)] //half radius +
        [TestCase(-1.5, 0, 0, 3, 2.598076)] //half radius -
        [TestCase(3, 0, 0, 3, 0)] //radius +
        [TestCase(-3, 0, 0, 3, 0)] //radius -
        [TestCase(5, 4, 5, 3, 7)] //Offset aligned with origin
        [TestCase(6.5, 4, 5, 3, 6.598076)] //Offset half radius +
        [TestCase(3.5, 4, 5, 3, 6.598076)] //Offset half radius -
        [TestCase(8, 4, 5, 3, 4)] //Offset radius +
        [TestCase(2, 4, 5, 3, 4)] //Offset radius -
        public static void XatY(double yCoord, double x, double y, double radius, double xCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(xCoordExpected, xCoord, Tolerance);
        }

        [TestCase(4, 0, 0, 3)] // Out of range +
        [TestCase(-4, 0, 0, 3)] // Out of range -
        [TestCase(8, 4, 5, 3)] // Offset out of range +
        [TestCase(0, 4, 5, 3)] // Offset out of range -
        public static void YatX_Out_of_Range_Return_Infinity(double xCoord, double x, double y, double radius)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double yCoord = curve.YatX(xCoord);

            Assert.AreEqual(double.PositiveInfinity, yCoord);
        }

        [TestCase(0, 0, 0, 3, 3)] //Aligned with origin
        [TestCase(1.5, 0, 0, 3, 2.598076)] //half radius +
        [TestCase(-1.5, 0, 0, 3, 2.598076)] //half radius -
        [TestCase(3, 0, 0, 3, 0)] //radius +
        [TestCase(-3, 0, 0, 3, 0)] //radius -
        [TestCase(4, 4, 5, 3, 8)] //Offset aligned with origin
        [TestCase(5.5, 4, 5, 3, 7.598076)] //Offset half radius +
        [TestCase(2.5, 4, 5, 3, 7.598076)] //Offset half radius -
        [TestCase(7, 4, 5, 3, 5)] //Offset radius +
        [TestCase(1, 4, 5, 3, 5)] //Offset radius -
        public static void YatX(double xCoord, double x, double y, double radius, double yCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double yCoord = curve.YatX(xCoord);

            Assert.AreEqual(yCoordExpected, yCoord, Tolerance);
        }

        #endregion

        #region Methods: Query  
        [Test]
        public static void IsClosedCurve_Returns_True_When_Curve_Closes_to_Form_a_Shape()
        {
            CircularCurve curve = new CircularCurve(3, new CartesianCoordinate(1, 2));

            Assert.IsTrue(curve.Range.Start.Limit == curve.Range.End.Limit);
            Assert.IsTrue(curve.IsClosedCurve());

            // Change limits
            CartesianCoordinate newRangeLimit = new CartesianCoordinate(-2, 2);
            curve.Range.Start.SetLimitByCoordinate(newRangeLimit);
            curve.Range.End.SetLimitByCoordinate(newRangeLimit);

            Assert.IsTrue(curve.Range.Start.Limit == curve.Range.End.Limit);
            Assert.IsTrue(curve.IsClosedCurve());
        }
        
        [Test]
        public static void IsClosedCurve_Returns_False_When_Curve_Closes_to_Form_a_Shape()
        {
            CircularCurve curve = new CircularCurve(3, new CartesianCoordinate(1, 2));

            Assert.IsTrue(curve.Range.Start.Limit == curve.Range.End.Limit);
            Assert.IsTrue(curve.IsClosedCurve());

            // Change limits
            CartesianCoordinate newRangeLimit = new CartesianCoordinate(-2, 2);
            curve.Range.End.SetLimitByCoordinate(newRangeLimit);

            Assert.IsFalse(curve.Range.Start.Limit == curve.Range.End.Limit);
            Assert.IsFalse(curve.IsClosedCurve());
        }

        [TestCase(0, 0, 6, 11, 0, 5, true)]  // Tangent
        [TestCase(4, 0, 6, 15, 0, 5, true)]  // Tangent, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 5, true)]  // Tangent, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 5, true)]  // Tangent, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, false)]  // Intersection, Translated  and Rotated to quadrant 1
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        public static void IsTangent(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            bool result = curve1.IsTangent(curve2);

            Assert.AreEqual(expectedResult, result);
        }     

        [TestCase(0, 0, 6, 11, 0, 3, false)]  // No intersection
        [TestCase(4, 0, 6, 15, 0, 3, false)]  // No intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 3, false)]  // No intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 3, false)]  // No intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 4
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, true)]  // Intersection, Translated  and Rotated to quadrant 1
        public static void IsIntersecting(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            bool result = curve1.IsIntersecting(curve2);

            Assert.AreEqual(expectedResult, result);
        }
        #endregion

        #region Methods: Properties
        #region Radius
        #region Focus, Right
        [TestCase(0)]
        [TestCase(Numbers.PiOver4)]
        [TestCase(Numbers.Pi)]
        [TestCase(-Numbers.Pi)]
        public static void RadiusAboutFocusRight_is_Constant(double angleRadians)
        {
            double actualRadius = curve.RadiusAboutFocusRight(angleRadians);

            Assert.AreEqual(curveRadius, actualRadius);
        }

        [TestCase(Numbers.PiOver2, 0)] // Vertical up
        [TestCase(3*Numbers.PiOver4, 8.485281)] // 45 deg up left
        [TestCase(Numbers.Pi, 2 * curveRadius)] // straight left
        [TestCase(5*Numbers.PiOver4, 8.485281)] // 45 deg down left
        [TestCase(3*Numbers.PiOver2, 0)] // Vertical down
        [TestCase(0, 0)] // OutOfRange
        public static void RadiusAboutVertexMajorRight(double angleRadians, double expectedRadius)
        {
            double actualRadius = curve.RadiusAboutVertexMajorRight(angleRadians);

            Assert.AreEqual(expectedRadius, actualRadius, Tolerance);
        }
        #endregion
        #region Focus, Left
        [TestCase(0)]
        [TestCase(Numbers.PiOver4)]
        [TestCase(Numbers.Pi)]
        [TestCase(-Numbers.Pi)]
        public static void RadiusAboutFocusLeft_is_Constant(double angleRadians)
        {
            double actualRadius = curve.RadiusAboutFocusLeft(angleRadians);

            Assert.AreEqual(curveRadius, actualRadius);
        }

        [TestCase(Numbers.PiOver2, 0)] // Vertical up
        [TestCase(Numbers.PiOver4, 8.485281)] // 45 deg up right
        [TestCase(0, 2 * curveRadius)] // straight right
        [TestCase(7 * Numbers.PiOver4, 8.485281)] // 45 deg down right
        [TestCase(3 * Numbers.PiOver2, 0)] // Vertical down
        [TestCase(Numbers.Pi, 0)] // OutOfRange
        public static void RadiusAboutVertexMajorLeft(double angleRadians, double expectedRadius)
        {
            double actualRadius = curve.RadiusAboutVertexMajorLeft(angleRadians);

            Assert.AreEqual(expectedRadius, actualRadius, Tolerance);
        }
        #endregion
        [TestCase(0)]
        [TestCase(Numbers.PiOver4)]
        [TestCase(Numbers.Pi)]
        [TestCase(-Numbers.Pi)]
        public static void RadiusAboutOrigin_is_Constant(double angleRadians)
        { 
            double actualRadius = curve.RadiusAboutOrigin(angleRadians);

            Assert.AreEqual(curveRadius, actualRadius);
        }
        #endregion

        [TestCase(0)]
        [TestCase(Numbers.PiOver4)]
        [TestCase(Numbers.Pi)]
        [TestCase(-Numbers.Pi)]
        public static void CurvatureByAngle_is_Constant(double angleRadians)
        {
            double actualCurvature = curve.CurvatureByAngle(angleRadians);

            Assert.AreEqual(1 / curveRadius, actualCurvature, Tolerance);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_String()
        {
            string toString = curve.ToString();
            Assert.AreEqual("CircularCurve - Center: {X: 4, Y: 5}, Radius: 6, I: {X: 10, Y: 5}, J: {X: 10, Y: 5}", toString);
        }

        [TestCase(5, 13, 12, 6, 5, 6, 6, 6.102084, 11.897916, 10.897916, 7.102084)]  // Sloped Intersection in translated coordinates
        public static void IntersectionCoordinates_with_Linear_Curve_Returns_Coordinates(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            double x1Expected, double y1Expected,
            double x2Expected, double y2Expected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = curve2.IntersectionCoordinate(curve1);

            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
            Assert.AreEqual(x2Expected, intersectionCoordinates[1].X, Tolerance);
            Assert.AreEqual(y2Expected, intersectionCoordinates[1].Y, Tolerance);
        }

        [TestCase(5, 14.4852813742386, 13.4852813742386, 6, 5, 6, 6, 9.24264068711928, 10.2426406871193)]  // Sloped Tangent in Quadrant 1 in translated coordinates
        public static void IntersectionCoordinates_of_Tangents_with_Linear_Curve_Returns_Tangent_Coordinate(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            double x1Expected, double y1Expected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = curve2.IntersectionCoordinate(curve1);

            Assert.AreEqual(1, intersectionCoordinates.Length);
            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        }

        [TestCase(5, 16, 15, 6, 5, 6, 6)]  // No Intersection in translated coordinates
        public static void IntersectionCoordinates_of_Not_Intersecting_with_Linear_Curve_Returns_Empty_Array(
            double x1, double y1, double x2, double y2,
            double x, double y, double r)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = curve2.IntersectionCoordinate(curve1);

            Assert.AreEqual(0, intersectionCoordinates.Length);
        }


        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 1
        public static void IntersectionCoordinates_of_Not_Intersecting_with_Circular_Curve_Returns_Empty_Array(
            double x1, double y1, double r1,
            double x2, double y2, double r2)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = curve1.IntersectionCoordinate(curve2);

            Assert.AreEqual(0, intersectionCoordinates.Length);
        }

        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, 9.196152, 8)]  // Tangent, Translated and Rotated to quadrant 1
        public static void IntersectionCoordinates_of_Tangents_with_Circular_Curve_Returns_Tangent_Coordinate(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double x1Expected, double y1Expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = curve1.IntersectionCoordinate(curve2);

            Assert.AreEqual(1, intersectionCoordinates.Length);
            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        }
        
        [TestCase(0, 0, 6, 11, 0, 7, 4.909091, 3.449757, 4.909091, -3.449757)]  // Intersection
        [TestCase(4, 0, 6, 15, 0, 7, 8.909091, 3.449757, 8.909091, -3.449757)]  // Intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 7, 0.909091, 3.449757, 0.909091, -3.449757)]  // Intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 7, 2.526519, 5.442123, 5.976276, -0.533032)]  // Intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, 6.526519, 10.442123, 9.976276, 4.466968)]  // Intersection, Translated and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 7, -9.976276, 4.466968, -6.526519, 10.442123)]  // Intersection, Translated and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 7, -6.526519, -10.442123, -9.976276, -4.466968)]  // Intersection, Translated and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 7, 9.976276, -4.466968, 6.526519, -10.442123)]  // Intersection, Translated and Rotated to quadrant 4
        public static void IntersectionCoordinates_with_Circular_Curve_Returns_Coordinates(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double x1Expected, double y1Expected,
            double x2Expected, double y2Expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = curve1.IntersectionCoordinate(curve2);

            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
            Assert.AreEqual(x2Expected, intersectionCoordinates[1].X, Tolerance);
            Assert.AreEqual(y2Expected, intersectionCoordinates[1].Y, Tolerance);
        }

        [TestCase(10, 5, 10, 5)] //x-axis + outside
        [TestCase(-2, 5, -2, 5)] //x-axis - outside
        [TestCase(4, 11, 4, 11)] //y-axis + outside
        [TestCase(4, -1, 4, -1)] //y-axis - outside
        [TestCase(10, 11, 8.242641, 9.242641)] //Diagonal Outside Quad 1
        [TestCase(-2, 11, -0.242641, 9.242641)] //Diagonal Outside Quad 2
        [TestCase(-2, -1, -0.242641, 0.757359)] //Diagonal Outside Quad 3
        [TestCase(10, -1, 8.242641, 0.757359)] //Diagonal Outside Quad 4
        [TestCase(5, 5, 10, 5)] //x-axis + inside
        [TestCase(3, 5, -2, 5)] //x-axis - inside
        [TestCase(4, 6, 4, 11)] //y-axis + inside
        [TestCase(4, 4, 4, -1)] //y-axis - inside
        [TestCase(5, 6, 8.242641, 9.242641)] //Diagonal inside Quad 1
        [TestCase(3, 6, -0.242641, 9.242641)] //Diagonal inside Quad 2
        [TestCase(3, 4, -0.242641, 0.757359)] //Diagonal inside Quad 3
        [TestCase(5, 4, 8.242641, 0.757359)] //Diagonal inside Quad 4
        public static void CoordinateOfPerpendicularProjection_Circle_Offset(
            double x, double y,
            double xIntersectionNear, double yIntersectionNear)
        {
            CartesianCoordinate point = new CartesianCoordinate(x, y, Tolerance);
            CartesianCoordinate intersection = curve.CoordinateOfPerpendicularProjection(point);

            CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(xIntersectionNear, yIntersectionNear, Tolerance);

            Assert.AreEqual(intersectionExpectedNear, intersection);
        }

        [TestCase(0, 0)]
        [TestCase(4, 5)]
        public static void CoordinateOfPerpendicularProjection_Returns_Infinity_when_Point_Is_at_Local_Origin(
            double xOrigin, double yOrigin)
        {
            CartesianCoordinate point = new CartesianCoordinate(xOrigin, yOrigin, Tolerance);
            CircularCurve curve = new CircularCurve(6, new CartesianCoordinate(xOrigin, yOrigin, Tolerance), Tolerance);
            CartesianCoordinate intersection = curve.CoordinateOfPerpendicularProjection(point);

            CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity, Tolerance);

            Assert.AreEqual(intersectionExpectedNear, intersection);
        }

        [TestCase(10, 5, 10, 5, -2, 5)] //x-axis + outside
        [TestCase(-2, 5, -2, 5, 10, 5)] //x-axis - outside
        [TestCase(4, 11, 4, 11, 4, -1)] //y-axis + outside
        [TestCase(4, -1, 4, -1, 4, 11)] //y-axis - outside
        [TestCase(10, 11, 8.242641, 9.242641, -0.242641, 0.757359)] //Diagonal Outside Quad 1
        [TestCase(-2, 11, -0.242641, 9.242641, 8.242641, 0.757359)] //Diagonal Outside Quad 2
        [TestCase(-2, -1, -0.242641, 0.757359, 8.242641, 9.242641)] //Diagonal Outside Quad 3
        [TestCase(10, -1, 8.242641, 0.757359, -0.242641, 9.242641)] //Diagonal Outside Quad 4
        [TestCase(5, 5, 10, 5, -2, 5)] //x-axis + inside
        [TestCase(3, 5, -2, 5, 10, 5)] //x-axis - inside
        [TestCase(4, 6, 4, 11, 4, -1)] //y-axis + inside
        [TestCase(4, 4, 4, -1, 4, 11)] //y-axis - inside
        [TestCase(5, 6, 8.242641, 9.242641, -0.242641, 0.757359)] //Diagonal inside Quad 1
        [TestCase(3, 6, -0.242641, 9.242641, 8.242641, 0.757359)] //Diagonal inside Quad 2
        [TestCase(3, 4, -0.242641, 0.757359, 8.242641, 9.242641)] //Diagonal inside Quad 3
        [TestCase(5, 4, 8.242641, 0.757359, -0.242641, 9.242641)] //Diagonal inside Quad 4
        public static void CoordinatesOfPerpendicularProjection_Circle_Offset(
            double x, double y,
            double xIntersectionNear, double yIntersectionNear,
            double xIntersectionFar, double yIntersectionFar)
        {
            CartesianCoordinate point = new CartesianCoordinate(x, y, Tolerance);
            Tuple<CartesianCoordinate, CartesianCoordinate> intersections = curve.CoordinatesOfPerpendicularProjection(point);

            CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(xIntersectionNear, yIntersectionNear, Tolerance);
            CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(xIntersectionFar, yIntersectionFar, Tolerance);

            Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
            Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        }

        [TestCase(0, 0)]
        [TestCase(4, 5)]
        public static void CoordinatesOfPerpendicularProjection_Returns_Infinity_when_Point_Is_at_Local_Origin(
            double xOrigin, double yOrigin)
        {
            CartesianCoordinate point = new CartesianCoordinate(xOrigin, yOrigin, Tolerance);
            CircularCurve curve = new CircularCurve(6, new CartesianCoordinate(xOrigin, yOrigin, Tolerance), Tolerance);
            Tuple<CartesianCoordinate, CartesianCoordinate> intersections = curve.CoordinatesOfPerpendicularProjection(point);

            CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity, Tolerance);
            CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity, Tolerance);

            Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
            Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        }

        [TestCase(0, 0)]
        [TestCase(Numbers.PiOver4, 4.712389)]
        [TestCase(Numbers.Pi, 18.849556)]
        [TestCase(Numbers.TwoPi, 37.699112)]
        [TestCase(-Numbers.Pi, -18.849556)]
        [TestCase(-Numbers.TwoPi, -37.699112)]
        public static void LengthBetween(double rotationRadians, double expectedLength)
        {
            double result = curve.LengthBetween(new AngularOffset(rotationRadians, Tolerance));

            Assert.AreEqual(expectedLength, result, Tolerance);
        }
        #endregion

        #region ICurveLimits

        public static void Length()
        {

        }


        public static void LengthBetween_Relative_Positions(double relativePositionStart, double relativePositionEnd)
        {

        }


        public static void TangentVector_by_Relative_Position(double relativePosition)
        {

        }


        public static void NormalVector_by_Relative_Position(double relativePosition)
        {

        }


        public static void CoordinatePolar_by_Relative_Position(double relativePosition)
        {

        }
        #endregion

        #region ICurvePositionCartesian
        [TestCase(4, 0, 0, 3)] // Out of range +
        [TestCase(-4, 0, 0, 3)] // Out of range -
        [TestCase(9, 4, 5, 3)] // Offset out of range +
        [TestCase(1, 4, 5, 3)] // Offset out of range -
        public static void XsAtY_Out_of_Range_Return_Empty_Arrays(double yCoord, double x, double y, double radius)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(0, xCoords.Length);
        }

        [TestCase(0, 0, 0, 3, 3, -3)] //Aligned with origin
        [TestCase(1.5, 0, 0, 3, 2.598076, -2.598076)] //half radius +
        [TestCase(-1.5, 0, 0, 3, 2.598076, -2.598076)] //half radius -
        [TestCase(3, 0, 0, 3, 0, 0)] //radius +
        [TestCase(-3, 0, 0, 3, 0, 0)] //radius -
        [TestCase(5, 4, 5, 3, 7, 1)] //Offset aligned with origin
        [TestCase(6.5, 4, 5, 3, 6.598076, 1.401924)] //Offset half radius +
        [TestCase(3.5, 4, 5, 3, 6.598076, 1.401924)] //Offset half radius -
        [TestCase(8, 4, 5, 3, 4, 4)] //Offset radius +
        [TestCase(2, 4, 5, 3, 4, 4)] //Offset radius -
        public static void XsAtY(double yCoord, double x, double y, double radius, double x1CoordExpected, double x2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(x1CoordExpected, xCoords[0], Tolerance);
            Assert.AreEqual(x2CoordExpected, xCoords[1], Tolerance);
        }

        [TestCase(4, 0, 0, 3)] // Out of range +
        [TestCase(-4, 0, 0, 3)] // Out of range -
        [TestCase(8, 4, 5, 3)] // Offset out of range +
        [TestCase(0, 4, 5, 3)] // Offset out of range -
        public static void YsAtX_Out_of_Range_Return_Empty_Arrays(double xCoord, double x, double y, double radius)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double[] yCoords = curve.YsAtX(xCoord);

            Assert.AreEqual(0, yCoords.Length);
        }

        [TestCase(0, 0, 0, 3, 3, -3)] //Aligned with origin
        [TestCase(1.5, 0, 0, 3, 2.598076, -2.598076)] //half radius +
        [TestCase(-1.5, 0, 0, 3, 2.598076, -2.598076)] //half radius -
        [TestCase(3, 0, 0, 3, 0, 0)] //radius +
        [TestCase(-3, 0, 0, 3, 0, 0)] //radius -
        [TestCase(4, 4, 5, 3, 8, 2)] //Offset aligned with origin
        [TestCase(5.5, 4, 5, 3, 7.598076, 2.401924)] //Offset half radius +
        [TestCase(2.5, 4, 5, 3, 7.598076, 2.401924)] //Offset half radius -
        [TestCase(7, 4, 5, 3, 5, 5)] //Offset radius +
        [TestCase(1, 4, 5, 3, 5, 5)] //Offset radius -
        public static void YsAtX(double xCoord, double x, double y, double radius, double y1CoordExpected, double y2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            CircularCurve curve = new CircularCurve(radius, center, Tolerance);

            double[] yCoords = curve.YsAtX(xCoord);

            Assert.AreEqual(y1CoordExpected, yCoords[0], Tolerance);
            Assert.AreEqual(y2CoordExpected, yCoords[1], Tolerance);
        }

        [TestCase(0, 0, false)] // origin
        [TestCase(4 + curveRadius, 5, true)] // major vertex
        [TestCase(4 - curveRadius, 5, true)] // major vertex2
        [TestCase(4, 5 + curveRadius, true)] // minor vertex
        [TestCase(4, 5 - curveRadius, true)] // minor vertex2
        [TestCase(4 + 4.242641, 5 + 4.242641, true)] // 45 deg rotation
        [TestCase(4.1 + 4.242641, 5 + 4.242640, false)] // close but not quite
        public static void IsIntersectingCoordinate(double x, double y, bool expectedIntersection)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y, Tolerance);
            bool isIntersecting = curve.IsIntersectingCoordinate(coordinate);
            Assert.AreEqual(expectedIntersection, isIntersecting);
        }
        #endregion

        #region ICurvePositionPolar
        [TestCase(0)]
        [TestCase(Numbers.PiOver4)]
        [TestCase(Numbers.Pi)]
        [TestCase(-Numbers.Pi)]
        public static void RadiiAboutOrigin_is_Constant(double angleRadians)
        {
            double[] actualRadius = curve.RadiiAboutOrigin(angleRadians);

            Assert.AreEqual(1, actualRadius.Length);
            Assert.AreEqual(curveRadius, actualRadius[0]);

        }
        #endregion

        #region Methods: Static      

        //public static void LengthBetween_Static(CartesianCoordinate pointI, CartesianCoordinate pointJ)
        //{

        //}


        //public static void LengthBetween_Static(CartesianCoordinate pointI, CartesianCoordinate pointJ, double radius)
        //{

        //}

        [TestCase(0, 0, 0)]
        [TestCase(0, Numbers.PiOver4, 0)]
        [TestCase(-1, Numbers.PiOver4, -0.785398)]
        [TestCase(1, Numbers.PiOver4, 0.785398)]
        [TestCase(2, Numbers.PiOver4, 1.570796)]
        [TestCase(-2, Numbers.PiOver4, -1.570796)]
        [TestCase(2, 0, 0)]
        [TestCase(2, Numbers.Pi, 6.283185)]
        [TestCase(2, Numbers.TwoPi, 12.566371)]
        [TestCase(2, -Numbers.Pi, -6.283185)]
        [TestCase(2, -Numbers.TwoPi, -12.566371)]
        public static void LengthBetween_Static(double rotationRadians, double radius, double expectedArcLength)
        {
            double result = CircularCurve.LengthBetween(new AngularOffset(rotationRadians, Tolerance), radius);

            Assert.AreEqual(expectedArcLength, result, Tolerance);
        }

        #region Aligment        
        [TestCase(0, 0, 6, 11, 0, 5, true)]  // Tangent
        [TestCase(4, 0, 6, 15, 0, 5, true)]  // Tangent, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 5, true)]  // Tangent, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 5, true)]  // Tangent, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, false)]  // Intersection, Translated  and Rotated to quadrant 1
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        public static void AreTangent_Static(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            bool result = CircularCurve.AreTangent(curve1, curve2);
            
            Assert.AreEqual(expectedResult, result);
        }
        #endregion

        #region Intersect        
        [TestCase(0, 0, 6, 11, 0, 3, false)]  // No intersection
        [TestCase(4, 0, 6, 15, 0, 3, false)]  // No intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 3, false)]  // No intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 3, false)]  // No intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 4
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, true)]  // Intersection, Translated  and Rotated to quadrant 1
        public static void AreIntersecting_Static(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            bool result = CircularCurve.AreIntersecting(curve1, curve2);

            Assert.AreEqual(expectedResult, result);
        }
        #endregion

        #region Projection
        [TestCase(6, 0, 6, 0, -6, 0)] //x-axis + outside
        [TestCase(-6, 0, -6, 0, 6, 0)] //x-axis - outside
        [TestCase(0, 6, 0, 6, 0, -6)] //y-axis + outside
        [TestCase(0, -6, 0, -6, 0, 6)] //y-axis - outside
        [TestCase(6, 6, 4.242641, 4.242641, -4.242641, -4.242641)] //Diagonal Outside Quad 1
        [TestCase(-6, 6, -4.242641, 4.242641, 4.242641, -4.242641)] //Diagonal Outside Quad 2
        [TestCase(-6, -6, -4.242641, -4.242641, 4.242641, 4.242641)] //Diagonal Outside Quad 3
        [TestCase(6, -6, 4.242641, -4.242641, -4.242641, 4.242641)] //Diagonal Outside Quad 4
        [TestCase(1, 0, 6, 0, -6, 0)] //x-axis + inside
        [TestCase(-1, 0, -6, 0, 6, 0)] //x-axis - inside
        [TestCase(0, 1, 0, 6, 0, -6)] //y-axis + inside
        [TestCase(0, -1, 0, -6, 0, 6)] //y-axis - inside
        [TestCase(1, 1, 4.242641, 4.242641, -4.242641, -4.242641)] //Diagonal inside Quad 1
        [TestCase(-1, 1, -4.242641, 4.242641, 4.242641, -4.242641)] //Diagonal inside Quad 2
        [TestCase(-1, -1, -4.242641, -4.242641, 4.242641, 4.242641)] //Diagonal inside Quad 3
        [TestCase(1, -1, 4.242641, -4.242641, -4.242641, 4.242641)] //Diagonal inside Quad 4
        public static void CoordinatesOfPerpendicularProjection_Static_Circle_at_Origin(
            double x, double y, 
            double xIntersectionNear, double yIntersectionNear,
            double xIntersectionFar, double yIntersectionFar)
        {
            CartesianCoordinate point = new CartesianCoordinate(x, y, Tolerance);
            CircularCurve curve = new CircularCurve(6, CartesianCoordinate.Origin(), Tolerance);
            Tuple<CartesianCoordinate,CartesianCoordinate> intersections = CircularCurve.CoordinatesOfPerpendicularProjection(point, curve);

            CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(xIntersectionNear, yIntersectionNear, Tolerance);
            CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(xIntersectionFar, yIntersectionFar, Tolerance);

            Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
            Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        }

        [TestCase(10, 5, 10, 5, -2, 5)] //x-axis + outside
        [TestCase(-2, 5, -2, 5, 10, 5)] //x-axis - outside
        [TestCase(4, 11, 4, 11, 4, -1)] //y-axis + outside
        [TestCase(4, -1, 4, -1, 4, 11)] //y-axis - outside
        [TestCase(10, 11, 8.242641, 9.242641, -0.242641, 0.757359)] //Diagonal Outside Quad 1
        [TestCase(-2, 11, -0.242641, 9.242641, 8.242641, 0.757359)] //Diagonal Outside Quad 2
        [TestCase(-2, -1, -0.242641, 0.757359, 8.242641, 9.242641)] //Diagonal Outside Quad 3
        [TestCase(10, -1, 8.242641, 0.757359, -0.242641, 9.242641)] //Diagonal Outside Quad 4
        [TestCase(5, 5, 10, 5, -2, 5)] //x-axis + inside
        [TestCase(3, 5, -2, 5, 10, 5)] //x-axis - inside
        [TestCase(4, 6, 4, 11, 4, -1)] //y-axis + inside
        [TestCase(4, 4, 4, -1, 4, 11)] //y-axis - inside
        [TestCase(5, 6, 8.242641, 9.242641, -0.242641, 0.757359)] //Diagonal inside Quad 1
        [TestCase(3, 6, -0.242641, 9.242641, 8.242641, 0.757359)] //Diagonal inside Quad 2
        [TestCase(3, 4, -0.242641, 0.757359, 8.242641, 9.242641)] //Diagonal inside Quad 3
        [TestCase(5, 4, 8.242641, 0.757359, -0.242641, 9.242641)] //Diagonal inside Quad 4
        public static void CoordinatesOfPerpendicularProjection_Static_Circle_Offset(
            double x, double y,
            double xIntersectionNear, double yIntersectionNear,
            double xIntersectionFar, double yIntersectionFar)
        {
            CartesianCoordinate point = new CartesianCoordinate(x, y, Tolerance);
            Tuple<CartesianCoordinate, CartesianCoordinate> intersections = CircularCurve.CoordinatesOfPerpendicularProjection(point, curve);

            CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(xIntersectionNear, yIntersectionNear, Tolerance);
            CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(xIntersectionFar, yIntersectionFar, Tolerance);

            Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
            Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        }

        [TestCase(0, 0)]
        [TestCase(4, 5)]
        public static void CoordinatesOfPerpendicularProjection_Static_Returns_Infinity_when_Point_Is_at_Local_Origin(
            double xOrigin, double yOrigin)
        {
            CartesianCoordinate point = new CartesianCoordinate(xOrigin, yOrigin, Tolerance);
            CircularCurve curve = new CircularCurve(6, new CartesianCoordinate(xOrigin, yOrigin, Tolerance), Tolerance);
            Tuple<CartesianCoordinate, CartesianCoordinate> intersections = CircularCurve.CoordinatesOfPerpendicularProjection(point, curve);

            CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity, Tolerance);
            CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity, Tolerance);

            Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
            Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        }
        #endregion
        #endregion

        #region ICloneable
        [Test]
        public static void Clone()
        {
            CircularCurve curveCloned = curve.Clone() as CircularCurve;

            Assert.AreEqual(curve.Center, curveCloned.Center);
            Assert.AreEqual(curve.Curvature, curveCloned.Curvature, Tolerance);
            Assert.AreEqual(curve.Radius, curveCloned.Radius, Tolerance);

            Assert.AreEqual(curve.Focus, curveCloned.Focus);
            Assert.AreEqual(curve.Directrix.ControlPointI, curveCloned.Directrix.ControlPointI);
            Assert.AreEqual(curve.Directrix.ControlPointJ, curveCloned.Directrix.ControlPointJ);
            Assert.AreEqual(curve.DistanceFromDirectrixToLocalOrigin, curveCloned.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(curve.DistanceFromFocusToDirectrix, curveCloned.DistanceFromFocusToDirectrix, Tolerance);
            Assert.AreEqual(curve.DistanceFromFocusToLocalOrigin, curveCloned.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(curve.DistanceFromVertexMajorToLocalOrigin, curveCloned.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(curve.DistanceFromVertexMinorToMajorAxis, curveCloned.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(curve.SemilatusRectumDistance, curveCloned.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(curve.Eccentricity, curveCloned.Eccentricity, Tolerance);
            Assert.AreEqual(curve.LocalOrigin, curveCloned.LocalOrigin);
            Assert.AreEqual(curve.Rotation, curveCloned.Rotation);
            Assert.AreEqual(curve.VerticesMajor.Item1, curveCloned.VerticesMajor.Item1);
            Assert.AreEqual(curve.VerticesMajor.Item2, curveCloned.VerticesMajor.Item2);
            Assert.AreEqual(curve.VerticesMinor.Item1, curveCloned.VerticesMinor.Item1);
            Assert.AreEqual(curve.VerticesMinor.Item2, curveCloned.VerticesMinor.Item2);
            Assert.AreEqual(curve.Range.Start.Limit, curveCloned.Range.Start.Limit);
            Assert.AreEqual(curve.Range.End.Limit, curveCloned.Range.End.Limit);
        }

        [Test]
        public static void CloneCurve()
        {
            CircularCurve curveCloned = curve.CloneCurve();

            Assert.AreEqual(curve.Center, curveCloned.Center);
            Assert.AreEqual(curve.Curvature, curveCloned.Curvature, Tolerance);
            Assert.AreEqual(curve.Radius, curveCloned.Radius, Tolerance);

            Assert.AreEqual(curve.Focus, curveCloned.Focus);
            Assert.AreEqual(curve.Directrix.ControlPointI, curveCloned.Directrix.ControlPointI);
            Assert.AreEqual(curve.Directrix.ControlPointJ, curveCloned.Directrix.ControlPointJ);
            Assert.AreEqual(curve.DistanceFromDirectrixToLocalOrigin, curveCloned.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(curve.DistanceFromFocusToDirectrix, curveCloned.DistanceFromFocusToDirectrix, Tolerance);
            Assert.AreEqual(curve.DistanceFromFocusToLocalOrigin, curveCloned.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(curve.DistanceFromVertexMajorToLocalOrigin, curveCloned.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(curve.DistanceFromVertexMinorToMajorAxis, curveCloned.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(curve.SemilatusRectumDistance, curveCloned.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(curve.Eccentricity, curveCloned.Eccentricity, Tolerance);
            Assert.AreEqual(curve.LocalOrigin, curveCloned.LocalOrigin);
            Assert.AreEqual(curve.Rotation, curveCloned.Rotation);
            Assert.AreEqual(curve.VerticesMajor.Item1, curveCloned.VerticesMajor.Item1);
            Assert.AreEqual(curve.VerticesMajor.Item2, curveCloned.VerticesMajor.Item2);
            Assert.AreEqual(curve.VerticesMinor.Item1, curveCloned.VerticesMinor.Item1);
            Assert.AreEqual(curve.VerticesMinor.Item2, curveCloned.VerticesMinor.Item2);
            Assert.AreEqual(curve.Range.Start.Limit, curveCloned.Range.Start.Limit);
            Assert.AreEqual(curve.Range.End.Limit, curveCloned.Range.End.Limit);
        }
        #endregion
    }
}
