using MPT.Math.Coordinates;
using NUnit.Framework;
using System;
using System.Numerics;

namespace MPT.Math.Vectors.UnitTests
{
    [TestFixture]
    public static class VectorTests
    {
        public static double Tolerance = 0.00001;

        #region Initialization
        [TestCase(0, 0)]
        [TestCase(2, 3)]
        [TestCase(-2, -3)]
        [TestCase(2.1, -3.2)]
        [TestCase(-2.1, 3.2)]
        public static void Initialize_Vector_with_Only_Magnitude(double xMagnitude, double yMagnitude)
        {
            Vector vector = new Vector(xMagnitude, yMagnitude);

            Assert.AreEqual(0, vector.Location.X);
            Assert.AreEqual(0, vector.Location.Y);
            Assert.AreEqual(xMagnitude, vector.Xcomponent);
            Assert.AreEqual(yMagnitude, vector.Ycomponent);
        }

        [Test]
        public static void Initialize_Vector_with_Only_Magnitudes_and_Custom_Tolerance()
        {
            double xMagnitude = 2;
            double yMagnitude = 3;

            Vector vector = new Vector(xMagnitude, yMagnitude, Tolerance);

            Assert.AreEqual(0, vector.Location.X);
            Assert.AreEqual(0, vector.Location.Y);
            Assert.AreEqual(xMagnitude, vector.Xcomponent);
            Assert.AreEqual(yMagnitude, vector.Ycomponent);
            Assert.AreEqual(Tolerance, vector.Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 3, 0, 0)]
        [TestCase(-2, -3, 0, 0)]
        [TestCase(2, 3, 2, 3)]
        [TestCase(-2, -3, 2, 3)]
        [TestCase(2, 3, -2, 3)]
        [TestCase(-2, -3, 2, -3)]
        [TestCase(-2.4, -3.2, 2.4, -3.6)]
        public static void Initialize_Vector_with_Magnitudes_and_a_Coordinate(double xMagnitude, double yMagnitude, double x, double y)
        {
            Vector vector = new Vector(
                xMagnitude, yMagnitude,
                new CartesianCoordinate(x, y));

            Assert.AreEqual(x, vector.Location.X);
            Assert.AreEqual(y, vector.Location.Y);
            Assert.AreEqual(xMagnitude, vector.Xcomponent);
            Assert.AreEqual(yMagnitude, vector.Ycomponent);
        }

        [Test]
        public static void Initialize_Vector_with_Magnitudes_and_a_Coordinate_and_Custom_Tolerance()
        {
            double xMagnitude = 2;
            double yMagnitude = 3;
            double x = 4;
            double y = 5;

            Vector vector = new Vector(
                xMagnitude, yMagnitude, 
                new CartesianCoordinate(x, y),
                Tolerance);

            Assert.AreEqual(x, vector.Location.X);
            Assert.AreEqual(y, vector.Location.Y);
            Assert.AreEqual(xMagnitude, vector.Xcomponent);
            Assert.AreEqual(yMagnitude, vector.Ycomponent);
            Assert.AreEqual(Tolerance, vector.Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 0, 3, 4)]
        [TestCase(2, 3, 0, 0)]
        [TestCase(-2, -3, 0, 0)]
        [TestCase(2, 3, 2, 3)]
        [TestCase(-2, -3, 2, 3)]
        [TestCase(2, 3, -2, 3)]
        [TestCase(-2, -3, 2, -3)]
        [TestCase(-2.1, -3.4, 2.8, -3.2)]
        public static void Initialize_Vector_with_Only_Coordinates(double x1, double y1, double x2, double y2)
        {
            double magnitudeX = x2 - x1;
            double magnitudeY = y2 - y1;

            Vector vector = new Vector(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(x1, vector.Location.X);
            Assert.AreEqual(y1, vector.Location.Y);
            Assert.AreEqual(magnitudeX, vector.Xcomponent);
            Assert.AreEqual(magnitudeY, vector.Ycomponent);
        }

        [Test]
        public static void Initialize_Vector_with_Only_Coordinates_and_Custom_Tolerance()
        {
            double x1 = 4;
            double y1 = 5;
            double x2 = 6;
            double y2 = 8;
            double magnitudeX = x2 - x1;
            double magnitudeY = y2 - y1;
            
            Vector vector = new Vector(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2),
                Tolerance);

            Assert.AreEqual(x1, vector.Location.X);
            Assert.AreEqual(y1, vector.Location.Y);
            Assert.AreEqual(magnitudeX, vector.Xcomponent);
            Assert.AreEqual(magnitudeY, vector.Ycomponent);
            Assert.AreEqual(Tolerance, vector.Tolerance);

        }
        #endregion

