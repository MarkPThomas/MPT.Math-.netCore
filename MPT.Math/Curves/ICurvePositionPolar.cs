// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 12-09-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="ICurvePositionInPolar.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.Math.Coordinates;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Curve position can be represented in polar coordinates.
    /// </summary>
    public interface ICurvePositionPolar
    {
        /// <summary>
        /// The radius measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        double RadiusAboutOrigin(double angleRadians);

        /// <summary>
        /// The radii measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        double[] RadiiAboutOrigin(double angleRadians);

        // TODO: Implement additional methods for ICurvePositionPolar
        ///// <summary>
        ///// The rotation about the local coordinate ray origin as a function of the provided radius measured from the local coordinate origin.
        ///// </summary>
        ///// <param name="radius">The radius, measured from the local coordinate origin.</param>
        ///// <returns>Angle.</returns>
        //Angle RotationAtRadius(double radius);

        ///// <summary>
        ///// The rotations about the local coordinate ray origin as a function of the provided radius measured from the local coordinate origin.
        ///// </summary>
        ///// <param name="radius">The radius, measured from the local coordinate origin.</param>
        ///// <returns>Angle.</returns>
        //Angle[] RotationsAtRadius(double radius);

        ///// <summary>
        ///// Provided point lies on the curve.
        ///// </summary>
        ///// <param name="coordinate">The coordinate.</param>
        ///// <returns><c>true</c> if [is intersecting coordinate] [the specified coordinate]; otherwise, <c>false</c>.</returns>
        //bool IsIntersectingPolarCoordinate(PolarCoordinate coordinate);
    }
}
