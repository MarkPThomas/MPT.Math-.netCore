// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-19-2020
// ***********************************************************************
// <copyright file="IntersectionLinearCircular.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.Curves.Tools.Intersections
{
    /// <summary>
    /// Class IntersectionLinearLinear.
    /// Implements the <see cref="ICurveIntersection{LinearCurve, CircularCurve}" />
    /// </summary>
    /// <seealso cref="ICurveIntersection{LinearCurve, CircularCurve}" />
    public class IntersectionLinearCircular : IntersectionAbstract<LinearCurve, CircularCurve>
    {
        #region Properties
        /// <summary>
        /// Gets the linear curve.
        /// </summary>
        /// <value>The linear curve.</value>
        public LinearCurve LinearCurve => Curve1;
        /// <summary>
        /// Gets the circular curve.
        /// </summary>
        /// <value>The circular curve.</value>
        public CircularCurve CircularCurve => Curve2;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearCircular"/> class.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        public IntersectionLinearCircular(LinearCurve linearCurve, CircularCurve circularCurve) : base(linearCurve, circularCurve)
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
            return AreTangent(LinearCurve, CircularCurve);
        }

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreIntersecting()
        {
            return AreIntersecting(LinearCurve, CircularCurve);
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <returns>CartesianCoordinate[].</returns>
        public override CartesianCoordinate[] IntersectionCoordinates()
        {
            return IntersectionCoordinates(LinearCurve, CircularCurve);
        }
        #endregion

        #region Static
        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreTangent(LinearCurve linearCurve, CircularCurve circularCurve)
        {
            //CartesianCoordinate[] coordinates = IntersectionCoordinates(linearCurve, circularCurve);
            //return coordinates.Length == 1;
            return incidenceDelta(linearCurve, circularCurve).IsEqualTo(0, Generics.GetTolerance(linearCurve, circularCurve));
        }

        /// <summary>
        /// The curves intersect and are not tangent.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(LinearCurve linearCurve, CircularCurve circularCurve)
        {
            //CartesianCoordinate[] coordinates = IntersectionCoordinates(linearCurve, circularCurve);
            //return coordinates.Length == 2;
            return incidenceDelta(linearCurve, circularCurve).IsGreaterThan(0, Generics.GetTolerance(linearCurve, circularCurve));
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(LinearCurve linearCurve, CircularCurve circularCurve)
        {
            double D = linearCurve.ControlPointI.CrossProduct(linearCurve.ControlPointJ);
            double r = circularCurve.Radius;
            CartesianOffset offset = linearCurve.LimitEnd.OffsetFrom(linearCurve.LimitStart);
            double dx = offset.X();
            double dy = offset.Y();
            double dr = AlgebraLibrary.SRSS(dx, dy);


            double incidenceDeltaSqrt = ((r * dr).Squared() - D.Squared()).Sqrt();

            double[] xIntersection = Numbers.PlusMinus(D * dy / dr.Squared(), (dy.Sign() * dx / dr.Squared()) * incidenceDeltaSqrt);
            double[] yIntersection = Numbers.PlusMinus(-1 * D * dx / dr.Squared(), (dy.Abs() / dr.Squared()) * incidenceDeltaSqrt);

            return new CartesianCoordinate[]
            {
                new CartesianCoordinate(xIntersection[0], yIntersection[0]),
                new CartesianCoordinate(xIntersection[1], yIntersection[1])
            };
        }

        /// <summary>
        /// The incidence delta.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <returns>System.Double.</returns>
        protected static double incidenceDelta(LinearCurve linearCurve, CircularCurve circularCurve)
        {
            double D = linearCurve.ControlPointI.CrossProduct(linearCurve.ControlPointJ);
            double r = circularCurve.Radius;
            CartesianOffset offset = linearCurve.LimitEnd.OffsetFrom(linearCurve.LimitStart);
            double dx = offset.X();
            double dy = offset.Y();
            double dr = AlgebraLibrary.SRSS(dx, dy);

            return (r * dr).Squared() - D.Squared();
        }
        #endregion
    }
}
