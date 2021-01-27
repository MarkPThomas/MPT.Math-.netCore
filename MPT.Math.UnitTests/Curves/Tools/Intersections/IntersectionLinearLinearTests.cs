using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Tools.Intersections;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools.Intersections
{
    [TestFixture]
    public static class IntersectionLinearLinearTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization() 
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(1, 2), new CartesianCoordinate(3, 4));
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(-1, 2), new CartesianCoordinate(-3, 4));

            IntersectionLinearLinear intersections = new IntersectionLinearLinear(curve1, curve2);
            Assert.AreEqual(curve1, intersections.Curve1);
            Assert.AreEqual(curve2, intersections.Curve2);
        }
        #endregion

        #region Methods: Public
        [TestCase(1, 2, 1, 3, 1, 4, 1, 5, true)]  // Vertical-Vertical Tangent
        [TestCase(1, 1, 3, 1, 4, 1, 6, 1, true)]  // Horizontal-Horizontal Tangent
        [TestCase(1, 1, 4, 3, 7, 5, 10, 7, true)]  // Sloped-Sloped Tangent
        [TestCase(5, 1, 7, 1, 6, 2, 8, 2, false)]  // Horizontal-Horizontal Parallel
        [TestCase(1, 2, 1, 3, 2, 3, 2, 4, false)]  // Vertical-Vertical Parallel
        [TestCase(1, 1, 4, 3, 9, 5, 12, 7, false)]  // Sloped-Sloped Parallel
        [TestCase(1, 1, 4, 3, 9, 6, 12, 9, false)]  // Sloped-Sloped
        public static void AreTangent(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4,
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            IntersectionLinearLinear intersections = new IntersectionLinearLinear(curve1, curve2);
            bool result = intersections.AreTangent();
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 2, 1, 3, 1, 4, 1, 5, false)]  // Vertical-Vertical Tangent
        [TestCase(1, 1, 3, 1, 4, 1, 6, 1, false)]  // Horizontal-Horizontal Tangent
        [TestCase(1, 1, 4, 3, 7, 5, 10, 7, false)]  // Sloped-Sloped Tangent
        [TestCase(5, 1, 7, 1, 6, 2, 8, 2, false)]  // Horizontal-Horizontal Parallel
        [TestCase(1, 2, 1, 3, 2, 3, 2, 4, false)]  // Vertical-Vertical Parallel
        [TestCase(1, 1, 4, 3, 9, 5, 12, 7, false)]  // Sloped-Sloped Parallel
        [TestCase(1, 1, 4, 3, 9, 6, 12, 9, true)]  // Sloped-Sloped
        public static void AreIntersecting(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4,
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            IntersectionLinearLinear intersections = new IntersectionLinearLinear(curve1, curve2);
            bool result = intersections.AreIntersecting();
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(-6, 4, 6, 4, 4, 6, 4, -6, 4, 4)]  // Perpedicular aligned quadrant 1
        [TestCase(-6, 4, 6, 4, -4, 6, -4, -6, -4, 4)]  // Perpedicular aligned quadrant 2
        [TestCase(-6, -4, 6, -4, -4, 6, -4, -6, -4, -4)]  // Perpedicular aligned quadrant 3
        [TestCase(-6, -4, 6, -4, 4, 6, 4, -6, 4, -4)]  // Perpedicular aligned quadrant 4
        [TestCase(0, 4, 4, 0, 0, 0, 4, 4, 2, 2)]  // Perpendicular, rotated
        [TestCase(0, 0, 4, 4, 2, 0, 2, 4, 2, 2)]  // Sloped-Vertical
        [TestCase(0, 0, 4, 4, 0, 2, 4, 2, 2, 2)]  // Sloped-Horizontal
        [TestCase(0, 0, -4, 4, -1, 0, -3, 4, -2, 2)]  // Sloped-Sloped
        [TestCase(1, 1, 4, 3, 7, 6, 10, 9, 4, 3)]  // Sloped-Sloped
        [TestCase(1, 1, 4, 3, 9, 6, 12, 9, 10, 7)]  // Sloped-Sloped
        public static void IntersectionCoordinates(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4,
            double xExpected, double yExpected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            IntersectionLinearLinear intersections = new IntersectionLinearLinear(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(xExpected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(yExpected, intersectionCoordinates[0].Y, Tolerance);
        }

        [TestCase(1, 2, 1, 3, 1, 4, 1, 5)]  // Vertical-Vertical Tangent
        [TestCase(1, 1, 3, 1, 4, 1, 6, 1)]  // Horizontal-Horizontal Tangent
        [TestCase(1, 1, 4, 3, 7, 5, 10, 7)]  // Sloped-Sloped Tangent
        public static void IntersectionCoordinates_of_Parallel_Lines_Returns_Empty_Array(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            IntersectionLinearLinear intersections = new IntersectionLinearLinear(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(0, intersectionCoordinates.Length);
        }

        [TestCase(5, 1, 7, 1, 6, 2, 8, 2)]  // Horizontal-Horizontal Parallel
        [TestCase(1, 2, 1, 3, 2, 3, 2, 4)]  // Vertical-Vertical Parallel
        [TestCase(1, 1, 4, 3, 9, 5, 12, 7)]  // Sloped-Sloped Parallel
        public static void IntersectionCoordinates_of_Collinear_Lines_Returns_Empty_Array(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            IntersectionLinearLinear intersections = new IntersectionLinearLinear(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(0, intersectionCoordinates.Length);
        }
        #endregion

        #region Methods: Static
        [TestCase(1, 2, 1, 3, 1, 4, 1, 5, true)]  // Vertical-Vertical Tangent
        [TestCase(1, 1, 3, 1, 4, 1, 6, 1, true)]  // Horizontal-Horizontal Tangent
        [TestCase(1, 1, 4, 3, 7, 5, 10, 7, true)]  // Sloped-Sloped Tangent
        [TestCase(5, 1, 7, 1, 6, 2, 8, 2, false)]  // Horizontal-Horizontal Parallel
        [TestCase(1, 2, 1, 3, 2, 3, 2, 4, false)]  // Vertical-Vertical Parallel
        [TestCase(1, 1, 4, 3, 9, 5, 12, 7, false)]  // Sloped-Sloped Parallel
        [TestCase(1, 1, 4, 3, 9, 6, 12, 9, false)]  // Sloped-Sloped
        public static void AreTangent_Static(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4,
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            bool result = IntersectionLinearLinear.AreTangent(curve1, curve2);
            Assert.AreEqual(expectedResult, result);
        }


        [TestCase(1, 2, 1, 3, 1, 4, 1, 5, false)]  // Vertical-Vertical Tangent
        [TestCase(1, 1, 3, 1, 4, 1, 6, 1, false)]  // Horizontal-Horizontal Tangent
        [TestCase(1, 1, 4, 3, 7, 5, 10, 7, false)]  // Sloped-Sloped Tangent
        [TestCase(5, 1, 7, 1, 6, 2, 8, 2, false)]  // Horizontal-Horizontal Parallel
        [TestCase(1, 2, 1, 3, 2, 3, 2, 4, false)]  // Vertical-Vertical Parallel
        [TestCase(1, 1, 4, 3, 9, 5, 12, 7, false)]  // Sloped-Sloped Parallel
        [TestCase(1, 1, 4, 3, 9, 6, 12, 9, true)]  // Sloped-Sloped
        public static void AreIntersecting_Static(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4,
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            bool result = IntersectionLinearLinear.AreIntersecting(curve1, curve2);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(-6, 4, 6, 4, 4, 6, 4, -6, 4, 4)]  // Perpedicular aligned quadrant 1
        [TestCase(-6, 4, 6, 4, -4, 6, -4, -6, -4, 4)]  // Perpedicular aligned quadrant 2
        [TestCase(-6, -4, 6, -4, -4, 6, -4, -6, -4, -4)]  // Perpedicular aligned quadrant 3
        [TestCase(-6, -4, 6, -4, 4, 6, 4, -6, 4, -4)]  // Perpedicular aligned quadrant 4
        [TestCase(0, 4, 4, 0, 0, 0, 4, 4, 2, 2)]  // Perpendicular, rotated
        [TestCase(0, 0, 4, 4, 2, 0, 2, 4, 2, 2)]  // Sloped-Vertical
        [TestCase(0, 0, 4, 4, 0, 2, 4, 2, 2, 2)]  // Sloped-Horizontal
        [TestCase(0, 0, -4, 4, -1, 0, -3, 4, -2, 2)]  // Sloped-Sloped
        [TestCase(1, 1, 4, 3, 7, 6, 10, 9, 4, 3)]  // Sloped-Sloped
        [TestCase(1, 1, 4, 3, 9, 6, 12, 9, 10, 7)]  // Sloped-Sloped
        public static void IntersectionCoordinates_Static(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4,
            double xExpected, double yExpected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionLinearLinear.IntersectionCoordinates(curve1, curve2);

            Assert.AreEqual(xExpected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(yExpected, intersectionCoordinates[0].Y, Tolerance);
        }

        [TestCase(1, 2, 1, 3, 1, 4, 1, 5)]  // Vertical-Vertical Tangent
        [TestCase(1, 1, 3, 1, 4, 1, 6, 1)]  // Horizontal-Horizontal Tangent
        [TestCase(1, 1, 4, 3, 7, 5, 10, 7)]  // Sloped-Sloped Tangent
        public static void IntersectionCoordinates_Static_of_Parallel_Lines_Returns_Empty_Array(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionLinearLinear.IntersectionCoordinates(curve1, curve2);
            Assert.AreEqual(0, intersectionCoordinates.Length);
        }

        [TestCase(5, 1, 7, 1, 6, 2, 8, 2)]  // Horizontal-Horizontal Parallel
        [TestCase(1, 2, 1, 3, 2, 3, 2, 4)]  // Vertical-Vertical Parallel
        [TestCase(1, 1, 4, 3, 9, 5, 12, 7)]  // Sloped-Sloped Parallel
        public static void IntersectionCoordinates_Static_of_Collinear_Lines_Returns_Empty_Array(
            double x1, double y1, double x2, double y2,
            double x3, double y3, double x4, double y4)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            LinearCurve curve2 = new LinearCurve(new CartesianCoordinate(x3, y3), new CartesianCoordinate(x4, y4));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionLinearLinear.IntersectionCoordinates(curve1, curve2);
            Assert.AreEqual(0, intersectionCoordinates.Length);
        }
        #endregion
    }
}
