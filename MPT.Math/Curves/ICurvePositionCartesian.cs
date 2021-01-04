// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 12-09-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="ICurvePositionInCartesian.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.Math.Coordinates;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Curve position can be represented in cartesian coordinates.
    /// </summary>
    public interface ICurvePositionCartesian
    {
        /// <summary>
        /// X-coordinate on the curve that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        double XatY(double y);

        /// <summary>
        /// Y-coordinate on the curve that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        double YatX(double x);

        /// <summary>
        /// X-coordinates on the curve that correspond to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which x-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        double[] XsAtY(double y);

        /// <summary>
        /// Y-coordinates on the curve that correspond to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which y-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        double[] YsAtX(double x);

        /// <summary>
        /// Provided point lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if [is intersecting coordinate] [the specified coordinate]; otherwise, <c>false</c>.</returns>
        bool IsIntersectingCoordinate(CartesianCoordinate coordinate);
    }
}
