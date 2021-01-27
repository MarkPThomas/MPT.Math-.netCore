// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 11-16-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="ConicSectionFiniteCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics.ConicSectionCurves.Elliptics;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Trigonometry;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Conic section curves that may form closed shapes.
    /// Implements the <see cref="MPT.Math.Curves.ConicSectionCurve" />
    /// Implements the <see cref="MPT.Math.Curves.ICurveLimits" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ConicSectionCurve" />
    /// <seealso cref="MPT.Math.Curves.ICurveLimits" />
    public abstract class ConicSectionEllipticCurve : ConicSectionCurve, ICurveLimits
    {
        #region Initialization     
        /// <summary>
        /// Initializes a new instance of the <see cref="ConicSectionEllipticCurve" /> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, M.</param>
        /// <param name="focus">The focus, f.</param>
        /// <param name="distanceFromMajorVertexToLocalOrigin">Distance, a, major vertex, M, to the local origin.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        protected ConicSectionEllipticCurve(
            CartesianCoordinate vertexMajor,
            CartesianCoordinate focus,
            double distanceFromMajorVertexToLocalOrigin,
            double tolerance = DEFAULT_TOLERANCE)
            : base(vertexMajor,
                   focus,
                   distanceFromMajorVertexToLocalOrigin,
                   tolerance)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConicSectionEllipticCurve"/> class.
        /// </summary>
        /// <param name="vertexMajor">The vertex major.</param>
        /// <param name="distanceFromMajorVertexToFocus">The distance from major vertex, M, to focus, f.</param>
        /// <param name="distanceFromMajorVertexToLocalOrigin">Distance, a, major vertex, M, to the local origin.</param>
        /// <param name="rotation">The rotation offset from the horizontal x-axis.</param>
        /// <param name="tolerance">The tolerance.</param>
        protected ConicSectionEllipticCurve(
            CartesianCoordinate vertexMajor,
            double distanceFromMajorVertexToFocus,
            double distanceFromMajorVertexToLocalOrigin,
            Angle rotation,
            double tolerance = DEFAULT_TOLERANCE)
            : base(vertexMajor,
                   distanceFromMajorVertexToFocus,
                   distanceFromMajorVertexToLocalOrigin,
                   rotation,
                   tolerance)
        {

        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override CartesianParametricEquationXY createParametricEquation()
        {
            return new EllipticalCurveParametric(this);
        }
        #endregion

        #region Methods: Query           
        /// <summary>
        /// Determines whether [is closed curve].
        /// </summary>
        /// <returns><c>true</c> if [is closed curve]; otherwise, <c>false</c>.</returns>
        public bool IsClosedCurve()
        {
            return Range.Start.Limit == Range.End.Limit;
        }
        #endregion

        #region Methods: Properties Derived with Limits  
        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public override double ChordLength()
        {
            return LinearCurve.Length(Range.Start.Limit, Range.End.Limit);
        }
        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public override double ChordLengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            return LinearCurve.Length(CoordinateCartesian(relativePositionStart), CoordinateCartesian(relativePositionEnd));
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public override LinearCurve Chord()
        {
            return new LinearCurve(Range.Start.Limit, Range.End.Limit);
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the linear curve is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the linear curve is ended.</param>
        /// <returns>LinearCurve.</returns>
        public override LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd)
        {
            return new LinearCurve(CoordinateCartesian(relativePositionStart), CoordinateCartesian(relativePositionEnd));
        }
        #endregion

        #region Methods: Properties
        #region Radius
        #region Focus, Right
        /// <summary>
        /// The radius measured from the right focus (+X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double RadiusAboutFocusRight(double angleRadians)
        {
            return base.RadiusAboutFocusLeft(angleRadians);
        }

        /// <summary>
        /// The differential change in radius corresponding with a differential change in the angle, measured from the right focus (+X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        protected override double radiusAboutFocusRightPrime(double angleRadians)
        {
            return base.radiusAboutFocusRightPrime(angleRadians);
        }

        /// <summary>
        /// The radius measured from the right (+X) major vertex as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double RadiusAboutVertexMajorRight(double angleRadians)
        {
            return base.RadiusAboutVertexMajorLeft(angleRadians);
        }
        #endregion
        #region Focus, Left
        /// <summary>
        /// The radius measured from the left focus (-X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double RadiusAboutFocusLeft(double angleRadians)
        {
            return base.RadiusAboutFocusRight(angleRadians);
        }

        /// <summary>
        /// The radius measured from the left (-X) major vertex as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double RadiusAboutVertexMajorLeft(double angleRadians)
        {
            return base.RadiusAboutVertexMajorRight(angleRadians);
        }
        #endregion
        #endregion

        /// <summary>
        /// Slope of the curve in local coordinates about the local origin that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">x-coordinate.</param>
        /// <returns></returns>
        public double SlopeAtX(double x)
        {
            return -1 * (DistanceFromVertexMinorToMajorAxis / DistanceFromVertexMajorToLocalOrigin) * (x / (DistanceFromVertexMajorToLocalOrigin.Squared() - x.Squared()).Sqrt());
        }

        /// <summary>
        /// Slope of the curve in local coordinates about the local origin that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">y-coordinate.</param>
        /// <returns></returns>
        public double SlopeAtY(double y)
        {
            return SlopeAtX(XatY(y));
        }

        /// <summary>
        /// Slope of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public override double SlopeByAngle(double angleRadians)
        {
            return -1 * (DistanceFromVertexMinorToMajorAxis / DistanceFromVertexMajorToLocalOrigin) * TrigonometryLibrary.Cot(angleRadians);
        }

        /// <summary>
        /// The length within the provided rotation along an elliptical curve.
        /// </summary>
        /// <param name="rotation">Rotation to get arc length between.</param>
        /// <returns>System.Double.</returns>
        public abstract double LengthBetween(AngularOffset rotation);
        #endregion

        #region Methods: Curve Position
        /// <summary>
        /// +X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which a +x-coordinate is desired.</param>
        /// <returns></returns>
        public override double XatY(double y)
        {
            return DistanceFromVertexMajorToLocalOrigin * (1 - (y / DistanceFromVertexMinorToMajorAxis).Squared()).Sqrt();
        }

        /// <summary>
        /// +Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a +y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return DistanceFromVertexMinorToMajorAxis * (1 - (x / DistanceFromVertexMajorToLocalOrigin).Squared()).Sqrt();
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutOrigin(double angleRadians)
        {
            return (DistanceFromVertexMajorToLocalOrigin * TrigonometryLibrary.Cos(angleRadians));
        }

        /// <summary>
        /// Y-coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public override double YbyRotationAboutOrigin(double angleRadians)
        {
            return (DistanceFromVertexMinorToMajorAxis * TrigonometryLibrary.Sin(angleRadians));
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusRight(double angleRadians)
        {
            return DistanceFromFocusToLocalOrigin + RadiusAboutFocusRight(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the left (-X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the left (-X) focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusLeft(double angleRadians)
        {
            return -1 * DistanceFromFocusToLocalOrigin + RadiusAboutFocusLeft(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
        }
        #endregion

        #region Methods: Protected
        /// <summary>
        /// Gets the minor vertices.
        /// </summary>
        /// <returns>Tuple&lt;CartesianCoordinate, CartesianCoordinate&gt;.</returns>
        protected override Tuple<CartesianCoordinate, CartesianCoordinate> getVerticesMinor()
        {
            return getVerticesMinor(LocalOrigin);
        }

        /// <summary>
        /// Gets the vertex major2 coordinate.
        /// </summary>
        /// <returns>CartesianCoordinate.</returns>
        protected override CartesianCoordinate getVertexMajor2()
        {
            return _vertexMajor.OffsetCoordinate(-2 * DistanceFromVertexMajorToLocalOrigin, _rotation);
        }

        /// <summary>
        /// Distance from local origin to minor Vertex, b.
        /// </summary>
        /// <param name="a">Distance from local origin to major vertex, a.</param>
        /// <param name="c">Distance from local origin to the focus, c.</param>
        /// <returns>System.Double.</returns>
        protected override double distanceFromVertexMinorToMajorAxis(double a, double c)
        {
            return (a.Squared() - c.Squared()).Sqrt();
        }

        /// <summary>
        /// Gets the directrix vertices.
        /// </summary>
        /// <returns>Tuple&lt;CartesianCoordinate, CartesianCoordinate&gt;.</returns>
        protected override Tuple<CartesianCoordinate, CartesianCoordinate> getVerticesDirectrix()
        {
            Angle rotation = new Angle(_rotation.Radians + Numbers.PiOver2);
            if (DistanceFromFocusToDirectrix == double.PositiveInfinity || DistanceFromFocusToDirectrix == double.NegativeInfinity)
            {
                return new Tuple<CartesianCoordinate, CartesianCoordinate>(
                    new CartesianCoordinate(DistanceFromFocusToDirectrix, 0),
                    new CartesianCoordinate(DistanceFromFocusToDirectrix, 1));
            }
            CartesianCoordinate directrixIntercept = _focus.OffsetCoordinate(DistanceFromFocusToDirectrix, _rotation);
            return new Tuple<CartesianCoordinate, CartesianCoordinate>(
                directrixIntercept,
                directrixIntercept.OffsetCoordinate(1, rotation));
        }
        #endregion
    }
}
