using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Tools.Intersections;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools.Intersections
{
    [TestFixture]
    public static class IntersectionLinearCircularTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization()
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(1, 2), new CartesianCoordinate(3, 4));
            CircularCurve curve2 = new CircularCurve(new CartesianCoordinate(-1, 2), new CartesianCoordinate(-3, 4));

            IntersectionLinearCircular intersections = new IntersectionLinearCircular(curve1, curve2);
            Assert.AreEqual(curve1, intersections.Curve1);
            Assert.AreEqual(curve2, intersections.Curve2);
        }
        #endregion

        #region Methods: Public
        [TestCase(6, 6, 6, -6, 0, 0, 6, true)]  // Vertical Tangent at +x
        [TestCase(-6, 6, -6, -6, 0, 0, 6, true)]  // Vertical Tangent at -x
        [TestCase(-6, 6, 6, 6, 0, 0, 6, true)]  // Horizontal Tangent at +y
        [TestCase(-6, -6, 6, -6, 0, 0, 6, true)]  // Horizontal Tangent at -y
        [TestCase(0, 8.48528137423857, 8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 1
        [TestCase(0, 8.48528137423857, -8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 2
        [TestCase(0, -8.48528137423857, -8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 3
        [TestCase(0, -8.48528137423857, 8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 4
        [TestCase(5, 14.4852813742386, 13.4852813742386, 6, 5, 6, 6, true)]  // Sloped Tangent in Quadrant 1 in translated coordinates
        [TestCase(5, 13, 12, 6, 5, 6, 6, false)]  // Sloped Intersection in translated coordinates
        public static void AreTangent(
            double x1, double y1, double x2, double y2,
            double x, double y, double r, 
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            IntersectionLinearCircular intersections = new IntersectionLinearCircular(curve1, curve2);
            bool result = intersections.AreTangent();
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 10, 10, 0, 0, 0, 6, false)]  // No Intersection
        [TestCase(5, 16, 15, 6, 5, 6, 6, false)]  // No Intersection in translated coordinates
        [TestCase(5, 13, 12, 6, 5, 6, 6, true)]  // Sloped Intersection in translated coordinates
        public static void AreIntersecting(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            IntersectionLinearCircular intersections = new IntersectionLinearCircular(curve1, curve2);
            bool result = intersections.AreIntersecting();
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, -10, 0, 10, 0, 0, 6, 0, 6, 0, -6)]  // Vertical Intersection through origin
        [TestCase(-10, 0, 10, 0, 0, 0, 6, 6, 0, -6, 0)]  // Horizontal Intersection through origin
        [TestCase(-6, -6, 6, 6, 0, 0, 6, 4.242641, 4.242641, -4.242641, -4.242641)]  // +Sloped Intersection through origin
        [TestCase(-6, 6, 6, -6, 0, 0, 6, -4.242641, 4.242641, 4.242641, -4.242641)]  // -Sloped Intersection through origin
        [TestCase(3, -10, 3, 10, 0, 0, 6, 3, 5.196152, 3, -5.196152)]  // Vertical Intersection at +x
        [TestCase(-3, -10, -3, 10, 0, 0, 6, -3, 5.196152, -3, -5.196152)]  // Vertical Intersection at -x
        [TestCase(10, 3, -10, 3, 0, 0, 6, -5.196152, 3, 5.196152, 3)]  // Horizontal Intersection at +y
        [TestCase(10, -3, -10, -3, 0, 0, 6, -5.196152, -3, 5.196152, -3)]  // Horizontal Intersection at -y
        [TestCase(0, 7, 7, 0, 0, 0, 6, 1.102084, 5.897916, 5.897916, 1.102084)]  // Sloped Intersection
        [TestCase(5, 13, 12, 6, 5, 6, 6, 6.102084, 11.897916, 10.897916, 7.102084)]  // Sloped Intersection in translated coordinates
        public static void IntersectionCoordinates(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            double x1Expected, double y1Expected,
            double x2Expected, double y2Expected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            IntersectionLinearCircular intersections = new IntersectionLinearCircular(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
            Assert.AreEqual(x2Expected, intersectionCoordinates[1].X, Tolerance);
            Assert.AreEqual(y2Expected, intersectionCoordinates[1].Y, Tolerance);
        }

        [TestCase(0, 10, 10, 0, 0, 0, 6)]  // No Intersection
        [TestCase(5, 16, 15, 6, 5, 6, 6)]  // No Intersection in translated coordinates
        public static void IntersectionCoordinates_of_Not_Intersecting_Returns_Empty_Array(
            double x1, double y1, double x2, double y2,
            double x, double y, double r)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            IntersectionLinearCircular intersections = new IntersectionLinearCircular(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(0, intersectionCoordinates.Length);
        }

        [TestCase(6, 6, 6, -6, 0, 0, 6, 6, 0)]  // Vertical Tangent at +x
        [TestCase(-6, 6, -6, -6, 0, 0, 6, -6, 0)]  // Vertical Tangent at -x
        [TestCase(-6, 6, 6, 6, 0, 0, 6, 0, 6)]  // Horizontal Tangent at +y
        [TestCase(-6, -6, 6, -6, 0, 0, 6, 0, -6)]  // Horizontal Tangent at -y
        [TestCase(0, 8.48528137423857, 8.48528137423857, 0, 0, 0, 6, 4.24264068711928, 4.24264068711929)]  // Sloped Tangent in Quadrant 1
        [TestCase(0, 8.48528137423857, -8.48528137423857, 0, 0, 0, 6, -4.24264068711928, 4.24264068711929)]  // Sloped Tangent in Quadrant 2
        [TestCase(0, -8.48528137423857, -8.48528137423857, 0, 0, 0, 6, -4.24264068711928, -4.24264068711929)]  // Sloped Tangent in Quadrant 3
        [TestCase(0, -8.48528137423857, 8.48528137423857, 0, 0, 0, 6, 4.24264068711928, -4.24264068711929)]  // Sloped Tangent in Quadrant 4
        [TestCase(5, 14.4852813742386, 13.4852813742386, 6, 5, 6, 6, 9.24264068711928, 10.2426406871193)]  // Sloped Tangent in Quadrant 1 in translated coordinates
        public static void IntersectionCoordinates_of_Tangents_Returns_Tangent_Coordinate(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            double x1Expected, double y1Expected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            IntersectionLinearCircular intersections = new IntersectionLinearCircular(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(1, intersectionCoordinates.Length);
            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        }
        #endregion

        #region Methods: Static
        [TestCase(6, 6, 6, -6, 0, 0, 6, true)]  // Vertical Tangent at +x
        [TestCase(-6, 6, -6, -6, 0, 0, 6, true)]  // Vertical Tangent at -x
        [TestCase(-6, 6, 6, 6, 0, 0, 6, true)]  // Horizontal Tangent at +y
        [TestCase(-6, -6, 6, -6, 0, 0, 6, true)]  // Horizontal Tangent at -y
        [TestCase(0, 8.48528137423857, 8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 1
        [TestCase(0, 8.48528137423857, -8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 2
        [TestCase(0, -8.48528137423857, -8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 3
        [TestCase(0, -8.48528137423857, 8.48528137423857, 0, 0, 0, 6, true)]  // Sloped Tangent in Quadrant 4
        [TestCase(5, 14.4852813742386, 13.4852813742386, 6, 5, 6, 6, true)]  // Sloped Tangent in Quadrant 1 in translated coordinates
        [TestCase(5, 13, 12, 6, 5, 6, 6, false)]  // Sloped Intersection in translated coordinates
        public static void AreTangent_Static(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            bool result = IntersectionLinearCircular.AreTangent(curve1, curve2);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 10, 10, 0, 0, 0, 6, false)]  // No Intersection
        [TestCase(5, 16, 15, 6, 5, 6, 6, false)]  // No Intersection in translated coordinates
        [TestCase(5, 13, 12, 6, 5, 6, 6, true)]  // Sloped Intersection in translated coordinates
        public static void AreIntersecting_Static(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            bool expectedResult)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            bool result = IntersectionLinearCircular.AreIntersecting(curve1, curve2);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, -10, 0, 10, 0, 0, 6, 0, 6, 0, -6)]  // Vertical Intersection through origin
        [TestCase(-10, 0, 10, 0, 0, 0, 6, 6, 0, -6, 0)]  // Horizontal Intersection through origin
        [TestCase(-6, -6, 6, 6, 0, 0, 6, 4.242641, 4.242641, -4.242641, -4.242641)]  // +Sloped Intersection through origin
        [TestCase(-6, 6, 6, -6, 0, 0, 6, -4.242641, 4.242641, 4.242641, -4.242641)]  // -Sloped Intersection through origin
        [TestCase(3, -10, 3, 10, 0, 0, 6, 3, 5.196152, 3, -5.196152)]  // Vertical Intersection at +x
        [TestCase(-3, -10, -3, 10, 0, 0, 6, -3, 5.196152, -3, -5.196152)]  // Vertical Intersection at -x
        [TestCase(10, 3, -10, 3, 0, 0, 6, -5.196152, 3, 5.196152, 3)]  // Horizontal Intersection at +y
        [TestCase(10, -3, -10, -3, 0, 0, 6, -5.196152, -3, 5.196152, -3)]  // Horizontal Intersection at -y
        [TestCase(0, 7, 7, 0, 0, 0, 6, 1.102084, 5.897916, 5.897916, 1.102084)]  // Sloped Intersection
        [TestCase(5, 13, 12, 6, 5, 6, 6, 6.102084, 11.897916, 10.897916, 7.102084)]  // Sloped Intersection in translated coordinates
        public static void IntersectionCoordinates_Static(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            double x1Expected, double y1Expected,
            double x2Expected, double y2Expected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionLinearCircular.IntersectionCoordinates(curve1, curve2);

            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
            Assert.AreEqual(x2Expected, intersectionCoordinates[1].X, Tolerance);
            Assert.AreEqual(y2Expected, intersectionCoordinates[1].Y, Tolerance);
        }



        [TestCase(0, 10, 10, 0, 0, 0, 6)]  // No Intersection
        [TestCase(5, 16, 15, 6, 5, 6, 6)]  // No Intersection in translated coordinates
        public static void IntersectionCoordinates_Static_of_Not_Intersecting_Returns_Empty_Array(
            double x1, double y1, double x2, double y2,
            double x, double y, double r)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionLinearCircular.IntersectionCoordinates(curve1, curve2);

            Assert.AreEqual(0, intersectionCoordinates.Length);
        }


        [TestCase(6, 6, 6, -6, 0, 0, 6, 6, 0)]  // Vertical Tangent at +x
        [TestCase(-6, 6, -6, -6, 0, 0, 6, -6, 0)]  // Vertical Tangent at -x
        [TestCase(-6, 6, 6, 6, 0, 0, 6, 0, 6)]  // Horizontal Tangent at +y
        [TestCase(-6, -6, 6, -6, 0, 0, 6, 0, -6)]  // Horizontal Tangent at -y
        [TestCase(0, 8.48528137423857, 8.48528137423857, 0, 0, 0, 6, 4.24264068711928, 4.24264068711929)]  // Sloped Tangent in Quadrant 1
        [TestCase(0, 8.48528137423857, -8.48528137423857, 0, 0, 0, 6, -4.24264068711928, 4.24264068711929)]  // Sloped Tangent in Quadrant 2
        [TestCase(0, -8.48528137423857, -8.48528137423857, 0, 0, 0, 6, -4.24264068711928, -4.24264068711929)]  // Sloped Tangent in Quadrant 3
        [TestCase(0, -8.48528137423857, 8.48528137423857, 0, 0, 0, 6, 4.24264068711928, -4.24264068711929)]  // Sloped Tangent in Quadrant 4
        [TestCase(5, 14.4852813742386, 13.4852813742386, 6, 5, 6, 6, 9.24264068711928, 10.2426406871193)]  // Sloped Tangent in Quadrant 1 in translated coordinates
        public static void IntersectionCoordinates_Static_of_Tangents_Returns_Tangent_Coordinate(
            double x1, double y1, double x2, double y2,
            double x, double y, double r,
            double x1Expected, double y1Expected)
        {
            LinearCurve curve1 = new LinearCurve(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r, new CartesianCoordinate(x, y));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionLinearCircular.IntersectionCoordinates(curve1, curve2);

            Assert.AreEqual(1, intersectionCoordinates.Length);
            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        }
        #endregion
    }
}
