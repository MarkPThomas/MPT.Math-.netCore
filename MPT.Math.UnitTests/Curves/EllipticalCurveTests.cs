using MPT.Math.Coordinates;
using NUnit.Framework;
using System;
using MPT.Math.Curves;
using MPT.Math.Vectors;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.UnitTests.Curves
{
    [TestFixture]
    public static class EllipticalCurveTests
    {
        public static double Tolerance = 0.00001;

        public static EllipticalCurve curve;
        public static EllipticalCurve curveLocal;
        public static EllipticalCurve curveOffset;
        public static EllipticalCurve curveRotated;
        public static EllipticalCurve curveOffsetRotated;
        public static double a = 5;
        public static double b = 3;
        public static double xOffset = 4;
        public static double yOffset = 5;
        public static double rotationRadians = Numbers.PiOver3;

        [SetUp]
        public static void SetUp()
        {
            CartesianCoordinate center = new CartesianCoordinate(xOffset, yOffset);
            Angle rotation = Angle.Origin();
            curve = new EllipticalCurve(a, b, center, rotation, Tolerance);
        }

        #region Initialization
        // TODO: Handle cases of a < b

        [Test]
        public static void Initialization_at_Local_Origin_with_Coordinates()
        {
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            double a = 5;
            double b = 3;
            double c = (a.Squared() - b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (1 - e.Squared());
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            // Initialization
            EllipticalCurve curve = new EllipticalCurve(vertexMajor1, b, center, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(xe - c, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(Angle.Origin(), curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);

            // Complex Properties unique to Ellipse
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
        }

        [Test]
        public static void Initialization_at_Local_Origin_with_Radii()
        { 
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            Angle rotation = Angle.Origin();
            double a = 5;
            double b = 3;
            double c = (a.Squared() - b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (1 - e.Squared());
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            // Initialization
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(xe - c, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(Angle.Origin(), curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);

            // Complex Properties unique to Ellipse
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Coordinates(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            double a = 5;
            double b = 3;
            double c = (a.Squared() - b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (1 - e.Squared());
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            // Initialization
            EllipticalCurve curve = new EllipticalCurve(vertexMajor1, b, center, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(xe - c, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(Angle.Origin(), curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);

            // Complex Properties unique to Ellipse
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Radii(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            Angle rotation = Angle.Origin();
            double a = 5;
            double b = 3;
            double c = (a.Squared() - b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (1 - e.Squared());
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(0, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(0, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            // Initialization
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(xe - c, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(Angle.Origin(), curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor2, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);

            // Complex Properties unique to Ellipse
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
        }

        [Test]
        public static void Changing_Tolerance_Cascades_to_Properties()
        {
            double defaultTolerance = 10E-6;

            Assert.AreEqual(defaultTolerance, curve.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Rotation.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMajor.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMajor.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointJ.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix2.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix2.ControlPointJ.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Focus.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Focus2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.LocalOrigin.Tolerance);

            double newTolerance = 10E-3;
            curve.Tolerance = newTolerance;

            Assert.AreEqual(newTolerance, curve.Tolerance);
            Assert.AreEqual(newTolerance, curve.Rotation.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMajor.Item1.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMajor.Item2.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMinor.Item1.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMinor.Item2.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix.ControlPointI.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix.ControlPointJ.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix2.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix2.ControlPointI.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix2.ControlPointJ.Tolerance);
            Assert.AreEqual(newTolerance, curve.Focus.Tolerance);
            Assert.AreEqual(newTolerance, curve.Focus2.Tolerance);
            Assert.AreEqual(newTolerance, curve.LocalOrigin.Tolerance);
        }
        #endregion

        #region Curve Position

        public static void XatY_Out_of_Range_Return_Infinity(double yCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(double.PositiveInfinity, xCoord);
        }


        public static void XatY(double yCoord, double x, double y, double rotation, double xCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(xCoordExpected, xCoord, Tolerance);
        }


        public static void YatX_Out_of_Range_Return_Infinity(double xCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double yCoord = curve.YatX(xCoord);

            Assert.AreEqual(double.PositiveInfinity, yCoord);
        }


        public static void YatX(double xCoord, double x, double y, double rotation, double yCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double yCoord = curve.YatX(xCoord);

            Assert.AreEqual(yCoordExpected, yCoord, Tolerance);
        }

        public static void XbyRotationAboutOrigin(double angleRadians)
        {

        }


        public static void YbyRotationAboutOrigin(double angleRadians)
        {

        }


        public static void XbyRotationAboutFocusRight(double angleRadians)
        {

        }


        public static void YbyRotationAboutFocusRight(double angleRadians)
        {

        }


        public static void XbyRotationAboutFocusLeft(double angleRadians)
        {

        }


        public static void YbyRotationAboutFocusLeft(double angleRadians)
        {

        }
        #endregion

        #region Methods: Query  
        [Test]
        public static void IsClosedCurve_Returns_True_When_Curve_Closes_to_Form_a_Shape()
        {
            EllipticalCurve curve = new EllipticalCurve(a, b, CartesianCoordinate.Origin(), Angle.Origin(), Tolerance);

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
        public static void IsClosedCurve_Returns_False_When_Curve_Does_Not_Close_to_Form_a_Shape()
        {
            EllipticalCurve curve = new EllipticalCurve(a, b, CartesianCoordinate.Origin(), Angle.Origin(), Tolerance);

            Assert.IsTrue(curve.Range.Start.Limit == curve.Range.End.Limit);
            Assert.IsTrue(curve.IsClosedCurve());

            // Change limits
            CartesianCoordinate newRangeLimit = new CartesianCoordinate(-2, 2);
            curve.Range.End.SetLimitByCoordinate(newRangeLimit);

            Assert.IsFalse(curve.Range.Start.Limit == curve.Range.End.Limit);
            Assert.IsFalse(curve.IsClosedCurve());
        }

        //[TestCase(0, 0, 6, 11, 0, 5, true)]  // Tangent
        //[TestCase(4, 0, 6, 15, 0, 5, true)]  // Tangent, Shifted+
        //[TestCase(-4, 0, 6, 7, 0, 5, true)]  // Tangent, Shifted-
        //[TestCase(0, 0, 6, 9.52627944162883, 5.5, 5, true)]  // Tangent, Rotated+
        //[TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 1
        //[TestCase(-4, 5, 6, -13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 2
        //[TestCase(-4, -5, 6, -13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 3
        //[TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        //[TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, false)]  // Intersection, Translated  and Rotated to quadrant 1
        //[TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        //public static void IsTangent(
        //    double x1, double y1, double r1,
        //    double x2, double y2, double r2,
        //    bool expectedResult)
        //{
        //    CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
        //    curve1.Tolerance = Tolerance;
        //    CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
        //    curve2.Tolerance = Tolerance;

        //    bool result = curve1.IsTangent(curve2);

        //    Assert.AreEqual(expectedResult, result);
        //}

        //[TestCase(0, 0, 6, 11, 0, 3, false)]  // No intersection
        //[TestCase(4, 0, 6, 15, 0, 3, false)]  // No intersection, Shifted+
        //[TestCase(-4, 0, 6, 7, 0, 3, false)]  // No intersection, Shifted-
        //[TestCase(0, 0, 6, 9.52627944162883, 5.5, 3, false)]  // No intersection, Rotated+
        //[TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        //[TestCase(-4, 5, 6, -13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 2
        //[TestCase(-4, -5, 6, -13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 3
        //[TestCase(4, -5, 6, 13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 4
        //[TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        //[TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, true)]  // Intersection, Translated  and Rotated to quadrant 1
        //public static void IsIntersecting(
        //    double x1, double y1, double r1,
        //    double x2, double y2, double r2,
        //    bool expectedResult)
        //{
        //    CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
        //    curve1.Tolerance = Tolerance;
        //    CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
        //    curve2.Tolerance = Tolerance;

        //    bool result = curve1.IsIntersecting(curve2);

        //    Assert.AreEqual(expectedResult, result);
        //}
        #endregion

        #region Methods: Properties
        #region Radius
        #region Focus, Right

        public static void RadiusAboutFocusRight(double angleRadians, double expectedRadius)
        {
            double actualRadius = curve.RadiusAboutFocusRight(angleRadians);

            Assert.AreEqual(expectedRadius, actualRadius);
        }


        public static void RadiusAboutVertexMajorRight(double angleRadians, double expectedRadius)
        {
            double actualRadius = curve.RadiusAboutVertexMajorRight(angleRadians);

            Assert.AreEqual(expectedRadius, actualRadius, Tolerance);
        }
        #endregion
        #region Focus, Left

        public static void RadiusAboutFocusLeft(double angleRadians, double expectedRadius)
        {
            double actualRadius = curve.RadiusAboutFocusLeft(angleRadians);

            Assert.AreEqual(expectedRadius, actualRadius);
        }


        public static void RadiusAboutVertexMajorLeft(double angleRadians, double expectedRadius)
        {
            double actualRadius = curve.RadiusAboutVertexMajorLeft(angleRadians);

            Assert.AreEqual(expectedRadius, actualRadius, Tolerance);
        }
        #endregion

        public static void RadiusAboutOrigin(double angleRadians, double expectedRadius)
        {
            double actualRadius = curve.RadiusAboutOrigin(angleRadians);

            Assert.AreEqual(expectedRadius, actualRadius);
        }
        #endregion


        public static void LengthBetween(double rotationRadians, double expectedLength)
        {
            double result = curve.LengthBetween(new AngularOffset(rotationRadians, Tolerance));

            Assert.AreEqual(expectedLength, result, Tolerance);
        }


        public static void CurvatureByAngle(double angleRadians, double expectedCurvature)
        {
            double actualCurvature = curve.CurvatureByAngle(angleRadians);

            Assert.AreEqual(expectedCurvature, actualCurvature, Tolerance);
        }


        public static void SlopeAtX(double x)
        {

        }


        public static void SlopeAtY(double y)
        {

        }


        public static void SlopeByAngle(double angleRadians)
        {

        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_String()
        {
            string toString = curve.ToString();
            Assert.AreEqual("EllipticalCurve - Center: {X: 4, Y: 5}, Rotation: 0 rad, a: 5, b: 3, I: {X: 9, Y: 5}, J: {X: 9, Y: 5}", toString);
        }


        //public static void IntersectionCoordinates_with_Linear_Curve_Returns_Coordinates(
        //    double x1, double y1, double x2, double y2,
        //    double x, double y, double rotation,
        //    double x1Expected, double y1Expected,
        //    double x2Expected, double y2Expected)
        //{
        //    LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
        //    curve1.Tolerance = Tolerance;
        //    EllipticalCurve curve2 = new EllipticalCurve(a, b, new CartesianCoordinate(x, y), rotation);
        //    curve2.Tolerance = Tolerance;

        //    CartesianCoordinate[] intersectionCoordinates = curve2.IntersectionCoordinate(curve1);

        //    Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
        //    Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        //    Assert.AreEqual(x2Expected, intersectionCoordinates[1].X, Tolerance);
        //    Assert.AreEqual(y2Expected, intersectionCoordinates[1].Y, Tolerance);
        //}


        //public static void IntersectionCoordinates_of_Tangents_with_Linear_Curve_Returns_Tangent_Coordinate(
        //    double x1, double y1, double x2, double y2,
        //    double x, double y, double rotation,
        //    double x1Expected, double y1Expected)
        //{
        //    LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
        //    curve1.Tolerance = Tolerance;
        //    EllipticalCurve curve2 = new EllipticalCurve(a, b, new CartesianCoordinate(x, y), rotation);
        //    curve2.Tolerance = Tolerance;

        //    CartesianCoordinate[] intersectionCoordinates = curve2.IntersectionCoordinate(curve1);

        //    Assert.AreEqual(1, intersectionCoordinates.Length);
        //    Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
        //    Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        //}


        //public static void IntersectionCoordinates_of_Not_Intersecting_with_Linear_Curve_Returns_Empty_Array(
        //    double x1, double y1, double x2, double y2,
        //    double x, double y, double rotation)
        //{
        //    LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
        //    curve1.Tolerance = Tolerance;
        //    EllipticalCurve curve2 = new EllipticalCurve(a, b, new CartesianCoordinate(x, y), rotation);
        //    curve2.Tolerance = Tolerance;

        //    CartesianCoordinate[] intersectionCoordinates = curve2.IntersectionCoordinate(curve1);

        //    Assert.AreEqual(0, intersectionCoordinates.Length);
        //}
        #endregion

        #region ICurveLimits

        public static void Length()
        {

        }


        public static void LengthBetween_Relative_Positions(double relativePositionStart, double relativePositionEnd)
        {

        }


        public static void ChordLength()
        {

        }


        public static void ChordLengthBetween(double relativePositionStart, double relativePositionEnd)
        {

        }


        public static void Chord()
        {

        }


        public static void ChordBetween(double relativePositionStart, double relativePositionEnd)
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

        public static void XsAtY_Out_of_Range_Return_Empty_Arrays(double yCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(0, xCoords.Length);
        }


        public static void XsAtY(double yCoord, double x, double y, double rotation, double x1CoordExpected, double x2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(x1CoordExpected, xCoords[0], Tolerance);
            Assert.AreEqual(x2CoordExpected, xCoords[1], Tolerance);
        }


        public static void YsAtX_Out_of_Range_Return_Empty_Arrays(double xCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double[] yCoords = curve.YsAtX(xCoord);

            Assert.AreEqual(0, yCoords.Length);
        }


        public static void YsAtX(double xCoord, double x, double y, double rotation, double y1CoordExpected, double y2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            EllipticalCurve curve = new EllipticalCurve(a, b, center, rotation, Tolerance);

            double[] yCoords = curve.YsAtX(xCoord);

            Assert.AreEqual(y1CoordExpected, yCoords[0], Tolerance);
            Assert.AreEqual(y2CoordExpected, yCoords[1], Tolerance);
        }


        public static void IsIntersectingCoordinate(double x, double y, bool expectedIntersection)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y, Tolerance);
            bool isIntersecting = curve.IsIntersectingCoordinate(coordinate);
            Assert.AreEqual(expectedIntersection, isIntersecting);
        }
        #endregion

        #region ICurvePositionPolar

        public static void RadiiAboutOrigin(double angleRadians, double expectedRadius)
        {
            double[] actualRadius = curve.RadiiAboutOrigin(angleRadians);

            Assert.AreEqual(1, actualRadius.Length);
            Assert.AreEqual(expectedRadius, actualRadius[0]);

        }
        #endregion

        //#region Methods: Static    

        //public static void LengthBetween_Static(double rotationRadians, double radius, double expectedArcLength)
        //{
        //    double result = EllipticalCurve.LengthBetween(new AngularOffset(rotationRadians, Tolerance), radius);

        //    Assert.AreEqual(expectedArcLength, result, Tolerance);
        //}

        //#region Aligment        

        //public static void AreTangent_Static(
        //    double x1, double y1, double a1, double b1, double rotation1,
        //    double x2, double y2, double a2, double b2, double rotation2,
        //    bool expectedResult)
        //{
        //    EllipticalCurve curve1 = new EllipticalCurve(a1, b1, new CartesianCoordinate(x1, y1), rotation1);
        //    curve1.Tolerance = Tolerance;
        //    EllipticalCurve curve2 = new EllipticalCurve(a2, b2, new CartesianCoordinate(x2, y2), rotation2);
        //    curve2.Tolerance = Tolerance;

        //    bool result = CircularCurve.AreTangent(curve1, curve2);

        //    Assert.AreEqual(expectedResult, result);
        //}
        //#endregion

        //#region Intersect        

        //public static void AreIntersecting_Static(
        //    double x1, double y1, double a1, double b1, double rotation1,
        //    double x2, double y2, double a2, double b2, double rotation2,
        //    bool expectedResult)
        //{
        //    EllipticalCurve curve1 = new EllipticalCurve(a1, b1, new CartesianCoordinate(x1, y1), rotation1);
        //    curve1.Tolerance = Tolerance;
        //    EllipticalCurve curve2 = new EllipticalCurve(a2, b2, new CartesianCoordinate(x2, y2), rotation2);
        //    curve2.Tolerance = Tolerance;

        //    bool result = EllipticalCurve.AreIntersecting(curve1, curve2);

        //    Assert.AreEqual(expectedResult, result);
        //}
        //#endregion

        //#region Projection

        //public static void CoordinatesOfPerpendicularProjection_Static_Circle_at_Origin(
        //    double x, double y,
        //    double xIntersectionNear, double yIntersectionNear,
        //    double xIntersectionFar, double yIntersectionFar)
        //{
        //    CartesianCoordinate point = new CartesianCoordinate(x, y, Tolerance);
        //    Tuple<CartesianCoordinate, CartesianCoordinate> intersections = EllipticalCurve.CoordinatesOfPerpendicularProjection(point, curve);

        //    CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(xIntersectionNear, yIntersectionNear, Tolerance);
        //    CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(xIntersectionFar, yIntersectionFar, Tolerance);

        //    Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
        //    Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        //}


        //public static void CoordinatesOfPerpendicularProjection_Static_Circle_Offset(
        //    double x, double y,
        //    double xIntersectionNear, double yIntersectionNear,
        //    double xIntersectionFar, double yIntersectionFar)
        //{
        //    CartesianCoordinate point = new CartesianCoordinate(x, y, Tolerance);
        //    Tuple<CartesianCoordinate, CartesianCoordinate> intersections = EllipticalCurve.CoordinatesOfPerpendicularProjection(point, curve);

        //    CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(xIntersectionNear, yIntersectionNear, Tolerance);
        //    CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(xIntersectionFar, yIntersectionFar, Tolerance);

        //    Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
        //    Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        //}


        //public static void CoordinatesOfPerpendicularProjection_Static_Returns_Infinity_when_Point_Is_at_Local_Origin(
        //    double xOrigin, double yOrigin)
        //{
        //    CartesianCoordinate point = new CartesianCoordinate(xOrigin, yOrigin, Tolerance);
        //    CircularCurve curve = new CircularCurve(6, new CartesianCoordinate(xOrigin, yOrigin, Tolerance), Tolerance);
        //    Tuple<CartesianCoordinate, CartesianCoordinate> intersections = EllipticalCurve.CoordinatesOfPerpendicularProjection(point, curve);

        //    CartesianCoordinate intersectionExpectedNear = new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity, Tolerance);
        //    CartesianCoordinate intersectionExpectedFar = new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity, Tolerance);

        //    Assert.AreEqual(intersectionExpectedNear, intersections.Item1);
        //    Assert.AreEqual(intersectionExpectedFar, intersections.Item2);
        //}
        //#endregion
        //#endregion

        #region ICloneable
        [Test]
        public static void Clone()
        {
            EllipticalCurve curveCloned = curve.Clone() as EllipticalCurve;

            Assert.AreEqual(curve.Focus, curveCloned.Focus);
            Assert.AreEqual(curve.Focus2, curveCloned.Focus2);
            Assert.AreEqual(curve.Directrix.ControlPointI, curveCloned.Directrix.ControlPointI);
            Assert.AreEqual(curve.Directrix.ControlPointJ, curveCloned.Directrix.ControlPointJ);
            Assert.AreEqual(curve.Directrix2.ControlPointI, curveCloned.Directrix2.ControlPointI);
            Assert.AreEqual(curve.Directrix2.ControlPointJ, curveCloned.Directrix2.ControlPointJ);
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
            EllipticalCurve curveCloned = curve.CloneCurve();

            Assert.AreEqual(curve.Focus, curveCloned.Focus);
            Assert.AreEqual(curve.Focus2, curveCloned.Focus2);
            Assert.AreEqual(curve.Directrix.ControlPointI, curveCloned.Directrix.ControlPointI);
            Assert.AreEqual(curve.Directrix.ControlPointJ, curveCloned.Directrix.ControlPointJ);
            Assert.AreEqual(curve.Directrix2.ControlPointI, curveCloned.Directrix2.ControlPointI);
            Assert.AreEqual(curve.Directrix2.ControlPointJ, curveCloned.Directrix2.ControlPointJ);
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
