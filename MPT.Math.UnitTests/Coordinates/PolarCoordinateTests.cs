using System;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Math.UnitTests.Coordinates
{
    [TestFixture]
    public static class PolarCoordinateTests
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
        public static void PolarCoordinate_InitializationWithDefaultTolerance(double radius, double azimuth)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, azimuth);

            Assert.AreEqual(radius, coordinate.Radius);
            Assert.AreEqual(azimuth, coordinate.Azimuth.Radians, Tolerance);
            Assert.AreEqual(Numbers.ZeroTolerance, coordinate.Tolerance);
        }

        [Test]
        public static void PolarCoordinate_Initialization()
        {
            double radius = 5.3;
            double azimuth = -2;
            double tolerance = 0.0002;
            PolarCoordinate coordinate = new PolarCoordinate(radius, azimuth, tolerance);

            Assert.AreEqual(radius, coordinate.Radius);
            Assert.AreEqual(azimuth, coordinate.Azimuth.Radians, Tolerance);
            Assert.AreEqual(tolerance, coordinate.Tolerance);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            double radius = 5.3;
            double azimuth = -2;
            double tolerance = 0.0002;
            PolarCoordinate coordinate = new PolarCoordinate(radius, azimuth, tolerance);

            Assert.AreEqual("MPT.Math.Coordinates.PolarCoordinate - radius:5.3, azimuth:-2", coordinate.ToString());
        }
        #endregion

        #region Methods: Conversion       
        [Test]
        public static void ToCartesian_Converts_Polar_Coordinate_to_Cartesian_Coordinate()
        {
            PolarCoordinate polar = new PolarCoordinate(2, Angle.CreateFromDegree(60), Tolerance);
            CartesianCoordinate cartesian = polar.ToCartesian();
            CartesianCoordinate cartesianExpected = new CartesianCoordinate(1, 1.73205081, Tolerance);

            Assert.AreEqual(cartesianExpected, cartesian);
        }
        #endregion

        #region Methods: Radius Add/Subtract/Multiply/Divide
        [TestCase(0, 0, 0)]
        [TestCase(0, 2, -2)]
        [TestCase(2, 0, 2)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, -1)]
        [TestCase(2, -2, 4)]
        [TestCase(-3, 2, -5)]
        [TestCase(-3.2, -2.1, -1.1)]
        public static void SubtractFromRadius_Returns_Difference_Between_Radii(double radius1, double radius2, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius1, 2);

            PolarCoordinate coordinateCombined = coordinate.SubtractFromRadius(radius2);
            Assert.AreEqual(expectedResult, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(2, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(0, 2, 2)]
        [TestCase(2, 0, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(2, -2, 0)]
        [TestCase(2, -3, -1)]
        [TestCase(-3, 2, -1)]
        [TestCase(-3.2, -2.1, -5.3)]
        public static void AddtoRadius_Returns_Combined_Radii(double radius1, double radius2, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius1, 2);

            PolarCoordinate coordinateCombined = coordinate.AddToRadius(radius2);
            Assert.AreEqual(expectedResult, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(2, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(2, 0, 0)]
        [TestCase(2, 2, 4)]
        [TestCase(2, -2, -4)]
        [TestCase(2, 3.3, 6.6)]
        [TestCase(-3, 2, -6)]
        [TestCase(-3.2, -2.1, 6.72)]
        public static void MultiplyRadiusBy_Multiplies_Radius_by_a_Scaling_Factor(double radius, double factor, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, 2);

            PolarCoordinate coordinateCombined = coordinate.MultiplyRadiusBy(factor);
            Assert.AreEqual(expectedResult, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(2, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 2, 0)]
        [TestCase(2, 2, 1)]
        [TestCase(2, -2, -1)]
        [TestCase(2, 3.3, 0.606061)]
        [TestCase(-3, 2, -1.5)]
        [TestCase(-3.2, -2.1, 1.52381)]
        public static void DivideRadiusBy_Divides_Radius_by_a_Scaling_Factor(double radius, double factor, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, 2);

            PolarCoordinate coordinateCombined = coordinate.DivideRadiusBy(factor);
            Assert.AreEqual(expectedResult, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(2, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [Test]
        public static void DivideRadiusBy_Throws_Exception_when_Dividing_by_Zero()
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, -3);
            Assert.Throws<DivideByZeroException>(() => { PolarCoordinate coordinateNew = coordinate.DivideRadiusBy(0); });
        }
        #endregion

        #region Methods: Azimuth Angle Add/Subtract/Multiply/Divide
        [TestCase(0, 0, 0)]
        [TestCase(Numbers.Pi, Numbers.PiOver4, (3d / 4) * Numbers.Pi)]
        [TestCase(Numbers.Pi, -Numbers.PiOver4, -(3d / 4) * Numbers.Pi)] // Not 5/4 due to wrap-around with +/- pi limits
        [TestCase(-Numbers.Pi, Numbers.PiOver4, (3d / 4) * Numbers.Pi)] // Not -5/4 due to wrap-around with +/- pi limits
        [TestCase(-Numbers.Pi, -Numbers.PiOver4, -(3d / 4) * Numbers.Pi)]
        public static void SubtractAngleAzimuthRadians_Returns_Difference_Between_Angles(double angleRadians1, double angleRadians2, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, angleRadians1);

            PolarCoordinate coordinateCombined = coordinate.SubtractAngleAzimuthRadians(angleRadians2);
            Assert.AreEqual(2, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(expectedResult, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(Numbers.Pi, Numbers.PiOver4, -(3d / 4) * Numbers.Pi)] // Not 5/4 due to wrap-around with +/- pi limits
        [TestCase(Numbers.Pi, -Numbers.PiOver4, (3d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.Pi, Numbers.PiOver4, -(3d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.Pi, -Numbers.PiOver4, (3d / 4) * Numbers.Pi)] // Not -5/4 due to wrap-around with +/- pi limits
        public static void AddAngleAzimuthRadians_Returns_Combined_Angles(double angleRadians1, double angleRadians2, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, angleRadians1);

            PolarCoordinate coordinateCombined = coordinate.AddAngleAzimuthRadians(angleRadians2);
            Assert.AreEqual(2, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(expectedResult, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(45, 4, 41)]
        [TestCase(45, 2.2, 42.8)]
        [TestCase(45, -2.2, 47.2)]
        [TestCase(-45, 4, -49)]
        [TestCase(-45, 2.2, -47.2)]
        [TestCase(-45, -2.2, -42.8)]
        public static void SubtractAngleAzimuthDegrees_Returns_Difference_Between_Angles(double angleDegrees1, double angleDegrees2, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, Angle.DegreesToRadians(angleDegrees1));

            PolarCoordinate coordinateCombined = coordinate.SubtractAngleAzimuthDegrees(angleDegrees2);
            Assert.AreEqual(2, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(expectedResult, coordinateCombined.Azimuth.Degrees, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(45, 4, 49)]
        [TestCase(45, 2.2, 47.2)]
        [TestCase(45, -2.2, 42.8)]
        [TestCase(-45, 4, -41)]
        [TestCase(-45, 2.2, -42.8)]
        [TestCase(-45, -2.2, -47.2)]
        public static void AddAngleAzimuthDegrees_Returns_Combined_Angles(double angleDegrees1, double angleDegrees2, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, Angle.DegreesToRadians(angleDegrees1));

            PolarCoordinate coordinateCombined = coordinate.AddAngleAzimuthDegrees(angleDegrees2);
            Assert.AreEqual(2, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(expectedResult, coordinateCombined.Azimuth.Degrees, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(Numbers.PiOver4, 0, 0)]
        [TestCase(Numbers.PiOver4, 2, Numbers.PiOver2)]
        [TestCase(Numbers.PiOver4, -2, -Numbers.PiOver2)]
        [TestCase(Numbers.PiOver4, 3.3, 3.3 * Numbers.PiOver4)]
        [TestCase(Numbers.PiOver4, -3.3, -3.3 * Numbers.PiOver4)]
        public static void MultiplyAngleAzimuthBy_Multiplies_Angle_by_a_Scaling_Factor(double angleRadians, double factor, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, angleRadians);

            PolarCoordinate coordinateCombined = coordinate.MultiplyAngleAzimuthBy(factor);
            Assert.AreEqual(2, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(expectedResult, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [TestCase(Numbers.PiOver4, Numbers.PiOver4, 1)]
        [TestCase(Numbers.PiOver2, 2, Numbers.PiOver4)]
        [TestCase(Numbers.PiOver2, -2, -Numbers.PiOver4)]
        [TestCase(Numbers.PiOver2, 2.2, 0.71399833)] // Not 2.2*Pi/2 due to wrap-around with +/- pi limits
        [TestCase(Numbers.PiOver2, -2.2, -0.71399833)] // Not -2.2*Pi/2 due to wrap-around with +/- pi limits
        public static void DivideAngleAzimuthBy_Divides_Angle_by_a_Scaling_Factor(double angleRadians, double factor, double expectedResult)
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, angleRadians);

            PolarCoordinate coordinateCombined = coordinate.DivideAngleAzimuthBy(factor);
            Assert.AreEqual(2, coordinateCombined.Radius, Tolerance);
            Assert.AreEqual(expectedResult, coordinateCombined.Azimuth.Radians, Tolerance);
        }

        [Test]
        public static void DivideAngleAzimuthBy_Throws_Exception_when_Dividing_by_Zero()
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, -3);
            Assert.Throws<DivideByZeroException>(() => { PolarCoordinate coordinateNew = coordinate.DivideAngleAzimuthBy(0); });
        }
        #endregion

        #region Operators: Equals & IEquatable
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_Coordinates()
        {
            double radius = 5.3;
            double azimuth = -2;
            double tolerance = 0.0002;
            PolarCoordinate coordinate1 = new PolarCoordinate(radius, azimuth, tolerance);
            PolarCoordinate coordinate2 = new PolarCoordinate(radius, azimuth, tolerance);

            Assert.IsTrue(coordinate1.Equals(coordinate2));
            Assert.IsTrue(coordinate1.Equals((object)coordinate2));
            Assert.IsTrue(coordinate1 == coordinate2);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_Coordinates()
        {
            double radius = 5.3;
            double azimuth = -2;
            double tolerance = 0.0002;
            PolarCoordinate coordinate = new PolarCoordinate(radius, azimuth, tolerance);
            PolarCoordinate coordinateDiffX = new PolarCoordinate(5.2, azimuth, tolerance);
            Assert.IsFalse(coordinate == coordinateDiffX);

            PolarCoordinate coordinateDiffY = new PolarCoordinate(radius, 0, tolerance);
            Assert.IsFalse(coordinate == coordinateDiffY);

            PolarCoordinate coordinateDiffT = new PolarCoordinate(radius, azimuth, 0.001);
            Assert.IsTrue(coordinate == coordinateDiffT);

            object obj = new object();
            Assert.IsFalse(coordinate.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_Coordinates()
        {
            double radius = 5.3;
            double azimuth = -2;
            double tolerance = 0.0002;
            PolarCoordinate coordinate = new PolarCoordinate(radius, azimuth, tolerance);
            PolarCoordinate coordinateDiffX = new PolarCoordinate(5.2, azimuth, tolerance);
            Assert.IsTrue(coordinate != coordinateDiffX);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_Coordinates()
        {
            double radius = 5.3;
            double azimuth = -2;
            double tolerance = 0.0002;
            PolarCoordinate coordinate1 = new PolarCoordinate(radius, azimuth, tolerance);
            PolarCoordinate coordinate2 = new PolarCoordinate(radius, azimuth, tolerance);

            Assert.AreEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_Coordinates()
        {
            double radius = 5.3;
            double azimuth = -2;
            double tolerance = 0.0002;
            PolarCoordinate coordinate1 = new PolarCoordinate(radius, azimuth, tolerance);

            PolarCoordinate coordinate2 = new PolarCoordinate(2 * radius, azimuth, tolerance);
            Assert.AreNotEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());

            coordinate2 = new PolarCoordinate(radius, 2 * azimuth, tolerance);
            Assert.AreNotEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());

            coordinate2 = new PolarCoordinate(radius, azimuth, 2 * tolerance);
            Assert.AreEqual(coordinate1.GetHashCode(), coordinate2.GetHashCode());
        }

        [TestCase(1, Numbers.PiOver4, 1, Numbers.PiOver4, true)]
        [TestCase(1, Numbers.PiOver4, 3, Numbers.PiOver2, false)]
        public static void EqualsOverride_of_ICoordinate_for_PolarCoordinate(double radius1, double azimuth1, double radius2, double azimuth2, bool expectedResult)
        {
            double tolerance = 0.0002;
            PolarCoordinate polarCoordinate = new PolarCoordinate(radius1, azimuth1, tolerance);
            ICoordinate iCoordinate = new PolarCoordinate(radius2, azimuth2, tolerance);

            Assert.AreEqual(expectedResult, polarCoordinate.Equals(iCoordinate));
            Assert.AreEqual(expectedResult, polarCoordinate == iCoordinate);
            Assert.AreEqual(!expectedResult, polarCoordinate != iCoordinate);
        }

        [Test]
        public static void EqualsOverride_of_ICoordinate_for_Incompatible_Coordinate_Compared_as_Object()
        {
            double tolerance = 0.0002;
            PolarCoordinate polarCoordinate = new PolarCoordinate(2, Numbers.PiOver4, tolerance);
            ICoordinate iCoordinate = new TestCoordinate();

            Assert.IsFalse(polarCoordinate.Equals(iCoordinate));
            Assert.IsFalse(polarCoordinate == iCoordinate);
            Assert.IsTrue(polarCoordinate != iCoordinate);
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
        public static void EqualsOverride_of_ICoordinate_for_CartesianCoordinate_with_Differing_Coordinates(double x, double y, double radius, double azimuth, bool expectedResult)
        {
            double tolerance = 0.0002;
            ICoordinate cartesianCoordinate = new CartesianCoordinate(x, y, tolerance);
            PolarCoordinate polarCoordinate = new PolarCoordinate(radius, azimuth, tolerance);

            Assert.AreEqual(expectedResult, polarCoordinate.Equals(cartesianCoordinate));
        }
        #endregion

        #region Operators: Multiply/Divide
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 0, 2, 0, 0)]
        [TestCase(2, Numbers.PiOver4, 0, 0, 0)]
        [TestCase(2, Numbers.PiOver4, 1, 2, Numbers.PiOver4)]
        [TestCase(2, Numbers.PiOver4, -1, -2, -Numbers.PiOver4)]
        [TestCase(2, Numbers.PiOver4, 3.2, 6.4, 3.2 * Numbers.PiOver4)]
        [TestCase(2, Numbers.PiOver4, -1.2, -2.4, -1.2 * Numbers.PiOver4)]
        public static void MultiplyOverride_Multiplies_Coordinate_by_a_Scaling_Factor(
            double radius, double angleRadians, double factor, double scaledRadius, double scaledAngleRadians)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, angleRadians);
            PolarCoordinate coordinateNew = coordinate * factor;
            Assert.AreEqual(scaledRadius, coordinateNew.Radius, Tolerance);
            Assert.AreEqual(scaledAngleRadians, coordinateNew.Azimuth.Radians, Tolerance);
        }

        [TestCase(2, Numbers.PiOver4, 1, 2, Numbers.PiOver4)]
        [TestCase(2, Numbers.PiOver4, -1, -2, -Numbers.PiOver4)]
        [TestCase(2, Numbers.PiOver2, 2, 1, Numbers.PiOver4)]
        [TestCase(2, Numbers.PiOver2, -2, -1, -Numbers.PiOver4)]
        [TestCase(2, Numbers.PiOver4, 3.2, 0.625, Numbers.PiOver4 / 3.2)]
        [TestCase(2, Numbers.PiOver4, -1.2, -1.666667, Numbers.PiOver4 / -1.2)]
        [TestCase(2.2, Numbers.PiOver4, 5.4, 0.407407, Numbers.PiOver4 / 5.4)]
        public static void DivideOverride_Divides_Coordinate_by_a_Scaling_Factor(
            double radius, double angleRadians, double factor, double scaledRadius, double scaledAngleRadians)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, angleRadians);
            PolarCoordinate coordinateNew = coordinate / factor;
            Assert.AreEqual(scaledRadius, coordinateNew.Radius, Tolerance);
            Assert.AreEqual(scaledAngleRadians, coordinateNew.Azimuth.Radians, Tolerance);
        }

        [Test]
        public static void DivideOverride_Throws_Exception_when_Dividing_by_Zero()
        {
            PolarCoordinate coordinate = new PolarCoordinate(2, -3);
            Assert.Throws<DivideByZeroException>(() => { PolarCoordinate coordinateNew = coordinate / 0; });
        }
        #endregion

        #region Operators: Conversion
        [Test]
        public static void Implicit_Conversion_Between_Polar_And_Cartesian_Coordinates()
        {
            PolarCoordinate polar = new PolarCoordinate(2, Angle.CreateFromDegree(60), Tolerance);
            CartesianCoordinate cartesian = polar;
            CartesianCoordinate cartesianExpected = new CartesianCoordinate(1, 1.73205081, Tolerance);

            Assert.AreEqual(cartesianExpected, cartesian);
        }
        #endregion

        #region Methods: Static        
        [Test]
        public static void Origin_Returns_Polar_Coordinate_at_Origin()
        {
            PolarCoordinate coordinate = PolarCoordinate.Origin();
            PolarCoordinate origin = new PolarCoordinate(0, 0);

            Assert.AreEqual(origin, coordinate);
        }
        #endregion
    }
}
