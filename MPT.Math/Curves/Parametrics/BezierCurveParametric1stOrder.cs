// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="BezierParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.BezierCurveComponents;
using MPT.Math.Curves.Parametrics.Components;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Represents a 1st-order Bezier curve in parametric equations defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class BezierCurveParametric1stOrder : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BezierCurveParametric1stOrder" /> class, which represents an n = 1 parametric.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public BezierCurveParametric1stOrder(BezierCurve parent)
        {
            _x = new BezierParametric1stOrderX(parent);
            _y = new BezierParametric1stOrderY(parent);
        }
    }
}
