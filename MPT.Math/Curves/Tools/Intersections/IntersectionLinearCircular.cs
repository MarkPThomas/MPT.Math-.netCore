// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 01-16-2021
// ***********************************************************************
// <copyright file="IntersectionLinearCircular.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using System.Transactions;

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

        /// <summary>
        /// The properties
        /// </summary>
        private IntersectionProperties _properties = null;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearCircular" /> class.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        public IntersectionLinearCircular(LinearCurve linearCurve, CircularCurve circularCurve) : base(linearCurve, circularCurve)
        {
            _properties = new IntersectionProperties(linearCurve, circularCurve);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreTangent()
        {
            return _areTangent(LinearCurve, CircularCurve, _properties);
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AreIntersecting()
        {
            return _areIntersecting(LinearCurve, CircularCurve, _properties);
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <returns>CartesianCoordinate[].</returns>
        public override CartesianCoordinate[] IntersectionCoordinates()
        {
            return _intersectionCoordinates(LinearCurve, CircularCurve, _properties);
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
            return _areTangent(linearCurve, circularCurve, new IntersectionProperties(linearCurve, circularCurve));
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(LinearCurve linearCurve, CircularCurve circularCurve)
        {
            return _areIntersecting(linearCurve, circularCurve, new IntersectionProperties(linearCurve, circularCurve));
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(LinearCurve linearCurve, CircularCurve circularCurve)
        {
            return _intersectionCoordinates(linearCurve, circularCurve, new IntersectionProperties(linearCurve, circularCurve));
        }

        /// <summary>
        /// The curves are tangent to each other.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <param name="_properties">Pre-calculated properties to be used for convenience.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool _areTangent(LinearCurve linearCurve, CircularCurve circularCurve, IntersectionProperties _properties)
        {
            return _properties.IncidenceDelta.IsEqualTo(0, Generics.GetTolerance(linearCurve, circularCurve));
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <param name="_properties">Pre-calculated properties to be used for convenience.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool _areIntersecting(LinearCurve linearCurve, CircularCurve circularCurve, IntersectionProperties _properties)
        {
            return _properties.IncidenceDelta.IsGreaterThanOrEqualTo(0, Generics.GetTolerance(linearCurve, circularCurve));
        }

        /// <summary>
        /// The coordinate of the intersection of two curves.
        /// </summary>
        /// <param name="linearCurve">The linear curve.</param>
        /// <param name="circularCurve">The circular curve.</param>
        /// <param name="_properties">Pre-calculated properties to be used for convenience.</param>
        /// <returns>CartesianCoordinate[].</returns>
        private static CartesianCoordinate[] _intersectionCoordinates(LinearCurve linearCurve, CircularCurve circularCurve, IntersectionProperties _properties)
        {
            if (!_areIntersecting(linearCurve, circularCurve, _properties))
            {
                return new CartesianCoordinate[0];
            }

            double D = _properties.D;
            double dx = _properties.dx;
            double dy = _properties.dy;
            double dr = _properties.dr;
            double incidenceDeltaSqrt = _properties.IncidenceDelta.Sqrt();

            double[] xIntersection = Numbers.PlusMinus(D * dy / dr.Squared(), (dy.Sign() * dx / dr.Squared()) * incidenceDeltaSqrt);
            double[] yIntersection = Numbers.PlusMinus(-1 * D * dx / dr.Squared(), (dy.Abs() / dr.Squared()) * incidenceDeltaSqrt);

            Transformations converter = _properties.Transformations;

            if (_areTangent(linearCurve, circularCurve, _properties))
            {
                return new CartesianCoordinate[] { converter.TransformToGlobal(new CartesianCoordinate(xIntersection[0], yIntersection[0])) };
            }
            
            return new CartesianCoordinate[]
            {
                converter.TransformToGlobal(new CartesianCoordinate(xIntersection[0], yIntersection[0])),
                converter.TransformToGlobal(new CartesianCoordinate(xIntersection[1], yIntersection[1]))
            };
        }
        #endregion


        /// <summary>
        /// Class IntersectionProperties.
        /// </summary>
        protected class IntersectionProperties
        {
            /// <summary>
            /// Gets the tolerance.
            /// </summary>
            /// <value>The tolerance.</value>
            public double Tolerance { get; }
            /// <summary>
            /// Cross-product of two points defining the linear curve.
            /// </summary>
            /// <value>The d.</value>
            public double D { get; }
            /// <summary>
            /// X-axis distance between two points defining the linear curve.
            /// </summary>
            /// <value>The dx.</value>
            public double dx { get; }
            /// <summary>
            /// Y-axis distance between two points defining the linear curve.
            /// </summary>
            /// <value>The dy.</value>
            public double dy { get; }
            /// <summary>
            /// Distance between two points defining the linear curve.
            /// </summary>
            /// <value>The dr.</value>
            public double dr { get; }
            /// <summary>
            /// Gets the incidence delta.
            /// </summary>
            /// <value>The incidence delta.</value>
            public double IncidenceDelta { get; }
            /// <summary>
            /// Gets the transformations object.
            /// </summary>
            /// <value>The transformations.</value>
            public Transformations Transformations { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="IntersectionProperties"/> class.
            /// </summary>
            /// <param name="linearCurve">The linear curve.</param>
            /// <param name="circularCurve">The circular curve.</param>
            public IntersectionProperties(LinearCurve linearCurve, CircularCurve circularCurve)
            {
                Tolerance = Generics.GetTolerance(linearCurve, circularCurve);
                Transformations = new Transformations(circularCurve.LocalOrigin, new CartesianCoordinate(circularCurve.LocalOrigin.X + 1, circularCurve.LocalOrigin.Y));
                LinearCurve linearCurveLocal = transformToLocal(linearCurve);

                D = Numbers.ValueAsZeroIfWithinAbsoluteTolerance(linearCurveLocal.ControlPointI.CrossProduct(linearCurveLocal.ControlPointJ), Tolerance);
                CartesianOffset offset = linearCurveLocal.Range.End.Limit.OffsetFrom(linearCurveLocal.Range.Start.Limit);
                dx = offset.X();
                dy = offset.Y();
                dr = AlgebraLibrary.SRSS(dx, dy);

                IncidenceDelta = Numbers.ValueAsZeroIfWithinAbsoluteTolerance((circularCurve.Radius * dr).Squared() - D.Squared(), Tolerance);
            }

            /// <summary>
            /// Returns new linear curve transformed to the local coordinates of the provided circular curve.
            /// </summary>
            /// <param name="linearCurve">The linear curve.</param>
            /// <returns>LinearCurve.</returns>
            protected LinearCurve transformToLocal(LinearCurve linearCurve)
            {
                return new LinearCurve(
                    Transformations.TransformToLocal(linearCurve.ControlPointI),
                    Transformations.TransformToLocal(linearCurve.ControlPointJ));
            }
        }
    }
}
