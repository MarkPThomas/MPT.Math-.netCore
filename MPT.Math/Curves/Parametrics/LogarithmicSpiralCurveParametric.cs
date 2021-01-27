// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="LogarithmicParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics.LogarithmicSpiralCurveComponents;

namespace MPT.Math.Curves.Parametrics
{
    /// <summary>
    /// Represents a logarithmic spiral curve in parametric equations about a local origin defining x- and y-components and their respective differentials.
    /// Implements the <see cref="CartesianParametricEquationXY" />
    /// </summary>
    /// <seealso cref="CartesianParametricEquationXY" />
    public class LogarithmicSpiralCurveParametric : CartesianParametricEquationXY
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogarithmicSpiralCurveParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public LogarithmicSpiralCurveParametric(LogarithmicSpiralCurve parent)
        {
            _x = new LogarithmicSpiralParametricX(parent);
            _y = new LogarithmicSpiralParametricY(parent);
        }
    }
}