        #region Inherit Methods
        // https://msdn.microsoft.com/en-us/library/system.windows.vector(v=vs.110).aspx


        #endregion

        #region Methods
        [TestCase(1, 1, 1.414214)]
        [TestCase(2, 3, 3.605551)]
        [TestCase(-2, 3, 3.605551)]
        [TestCase(2, -3, 3.605551)]
        [TestCase(1.4, -3.5, 3.769615)]
        public static void Magnitude(double magnitudeX, double magnitudeY, double expectedResult)
        {
            Vector vector = new Vector(magnitudeX, magnitudeY);

            Assert.AreEqual(expectedResult, vector.Magnitude(), Tolerance);
        }

        [Test]
        public static void Magnitude_Throws_ArgumentException_for_Empty_Vector()
        {
            Vector vector = new Vector(0, 0);
            Assert.Throws<ArgumentException>(() => vector.Magnitude());
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 1, 2)]
        [TestCase(2, 3, 13)]
        [TestCase(-2, 3, 13)]
        [TestCase(2, -3, 13)]
        [TestCase(1.4, -3.5, 14.21)]
        public static void MagnitudeSquared(double magnitudeX, double magnitudeY, double expectedResult)
        {
            Vector vector = new Vector(magnitudeX, magnitudeY);

            Assert.AreEqual(expectedResult, vector.MagnitudeSquared, Tolerance);
        }

