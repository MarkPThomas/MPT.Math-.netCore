using System;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Math.UnitTests.Coordinates
{
    [TestFixture]
    public static class PolarOffsetTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void PolarOffset_InitializationWithDefaultTolerance()
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(1, 2);
            PolarCoordinate coordinate2 = new PolarCoordinate(3, 4);
            PolarOffset offset = new PolarOffset(coordinate1, coordinate2);

            Assert.AreEqual(coordinate1.Radius, offset.I.Radius);
            Assert.AreEqual(coordinate1.Azimuth.Radians, offset.I.Azimuth.Radians);
            Assert.AreEqual(coordinate2.Radius, offset.J.Radius);
            Assert.AreEqual(coordinate2.Azimuth.Radians, offset.J.Azimuth.Radians);
            Assert.AreEqual(Numbers.ZeroTolerance, offset.Tolerance);
        }

        [Test]
        public static void PolarOffset_Initialization()
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(1, 2);
            PolarCoordinate coordinate2 = new PolarCoordinate(3, 4);
            double tolerance = 0.5;
            PolarOffset offset = new PolarOffset(coordinate1, coordinate2, tolerance);

            Assert.AreEqual(coordinate1.Radius, offset.I.Radius);
            Assert.AreEqual(coordinate1.Azimuth.Radians, offset.I.Azimuth.Radians);
            Assert.AreEqual(coordinate2.Radius, offset.J.Radius);
            Assert.AreEqual(coordinate2.Azimuth.Radians, offset.J.Azimuth.Radians);
            Assert.AreEqual(tolerance, offset.Tolerance);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            PolarOffset offset = new PolarOffset(new PolarCoordinate(0.1, 2), new PolarCoordinate(0.5, 3));

            Assert.AreEqual("MPT.Math.Coordinates.PolarOffset - I: (r:0.1, a:2), J: (r:0.5, a:3)", offset.ToString());
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, -1)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 2.2, 1.2)]
        [TestCase(-1.1, 2.2, 3.3)]
        [TestCase(-1.1, -2.2, -1.1)]
        [TestCase(1.1, -2.2, -3.3)]
        public static void Radius_Returns_Radius_Difference(double radiusI, double radiusJ, double radiusDifference)
        {
            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(radiusI, 0),
                new PolarCoordinate(radiusJ, 0));
            Assert.AreEqual(radiusDifference, offset.Radius(), Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, -1)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 2.2, 1.2)]
        [TestCase(-Numbers.PiOver4, Numbers.Pi, -(3d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.PiOver4, -Numbers.Pi, -(3d / 4) * Numbers.Pi)]
        [TestCase(Numbers.PiOver4, -Numbers.Pi, (3d / 4) * Numbers.Pi)]
        public static void Azimuth_Returns_Azimuth_Difference(double angleI, double angleJ, double angleDifference)
        {
            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(0, angleI),
                new PolarCoordinate(0, angleJ));
            Assert.AreEqual(angleDifference, offset.Azimuth().Radians, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 0, 0, 1, 0)]
        [TestCase(0, 0, 1, 0, 1)]
        [TestCase(1.1, 0.785398, 3.3, 3.141593, 4.151337)]
        [TestCase(1.1, -0.785398, -3.3, -3.141593, 2.639395)]
        [TestCase(-1.1, -0.785398, -3.3, 3.141593, 4.151337)]
        [TestCase(-1.1, -0.785398, 3.3, -3.141593, 2.639395)]
        public static void Length_Returns_Linear_Distance_Between_Offset_Points(double radiusI, double angleI, double radiusJ, double angleJ, double distance)
        {
            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(radiusI, angleI),
                new PolarCoordinate(radiusJ, angleJ));
            Assert.AreEqual(distance, offset.Length(), Tolerance);
        }
        #endregion

        #region Operators: Equals & IEquatable
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_Coordinates()
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(1, 2);
            PolarCoordinate coordinate2 = new PolarCoordinate(3, 4);
            PolarOffset offset1 = new PolarOffset(coordinate1, coordinate2);
            PolarOffset offset2 = new PolarOffset(coordinate1, coordinate2);

            Assert.IsTrue(offset1.Equals(offset2));
            Assert.IsTrue(offset1.Equals((object)offset2));
            Assert.IsTrue(offset1 == offset2);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_MaxMin_Coordinates()
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(1, 2);
            PolarCoordinate coordinate2 = new PolarCoordinate(3, 4);
            PolarCoordinate coordinate3 = new PolarCoordinate(3, 4);
            PolarOffset offset1 = new PolarOffset(coordinate1, coordinate2);
            PolarOffset offsetDiffI = new PolarOffset(coordinate3, coordinate2);
            Assert.IsFalse(offset1 == offsetDiffI);

            PolarCoordinate coordinate4 = new PolarCoordinate(3, 5);
            PolarOffset offsetDiffJ = new PolarOffset(coordinate1, coordinate4);
            Assert.IsFalse(offset1 == offsetDiffJ);

            PolarOffset offsetDiffT = new PolarOffset(coordinate1, coordinate2, 0.001);
            Assert.IsTrue(offset1 == offsetDiffT);

            object obj = new object();
            Assert.IsFalse(offset1.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_MaxMin_Coordinates()
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(1, 2);
            PolarCoordinate coordinate2 = new PolarCoordinate(3, 4);
            PolarCoordinate coordinate3 = new PolarCoordinate(3, 4);
            PolarOffset offset1 = new PolarOffset(coordinate1, coordinate2);
            PolarOffset offsetDiffI = new PolarOffset(coordinate3, coordinate2);

            Assert.IsTrue(offset1 != offsetDiffI);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_Coordinates()
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(1, 2);
            PolarCoordinate coordinate2 = new PolarCoordinate(3, 4);
            double tolerance = 0.0002;
            PolarOffset offset1 = new PolarOffset(coordinate1, coordinate2, tolerance);
            PolarOffset offset2 = new PolarOffset(coordinate1, coordinate2, tolerance);

            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_MaxMin_Coordinates()
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(1, 2);
            PolarCoordinate coordinate2 = new PolarCoordinate(3, 4);
            double tolerance = 0.0002;
            PolarOffset offset1 = new PolarOffset(coordinate1, coordinate2, tolerance);

            PolarOffset offset2 = new PolarOffset(2 * coordinate1, coordinate2, tolerance);
            Assert.AreNotEqual(offset1.GetHashCode(), offset2.GetHashCode());

            offset2 = new PolarOffset(coordinate1, 2 * coordinate2, tolerance);
            Assert.AreNotEqual(offset1.GetHashCode(), offset2.GetHashCode());

            offset2 = new PolarOffset(coordinate1, coordinate2, 2 * tolerance);
            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }
        #endregion

        #region Operators: Combining
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 7, Numbers.Pi, 1, Numbers.PiOver4)]
        [TestCase(-1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, -1, Numbers.PiOver4)]
        [TestCase(-1, -Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, -1, -Numbers.PiOver4)]
        [TestCase(1, -Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, 1, -Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, -3, -(3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, -3, Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, 1, -(3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 5, Numbers.Pi, -1, Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 4, Numbers.Pi, -2, Numbers.PiOver4)]
        [TestCase(1.1, Numbers.PiOver4, 5.5, Numbers.PiOver2, 4.4, Numbers.Pi, -2.2, Numbers.PiOver4)]
        public static void SubtractOverride_Subtracting_Coordinate_Returns_Coordinate_Offset_Between_Coordinates(
             double radius, double angle,
            double radiusI2, double angleI2, double radiusJ2, double angleJ2,
            double radiusResult, double angleResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, angle);

            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(radiusI2, angleI2),
                new PolarCoordinate(radiusJ2, angleJ2));

            PolarCoordinate offsetCoordinate = offset - coordinate;

            Assert.AreEqual(radiusResult, offsetCoordinate.Radius, Tolerance);
            Assert.AreEqual(angleResult, offsetCoordinate.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 7, Numbers.Pi, -1, -Numbers.PiOver4)]
        [TestCase(-1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, 1, -Numbers.PiOver4)]
        [TestCase(-1, -Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, 1, Numbers.PiOver4)]
        [TestCase(1, -Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, -1, Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, 3, (3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, 3, -Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, -1, (3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 5, Numbers.Pi, 1, -Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 4, Numbers.Pi, 2, -Numbers.PiOver4)]
        [TestCase(1.1, Numbers.PiOver4, 5.5, Numbers.PiOver2, 4.4, Numbers.Pi, 2.2, -Numbers.PiOver4)]
        public static void SubtractOverride_Subtracted_by_Coordinate_Returns_Coordinate_Subtracting_Offset(
            double radius, double angle,
            double radiusI2, double angleI2, double radiusJ2, double angleJ2,
            double radiusResult, double angleResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, angle);

            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(radiusI2, angleI2),
                new PolarCoordinate(radiusJ2, angleJ2));

            PolarCoordinate offsetCoordinate = coordinate - offset;

            Assert.AreEqual(radiusResult, offsetCoordinate.Radius, Tolerance);
            Assert.AreEqual(angleResult, offsetCoordinate.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 7, Numbers.Pi, 3, (3d / 4) * Numbers.Pi)]
        [TestCase(-1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, -3, (3d / 4) * Numbers.Pi)]
        [TestCase(-1, -Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, -3, -(3d / 4) * Numbers.Pi)]
        [TestCase(1, -Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, 3, -(3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, -1, -Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, -1, (3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, 3, -Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 5, Numbers.Pi, 1, (3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 4, Numbers.Pi, 0, (3d / 4) * Numbers.Pi)]
        [TestCase(1.1, Numbers.PiOver4, 5.5, Numbers.PiOver2, 4.4, Numbers.Pi, 0, (3d / 4) * Numbers.Pi)]
        public static void AddOverride_Added_to_CoordinateReturns_Combined_Coordinates(
           double radius, double angle,
            double radiusI2, double angleI2, double radiusJ2, double angleJ2,
            double radiusResult, double angleResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, angle);

            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(radiusI2, angleI2),
                new PolarCoordinate(radiusJ2, angleJ2));

            PolarCoordinate offsetCoordinate = offset + coordinate;

            Assert.AreEqual(radiusResult, offsetCoordinate.Radius, Tolerance);
            Assert.AreEqual(angleResult, offsetCoordinate.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 7, Numbers.Pi, 3, (3d / 4) * Numbers.Pi)]
        [TestCase(-1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, -3, (3d / 4) * Numbers.Pi)]
        [TestCase(-1, -Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, -3, -(3d / 4) * Numbers.Pi)]
        [TestCase(1, -Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, 3, -(3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, -5, -Numbers.PiOver2, -7, -Numbers.Pi, -1, -Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, -5, Numbers.PiOver2, -7, Numbers.Pi, -1, (3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, 5, -Numbers.PiOver2, 7, -Numbers.Pi, 3, -Numbers.PiOver4)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 5, Numbers.Pi, 1, (3d / 4) * Numbers.Pi)]
        [TestCase(1, Numbers.PiOver4, 5, Numbers.PiOver2, 4, Numbers.Pi, 0, (3d / 4) * Numbers.Pi)]
        [TestCase(1.1, Numbers.PiOver4, 5.5, Numbers.PiOver2, 4.4, Numbers.Pi, 0, (3d / 4) * Numbers.Pi)]
        public static void AddOverride_Added_by_Coordinate_Returns_Coordinate_Adding_Offset(
            double radius, double angle,
            double radiusI2, double angleI2, double radiusJ2, double angleJ2,
            double radiusResult, double angleResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, angle);

            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(radiusI2, angleI2),
                new PolarCoordinate(radiusJ2, angleJ2));

            PolarCoordinate offsetCoordinate = coordinate + offset;

            Assert.AreEqual(radiusResult, offsetCoordinate.Radius, Tolerance);
            Assert.AreEqual(angleResult, offsetCoordinate.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 2, 0, 0, 0, 0)]
        [TestCase(2, 3, 0, 0, 0, 0, 0)]
        [TestCase(2, 3, 1, 2, 3, 3, 2)]
        [TestCase(2, 3, -1, -2, -3, -3, -2)]
        [TestCase(2, 3, 3.2, 6.4, -2.9663703, 9.6, 0.116815)]
        [TestCase(2, 3, -1.2, -2.4, 2.683185, -3.600000, -2.4)]
        [TestCase(2.2, 3.1, 5.4, 11.88, -2.109555, 16.74, -0.686370)]
        public static void MultiplyOverride_Multiplies_Coordinate_by_a_Scaling_Factor(double a1, double a2, double factor,
            double scaledIRadius, double scaledIAngle, double scaledJRadius, double scaledJAngle)
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(a1, a2);
            PolarCoordinate coordinate2 = new PolarCoordinate(a2, a1);
            PolarOffset offset = new PolarOffset(coordinate1, coordinate2);

            PolarOffset offsetNew = offset * factor;
            Assert.AreEqual(scaledIRadius, offsetNew.I.Radius, Tolerance);
            Assert.AreEqual(scaledIAngle, offsetNew.I.Azimuth.Radians, Tolerance);
            Assert.AreEqual(scaledJRadius, offsetNew.J.Radius, Tolerance);
            Assert.AreEqual(scaledJAngle, offsetNew.J.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 2, 0, 0, 0, 0)]
        [TestCase(2, 3, 1, 2, 3, 3, 2)]
        [TestCase(2, 3, -1, -2, -3, -3, -2)]
        [TestCase(2, 3, 3.2, 0.625, 0.9375, 0.9375, 0.625)]
        [TestCase(2, 3, -1.2, -1.666667, -2.5, -2.5, -1.666667)]
        [TestCase(2.2, 3.1, 5.4, 0.407407, 0.574074, 0.574074, 0.407407)]
        public static void DivideOverride_Divides_Coordinate_by_a_Scaling_Factor(double a1, double a2, double factor,
            double scaledIRadius, double scaledIAngle, double scaledJRadius, double scaledJAngle)
        {
            PolarCoordinate coordinate1 = new PolarCoordinate(a1, a2);
            PolarCoordinate coordinate2 = new PolarCoordinate(a2, a1);
            PolarOffset offset = new PolarOffset(coordinate1, coordinate2);

            PolarOffset offsetNew = offset / factor;
            Assert.AreEqual(scaledIRadius, offsetNew.I.Radius, Tolerance);
            Assert.AreEqual(scaledIAngle, offsetNew.I.Azimuth.Radians, Tolerance);
            Assert.AreEqual(scaledJRadius, offsetNew.J.Radius, Tolerance);
            Assert.AreEqual(scaledJAngle, offsetNew.J.Azimuth.Radians, Tolerance);
        }

        [Test]
        public static void DivideOverride_Throws_Exception_when_Dividing_by_Zero()
        {
            PolarOffset offset = new PolarOffset(
                new PolarCoordinate(1, 2),
                new PolarCoordinate(-2, 3));
            Assert.Throws<DivideByZeroException>(() => { PolarOffset offsetNew = offset / 0; });
        }
        #endregion
    }
}
