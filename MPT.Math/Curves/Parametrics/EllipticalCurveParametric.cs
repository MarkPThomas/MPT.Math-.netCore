// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="EllipticParametric.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics.ConicSectionCurveComponents;

namespace MPT.Math.Curves.Parametrics
{

    /// <summary>
    /// Represents an elliptical about a local origin curve in parametric equations defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class EllipticalCurveParametric : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurveParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public EllipticalCurveParametric(ConicSectionEllipticCurve parent)
        {
            _x = new EllipticalParametricX(parent);
            _y = new EllipticalParametricY(parent);
        }
    }
}