        [TestCase(1, 0, 2, 0, true)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, true)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, true)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, true)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsCollinearSameDirection(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1, Tolerance);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2, Tolerance);

            Assert.AreEqual(expectedResult, vector1.IsCollinearSameDirection(vector2));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, true)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsConcave(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1, Tolerance);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2, Tolerance);

            Assert.AreEqual(expectedResult, vector1.IsConcave(vector2));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, true)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, true)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, true)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, true)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, true)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, true)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsOrthogonal(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.IsOrthogonal(vector2));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, true)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsConvex(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1, Tolerance);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2, Tolerance);

            Assert.AreEqual(expectedResult, vector1.IsConvex(vector2));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, true)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, true)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, true)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, true)]  // Sloped Pointing Opposite Way
        public static void IsCollinearOppositeDirection(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1, Tolerance);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2, Tolerance);

            Assert.AreEqual(expectedResult, vector1.IsCollinearOppositeDirection(vector2));
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(1, 0, 0, 1, true)]
        [TestCase(-1, 0, 0, -1, true)]
        [TestCase(-1, 0, 0, 1, false)]
        [TestCase(1, 0, 0, -1, false)]
        [TestCase(0, 1, 1, 0, false)]
        [TestCase(0, -1, -1, 0, false)]
        [TestCase(0, -1, 1, 0, true)]
        [TestCase(0, 1, -1, 0, true)]
        [TestCase(1, 2, 3, 4, false)]
        [TestCase(1, 2, -3, 4, true)]
        [TestCase(1, 2, -3, -4, true)]
        [TestCase(1, 2, 3, -4, false)]
        public static void IsConcaveInside(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.IsConcaveInside(vector2));
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(1, 0, 0, 1, false)]
        [TestCase(-1, 0, 0, -1, false)]
        [TestCase(-1, 0, 0, 1, true)]
        [TestCase(1, 0, 0, -1, true)]
        [TestCase(0, 1, 1, 0, true)]
        [TestCase(0, -1, -1, 0, true)]
        [TestCase(0, -1, 1, 0, false)]
        [TestCase(0, 1, -1, 0, false)]
        [TestCase(1, 2, 3, 4, true)]
        [TestCase(1, 2, -3, 4, false)]
        [TestCase(1, 2, -3, -4, false)]
        [TestCase(1, 2, 3, -4, true)]
        public static void IsConvexInside(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.IsConvexInside(vector2));
        }

        [TestCase(1, 0, 2, 0, 1)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, 1)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, 1)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, 1)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, 0.8)]  //  Concave
        [TestCase(1, 0, 0, 1, 0)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, 0)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, 0)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, 0)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, 0)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, 0)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, -0.8)]  //  Convex
        [TestCase(1, 0, -2, 0, -1)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, -1)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, -1)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, -1)]  // Sloped Pointing Opposite Way
        public static void ConcavityCollinearity(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.ConcavityCollinearity(vector2), Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 1, 0, 0)]
        public static void ConcavityCollinearity_Throws_ArgumentException_for_Vector_without_Magnitude(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.Throws<ArgumentException>(() => vector1.ConcavityCollinearity(vector2));
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 1, 0, 1)]
        [TestCase(1, 1, 1, 1, 2)]
        [TestCase(1, 2, 3, 4, 11)]
        [TestCase(-1, -2, -3, -4, 11)]
        [TestCase(1, -2, 3, 4, -5)]
        [TestCase(1.1, -2.2, 3.3, 4.4, -6.05)]
        public static void DotProduct(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.DotProduct(vector2), Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 1, 0, 0)]
        [TestCase(1, 1, 1, 1, 0)]
        [TestCase(1, 2, 3, 4, -2)]
        [TestCase(-1, -2, -3, -4, -2)]
        [TestCase(1, -2, 3, 4, 10)]
        [TestCase(1.1, -2.2, 3.3, 4.4, 12.1)]
        public static void CrossProduct(double magnitudeX1, double magnitudeY1, double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.CrossProduct(vector2), Tolerance);
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
        public static void Angle_of_Vector_from_X_Axis(double magnitudeX, double magnitudeY, double expectedResult)
        {
            Vector vector = new Vector(magnitudeX, magnitudeY);

            Assert.AreEqual(expectedResult, vector.Angle(), Tolerance);
        }

        [TestCase(1, 0, 2, 0, 0)]
        [TestCase(0, 1, 0, 2, 0)]
        [TestCase(1, 2, 2, 4, 0)]
        [TestCase(1.1, 2.2, 2.1, 4.2, 0)]
        [TestCase(2, 1, 1, 2, 0.643501)]
        [TestCase(1, 0, 0, 1, 1.570796)]
        [TestCase(0, 1, -1, 0, 1.570796)]
        [TestCase(-1, 0, 0, -1, 1.570796)]
        [TestCase(0, -1, 1, 0, 1.570796)]
        [TestCase(1, 0, 0, -1, 1.570796)]
        [TestCase(1, 2, -2, 1, 1.570796)]
        [TestCase(-2, 1, 1, -2, 2.498092)]
        [TestCase(1, 0, -2, 0, 3.141593)]
        [TestCase(0, 1, 0, -2, 3.141593)]
        [TestCase(1, 2, -2, -4, 3.141593)]
        [TestCase(1.1, 2.2, -2.1, -4.2, 3.141593)]
        [TestCase(1, 0, -1, -1, 2.356194)]
        [TestCase(1, 0, 1, -1, 0.785398)]
        public static void Angle_Between_Vectors(
            double magnitudeX1, double magnitudeY1, 
            double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.Angle(vector2), Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 1, 0, 0)]
        public static void Angle_Throws_ArgumentException_for_Vector_without_Magnitude(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.Throws<ArgumentException>(() => vector1.Angle(vector2));
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 1, 0.5)]
        [TestCase(-1, 0, 0, -1, 0.5)]
        [TestCase(-1, 0, 0, 1, -0.5)]
        [TestCase(1, 0, 0, -1, -0.5)]
        [TestCase(0, 1, 1, 0, -0.5)]
        [TestCase(0, -1, -1, 0, -0.5)]
        [TestCase(0, -1, 1, 0, 0.5)]
        [TestCase(0, 1, -1, 0, 0.5)]
        [TestCase(1, 2, 3, 4, -1)]
        [TestCase(1, 2, -3, 4, 5)]
        [TestCase(1, 2, -3, -4, 1)]
        [TestCase(1, 2, 3, -4, -5)]
        public static void Area(double magnitudeX1, double magnitudeY1, double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1.Area(vector2));
        }


        [TestCase(0, 0, 1, 0, 1, 0)]
        [TestCase(0, 0, 1, 1, 0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, 0, 1)]
        [TestCase(0, 0, -1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, -1, 0, -1, 0)]
        [TestCase(0, 0, -1, -1, -0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 0, -1)]
        [TestCase(0, 0, 1, -1, 0.707107, -0.707107)]
        [TestCase(2, 3, 0, 0, -0.5547, -0.83205)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, 1.5, 0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, 0, 1)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, -0.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 0, -1)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, -0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, -0.5547, -0.83205)]
        public static void UnitVector(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector vector = new Vector(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            Vector unitVector = vector.UnitVector();

            Assert.AreEqual(expectedMagnitudeX, unitVector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, unitVector.Ycomponent, Tolerance);
        }

        [Test]
        public static void UnitVector_Throws_ArgumentException_for_Empty_Vector()
        {
            Vector vector = new Vector(new CartesianCoordinate(0, 0), new CartesianCoordinate(0, 0));
            Assert.Throws<ArgumentException>(() => vector.UnitVector());
        }

        [TestCase(0, 0, 1, 0, 1, 0)]
        [TestCase(0, 0, 1, 1, 0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, 0, 1)]
        [TestCase(0, 0, -1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, -1, 0, -1, 0)]
        [TestCase(0, 0, -1, -1, -0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 0, -1)]
        [TestCase(0, 0, 1, -1, 0.707107, -0.707107)]
        [TestCase(2, 3, 0, 0, -0.5547, -0.83205)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, 1.5, 0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, 0, 1)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, -0.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 0, -1)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, -0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, -0.5547, -0.83205)]
        public static void UnitTangentVector(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector vector = new Vector(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            Vector unitTangentVector = vector.UnitTangentVector();

            Assert.AreEqual(expectedMagnitudeX, unitTangentVector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, unitTangentVector.Ycomponent, Tolerance);
        }


        [Test]
        public static void UnitTangentVector_Throws_ArgumentException_for_Empty_Vector()
        {
            Vector vector = new Vector(new CartesianCoordinate(0, 0), new CartesianCoordinate(0, 0));
            Assert.Throws<ArgumentException>(() => vector.UnitTangentVector());
        }


        [TestCase(0, 0, 1, 0, 0, 1)]
        [TestCase(0, 0, 1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, -1, 0)]
        [TestCase(0, 0, -1, 1, -0.707107, -0.707107)]
        [TestCase(0, 0, -1, 0, 0, -1)]
        [TestCase(0, 0, -1, -1, 0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 1, 0)]
        [TestCase(0, 0, 1, -1, 0.707107, 0.707107)]
        [TestCase(2, 3, 0, 0, 0.83205, -0.5547)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 0, 1)]
        [TestCase(0.5, 0.5, 1.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, 0, -1)]
        [TestCase(0.5, 0.5, -0.5, -0.5, 0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, 0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, 0.83205, -0.5547)]
        public static void UnitNormalVector(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector vector = new Vector(new CartesianCoordinate(x1, y1), new CartesianCoordinate(x2, y2));
            Vector unitNormalVector = vector.UnitNormalVector();

            Assert.AreEqual(expectedMagnitudeX, unitNormalVector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, unitNormalVector.Ycomponent, Tolerance);
        }

        [Test]
        public static void UnitNormalVector_Throws_ArgumentException_for_Empty_Vector()
        {
            Vector vector = new Vector(new CartesianCoordinate(0, 0), new CartesianCoordinate(0, 0));
            Assert.Throws<ArgumentException>(() => vector.UnitNormalVector());
        }
        #endregion

        #region Methods: Static
        [TestCase(0, 0, 1, 0, 1, 0)]
        [TestCase(0, 0, 1, 1, 0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, 0, 1)]
        [TestCase(0, 0, -1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, -1, 0, -1, 0)]
        [TestCase(0, 0, -1, -1, -0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 0, -1)]
        [TestCase(0, 0, 1, -1, 0.707107, -0.707107)]
        [TestCase(2, 3, 0, 0, -0.5547, -0.83205)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, 1.5, 0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, 0, 1)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, -0.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 0, -1)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, -0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, -0.5547, -0.83205)]
        public static void UnitVector_Static(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector unitVector = Vector.UnitVector((x2-x1), (y2-y1));

            Assert.AreEqual(expectedMagnitudeX, unitVector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, unitVector.Ycomponent, Tolerance);
        }

        [TestCase(1, 0, 1, 0)]
        [TestCase(1, 1, 0.707107, 0.707107)]
        [TestCase(0, 1, 0, 1)]
        [TestCase(-1, 1, -0.707107, 0.707107)]
        [TestCase(-1, 0, -1, 0)]
        [TestCase(-1, -1, -0.707107, -0.707107)]
        [TestCase(0, -1, 0, -1)]
        [TestCase(1, -1, 0.707107, -0.707107)]
        public static void UnitVector_Static_by_Component(double magnitudeX, double magnitudeY, double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector unitVector = Vector.UnitVector(new CartesianCoordinate(), new CartesianCoordinate(magnitudeX, magnitudeY));

            Assert.AreEqual(expectedMagnitudeX, unitVector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, unitVector.Ycomponent, Tolerance);
        }

        [Test]
        public static void UnitVector_Static_Throws_ArgumentException_for_Empty_Vector()
        {
            Assert.Throws<ArgumentException>(() => Vector.UnitVector(new CartesianCoordinate(), new CartesianCoordinate()));
        }

        [TestCase(0, 0, 1, 0, 1, 0)]
        [TestCase(0, 0, 1, 1, 0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, 0, 1)]
        [TestCase(0, 0, -1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, -1, 0, -1, 0)]
        [TestCase(0, 0, -1, -1, -0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 0, -1)]
        [TestCase(0, 0, 1, -1, 0.707107, -0.707107)]
        [TestCase(2, 3, 0, 0, -0.5547, -0.83205)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, 1.5, 0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, 0, 1)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, -0.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 0, -1)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, -0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, -0.5547, -0.83205)]
        public static void UnitTangentVector_Static(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector vector = Vector.UnitTangentVector((x2-x1), (y2-y1));

            Assert.AreEqual(expectedMagnitudeX, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, vector.Ycomponent, Tolerance);
        }

        [TestCase(0, 0, 1, 0, 1, 0)]
        [TestCase(0, 0, 1, 1, 0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, 0, 1)]
        [TestCase(0, 0, -1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, -1, 0, -1, 0)]
        [TestCase(0, 0, -1, -1, -0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 0, -1)]
        [TestCase(0, 0, 1, -1, 0.707107, -0.707107)]
        [TestCase(2, 3, 0, 0, -0.5547, -0.83205)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, 1.5, 0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, 0, 1)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, -0.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 0, -1)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, -0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, -0.5547, -0.83205)]
        public static void UnitTangentVector_Static_by_Component(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector vector = Vector.UnitTangentVector(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedMagnitudeX, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, vector.Ycomponent, Tolerance);
        }


        [Test]
        public static void UnitTangentVector_Static_Throws_ArgumentException_for_Empty_Vector()
        {
            Assert.Throws<ArgumentException>(() => Vector.UnitTangentVector(new CartesianCoordinate(), new CartesianCoordinate()));
        }


        [TestCase(0, 0, 1, 0, 0, 1)]
        [TestCase(0, 0, 1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, -1, 0)]
        [TestCase(0, 0, -1, 1, -0.707107, -0.707107)]
        [TestCase(0, 0, -1, 0, 0, -1)]
        [TestCase(0, 0, -1, -1, 0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 1, 0)]
        [TestCase(0, 0, 1, -1, 0.707107, 0.707107)]
        [TestCase(2, 3, 0, 0, 0.83205, -0.5547)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 0, 1)]
        [TestCase(0.5, 0.5, 1.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, 0, -1)]
        [TestCase(0.5, 0.5, -0.5, -0.5, 0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, 0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, 0.83205, -0.5547)]
        public static void UnitNormalVector_Static(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector vector = Vector.UnitNormalVector((x2 - x1), (y2 - y1));

            Assert.AreEqual(expectedMagnitudeX, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, vector.Ycomponent, Tolerance);
        }

        [TestCase(0, 0, 1, 0, 0, 1)]
        [TestCase(0, 0, 1, 1, -0.707107, 0.707107)]
        [TestCase(0, 0, 0, 1, -1, 0)]
        [TestCase(0, 0, -1, 1, -0.707107, -0.707107)]
        [TestCase(0, 0, -1, 0, 0, -1)]
        [TestCase(0, 0, -1, -1, 0.707107, -0.707107)]
        [TestCase(0, 0, 0, -1, 1, 0)]
        [TestCase(0, 0, 1, -1, 0.707107, 0.707107)]
        [TestCase(2, 3, 0, 0, 0.83205, -0.5547)]
        [TestCase(0.5, 0.5, 1.5, 0.5, 0, 1)]
        [TestCase(0.5, 0.5, 1.5, 1.5, -0.707107, 0.707107)]
        [TestCase(0.5, 0.5, 0.5, 1.5, -1, 0)]
        [TestCase(0.5, 0.5, -0.5, 1.5, -0.707107, -0.707107)]
        [TestCase(0.5, 0.5, -0.5, 0.5, 0, -1)]
        [TestCase(0.5, 0.5, -0.5, -0.5, 0.707107, -0.707107)]
        [TestCase(0.5, 0.5, 0.5, -0.5, 1, 0)]
        [TestCase(0.5, 0.5, 1.5, -0.5, 0.707107, 0.707107)]
        [TestCase(2.5, 3.5, 0.5, 0.5, 0.83205, -0.5547)]
        public static void UnitNormalVector_Static_by_Components(
            double x1, double y1,
            double x2, double y2,
            double expectedMagnitudeX, double expectedMagnitudeY)
        {
            Vector vector = Vector.UnitNormalVector(
                new CartesianCoordinate(x1, y1),
                new CartesianCoordinate(x2, y2));

            Assert.AreEqual(expectedMagnitudeX, vector.Xcomponent, Tolerance);
            Assert.AreEqual(expectedMagnitudeY, vector.Ycomponent, Tolerance);
        }

        [Test]
        public static void UnitNormalVector_Static_Throws_ArgumentException_for_Empty_Vector()
        {
            Assert.Throws<ArgumentException>(() => Vector.UnitNormalVector(new CartesianCoordinate(), new CartesianCoordinate()));
        }

        [TestCase(1, 0, 2, 0, 0)]
        [TestCase(0, 1, 0, 2, 0)]
        [TestCase(1, 2, 2, 4, 0)]
        [TestCase(1.1, 2.2, 2.1, 4.2, 0)]
        [TestCase(2, 1, 1, 2, 0.643501)]
        [TestCase(1, 0, 0, 1, 1.570796)]
        [TestCase(0, 1, -1, 0, 1.570796)]
        [TestCase(-1, 0, 0, -1, 1.570796)]
        [TestCase(0, -1, 1, 0, 1.570796)]
        [TestCase(1, 0, 0, -1, 1.570796)]
        [TestCase(1, 2, -2, 1, 1.570796)]
        [TestCase(-2, 1, 1, -2, 2.498092)]
        [TestCase(1, 0, -2, 0, 3.141593)]
        [TestCase(0, 1, 0, -2, 3.141593)]
        [TestCase(1, 2, -2, -4, 3.141593)]
        [TestCase(1.1, 2.2, -2.1, -4.2, 3.141593)]
        [TestCase(1, 0, -1, -1, 2.356194)]
        [TestCase(1, 0, 1, -1, 0.785398)]
        public static void Angle_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.Angle(vector1, vector2), Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 1, 0, 0)]
        public static void Angle_Static_Throws_ArgumentException_for_Vector_without_Magnitude(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.Throws<ArgumentException>(() => Vector.Angle(vector1, vector2, Tolerance));
        }

        [TestCase(1, 0, 2, 0, true)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, true)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, true)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, true)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsCollinearSameDirection_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.IsCollinearSameDirection(vector1, vector2, Tolerance));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, true)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsConcave_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.IsConcave(vector1, vector2, Tolerance));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, true)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, true)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, true)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, true)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, true)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, true)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsOrthogonal_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.IsOrthogonal(vector1, vector2, Tolerance));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, true)]  //  Convex
        [TestCase(1, 0, -2, 0, false)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, false)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, false)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, false)]  // Sloped Pointing Opposite Way
        public static void IsConvex_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.IsConvex(vector1, vector2, Tolerance));
        }

        [TestCase(1, 0, 2, 0, false)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, false)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, false)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, false)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, false)]  //  Concave
        [TestCase(1, 0, 0, 1, false)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, false)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, false)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, false)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, false)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, false)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, false)]  //  Convex
        [TestCase(1, 0, -2, 0, true)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, true)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, true)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, true)]  // Sloped Pointing Opposite Way
        public static void IsCollinearOppositeDirection_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.IsCollinearOppositeDirection(vector1, vector2, Tolerance));
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(1, 0, 0, 1, true)]
        [TestCase(-1, 0, 0, -1, true)]
        [TestCase(-1, 0, 0, 1, false)]
        [TestCase(1, 0, 0, -1, false)]
        [TestCase(0, 1, 1, 0, false)]
        [TestCase(0, -1, -1, 0, false)]
        [TestCase(0, -1, 1, 0, true)]
        [TestCase(0, 1, -1, 0, true)]
        [TestCase(1, 2, 3, 4, false)]
        [TestCase(1, 2, -3, 4, true)]
        [TestCase(1, 2, -3, -4, true)]
        [TestCase(1, 2, 3, -4, false)]
        public static void IsConcaveInside_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.IsConcaveInside(vector1, vector2));
        }

        [TestCase(0, 0, 0, 0, false)]
        [TestCase(1, 0, 0, 1, false)]
        [TestCase(-1, 0, 0, -1, false)]
        [TestCase(-1, 0, 0, 1, true)]
        [TestCase(1, 0, 0, -1, true)]
        [TestCase(0, 1, 1, 0, true)]
        [TestCase(0, -1, -1, 0, true)]
        [TestCase(0, -1, 1, 0, false)]
        [TestCase(0, 1, -1, 0, false)]
        [TestCase(1, 2, 3, 4, true)]
        [TestCase(1, 2, -3, 4, false)]
        [TestCase(1, 2, -3, -4, false)]
        [TestCase(1, 2, 3, -4, true)]
        public static void IsConvexInside_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, bool expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.IsConvexInside(vector1, vector2));
        }

        [TestCase(1, 0, 2, 0, 1)]  // X Pointing Same Way
        [TestCase(0, 1, 0, 2, 1)]  // Y Pointing Same Way
        [TestCase(1, 2, 2, 4, 1)]  // Sloped Pointing Same Way
        [TestCase(1.1, 2.2, 2.1, 4.2, 1)]  // Sloped Pointing Same Way
        [TestCase(2, 1, 1, 2, 0.8)]  //  Concave
        [TestCase(1, 0, 0, 1, 0)]  // Quad1 Orthogonal
        [TestCase(0, 1, -1, 0, 0)]  // Quad2 Orthogonal
        [TestCase(-1, 0, 0, -1, 0)]  // Quad3 Orthogonal
        [TestCase(0, -1, 1, 0, 0)]  // Quad4 Orthogonal
        [TestCase(1, 0, 0, -1, 0)]  // Mirrored Axis Orthogonal
        [TestCase(1, 2, -2, 1, 0)]  // Rotated 45 deg Orthogonal
        [TestCase(-2, 1, 1, -2, -0.8)]  //  Convex
        [TestCase(1, 0, -2, 0, -1)]  // X Pointing Opposite Way
        [TestCase(0, 1, 0, -2, -1)]  // Y Pointing Opposite Way
        [TestCase(1, 2, -2, -4, -1)]  // Sloped Pointing Opposite Way
        [TestCase(1.1, 2.2, -2.1, -4.2, -1)]  // Sloped Pointing Opposite Way
        public static void ConcavityCollinearity_Static(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, Vector.ConcavityCollinearity(vector1, vector2), Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 1, 0, 0)]
        public static void ConcavityCollinearity_Static_Throws_ArgumentException_for_Vector_without_Magnitude(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.Throws<ArgumentException>(() => Vector.ConcavityCollinearity(vector1, vector2));
        }
        #endregion

        #region Operators & Equals
        [Test]
        public static void EqualsOverride_Is_True_for_Object_with_Identical_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            Vector vector1 = new Vector(x, y, tolerance);
            Vector vector2 = new Vector(x, y, tolerance);

            Assert.IsTrue(vector1.Equals(vector2));
            Assert.IsTrue(vector1.Equals((object)vector2));
            Assert.IsTrue(vector1 == vector2);
        }

        [Test]
        public static void EqualsOverride_Is_False_for_Object_with_Differing_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            Vector vector = new Vector(x, y, tolerance);
            Vector vectorDiffX = new Vector(5.2, y, tolerance);
            Assert.IsFalse(vector == vectorDiffX);

            Vector coordinateDiffY = new Vector(x, 0, tolerance);
            Assert.IsFalse(vector == coordinateDiffY);

            Vector vectorDiffT = new Vector(x, y, 0.001);
            Assert.IsTrue(vector == vectorDiffT);

            object obj = new object();
            Assert.IsFalse(vector.Equals(obj));
        }

        [Test]
        public static void NotEqualsOverride_Is_True_for_Object_with_Differing_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            Vector vector = new Vector(x, y, tolerance);
            Vector vectorDiffX = new Vector(5.2, y, tolerance);
            Assert.IsTrue(vector != vectorDiffX);
        }

        [Test]
        public static void Hashcode_Matches_for_Object_with_Identical_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            Vector vector1 = new Vector(x, y, tolerance);
            Vector vector2 = new Vector(x, y, tolerance);

            Assert.AreEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [Test]
        public static void Hashcode_Differs_for_Object_with_Differing_Coordinates()
        {
            double x = 5.3;
            double y = -2;
            double tolerance = 0.0002;
            Vector vector1 = new Vector(x, y, tolerance);

            Vector vector2 = new Vector(2 * x, y, tolerance);
            Assert.AreNotEqual(vector1.GetHashCode(), vector2.GetHashCode());

            vector2 = new Vector(x, 2 * y, tolerance);
            Assert.AreNotEqual(vector1.GetHashCode(), vector2.GetHashCode());

            vector2 = new Vector(x, y, 2 * tolerance);
            Assert.AreEqual(vector1.GetHashCode(), vector2.GetHashCode());
        }

        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4, -2, -2)]
        [TestCase(1, 2, -1, -2, 2, 4)]
        [TestCase(1, -2, -2, 4, 3, -6)]
        [TestCase(-3, 2, -2, -4, -1, 6)]
        [TestCase(-3, 2, -3, 2, 0, 0)]
        public static void SubtractOverride_Returns_Coordinate_Offset_Between_Coordinates(
            double x1, double y1, double x2, double y2, 
            double xResult, double yResult)
        {
            Vector vector1 = new Vector(x1, y1);
            Vector vector2 = new Vector(x2, y2);

            Vector vector = vector1 - vector2;
            Assert.AreEqual(xResult, vector.Xcomponent, Tolerance);
            Assert.AreEqual(yResult, vector.Ycomponent, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 2, 3, 4, 4, 6)]
        [TestCase(1, 2, -1, -2, 0, 0)]
        [TestCase(1, -2, -2, 4, -1, 2)]
        [TestCase(-1, 2, -2, -4, -3, -2)]
        [TestCase(-1, 2, 2, -4, 1, -2)]
        public static void AddOverride_Returns_Combined_Coordinates(double x1, double y1, double x2, double y2, double xResult, double yResult)
        {
            Vector vector1 = new Vector(x1, y1);
            Vector vector2 = new Vector(x2, y2);

            Vector vector3 = vector1 + vector2;
            Assert.AreEqual(xResult, vector3.Xcomponent, Tolerance);
            Assert.AreEqual(yResult, vector3.Ycomponent, Tolerance);
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
            Vector vector = new Vector(x, y);
            Vector vectorNew = vector * factor;
            Assert.AreEqual(scaledX, vectorNew.Xcomponent, Tolerance);
            Assert.AreEqual(scaledY, vectorNew.Ycomponent, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 1, 0, 1)]
        [TestCase(1, 1, 1, 1, 2)]
        [TestCase(1, 2, 3, 4, 11)]
        [TestCase(-1, -2, -3, -4, 11)]
        [TestCase(1, -2, 3, 4, -5)]
        [TestCase(1.1, -2.2, 3.3, 4.4, -6.05)]
        public static void MultiplyOverride_Multiplies_Vectors_as_Dot_Product(
            double magnitudeX1, double magnitudeY1,
            double magnitudeX2, double magnitudeY2, double expectedResult)
        {
            Vector vector1 = new Vector(magnitudeX1, magnitudeY1);
            Vector vector2 = new Vector(magnitudeX2, magnitudeY2);

            Assert.AreEqual(expectedResult, vector1 * vector2, Tolerance);
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
            Vector vector = new Vector(x, y);
            Vector vectorNew = vector / factor;
            Assert.AreEqual(scaledX, vectorNew.Xcomponent, Tolerance);
            Assert.AreEqual(scaledY, vectorNew.Ycomponent, Tolerance);
        }

        [Test]
        public static void DivideOverride_Returns_Infinity_when_Dividing_by_Zero()
        {
            Vector vector = new Vector(2, -3);
            Vector vectorNew = vector / 0;

            Assert.AreEqual(double.PositiveInfinity, vectorNew.Xcomponent);
            Assert.AreEqual(double.NegativeInfinity, vectorNew.Ycomponent);
        }
        #endregion
    }
}
