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

using MPT.Math.Curves.Parametrics.Components.Types;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Class containing the assigned components of a parametric equation in <see cref="double"/> coordinates and the corresponding curve object for conic sections.
    /// This class has the basic components of differentiating and accessing the different parametric equations.
    /// Also contains operators to implement scaling of the parametric equations.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.Components.Types.DoubleParametricComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.Components.Types.DoubleParametricComponents" />
    internal abstract class ConicParametricDoubleComponents : DoubleParametricComponents
    {
        /// <summary>
        /// Gets the parent object whose properties are used in the associated parametric equations.
        /// </summary>
        /// <value>The parent.</value>
        protected ConicSectionCurve _parent => _parentCurve as ConicSectionCurve;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConicParametricDoubleComponents"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ConicParametricDoubleComponents(ConicSectionCurve parent) : base(parent) { }
    }
}
