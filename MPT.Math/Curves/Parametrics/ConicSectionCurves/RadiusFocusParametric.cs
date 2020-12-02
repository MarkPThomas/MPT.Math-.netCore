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

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves
{
    /// <summary>
    /// Class RadiusFocusParametric.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.PolarParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.PolarParametricEquation" />
    public class RadiusFocusParametric : PolarParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadiusFocusParametric"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public RadiusFocusParametric(ConicSectionCurve parent)
        {
            _radius = new RadiusFocusParametricLength(parent);
            _azimuth = new RadiusFocusParametricRotation(parent);
        }
    }
}
