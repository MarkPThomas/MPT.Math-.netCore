// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-18-2020
// ***********************************************************************
// <copyright file="IParametricSpherical.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.Components
{
    /// <summary>
    /// Represents a parametric equation in spherical coordinates.
    /// </summary>
    public interface IParametricSpherical
    {
        /// <summary>
        /// The radial length, r, at position s.
        /// </summary>
        /// <value>The radius.</value>
        ParametricEquation<double> Radius { get; }

        /// <summary>
        /// The inclination angle, θ, at position s, which lies in the vertical plane sweeping out from the Z-axis.
        /// </summary>
        /// <value>The inclination.</value>
        ParametricEquation<Angle> Inclination { get; }

        /// <summary>
        /// The azimuth angle, φ, at position s, which lies in the x-y plane sweeping out from the X-axis.
        /// </summary>
        /// <value>The azimuth.</value>
        ParametricEquation<Angle> Azimuth { get; }
    }
}
