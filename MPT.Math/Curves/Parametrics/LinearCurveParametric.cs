// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LinearParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics.LinearCurveComponents;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Represents a linear curve in parametric equations defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class LinearCurveParametric : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearCurveParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public LinearCurveParametric(LinearCurve parent)
        {
            _x = new LinearParametricX(parent);
            _y = new LinearParametricY(parent);
        }
    }
}
