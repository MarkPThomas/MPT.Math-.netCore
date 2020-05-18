using MPT.Math.Coordinates;
using MPT.Math.Coordinates3D;
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Algebra
{
    /// <summary>
    /// Contains static methods for common algebraic operations.
    /// </summary>
    public static class AlgebraLibrary
    {       
        #region Solutions to Formulas
        /// <summary>
        /// Returns the 2 x solutions to the equation ax^2 + bx + c = 0.
        /// </summary>
        /// <param name="a">Multiplier to x^2.</param>
        /// <param name="b">Multiplier to x.</param>
        /// <param name="c">Constant.</param>
        /// <returns></returns>
        public static double[] QuadraticFormula(double a, double b, double c)
        {
            if (a.IsZero()) { throw new ArgumentException("Argument 'a' cannot be 0"); }
            double denominator = 2 * a;
            double operand1 = -b / denominator;
            double operand2Sqrt = b.Squared() - 4 * a * c;
            if (operand2Sqrt.IsNegative()) { throw new ArgumentException("'b^2 - 4 * a * c' cannot be negative"); }
            double operand2 = Numbers.Sqrt(operand2Sqrt) / denominator;
           

            return operand1.PlusMinus(operand2);
        }

        /// <summary>
        /// Returns the least positive solution to the equation ax^3 + bx^2 + cx + d = 0.
        /// From: https://mathworld.wolfram.com/CubicFormula.html
        /// </summary>
        /// <param name="a">Multiplier to x^3.</param>
        /// <param name="b">Multiplier to x^2.</param>
        /// <param name="c">Multiplier to x.</param>
        /// <param name="d">Constant.</param>
        /// <returns></returns>
        public static double CubicCurveLowestRoot(double a, double b, double c, double d)
        {
            double a2 = b / a;
            double a1 = c / a;
            double a0 = d / a;

            double B = -(9 * a1 * a2 - 27 * a0 - 2 * a2.Cubed())/27d;

            return cubicCurveLeastRootNormalized(a0, a1, a2, B);
        }

        /// <summary>
        /// Returns the least positive solution to the equation x^3 + a2x^2 + a1x + a0 = 0.
        /// </summary>
        /// <param name="a0">Constant.</param>
        /// <param name="a1">Multiplier to x^2.</param>
        /// <param name="a2">Multiplier to x.</param>
        /// <param name="B">Derived constant.</param>
        /// <returns>System.Double[].</returns>
        private static double cubicCurveLeastRootNormalized(double a0, double a1, double a2, double B)
        {
            double A = (1 / 3d) * (3 * a1 - a2.Pow(2));
            double tSqrt = Numbers.Sqrt(B.Squared() + (4 / 27d) * A.Cubed());

            if (B.IsNegative() || tSqrt.IsNegative())
            {
                return cubicCurveRootsNormalized(0, a1, a2, returnLowestRoot: true)[0];
            }
            double t = Numbers.CubeRoot(0.5 * (-B + tSqrt));

            return Numbers.CubeRoot(B + t.Cubed()) - t - a2 / 3d;
        }

        /// <summary>
        /// Returns the 3 'x' solutions to the equation ax^3 + bx^2 + cx + d = 0.
        /// From: https://mathworld.wolfram.com/CubicFormula.html
        /// </summary>
        /// <param name="a">Multiplier to x^3.</param>
        /// <param name="b">Multiplier to x^2.</param>
        /// <param name="c">Multiplier to x.</param>
        /// <param name="d">Constant.</param>
        /// <returns></returns>
        public static double[] CubicCurveRoots(double a, double b, double c, double d)
        {
            double a2 = b / a;
            double a1 = c / a;
            double a0 = d / a;

            return cubicCurveRootsNormalized(a0, a1, a2);
        }

        /// <summary>
        /// Returns the 3 'x' solutions to the equation x^3 + a2*x^2 + a1*x + a0 = 0.
        /// </summary>
        /// <param name="a0">Constant.</param>
        /// <param name="a1">Multiplier to x.</param>
        /// <param name="a2">Multiplier to x^2.</param>
        /// <param name="returnLowestRoot"></param>
        /// <returns></returns>
        private static double[] cubicCurveRootsNormalized(double a0, double a1, double a2, bool returnLowestRoot = false)
        {
            double Q = (3 * a1 - a2.Squared()) / 9d;
            double R = (9 * a2 * a1 - 27 * a0 - 2 * a2.Cubed()) / 54d;
            double D = Q.Cubed() + R.Squared();
            double x1;

            if (returnLowestRoot && D.IsGreaterThanOrEqualTo(0))
            {
                double S = Numbers.CubeRoot(R + Numbers.Sqrt(D));
                double T = Numbers.CubeRoot(R - D.Pow(0.5));
                x1 = (S + T) - (1 / 3d) * a2;

                return new double[] { x1 };
            }

            double theta = NMath.Acos(R / Numbers.Sqrt(-(NMath.Abs(Q.Cubed()))));

            x1 = 2 * Numbers.Sqrt(-(NMath.Abs(Q))) * NMath.Cos(theta / 3d) - a2 / 3d;
            double x2 = 2 * Numbers.Sqrt(-(NMath.Abs(Q))) * NMath.Cos((theta + 2 * Numbers.Pi) / 3d) - a2 / 3d;
            double x3 = 2 * Numbers.Sqrt(-(NMath.Abs(Q))) * NMath.Cos((theta + 4 * Numbers.Pi) / 3d) - a2 / 3d;

            return new double[] { x1, x2, x3 };
        }
        #endregion

        #region Interpolations
        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="value1">First value.</param>
        /// <param name="value2">Second value.</param>
        /// <param name="point2Weight">Value between 0 and 1 indicating the weight of the second value.</param>
        /// <returns>Interpolated value.</returns>
        public static double InterpolationLinear(double value1,
                                                 double value2,
                                                 double point2Weight)
        {
            return value1 + (value2 - value1) * point2Weight;
        }

        /// <summary>
        /// Linearly interpolates between two values.
        /// </summary>
        /// <param name="point1">First point.</param>
        /// <param name="point2">Second point.</param>
        /// <param name="point2Weight">Value between 0 and 1 indicating the weight of the second point.</param>
        /// <returns>Interpolated value.</returns>
        public static CartesianCoordinate InterpolationLinear(CartesianCoordinate point1,
                                                CartesianCoordinate point2,
                                                double point2Weight)
        {
            return point1 + (point2 - point1) * point2Weight;
        }

        /// <summary>
        /// Interpolates the polynomial.
        /// From: https://en.wikipedia.org/wiki/Polynomial_interpolation
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static double InterpolationPolynomial(CartesianCoordinate[] points,
                                                     double amount)
        {
            // TODO: Use Lagrange polynomials:
            // https://en.wikipedia.org/wiki/Polynomial_interpolation
            // Consider making object in order to follow curve
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lineary interpolates across a 2D plane to return an interpolated third dimensional value.
        /// </summary>
        /// <param name="Po">The point in the plane to get the corresponding magnitude of.</param>
        /// <param name="ii">Point ii (closest to the origin), where <see cref="CartesianCoordinate3D.Z" /> is the corresponding magnitude.</param>
        /// <param name="ij">Point ij (farthest from the y-axis), where <see cref="CartesianCoordinate3D.Z" /> property is the corresponding magnitude.</param>
        /// <param name="ji">Point ji (farthest from the x-axis), where <see cref="CartesianCoordinate3D.Z" /> property is the corresponding magnitude.</param>
        /// <param name="jj">Point jj (farthest from the origin), where <see cref="CartesianCoordinate3D.Z" /> property is the corresponding magnitude.</param>
        /// <returns>System.Double.</returns>
        public static double InterpolationLinear2D(
            CartesianCoordinate Po,
            CartesianCoordinate3D ii,
            CartesianCoordinate3D ij,
            CartesianCoordinate3D ji,
            CartesianCoordinate3D jj
            )
        {
            double Wii = (Po.X - ii.X) * (Po.Y - ii.Y);
            double Wij = (Po.X - ij.X) * (ij.Y - Po.Y);
            double Wji = (ji.X - Po.X) * (Po.Y - ii.Y);
            double Wjj = (ji.X - Po.X) * (ii.Y - Po.Y);
            double Ao = (ij.X - ii.X) * (ji.Y - ii.Y);

            return (ii.Z * Wjj + ij.Z * Wji + ji.Z * Wij + jj.Z * Wii) / Ao;
        }

        #endregion

        #region Intersections
        /// <summary>
        /// X-coordinate of a horizontal line intersecting the line described by the points provided.
        /// </summary>
        /// <param name="y">Y-coordinate of the horizontal line.</param>
        /// <param name="I">First point.</param>
        /// <param name="J">Second point.</param>
        /// <returns></returns>
        public static double IntersectionX(double y, CartesianCoordinate I, CartesianCoordinate J)
        {
            return IntersectionX(y, I.X, I.Y, J.X, J.Y);
        }
        /// <summary>
        /// X-coordinate of a horizontal line intersecting the line described by the points provided.
        /// </summary>
        /// <param name="y">Y-coordinate of the horizontal line.</param>
        /// <param name="x1">X-coordinate of first point.</param>
        /// <param name="y1">Y-coordinate of first point.</param>
        /// <param name="x2">X-coordinate of second point.</param>
        /// <param name="y2">Y-coordinate of second point.</param>
        /// <returns></returns>
        public static double IntersectionX(double y, double x1, double y1, double x2, double y2)
        {
            return ((((y - y1) * (x2 - x1)) / (y2 - y1)) + x1);
        }
        #endregion

        #region Calcs
        /// <summary>
        /// Performs the square root of the sum of the squares of the provided values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double SRSS(params double[] values)
        {
            double sumOfSquares = 0;
            foreach (double value in values)
            {
                sumOfSquares += value.Squared();
            }

            return NMath.Sqrt(sumOfSquares);
        }
        #endregion
    }
}
