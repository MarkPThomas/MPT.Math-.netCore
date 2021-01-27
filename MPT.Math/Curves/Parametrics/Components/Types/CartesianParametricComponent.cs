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

namespace MPT.Math.Curves.Parametrics.Components.Types
{
    /// <summary>
    /// Class for a parametric equation that returns a value in <see cref="CartesianCoordinate"/> coordinates.
    /// Also assigns the differential of the parametric equation, if supplied.
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
        /// Initializes a new instance of the <see cref="CartesianParametricComponent" /> class that returns a parametric function in <see cref="CartesianCoordinate"/> coordinates, and possibly a differential of the function.
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
