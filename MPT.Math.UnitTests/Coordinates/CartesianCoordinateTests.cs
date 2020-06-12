﻿using MPT.Math.Coordinates;
using NUnit.Framework;
using System;

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
        #endregion

        #region Methods: Public Static
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
        public static void Rotate(double x, double y, double angleRadians, double expectedX, double expectedY)
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
        public static void RotateAboutPoint(double x, double y, double centerX, double centerY, double angleRadians, double expectedX, double expectedY)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);
            CartesianCoordinate centerOfRotation = new CartesianCoordinate(centerX, centerY);
            CartesianCoordinate cartesianCoordinate = CartesianCoordinate.RotateAboutPoint(coordinate, centerOfRotation, angleRadians);

            Assert.AreEqual(expectedX, cartesianCoordinate.X, Tolerance);
            Assert.AreEqual(expectedY, cartesianCoordinate.Y, Tolerance);
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
            PolarCoordinate polarCoordinate = new PolarCoordinate(radius, azimuth, tolerance);

            Assert.AreEqual(expectedResult, cartesianCoordinate.Equals(polarCoordinate));
            Assert.AreEqual(expectedResult, cartesianCoordinate == polarCoordinate);
            Assert.AreEqual(!expectedResult, cartesianCoordinate != polarCoordinate);
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
    }
}
