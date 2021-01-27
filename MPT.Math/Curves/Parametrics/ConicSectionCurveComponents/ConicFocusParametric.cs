// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="RadiusFocusParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics.Components;

namespace MPT.Math.Curves.Parametrics.ConicSectionCurveComponents
{
    /// <summary>
    /// Represents a set of parametric equations in radial <see cref="double"/> and rotational <see cref="Angle"/> components to describe curve positions in relation to the conic focus.
    /// These equations can be scaled and differentiated.
    /// Implements the <see cref="PolarParametricEquation" />
    /// </summary>
    /// <seealso cref="PolarParametricEquation" />
    public class ConicFocusParametric : PolarParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConicFocusParametric"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ConicFocusParametric(ConicSectionCurve parent)
        {
            _radius = new ConicFocusParametricDoubleComponents(parent);
            _azimuth = new ConicFocusParametricAngularComponents(parent);
        }
    }
}
