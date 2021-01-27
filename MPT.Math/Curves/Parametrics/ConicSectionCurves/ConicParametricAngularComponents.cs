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
using MPT.Math.Curves.Parametrics.Components.Types;
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Class containing the assigned components of a parametric equation in <see cref="Angle"/> coordinates and the corresponding curve object for conic sections.
    /// This class has the basic components of differentiating and accessing the different parametric equations.
    /// Also contains operators to implement scaling of the parametric equations.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.Components.Types.AngularParametricComponents" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.Components.Types.AngularParametricComponents" />
    internal abstract class ConicParametricAngularComponents : AngularParametricComponents
    {
        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>The parent.</value>
        protected ConicSectionCurve _parent => _parentCurve as ConicSectionCurve;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConicParametricAngularComponents"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ConicParametricAngularComponents(ConicSectionCurve parent) : base(parent) { }
    }
}
