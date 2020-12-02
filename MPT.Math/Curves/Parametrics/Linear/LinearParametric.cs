// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-22-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LinearParametric.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


namespace MPT.Math.Curves.Parametrics.Linear
{
    /// <summary>
    /// Class LinearParametric.
    /// Implements the <see cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Parametrics.LinearParametricEquation" />
    public class LinearParametric : LinearParametricEquation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinearParametric" /> class.
        /// </summary>
        /// <param name="parent">The parent object whose properties are used in the associated parametric equations.</param>
        public LinearParametric(LinearCurve parent)
        {
            _x = new LinearParametricX(parent);
            _y = new LinearParametricY(parent);
        }
    }
}
