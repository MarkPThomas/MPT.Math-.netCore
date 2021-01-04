using MPT.Math.Coordinates;
using MPT.Math.Vectors;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Coordinates
{
    [TestFixture]
    public static class AngleTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [TestCase(0, 0)]
        [TestCase(0.785398, 0.7854)]
        [TestCase(1.570796, 1.5708)]
        [TestCase(2.356194, 2.35619)]
        [TestCase(3.14159265, 3.14159265)]
        [TestCase(3.926991, -2.35619)]
        [TestCase(4.712389, -1.5708)]
        [TestCase(5.497787, -0.7854)]
        [TestCase(6.283185, 0)]
        [TestCase(10.995574, -1.5708)]
        [TestCase(12.566371, 0)]
        [TestCase(31.415927, 0)]
        [TestCase(-0.785398, -0.7854)]
        [TestCase(-1.570796, -1.5708)]
        [TestCase(-2.356194, -2.35619)]
        [TestCase(-3.141593, 3.14159)]
        [TestCase(-3.926991, 2.35619)]
        [TestCase(-4.712389, 1.5708)]
        [TestCase(-5.497787, 0.7854)]
        [TestCase(-6.283185, 0)]
        [TestCase(-10.995574, 1.5708)]
        [TestCase(-12.566371, 0)]
        [TestCase(-31.415927, 0)]
        public static void Angle_InitializationWithDefaultTolerance(double radians, double expectedResult)
        {
            Angle angle = new Angle(radians);

            Assert.AreEqual(expectedResult, angle.Radians, Tolerance);
            Assert.AreEqual(Numbers.ZeroTolerance, angle.Tolerance);

            // Check modified properties
            double expectedClockwiseRadians = -expectedResult;
            Assert.AreEqual(expectedClockwiseRadians, angle.ClockwiseRadians, Tolerance);

            double expectedDegrees = Angle.RadiansToDegrees(expectedResult);
            Assert.AreEqual(expectedDegrees, angle.Degrees, 0.0005);  // Lowered tolerance due to rounding effects for large decimal numbers

            double expectedClockwiseDegrees = Angle.RadiansToDegrees(-expectedResult);
            Assert.AreEqual(expectedClockwiseDegrees, angle.ClockwiseDegrees, 0.0005);  // Lowered tolerance due to rounding effects for large decimal numbers

            Assert.AreEqual(radians, angle.RadiansRaw);
            Assert.AreEqual(radians * (180d / Numbers.Pi), angle.DegreesRaw);
        }

        [Test]
        public static void Angle_Initialization()
        {
            double radians = Numbers.PiOver4;
            double tolerance = 0.0002;
            Angle angle = new Angle(radians, tolerance);

            Assert.AreEqual(radians, angle.Radians);
            Assert.AreEqual(tolerance, angle.Tolerance);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            Angle angle = new Angle(0.5);

            Assert.AreEqual("MPT.Math.Coordinates.Angle - Radians: 0.5", angle.ToString());
        }

        [TestCase(0, 1, 0)]
        [TestCase(0.785398, 0.707107, 0.707107)]
        [TestCase(1.570796, 0, 1)]
        [TestCase(2.356194, -0.707107, 0.707107)]
        [TestCase(3.141593, -1, 0)]
        [TestCase(3.926991, -0.707107, -0.707107)]
        [TestCase(4.712389, 0, -1)]
        [TestCase(5.497787, 0.707107, -0.707107)]
        [TestCase(6.283185, 1, 0)]
        public static void GetDirectionVector_Returns_Direction_Vector_of_Angle(double angleRadians, double expectedXComponent, double expectedYComponent)
        {
            Angle angle = new Angle(angleRadians);
            Vector vector = angle.GetDirectionVector();

            Assert.AreEqual(expectedXComponent, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedYComponent, vector.Ycomponent, Tolerance);
        }

        [TestCase(1, 1, 0, 1, 1)]
        [TestCase(1, 1, 0.785398, 0, 1.414214)]
        [TestCase(1, 1, 1.570796, -1, 1)]
        [TestCase(1, 1, 2.356194, -1.414214, 0)]
        [TestCase(1, 1, 3.141593, -1, -1)]
        [TestCase(1, 1, 3.926991, 0, -1.414214)]
        [TestCase(1, 1, 4.712389, 1, -1)]
        [TestCase(1, 1, 5.497787, 1.414214, 0)]
        [TestCase(1, 1, 6.283185, 1, 1)]
        public static void RotateVector_Returns_Vector_Rotated_by_Angle(
            double xComponent, double yComponent, 
            double angleRadians, double expectedXComponent, double expectedYComponent)
        {
            Angle angle = new Angle(angleRadians);
            Vector vector = new Vector(xComponent, yComponent);
            Vector vectorRotated = angle.RotateVector(vector);

            Assert.AreEqual(expectedXComponent, vectorRotated.Xcomponent, Tolerance);
            Assert.AreEqual(expectedYComponent, vectorRotated.Ycomponent, Tolerance);
        }
        #endregion

        #region Methods: Static
        [Test]
        public static void CreateFromRadian_Creates_Angle_from_Specified_Angle_in_Radians()
        {
            double radians = Numbers.PiOver4;
            double tolerance = 0.0002;
            Angle angle = Angle.CreateFromRadian(radians, tolerance);

            Assert.AreEqual(radians, angle.Radians, Tolerance);
            Assert.AreEqual(tolerance, angle.Tolerance);
        }

        [Test]
        public static void CreateFromDegree_Creates_Angle_from_Specified_Angle_in_Degrees()
        {
            double degrees = 30;
            double tolerance = 0.0002;
            Angle angle = Angle.CreateFromDegree(degrees, tolerance);

            Assert.AreEqual(degrees, angle.Degrees, Tolerance);
            Assert.AreEqual(tolerance, angle.Tolerance);
        }

        [Test]
        public static void CreateFromVector_Creates_Angle_from_Specified_Vector()
        {
            Vector vector = new Vector(2, 3);
            double expectedAngle = vector.Angle();
            double tolerance = 0.0002;
            Angle angle = Angle.CreateFromVector(vector, tolerance);

            Assert.AreEqual(expectedAngle, angle.Radians, Tolerance);
            Assert.AreEqual(tolerance, angle.Tolerance);
        }

        [Test]
        public static void CreateFromPoint_Creates_Angle_from_Origin_to_Specified_Point()
        {
            CartesianCoordinate point = new CartesianCoordinate(2, 3);
            double expectedAngle = System.Math.Atan(3d / 2);
            Angle angle = Angle.CreateFromPoint(point);

            Assert.AreEqual(expectedAngle, angle.Radians, Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(0.7853982, 45)]
        [TestCase(1.5707963, 90)]
        [TestCase(2.3561945, 135)]
        [TestCase(3.1415927, 180)]
        [TestCase(3.9269908, 225)]
        [TestCase(4.712389, 270)]
        [TestCase(5.4977871, 315)]
        [TestCase(0.5235988, 30)]
        [TestCase(1.0471976, 60)]
        [TestCase(-0.7853982, -45)]
        [TestCase(-1.5707963, -90)]
        [TestCase(-2.3561945, -135)]
        [TestCase(-3.1415927, -180)]
        [TestCase(-3.9269908, -225)]
        [TestCase(-4.712389, -270)]
        [TestCase(-5.4977871, -315)]
        [TestCase(-0.5235988, -30)]
        [TestCase(-1.0471976, -60)]
        public static void RadiansToDegrees(double radians, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Angle.RadiansToDegrees(radians), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(45, 0.7854)]
        [TestCase(90, 1.5708)]
        [TestCase(135, 2.35619)]
        [TestCase(180, 3.14159)]
        [TestCase(225, 3.92699)]
        [TestCase(270, 4.71239)]
        [TestCase(315, 5.49779)]
        [TestCase(30, 0.5236)]
        [TestCase(60, 1.0472)]
        [TestCase(-45, -0.7854)]
        [TestCase(-90, -1.5708)]
        [TestCase(-135, -2.35619)]
        [TestCase(-180, -3.14159)]
        [TestCase(-225, -3.92699)]
        [TestCase(-270, -4.71239)]
        [TestCase(-315, -5.49779)]
        [TestCase(-30, -0.5236)]
        [TestCase(-60, -1.0472)]
        public static void DegreesToRadians(double degrees, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Angle.DegreesToRadians(degrees), Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)] // 0 deg
        [TestCase(0, 1, 90)] // 90 deg
        [TestCase(-1, 0, 180)] // 180 deg
        [TestCase(0, -1, 270)] // 270 deg
        [TestCase(1, 1, 45)] // quadrant 1
        [TestCase(-1, 1, 135)] // quadrant 2
        [TestCase(-1, -1, 225)] // quadrant 3
        [TestCase(1, -1, 315)] // quadrant 4
        [TestCase(2, 1, 26.56505)]
        [TestCase(1, 2, 63.43495)]
        public static void AsDegrees_from_CartesianCoordinates(double x, double y, double expectedResult)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            Assert.AreEqual(expectedResult, Angle.AsDegrees(coordinate), Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)] // 0 deg
        [TestCase(0, 1, 90)] // 90 deg
        [TestCase(-1, 0, 180)] // 180 deg
        [TestCase(0, -1, 270)] // 270 deg
        [TestCase(1, 1, 45)] // quadrant 1
        [TestCase(-1, 1, 135)] // quadrant 2
        [TestCase(-1, -1, 225)] // quadrant 3
        [TestCase(1, -1, 315)] // quadrant 4
        [TestCase(2, 1, 26.56505)]
        [TestCase(1, 2, 63.43495)]
        public static void AsDegrees(double x, double y, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Angle.AsDegrees(x, y), Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)] // 0 deg
        [TestCase(0, 1, Numbers.Pi / 2)] // 90 deg
        [TestCase(-1, 0, Numbers.Pi)] // 180 deg
        [TestCase(0, -1, 3 * Numbers.Pi / 2)] // 270 deg
        [TestCase(1, 1, Numbers.Pi / 4)] // quadrant 1
        [TestCase(-1, 1, 3 * Numbers.Pi / 4)] // quadrant 2
        [TestCase(-1, -1, 5 * Numbers.Pi / 4)] // quadrant 3
        [TestCase(1, -1, 7 * Numbers.Pi / 4)] // quadrant 4
        [TestCase(2, 1, 0.463648)]
        [TestCase(1, 2, 1.107149)]
        public static void AsRadians_from_CartesianCoordinates(double x, double y, double expectedResult)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            Assert.AreEqual(expectedResult, Angle.AsRadians(coordinate), Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)] // 0 deg
        [TestCase(0, 1, Numbers.Pi / 2)] // 90 deg
        [TestCase(-1, 0, Numbers.Pi)] // 180 deg
        [TestCase(0, -1, 3 * Numbers.Pi / 2)] // 270 deg
        [TestCase(1, 1, Numbers.Pi / 4)] // quadrant 1
        [TestCase(-1, 1, 3 * Numbers.Pi / 4)] // quadrant 2
        [TestCase(-1, -1, 5 * Numbers.Pi / 4)] // quadrant 3
        [TestCase(1, -1, 7 * Numbers.Pi / 4)] // quadrant 4
        [TestCase(2, 1, 0.463648)]
        [TestCase(1, 2, 1.107149)]
        public static void AsRadians(double x, double y, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Angle.AsRadians(x, y), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(0.785398, 0.7854)]
        [TestCase(1.570796, 1.5708)]
        [TestCase(2.356194, 2.35619)]
        [TestCase(3.14159265, 3.14159265)]
        [TestCase(3.926991, -2.35619)]
        [TestCase(4.712389, -1.5708)]
        [TestCase(5.497787, -0.7854)]
        [TestCase(6.283185, 0)]
        [TestCase(10.995574, -1.5708)]
        [TestCase(12.566371, 0)]
        [TestCase(31.415927, 0)]
        [TestCase(-0.785398, -0.7854)]
        [TestCase(-1.570796, -1.5708)]
        [TestCase(-2.356194, -2.35619)]
        [TestCase(-3.141593, 3.14159)]
        [TestCase(-3.926991, 2.35619)]
        [TestCase(-4.712389, 1.5708)]
        [TestCase(-5.497787, 0.7854)]
        [TestCase(-6.283185, 0)]
        [TestCase(-10.995574, 1.5708)]
        [TestCase(-12.566371, 0)]
        [TestCase(-31.415927, 0)]
        public static void WrapAngleWithinPositiveNegativePi(double radians, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Angle.WrapAngleWithinPositiveNegativePi(radians), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(0.7853982, 0.7854)]
        [TestCase(1.5707963, 1.5708)]
        [TestCase(2.3561945, 2.35619)]
        [TestCase(3.1415927, 3.14159)]
        [TestCase(3.9269908, 3.92699)]
        [TestCase(4.712389, 4.71239)]
        [TestCase(5.4977871, 5.49779)]
        [TestCase(6.2831853, 0)]
        [TestCase(10.9955743, 4.71239)]
        [TestCase(12.5663706, 0)]
        [TestCase(31.4159265, 0)]
        [TestCase(-0.7853982, 5.49779)]
        [TestCase(-1.5707963, 4.71239)]
        [TestCase(-2.3561945, 3.92699)]
        [TestCase(-3.1415927, 3.14159)]
        [TestCase(-3.9269908, 2.35619)]
        [TestCase(-4.712389, 1.5708)]
        [TestCase(-5.4977871, 0.7854)]
        [TestCase(-6.2831853, 0)]
        [TestCase(-10.9955743, 1.5708)]
        [TestCase(-12.5663706, 0)]
        [TestCase(-31.4159265, 0)]
        [TestCase(1.2345E-7, 0)]
        [TestCase(1.437821381955473E-07, 0)]
        [TestCase(-1.2345E-7, 6.2831851837300006)] // Not 0 due to no rounding
        [TestCase(-1.437821381955473E-07, 6.283185163397448)] // Not 0 due to no rounding
        [TestCase(-0.5, 5.783185)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity)]
        public static void WrapAngleWithinTwoPi(double radians, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Angle.WrapAngleWithinTwoPi(radians), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(0.7853982, 0.7854)]
        [TestCase(1.5707963, 1.5708)]
        [TestCase(2.3561945, 2.35619)]
        [TestCase(3.1415927, 3.14159)]
        [TestCase(3.9269908, 3.92699)]
        [TestCase(4.712389, 4.71239)]
        [TestCase(5.4977871, 5.49779)]
        [TestCase(6.2831853, 0)]
        [TestCase(10.9955743, 4.71239)]
        [TestCase(12.5663706, 0)]
        [TestCase(31.4159265, 0)]
        [TestCase(-0.7853982, 5.49779)]
        [TestCase(-1.5707963, 4.71239)]
        [TestCase(-2.3561945, 3.92699)]
        [TestCase(-3.1415927, 3.14159)]
        [TestCase(-3.9269908, 2.35619)]
        [TestCase(-4.712389, 1.5708)]
        [TestCase(-5.4977871, 0.7854)]
        [TestCase(-6.2831853, 0)]
        [TestCase(-10.9955743, 1.5708)]
        [TestCase(-12.5663706, 0)]
        [TestCase(-31.4159265, 0)]
        [TestCase(1.2345E-7, 0)]
        [TestCase(1.437821381955473E-07, 0)]
        [TestCase(-1.2345E-7, 0)]
        [TestCase(-1.437821381955473E-07, 0)]
        public static void WrapAngleWithinTwoPi_with_Tolerances(double radians, double expectedResult)
        {
            Assert.AreEqual(expectedResult, Angle.WrapAngleWithinTwoPi(radians, Tolerance), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(2, 1)]
        [TestCase(-1, -2)]
        public static void OffsetFrom_Returns_Offset_Coordinate(double radiansI, double radiansJ)
        {
            Angle angle1 = new Angle(radiansJ);
            Angle angle2 = new Angle(radiansI);
            AngularOffset offset = angle1.OffsetFrom(angle2);

            Assert.AreEqual(radiansI, offset.I.Radians, Tolerance);
            Assert.AreEqual(radiansJ, offset.J.Radians, Tolerance);
        }

        [Test]
        public static void Origin_Returns_Angle_Aligned_with_Origin_Axis()
        {
            Angle angle = Angle.Origin();
            Assert.AreEqual(0, angle.Degrees);
        }
        #endregion

        #region Operators: Equals & IEquatable
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_Coordinates()
        {
            double angleRadians = Numbers.PiOver4;
            double tolerance = 0.0002;
            Angle angle1 = new Angle(angleRadians, tolerance);
            Angle angle2 = new Angle(angleRadians, tolerance);

            Assert.IsTrue(angle1.Equals(angle2));
            Assert.IsTrue(angle1.Equals((object)angle2));
            Assert.IsTrue(angle1 == angle2);
            Assert.IsTrue(angle1 == angleRadians);
            Assert.IsTrue(angleRadians == angle1);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_Coordinates()
        {
            double angleRadians = Numbers.PiOver4;
            double tolerance = 0.0002;
            Angle angle = new Angle(angleRadians, tolerance);
            Angle angleDiff = new Angle(Numbers.PiOver2, tolerance);
            Assert.IsFalse(angle == angleDiff);

            Angle angleDiffT = new Angle(angleRadians, 0.001);
            Assert.IsTrue(angle == angleDiffT);

            object obj = new object();
            Assert.IsFalse(angle.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_Coordinates()
        {
            double angleRadians = Numbers.PiOver4;
            double angleRadiansDiff = Numbers.PiOver2;
            double tolerance = 0.0002;
            Angle angle = new Angle(angleRadians, tolerance);
            Angle angleDiff = new Angle(angleRadiansDiff, tolerance);
            Assert.IsTrue(angle != angleDiff);
            Assert.IsTrue(angle != angleRadiansDiff);
            Assert.IsTrue(angleRadiansDiff != angle);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_Coordinates()
        {
            double angleRadians = 5.3;
            double tolerance = 0.0002;
            Angle angle1 = new Angle(angleRadians, tolerance);
            Angle angle2 = new Angle(angleRadians, tolerance);

            Assert.AreEqual(angle1.GetHashCode(), angle2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_Coordinates()
        {
            double angleRadians1 = 5.3;
            double angleRadians2 = -2;
            double tolerance = 0.0002;
            Angle angle1 = new Angle(angleRadians1, tolerance);

            Angle angle2 = new Angle(angleRadians2, tolerance);
            Assert.AreNotEqual(angle1.GetHashCode(), angle2.GetHashCode());

            angle2 = new Angle(angleRadians1, 2 * tolerance);
            Assert.AreEqual(angle1.GetHashCode(), angle2.GetHashCode());
        }
        #endregion

        #region Operators: Combining
        [TestCase(0, 0, 0)]
        [TestCase(Numbers.PiOver4, Numbers.PiOver2, -(1d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.PiOver4, Numbers.PiOver2, -(3d / 4) * Numbers.Pi)]
        [TestCase(Numbers.PiOver4, -Numbers.PiOver2, (3d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.PiOver4, -Numbers.PiOver2, (1d / 4) * Numbers.Pi)]
        [TestCase(Numbers.PiOver2, Numbers.PiOver4, (1d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.PiOver2, Numbers.PiOver4, -(3d / 4) * Numbers.Pi)]
        [TestCase(Numbers.PiOver2, -Numbers.PiOver4, (3d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.PiOver2, -Numbers.PiOver4, -(1d / 4) * Numbers.Pi)]
        public static void SubtractOverride_Returns_Difference_of_Coordinates(double angleRadians1, double angleRadians2, double angleResult)
        {
            Angle angle1 = new Angle(angleRadians1);
            Angle angle2 = new Angle(angleRadians2);

            Angle angle3 = angle1 - angle2;
            Assert.AreEqual(angleResult, angle3.Radians, Tolerance);

            Angle angle4 = angle1 - angleRadians2;
            Assert.AreEqual(angleResult, angle4.Radians, Tolerance);

            Angle angle5 = angleRadians1 - angle2;
            Assert.AreEqual(angleResult, angle5.Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(Numbers.PiOver4, Numbers.PiOver2, (3d / 4)*Numbers.Pi)]
        [TestCase(-Numbers.PiOver4, Numbers.PiOver2, Numbers.PiOver4)]
        [TestCase(Numbers.PiOver4, -Numbers.PiOver2, -Numbers.PiOver4)]
        [TestCase(-Numbers.PiOver4, -Numbers.PiOver2, -(3d / 4) * Numbers.Pi)]
        public static void AddOverride_Returns_Combined_Coordinates(double angleRadians1, double angleRadians2, double angleResult)
        {
            Angle angle1 = new Angle(angleRadians1);
            Angle angle2 = new Angle(angleRadians2);

            Angle angle3 = angle1 + angle2;
            Assert.AreEqual(angleResult, angle3.Radians, Tolerance);

            Angle angle4 = angle1 + angleRadians2;
            Assert.AreEqual(angleResult, angle4.Radians, Tolerance);

            Angle angle5 = angleRadians1 + angle2;
            Assert.AreEqual(angleResult, angle5.Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(Numbers.PiOver4, 2, Numbers.PiOver2)]
        [TestCase(-Numbers.PiOver4, 2, -Numbers.PiOver2)]
        [TestCase(Numbers.PiOver4, -2, -Numbers.PiOver2)]
        [TestCase(-Numbers.PiOver4, -2, Numbers.PiOver2)]
        public static void MultiplyOverride_Multiplies_Coordinate_by_a_Scaling_Factor(double angleRadians, double factor, double scaledAngle)
        {
            Angle angle = new Angle(angleRadians);

            Angle angleNew1 = angle * factor;
            Assert.AreEqual(scaledAngle, angleNew1.Radians, Tolerance);

            Angle angleNew2 = factor * angle;
            Assert.AreEqual(scaledAngle, angleNew2.Radians, Tolerance);
        }

        [TestCase(0, Numbers.PiOver2, 0)]
        [TestCase(Numbers.PiOver4, Numbers.PiOver4, 1)]
        [TestCase(Numbers.PiOver4, Numbers.PiOver2, 0.5)]
        [TestCase(-Numbers.PiOver4, Numbers.PiOver2, -0.5)]
        [TestCase(Numbers.PiOver4, -Numbers.PiOver2, -0.5)]
        [TestCase(-Numbers.PiOver4, -Numbers.PiOver2, 0.5)]
        public static void DivideOverride_Divides_Coordinate_by_a_Scaling_Factor(double angleRadians, double factor, double scaledAngle)
        {
            Angle angle = new Angle(angleRadians);
            Angle angleNew = angle / factor;
            Assert.AreEqual(scaledAngle, angleNew.Radians, Tolerance);
        }

        [Test]
        public static void DivideOverride_Throws_Exception_when_Dividing_by_Zero()
        {
            Angle angle = new Angle(2);
            Assert.Throws<DivideByZeroException>(() => { Angle angleNew = angle / 0; });
        }
        #endregion

        #region Conversion
        [Test]
        public static void ExplicitOperator()
        {
            double angleRadians = Numbers.PiOver4;
            Angle angle = new Angle(angleRadians);

            Assert.AreEqual(angleRadians, (double)angle);
        }

        [Test]
        public static void ImplicitOperator()
        {
            double angleRadians = Numbers.PiOver4;
            Angle angle = angleRadians;

            Assert.AreEqual(angleRadians, (double)angle);
        }
        #endregion

        #region Operators: Comparison & IComparable
        [Test]
        public static void CompareTo_Angle()
        {
            Angle angle = new Angle(Numbers.PiOver2);

            Angle angleEqual = new Angle(Numbers.PiOver2);
            Assert.AreEqual(0, angle.CompareTo(angleEqual));

            Angle angleGreater = new Angle(Numbers.Pi);
            Assert.AreEqual(-1, angle.CompareTo(angleGreater));

            Angle angleLesser = new Angle(Numbers.PiOver4);
            Assert.AreEqual(1, angle.CompareTo(angleLesser));
        }

        [Test]
        public static void CompareTo_Double()
        {
            Angle angle = new Angle(Numbers.PiOver2);

            double angleEqual = Numbers.PiOver2;
            Assert.AreEqual(0, angle.CompareTo(angleEqual));

            double angleGreater = Numbers.Pi;
            Assert.AreEqual(-1, angle.CompareTo(angleGreater));

            double angleLesser = Numbers.PiOver4;
            Assert.AreEqual(1, angle.CompareTo(angleLesser));
        }

        [Test]
        public static void GreaterThanOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            Angle angle = new Angle(angleRadiansEqual);
            Angle angleEqual = new Angle(angleRadiansEqual);
            Assert.IsFalse(angle > angleEqual);
            Assert.IsFalse(angle > angleRadiansEqual);
            Assert.IsFalse(angleRadiansEqual > angle);

            double angleRadiansGreater = Numbers.Pi;
            Angle angleGreater = new Angle(angleRadiansGreater);
            Assert.IsFalse(angle > angleGreater);
            Assert.IsFalse(angle > angleRadiansGreater);
            Assert.IsTrue(angleRadiansGreater > angle);

            double angleRadiansLesser = Numbers.PiOver4;
            Angle angleLesser = new Angle(angleRadiansLesser);
            Assert.IsTrue(angle > angleLesser);
            Assert.IsTrue(angle > angleRadiansLesser);
            Assert.IsFalse(angleRadiansLesser > angle);
        }

        [Test]
        public static void LesserThanOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            Angle angle = new Angle(angleRadiansEqual);
            Angle angleEqual = new Angle(angleRadiansEqual);
            Assert.IsFalse(angle < angleEqual);
            Assert.IsFalse(angle < angleRadiansEqual);
            Assert.IsFalse(angleRadiansEqual < angle);

            double angleRadiansGreater = Numbers.Pi;
            Angle angleGreater = new Angle(angleRadiansGreater);
            Assert.IsTrue(angle < angleGreater);
            Assert.IsTrue(angle < angleRadiansGreater);
            Assert.IsFalse(angleRadiansGreater < angle);

            double angleRadiansLesser = Numbers.PiOver4;
            Angle angleLesser = new Angle(angleRadiansLesser);
            Assert.IsFalse(angle < angleLesser);
            Assert.IsFalse(angle < angleRadiansLesser);
            Assert.IsTrue(angleRadiansLesser < angle);
        }

        [Test]
        public static void GreaterThanOrEqualToOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            Angle angle = new Angle(angleRadiansEqual);
            Angle angleEqual = new Angle(angleRadiansEqual);
            Assert.IsTrue(angle >= angleEqual);
            Assert.IsTrue(angle >= angleRadiansEqual);
            Assert.IsTrue(angleRadiansEqual >= angle);

            double angleRadiansGreater = Numbers.Pi;
            Angle angleGreater = new Angle(angleRadiansGreater);
            Assert.IsFalse(angle >= angleGreater);
            Assert.IsFalse(angle >= angleRadiansGreater);
            Assert.IsTrue(angleRadiansGreater >= angle);

            double angleRadiansLesser = Numbers.PiOver4;
            Angle angleLesser = new Angle(angleRadiansLesser);
            Assert.IsTrue(angle >= angleLesser);
            Assert.IsTrue(angle >= angleRadiansLesser);
            Assert.IsFalse(angleRadiansLesser >= angle);
        }

        [Test]
        public static void LesserThanOrEqualToOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            Angle angle = new Angle(angleRadiansEqual);
            Angle angleEqual = new Angle(angleRadiansEqual);
            Assert.IsTrue(angle <= angleEqual);
            Assert.IsTrue(angle <= angleRadiansEqual);
            Assert.IsTrue(angleRadiansEqual <= angle);

            double angleRadiansGreater = Numbers.Pi;
            Angle angleGreater = new Angle(angleRadiansGreater);
            Assert.IsTrue(angle <= angleGreater);
            Assert.IsFalse(angleRadiansGreater <= angle);
            Assert.IsFalse(angleRadiansGreater <= angle);

            double angleRadiansLesser = Numbers.PiOver4;
            Angle angleLesser = new Angle(angleRadiansLesser);
            Assert.IsFalse(angle <= angleLesser);
            Assert.IsFalse(angle <= angleRadiansLesser);
            Assert.IsTrue(angleRadiansLesser <= angle);
        }
        #endregion
    }
}
