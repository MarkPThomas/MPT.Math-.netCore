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
using MPT.Math.Curves.Parametrics;
using MPT.Math.Curves.Parametrics.ConicSectionCurves.Elliptics;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Trigonometry;
using MPT.Math.Vectors;

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
        #region Properties        
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public override double Tolerance
        {
            get => base.Tolerance;
            set
            {
                base.Tolerance = value;
                _limitStart.Tolerance = _tolerance;
                _limitEnd.Tolerance = _tolerance;
            }
        }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConicSectionEllipticCurve" /> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, a.</param>
        /// <param name="distanceFromFocusToLocalOrigin">The distance from focus, c, to local origin.</param>
        /// <param name="localOrigin">The coordinate of the local origin.</param>
        protected ConicSectionEllipticCurve(
            CartesianCoordinate vertexMajor,
            double distanceFromFocusToLocalOrigin,
            CartesianCoordinate localOrigin)
            : base(vertexMajor, distanceFromFocusToLocalOrigin, localOrigin)
        {

        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override LinearParametricEquation createParametricEquation()
        {
            return new EllipticParametric(this);
        }
        #endregion

        #region Methods: Query           
        /// <summary>
        /// Determines whether [is closed curve].
        /// </summary>
        /// <returns><c>true</c> if [is closed curve]; otherwise, <c>false</c>.</returns>
        public bool IsClosedCurve()
        {
            return LimitStart == LimitEnd;
        }
        #endregion

        #region Methods: Properties Derived with Limits  
        /// <summary>
        /// The limit where the curve starts.
        /// </summary>
        protected CartesianCoordinate _limitStart;
        /// <summary>
        /// The limit where the curve starts.
        /// </summary>
        /// <value>The limit start.</value>
        public CartesianCoordinate LimitStart => _limitStart;

        /// <summary>
        /// The limit where the curve ends.
        /// </summary>
        protected CartesianCoordinate _limitEnd;
        /// <summary>
        /// The limit where the curve ends.
        /// </summary>
        /// <value>The limit end.</value>
        public CartesianCoordinate LimitEnd => _limitEnd;

        /// <summary>
        /// Length of the curve between the limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public abstract double Length();

        /// <summary>
        /// Length of the curve between two points.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public abstract double LengthBetween(double relativePositionStart, double relativePositionEnd);

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double ChordLength()
        {
            return LinearCurve.Length(LimitStart, LimitEnd);
        }
        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public double ChordLengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            return LinearCurve.Length(CoordinateCartesian(relativePositionStart), CoordinateCartesian(relativePositionEnd));
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public LinearCurve Chord()
        {
            return new LinearCurve(LimitStart, LimitEnd);
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the linear curve is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the linear curve is ended.</param>
        /// <returns>LinearCurve.</returns>
        public LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd)
        {
            return new LinearCurve(CoordinateCartesian(relativePositionStart), CoordinateCartesian(relativePositionEnd));
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        public abstract Vector TangentVector(double relativePosition);

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        public abstract Vector NormalVector(double relativePosition);

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        public abstract CartesianCoordinate CoordinateCartesian(double relativePosition);

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        public abstract PolarCoordinate CoordinatePolar(double relativePosition);
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
            return -1 * (DistanceFromVertexMinorToOrigin / DistanceFromVertexMajorToOrigin) * (x / (DistanceFromVertexMajorToOrigin.Squared() - x.Squared()).Sqrt());
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
            return -1 * (DistanceFromVertexMinorToOrigin / DistanceFromVertexMajorToOrigin) * TrigonometryLibrary.Cot(angleRadians);
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
            return DistanceFromVertexMajorToOrigin * (1 - (y / DistanceFromVertexMinorToOrigin).Squared()).Sqrt();
        }

        /// <summary>
        /// +Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a +y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return DistanceFromVertexMinorToOrigin * (1 - (x / DistanceFromVertexMajorToOrigin).Squared()).Sqrt();
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutOrigin(double angleRadians)
        {
            return (DistanceFromVertexMajorToOrigin * TrigonometryLibrary.Cos(angleRadians));
        }

        /// <summary>
        /// Y-coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public override double YbyRotationAboutOrigin(double angleRadians)
        {
            return (DistanceFromVertexMinorToOrigin * TrigonometryLibrary.Sin(angleRadians));
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusRight(double angleRadians)
        {
            return DistanceFromFocusToOrigin + RadiusAboutFocusRight(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the left (-X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the left (-X) focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusLeft(double angleRadians)
        {
            return -1 * DistanceFromFocusToOrigin + RadiusAboutFocusLeft(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
        }
        #endregion

        #region Methods: Protected
        /// <summary>
        /// Distance from local origin to minor Vertex, b.
        /// </summary>
        /// <param name="a">Distance from local origin to major vertex, a.</param>
        /// <param name="c">Distance from local origin to the focus, c.</param>
        /// <returns>System.Double.</returns>
        protected override double distanceFromVertexMinorToOrigin(double a, double c)
        {
            return (a.Squared() - c.Squared()).Sqrt();
        }
        #endregion
    }
}
