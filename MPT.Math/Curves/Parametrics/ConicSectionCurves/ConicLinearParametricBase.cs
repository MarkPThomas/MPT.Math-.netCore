// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ConicParametricBase.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.Math.Curves.Parametrics.ComponentsLinear;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Class ConicParametricBase.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    internal abstract class ConicLinearParametricBase : LinearParametricComponentBase
    {
        /// <summary>
        /// Gets the parent object whose properties are used in the associated parametric equations.
        /// </summary>
        /// <value>The parent.</value>
        protected ConicSectionCurve _parent => _parentCurve as ConicSectionCurve;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConicLinearParametricBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ConicLinearParametricBase(ConicSectionCurve parent) : base(parent) { }
    }
}
