// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-23-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-23-2020
// ***********************************************************************
// <copyright file="IntersectionAbstract.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Tools.Intersections
{
    /// <summary>
    /// Class IntersectionAbstract.
    /// Implements the <see cref="MPT.Math.Curves.Tools.Intersections.ICurveIntersection{T1, T2}" />
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// <typeparam name="T2">The type of the t2.</typeparam>
    /// <seealso cref="MPT.Math.Curves.Tools.Intersections.ICurveIntersection{T1, T2}" />
    public abstract class IntersectionAbstract<T1, T2> : ICurveIntersection<T1, T2>
        where T1 : Curve
        where T2 : Curve
    {
        #region Properties
        /// <summary>
        /// Gets the curve1.
        /// </summary>
        /// <value>The curve1.</value>
        public T1 Curve1 { get; }
        /// <summary>
        /// Gets the curve2.
        /// </summary>
        /// <value>The curve2.</value>
        public T2 Curve2 { get; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionAbstract{T1, T2}" /> class.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The second curve.</param>
        protected IntersectionAbstract(T1 curve1, T2 curve2)
        {
            Curve1 = curve1;
            Curve2 = curve2;
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool AreTangent();

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool AreIntersecting();

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <returns>CartesianCoordinate[].</returns>
        public abstract CartesianCoordinate[] IntersectionCoordinates();
        #endregion
    }
}
