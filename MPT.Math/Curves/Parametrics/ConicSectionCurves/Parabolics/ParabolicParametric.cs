// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ParabolicParametric.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Parabolics
{

    /// <summary>
    /// Class ParabolicParametric.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    public class ParabolicParametric : LinearParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public ParabolicParametric(ParabolicCurve parent)
        {
            _x = new ParabolicParametricX(parent);
            _y = new ParabolicParametricY(parent);
        }
    }
}
