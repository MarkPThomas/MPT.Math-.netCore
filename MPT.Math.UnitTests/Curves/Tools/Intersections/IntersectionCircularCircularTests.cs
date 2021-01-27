using MPT.Math.Coordinates;
using MPT.Math.Curves;
using MPT.Math.Curves.Tools.Intersections;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Tools.Intersections
{
    [TestFixture]
    public static class IntersectionCircularCircularTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [Test]
        public static void Initialization()
        {
            CircularCurve curve1 = new CircularCurve(new CartesianCoordinate(1, 2), new CartesianCoordinate(3, 4));
            CircularCurve curve2 = new CircularCurve(new CartesianCoordinate(-1, 2), new CartesianCoordinate(-3, 4));

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            Assert.AreEqual(curve1, intersections.Curve1);
            Assert.AreEqual(curve2, intersections.Curve2);
        }
        #endregion

        #region Methods: Public
        [TestCase(0, 0, 6, 11, 0, 5, true)]  // Tangent
        [TestCase(4, 0, 6, 15, 0, 5, true)]  // Tangent, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 5, true)]  // Tangent, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 5, true)]  // Tangent, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, false)]  // Intersection, Translated  and Rotated to quadrant 1
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        public static void AreTangent(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            bool result = intersections.AreTangent();

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 0, 6, 11, 0, 3, false)]  // No intersection
        [TestCase(4, 0, 6, 15, 0, 3, false)]  // No intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 3, false)]  // No intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 3, false)]  // No intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 4
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, true)]  // Intersection, Translated  and Rotated to quadrant 1
        public static void AreIntersecting(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            bool result = intersections.AreIntersecting();

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 0, 6, 11, 0, 7, 4.909091, 3.449757, 4.909091, -3.449757)]  // Intersection
        [TestCase(4, 0, 6, 15, 0, 7, 8.909091, 3.449757, 8.909091, -3.449757)]  // Intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 7, 0.909091, 3.449757, 0.909091, -3.449757)]  // Intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 7, 2.526519, 5.442123, 5.976276, -0.533032)]  // Intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, 6.526519, 10.442123, 9.976276, 4.466968)]  // Intersection, Translated and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 7, -9.976276, 4.466968, -6.526519, 10.442123)]  // Intersection, Translated and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 7, -6.526519, -10.442123, -9.976276, -4.466968)]  // Intersection, Translated and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 7, 9.976276, -4.466968, 6.526519, -10.442123)]  // Intersection, Translated and Rotated to quadrant 4
        public static void IntersectionCoordinates(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double x1Expected, double y1Expected,
            double x2Expected, double y2Expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
            Assert.AreEqual(x2Expected, intersectionCoordinates[1].X, Tolerance);
            Assert.AreEqual(y2Expected, intersectionCoordinates[1].Y, Tolerance);
        }

        [TestCase(0, 0, 6, 11, 0, 3)]  // No intersection
        [TestCase(4, 0, 6, 15, 0, 3)]  // No intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 3)]  // No intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 3)]  // No intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 4
        public static void IntersectionCoordinates_of_Not_Intersecting_Returns_Empty_Array(
            double x1, double y1, double r1,
            double x2, double y2, double r2)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(0, intersectionCoordinates.Length);
        }

        [TestCase(0, 0, 6, 11, 0, 5, 6, 0)]  // Tangent
        [TestCase(4, 0, 6, 15, 0, 5, 10, 0)]  // Tangent, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 5, 2, 0)]  // Tangent, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 5, 5.196152, 3)]  // Tangent, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, 9.196152, 8)]  // Tangent, Translated and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 5, -9.196152, 8)]  // Tangent, Translated and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 5, -9.196152, -8)]  // Tangent, Translated and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, 9.196152, -8)]  // Tangent, Translated and Rotated to quadrant 4
        public static void IntersectionCoordinates_of_Tangents_Returns_Tangent_Coordinate(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double x1Expected, double y1Expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            CartesianCoordinate[] intersectionCoordinates = intersections.IntersectionCoordinates();

            Assert.AreEqual(1, intersectionCoordinates.Length);
            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        }

        [TestCase(0, 0, 6, 11, 0, 3, 11)]  // No intersection
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 7, 11)]  // Intersection, Translated and Rotated to quadrant 4
        [TestCase(0, 0, 6, 0, 0, 3, 0)]  // Circles overlap
        public static void CenterSeparations_Returns_Distance_Circle_Centers_Are_Separated_By(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            double result = intersections.CenterSeparations();

            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0, 6, 11, 0, 5, 0)]  // Tangent
        [TestCase(0, 0, 6, 11, 0, 7, 6.899515)]  // Intersection
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 7, 6.899515)]  // Intersection, Translated and Rotated to quadrant 4
        public static void RadicalLineLength_Returns_Length_of_Radical_Line_Formed_By_Circular_Intersection(
           double x1, double y1, double r1,
           double x2, double y2, double r2,
           double expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);
            double result = intersections.RadicalLineLength();

            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0, 6, 11, 0, 3)]  // No intersection
        public static void RadicalLineLength_Throws_IntersectingCurveException_when_No_Intersection(
          double x1, double y1, double r1,
          double x2, double y2, double r2)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);

            Assert.Throws<IntersectingCurveException>(() => intersections.RadicalLineLength());
        }

        [TestCase(0, 0, 6, 0, 0, 3)]  // Circles overlap, different sizes
        [TestCase(0, 0, 6, 0, 0, 6)]  // Circles overlap, same sizes
        public static void RadicalLineLength_Throws_OverlappingCurvesException_when_No_Separation(
          double x1, double y1, double r1,
          double x2, double y2, double r2)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            IntersectionCircularCircular intersections = new IntersectionCircularCircular(curve1, curve2);

            Assert.Throws<OverlappingCurvesException>(() => intersections.RadicalLineLength());
        }
        #endregion

        #region Methods: Static
        [TestCase(0, 0, 6, 11, 0, 5, true)]  // Tangent
        [TestCase(4, 0, 6, 15, 0, 5, true)]  // Tangent, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 5, true)]  // Tangent, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 5, true)]  // Tangent, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, false)]  // Intersection, Translated  and Rotated to quadrant 1
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        public static void AreTangent_Static(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            bool result = IntersectionCircularCircular.AreTangent(curve1, curve2);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 0, 6, 11, 0, 3, false)]  // No intersection
        [TestCase(4, 0, 6, 15, 0, 3, false)]  // No intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 3, false)]  // No intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 3, false)]  // No intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 3, false)]  // No intersection, Translated  and Rotated to quadrant 4
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, true)]  // Tangent, Translated  and Rotated to quadrant 4
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, true)]  // Intersection, Translated  and Rotated to quadrant 1
        public static void AreIntersecting_Static(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            bool expectedResult)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            bool result = IntersectionCircularCircular.AreIntersecting(curve1, curve2);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 0, 6, 11, 0, 7, 4.909091, 3.449757, 4.909091, -3.449757)]  // Intersection
        [TestCase(4, 0, 6, 15, 0, 7, 8.909091, 3.449757, 8.909091, -3.449757)]  // Intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 7, 0.909091, 3.449757, 0.909091, -3.449757)]  // Intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 7, 2.526519, 5.442123, 5.976276, -0.533032)]  // Intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 7, 6.526519, 10.442123, 9.976276, 4.466968)]  // Intersection, Translated and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 7, -9.976276, 4.466968, -6.526519, 10.442123)]  // Intersection, Translated and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 7, -6.526519, -10.442123, -9.976276, -4.466968)]  // Intersection, Translated and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 7, 9.976276, -4.466968, 6.526519, -10.442123)]  // Intersection, Translated and Rotated to quadrant 4
        public static void IntersectionCoordinates_Static(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double x1Expected, double y1Expected,
            double x2Expected, double y2Expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionCircularCircular.IntersectionCoordinates(curve1, curve2);

            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
            Assert.AreEqual(x2Expected, intersectionCoordinates[1].X, Tolerance);
            Assert.AreEqual(y2Expected, intersectionCoordinates[1].Y, Tolerance);
        }

        [TestCase(0, 0, 6, 11, 0, 3)]  // No intersection
        [TestCase(4, 0, 6, 15, 0, 3)]  // No intersection, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 3)]  // No intersection, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 3)]  // No intersection, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 3)]  // No intersection, Translated  and Rotated to quadrant 4
        public static void IntersectionCoordinates_Static_of_Not_Intersecting_Returns_Empty_Array(
            double x1, double y1, double r1,
            double x2, double y2, double r2)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionCircularCircular.IntersectionCoordinates(curve1, curve2);
            Assert.AreEqual(0, intersectionCoordinates.Length);
        }

        [TestCase(0, 0, 6, 11, 0, 5, 6, 0)]  // Tangent
        [TestCase(4, 0, 6, 15, 0, 5, 10, 0)]  // Tangent, Shifted+
        [TestCase(-4, 0, 6, 7, 0, 5, 2, 0)]  // Tangent, Shifted-
        [TestCase(0, 0, 6, 9.52627944162883, 5.5, 5, 5.196152, 3)]  // Tangent, Rotated+
        [TestCase(4, 5, 6, 13.5262794416288, 10.5, 5, 9.196152, 8)]  // Tangent, Translated and Rotated to quadrant 1
        [TestCase(-4, 5, 6, -13.5262794416288, 10.5, 5, -9.196152, 8)]  // Tangent, Translated and Rotated to quadrant 2
        [TestCase(-4, -5, 6, -13.5262794416288, -10.5, 5, -9.196152, -8)]  // Tangent, Translated and Rotated to quadrant 3
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 5, 9.196152, -8)]  // Tangent, Translated and Rotated to quadrant 4
        public static void IntersectionCoordinates_Static_of_Tangents_Returns_Tangent_Coordinate(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double x1Expected, double y1Expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            CartesianCoordinate[] intersectionCoordinates = IntersectionCircularCircular.IntersectionCoordinates(curve1, curve2);
            Assert.AreEqual(1, intersectionCoordinates.Length);
            Assert.AreEqual(x1Expected, intersectionCoordinates[0].X, Tolerance);
            Assert.AreEqual(y1Expected, intersectionCoordinates[0].Y, Tolerance);
        }

        [TestCase(0, 0, 6, 11, 0, 3, 11)]  // No intersection
        [TestCase(4, -5, 6, 13.5262794416288, -10.5, 7, 11)]  // Intersection, Translated and Rotated to quadrant 4
        [TestCase(0, 0, 6, 0, 0, 3, 0)]  // Circles overlap
        public static void CenterSeparations_Static_Returns_Distance_Circle_Centers_Are_Separated_By(
            double x1, double y1, double r1,
            double x2, double y2, double r2,
            double expected)
        {
            CircularCurve curve1 = new CircularCurve(r1, new CartesianCoordinate(x1, y1));
            curve1.Tolerance = Tolerance;
            CircularCurve curve2 = new CircularCurve(r2, new CartesianCoordinate(x2, y2));
            curve2.Tolerance = Tolerance;

            double result = IntersectionCircularCircular.CenterSeparations(curve1, curve2);

            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(11, 6, 5, 0)]  // Tangent
        [TestCase(11, 6, 7, 6.899515)]  // Intersection
        public static void RadicalLineLength_Static_Returns_Length_of_Radical_Line_Formed_By_Circular_Intersection(
           double separation, double radius1, double radius2,
           double expected)
        {
            double result = IntersectionCircularCircular.RadicalLineLength(separation, radius1, radius2, Tolerance);

            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(50, 6, 3)]  // No intersection
        public static void RadicalLineLength_Static_Throws_IntersectingCurveException_when_No_Intersection(double separation, double radius1, double radius2)
        {
            Assert.Throws<IntersectingCurveException>(() => IntersectionCircularCircular.RadicalLineLength(separation, radius1, radius2));
        }

        [TestCase(0, 6, 3)]  // Circles overlap, different sizes
        [TestCase(0, 6, 6)]  // Circles overlap, same sizes
        public static void RadicalLineLength_Static_Throws_OverlappingCurvesException_when_No_Separation(double separation, double radius1, double radius2)
        {
            Assert.Throws<OverlappingCurvesException>(() => IntersectionCircularCircular.RadicalLineLength(separation, radius1, radius2));
        }
        #endregion
    }
}
