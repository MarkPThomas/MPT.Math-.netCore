// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ParabolicFocusParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.Math.Curves.Parametrics.Components;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Represents a parabolic curve in parametric equations about the focus defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class ParabolicCurveFocusParametric : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicCurveFocusParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public ParabolicCurveFocusParametric(ParabolicCurve parent)
        {
            _x = new ParabolicFocusParametricX(parent);
            _y = new ParabolicFocusParametricY(parent);
        }
    }
}
