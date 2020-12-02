// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="LogarithmicParametricBase.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.ComponentsLinear;

namespace MPT.Math.Curves.Parametrics.LogarithmicSpirals
{
    /// <summary>
    /// Class LogarithmicParametricBase.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    internal abstract class LogarithmicSpiralParametricBase : LinearParametricComponentBase
    {
        /// <summary>
        /// Gets the parent object whose properties are used in the associated parametric equations.
        /// </summary>
        /// <value>The parent.</value>
        protected LogarithmicSpiralCurve _parent => _parentCurve as LogarithmicSpiralCurve;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogarithmicSpiralParametricBase" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LogarithmicSpiralParametricBase(LogarithmicSpiralCurve parent) : base(parent) { }
    }
}
