// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="BezierParametricBase.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.ComponentsCartesian;

namespace MPT.Math.Curves.Parametrics.Beziers
{
    /// <summary>
    /// Class BezierParametricBase.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    internal abstract class BezierParametricBase : CartesianParametricComponentBase
    {
        /// <summary>
        /// Gets the parent object whose properties are used in the associated parametric equations.
        /// </summary>
        /// <value>The parent.</value>
        protected BezierCurve _parent => _parentCurve as BezierCurve;

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierParametricBase" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public BezierParametricBase(BezierCurve parent) : base(parent) { }
    }
}
