// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-17-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="IntersectionLinearElliptical.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Tools.Intersections
{
    /// <summary>
    /// Class IntersectionLinearElliptical.
    /// Implements the <see cref="ICurveIntersection{LinearCurve, EllipticalCurve}" />
    /// </summary>
    /// <seealso cref="ICurveIntersection{LinearCurve, EllipticalCurve}" />
    public class IntersectionLinearElliptical : IntersectionAbstract<LinearCurve, EllipticalCurve>
    {
        #region Properties
        /// <summary>
        /// Gets the linear curve.
        /// </summary>
        /// <value>The linear curve.</value>
        public LinearCurve LinearCurve => Curve1;
        /// <summary>
        /// Gets the elliptical curve.
        /// </summary>
        /// <value>The elliptical curve.</value>
        public EllipticalCurve EllipticalCurve => Curve2;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearElliptical"/> class.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="ellipticalCurve">The elliptical curve.</param>
        public IntersectionLinearElliptical(LinearCurve linearCurve, EllipticalCurve ellipticalCurve) : base(linearCurve, ellipticalCurve)
        {

        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreTangent()
        {
            return AreTangent(LinearCurve, EllipticalCurve);
        }

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreIntersecting()
        {
            return AreIntersecting(LinearCurve, EllipticalCurve);
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <returns>CartesianCoordinate[].</returns>
        public override CartesianCoordinate[] IntersectionCoordinates()
        {
            return IntersectionCoordinates(LinearCurve, EllipticalCurve);
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="ellipticalCurve">The elliptical curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreTangent(LinearCurve linearCurve, EllipticalCurve ellipticalCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="ellipticalCurve">The elliptical curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(LinearCurve linearCurve, EllipticalCurve ellipticalCurve)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="ellipticalCurve">The elliptical curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(LinearCurve linearCurve, EllipticalCurve ellipticalCurve)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
