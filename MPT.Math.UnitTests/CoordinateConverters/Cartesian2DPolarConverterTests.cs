using MPT.Math.CoordinateConverters;
using MPT.Math.Coordinates;
using NUnit.Framework;

namespace MPT.Math.UnitTests.CoordinateConverters
{
    [TestFixture]
    public static class Cartesian2DPolarConverterTests
    {
        public static double Tolerance = 0.00001;

        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 0, 2, 0)]
        [TestCase(2, 2, 2.828427125, Numbers.PiOver4)]
        [TestCase(0, 2, 2, Numbers.PiOver2)]
        [TestCase(-2, 2, 2.828427125, (3d / 4) * Numbers.Pi)]
        [TestCase(-2, 0, 2, Numbers.Pi)]
        [TestCase(-2, -2, 2.828427125, -(3d / 4) * Numbers.Pi)]
        [TestCase(0, -2, 2, -Numbers.PiOver2)]
        [TestCase(2, -2, 2.828427125, -Numbers.PiOver4)]
        public static void ToPolar_Converts_Cartesian_Coordinate_to_Polar_Coordinate(double x, double y, double expectedRadius, double expectedAngleRadians)
        {
            CartesianCoordinate coordinate = new CartesianCoordinate(x, y);

            PolarCoordinate coordinateConverted = Cartesian2DPolarConverter.ToPolar(coordinate);

            Assert.AreEqual(expectedRadius, coordinateConverted.Radius, Tolerance);
            Assert.AreEqual(expectedAngleRadians, coordinateConverted.Azimuth.Radians, Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 0, 2, 0)]
        [TestCase(2.828427125, Numbers.PiOver4, 2, 2)]
        [TestCase(2, Numbers.PiOver2, 0, 2)]
        [TestCase(2.828427125, (3d / 4) * Numbers.Pi, -2, 2)]
        [TestCase(2, Numbers.Pi, -2, 0)]
        [TestCase(2.828427125, -(3d / 4) * Numbers.Pi, -2, -2)]
        [TestCase(2, -Numbers.PiOver2, 0, -2)]
        [TestCase(2.828427125, -Numbers.PiOver4, 2, -2)]
        public static void ToCartesian_Converts_Polar_Coordinate_to_Cartesian_Coordinate(double radius, double angleRadians, double expectedX, double expectedY)
        {
            PolarCoordinate coordinate = new PolarCoordinate(radius, angleRadians);

            CartesianCoordinate coordinateConverted = Cartesian2DPolarConverter.ToCartesian(coordinate);

            Assert.AreEqual(expectedX, coordinateConverted.X, Tolerance);
            Assert.AreEqual(expectedY, coordinateConverted.Y, Tolerance);
        }
    }
}
