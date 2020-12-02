// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="CurveParametricComponent.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.ComponentsLinear
{
    /// <summary>
    /// Class HyperbolicParametricComponent.
    /// Implements the <see cref="ParametricEquation{Double}" />
    /// </summary>
    /// <seealso cref="ParametricEquation{Double}" />
    public class LinearParametricComponent : ParametricEquation<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametricComponent" /> class that only returns 0.
        /// </summary>
        public LinearParametricComponent() : base(0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametricComponent" /> class that returns a parametric function, and possibly a differential of the function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="functionDifferential">The differential of the function.</param>
        public LinearParametricComponent(
            ValueAtPosition<double> function,
            LinearParametricComponent functionDifferential = null)
            : base(function)
        {
            _differential = functionDifferential;
        }
    }
}
