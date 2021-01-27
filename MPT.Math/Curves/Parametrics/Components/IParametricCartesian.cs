// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="IParametricCartesian.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.Components
{
    /// <summary>
    /// Represents a parametric equation in a single <see cref="CartesianCoordinate"/> component, such as 2D coordinate systems for x- and y-axes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParametricCartesian<T>
           where T : ParametricEquation<CartesianCoordinate>
    {
        /// <summary>
        /// The component, at position s.
        /// </summary>
        /// <value>The xcomponent.</value>
        T Component { get; }
    }
}
