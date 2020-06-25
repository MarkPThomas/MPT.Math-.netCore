using MPT.Math.Coordinates;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Coordinates
{
    [TestFixture]
    public static class CartesianOffsetTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void CartesianOffset_InitializationWithDefaultTolerance()
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(1, 2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(3, 4);
            CartesianOffset offset = new CartesianOffset(coordinate1, coordinate2);

            Assert.AreEqual(coordinate1.X, offset.I.X);
            Assert.AreEqual(coordinate1.Y, offset.I.Y);
            Assert.AreEqual(coordinate2.X, offset.J.X);
            Assert.AreEqual(coordinate2.Y, offset.J.Y);
            Assert.AreEqual(Numbers.ZeroTolerance, offset.Tolerance);
        }

        [Test]
        public static void CartesianOffset_Initialization_with_Coordinates()
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(1, 2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(3, 4);
            double tolerance = 0.5;
            CartesianOffset offset = new CartesianOffset(coordinate1, coordinate2, tolerance);

            Assert.AreEqual(coordinate1.X, offset.I.X);
            Assert.AreEqual(coordinate1.Y, offset.I.Y);
            Assert.AreEqual(coordinate2.X, offset.J.X);
            Assert.AreEqual(coordinate2.Y, offset.J.Y);
            Assert.AreEqual(tolerance, offset.Tolerance);
        }

        [Test]
        public static void CartesianOffset_Initialization_with_Offsets()
        {
            double tolerance = 0.5;
            CartesianOffset offset = new CartesianOffset(2, 3, tolerance);

            Assert.AreEqual(0, offset.I.X);
            Assert.AreEqual(0, offset.I.Y);
            Assert.AreEqual(2, offset.J.X);
            Assert.AreEqual(3, offset.J.Y);
            Assert.AreEqual(tolerance, offset.Tolerance);
        }
        #endregion

        #region Methods: Public
        [Test]
        public static void ToString_Returns_Overridden_Value()
        {
            CartesianOffset offset = new CartesianOffset(new CartesianCoordinate(0.1, 2), new CartesianCoordinate(0.5, 3));

            Assert.AreEqual("MPT.Math.Coordinates.CartesianOffset - I: (0.1, 2), J: (0.5, 3)", offset.ToString());
        }

        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 1, 2, 1, 2)]
        [TestCase(1.1, 2.3, 3.3, 4.4, 2.2, 2.1)]
        [TestCase(1.1, -2.3, -3.3, -4.4, -4.4, -2.1)]
        [TestCase(-1.1, -2.3, -3.3, 4.4, -2.2, 6.7)]
        [TestCase(-1.1, -2.3, 3.3, -4.4, 4.4, -2.1)]
        public static void ToCartesianCoordinate_Returns_Cartesian_Coordinate_Offset_from_Orgin_by_Equal_Amounts(
            double xI, double yI, double xJ, double yJ,
            double xNew, double yNew)
        {
            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(xI, yI),
                new CartesianCoordinate(xJ, yJ));

            CartesianCoordinate newCoordinate = offset.ToCartesianCoordinate();

            Assert.AreEqual(xNew, newCoordinate.X, Tolerance);
            Assert.AreEqual(yNew, newCoordinate.Y, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, -1)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 2.2, 1.2)]
        [TestCase(-1.1, 2.2, 3.3)]
        [TestCase(-1.1, -2.2, -1.1)]
        [TestCase(1.1, -2.2, -3.3)]
        public static void X_Returns_X_Coordinates_Difference(double xI, double xJ, double xDifference)
        {
            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(xI, 0),
                new CartesianCoordinate(xJ, 0));
            Assert.AreEqual(xDifference, offset.X(), Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, -1)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 2.2, 1.2)]
        [TestCase(-1.1, 2.2, 3.3)]
        [TestCase(-1.1, -2.2, -1.1)]
        [TestCase(1.1, -2.2, -3.3)]
        public static void Y_Returns_Y_Coordinates_Difference(double yI, double yJ, double yDifference)
        {
            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(0, yI),
                new CartesianCoordinate(0, yJ));
            Assert.AreEqual(yDifference, offset.Y(), Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 0, 0, 1, 1)]
        [TestCase(0, 0, 1, 0, 1)]
        [TestCase(1.1, 2.3, 3.3, 4.4, 3.041381)]
        [TestCase(1.1, -2.3, -3.3, -4.4, 4.875449)]
        [TestCase(-1.1, -2.3, -3.3, 4.4, 7.05195)]
        [TestCase(-1.1, -2.3, 3.3, -4.4, 4.875449)]
        public static void Length_Returns_Linear_Distance_Between_Offset_Points(double xI, double yI, double xJ, double yJ, double distance)
        {
            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(xI, yI),
                new CartesianCoordinate(xJ, yJ));
            Assert.AreEqual(distance, offset.Length(), Tolerance);
        }
        #endregion

        #region Operators: Equals & IEquatable
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_Coordinates()
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(1, 2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(3, 4);
            CartesianOffset offset1 = new CartesianOffset(coordinate1, coordinate2);
            CartesianOffset offset2 = new CartesianOffset(coordinate1, coordinate2);

            Assert.IsTrue(offset1.Equals(offset2));
            Assert.IsTrue(offset1.Equals((object)offset2));
            Assert.IsTrue(offset1 == offset2);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_MaxMin_Coordinates()
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(1, 2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(3, 4);
            CartesianCoordinate coordinate3 = new CartesianCoordinate(3, 4);
            CartesianOffset offset1 = new CartesianOffset(coordinate1, coordinate2);
            CartesianOffset offsetDiffI = new CartesianOffset(coordinate3, coordinate2);
            Assert.IsFalse(offset1 == offsetDiffI);

            CartesianCoordinate coordinate4 = new CartesianCoordinate(3, 5);
            CartesianOffset offsetDiffJ = new CartesianOffset(coordinate1, coordinate4);
            Assert.IsFalse(offset1 == offsetDiffJ);

            CartesianOffset offsetDiffT = new CartesianOffset(coordinate1, coordinate2, 0.001);
            Assert.IsTrue(offset1 == offsetDiffT);

            object obj = new object();
            Assert.IsFalse(offset1.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_MaxMin_Coordinates()
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(1, 2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(3, 4);
            CartesianCoordinate coordinate3 = new CartesianCoordinate(3, 4);
            CartesianOffset offset1 = new CartesianOffset(coordinate1, coordinate2);
            CartesianOffset offsetDiffI = new CartesianOffset(coordinate3, coordinate2);

            Assert.IsTrue(offset1 != offsetDiffI);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_Coordinates()
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(1, 2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(3, 4);
            double tolerance = 0.0002;
            CartesianOffset offset1 = new CartesianOffset(coordinate1, coordinate2, tolerance);
            CartesianOffset offset2 = new CartesianOffset(coordinate1, coordinate2, tolerance);

            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_MaxMin_Coordinates()
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(1, 2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(3, 4);
            double tolerance = 0.0002;
            CartesianOffset offset1 = new CartesianOffset(coordinate1, coordinate2, tolerance);

            CartesianOffset offset2 = new CartesianOffset(2 * coordinate1, coordinate2, tolerance);
            Assert.AreNotEqual(offset1.GetHashCode(), offset2.GetHashCode());

            offset2 = new CartesianOffset(coordinate1, 2 * coordinate2, tolerance); 
            Assert.AreNotEqual(offset1.GetHashCode(), offset2.GetHashCode());

            offset2 = new CartesianOffset(coordinate1, coordinate2, 2 * tolerance);
            Assert.AreEqual(offset1.GetHashCode(), offset2.GetHashCode());
        }
        #endregion

        #region Operators: Combining
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(2, 4, 3, 4, 5, 6, 7, 8, 0, 0, -1, -2)]
        [TestCase(-2, 4, -3, 4, -5, 6, -7, 8, 0, 0, 1, -2)]
        [TestCase(-2, -4, -3, -4, -5, -6, -7, -8, 0, 0, 1, 2)]
        [TestCase(2, -4, 3, -4, 5, -6, 7, -8, 0, 0, -1, 2)]
        [TestCase(2, 4, 3, 4, -5, -6, -7, -8, 0, 0, 3, 2)]
        [TestCase(1, 4, 3, 4, -5, 6, -7, 8, 0, 0, 4, -2)]
        [TestCase(2, 4, 3, 4, 5, -6, 7, -8, 0, 0, -1, 2)]
        [TestCase(1, 4, 3, 4, 5, 6, 5, 8, 0, 0, 2, -2)]
        [TestCase(1, 4, 3, 4, 5, 6, 4, 8, 0, 0, 3, -2)]
        [TestCase(1.1, 2.3, 2.6, 4.7, 5.9, 6.6, 7.7, 8.8, 0, 0, -0.3, 0.2)]
        public static void SubtractOverride_Returns_Coordinate_Offset_Between_Coordinates(
            double Xi1, double Yi1, double Xj1, double Yj1,
            double Xi2, double Yi2, double Xj2, double Yj2,
            double XiResult, double YiResult, double XjResult, double YjResult)
        {
            CartesianOffset offset1 = new CartesianOffset(
                new CartesianCoordinate(Xi1, Yi1),
                new CartesianCoordinate(Xj1, Yj1));

            CartesianOffset offset2 = new CartesianOffset(
                new CartesianCoordinate(Xi2, Yi2),
                new CartesianCoordinate(Xj2, Yj2));

            CartesianOffset offset = offset1 - offset2;

            Assert.AreEqual(XiResult, offset.I.X, Tolerance);
            Assert.AreEqual(YiResult, offset.I.Y, Tolerance);
            Assert.AreEqual(XjResult, offset.J.X, Tolerance);
            Assert.AreEqual(YjResult, offset.J.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 4, 5, 6, 7, 8, -1, 2)]
        [TestCase(-1, 4, -5, 6, -7, 8, 1, 2)]
        [TestCase(-1, -4, -5, -6, -7, -8, 1, -2)]
        [TestCase(1, -4, 5, -6, 7, -8, -1, -2)]
        [TestCase(1, 4, -5, -6, -7, -8, 3, 6)]
        [TestCase(1, 4, -5, 6, -7, 8, 3, 2)]
        [TestCase(1, 4, 5, -6, 7, -8, -1, 6)]
        [TestCase(1, 4, 5, 6, 5, 8, 1, 2)]
        [TestCase(1, 4, 5, 6, 4, 8, 2, 2)]
        [TestCase(1.1, 4.4, 5.5, 6.6, 4.4, 8.1, 2.2, 2.9)]
        public static void SubtractOverride_Subtracted_by_Coordinate_Returns_Coordinate_Subtracting_Offset(
            double x, double y, 
            double Xi2, double Yi2, double Xj2, double Yj2,
            double XResult, double YResult)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);

            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(Xi2, Yi2),
                new CartesianCoordinate(Xj2, Yj2));

            CartesianCoordinate offsetCoordinate = coordinate - offset;

            Assert.AreEqual(XResult, offsetCoordinate.X, Tolerance);
            Assert.AreEqual(YResult, offsetCoordinate.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 4, 5, 6, 7, 8, 1, -2)]
        [TestCase(-1, 4, -5, 6, -7, 8, -1, -2)]
        [TestCase(-1, -4, -5, -6, -7, -8, -1, 2)]
        [TestCase(1, -4, 5, -6, 7, -8, 1, 2)]
        [TestCase(1, 4, -5, -6, -7, -8, -3, -6)]
        [TestCase(1, 4, -5, 6, -7, 8, -3, -2)]
        [TestCase(1, 4, 5, -6, 7, -8, 1, -6)]
        [TestCase(1, 4, 5, 6, 5, 8, -1, -2)]
        [TestCase(1, 4, 5, 6, 4, 8, -2, -2)]
        [TestCase(1.1, 4.4, 5.5, 6.6, 4.4, 8.1, -2.2, -2.9)]
        public static void SubtractOverride_Subtracting_Coordinate_Returns_Coordinate_Subtracted_by_Offset(
            double x, double y,
            double Xi2, double Yi2, double Xj2, double Yj2,
            double XResult, double YResult)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);

            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(Xi2, Yi2),
                new CartesianCoordinate(Xj2, Yj2));

            CartesianCoordinate offsetCoordinate = offset - coordinate;

            Assert.AreEqual(XResult, offsetCoordinate.X, Tolerance);
            Assert.AreEqual(YResult, offsetCoordinate.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 0, 0, 4, 4)]
        [TestCase(-1, 2, -3, 4, -5, 6, -7, 8, 0, 0, -4, 4)]
        [TestCase(-1, -2, -3, -4, -5, -6, -7, -8, 0, 0, -4, -4)]
        [TestCase(1, -2, 3, -4, 5, -6, 7, -8, 0, 0, 4, -4)]
        [TestCase(2, 4, 3, 4, -5, -6, -7, -8, 0, 0, -1, -2)]
        [TestCase(2, 2, 3, 4, -5, 6, -7, 8, 0, 0, -1, 4)]
        [TestCase(2, 4, 3, 4, 5, -6, 7, -8, 0, 0, 3, -2)]
        [TestCase(1, 2, 3, 4, 5, 6, 5, 8, 0, 0, 2, 4)]
        [TestCase(1, 2, 3, 4, 5, 6, 4, 8, 0, 0, 1, 4)]
        [TestCase(1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 0, 0, 4.4, 4.4)]
        public static void AddOverride_Returns_Combined_Coordinates(
            double Xi1, double Yi1, double Xj1, double Yj1,
            double Xi2, double Yi2, double Xj2, double Yj2,
            double XiResult, double YiResult, double XjResult, double YjResult)
        {
            CartesianOffset offset1 = new CartesianOffset(
                new CartesianCoordinate(Xi1, Yi1),
                new CartesianCoordinate(Xj1, Yj1));

            CartesianOffset offset2 = new CartesianOffset(
                new CartesianCoordinate(Xi2, Yi2),
                new CartesianCoordinate(Xj2, Yj2));

            CartesianOffset offset = offset1 + offset2;

            Assert.AreEqual(XiResult, offset.I.X, Tolerance);
            Assert.AreEqual(YiResult, offset.I.Y, Tolerance);
            Assert.AreEqual(XjResult, offset.J.X, Tolerance);
            Assert.AreEqual(YjResult, offset.J.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 4, 5, 6, 7, 8, 3, 6)]
        [TestCase(-1, 4, -5, 6, -7, 8, -3, 6)]
        [TestCase(-1, -4, -5, -6, -7, -8, -3, -6)]
        [TestCase(1, -4, 5, -6, 7, -8, 3, -6)]
        [TestCase(1, 4, -5, -6, -7, -8, -1, 2)]
        [TestCase(1, 4, -5, 6, -7, 8, -1, 6)]
        [TestCase(1, 4, 5, -6, 7, -8, 3, 2)]
        [TestCase(1, 4, 5, 6, 5, 8, 1, 6)]
        [TestCase(1, 4, 5, 6, 4, 8, 0, 6)]
        [TestCase(3.2, 4.4, 5.5, 6.6, 4.4, 8.8, 2.1, 6.6)]
        public static void AddOverride_Added_by_Coordinate_Returns_Coordinate_Adding_Offset(
            double x, double y,
            double Xi2, double Yi2, double Xj2, double Yj2,
            double XResult, double YResult)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);

            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(Xi2, Yi2),
                new CartesianCoordinate(Xj2, Yj2));

            CartesianCoordinate offsetCoordinate = coordinate + offset;

            Assert.AreEqual(XResult, offsetCoordinate.X, Tolerance);
            Assert.AreEqual(YResult, offsetCoordinate.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(1, 4, 5, 6, 7, 8, 3, 6)]
        [TestCase(-1, 4, -5, 6, -7, 8, -3, 6)]
        [TestCase(-1, -4, -5, -6, -7, -8, -3, -6)]
        [TestCase(1, -4, 5, -6, 7, -8, 3, -6)]
        [TestCase(1, 4, -5, -6, -7, -8, -1, 2)]
        [TestCase(1, 4, -5, 6, -7, 8, -1, 6)]
        [TestCase(1, 4, 5, -6, 7, -8, 3, 2)]
        [TestCase(1, 4, 5, 6, 5, 8, 1, 6)]
        [TestCase(1, 4, 5, 6, 4, 8, 0, 6)]
        [TestCase(3.2, 4.4, 5.5, 6.6, 4.4, 8.8, 2.1, 6.6)]
        public static void AddOverride_Adding_Coordinate_Returns_Coordinate_Added_by_Offset(
            double x, double y,
            double Xi2, double Yi2, double Xj2, double Yj2,
            double XResult, double YResult)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);

            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(Xi2, Yi2),
                new CartesianCoordinate(Xj2, Yj2));

            CartesianCoordinate offsetCoordinate = offset + coordinate;

            Assert.AreEqual(XResult, offsetCoordinate.X, Tolerance);
            Assert.AreEqual(YResult, offsetCoordinate.Y, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 2, 0, 0, 0, 0)]
        [TestCase(2, 3, 0, 0, 0, 0, 0)]
        [TestCase(2, 3, 1, 2, 3, 3, 2)]
        [TestCase(2, 3, -1, -2, -3, -3, -2)]
        [TestCase(2, 3, 3.2, 6.4, 9.6, 9.6, 6.4)]
        [TestCase(2, 3, -1.2, -2.4, -3.6, -3.6, -2.4)]
        [TestCase(2.2, 3.1, 5.4, 11.88, 16.74, 16.74, 11.88)]
        public static void MultiplyOverride_Multiplies_Coordinate_by_a_Scaling_Factor(double a1, double a2, double factor, 
            double scaledIX, double scaledIY, double scaledJX, double scaledJY)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(a1, a2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(a2, a1);
            CartesianOffset offset = new CartesianOffset(coordinate1, coordinate2);

            CartesianOffset offsetNew = offset * factor;
            Assert.AreEqual(scaledIX, offsetNew.I.X, Tolerance);
            Assert.AreEqual(scaledIY, offsetNew.I.Y, Tolerance);
            Assert.AreEqual(scaledJX, offsetNew.J.X, Tolerance);
            Assert.AreEqual(scaledJY, offsetNew.J.Y, Tolerance);
        }

        [TestCase(0, 0, 2, 0, 0, 0, 0)]
        [TestCase(2, 3, 1, 2, 3, 3, 2)]
        [TestCase(2, 3, -1, -2, -3, -3, -2)]
        [TestCase(2, 3, 3.2, 0.625, 0.9375, 0.9375, 0.625)]
        [TestCase(2, 3, -1.2, -1.666667, -2.5, -2.5, -1.666667)]
        [TestCase(2.2, 3.1, 5.4, 0.407407, 0.574074, 0.574074, 0.407407)]
        public static void DivideOverride_Divides_Coordinate_by_a_Scaling_Factor(double a1, double a2, double factor,
            double scaledIX, double scaledIY, double scaledJX, double scaledJY)
        {
            CartesianCoordinate coordinate1 = new CartesianCoordinate(a1, a2);
            CartesianCoordinate coordinate2 = new CartesianCoordinate(a2, a1);
            CartesianOffset offset = new CartesianOffset(coordinate1, coordinate2);

            CartesianOffset offsetNew = offset / factor;
            Assert.AreEqual(scaledIX, offsetNew.I.X, Tolerance);
            Assert.AreEqual(scaledIY, offsetNew.I.Y, Tolerance);
            Assert.AreEqual(scaledJX, offsetNew.J.X, Tolerance);
            Assert.AreEqual(scaledJY, offsetNew.J.Y, Tolerance);
        }

        [Test]
        public static void DivideOverride_Throws_Exception_when_Dividing_by_Zero()
        {
            CartesianOffset offset = new CartesianOffset(
                new CartesianCoordinate(1, 2),
                new CartesianCoordinate(-2, 3));
            Assert.Throws<DivideByZeroException>(() => { CartesianOffset offsetNew = offset / 0; });
        }
        #endregion
    }
}
