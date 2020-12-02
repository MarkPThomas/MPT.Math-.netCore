// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="HyperbolicParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Hyperbolics
{

    /// <summary>
    /// Class HyperbolicParametric.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    public class HyperbolicParametric : LinearParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HyperbolicParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public HyperbolicParametric(HyperbolicCurve parent)
        {
            _x = new HyperbolicParametricX(parent);
            _y = new HyperbolicParametricY(parent);
        }
    }
}
