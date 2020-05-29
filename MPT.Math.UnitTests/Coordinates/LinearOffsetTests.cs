using System;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Math.UnitTests.Coordinates
{
    [TestFixture]
    public static class LinearOffsetTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void LinearOffset_InitializationWithDefaultTolerance()
        {
            double pointI = 1;
            double pointJ = 3;
            LinearOffset offset = new LinearOffset(pointI, pointJ);

            Assert.AreEqual(pointI, offset.I);
            Assert.AreEqual(pointJ, offset.J);
            Assert.AreEqual(Numbers.ZeroTolerance, offset.Tolerance);
        }

        [Test]
        public static void LinearOffset_Initialization()
        {
            double pointI = 1;
            double pointJ = 3;
            double tolerance = 0.5;
            LinearOffset offset = new LinearOffset(pointI, pointJ, tolerance);

            Assert.AreEqual(pointI, offset.I);
            Assert.AreEqual(pointJ, offset.J);
            Assert.AreEqual(tolerance, offset.Tolerance);
        }
        #endregion

        #region Conversion
        [Test]
        public static void ToValue_Returns_double_of_Offset()
        {
            double pointI = Numbers.PiOver2;
            double pointJ = Numbers.PiOver4;
            LinearOffset offset = new LinearOffset(pointI, pointJ);

            double distanceOfOffset = offset.ToValue();
            Assert.AreEqual(-Numbers.PiOver4, distanceOfOffset, Tolerance);
        }

        [Test]
        public static void ExplicitOperator()
        {
            double pointI = 5.3;
            double pointJ = 8;
            double distance = pointJ - pointI;
            LinearOffset offset = new LinearOffset(pointI, pointJ);

            Assert.AreEqual(distance, (double)offset);
        }

        [Test]
        public static void ImplicitOperator()
        {
            double pointI = 5.3;
            double pointJ = 8;
            double distance = pointJ - pointI;
            LinearOffset offset = new LinearOffset(pointI, pointJ);

            Assert.AreEqual(distance, (double)offset);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            LinearOffset offset = new LinearOffset(0.1, 0.5);

            Assert.AreEqual("MPT.Math.Coordinates.LinearOffset - I: 0.1, J: 0.5", offset.ToString());
        }

        [Test]
        public static void Delta_Returns_double_of_Rotation_Difference()
        {
            double pointI = Numbers.PiOver2;
            double pointJ = Numbers.PiOver4;
            LinearOffset offset = new LinearOffset(pointI, pointJ);

            double distanceOfOffset = offset.Delta();
            Assert.AreEqual(-Numbers.PiOver4, distanceOfOffset, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(2, 3, 1)]
        [TestCase(-5, -7, -2)]
        public static void Length_Returns_Straight_Line_Distance_Between_Offset_Points(double pointI, double pointJ, double expectedResult)
        {
            LinearOffset offset = new LinearOffset(pointI, pointJ);

            Assert.AreEqual(expectedResult, offset.Length(), Tolerance);
        }
        #endregion

        #region Operators: Equals & IEquatable
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_doubles()
        {
            double pointI = 2;
            double pointJ = Numbers.PiOver4;
            double distance = pointJ - pointI;
            double tolerance = 0.0002;
            LinearOffset offset1 = new LinearOffset(pointI, pointJ, tolerance);
            LinearOffset offset2 = new LinearOffset(pointI, pointJ, tolerance);

            Assert.IsTrue(offset1.Equals(offset2));
            Assert.IsTrue(offset1.Equals((object)offset2));
            Assert.IsTrue(offset1 == offset2);
            Assert.IsTrue(offset1 == distance);
            Assert.IsTrue(distance == offset1);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_doubles()
        {
            double pointI = Numbers.PiOver4;
            double pointJ = 6;
            double tolerance = 0.0002;
            LinearOffset offset = new LinearOffset(pointI, pointJ, tolerance);
            LinearOffset offsetDiff1 = new LinearOffset(pointI, Numbers.PiOver2, tolerance);
            Assert.IsFalse(offset == offsetDiff1);
            LinearOffset offsetDiff2 = new LinearOffset(Numbers.PiOver2, pointJ, tolerance);
            Assert.IsFalse(offset == offsetDiff2);

            LinearOffset offsetDiffT = new LinearOffset(pointI, pointJ, 0.001);
            Assert.IsTrue(offset == offsetDiffT);

            object obj = new object();
            Assert.IsFalse(offset.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_doubles()
        {
            double pointI = Numbers.PiOver4;
            double pointJ = Numbers.PiOver2;
            double tolerance = 0.0002;
            LinearOffset offset = new LinearOffset(pointI, pointJ, tolerance);
            double pointJDiff = Numbers.PiOver4;
            LinearOffset offsetDiff1 = new LinearOffset(pointI, pointJDiff, tolerance);
            Assert.IsTrue(offset != offsetDiff1);
            double pointIDiff = Numbers.PiOver2;
            LinearOffset offsetDiff2 = new LinearOffset(pointIDiff, pointJ, tolerance);
            Assert.IsTrue(offset != offsetDiff2);
            Assert.IsTrue(offset != 5);
            Assert.IsTrue(5 != offset);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_doubles()
        {
            double pointI = 3.2;
            double pointJ = 5.3;
            double tolerance = 0.0002;
            LinearOffset offset1 = new LinearOffset(pointI, pointJ, tolerance);
            LinearOffset offset2 = new LinearOffset(pointI, pointJ, tolerance);

            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_doubles()
        {
            double pointI = 5.3;
            double pointJ = -2;
            double tolerance = 0.0002;
            LinearOffset offset1 = new LinearOffset(pointI, pointJ, tolerance);

            double pointJDiff = Numbers.PiOver4;
            LinearOffset offset2 = new LinearOffset(pointI, pointJDiff, tolerance);
            Assert.AreNotEqual(offset1.GetHashCode(), offset2.GetHashCode());

            double pointIDiff = Numbers.PiOver2;
            LinearOffset offset3 = new LinearOffset(pointIDiff, pointJ, tolerance);
            Assert.AreNotEqual(offset1.GetHashCode(), offset3.GetHashCode());

            LinearOffset offset4 = new LinearOffset(pointI, pointJ, 2 * tolerance);
            Assert.AreEqual(offset1.GetHashCode(), offset4.GetHashCode());
        }
        #endregion

        #region Operators: Comparison & IComparable
        [Test]
        public static void CompareTo_double()
        {
            LinearOffset offset = new LinearOffset(2, Numbers.PiOver2);

            LinearOffset offsetEqual = new LinearOffset(2, Numbers.PiOver2);
            Assert.AreEqual(0, offset.CompareTo(offsetEqual));

            LinearOffset offsetGreater = new LinearOffset(1, Numbers.Pi);
            Assert.AreEqual(-1, offset.CompareTo(offsetGreater));

            LinearOffset offsetLesser = new LinearOffset(3, Numbers.PiOver4);
            Assert.AreEqual(1, offset.CompareTo(offsetLesser));
        }

        [Test]
        public static void CompareTo_Double()
        {
            double offsetEqual = Numbers.PiOver2;
            LinearOffset offset = new LinearOffset(0, offsetEqual);

            Assert.AreEqual(0, offset.CompareTo(offsetEqual));

            double offsetGreater = Numbers.Pi;
            Assert.AreEqual(-1, offset.CompareTo(offsetGreater));

            double offsetLesser = Numbers.PiOver4;
            Assert.AreEqual(1, offset.CompareTo(offsetLesser));
        }

        [Test]
        public static void GreaterThanOverride()
        {
            double pointI = 5.3;
            double pointJ = 8;
            double distanceEqual = pointJ - pointI;
            LinearOffset offset = new LinearOffset(pointI, pointJ);
            LinearOffset offsetEqual = new LinearOffset(pointI, pointJ);
            Assert.IsFalse(offset > offsetEqual);
            Assert.IsFalse(offset > distanceEqual);
            Assert.IsFalse(distanceEqual > offset);

            double pointJGreater = 10;
            double distanceGreater = pointJGreater - pointI;
            LinearOffset offsetGreater = new LinearOffset(pointI, pointJGreater);
            Assert.IsFalse(offset > offsetGreater);
            Assert.IsFalse(offset > distanceGreater);
            Assert.IsTrue(distanceGreater > offset);

            double pointJLesser = 5;
            double distanceLesser = pointJLesser - pointI;
            LinearOffset offsetLesser = new LinearOffset(pointI, pointJLesser);
            Assert.IsTrue(offset > offsetLesser);
            Assert.IsTrue(offset > distanceLesser);
            Assert.IsFalse(distanceLesser > offset);
        }

        [Test]
        public static void LesserThanOverride()
        {
            double pointI = 5.3;
            double pointJ = 8;
            double distanceEqual = pointJ - pointI;
            LinearOffset offset = new LinearOffset(pointI, pointJ);
            LinearOffset offsetEqual = new LinearOffset(pointI, pointJ);
            Assert.IsFalse(offset < offsetEqual);
            Assert.IsFalse(offset < distanceEqual);
            Assert.IsFalse(distanceEqual < offset);

            double pointJGreater = 10;
            double distanceGreater = pointJGreater - pointI;
            LinearOffset offsetGreater = new LinearOffset(pointI, pointJGreater);
            Assert.IsTrue(offset < offsetGreater);
            Assert.IsTrue(offset < distanceGreater);
            Assert.IsFalse(distanceGreater < offset);

            double pointJLesser = 5;
            double distanceLesser = pointJLesser - pointI;
            LinearOffset offsetLesser = new LinearOffset(pointI, pointJLesser);
            Assert.IsFalse(offset < offsetLesser);
            Assert.IsFalse(offset < distanceLesser);
            Assert.IsTrue(distanceLesser < offset);
        }

        [Test]
        public static void GreaterThanOrEqualToOverride()
        {
            double pointI = 5.3;
            double pointJ = 8;
            double distanceEqual = pointJ - pointI;
            LinearOffset offset = new LinearOffset(pointI, pointJ);
            LinearOffset offsetEqual = new LinearOffset(pointI, pointJ);
            Assert.IsTrue(offset >= offsetEqual);
            Assert.IsTrue(offset >= distanceEqual);
            Assert.IsTrue(distanceEqual >= offset);

            double pointJGreater = 10;
            double distanceGreater = pointJGreater - pointI;
            LinearOffset offsetGreater = new LinearOffset(pointI, pointJGreater);
            Assert.IsFalse(offset >= offsetGreater);
            Assert.IsFalse(offset >= distanceGreater);
            Assert.IsTrue(distanceGreater >= offset);

            double pointJLesser = 5;
            double distanceLesser = pointJLesser - pointI;
            LinearOffset offsetLesser = new LinearOffset(pointI, pointJLesser);
            Assert.IsTrue(offset >= offsetLesser);
            Assert.IsTrue(offset >= distanceLesser);
            Assert.IsFalse(distanceLesser >= offset);
        }

        [Test]
        public static void LesserThanOrEqualToOverride()
        {
            double pointI = 5.3;
            double pointJ = 8;
            double distanceEqual = pointJ - pointI;
            LinearOffset offset = new LinearOffset(pointI, pointJ);
            LinearOffset offsetEqual = new LinearOffset(pointI, pointJ);
            Assert.IsTrue(offset <= offsetEqual);
            Assert.IsTrue(offset <= distanceEqual);
            Assert.IsTrue(distanceEqual <= offset);

            double pointJGreater = 10;
            double distanceGreater = pointJGreater - pointI;
            LinearOffset offsetGreater = new LinearOffset(pointI, pointJGreater);
            Assert.IsTrue(offset <= offsetGreater);
            Assert.IsFalse(distanceGreater <= offset);
            Assert.IsFalse(distanceGreater <= offset);

            double pointJLesser = 5;
            double distanceLesser = pointJLesser - pointI;
            LinearOffset offsetLesser = new LinearOffset(pointI, pointJLesser);
            Assert.IsFalse(offset <= offsetLesser);
            Assert.IsFalse(offset <= distanceLesser);
            Assert.IsTrue(distanceLesser <= offset);
        }
        #endregion

        #region Operators: Combining
        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 4, 6, -2)]
        [TestCase(-2, 4, 6, -2)]
        [TestCase(2, -4, -6, 2)]
        [TestCase(-2, -4, -6, 2)]
        public static void SubtractOverride_Returns_Difference_of_Coordinates(double pointI1, double pointJ1, double pointJ2, double offsetResult)
        {
            LinearOffset offset1 = new LinearOffset(pointI1, pointJ1);
            LinearOffset offset2 = new LinearOffset(pointI1, pointJ2);

            LinearOffset offset3 = offset1 - offset2;
            Assert.AreEqual(offsetResult, offset3.Delta(), Tolerance);

            double distance2 = pointJ2 - pointI1;
            LinearOffset offset4 = offset1 - distance2;
            Assert.AreEqual(offsetResult, offset4.Delta(), Tolerance);

            double distance1 = pointJ1 - pointI1;
            LinearOffset offset5 = distance1 - offset2;
            Assert.AreEqual(offsetResult, offset5.Delta(), Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 4, 6, 6)]
        [TestCase(-2, 4, 6, 14)]
        [TestCase(2, -4, -6, -14)]
        [TestCase(-2, -4, -6, -6)]
        public static void AddOverride_Returns_Combined_Coordinates(double pointI1, double pointJ1, double pointJ2, double offsetResult)
        {
            LinearOffset offset1 = new LinearOffset(pointI1, pointJ1);
            LinearOffset offset2 = new LinearOffset(pointI1, pointJ2);

            LinearOffset offset3 = offset1 + offset2;
            Assert.AreEqual(offsetResult, offset3.Delta(), Tolerance);

            double distance2 = pointJ2 - pointI1;
            LinearOffset offset4 = offset1 + distance2;
            Assert.AreEqual(offsetResult, offset4.Delta(), Tolerance);

            double distance1 = pointJ1 - pointI1;
            LinearOffset offset5 = distance1 + offset2;
            Assert.AreEqual(offsetResult, offset5.Delta(), Tolerance);
        }

        [TestCase(0, 0, 1, 0)]
        [TestCase(2.2, 4.4, 1, 2.2)]
        [TestCase(2.2, 4.4, 0.5, 1.1)]
        [TestCase(-2.2, 4.4, -0.5, -3.3)]
        [TestCase(2.2, -4.4, -0.5, 3.3)]
        [TestCase(-2.2, -4.4, 0.5, -1.1)]
        public static void MultiplyOverride_Multiplies_Coordinate_by_a_Scaling_Factor(double pointI, double pointJ, double factor, double scaledOffset)
        {
            LinearOffset offset = new LinearOffset(pointI, pointJ);

            LinearOffset offsetNew1 = offset * factor;
            Assert.AreEqual(scaledOffset, offsetNew1.Delta(), Tolerance);

            LinearOffset offsetNew2 = factor * offset;
            Assert.AreEqual(scaledOffset, offsetNew2.Delta(), Tolerance);
        }

        [TestCase(0, 0, 1, 0)]
        [TestCase(2.2, 4.4, 1, 2.2)]
        [TestCase(2.2, 4.4, 0.5, 4.4)]
        [TestCase(-2.2, 4.4, -0.5, -13.2)]
        [TestCase(2.2, -4.4, -0.5, 13.2)]
        [TestCase(-2.2, -4.4, 0.5, -4.4)]
        public static void DivideOverride_Divides_Coordinate_by_a_Scaling_Factor(double pointI, double pointJ, double factor, double scaledOffset)
        {
            LinearOffset offset = new LinearOffset(pointI, pointJ);
            LinearOffset offsetNew = offset / factor;
            Assert.AreEqual(scaledOffset, offsetNew.Delta(), Tolerance);
        }

        [Test]
        public static void DivideOverride_Throws_Exception_when_Dividing_by_Zero()
        {
            LinearOffset offset = new LinearOffset(1, 2);
            Assert.Throws<DivideByZeroException>(() => { LinearOffset offsetNew = offset / 0; });
        }
        #endregion
    }
}