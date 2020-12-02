// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-19-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="EllipticParametric.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.ConicSectionCurves.Elliptics
{

    /// <summary>
    /// Class EllipticParametric.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    public class EllipticParametric : LinearParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public EllipticParametric(ConicSectionEllipticCurve parent)
        {
            _x = new EllipticParametricX(parent);
            _y = new EllipticParametricY(parent);
        }
    }
}
