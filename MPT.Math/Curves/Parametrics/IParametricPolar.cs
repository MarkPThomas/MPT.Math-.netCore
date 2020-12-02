// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-18-2020
// ***********************************************************************
// <copyright file="IParametricPolar.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Represents a parametric equation in polar coordinates.
    /// </summary>
    interface IParametricPolar<T1, T2> 
        where T1 : ParametricEquation<double>
        where T2 : ParametricEquation<Angle>
    {
        /// <summary>
        /// The radial length, r, at position s.
        /// </summary>
        /// <value>The radial length.</value>
        T1 Radius { get; }

        /// <summary>
        /// The azimuth angle, φ, at position s.
        /// </summary>
        /// <value>The azimuth.</value>
        T2 Azimuth { get; }
    }
}
