// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ConicRadialParametricBase.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.ComponentsAngular;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Class ConicRadialParametricBase.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ComponentsAngular.AngularParametricComponentBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ComponentsAngular.AngularParametricComponentBase" />
    internal abstract class ConicAngularParametricBase : AngularParametricComponentBase
    {
        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>The parent.</value>
        protected ConicSectionCurve _parent => _parentCurve as ConicSectionCurve;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConicAngularParametricBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ConicAngularParametricBase(ConicSectionCurve parent) : base(parent) { }
    }
}
