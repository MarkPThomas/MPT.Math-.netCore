// ***********************************************************************
// Assembly         : 
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="IntersectionCircularCircular.cs" company="MarkPThomas Inc.">
//     Copyright (c) MarkPThomas Inc.. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;

namespace MPT.Math.Curves.Tools.Intersections
{
    /// <summary>
    /// Class IntersectionLinearLinear.
    /// Implements the <see cref="ICurveIntersection{CircularCurve, CircularCurve}" />
    /// </summary>
    /// <seealso cref="ICurveIntersection{CircularCurve, CircularCurve}" />
    public class IntersectionCircularCircular : IntersectionAbstract<CircularCurve, CircularCurve>
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearLinear"/> class.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The second curve.</param>
        public IntersectionCircularCircular(CircularCurve curve1, CircularCurve curve2) : base(curve1, curve2)
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
        
        /// <summary>
        /// The separation of the centers of the curves.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double CenterSeparations()
        {
            return CenterSeparations(Curve1, Curve2);
        }

        /// <summary>
        /// The length of the radical line, which is the straight line connecting the two intersection points.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double RadicalLineLength()
        {
            return RadicalLineLength(CenterSeparations(), Curve1.Radius, Curve2.Radius);
        }
        #endregion

        #region Static        
        /// <summary>
        /// The separation of the centers of the curves.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns>System.Double.</returns>
        public static double CenterSeparations(CircularCurve curve1, CircularCurve curve2)
        {
            return curve1.Center.OffsetFrom(curve2.Center).Length();
        }

        /// <summary>
        /// Determines if the curves are tangent to each other.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreTangent(CircularCurve curve1, CircularCurve curve2)
        {
            double tolerance = Generics.GetTolerance(curve1, curve2);

            return (curve1.Radius + curve2.Radius).IsEqualTo(CenterSeparations(curve1, curve2), tolerance);
        }

        /// <summary>
        /// The curves intersect.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreIntersecting(CircularCurve curve1, CircularCurve curve2)
        {
            double tolerance = Generics.GetTolerance(curve1, curve2);
            double centerSeparation = curve1.Center.OffsetFrom(curve2.Center).Length();

            return (curve1.Radius + curve2.Radius).IsGreaterThan(centerSeparation, tolerance);
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(CircularCurve curve1, CircularCurve curve2)
        {
            double separation = CenterSeparations(curve1, curve2);
            double radius1 = curve1.Radius;
            double radius2 = curve2.Radius;

            double x = factor(separation, radius1, radius2) / (2 * separation);
            double[] y = Numbers.PlusMinus(0, RadicalLineLength(separation, radius1, radius2) / 2);

            CartesianCoordinate localPoint1 = new CartesianCoordinate(x, y[0]);
            CartesianCoordinate localPoint2 = new CartesianCoordinate(x, y[1]);
            Transformations transformation = new Transformations(curve1.Center, curve2.Center);

            return new CartesianCoordinate[]
                {
                    transformation.TransformToGlobal(localPoint1),
                    transformation.TransformToGlobal(localPoint2)
                };
        }

        /// <summary>
        /// The length of the radical line, which is the straight line connecting the two intersection points.
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double RadicalLineLength(double separation, double radius1, double radius2)
        {
            return (4 * (separation * radius1).Squared() - factor(separation, radius1, radius2).Squared()).Sqrt() / separation;
        }

        /// <summary>
        /// Factors the specified separation.
        /// </summary>
        /// <param name="separation">The separation.</param>
        /// <param name="radius1">The radius1.</param>
        /// <param name="radius2">The radius2.</param>
        /// <returns>System.Double.</returns>
        protected static double factor(double separation, double radius1, double radius2)
        {
            return separation.Squared() - radius2.Squared() + radius1.Squared();
        }
        #endregion
    }
}
