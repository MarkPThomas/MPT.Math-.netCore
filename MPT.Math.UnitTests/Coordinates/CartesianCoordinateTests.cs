using MPT.Math.Coordinates;
using MPT.Math.Curves;
using NUnit.Framework;
using System;
using System.Transactions;

namespace MPT.Math.UnitTests.Coordinates
{
    [TestFixture]
    public static class CartesianCoordinateTests
    {
        public static double Tolerance = 0.00001;

        public class TestCoordinate : ICoordinate
        {
            public double Tolerance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public bool Equals(ICoordinate other)
            {
                throw new NotImplementedException();
            }
        }

        #region Initialization
        [TestCase(5.3, -2)]
        public static void CartesianCoordinate_InitializationWithDefaultTolerance(double x, double y)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);

            Assert.AreEqual(x, coordinate.X);
            Assert.AreEqual(y, coordinate.Y);
            Assert.AreEqual(Numbers.ZeroTolerance, coordinate.Tolerance);
        }

        [Test]
        public static void CartesianCoordinate_Initialization()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y, tolerance);

            Assert.AreEqual(x, coordinate.X);
            Assert.AreEqual(y, coordinate.Y);
            Assert.AreEqual(tolerance, coordinate.Tolerance);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(1, 2);

            Assert.AreEqual("MPT.Math.Coordinates.CartesianCoordinate - X: 1, Y: 2", coordinate.ToString());
        }

        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(1, 0, 1, 0, ExpectedResult = 1)]
        [TestCase(1, 1, 1, 1, ExpectedResult = 2)]
        [TestCase(1, 2, 3, 4, ExpectedResult = 11)]
        [TestCase(-1, -2, -3, -4, ExpectedResult = 11)]
        [TestCase(1, -2, 3, 4, ExpectedResult = -5)]
        public static double DotProduct(double x1, double y1, double x2, double y2)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(x2, y2);

            return coordinate1.DotProduct(coordinate2);
        }

        [TestCase(0, 0, 0, 0, ExpectedResult = 0)]
        [TestCase(1, 0, 1, 0, ExpectedResult = 0)]
        [TestCase(1, 1, 1, 1, ExpectedResult = 0)]
        [TestCase(1, 2, 3, 4, ExpectedResult = -2)]
        [TestCase(-1, -2, -3, -4, ExpectedResult = -2)]
        [TestCase(1, -2, 3, 4, ExpectedResult = 10)]
        public static double CrossProduct(double x1, double y1, double x2, double y2)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(x2, y2);

            return coordinate1.CrossProduct(coordinate2);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 1, -2, -1)]
        [TestCase(-1, -2, 3, 4)]
        public static void OffsetFrom_Returns_Offset_Coordinate(double xi, double yi, double xj, double yj)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(xj, yj);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(xi, yi);
            CartesianOffset offset = coordinate1.OffsetFrom(coordinate2);

            Assert.AreEqual(xi, offset.I.X);
            Assert.AreEqual(yi, offset.I.Y);
            Assert.AreEqual(xj, offset.J.X);
            Assert.AreEqual(yj, offset.J.Y);
        }

        [TestCase(2, 3, 0, 0, 2, 3)]  // No offset
        [TestCase(2, 3, 0, 45, 2, 3)]  // Rotation of coordinate about itself
        [TestCase(2, 3, 0, -45, 2, 3)]  // Negative Rotation of coordinate about itself
        [TestCase(2, 3, 1, 0, 3, 3)]  // Linear offset with default rotation
        [TestCase(2, 3, -1, 0, 1, 3)]  // Negative linear offset with default rotation
        [TestCase(2, 3, 1, 60, 2.5, 3.866025)]
        [TestCase(2, 3, -1, 60, 1.5, 2.133975)]
        [TestCase(2, 3, -1, -60, 1.5, 3.866025)]
        [TestCase(2, 3, 1, -60, 2.5, 2.133975)]
        public static void OffsetCoordinate_Returns_Coordinate_Offset_by_Rotation_and_Displacement(
            double x, double y, double distance, double rotationDegrees,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y, Tolerance);
            Angle rotation = Angle.CreateFromDegree(rotationDegrees);
            CartesianCoordinate offsetCoordinate = coordinate.OffsetCoordinate(distance, rotation);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY, Tolerance);

            Assert.AreEqual(expectedCoordinate, offsetCoordinate);
        }

        [TestCase(2, 3, 3.605551)]
        [TestCase(-2, 3, 3.605551)]
        [TestCase(-2, -3, 3.605551)]
        [TestCase(2, -3, 3.605551)]
        [TestCase(0, 0, 0)]
        public static void DistanceFromOrigin_Returns_Coordinate_Distance_from_Origin(double x, double y, double expectedDistance)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            double distance = coordinate.DistanceFromOrigin();
            Assert.AreEqual(expectedDistance, distance, Tolerance);
        }
        #endregion

        #region Methods: Static
        [Test]
        public static void Origin_Returns_Cartesian_Coordinate_at_Origin()
        {
            CartesianCoordinate coordinate = CartesianCoordinate.Origin();
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(0, 0);

            Assert.AreEqual(expectedCoordinate, coordinate);
        }

        [TestCase(2, 3, 0, 0, 2, 3)]  // No offset
        [TestCase(2, 3, 0, 45, 2, 3)]  // Rotation of coordinate about itself
        [TestCase(2, 3, 0, -45, 2, 3)]  // Negative Rotation of coordinate about itself
        [TestCase(2, 3, 1, 0, 3, 3)]  // Linear offset with default rotation
        [TestCase(2, 3, -1, 0, 1, 3)]  // Negative linear offset with default rotation
        [TestCase(2, 3, 1, 60, 2.5, 3.866025)]
        [TestCase(2, 3, -1, 60, 1.5, 2.133975)]
        [TestCase(2, 3, -1, -60, 1.5, 3.866025)]
        [TestCase(2, 3, 1, -60, 2.5, 2.133975)]
        public static void OffsetCoordinate_Returns_Coordinate_Offset_by_Rotation_and_Displacement_Static(
            double x, double y, double distance, double rotationDegrees,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y, Tolerance);
            Angle rotation = Angle.CreateFromDegree(rotationDegrees);
            CartesianCoordinate offsetCoordinate = CartesianCoordinate.OffsetCoordinate(coordinate, distance, rotation);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY, Tolerance);

            Assert.AreEqual(expectedCoordinate, offsetCoordinate);
        }
        #endregion

        #region Methods: Static/ITransform
        [TestCase(0, 0, Numbers.PiOver2, 0, 0)]
        [TestCase(3, 4, 0, 3, 4)]
        [TestCase(3, 4, 0.785398, -0.707107, 4.949747)]
        [TestCase(3, 4, 1.570796, -4, 3)]
        [TestCase(3, 4, 2.356194, -4.949747, -0.707107)]
        [TestCase(3, 4, 3.141593, -3, -4)]
        [TestCase(3, 4, 3.926991, 0.707107, -4.949747)]
        [TestCase(3, 4, 4.712389, 4, -3)]
        [TestCase(3, 4, 5.497787, 4.949747, 0.707107)]
        [TestCase(3, 4, 6.283185, 3, 4)]
        public static void Rotate(
            double x, double y, 
            double angleRadians, 
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);

            CartesianCoordinate cartesianCoordinate = CartesianCoordinate.Rotate(coordinate, angleRadians);

            Assert.AreEqual(expectedX, cartesianCoordinate.X, Tolerance);
            Assert.AreEqual(expectedY, cartesianCoordinate.Y, Tolerance);
        }

        [TestCase(3, 4, 2, 1, 0, 3, 4)]
        [TestCase(3, 4, 2, 1, 0.785398, 0.585786, 3.828427)]
        [TestCase(3, 4, 2, 1, 1.570796, -1, 2)]
        [TestCase(3, 4, 2, 1, 2.356194, -0.828427, -0.414214)]
        [TestCase(3, 4, 2, 1, 3.141593, 1, -2)]
        [TestCase(3, 4, 2, 1, 3.926991, 3.414214, -1.828427)]
        [TestCase(3, 4, 2, 1, 4.712389, 5, 0)]
        [TestCase(3, 4, 2, 1, 5.497787, 4.828427, 2.414214)]
        [TestCase(3, 4, 2, 1, 6.283185, 3, 4)]
        public static void RotateAboutPoint(
            double x, double y,
            double centerX, double centerY, double angleRadians,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate centerOfRotation = new CartesianCoordinate(centerX, centerY);

            CartesianCoordinate cartesianCoordinate = CartesianCoordinate.RotateAboutPoint(coordinate, centerOfRotation, angleRadians);

            Assert.AreEqual(expectedX, cartesianCoordinate.X, Tolerance);
            Assert.AreEqual(expectedY, cartesianCoordinate.Y, Tolerance);
        }

        [TestCase(3, 2, 2, 6, 4)]    // Default - larger, Quadrant I
        [TestCase(3, 2, 0.5, 1.5, 1)]    // Smaller
        [TestCase(3, 2, -2, -6, -4)]    // Negative
        [TestCase(3, 2, 0, 0, 0)]    // 0
        [TestCase(-3, 2, 2, -6, 4)]    // Default in Quadrant II
        [TestCase(-3, -2, 2, -6, -4)]    // Default in Quadrant III
        [TestCase(3, -2, 2, 6, -4)]    // Default in Quadrant IV
        public static void Scale(
            double x, double y,
            double scale,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY);

            CartesianCoordinate transformedCoordinate = CartesianCoordinate.Scale(coordinate, scale);

            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }

        [TestCase(3, 2, 2, 6, 4)]    // Default - larger, Quadrant I
        [TestCase(3, 2, 0.5, 1.5, 1)]    // Smaller
        [TestCase(3, 2, -2, -6, -4)]    // Negative
        [TestCase(3, 2, 0, 0, 0)]    // 0
        [TestCase(-3, 2, 2, -6, 4)]    // Default in Quadrant II
        [TestCase(-3, -2, 2, -6, -4)]    // Default in Quadrant III
        [TestCase(3, -2, 2, 6, -4)]    // Default in Quadrant IV
        public static void ScaleFromPoint(
            double x, double y,
            double scale,
            double expectedX, double expectedY)
        {
            CartesianCoordinate referencePoint = new CartesianCoordinate(2, -3);
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y) + referencePoint;
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY) + referencePoint;

            CartesianCoordinate transformedCoordinate = CartesianCoordinate.ScaleFromPoint(coordinate, referencePoint, scale);

            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }

        [TestCase(3, 2, 0.4, 0, 3.8, 2)]    // Shear +x
        [TestCase(3, 2, -0.4, 0, 2.2, 2)]    // Shear -x
        [TestCase(3, 2, 0, 0.4, 3, 3.2)]    // Shear +y
        [TestCase(3, 2, 0, -0.4, 3, 0.8)]    // Shear -y
        [TestCase(3, 2, 0.4, 0.6, 3.8, 3.8)]    // Shear +x, +y, Quadrant I
        [TestCase(-3, 2, 0.4, -1.5, -2.2, 6.5)]    // Shear +x, +y, Quadrant II
        [TestCase(-3, -2, -1, -1.5, -1, 2.5)]    // Shear +x, +y, Quadrant III
        [TestCase(3, -2, -1, 0.6, 5, -0.2)]    // Shear +x, +y, Quadrant IV
        public static void Skew_With_Lambdas(
            double x, double y,
            double lambdaX, double lambdaY,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY);

            CartesianCoordinate transformedCoordinate = CartesianCoordinate.Skew(coordinate, lambdaX, lambdaY);
            transformedCoordinate.Tolerance = Tolerance;
            expectedCoordinate.Tolerance = Tolerance;
            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }

        [TestCase(3, 2, 0.4, 0, 3.8, 2)]    // Shear +x
        [TestCase(3, 2, -0.4, 0, 2.2, 2)]    // Shear -x
        [TestCase(3, 2, 0, 0.4, 3, 3.2)]    // Shear +y
        [TestCase(3, 2, 0, -0.4, 3, 0.8)]    // Shear -y
        [TestCase(3, 2, 0.4, 0.6, 3.8, 3.8)]    // Shear +x, +y, Quadrant I
        [TestCase(-3, 2, 0.4, -1.5, -2.2, 6.5)]    // Shear +x, +y, Quadrant II
        [TestCase(-3, -2, -1, -1.5, -1, 2.5)]    // Shear +x, +y, Quadrant III
        [TestCase(3, -2, -1, 0.6, 5, -0.2)]    // Shear +x, +y, Quadrant IV
        public static void Skew_With_Lambda_Object(
            double x, double y,
            double lambdaX, double lambdaY,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY);
            CartesianOffset magnitude = new CartesianOffset(lambdaX, lambdaY);

            CartesianCoordinate transformedCoordinate = CartesianCoordinate.Skew(coordinate, magnitude);
            transformedCoordinate.Tolerance = Tolerance;
            expectedCoordinate.Tolerance = Tolerance;
            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }

        [TestCase(3, 2, 0, 0, 5, 5, 2, 0, 3.8, 2)]    // Shear +x
        [TestCase(3, 2, 0, 0, 5, 5, -2, 0, 2.2, 2)]    // Shear -x
        [TestCase(3, 2, 0, 0, 5, 5, 0, 2, 3, 3.2)]    // Shear +y
        [TestCase(3, 2, 0, 0, 5, 5, 0, -2, 3, 0.8)]    // Shear -y
        [TestCase(3, 2, 0, 0, 5, 5, 2, 3, 3.8, 3.8)]    // Shear +x, +y, Quadrant I
        [TestCase(-3, 2, 0, 0, -5, 5, 2, 3, -2.2, 3.8)]    // Shear +x, +y, Quadrant II
        [TestCase(-3, -2, 0, 0, -5, -5, 2, 3, -2.2, -0.2)]    // Shear +x, +y, Quadrant III
        [TestCase(3, -2, 0, 0, 5, -5, 2, 3, 3.8, -0.2)]    // Shear +x, +y, Quadrant IV
        [TestCase(3, 2, 2, 2, 5, 5, 2, 0, 4.33333333333333, 2)]    // Bounding box as skew box
        [TestCase(3, 2, 2, 2, 5, 5, 0, 2, 3, 4)]    // Bounding box as skew box
        public static void SkewWithinBox(
            double x, double y,
            double stationaryPointX, double stationaryPointY,
            double skewingPointX, double skewingPointY,
            double magnitudeX, double magnitudeY,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY);
            CartesianCoordinate stationaryReferencePoint = new CartesianCoordinate(stationaryPointX, stationaryPointY);
            CartesianCoordinate skewingReferencePoint = new CartesianCoordinate(skewingPointX, skewingPointY);
            CartesianOffset magnitude = new CartesianOffset(magnitudeX, magnitudeY);

            CartesianCoordinate transformedCoordinate = CartesianCoordinate.SkewWithinBox(coordinate, stationaryReferencePoint, skewingReferencePoint, magnitude);

            expectedCoordinate.Tolerance = Tolerance;
            transformedCoordinate.Tolerance = Tolerance;
            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }

        [TestCase(3, 2, 3, -2)]    // Mirror about x-axis to Quadrant IV
        [TestCase(-3, -2, -3, 2)]    // Mirror about x-axis to Quadrant IV, reversed line
        public static void MirrorAboutAxisX(
            double x, double y,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY);

            CartesianCoordinate transformedCoordinate = CartesianCoordinate.MirrorAboutAxisX(coordinate);

            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }

        [TestCase(3, 2, -3, 2)]    // Mirror about y-axis to Quadrant II
        [TestCase(-3, -2, 3, -2)]    // Mirror about y-axis to Quadrant II, reversed line
        public static void MirrorAboutAxisY(
            double x, double y,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY);

            CartesianCoordinate transformedCoordinate = CartesianCoordinate.MirrorAboutAxisY(coordinate);

            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }

        [TestCase(3, 2, 0, 1, 0, -1, -3, 2)]    // Mirror about y-axis to Quadrant II
        [TestCase(3, 2, 0, -1, 0, 1, -3, 2)]    // Mirror about y-axis to Quadrant II, reversed line
        [TestCase(3, 2, 1, 0, -1, 0, 3, -2)]    // Mirror about x-axis to Quadrant IV
        [TestCase(3, 2, -1, 0, 1, 0, 3, -2)]    // Mirror about x-axis to Quadrant IV, reversed line
        [TestCase(3, 2, 0, 0, 1, 1, 2, 3)]    // Mirror about 45 deg sloped line about shape center
        [TestCase(3, 2, 0, 0, -1, -1, 2, 3)]    // Mirror about 45 deg sloped line about shape center, reversed line
        [TestCase(3, 2, 0, 0, -1, 1, -2, -3)]    // Mirror about 45 deg sloped line to quadrant III
        [TestCase(3, 2, 0, 0, 1, -1, -2, -3)]    // Mirror about 45 deg sloped line to quadrant III, reversed line
        public static void MirrorAboutLine(
            double x, double y,
            double lineX1, double lineY1, double lineX2, double lineY2,
            double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate expectedCoordinate = new CartesianCoordinate(expectedX, expectedY);
            LinearCurve lineOfReflection = new LinearCurve(new CartesianCoordinate(lineX1, lineY1), new CartesianCoordinate(lineX2, lineY2));
            
            CartesianCoordinate transformedCoordinate = CartesianCoordinate.MirrorAboutLine(coordinate, lineOfReflection);

            Assert.AreEqual(expectedCoordinate, transformedCoordinate);
        }
        #endregion

        #region Methods: Conversion        
        [Test]
        public static void ToPolar_Converts_Cartesian_Coordinate_to_Polar_Coordinate()
        {
            CartesianCoordinate cartesian = new CartesianCoordinate(1, 1.73205081, Tolerance);
            PolarCoordinate polar = cartesian.ToPolar();
            PolarCoordinate polarExpected = new PolarCoordinate(2, Angle.CreateFromDegree(60), Tolerance);

            Assert.AreEqual(polarExpected, polar);
        }
        #endregion

        #region Operators: Equals & IEquatable
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x, y, tolerance);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(x, y, tolerance);

            Assert.IsTrue(coordinate1.Equals(coordinate2));
            Assert.IsTrue(coordinate1.Equals((object)coordinate2));
            Assert.IsTrue(coordinate1 == coordinate2);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y, tolerance);
            CartesianCoordinate coordinateDiffX = new CartesianCoordinate(5.2, y, tolerance);
            Assert.IsFalse(coordinate == coordinateDiffX);

            CartesianCoordinate coordinateDiffY = new CartesianCoordinate(x, 0, tolerance);
            Assert.IsFalse(coordinate == coordinateDiffY);

            CartesianCoordinate coordinateDiffT = new CartesianCoordinate(x, y, 0.001);
            Assert.IsTrue(coordinate == coordinateDiffT);

            object obj = new object();
            Assert.IsFalse(coordinate.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y, tolerance);
            CartesianCoordinate coordinateDiffX = new CartesianCoordinate(5.2, y, tolerance);
            Assert.IsTrue(coordinate != coordinateDiffX);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x, y, tolerance);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(x, y, tolerance);

            Assert.AreEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x, y, tolerance);

            CartesianCoordinate coordinate2 = new CartesianCoordinate(2*x, y, tolerance);
            Assert.AreNotEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());

            coordinate2 = new CartesianCoordinate(x, 2*y, tolerance);
            Assert.AreNotEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());

            coordinate2 = new CartesianCoordinate(x, y, 2 * tolerance);
            Assert.AreEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());
        }

        [TestCase(1, 2, 1, 2, true)]
        [TestCase(1, 2, 3, 4, false)]
        public static void EqualsOverride_of_ICoordinate_for_CartesiaCoordinate(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            double tolerance = 0.0002;
            CartesianCoordinate cartesianCoordinate = new CartesianCoordinate(x1, y1, tolerance);
            ICoordinate iCoordinate = new CartesianCoordinate(x2, y2, tolerance);

            Assert.AreEqual(expectedResult, cartesianCoordinate.Equals(iCoordinate));
            Assert.AreEqual(expectedResult, cartesianCoordinate == iCoordinate);
            Assert.AreEqual(!expectedResult, cartesianCoordinate != iCoordinate);
        }

        [Test]
        public static void EqualsOverride_of_ICoordinate_for_Incompatible_Coordinate_Compared_as_Object()
        {
            double tolerance = 0.0002;
            CartesianCoordinate cartesianCoordinate = new CartesianCoordinate(1, 2, tolerance);
            ICoordinate iCoordinate = new TestCoordinate();

            Assert.IsFalse(cartesianCoordinate.Equals(iCoordinate));
            Assert.IsFalse(cartesianCoordinate == iCoordinate);
            Assert.IsTrue(cartesianCoordinate != iCoordinate);
        }

        [TestCase(0, 0, 0, 0, true)]
        [TestCase(2, 0, 2, 0, true)]
        [TestCase(2, 2, 2.828427125, Numbers.PiOver4, true)]
        [TestCase(0, 2, 2, Numbers.PiOver2, true)]
        [TestCase(-2, 2, 2.828427125, (3d / 4) * Numbers.Pi, true)]
        [TestCase(-2, 0, 2, Numbers.Pi, true)]
        [TestCase(-2, -2, 2.828427125, -(3d / 4) * Numbers.Pi, true)]
        [TestCase(0, -2, 2, -Numbers.PiOver2, true)]
        [TestCase(2, -2, 2.828427125, -Numbers.PiOver4, true)]
        [TestCase(2, -2, 5, -Numbers.PiOver2, false)]
        public static void EqualsOverride_of_ICoordinate_for_PolarCoordinate(double x, double y, double radius, double azimuth, bool expectedResult)
        {
            double tolerance = 0.0002;
            CartesianCoordinate cartesianCoordinate = new CartesianCoordinate(x, y, tolerance);
            ICoordinate polarCoordinate = new PolarCoordinate(radius, azimuth, tolerance);

            Assert.AreEqual(expectedResult, cartesianCoordinate.Equals(polarCoordinate));
        }
        #endregion

        #region Operators: Combining
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4, -2, -2)]
        [TestCase(1, 2, -1, -2, 2, 4)]
        [TestCase(1, -2, -2, 4, 3, -6)]
        [TestCase(-3, 2, -2, -4,-1, 6)]
        [TestCase(-3, 2, -3, 2, 0, 0)]
        public static void SubtractOverride_Returns_Coordinate_Offset_Between_Coordinates(double x1, double y1, double x2, double y2, double xResult, double yResult)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(x2, y2);

            CartesianCoordinate coordinate3 = coordinate1 - coordinate2;
            Assert.AreEqual(xResult, coordinate3.X, Tolerance);
            Assert.AreEqual(yResult, coordinate3.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4, 4, 6)]
        [TestCase(1, 2, -1, -2, 0, 0)]
        [TestCase(1, -2,-2, 4, -1, 2)]
        [TestCase(-1, 2, -2, -4, -3, -2)]
        [TestCase(-1, 2, 2, -4, 1, -2)]
        public static void AddOverride_Returns_Combined_Coordinates(double x1, double y1, double x2, double y2, double xResult, double yResult)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(x2, y2);

            CartesianCoordinate coordinate3 = coordinate1 + coordinate2;
            Assert.AreEqual(xResult, coordinate3.X, Tolerance);
            Assert.AreEqual(yResult, coordinate3.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 0, 2, 0, 0)]
        [TestCase(2, 3, 0, 0, 0)]
        [TestCase(2, 3, 1, 2, 3)]
        [TestCase(2, 3, -1, -2, -3)]
        [TestCase(2, 3, 3.2, 6.4, 9.6)]
        [TestCase(2, 3, -1.2, -2.4, -3.6)]
        public static void MultiplyOverride_Multiplies_Coordinate_by_a_Scaling_Factor(double x, double y, double factor, double scaledX, double scaledY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate coordinateNew = coordinate * factor;
            Assert.AreEqual(scaledX, coordinateNew.X, Tolerance);
            Assert.AreEqual(scaledY, coordinateNew.Y, Tolerance);
        }

        [TestCase(2, 3, 1, 2, 3)]
        [TestCase(2, 3, -1, -2, -3)]
        [TestCase(2, 3, 2, 1, 1.5)]
        [TestCase(2, 3, -2, -1, -1.5)]
        [TestCase(2, 3, 3.2, 0.625, 0.9375)]
        [TestCase(2, 3, -1.2, -1.666667, -2.5)]
        [TestCase(2.2, 3.1, 5.4, 0.407407, 0.574074)]
        public static void DivideOverride_Divides_Coordinate_by_a_Scaling_Factor(
            double x, double y, double factor, double scaledX, double scaledY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate coordinateNew = coordinate / factor;
            Assert.AreEqual(scaledX, coordinateNew.X, Tolerance);
            Assert.AreEqual(scaledY, coordinateNew.Y, Tolerance);
        }

        [Test]
        public static void DivideOverride_Throws_Exception_when_Dividing_by_Zero()
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(2, -3);
            Assert.Throws<DivideByZeroException>(() => { CartesianCoordinate coordinateNew = coordinate / 0; });
        }
        #endregion

        #region Operators: Conversion
        [Test]
        public static void Implicit_Conversion_Between_Cartesian_And_Polar_Coordinates()
        {
            CartesianCoordinate cartesian = new CartesianCoordinate(1, 1.73205081, Tolerance);
            PolarCoordinate polar = cartesian;
            PolarCoordinate polarExpected = new PolarCoordinate(2, Angle.CreateFromDegree(60), Tolerance);

            Assert.AreEqual(polarExpected, polar);
        }
        #endregion
    }
}
