using MPT.Math.Coordinates;
using NUnit.Framework;
using System;
using MPT.Math.Curves;
using MPT.Math.Vectors;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.UnitTests.Curves
{
    [TestFixture]
    public static class HyperbolicCurveTests
    {
        public static double Tolerance = 0.00001;

        public static HyperbolicCurve curve;
        public static HyperbolicCurve curveLocal;
        public static HyperbolicCurve curveOffset;
        public static HyperbolicCurve curveRotated;
        public static HyperbolicCurve curveOffsetRotated;
        public static double a = 5;
        public static double b = 3;
        public static double xOffset = 4;
        public static double yOffset = 5;
        public static double rotationRadians = Numbers.PiOver3;

        [SetUp]
        public static void SetUp()
        {
            CartesianCoordinate localOrigin = new CartesianCoordinate(xOffset, yOffset);
            Angle rotation = Angle.Origin();
            curve = new HyperbolicCurve(a, b, localOrigin, rotation, Tolerance);
        }

        #region Initialization       
        [Test]
        public static void Initialization_at_Local_Origin_with_Coordinates()
        {
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            double a = 5;
            double b = 3;
            double c = (a.Squared() + b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (e.Squared() - 1);
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b);
            CartesianCoordinate vertexMinor1_2 = new CartesianCoordinate(-a, b);
            CartesianCoordinate vertexMinor2_2 = new CartesianCoordinate(-a, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            CartesianCoordinate Asymptote1I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote1J = vertexMinor1;
            CartesianCoordinate Asymptote2I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote2J = vertexMinor2;

            // Initialization
            HyperbolicCurve curve = new HyperbolicCurve(a, vertexMajor1, focus1, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c - xe, curve.DistanceFromFocusToDirectrix, Tolerance);

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

            // Complex Properties unique to Hyperbola
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
            Assert.AreEqual(vertexMinor1_2, curve.VerticesMinor2.Item1);
            Assert.AreEqual(vertexMinor2_2, curve.VerticesMinor2.Item2);
            Assert.AreEqual(Asymptote1I, curve.Asymptotes.Item1.ControlPointI);
            Assert.AreEqual(Asymptote1J, curve.Asymptotes.Item1.ControlPointJ);
            Assert.AreEqual(Asymptote2I, curve.Asymptotes.Item2.ControlPointI);
            Assert.AreEqual(Asymptote2J, curve.Asymptotes.Item2.ControlPointJ);
        }

        [Test]
        public static void Initialization_at_Local_Origin_with_Distances()
        {
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            Angle rotation = Angle.Origin();
            double a = 5;
            double b = 3;
            double c = (a.Squared() + b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (e.Squared() - 1);
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b);
            CartesianCoordinate vertexMinor1_2 = new CartesianCoordinate(-a, b);
            CartesianCoordinate vertexMinor2_2 = new CartesianCoordinate(-a, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            CartesianCoordinate Asymptote1I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote1J = vertexMinor1;
            CartesianCoordinate Asymptote2I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote2J = vertexMinor2;

            // Initialization
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c - xe, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(rotation, curve.Rotation);
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

            // Complex Properties unique to Hyperbola
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
            Assert.AreEqual(vertexMinor1_2, curve.VerticesMinor2.Item1);
            Assert.AreEqual(vertexMinor2_2, curve.VerticesMinor2.Item2);
            Assert.AreEqual(Asymptote1I, curve.Asymptotes.Item1.ControlPointI);
            Assert.AreEqual(Asymptote1J, curve.Asymptotes.Item1.ControlPointJ);
            Assert.AreEqual(Asymptote2I, curve.Asymptotes.Item2.ControlPointI);
            Assert.AreEqual(Asymptote2J, curve.Asymptotes.Item2.ControlPointJ);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Coordinates(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            double a = 5;
            double b = 3;
            double c = (a.Squared() + b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (e.Squared() - 1);
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b);
            CartesianCoordinate vertexMinor1_2 = new CartesianCoordinate(-a, b);
            CartesianCoordinate vertexMinor2_2 = new CartesianCoordinate(-a, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            CartesianCoordinate Asymptote1I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote1J = vertexMinor1;
            CartesianCoordinate Asymptote2I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote2J = vertexMinor2;

            // Initialization
            HyperbolicCurve curve = new HyperbolicCurve(a, vertexMajor1, focus1, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c - xe, curve.DistanceFromFocusToDirectrix, Tolerance);

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

            // Complex Properties unique to Hyperbola
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
            Assert.AreEqual(vertexMinor1_2, curve.VerticesMinor2.Item1);
            Assert.AreEqual(vertexMinor2_2, curve.VerticesMinor2.Item2);
            Assert.AreEqual(Asymptote1I, curve.Asymptotes.Item1.ControlPointI);
            Assert.AreEqual(Asymptote1J, curve.Asymptotes.Item1.ControlPointJ);
            Assert.AreEqual(Asymptote2I, curve.Asymptotes.Item2.ControlPointI);
            Assert.AreEqual(Asymptote2J, curve.Asymptotes.Item2.ControlPointJ);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Distances(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            Angle rotation = Angle.Origin();
            double a = 5;
            double b = 3;
            double c = (a.Squared() + b.Squared()).Sqrt();
            double e = c / a;
            double p = a * (e.Squared() - 1);
            double xe = a.Squared() / c;

            CartesianCoordinate vertexMajor1 = new CartesianCoordinate(a, 0);
            CartesianCoordinate vertexMajor2 = new CartesianCoordinate(-a, 0);
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b);
            CartesianCoordinate vertexMinor1_2 = new CartesianCoordinate(-a, b);
            CartesianCoordinate vertexMinor2_2 = new CartesianCoordinate(-a, -b);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0);
            CartesianCoordinate focus2 = new CartesianCoordinate(-c, 0);

            CartesianCoordinate directrix1I = new CartesianCoordinate(xe, 0);
            CartesianCoordinate directrix1J = new CartesianCoordinate(xe, 1);
            CartesianCoordinate directrix2I = new CartesianCoordinate(-xe, 0);
            CartesianCoordinate directrix2J = new CartesianCoordinate(-xe, 1);

            CartesianCoordinate Asymptote1I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote1J = vertexMinor1;
            CartesianCoordinate Asymptote2I = CartesianCoordinate.Origin();
            CartesianCoordinate Asymptote2J = vertexMinor2;

            // Initialization
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            // Simple properties
            Assert.AreEqual(a, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(c, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c - xe, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(rotation, curve.Rotation);
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

            // Complex Properties unique to Hyperbola
            Assert.AreEqual(focus2, curve.Focus2);
            Assert.AreEqual(directrix2I, curve.Directrix2.ControlPointI);
            Assert.AreEqual(directrix2J, curve.Directrix2.ControlPointJ);
            Assert.AreEqual(vertexMinor1_2, curve.VerticesMinor2.Item1);
            Assert.AreEqual(vertexMinor2_2, curve.VerticesMinor2.Item2);
            Assert.AreEqual(Asymptote1I, curve.Asymptotes.Item1.ControlPointI);
            Assert.AreEqual(Asymptote1J, curve.Asymptotes.Item1.ControlPointJ);
            Assert.AreEqual(Asymptote2I, curve.Asymptotes.Item2.ControlPointI);
            Assert.AreEqual(Asymptote2J, curve.Asymptotes.Item2.ControlPointJ);
        }

        [Test]
        public static void Changing_Tolerance_Cascades_to_Properties()
        {

            double defaultTolerance = 10E-6;

            Assert.AreEqual(defaultTolerance, curve.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Focus.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Rotation.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMajor.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMajor.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointJ.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.LocalOrigin.Tolerance);

            Assert.AreEqual(defaultTolerance, curve.Focus2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix2.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix2.ControlPointJ.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor2.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.VerticesMinor2.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Asymptotes.Item1.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Asymptotes.Item1.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Asymptotes.Item1.ControlPointJ.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Asymptotes.Item2.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Asymptotes.Item2.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Asymptotes.Item2.ControlPointJ.Tolerance);

            double newTolerance = 10E-3;
            curve.Tolerance = newTolerance;

            Assert.AreEqual(newTolerance, curve.Tolerance);
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

            Assert.AreEqual(newTolerance, curve.Focus2.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix2.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix2.ControlPointI.Tolerance);
            Assert.AreEqual(newTolerance, curve.Directrix2.ControlPointJ.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMinor2.Item1.Tolerance);
            Assert.AreEqual(newTolerance, curve.VerticesMinor2.Item2.Tolerance);
            Assert.AreEqual(newTolerance, curve.Asymptotes.Item1.Tolerance);
            Assert.AreEqual(newTolerance, curve.Asymptotes.Item1.ControlPointI.Tolerance);
            Assert.AreEqual(newTolerance, curve.Asymptotes.Item1.ControlPointJ.Tolerance);
            Assert.AreEqual(newTolerance, curve.Asymptotes.Item2.Tolerance);
            Assert.AreEqual(newTolerance, curve.Asymptotes.Item2.ControlPointI.Tolerance);
            Assert.AreEqual(newTolerance, curve.Asymptotes.Item2.ControlPointJ.Tolerance);
        }
        #endregion

        #region Curve Position

        public static void XatY_Out_of_Range_Return_Infinity(double yCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(double.PositiveInfinity, xCoord);
        }


        public static void XatY(double yCoord, double x, double y, double rotation, double xCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(xCoordExpected, xCoord, Tolerance);
        }


        public static void YatX_Out_of_Range_Return_Infinity(double xCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            double yCoord = curve.YatX(xCoord);

            Assert.AreEqual(double.PositiveInfinity, yCoord);
        }


        public static void YatX(double xCoord, double x, double y, double rotation, double yCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

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
            Assert.AreEqual("HyperbolicCurve - Center: {X: 4, Y: 5}, Rotation: 0 rad, a: 5, b: 3, I: {X: 9, Y: 5}, J: {X: 9, Y: 5}", toString);
        }
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
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(0, xCoords.Length);
        }


        public static void XsAtY(double yCoord, double x, double y, double rotation, double x1CoordExpected, double x2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(x1CoordExpected, xCoords[0], Tolerance);
            Assert.AreEqual(x2CoordExpected, xCoords[1], Tolerance);
        }


        public static void YsAtX_Out_of_Range_Return_Empty_Arrays(double xCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

            double[] yCoords = curve.YsAtX(xCoord);

            Assert.AreEqual(0, yCoords.Length);
        }


        public static void YsAtX(double xCoord, double x, double y, double rotation, double y1CoordExpected, double y2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            HyperbolicCurve curve = new HyperbolicCurve(a, b, center, rotation, Tolerance);

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

        #region ICloneable
        [Test]
        public static void Clone()
        {
            HyperbolicCurve curveCloned = curve.Clone() as HyperbolicCurve;

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
            
            Assert.AreEqual(curve.Focus2, curveCloned.Focus2);
            Assert.AreEqual(curve.VerticesMinor2.Item1, curveCloned.VerticesMinor2.Item1);
            Assert.AreEqual(curve.VerticesMinor2.Item2, curveCloned.VerticesMinor2.Item2);
            Assert.AreEqual(curve.Directrix2.ControlPointI, curveCloned.Directrix2.ControlPointI);
            Assert.AreEqual(curve.Directrix2.ControlPointJ, curveCloned.Directrix2.ControlPointJ);
            Assert.AreEqual(curve.Asymptotes.Item1.ControlPointI, curveCloned.Asymptotes.Item1.ControlPointI);
            Assert.AreEqual(curve.Asymptotes.Item1.ControlPointJ, curveCloned.Asymptotes.Item1.ControlPointJ);
            Assert.AreEqual(curve.Asymptotes.Item2.ControlPointI, curveCloned.Asymptotes.Item2.ControlPointI);
            Assert.AreEqual(curve.Asymptotes.Item2.ControlPointJ, curveCloned.Asymptotes.Item2.ControlPointJ);
            
            Assert.AreEqual(curve.Range.Start.Limit, curveCloned.Range.Start.Limit);
            Assert.AreEqual(curve.Range.End.Limit, curveCloned.Range.End.Limit);
        }

        [Test]
        public static void CloneCurve()
        {
            HyperbolicCurve curveCloned = curve.CloneCurve();

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

            Assert.AreEqual(curve.Focus2, curveCloned.Focus2);
            Assert.AreEqual(curve.VerticesMinor2.Item1, curveCloned.VerticesMinor2.Item1);
            Assert.AreEqual(curve.VerticesMinor2.Item2, curveCloned.VerticesMinor2.Item2);
            Assert.AreEqual(curve.Directrix2.ControlPointI, curveCloned.Directrix2.ControlPointI);
            Assert.AreEqual(curve.Directrix2.ControlPointJ, curveCloned.Directrix2.ControlPointJ);
            Assert.AreEqual(curve.Asymptotes.Item1.ControlPointI, curveCloned.Asymptotes.Item1.ControlPointI);
            Assert.AreEqual(curve.Asymptotes.Item1.ControlPointJ, curveCloned.Asymptotes.Item1.ControlPointJ);
            Assert.AreEqual(curve.Asymptotes.Item2.ControlPointI, curveCloned.Asymptotes.Item2.ControlPointI);
            Assert.AreEqual(curve.Asymptotes.Item2.ControlPointJ, curveCloned.Asymptotes.Item2.ControlPointJ);

            Assert.AreEqual(curve.Range.Start.Limit, curveCloned.Range.Start.Limit);
            Assert.AreEqual(curve.Range.End.Limit, curveCloned.Range.End.Limit);
        }
        #endregion
    }
}
