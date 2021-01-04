// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="VectorLibrary.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.NumberTypeExtensions;
using System;
using MPT.Math.Algebra;

namespace MPT.Math.Vectors
{
    /// <summary>
    /// Contains static methods for common vector operations.
    /// </summary>
    public static class VectorLibrary
    {
        #region 2D Vectors    
        /// <summary>
        /// Gets the magnitude from parametric vector components.
        /// </summary>
        /// <param name="xComponent">The x component.</param>
        /// <param name="yComponent">The y component.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="Exception">Ill-formed vector. Vector magnitude cannot be zero.</exception>
        public static double Magnitude(
            double xComponent, double yComponent, 
            double tolerance = Numbers.ZeroTolerance)
        {
            double magnitude = AlgebraLibrary.SRSS(xComponent, yComponent);
            return validatedMagnitude(magnitude, tolerance);
        }

        /// <summary>
        /// Returns the dot product of parametric vector components.
        /// x1*x2 + y1*y2
        /// </summary>
        /// <param name="x1">The x component of the first vector.</param>
        /// <param name="y1">The y component of the first vector.</param>
        /// <param name="x2">The x component of the second vector.</param>
        /// <param name="y2">The y component of the second vector.</param>
        /// <returns>System.Double.</returns>
        public static double DotProduct(
            double x1, double y1,
            double x2, double y2)
        {
            return (x1 * x2 + y1 * y2);
        }

        /// <summary>
        /// Returns the cross product/determinant of parametric vector components.
        /// x1*y2 - x2*y1
        /// </summary>
        /// <param name="x1">The x component of the first vector.</param>
        /// <param name="y1">The y component of the first vector.</param>
        /// <param name="x2">The x component of the second vector.</param>
        /// <param name="y2">The y component of the second vector.</param>
        /// <returns>System.Double.</returns>
        public static double CrossProduct(
            double x1, double y1,
            double x2, double y2)
        {
            return ((x1 * y2) - (y1 * x2));
        }
        #endregion

        #region 3D Vectors
        /// <summary>
        /// Gets the magnitude from parametric vector components.
        /// </summary>
        /// <param name="xComponent">The x component.</param>
        /// <param name="yComponent">The y component.</param>
        /// <param name="zComponent">The z component.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="Exception">Ill-formed vector. Vector magnitude cannot be zero.</exception>
        public static double Magnitude3D(
            double xComponent, double yComponent, double zComponent,
            double tolerance = Numbers.ZeroTolerance)
        {
            double magnitude = AlgebraLibrary.SRSS(xComponent, yComponent, zComponent);
            return validatedMagnitude(magnitude, tolerance);
        }

        /// <summary>
        /// Returns the dot product of the points.
        /// </summary>
        /// <param name="x1">The x component of the first vector.</param>
        /// <param name="y1">The y component of the first vector.</param>
        /// <param name="z1">The z component of the first vector.</param>
        /// <param name="x2">The x component of the second vector.</param>
        /// <param name="y2">The y component of the second vector.</param>
        /// <param name="z2">The z component of the second vector.</param>
        /// <returns>System.Double.</returns>
        public static double DotProduct3D(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            return (x1 * x2 + y1 * y2 + z1 * z2);
        }


        /// <summary>
        /// Returns the cross product/determinant of the points.
        /// </summary>
        /// <param name="x1">The x component of the first vector.</param>
        /// <param name="y1">The y component of the first vector.</param>
        /// <param name="z1">The z component of the first vector.</param>
        /// <param name="x2">The x component of the second vector.</param>
        /// <param name="y2">The y component of the second vector.</param>
        /// <param name="z2">The z component of the second vector.</param>
        /// <returns>System.Double.</returns>
        public static double[] CrossProduct3D(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            double x = (y1 * z2) - (z1 * y2);
            double y = (z1 * x2) - (x1 * z2);
            double z = (x1 * y2) - (y1 * x2);

            double[] matrix = { x, y, z };

            return matrix;
        }
        #endregion

        #region Private
        /// <summary>
        /// Validateds the magnitude.
        /// </summary>
        /// <param name="magnitude">The magnitude.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="Exception">Ill-formed vector. Vector magnitude cannot be zero.</exception>
        private static double validatedMagnitude(double magnitude, double tolerance = Numbers.ZeroTolerance)
        {
            if (magnitude.IsZeroSign(tolerance)) { throw new ArgumentException("Ill-formed vector. Vector magnitude cannot be zero."); }
            return magnitude;
        }
        #endregion
    }
}
