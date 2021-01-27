// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-18-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-18-2020
// ***********************************************************************
// <copyright file="IParametricEquation.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.Math.Curves.Parametrics.Components
{
    /// <summary>
    /// Represents a parametric equation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IParametricEquation<T>
    {
        /// <summary>
        /// The value, at position s.
        /// </summary>
        /// <param name="s">The parametric position, s.</param>
        /// <returns>T.</returns>
        T ValueAt(double s);
    }
}
