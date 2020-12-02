// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LinearParametricLinearBase.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Curves.Parametrics.ComponentsLinear;

namespace MPT.Math.Curves.Parametrics.Linear
{
    /// <summary>
    /// Class LinearParametricLinearBase.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.ComponentsLinear.LinearParametricComponentBase" />
    internal abstract class LinearParametricLinearBase : LinearParametricComponentBase
    {
        /// <summary>
        /// Gets the parent object whose properties are used in the associated parametric equations.
        /// </summary>
        /// <value>The parent.</value>
        protected LinearCurve _parent => _parentCurve as LinearCurve;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametricBase" /> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public LinearParametricLinearBase(LinearCurve parent) : base(parent) { }
    }
}
