using MPT.Math.Geometry;
using NUnit.Framework;
using System;

namespace MPT.Math.UnitTests.Geometry
{
    [TestFixture]
    public static class GeometryLibraryTests
    {
        public static double Tolerance = 0.00001;

        #region Parametric Representations
        [TestCase(2, 0, 0)]
        [TestCase(2, 4, 2)]
        [TestCase(-2, 4, -2)]
        [TestCase(2, -4, -2)]
        [TestCase(-2, -4, 2)]

        public static void SlopeParametric(double xPrime, double yPrime, double expected)
        {
            double result = GeometryLibrary.SlopeParametric(xPrime, yPrime);
            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(0, 2)]
        public static void SlopeParametric_Throws_DivideByZeroException(double xPrime, double yPrime)
        {
            Assert.Throws<DivideByZeroException>(() => GeometryLibrary.SlopeParametric(xPrime, yPrime));
        }

        [TestCase(2, 0, 0, 0, 0)]
        [TestCase(0, 2, 0, 0, 0)]
        [TestCase(2, 4, 0, 0, 0)]
        [TestCase(2, 4, 2, 0, -0.089443)]
        [TestCase(2, 4, 0, 2, 0.044721)]
        [TestCase(0, 4, 0, 2, 0)]
        [TestCase(2, 0, 2, 0, 0)]
        [TestCase(2, 3, 4, 5, -0.042669)]
        [TestCase(-2, 3, -4, 5, 0.042669)]
        [TestCase(2, -3, 4, -5, 0.042669)]
        public static void CurvatureParametric(
            double xPrime, double yPrime,
            double xPrimeDouble, double yPrimeDouble, 
            double expected)
        {
            double result = GeometryLibrary.CurvatureParametric(xPrime, yPrime, xPrimeDouble, yPrimeDouble);
            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0, 0, 0)]
        public static void CurvatureParametric_Throws_DivideByZeroException(
            double xPrime, double yPrime,
            double xPrimeDouble, double yPrimeDouble)
        {
            Assert.Throws<DivideByZeroException>(() => GeometryLibrary.CurvatureParametric(xPrime, yPrime, xPrimeDouble, yPrimeDouble));
        }
        #endregion

        #region Graph of a Function
        [TestCase(0, 0)]
        [TestCase(2, 2)]
        [TestCase(-2, -2)]
        public static void SlopeGraph(double yPrime, double expected)
        {
            double result = GeometryLibrary.SlopeGraph(yPrime);
            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(2, 0, 0)]
        [TestCase(0, 2, 2)]
        [TestCase(2, 4, 0.357771)]
        [TestCase(-2, 4, 0.357771)]
        [TestCase(2, -4, -0.357771)]
        [TestCase(-2, -4, -0.357771)]
        public static void CurvatureGraph(double yPrime, double yPrimeDouble, double expected)
        {
            double result = GeometryLibrary.CurvatureGraph(yPrime, yPrimeDouble);
            Assert.AreEqual(expected, result, Tolerance);
        }
        #endregion

        #region Polar Coordinates
        [TestCase(0, 0, 2, 0)]
        [TestCase(0, 1, 2, 0.5)]
        [TestCase(2, 3, 4, -0.543827)]
        [TestCase(-2, 3, 4, -4.594759)]
        [TestCase(2, -3, -4, -0.543827)]
        public static void SlopePolar(double thetaRadians, double radius, double radiusPrime, double expected)
        {
            double result = GeometryLibrary.SlopePolar(thetaRadians, radius, radiusPrime);
            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(2, 0, 0)]
        [TestCase(0, 2, 0)]
        public static void SlopePolar_Throws_DivideByZeroException(double thetaRadians, double radius, double radiusPrime)
        {
            Assert.Throws<DivideByZeroException>(() => GeometryLibrary.SlopePolar(thetaRadians, radius, radiusPrime));
        }

        [TestCase(2, 0, 0, 0.5)]
        [TestCase(0, 2, 0, 1)]
        [TestCase(0, 2, 3, 1)]
        [TestCase(2, 3, 4, 0.298685)]
        [TestCase(-2, 3, 4, 0.640039)]
        [TestCase(2, -3, 4, 0.298685)]
        [TestCase(2, 3, -4, 0.640039)]
        public static void CurvaturePolar(double radius, double radiusPrime, double radiusPrimeDouble, double expected)
        {
            double result = GeometryLibrary.CurvaturePolar(radius, radiusPrime, radiusPrimeDouble);
            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0, 0)]
        [TestCase(0, 0, 2)]
        public static void CurvaturePolar_Throws_DivideByZeroException(double radius, double radiusPrime, double radiusPrimeDouble)
        {
            Assert.Throws<DivideByZeroException>(() => GeometryLibrary.CurvaturePolar(radius, radiusPrime, radiusPrimeDouble));
        }
        #endregion

        #region Implicit Curves
        [TestCase(0, 2, 0)]
        [TestCase(2, 4, -0.5)]
        [TestCase(-2, 4, 0.5)]
        [TestCase(2, -4, 0.5)]
        [TestCase(-2, -4, -0.5)]
        public static void SlopeImplicit(double Fx, double Fy, double expected)
        {
            double result = GeometryLibrary.SlopeImplicit(Fx, Fy);
            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0)]
        [TestCase(2, 0)]
        public static void SlopeImplicit_Throws_DivideByZeroException(double Fx, double Fy)
        {
            Assert.Throws<DivideByZeroException>(() => GeometryLibrary.SlopeImplicit(Fx, Fy));
        }

        [TestCase(2, 0, 0, 0, 0, 0)]
        [TestCase(0, 2, 0, 0, 0, 0)]
        [TestCase(2, 0, 4, 5, 6, 3)]
        [TestCase(0, 3, 4, 5, 6, 1.333333)]
        [TestCase(2, 3, 0, 0, 0, 0)]
        [TestCase(2, 3, 0, 5, 6, 0.768046)]
        [TestCase(2, 3, 4, 0, 6, 1.280077)]
        [TestCase(2, 3, 4, 5, 0, 0.512031)]
        [TestCase(2, 3, 4, 5, 6, 0)]
        [TestCase(2, 6, 4, 3, 2, 0.316228)]
        public static void CurvatureImplicit(
            double Fx, double Fy,
            double Fxx, double Fxy, double Fyy, 
            double expected)
        {
            double result = GeometryLibrary.CurvatureImplicit(Fx, Fy, Fxx, Fxy, Fyy);
            Assert.AreEqual(expected, result, Tolerance);
        }

        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(0, 0, 4, 5, 6)]
        public static void CurvatureImplicit_Throws_DivideByZeroException(
            double Fx, double Fy,
            double Fxx, double Fxy, double Fyy)
        {
            Assert.Throws<DivideByZeroException>(() => GeometryLibrary.CurvatureImplicit(Fx, Fy, Fxx, Fxy, Fyy));
        }
        #endregion
    }
}
