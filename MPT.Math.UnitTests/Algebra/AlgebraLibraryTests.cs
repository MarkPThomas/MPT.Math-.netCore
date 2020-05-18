
using MPT.Math.NumberTypeExtensions;
using NMath = System.Math;
using NUnit.Framework;

namespace MPT.Math.Algebra.UnitTests
{
    [TestFixture]
    public class AlgebraTests
    {
        // TODO: Quadratic formula from object
        // TODO: Summation from object & strategy pattern

        // a = 0
        // operand2Sqrt < 0
        // public static double[] QuadraticFormula(double a, double b, double c)

        // B.IsNegative()
        // tSqrt.IsNegative()
        // Either of above with D.IsGreaterThanOrEqualTo(0)
        // public static double CubicCurveLowestRoot(double a, double b, double c, double d)

        // public static double[] CubicCurveRoots(double a, double b, double c, double d)

        // public static double InterpolationLinear(double value1, double value2, double point2Weight)

        // public static CartesianCoordinate InterpolationLinear(CartesianCoordinate point1, CartesianCoordinate point2, double point2Weight)


        // Check for each corner
        // Check outside of bounds
        //Check all corners have same value
        // public static double InterpolationLinear2D(CartesianCoordinate Po, CartesianCoordinate3D ii, CartesianCoordinate3D ij, CartesianCoordinate3D ji, CartesianCoordinate3D jj )

            // Horizonal line colinear
            // Horizontal line parallel offset
        // public static double IntersectionX(double y, double x1, double y1, double x2, double y2)

        // public static double IntersectionX(double y, CartesianCoordinate I, CartesianCoordinate J)

        [TestCase(0, 0, 0)]
        [TestCase(0, 0, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(3, 5.5, 12)]
        [TestCase(3, -5.5, 12)]
        public void SRSS(double value1, double value2, double value3)
        {
            double expectedResult = NMath.Sqrt(value1.Squared() + value2.Squared() + value3.Squared());
            Assert.AreEqual(expectedResult, AlgebraLibrary.SRSS(value1, value2, value3));
        }
    }    
}
