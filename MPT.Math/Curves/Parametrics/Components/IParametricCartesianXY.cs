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

namespace MPT.Math.Curves.Parametrics.Components
{
    /// <summary>
    /// Represents a set of parametric equations in x- and y-components as <see cref="double"/>, such as 2D coordinate systems for x- and y-axes.
    /// </summary>
    public interface IParametricCartesianXY<T> 
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
