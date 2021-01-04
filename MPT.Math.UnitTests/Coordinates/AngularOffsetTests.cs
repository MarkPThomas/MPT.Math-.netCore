using MPT.Math.Coordinates;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Coordinates
{
    [TestFixture]
    public static class AngularOffsetTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void AngularOffset_InitializationWithDefaultTolerance()
        {
            Angle angle1 = new Angle(1);
            Angle angle2 = new Angle(3);
            AngularOffset offset = new AngularOffset(angle1, angle2);

            Assert.AreEqual(angle1.Radians, offset.I.Radians);
            Assert.AreEqual(angle2.Radians, offset.J.Radians);
            Assert.AreEqual(Numbers.ZeroTolerance, offset.Tolerance);
        }

        [Test]
        public static void AngularOffset_Initialization_with_Coordinates()
        {
            Angle angle1 = new Angle(1);
            Angle angle2 = new Angle(3);
            double tolerance = 0.5;
            AngularOffset offset = new AngularOffset(angle1, angle2, tolerance);

            Assert.AreEqual(angle1.Radians, offset.I.Radians);
            Assert.AreEqual(angle2.Radians, offset.J.Radians);
            Assert.AreEqual(tolerance, offset.Tolerance);
        }

        [Test]
        public static void AngularOffset_Initialization_with_Offsets()
        {
            double tolerance = 0.5;
            AngularOffset offset = new AngularOffset(2, tolerance);

            Assert.AreEqual(0, offset.I.Radians);
            Assert.AreEqual(2, offset.J.Radians);
            Assert.AreEqual(tolerance, offset.Tolerance);
        }
        #endregion

        #region Methods: Static
        [TestCase(1, 2, 0, 1, -1, 2, 90)] // 90 deg rotated
        [TestCase(4, 2, 3, 2, 3, 4, 90)] // 90 deg aligned
        [TestCase(4, 2, 3, 2, 5, 4, 135)] // acute deg aligned
        [TestCase(5, 4, 3, 2, 3, 4, 135)] // acute deg rotated
        [TestCase(4, 2, 3, 2, 2, 3, 45)] // obtuse deg aligned
        [TestCase(4, 3, 3, 2, 1, 2, 45)] // obtuse deg rotated
        public static void CreateFromPoints_Creates_Angular_Offset_Formed_by_3_Points(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double expectedAngleDegrees)
        {
            CartesianCoordinate point1 = new CartesianCoordinate(x1, y1);
            CartesianCoordinate point2 = new CartesianCoordinate(x2, y2);
            CartesianCoordinate point3 = new CartesianCoordinate(x3, y3);
            AngularOffset offset = AngularOffset.CreateFromPoints(point1, point2, point3);

            Assert.AreEqual(expectedAngleDegrees, offset.ToAngle().Degrees, Tolerance);
        }
        #endregion

        #region Conversion
        [Test]
        public static void ToAngle_Returns_Angle_of_Offset()
        {
            Angle angle1 = new Angle(Numbers.PiOver2);
            Angle angle2 = new Angle(Numbers.PiOver4);
            AngularOffset offset = new AngularOffset(angle1, angle2);

            Angle angleFromOffset = offset.ToAngle();
            Assert.AreEqual(-Numbers.PiOver4, angleFromOffset.Radians, Tolerance);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            AngularOffset offset = new AngularOffset(new Angle(0.1), new Angle(0.5));

            Assert.AreEqual("MPT.Math.Coordinates.AngularOffset - Radians_i: 0.1 - Radians_j: 0.5", offset.ToString());
        }

        [Test]
        public static void Delta_Returns_Angle_of_Rotation_Difference()
        {
            Angle angle1 = new Angle(Numbers.PiOver2);
            Angle angle2 = new Angle(Numbers.PiOver4);
            AngularOffset offset = new AngularOffset(angle1, angle2);

            Angle angleFromOffset = offset.Delta();
            Assert.AreEqual(-Numbers.PiOver4, angleFromOffset.Radians, Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(1.570796, 1.414214)]
        [TestCase(-1.570796, -1.414214)]
        public static void LengthChord_Returns_Straight_Line_Distance_Between_Offset_Points_of_Unit_Radius(double angleRadians, double expectedResult)
        {
            Angle angle1 = new Angle(0);
            Angle angle2 = new Angle(angleRadians);
            AngularOffset offset = new AngularOffset(angle1, angle2);

            Assert.AreEqual(expectedResult, offset.LengthChord(),Tolerance);
        }

        [TestCase(0, 1, 0)]
        [TestCase(1.570796, 1, 1.414214)]
        [TestCase(-1.570796, 1, -1.414214)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(1.570796, 3, 4.242641)]
        [TestCase(-1.570796, 3, -4.242641)]
        [TestCase(0, -3, 0)]
        [TestCase(1.570796, -3, -4.242641)]
        [TestCase(-1.570796, -3, 4.242641)]
        public static void LengthChord_Returns_Straight_Line_Distance_Between_Offset_Points_of_Specified_Radius(double angleRadians, double radius, double expectedResult)
        {
            Angle angle1 = new Angle(0);
            Angle angle2 = new Angle(angleRadians);
            AngularOffset offset = new AngularOffset(angle1, angle2);

            Assert.AreEqual(expectedResult, offset.LengthChord(radius), Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(1.570796, 1.570796)]
        [TestCase(-1.570796, -1.570796)]
        public static void LengthArc_Returns_Curve_Line_Distance_Between_Offset_Points_of_Unit_Radius(double angleRadians, double expectedResult)
        {
            Angle angle1 = new Angle(0);
            Angle angle2 = new Angle(angleRadians);
            AngularOffset offset = new AngularOffset(angle1, angle2);

            Assert.AreEqual(expectedResult, offset.LengthArc(), Tolerance);
        }

        [TestCase(0, 1, 0)]
        [TestCase(1.570796, 1, 1.570796)]
        [TestCase(-1.570796, 1, -1.570796)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(1.570796, 3, 4.712389)]
        [TestCase(-1.570796, 3, -4.712389)]
        [TestCase(0, -3, 0)]
        [TestCase(1.570796, -3, -4.712389)]
        [TestCase(-1.570796, -3, 4.712389)]
        public static void LengthArc_Returns_Curve_Line_Distance_Between_Offset_Points_of_Specified_Radius(double angleRadians, double radius, double expectedResult)
        {
            Angle angle1 = new Angle(0);
            Angle angle2 = new Angle(angleRadians);
            AngularOffset offset = new AngularOffset(angle1, angle2);

            Assert.AreEqual(expectedResult, offset.LengthArc(radius), Tolerance);
        }
        #endregion

        #region Operators: Equals & IEquatable
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_Angles()
        {
            double angleRadians = Numbers.PiOver4;
            double tolerance = 0.0002;
            AngularOffset offset1 = new AngularOffset(new Angle(), new Angle(angleRadians), tolerance);
            AngularOffset offset2 = new AngularOffset(new Angle(), new Angle(angleRadians), tolerance);

            Assert.IsTrue(offset1.Equals(offset2));
            Assert.IsTrue(offset1.Equals((object)offset2));
            Assert.IsTrue(offset1 == offset2);
            Assert.IsTrue(offset1 == angleRadians);
            Assert.IsTrue(angleRadians == offset1);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_Angles()
        {
            double angleRadians = Numbers.PiOver4;
            double tolerance = 0.0002;
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadians), tolerance);
            AngularOffset offsetDiff = new AngularOffset(new Angle(), new Angle(Numbers.PiOver2), tolerance);
            Assert.IsFalse(offset == offsetDiff);

            AngularOffset offsetDiffT = new AngularOffset(new Angle(), new Angle(angleRadians), 0.001);
            Assert.IsTrue(offset == offsetDiffT);

            object obj = new object();
            Assert.IsFalse(offset.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_Angles()
        {
            double angleRadians = Numbers.PiOver4;
            double angleRadiansDiff = Numbers.PiOver2;
            double tolerance = 0.0002;
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadians), tolerance);
            AngularOffset angleDiff = new AngularOffset(new Angle(), new Angle(angleRadiansDiff), tolerance);
            Assert.IsTrue(offset != angleDiff);
            Assert.IsTrue(offset != angleRadiansDiff);
            Assert.IsTrue(angleRadiansDiff != offset);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_Angles()
        {
            double angleRadians = 5.3;
            double tolerance = 0.0002;
            AngularOffset offset1 = new AngularOffset(new Angle(), new Angle(angleRadians), tolerance);
            AngularOffset offset2 = new AngularOffset(new Angle(), new Angle(angleRadians), tolerance);

            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_Angles()
        {
            double angleRadians1 = 5.3;
            double angleRadians2 = -2;
            double tolerance = 0.0002;
            AngularOffset offset1 = new AngularOffset(new Angle(), new Angle(angleRadians1), tolerance);

            AngularOffset offset2 = new AngularOffset(new Angle(), new Angle(angleRadians2), tolerance);
            Assert.AreNotEqual(offset1.GetHashCode(), offset2.GetHashCode());

            offset2 = new AngularOffset(new Angle(), new Angle(angleRadians1), 2 * tolerance);
            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }
        #endregion

        #region Operators: Comparison & IComparable
        [Test]
        public static void CompareTo_Angle()
        {
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(Numbers.PiOver2));

            AngularOffset angleEqual = new AngularOffset(new Angle(), new Angle(Numbers.PiOver2));
            Assert.AreEqual(0, offset.CompareTo(angleEqual));

            AngularOffset angleGreater = new AngularOffset(new Angle(), new Angle(Numbers.Pi));
            Assert.AreEqual(-1, offset.CompareTo(angleGreater));

            AngularOffset angleLesser = new AngularOffset(new Angle(), new Angle(Numbers.PiOver4));
            Assert.AreEqual(1, offset.CompareTo(angleLesser));
        }

        [Test]
        public static void CompareTo_Double()
        {
            double angleEqual = Numbers.PiOver2;
            AngularOffset offset = new AngularOffset(new Angle(0), new Angle(angleEqual));

            Assert.AreEqual(0, offset.CompareTo(angleEqual));

            double angleGreater = Numbers.Pi;
            Assert.AreEqual(-1, offset.CompareTo(angleGreater));

            double angleLesser = Numbers.PiOver4;
            Assert.AreEqual(1, offset.CompareTo(angleLesser));
        }

        [Test]
        public static void GreaterThanOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            AngularOffset angleEqual = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            Assert.IsFalse(offset > angleEqual);
            Assert.IsFalse(offset > angleRadiansEqual);
            Assert.IsFalse(angleRadiansEqual > offset);

            double angleRadiansGreater = Numbers.Pi;
            AngularOffset angleGreater = new AngularOffset(new Angle(), new Angle(angleRadiansGreater));
            Assert.IsFalse(offset > angleGreater);
            Assert.IsFalse(offset > angleRadiansGreater);
            Assert.IsTrue(angleRadiansGreater > offset);

            double angleRadiansLesser = Numbers.PiOver4;
            AngularOffset angleLesser = new AngularOffset(new Angle(), new Angle(angleRadiansLesser));
            Assert.IsTrue(offset > angleLesser);
            Assert.IsTrue(offset > angleRadiansLesser);
            Assert.IsFalse(angleRadiansLesser > offset);
        }

        [Test]
        public static void LesserThanOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            AngularOffset angleEqual = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            Assert.IsFalse(offset < angleEqual);
            Assert.IsFalse(offset < angleRadiansEqual);
            Assert.IsFalse(angleRadiansEqual < offset);

            double angleRadiansGreater = Numbers.Pi;
            AngularOffset angleGreater = new AngularOffset(new Angle(), new Angle(angleRadiansGreater));
            Assert.IsTrue(offset < angleGreater);
            Assert.IsTrue(offset < angleRadiansGreater);
            Assert.IsFalse(angleRadiansGreater < offset);

            double angleRadiansLesser = Numbers.PiOver4;
            AngularOffset angleLesser = new AngularOffset(new Angle(), new Angle(angleRadiansLesser));
            Assert.IsFalse(offset < angleLesser);
            Assert.IsFalse(offset < angleRadiansLesser);
            Assert.IsTrue(angleRadiansLesser < offset);
        }

        [Test]
        public static void GreaterThanOrEqualToOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            AngularOffset angleEqual = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            Assert.IsTrue(offset >= angleEqual);
            Assert.IsTrue(offset >= angleRadiansEqual);
            Assert.IsTrue(angleRadiansEqual >= offset);

            double angleRadiansGreater = Numbers.Pi;
            AngularOffset angleGreater = new AngularOffset(new Angle(), new Angle(angleRadiansGreater));
            Assert.IsFalse(offset >= angleGreater);
            Assert.IsFalse(offset >= angleRadiansGreater);
            Assert.IsTrue(angleRadiansGreater >= offset);

            double angleRadiansLesser = Numbers.PiOver4;
            AngularOffset angleLesser = new AngularOffset(new Angle(), new Angle(angleRadiansLesser));
            Assert.IsTrue(offset >= angleLesser);
            Assert.IsTrue(offset >= angleRadiansLesser);
            Assert.IsFalse(angleRadiansLesser >= offset);
        }

        [Test]
        public static void LesserThanOrEqualToOverride()
        {
            double angleRadiansEqual = Numbers.PiOver2;
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            AngularOffset angleEqual = new AngularOffset(new Angle(), new Angle(angleRadiansEqual));
            Assert.IsTrue(offset <= angleEqual);
            Assert.IsTrue(offset <= angleRadiansEqual);
            Assert.IsTrue(angleRadiansEqual <= offset);

            double angleRadiansGreater = Numbers.Pi;
            AngularOffset angleGreater = new AngularOffset(new Angle(), new Angle(angleRadiansGreater));
            Assert.IsTrue(offset <= angleGreater);
            Assert.IsFalse(angleRadiansGreater <= offset);
            Assert.IsFalse(angleRadiansGreater <= offset);

            double angleRadiansLesser = Numbers.PiOver4;
            AngularOffset angleLesser = new AngularOffset(new Angle(), new Angle(angleRadiansLesser));
            Assert.IsFalse(offset <= angleLesser);
            Assert.IsFalse(offset <= angleRadiansLesser);
            Assert.IsTrue(angleRadiansLesser <= offset);
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
            AngularOffset offset1 = new AngularOffset(new Angle(), new Angle(angleRadians1));
            AngularOffset offset2 = new AngularOffset(new Angle(), new Angle(angleRadians2));

            AngularOffset offset3 = offset1 - offset2;
            Assert.AreEqual(angleResult, offset3.Delta().Radians, Tolerance);

            AngularOffset offset4 = offset1 - angleRadians2;
            Assert.AreEqual(angleResult, offset4.Delta().Radians, Tolerance);

            AngularOffset offset5 = angleRadians1 - offset2;
            Assert.AreEqual(angleResult, offset5.Delta().Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(Numbers.PiOver4, Numbers.PiOver2, (3d / 4) * Numbers.Pi)]
        [TestCase(-Numbers.PiOver4, Numbers.PiOver2, Numbers.PiOver4)]
        [TestCase(Numbers.PiOver4, -Numbers.PiOver2, -Numbers.PiOver4)]
        [TestCase(-Numbers.PiOver4, -Numbers.PiOver2, -(3d / 4) * Numbers.Pi)]
        public static void AddOverride_Returns_Combined_Coordinates(double angleRadians1, double angleRadians2, double angleResult)
        {
            AngularOffset offset1 = new AngularOffset(new Angle(), new Angle(angleRadians1));
            AngularOffset offset2 = new AngularOffset(new Angle(), new Angle(angleRadians2));

            AngularOffset offset3 = offset1 + offset2;
            Assert.AreEqual(angleResult, offset3.Delta().Radians, Tolerance);

            AngularOffset offset4 = offset1 + angleRadians2;
            Assert.AreEqual(angleResult, offset4.Delta().Radians, Tolerance);

            AngularOffset offset5 = angleRadians1 + offset2;
            Assert.AreEqual(angleResult, offset5.Delta().Radians, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(Numbers.PiOver4, 2, Numbers.PiOver2)]
        [TestCase(-Numbers.PiOver4, 2, -Numbers.PiOver2)]
        [TestCase(Numbers.PiOver4, -2, -Numbers.PiOver2)]
        [TestCase(-Numbers.PiOver4, -2, Numbers.PiOver2)]
        public static void MultiplyOverride_Multiplies_Coordinate_by_a_Scaling_Factor(double angleRadians, double factor, double scaledAngle)
        {
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadians));

            AngularOffset offsetNew1 = offset * factor;
            Assert.AreEqual(scaledAngle, offsetNew1.Delta().Radians, Tolerance);

            AngularOffset offsetNew2 = factor * offset;
            Assert.AreEqual(scaledAngle, offsetNew2.Delta().Radians, Tolerance);
        }

        [TestCase(0, Numbers.PiOver2, 0)]
        [TestCase(Numbers.PiOver4, Numbers.PiOver4, 1)]
        [TestCase(Numbers.PiOver4, Numbers.PiOver2, 0.5)]
        [TestCase(-Numbers.PiOver4, Numbers.PiOver2, -0.5)]
        [TestCase(Numbers.PiOver4, -Numbers.PiOver2, -0.5)]
        [TestCase(-Numbers.PiOver4, -Numbers.PiOver2, 0.5)]
        public static void DivideOverride_Divides_Coordinate_by_a_Scaling_Factor(double angleRadians, double factor, double scaledAngle)
        {
            AngularOffset offset = new AngularOffset(new Angle(), new Angle(angleRadians));
            AngularOffset offsetNew = offset / factor;
            Assert.AreEqual(scaledAngle, offsetNew.Delta().Radians, Tolerance);
        }

        [Test]
        public static void DivideOverride_Throws_Exception_when_Dividing_by_Zero()
        {
            AngularOffset offset = new AngularOffset(new Angle(1), new Angle(2));
            Assert.Throws<DivideByZeroException>(() => { AngularOffset offsetNew = offset / 0; });
        }
        #endregion

        #region Conversion
        [Test]
        public static void ExplicitOperator()
        {
            double angleRadians = Numbers.PiOver4;
            AngularOffset offset = new AngularOffset(new Angle(0), new Angle(angleRadians));

            Assert.AreEqual(angleRadians, (double)offset);
        }

        [Test]
        public static void ImplicitOperator()
        {
            double angleRadians = Numbers.PiOver4;
            AngularOffset offset = new AngularOffset(new Angle(0), new Angle(angleRadians));

            Assert.AreEqual(angleRadians, (double)offset);
        }
        #endregion
    }
}
