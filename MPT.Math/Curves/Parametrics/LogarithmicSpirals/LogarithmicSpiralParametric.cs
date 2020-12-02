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
namespace MPT.Math.Curves.Parametrics.LogarithmicSpirals
{
    /// <summary>
    /// Class LogarithmicParametric.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    public class LogarithmicSpiralParametric : LinearParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogarithmicSpiralParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public LogarithmicSpiralParametric(LogarithmicSpiralCurve parent)
        {
            _x = new LogarithmicSpiralParametricX(parent);
            _y = new LogarithmicSpiralParametricY(parent);
        }
    }
}
