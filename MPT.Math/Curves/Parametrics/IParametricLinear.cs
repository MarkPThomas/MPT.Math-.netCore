// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-18-2020
// ***********************************************************************
// <copyright file="IParametricCartesian.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Represents a parametric equation in 2D cartesian coordinates.
    /// </summary>
    public interface IParametricLinear<T> 
        where T : ParametricEquation<double>
    {
        /// <summary>
        /// The x-component, at position s.
        /// </summary>
        /// <value>The xcomponent.</value>
        T Xcomponent { get; }

        /// <summary>
        /// The y-component, at position s.
        /// </summary>
        /// <value>The ycomponent.</value>
        T Ycomponent { get; }
    }
}
