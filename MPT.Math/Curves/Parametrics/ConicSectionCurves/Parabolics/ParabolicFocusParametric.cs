// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ParabolicFocusParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Parabolics
{
    /// <summary>
    /// Class ParabolicFocusParametric.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    public class ParabolicFocusParametric : LinearParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicFocusParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public ParabolicFocusParametric(ParabolicCurve parent)
        {
            _x = new ParabolicFocusParametricX(parent);
            _y = new ParabolicFocusParametricY(parent);
        }
    }
}
