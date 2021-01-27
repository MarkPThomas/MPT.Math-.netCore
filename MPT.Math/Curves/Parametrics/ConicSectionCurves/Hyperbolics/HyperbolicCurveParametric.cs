// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="HyperbolicParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using MPT.Math.Curves.Parametrics.Components;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Hyperbolics
{

    /// <summary>
    /// Represents a hyperbolic curve in parametric equations about a local origin defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class HyperbolicCurveParametric : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicCurveParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public HyperbolicCurveParametric(HyperbolicCurve parent)
        {
            _x = new HyperbolicParametricX(parent);
            _y = new HyperbolicParametricY(parent);
        }
    }
}
