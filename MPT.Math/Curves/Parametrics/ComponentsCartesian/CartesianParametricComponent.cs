// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="CartesianParametricComponent.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics.ComponentsLinear;

namespace MPT.Math.Curves.Parametrics.ComponentsCartesian
{
    /// <summary>
    /// Class CartesianParametricComponent.
    /// Implements the <see cref="ParametricEquation{CartesianCoordinate}" />
    /// </summary>
    /// <seealso cref="ParametricEquation{CartesianCoordinate}" />
    public class CartesianParametricComponent : ParametricEquation<CartesianCoordinate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianParametricComponent" /> class that only returns 0.
        /// </summary>
        public CartesianParametricComponent() : base(CartesianCoordinate.Origin()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianParametricComponent" /> class that returns a parametric function, and possibly a differential of the function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="functionDifferential">The differential of the function.</param>
        public CartesianParametricComponent(
            ValueAtPosition<CartesianCoordinate> function,
            CartesianParametricComponent functionDifferential = null)
            : base(function)
        {
            _differential = functionDifferential;
        }
    }
}
