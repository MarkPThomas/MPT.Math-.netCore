// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 12-09-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 12-09-2020
// ***********************************************************************
// <copyright file="IPerpendicularProjections.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using MPT.Math.Coordinates;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Interface IPerpendicularProjections
    /// </summary>
    public interface IPerpendicularProjections
    {
        /// <summary>
        /// Coordinate of where a perpendicular projection from a curve tangent intersects the provided coordinate.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>CartesianCoordinate.</returns>
        CartesianCoordinate CoordinateOfPerpendicularProjection(CartesianCoordinate point);
    }
}
