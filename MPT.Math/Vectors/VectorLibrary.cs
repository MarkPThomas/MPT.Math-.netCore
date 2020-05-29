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
using NMath = System.Math;
using MPT.Math.NumberTypeExtensions;
using System;

namespace MPT.Math.Vectors
{
    /// <summary>
    /// Contains static methods for common vector operations.
    /// </summary>
    public static class VectorLibrary
    {
        #region 2D Vectors

        /// <summary>
        /// Returns the dot product of the points.
        /// x1*x2 + y1*y2
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>System.Double.</returns>
        public static double DotProduct(
            double x1, double y1,
            double x2, double y2)
        {
            return (x1 * x2 + y1 * y2);
        }

        /// <summary>
        /// Returns the cross product/determinant of the points.
        /// x1*y2 - x2*y1
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
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
        /// Returns the dot product of the points.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>System.Double.</returns>
        public static double DotProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            return (x1 * x2 + y1 * y2 + z1 * z2);
        }


        /// <summary>
        /// Returns the cross product/determinant of the points.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>System.Double.</returns>
        public static double[] CrossProduct(
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
    }
}
