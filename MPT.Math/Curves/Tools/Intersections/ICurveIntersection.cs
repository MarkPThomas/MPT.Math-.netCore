// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="ICurveIntersection.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
namespace MPT.Math.Curves.Tools.Intersections
{
    /// <summary>
    /// Interface ICurveIntersection
    /// </summary>
    /// <typeparam name="T1">The type of curve 1.</typeparam>
    /// <typeparam name="T2">The type of curve 2.</typeparam>
    public interface ICurveIntersection<T1, T2>
        where T1 : ICurve
        where T2 : ICurve
    {
        /// <summary>
        /// Gets the curve1.
        /// </summary>
        /// <value>The curve1.</value>
        T1 Curve1 { get; }
        /// <summary>
        /// Gets the curve2.
        /// </summary>
        /// <value>The curve2.</value>
        T2 Curve2 { get; }

        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AreTangent();

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AreIntersecting();

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <returns>CartesianCoordinate[].</returns>
        CartesianCoordinate[] IntersectionCoordinates();
    }
}
