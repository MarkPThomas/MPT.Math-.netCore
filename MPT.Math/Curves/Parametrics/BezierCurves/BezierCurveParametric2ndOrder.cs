// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="BezierParametric2.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.Math.Curves.Parametrics.Components;

namespace MPT.Math.Curves.Parametrics.BezierCurves
{
    /// <summary>
    /// Represents a 2nd-order Bezier curve in parametric equations defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class BezierCurveParametric2ndOrder : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BezierCurveParametric2ndOrder" /> class, which represents an n = 2 parametric.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public BezierCurveParametric2ndOrder(BezierCurve parent)
        {
            _x = new BezierParametric2ndOrderX(parent);
            _y = new BezierParametric2ndOrderY(parent);
        }
    }
}
