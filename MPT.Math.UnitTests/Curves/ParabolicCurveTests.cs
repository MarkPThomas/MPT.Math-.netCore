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

        [SetUp]
        public static void SetUp()
        {
            double distanceFromFocusToMajorVertex = 5;
            CartesianCoordinate localOrigin = new CartesianCoordinate(4, 5);
            Angle rotation = Angle.Origin();
            curve = new ParabolicCurve(distanceFromFocusToMajorVertex, localOrigin, rotation, Tolerance);
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

        #region Methods: Public
        [Test]
        public static void ToString_Returns_String()
        {
            string toString = curve.ToString();
            Assert.AreEqual("ParabolicCurve - Center: {X: 4, Y: 5}, Rotation: 0 rad, c: 5, I: {X: 4, Y: 5}, J: {X: 4, Y: 5}", toString);
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
