// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ParabolicParametric.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using MPT.Math.Curves.Parametrics.Components;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{

    /// <summary>
    /// Represents a parabolic curve in parametric equations about a local origin defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class ParabolicCurveParametric : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicCurveParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public ParabolicCurveParametric(ParabolicCurve parent)
        {
            _x = new ParabolicParametricX(parent);
            _y = new ParabolicParametricY(parent);
        }
    }
}
