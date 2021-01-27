// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="PolarParametricComponent.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.Components.Types
{
    /// <summary>
    /// Class for a parametric equation that returns a value in <see cref="Angle"/> coordinates.
    /// Also assigns the differential of the parametric equation, if supplied.
    /// Implements the <see cref="ParametricEquation{Angle}" />
    /// </summary>
    /// <seealso cref="ParametricEquation{Angle}" />
    public class AngularParametricComponent : ParametricEquation<Angle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AngularParametricComponent" /> class that only returns an angle of 0.
        /// </summary>
        public AngularParametricComponent() : base(Angle.Origin()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AngularParametricComponent" /> class that returns a parametric function, and possibly a differential of the function.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="functionDifferential">The differential of the function.</param>
        public AngularParametricComponent(
            ValueAtPosition<Angle> function,
            AngularParametricComponent functionDifferential = null)
            : base(function)
        {
            _differential = functionDifferential;
        }
    }
}
