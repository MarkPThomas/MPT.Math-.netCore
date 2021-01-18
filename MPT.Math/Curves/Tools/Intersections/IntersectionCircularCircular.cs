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
        /// <summary>
        /// Gets the transformations object.
        /// </summary>
        /// <value>The transformations.</value>
        private Transformations _transformations = null;

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionLinearLinear"/> class.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The second curve.</param>
        public IntersectionCircularCircular(CircularCurve curve1, CircularCurve curve2) : base(curve1, curve2)
        {
            _transformations = getTransformations(curve1, curve2);
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
            return _intersectionCoordinates(Curve1, Curve2, _transformations);
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
            double separation = CenterSeparations();
            if (separation == 0) throw new OverlappingCurvesException($"Circles must have different origins in order to calculate radical line length. {Curve1}, {Curve2}");
            if (separation > Curve1.Radius + Curve2.Radius) throw new IntersectingCurveException($"Circles must intersect in order to calculate radical line length. {Curve1}, {Curve2}");

            return RadicalLineLength(separation, Curve1.Radius, Curve2.Radius);
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
            return curve1.LocalOrigin.OffsetFrom(curve2.LocalOrigin).Length();
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
            double centerSeparation = curve1.LocalOrigin.OffsetFrom(curve2.LocalOrigin).Length();

            return (curve1.Radius + curve2.Radius).IsGreaterThanOrEqualTo(centerSeparation, tolerance);
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <returns>CartesianCoordinate[].</returns>
        public static CartesianCoordinate[] IntersectionCoordinates(CircularCurve curve1, CircularCurve curve2)
        {
            Transformations transformations = getTransformations(curve1, curve2);
            return _intersectionCoordinates(curve1, curve2, transformations);
        }

        /// <summary>
        /// The coordinate(s) of the intersection(s) of two curves.
        /// </summary>
        /// <param name="curve1">The first curve.</param>
        /// <param name="curve2">The first curve.</param>
        /// <param name="converter">The converter.</param>
        /// <returns>CartesianCoordinate[].</returns>
        private static CartesianCoordinate[] _intersectionCoordinates(CircularCurve curve1, CircularCurve curve2, Transformations converter)
        {
            if (!AreIntersecting(curve1, curve2))
            {
                return new CartesianCoordinate[0];
            }

            double separation = CenterSeparations(curve1, curve2);
            double radius1 = curve1.Radius;
            double radius2 = curve2.Radius;
            double tolerance = Generics.GetTolerance(curve1, curve2);

            double xIntersection = factor(separation, radius1, radius2) / (2 * separation);
            double[] yIntersection = Numbers.PlusMinus(0, RadicalLineLength(separation, radius1, radius2, tolerance) / 2);

            CartesianCoordinate localPoint1 = converter.TransformToGlobal(new CartesianCoordinate(xIntersection, yIntersection[0]));

            if (AreTangent(curve1, curve2))
            {
                return new CartesianCoordinate[] { localPoint1 };
            }

            CartesianCoordinate localPoint2 = converter.TransformToGlobal(new CartesianCoordinate(xIntersection, yIntersection[1]));

            return new CartesianCoordinate[]
                {
                    localPoint1,
                    localPoint2
                };
        }

        /// <summary>
        /// The length of the radical line, which is the straight line connecting the two intersection points.
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double RadicalLineLength(double separation, double radius1, double radius2, double tolerance = Numbers.ZeroTolerance)
        {
            if (separation == 0) throw new OverlappingCurvesException("Circles must have different origins in order to calculate radical line length.");
            if (separation.IsGreaterThan(radius1 + radius2, tolerance)) throw new IntersectingCurveException("Circles must intersect in order to calculate radical line length.");

            double component = Numbers.ValueAsZeroIfWithinAbsoluteTolerance(4 * (separation * radius1).Squared() - factor(separation, radius1, radius2).Squared(), tolerance);
            return component.Sqrt() / separation;
        }

        /// <summary>
        /// Factors the specified separation.
        /// </summary>
        /// <param name="separation">The separation.</param>
        /// <param name="radius1">The radius1.</param>
        /// <param name="radius2">The radius2.</param>
        /// <returns>System.Double.</returns>
        private static double factor(double separation, double radius1, double radius2)
        {
            return separation.Squared() - radius2.Squared() + radius1.Squared();
        }

        /// <summary>
        /// Gets the transformations to use for local vs. global coordinates.
        /// </summary>
        /// <param name="circleAtOrigin">The circular curve set to the local origin.</param>
        /// <param name="otherCircle">The other circular curve set to the local x-axis.</param>
        /// <returns>Transformations.</returns>
        private static Transformations getTransformations(CircularCurve circleAtOrigin, CircularCurve otherCircle)
        {
            return new Transformations(circleAtOrigin.LocalOrigin, otherCircle.LocalOrigin);
        }
        #endregion
    }
}
