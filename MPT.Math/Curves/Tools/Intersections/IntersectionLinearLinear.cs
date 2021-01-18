// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="IntersectionLinearLinear.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;

namespace MPT.Math.Curves.Tools.Intersections
{
    /// <summary>
    /// Class IntersectionLinearLinear.
    /// Implements the <see cref="ICurveIntersection{LinearCurve, LinearCurve}" />
    /// </summary>
    /// <seealso cref="ICurveIntersection{LinearCurve, LinearCurve}" />
    public class IntersectionLinearLinear : IntersectionAbstract<LinearCurve, LinearCurve>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearLinear"/> class.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The second curve.</param>
        public IntersectionLinearLinear(LinearCurve curve1, LinearCurve curve2) : base(curve1, curve2)
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
            return AreTangent(Curve1, Curve2);
        }

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreIntersecting()
        {
            return AreIntersecting(Curve1, Curve2);
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <returns>CartesianCoordinate[].</returns>
        public override CartesianCoordinate[] IntersectionCoordinates()
        {
            return IntersectionCoordinates(Curve1, Curve2);
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreTangent(LinearCurve curve1, LinearCurve curve2)
        {
            double tolerance = Generics.GetTolerance(curve1, curve2);
            bool yInterceptsMatch = Numbers.AreEqual(curve1.InterceptY(), curve2.InterceptY(), tolerance) && !curve1.IsVertical();
            bool xInterceptsMatch = Numbers.AreEqual(curve1.InterceptX(), curve2.InterceptX(), tolerance) && !curve1.IsHorizontal();
            return (yInterceptsMatch || xInterceptsMatch) && curve1.IsParallel(curve2);
        }

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(LinearCurve curve1, LinearCurve curve2)
        {
            return !curve1.IsParallel(curve2);
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(LinearCurve curve1, LinearCurve curve2)
        {
            CartesianCoordinate[] intersectionCoordinates = { curve2.IntersectionCoordinate(curve1) };
            if (intersectionCoordinates.Length > 0 && double.IsInfinity(intersectionCoordinates[0].X) && double.IsInfinity(intersectionCoordinates[0].Y))
            {
                return new CartesianCoordinate[0];
            }
            return new CartesianCoordinate[] { curve2.IntersectionCoordinate(curve1) };
        }
        #endregion
    }
}
