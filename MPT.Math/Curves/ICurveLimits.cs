// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="ICurveSingular.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Interface for a curve that is comprised of a singular path.
    /// </summary>
    public interface ICurveLimits
    {
        #region Methods: Properties Derived with Limits   
        /// <summary>
        /// The limit where the curve starts.
        /// </summary>
        /// <value>The limit start.</value>
        CartesianCoordinate LimitStart { get; }

        /// <summary>
        /// The limit where the curve ends.
        /// </summary>
        /// <value>The limit end.</value>
        CartesianCoordinate LimitEnd { get; }

        /// <summary>
        /// Length of the curve.
        /// </summary>
        /// <returns>System.Double.</returns>
        double Length();
        #endregion
    }
}
