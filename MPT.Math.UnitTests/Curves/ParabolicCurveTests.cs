using MPT.Math.Coordinates;
using NUnit.Framework;
using System;
using MPT.Math.Curves;
using MPT.Math.Vectors;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.UnitTests.Curves
{
    [TestFixture]
    public static class ParabolicCurveTests
    {
        public static double Tolerance = 0.00001;

        public static ParabolicCurve curve;
        public static ParabolicCurve curveLocal;
        public static ParabolicCurve curveOffset;
        public static ParabolicCurve curveRotated;
        public static ParabolicCurve curveOffsetRotated;
        public static double a = 5;
        public static double xOffset = 4;
        public static double yOffset = 5;
        public static double rotationRadians = Numbers.PiOver3;

        [SetUp]
        public static void SetUp()
        {
            double a = 5;
            CartesianCoordinate localOrigin = new CartesianCoordinate(xOffset, yOffset);
            Angle rotation = Angle.Origin();
            curve = new ParabolicCurve(a, localOrigin, rotation, Tolerance);
        }

        #region Initialization        
        [Test]
        public static void Initialization_at_Local_Origin_with_Coordinates()
        {
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            double a = 5;
            double b = 2*a;
            double c = a;
            double e = 1;
            double p = 2*a;
            double xe = a;

            CartesianCoordinate vertexMajor1 = center;
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b, Tolerance);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b, Tolerance);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0, Tolerance);

            CartesianCoordinate directrix1I = new CartesianCoordinate(-xe, 0, Tolerance);
            CartesianCoordinate directrix1J = new CartesianCoordinate(-xe, 1, Tolerance);

            // Initialization
            ParabolicCurve curve = new ParabolicCurve(vertexMajor1, focus1, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(a, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c + xe, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(Angle.Origin(), curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
        }

        [Test]
        public static void Initialization_at_Local_Origin_with_Distances()
        {
            // Expected Complex Results
            CartesianCoordinate center = CartesianCoordinate.Origin();
            Angle rotation = Angle.Origin();
            double a = 5;
            double b = 2 * a;
            double c = a;
            double e = 1;
            double p = 2 * a;
            double xe = a;

            CartesianCoordinate vertexMajor1 = center;
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b, Tolerance);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b, Tolerance);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0, Tolerance);

            CartesianCoordinate directrix1I = new CartesianCoordinate(-xe, 0, Tolerance);
            CartesianCoordinate directrix1J = new CartesianCoordinate(-xe, 1, Tolerance);

            // Initialization
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(a, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c + xe, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(Angle.Origin(), curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Coordinates(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            double a = 5;
            double b = 2 * a;
            double c = a;
            double e = 1;
            double p = 2 * a;
            double xe = a;

            CartesianCoordinate vertexMajor1 = center;
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b, Tolerance);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b, Tolerance);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0, Tolerance);

            CartesianCoordinate directrix1I = new CartesianCoordinate(-xe, 0, Tolerance);
            CartesianCoordinate directrix1J = new CartesianCoordinate(-xe, 1, Tolerance);

            // Initialization
            ParabolicCurve curve = new ParabolicCurve(vertexMajor1, focus1, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(a, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c + xe, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(Angle.Origin(), curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
        }

        [TestCase(0, 0)]
        public static void Initialization_at_Offset_From_Origin_with_Distances(double x, double y)
        {
            // Expected Complex Results
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            Angle rotation = Angle.Origin();
            double a = 5;
            double b = 2 * a;
            double c = a;
            double e = 1;
            double p = 2 * a;
            double xe = a;

            CartesianCoordinate vertexMajor1 = center;
            CartesianCoordinate vertexMinor1 = new CartesianCoordinate(a, b, Tolerance);
            CartesianCoordinate vertexMinor2 = new CartesianCoordinate(a, -b, Tolerance);
            CartesianCoordinate focus1 = new CartesianCoordinate(c, 0, Tolerance);

            CartesianCoordinate directrix1I = new CartesianCoordinate(-xe, 0, Tolerance);
            CartesianCoordinate directrix1J = new CartesianCoordinate(-xe, 1, Tolerance);

            // Initialization
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            // Simple properties
            Assert.AreEqual(0, curve.DistanceFromVertexMajorToLocalOrigin, Tolerance);
            Assert.AreEqual(b, curve.DistanceFromVertexMinorToMajorAxis, Tolerance);
            Assert.AreEqual(a, curve.DistanceFromFocusToLocalOrigin, Tolerance);
            Assert.AreEqual(e, curve.Eccentricity, Tolerance);
            Assert.AreEqual(p, curve.SemilatusRectumDistance, Tolerance);
            Assert.AreEqual(xe, curve.DistanceFromDirectrixToLocalOrigin, Tolerance);
            Assert.AreEqual(c + xe, curve.DistanceFromFocusToDirectrix, Tolerance);

            // Position properties
            Assert.AreEqual(rotation, curve.Rotation);
            Assert.AreEqual(focus1, curve.Focus);
            Assert.AreEqual(center, curve.LocalOrigin);

            // Coordinate properties
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item1);
            Assert.AreEqual(vertexMajor1, curve.VerticesMajor.Item2);
            Assert.AreEqual(vertexMinor1, curve.VerticesMinor.Item1);
            Assert.AreEqual(vertexMinor2, curve.VerticesMinor.Item2);
            Assert.AreEqual(directrix1I, curve.Directrix.ControlPointI);
            Assert.AreEqual(directrix1J, curve.Directrix.ControlPointJ);
            Assert.AreEqual(vertexMajor1, curve.Range.Start.Limit);
            Assert.AreEqual(vertexMajor1, curve.Range.End.Limit);
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
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointI.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.ControlPointJ.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.Directrix.Tolerance);
            Assert.AreEqual(defaultTolerance, curve.LocalOrigin.Tolerance);

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
        }
        #endregion

        #region Curve Position

        public static void XatY_Out_of_Range_Return_Infinity(double yCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(double.PositiveInfinity, xCoord);
        }


        public static void XatY(double yCoord, double x, double y, double rotation, double xCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            double xCoord = curve.XatY(yCoord);

            Assert.AreEqual(xCoordExpected, xCoord, Tolerance);
        }


        public static void YatX_Out_of_Range_Return_Infinity(double xCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            double yCoord = curve.YatX(xCoord);

            Assert.AreEqual(double.PositiveInfinity, yCoord);
        }


        public static void YatX(double xCoord, double x, double y, double rotation, double yCoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

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
            Assert.AreEqual("ParabolicCurve - Center: {X: 4, Y: 5}, Rotation: 0 rad, c: 5, I: {X: 4, Y: 5}, J: {X: 4, Y: 5}", toString);
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
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(0, xCoords.Length);
        }


        public static void XsAtY(double yCoord, double x, double y, double rotation, double x1CoordExpected, double x2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            double[] xCoords = curve.XsAtY(yCoord);

            Assert.AreEqual(x1CoordExpected, xCoords[0], Tolerance);
            Assert.AreEqual(x2CoordExpected, xCoords[1], Tolerance);
        }


        public static void YsAtX_Out_of_Range_Return_Empty_Arrays(double xCoord, double x, double y, double rotation)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

            double[] yCoords = curve.YsAtX(xCoord);

            Assert.AreEqual(0, yCoords.Length);
        }


        public static void YsAtX(double xCoord, double x, double y, double rotation, double y1CoordExpected, double y2CoordExpected)
        {
            CartesianCoordinate center = new CartesianCoordinate(x, y);
            ParabolicCurve curve = new ParabolicCurve(a, center, rotation, Tolerance);

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
            ParabolicCurve curveCloned = curve.Clone() as ParabolicCurve;

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
            ParabolicCurve curveCloned = curve.CloneCurve();

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
