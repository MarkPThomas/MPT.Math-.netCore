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

namespace MPT.Math.Curves.Parametrics.Components.Types
{
    /// <summary>
    /// Class for a parametric equation that returns a value in <see cref="double"/> coordinates.
    /// Also assigns the differential of the parametric equation, if supplied.
    /// Implements the <see cref="ParametricEquation{Double}" />
    /// </summary>
    /// <seealso cref="ParametricEquation{Double}" />
    public class DoubleParametricComponent : ParametricEquation<double>
    {
        /// <summary>
        ///  class that returns a parametric function in <see cref="double"/> coordinates, and possibly a differential of the function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="functionDifferential">The differential of the function.</param>
        internal DoubleParametricComponent(
            ValueAtPosition<double> function,
            DoubleParametricComponent functionDifferential = null)
            : base(function)
        {
            _differential = functionDifferential;
        }
    }
}
