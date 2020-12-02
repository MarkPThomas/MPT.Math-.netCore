// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-18-2020
// ***********************************************************************
// <copyright file="IParametricCartesian3D.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics
{

    /// <summary>
    /// Represents a parametric equation in 3D cartesian coordinates.
    /// </summary>
    public interface IParametricLinear3D<T> : IParametricLinear<T> where T : ParametricEquation<double>
    {
        /// <summary>
        /// The z-component, at position s.
        /// </summary>
        /// <value>The zcomponent.</value>
        T Zcomponent { get; }
    }
}
