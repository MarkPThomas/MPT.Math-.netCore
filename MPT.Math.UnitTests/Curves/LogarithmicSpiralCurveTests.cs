using MPT.Math.Coordinates;
using NUnit.Framework;
using System;
using MPT.Math.Curves;
using MPT.Math.Vectors;

namespace MPT.Math.UnitTests.Curves
{
    [TestFixture]
    public static class LogarithmicSpiralCurveTests
    {
        public static double Tolerance = 0.00001;

        public static CircularCurve curve;

        [SetUp]
        public static void SetUp()
        {
            //CartesianCoordinate center = new CartesianCoordinate(4, 5);
            //double radius = 6;
            //curve = new CircularCurve(radius, center);
        }
    }
}
