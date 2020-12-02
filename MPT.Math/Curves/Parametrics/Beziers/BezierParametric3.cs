﻿// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-21-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-21-2020
// ***********************************************************************
// <copyright file="BezierParametric3.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.Beziers
{
    /// <summary>
    /// Class BezierParametric3.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    public class BezierParametric3 : LinearParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BezierParametric3" /> class, which represents an n = 1 parametric.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public BezierParametric3(BezierCurve parent)
        {
            _x = new BezierParametricX3(parent);
            _y = new BezierParametricY3(parent);
        }
    }
}
