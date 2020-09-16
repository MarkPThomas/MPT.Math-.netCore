// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 05-26-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="ITolerance.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.Math
{
    /// <summary>
    /// Interface for objects that use a tolerance for floating-point numerical operations.
    /// </summary>
    public interface ITolerance
    {
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        double Tolerance { get; set; }
    }
}
