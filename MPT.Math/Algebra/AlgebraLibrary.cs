// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-17-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 06-05-2020
// ***********************************************************************
// <copyright file="AlgebraLibrary.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Algebra
{
    // TODO: Quadratic formula from object
    // TODO: Summation from object & strategy pattern

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
        /// <returns>System.Double[].</returns>
        /// <exception cref="ArgumentException">Argument 'a' cannot be 0</exception>
        /// <exception cref="ArgumentException">'b^2 - 4 * a * c' cannot be negative</exception>
        /// <exception cref="ArgumentException">Argument 'a' cannot be 0</exception>
        /// <exception cref="ArgumentException">'b^2 - 4 * a * c' cannot be negative</exception>
        /// <exception cref="ArgumentException">Argument 'a' cannot be 0</exception>
        /// <exception cref="ArgumentException">'b^2 - 4 * a * c' cannot be negative</exception>
        public static double[] QuadraticFormula(double a, double b, double c)
        {
            if (a.IsZeroSign()) { throw new ArgumentException("Argument 'a' cannot be 0"); }
            double denominator = 2 * a;
            double operand1 = -b / denominator;
            double operand2Sqrt = b.Squared() - 4 * a * c;
            if (operand2Sqrt.IsNegativeSign()) { throw new ArgumentException("'b^2 - 4 * a * c' cannot be negative"); }
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
        /// <returns>System.Double.</returns>
        public static double CubicCurveLowestRoot(double a, double b, double c, double d)
        {
            double a2 = b / a;
            double a1 = c / a;
            double a0 = d / a;

            double B = (9 * a1 * a2 - 27 * a0 - 2 * a2.Cubed())/27d;

            return cubicCurveLeastRootNormalized(a0, a1, a2, B);
        }

        /// <summary>
        /// Returns the least positive solution to the equation x^3 + a2x^2 + a1x + a0 = 0.
        /// </summary>
        /// <param name="a0">The a0.</param>
        /// <param name="a1">Multiplier to x^2.</param>
        /// <param name="a2">Multiplier to x.</param>
        /// <param name="B">Derived constant.</param>
        /// <returns>System.Double[].</returns>
        private static double cubicCurveLeastRootNormalized(double a0, double a1, double a2, double B)
        {
            double A = (1 / 3d) * (3 * a1 - a2.Pow(2));
            double tSqrt = B.Squared() + (4 / 27d) * A.Cubed();

            if (B.IsNegativeSign() || tSqrt.IsNegativeSign())
            {
                return Numbers.Min(cubicCurveRootsNormalized(a0, a1, a2, returnFirstRoot: true));
            }
            double t = Numbers.CubeRoot(0.5 * (-B + Numbers.Sqrt(tSqrt)));

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
        /// <returns>System.Double[].</returns>
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
        /// <param name="returnFirstRoot">if set to <c>true</c> [return first root].</param>
        /// <returns>System.Double[].</returns>
        private static double[] cubicCurveRootsNormalized(double a0, double a1, double a2, bool returnFirstRoot = false)
        {
            double Q = (3 * a1 - a2.Squared()) / 9d;
            double R = (9 * a2 * a1 - 27 * a0 - 2 * a2.Cubed()) / 54d;
            double D = Q.Cubed() + R.Squared();
            double aCosRatio = R / Numbers.Sqrt(NMath.Abs(-Q.Cubed()));
            double x1;

            if ((returnFirstRoot && D.IsGreaterThanOrEqualTo(0)) ||
                (aCosRatio.IsLessThan(-1) || aCosRatio.IsGreaterThan(1)))
            {
                double S = Numbers.CubeRoot(R + Numbers.Sqrt(D));
                double T = Numbers.CubeRoot(R - Numbers.Sqrt(D));
                x1 = (S + T) - (1 / 3d) * a2;

                return new double[] { x1 };
            }

            double theta = NMath.Acos(aCosRatio);

            x1 = 2 * Numbers.Sqrt((NMath.Abs(-Q))) * NMath.Cos(theta / 3d) - a2 / 3d;
            double x2 = 2 * Numbers.Sqrt(NMath.Abs(-Q)) * NMath.Cos((theta + 2 * Numbers.Pi) / 3d) - a2 / 3d;
            double x3 = 2 * Numbers.Sqrt(NMath.Abs(-Q)) * NMath.Cos((theta + 4 * Numbers.Pi) / 3d) - a2 / 3d;

            return new double[] { x1, x2, x3 };
        }
        #endregion

        #region Interpolations
        /// <summary>
        /// Interpolates linearly between two values.
        /// </summary>
        /// <param name="value1">Value 1.</param>
        /// <param name="value2">Value 2.</param>
        /// <param name="value2Weight">The weight applied to the difference between value 1 and value 2.
        /// 0 &lt;= weight &lt;= 1</param>
        /// <param name="tolerance">The tolerance used for determining if a weight lies within the inclusive range of 0 to 1.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Weight must be between 0 and 1. Weight provided was {value2Weight}</exception>
        public static double InterpolationLinear(double value1,
                                                 double value2,
                                                 double value2Weight, 
                                                 double tolerance = Numbers.ZeroTolerance)
        {
            if (!value2Weight.IsWithinInclusive(0, 1, tolerance))
            {
                throw new ArgumentOutOfRangeException($"Weight must be between 0 and 1. Weight provided was {value2Weight}");
            }
            return value1 + (value2 - value1) * value2Weight;
        }

        /// <summary>
        /// Interpolates linearly between two points.
        /// </summary>
        /// <param name="point1">Point 1.</param>
        /// <param name="point2">Point 2.</param>
        /// <param name="point2Weight">The weight applied to the difference between point 1 and point 2.
        /// 0 &lt;= weight &lt;= 1</param>
        /// <param name="tolerance">The tolerance used for determining if a weight lies within the inclusive range of 0 to 1.</param>
        /// <returns>CartesianCoordinate.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Weight must be between 0 and 1. Weight provided was {point2Weight}</exception>
        public static CartesianCoordinate InterpolationLinear(CartesianCoordinate point1,
                                                CartesianCoordinate point2,
                                                double point2Weight,
                                                double tolerance = Numbers.ZeroTolerance)
        {
            if (!point2Weight.IsWithinInclusive(0, 1, tolerance))
            {
                throw new ArgumentOutOfRangeException($"Weight must be between 0 and 1. Weight provided was {point2Weight}");
            }
            return point1 + (point2 - point1) * point2Weight;
        }

        /// <summary>
        /// Lineary interpolates across a 2D plane to return an interpolated third dimensional value.
        /// Expected to be used for table interpolation, where x-axis are the columns, and y-axis are the rows.
        /// </summary>
        /// <param name="Po">The point in the plane to get the corresponding magnitude of.</param>
        /// <param name="ii">Point ii (closest to the origin), where <see paramref="iiValue" /> is the corresponding value.</param>
        /// <param name="jj">Point jj (farthest from the origin), where <see paramref="jjValue" /> property is the corresponding value.</param>
        /// <param name="iiValue">The value at point ii, which is closest to the origin.</param>
        /// <param name="ijValue">The value at point ij, which is in line with point ii but farthest along the x-axis (columns).</param>
        /// <param name="jiValue">The value at point ji, which is in line with point ii but farthest along the y-axis (rows).</param>
        /// <param name="jjValue">The value at point jj, which is farthest from the origin.</param>
        /// <param name="tolerance">The tolerance used for determining if a weight lies within the boundaries of the values being interpolated.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="ArgumentException">Different columns must be chosen: Column ii = Column jj = {ii.X}</exception>
        /// <exception cref="ArgumentException">Different rows must be chosen: Row ii = Row jj = {ii.Y}</exception>
        /// <exception cref="ArgumentOutOfRangeException">Point ({Po.X}, {Po.Y}) must lie within the bounds of values to interpolate within, ({ii.X}, {ii.Y}), ({jj.X}, {jj.Y})</exception>
        /// <exception cref="ArgumentException">Different columns must be chosen: Column ii = Column jj = {ii.X}</exception>
        public static double InterpolationLinear2D(
            CartesianCoordinate Po,
            CartesianCoordinate ii,
            CartesianCoordinate jj,
            double iiValue,
            double ijValue,
            double jiValue,
            double jjValue,
            double tolerance = Numbers.ZeroTolerance)
        {
            double toleranceActual = Helper.GetTolerance(Po, Helper.GetTolerance(ii, jj, tolerance));
            if (ii.X.IsEqualTo(jj.X, toleranceActual))
            {
                throw new ArgumentException($"Different columns must be chosen: Column ii = Column jj = {ii.X}");
            }
            if (ii.Y.IsEqualTo(jj.Y, toleranceActual))
            {
                throw new ArgumentException($"Different rows must be chosen: Row ii = Row jj = {ii.Y}");
            }
            if (!Po.X.IsWithinInclusive(ii.X, jj.X, tolerance) || !Po.Y.IsWithinInclusive(ii.Y, jj.Y, tolerance))
            {
                throw new ArgumentOutOfRangeException($"Point ({Po.X}, {Po.Y}) must lie within the bounds of values to interpolate within, ({ii.X}, {ii.Y}), ({jj.X}, {jj.Y})");
            }

            double Wii = (Po.X - ii.X) * (Po.Y - ii.Y);
            double Wij = (jj.X - Po.X) * (Po.Y - ii.Y);
            double Wji = (Po.X - ii.X) * (jj.Y - Po.Y);
            double Wjj = (jj.X - Po.X) * (jj.Y - Po.Y);
            double Ao = (jj.X - ii.X) * (jj.Y - ii.Y);

            return (iiValue * Wjj + ijValue * Wji + jiValue * Wij + jjValue * Wii) / Ao;
        }


        ///// <summary>
        ///// Interpolates the polynomial.
        ///// From: https://en.wikipedia.org/wiki/Polynomial_interpolation
        ///// </summary>
        ///// <param name="points">The points.</param>
        ///// <param name="amount">The amount.</param>
        ///// <returns>System.Double.</returns>
        //public static double InterpolationPolynomial(CartesianCoordinate[] points,
        //                                             double amount)
        //{
        //    // TODO: Use Lagrange polynomials:
        //    // https://en.wikipedia.org/wiki/Polynomial_interpolation
        //    // Consider making object in order to follow curve
        //    throw new NotImplementedException();
        //}
        #endregion

        #region Intersections
        /// <summary>
        /// X-coordinate of a horizontal line intersecting the line described by the points provided.
        /// </summary>
        /// <param name="y">Y-coordinate of the horizontal line.</param>
        /// <param name="I">First point.</param>
        /// <param name="J">Second point.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        public static double IntersectionX(double y, CartesianCoordinate I, CartesianCoordinate J, double tolerance = Numbers.ZeroTolerance)
        {
            double actualTolerance = Helper.GetTolerance(I, J, tolerance);
            return IntersectionX(y, I.X, I.Y, J.X, J.Y, actualTolerance);
        }
        /// <summary>
        /// X-coordinate of a horizontal line intersecting the line described by the points provided.
        /// </summary>
        /// <param name="y">Y-coordinate of the horizontal line.</param>
        /// <param name="x1">X-coordinate of first point.</param>
        /// <param name="y1">Y-coordinate of first point.</param>
        /// <param name="x2">X-coordinate of second point.</param>
        /// <param name="y2">Y-coordinate of second point.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="ArgumentException">Identical points provided. Points need to define a line.</exception>
        /// <exception cref="ArgumentException">Line is collinear to horizontal projection.</exception>
        /// <exception cref="ArgumentException">Line is parallel to horizontal projection.</exception>
        public static double IntersectionX(double y, double x1, double y1, double x2, double y2, double tolerance = Numbers.ZeroTolerance)
        {
            if (x1.IsEqualTo(x2, tolerance) && y1.IsEqualTo(y2, tolerance))
            {
                throw new ArgumentException("Identical points provided. Points need to define a line.");
            }
            if (y1.IsEqualTo(y2, tolerance)) // Horizontal line
            {
                if (y.IsEqualTo(y1, tolerance)) // Collinear line
                {
                    throw new ArgumentException("Line is collinear to horizontal projection.");
                }
                throw new ArgumentException("Line is parallel to horizontal projection.");
            }
            return ((((y - y1) * (x2 - x1)) / (y2 - y1)) + x1);
        }
        #endregion

        #region Calcs
        /// <summary>
        /// Performs the square root of the sum of the squares of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Double.</returns>
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

        // TODO: Work out if better to call System.Math often with this library, or just encapsulate methods.
        //#region Functions from System.Math
        //public static double Abs(double a)
        //{
        //    return NMath.Abs(a);
        //}

        //public static int Abs(int a)
        //{
        //    return NMath.Abs(a);
        //}
        //#endregion
    }
}
