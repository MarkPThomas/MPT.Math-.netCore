﻿using MPT.Math.Coordinates;
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

        [SetUp]
        public static void SetUp()
        {
            CartesianCoordinate center = new CartesianCoordinate(4, 5);
            double a = 5;
            double b = 3;
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

        #region Methods: Public
        [Test]
        public static void ToString_Returns_String()
        {
            string toString = curve.ToString();
            Assert.AreEqual("EllipticalCurve - Center: {X: 4, Y: 5}, Rotation: 0 rad, a: 5, b: 3, I: {X: 9, Y: 5}, J: {X: 9, Y: 5}", toString);
        }
        #endregion

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
