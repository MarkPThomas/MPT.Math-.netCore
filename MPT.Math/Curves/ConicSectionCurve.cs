// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="ConicSectionCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.Geometry;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;
using System;
using MPT.Math.Curves.Parametrics.ConicSectionCurves;
using MPT.Math.Trigonometry;

namespace MPT.Math.Curves
{
    /// <summary>
    /// A conic section (or simply conic) is a curve obtained as the intersection of the surface of a cone with a plane; the three types are parabolas, ellipses (circles are a subtype), and hyperbolas.
    /// A conic section is the locus of points P whose distance to the focus is a constant multiple of the distance from P to the directrix of the conic.
    /// <a href="https://courses.lumenlearning.com/boundless-algebra/chapter/introduction-to-conic-sections">Reference</a>. 
    /// <a href="https://en.wikipedia.org/wiki/Conic_section">Wikipedia</a>
    /// Implements the <see cref="MPT.Math.Curves.Curve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.Curve" />
    public abstract class ConicSectionCurve : Curve,
        ICurveLimits,
        ICurvePositionCartesian, ICurvePositionPolar
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
                _localRotation.Tolerance = _tolerance;
                _directrixILocal.Tolerance = _tolerance;
                _directrixJLocal.Tolerance = _tolerance;
                _vertexMajorLocal.Tolerance = _tolerance;
                _vertexMinorLocal.Tolerance = _tolerance;
                _focusLocal.Tolerance = _tolerance;
            }
        }

        /// <summary>
        /// The parametric equation of the radius measured from the focus.
        /// </summary>
        protected RadiusFocusParametric _radiusFromRightFocus;

        /// <summary>
        /// The rotational offset of local coordinates from global coordinates.
        /// </summary>
        protected Angle _localRotation;
        /// <summary>
        /// Gets the rotational offset of local coordinates from global coordinates.
        /// </summary>
        /// <value>The local off set rotation.</value>
        public Angle LocalRotation => _localRotation;

        /// <summary>
        /// The coordinate of the local origin.
        /// </summary>
        protected CartesianCoordinate _originLocal;
        /// <summary>
        /// The coordinate of the local origin.
        /// </summary>
        /// <value>The local origin.</value>
        public CartesianCoordinate LocalOrigin => _originLocal;

        /// <summary>
        /// The I-coordinate, in local coordinates, that lies on the directrix, Xe.
        /// </summary>
        protected CartesianCoordinate _directrixILocal;
        /// <summary>
        /// The J-coordinate, in local coordinates, that lies on the directrix, Xe.
        /// </summary>
        protected CartesianCoordinate _directrixJLocal;
        /// <summary>
        /// Gets the directrices, Xe, in local coordinates.
        /// </summary>
        /// <value>The directrices.</value>
        public virtual Tuple<LinearCurve, LinearCurve> Directrices
        {
            get
            {
                return new Tuple<LinearCurve, LinearCurve>
                    (
                    new LinearCurve(_directrixILocal, _directrixJLocal), 
                    new LinearCurve(-1 * _directrixILocal, -1 * _directrixJLocal)
                    );
            }
        }
        /// <summary>
        /// Distance from local origin to the directrix line, Xe.
        /// </summary>
        /// <value>The distance from directrix to origin.</value>
        public virtual double DistanceFromDirectrixToOrigin => Eccentricity == 0 ? double.PositiveInfinity : DistanceFromVertexMajorToOrigin.Squared() / DistanceFromFocusToOrigin;

        /// <summary>
        /// The major vertex, a, in local coordinates, which is the point on a conic section that lies closest to the directrix.
        /// </summary>
        protected CartesianCoordinate _vertexMajorLocal;
        /// <summary>
        /// Gets the major vertices, a, in local coordinates, which are the points on a conic section that lie closest to the directrices.
        /// </summary>
        /// <value>The vertices.</value>
        public virtual Tuple<CartesianCoordinate, CartesianCoordinate> VerticesMajor 
        {
            get
            {
                return new Tuple<CartesianCoordinate, CartesianCoordinate>(_vertexMajorLocal, -1 * _vertexMajorLocal);
            }
        }
        /// <summary>
        /// Distance from local origin to major vertex, a.
        /// </summary>
        /// <value>a.</value>
        public double DistanceFromVertexMajorToOrigin { get; }

        /// <summary>
        /// The minor vertex, b, in local coordinates, which lies along a line perpendicular to a line passing through the major vertex, a.
        /// </summary>
        protected CartesianCoordinate _vertexMinorLocal;
        /// <summary>
        /// Gets the minor vertices, b, in local coordinates, which lie along a line perpendicular to a line passing through the major vertex, a.
        /// </summary>
        /// <value>The conjugate vertices.</value>
        public Tuple<CartesianCoordinate, CartesianCoordinate> VerticesMinor 
        {
            get
            {
                return new Tuple<CartesianCoordinate, CartesianCoordinate>(_vertexMinorLocal, -1 * _vertexMinorLocal);
            }
        }
        /// <summary>
        /// Distance from local origin to minor vertex, b.
        /// </summary>
        /// <value>The b.</value>
        public virtual double DistanceFromVertexMinorToOrigin { get; }

        /// <summary>
        /// The focus,c, in local coordinates.
        /// </summary>
        protected CartesianCoordinate _focusLocal;
        /// <summary>
        /// Gets the foci, c, in local coordinates.
        /// </summary>
        /// <value>The foci.</value>
        public virtual Tuple<CartesianCoordinate, CartesianCoordinate> Foci
        {
            get
            {
                return new Tuple<CartesianCoordinate, CartesianCoordinate>(_focusLocal, -1 * _focusLocal);
            }
        }
        /// <summary>
        /// Distance from local origin to the focus, c.
        /// </summary>
        /// <value>The distance from focus to origin.</value>
        public virtual double DistanceFromFocusToOrigin { get; }

        /// <summary>
        /// Distance from the focus to the directrix, p.
        /// </summary>
        /// <value>The distance from focus to directrix.</value>
        public virtual double DistanceFromFocusToDirectrix => Eccentricity == 0 ? 
            double.PositiveInfinity : 
            DistanceFromFocusToOrigin - DistanceFromDirectrixToOrigin;


        /// <summary>
        /// The eccentricity, e.
        /// A measure of how much the conic section deviates from being circular.
        /// Distance from any point on the conic section to its focus, divided by the perpendicular distance from that point to the nearest directrix.
        /// </summary>
        /// <value>The eccentricity.</value>
        public virtual double Eccentricity => DistanceFromFocusToOrigin / DistanceFromVertexMajorToOrigin;
        #endregion

        #region Initialization                        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConicSectionCurve" /> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, a.</param>
        /// <param name="distanceFromFocusToLocalOrigin">The distance from focus, c, to local origin.</param>
        /// <param name="localOrigin">The coordinate of the local origin.</param>
        protected ConicSectionCurve(CartesianCoordinate vertexMajor, double distanceFromFocusToLocalOrigin, CartesianCoordinate localOrigin)
        {
            DistanceFromVertexMajorToOrigin = AlgebraLibrary.SRSS(vertexMajor.X - localOrigin.X, vertexMajor.Y - localOrigin.Y);
            DistanceFromFocusToOrigin = distanceFromFocusToLocalOrigin;
            DistanceFromVertexMinorToOrigin = distanceFromVertexMinorToOrigin(DistanceFromVertexMajorToOrigin, DistanceFromFocusToOrigin);

            LinearCurve localAxisX = new LinearCurve(localOrigin, vertexMajor);
            _localRotation = localAxisX.TangentVector().Angle();
            _originLocal = localOrigin;
            _focusLocal = new CartesianCoordinate(DistanceFromFocusToOrigin, 0);
            _vertexMajorLocal = new CartesianCoordinate(DistanceFromVertexMajorToOrigin, 0);
            _vertexMinorLocal = new CartesianCoordinate(0, DistanceFromVertexMinorToOrigin);
            _directrixILocal = new CartesianCoordinate(DistanceFromDirectrixToOrigin, 1);
            _directrixJLocal = new CartesianCoordinate(DistanceFromDirectrixToOrigin, -1);

            _radiusFromRightFocus = new RadiusFocusParametric(this);
        }
        #endregion

        #region Methods: Properties  
        #region Radius
        #region Focus, Right
        /// <summary>
        /// The radius measured from the right focus (+X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angle">The angle in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public double RadiusAboutFocusRight(Angle angle)
        {
            return RadiusAboutFocusRight(angle.Radians);
        }

        /// <summary>
        /// The radius measured from the right focus (+X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutFocusRight(double angleRadians)
        {
            return _radiusFromRightFocus.Radius.ValueAt(angleRadians);
        }

        /// <summary>
        /// The differential change in radius corresponding with a differential change in the angle, measured from the right focus (+X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        protected virtual double radiusAboutFocusRightPrime(double angleRadians)
        {
            return _radiusFromRightFocus.Radius.Differential.ValueAt(angleRadians);
        }

        /// <summary>
        /// The radius measured from the right (+X) major vertex as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutVertexMajorRight(double angleRadians)
        {
            return RadiusAboutVertexMajorLeft(Numbers.Pi - angleRadians);
        }
        #endregion
        #region Focus, Left
        /// <summary>
        /// The radius measured from the left focus (-X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angle">The angle in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public double RadiusAboutFocusLeft(Angle angle)
        {
            return RadiusAboutFocusLeft(angle.Radians);
        }

        /// <summary>
        /// The radius measured from the left focus (-X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutFocusLeft(double angleRadians)
        {
            return RadiusAboutFocusRight(Numbers.Pi - angleRadians);
        }

        /// <summary>
        /// The radius measured from the left (-X) major vertex as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutVertexMajorLeft(double angleRadians)
        {
            throw new NotImplementedException();
        }
        #endregion


        /// <summary>
        /// The radius measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <param name="offset">The offst of the shape center/origin from the coordinate origin.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutOffset(double angleRadians, CartesianCoordinate offset)
        {
            return ((offset.X + XbyRotationAboutOrigin(angleRadians)).Squared() + (offset.Y + YbyRotationAboutOrigin(angleRadians)).Squared()).Sqrt();
        }
        #endregion

        /// <summary>
        /// Slope of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public virtual double SlopeByAngle(double angleRadians)
        {
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);

            return GeometryLibrary.SlopeParametric(xPrime, yPrime);
        }

        /// <summary>
        /// Curvature of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public virtual double CurvatureByAngle(double angleRadians)
        {
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);
            double xPrimeDouble = xPrimeDoubleByParameter(angleRadians);
            double yPrimeDouble = yPrimeDoubleByParameter(angleRadians);

            return GeometryLibrary.CurvatureParametric(xPrime, yPrime, xPrimeDouble, yPrimeDouble);
        }

        /// <summary>
        /// Tangential angle of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public double TangentialAngleByAngle(double angleRadians)
        {
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);

            return Trig.ArcTan(yPrime / xPrime);
        }

        /// <summary>
        /// Angle between the tangent of the curve and the radius connecting the origin to the point considered. 
        /// This is in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// <a href="https://planetmath.org/polartangentialangle">Reference</a> &amp; <a href="https://www.youtube.com/watch?v=6RwOoPN2zqE">Video</a>.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public double PolarTangentialAngleAboutByAngle(double angleRadians)
        {
            // TODO: PolarTangentialAngleAboutByAngle - Check whether this is really about the origin vs. right focus, in which case a left focus method is also needed.
            double radius = RadiusAboutFocusRight((Angle)angleRadians);
            double radiusPrime = radiusAboutFocusRightPrime(angleRadians);

            return Trig.ArcTan(radius / radiusPrime);
        }

        /// <summary>
        /// Vector that is tangential to the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>Vector.</returns>
        public virtual Vector TangentVectorByAngle(double angleRadians)
        {
            // TODO: Compare methods of components vs. parametric vector for TangentVectorByAngle? Commented method might be slower?
            //return vectorParametric.UnitTangentVectorAt(angleRadians).ToVectorAt(angleRadians);
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);
            return Vector.UnitTangentVector(xPrime, yPrime, Tolerance);
        }

        /// <summary>
        /// Vector that is tangential to the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns>Vector.</returns>
        public virtual Vector NormalVectorByAngle(double angleRadians)
        {
            // TODO: Compare methods of components vs. parametric vector for NormalVectorByAngle? Commented method might be slower?
            //return vectorParametric.UnitNormalVectorAt(angleRadians).ToVectorAt(angleRadians);
            double xPrime = xPrimeByParameter(angleRadians);
            double yPrime = yPrimeByParameter(angleRadians);
            return Vector.UnitNormalVector(xPrime, yPrime, Tolerance);
        }
        #endregion

        #region Methods: Curve Position
        /// <summary>
        /// The cartesian coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angle">Angle of rotation about the local origin</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateByAngle(Angle angle)
        {
            return CoordinateByAngle(angle.Radians);
        }

        /// <summary>
        /// The cartesian coordinate on the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the local origin, in radians.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateByAngle(double angleRadians)
        {
            double x = XbyRotationAboutOrigin(angleRadians);
            double y = YbyRotationAboutOrigin(angleRadians);
            return new CartesianCoordinate(x, y);
        }

        #region Focus, Right
        /// <summary>
        /// X-coordinate on the curve in local coordinates about the right (+X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the right (+X) focus, in radians.</param>
        /// <returns></returns>
        public abstract double XbyRotationAboutFocusRight(double angleRadians);

        /// <summary>
        /// Y-coordinate on the curve in local coordinates about the right (+X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the right (+X) focus, in radians.</param>
        /// <returns></returns>
        public double YbyRotationAboutFocusRight(double angleRadians)
        {
            return RadiusAboutFocusRight((Angle)angleRadians) * Trig.Sin(angleRadians);
        }

        /// <summary>
        /// The angle about the origin, in radians, determined by the angle about the right focus.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the right (+X) focus, in radians.</param>
        /// <returns>System.Double.</returns>
        public double RotationAboutOriginByRotationAboutFocusRight(double angleRadians)
        {
            double x = XbyRotationAboutFocusRight(angleRadians);
            double y = YbyRotationAboutFocusRight(angleRadians);
            return Trig.ArcTan(y / x);
        }

        /// <summary>
        /// The angle about the right (+X) focus, in radians, determined by the angle about the origin.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public double RotationAboutFocusRightByRotationAboutOrigin(double angleRadians)
        {
            double x = XbyRotationAboutOrigin(angleRadians);
            double y = XbyRotationAboutOrigin(angleRadians);
            return Trig.ArcTan(y / (x - DistanceFromFocusToOrigin));
        }
        #endregion
        #region Focus, Left
        /// <summary>
        /// X-coordinate on the curve in local coordinates about the left (-X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the left (-X) focus, in radians.</param>
        /// <returns></returns>
        public abstract double XbyRotationAboutFocusLeft(double angleRadians);

        /// <summary>
        /// Y-coordinate on the curve in local coordinates about the left (-X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the left (-X) focus, in radians.</param>
        /// <returns></returns>
        public double YbyRotationAboutFocusLeft(double angleRadians)
        {
            return RadiusAboutFocusLeft((Angle)angleRadians) * Trig.Sin(angleRadians);
        }

        /// <summary>
        /// The angle about the origin, in radians, determined by the angle about the right focus.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the left (-X) focus, in radians.</param>
        /// <returns>System.Double.</returns>
        public double RotationAboutOriginByRotationAboutFocusLeft(double angleRadians)
        {
            double x = XbyRotationAboutFocusLeft(angleRadians);
            double y = YbyRotationAboutFocusLeft(angleRadians);
            return Trig.ArcTan(y / x);
        }

        /// <summary>
        /// The angle about the left (-X) focus, in radians, determined by the angle about the origin.
        /// </summary>
        /// <param name="angleRadians">The angle radians.</param>
        /// <returns>System.Double.</returns>
        public double RotationAboutFocusLeftByRotationAboutOrigin(double angleRadians)
        {
            double x = XbyRotationAboutOrigin(angleRadians);
            double y = XbyRotationAboutOrigin(angleRadians);
            return Trig.ArcTan(y / (x + DistanceFromFocusToOrigin));
        }
        #endregion
        #endregion

        #region Methods: Protected Abstract       
        /// <summary>
        /// Distance from local origin to minor Vertex, b.
        /// </summary>
        /// <param name="a">Distance from local origin to major vertex, a.</param>
        /// <param name="c">Distance from local origin to the focus, c.</param>
        /// <returns>System.Double.</returns>
        protected abstract double distanceFromVertexMinorToOrigin(double a, double c);
        #endregion

        #region ICurvePositionPolar   
        /// <summary>
        /// The radius measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public virtual double RadiusAboutOrigin(double angleRadians)
        {
            return (XbyRotationAboutOrigin(angleRadians).Squared() + YbyRotationAboutOrigin(angleRadians).Squared()).Sqrt();
        }

        /// <summary>
        /// The radii measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public virtual double[] RadiiAboutOrigin(double angleRadians)
        {
            double radiusLeft = RadiusAboutFocusLeft(RotationAboutFocusLeftByRotationAboutOrigin(angleRadians));
            double radiusRight = RadiusAboutFocusRight(RotationAboutFocusRightByRotationAboutOrigin(angleRadians));
            return new[] { radiusLeft, radiusRight };
        }
        #endregion

        #region ICurvePositionCartesian
        /// <summary>
        /// X-coordinate on the curve that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public abstract double XatY(double y);

        /// <summary>
        /// Y-coordinate on the curve that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which y-coordinates are desired.</param>
        /// <returns></returns>
        public abstract double YatX(double x);

        /// <summary>
        /// X-coordinates on the curve that correspond to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which x-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        public abstract double[] XsAtY(double y);

        /// <summary>
        /// Y-coordinates on the curve that correspond to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public abstract double[] YsAtX(double x);

        /// <summary>
        /// Provided point lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if [is intersecting coordinate] [the specified coordinate]; otherwise, <c>false</c>.</returns>
        public abstract bool IsIntersectingCoordinate(CartesianCoordinate coordinate);
        #endregion

        #region ICurveLimits
        /// <summary>
        /// Length of the line segment.
        /// </summary>
        /// <returns>System.Double.</returns>
        public abstract double Length();

        /// <summary>
        /// Length of the curve between two points.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract double LengthBetween(double relativePositionStart, double relativePositionEnd);

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public abstract double ChordLength();

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public abstract double ChordLengthBetween(double relativePositionStart, double relativePositionEnd);

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public abstract LinearCurve Chord();

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the linear curve is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the linear curve is ended.</param>
        /// <returns>LinearCurve.</returns>
        public abstract LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd);

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
        /// <exception cref="NotImplementedException"></exception>
        public CartesianCoordinate CoordinateCartesian(double relativePosition)
        {
            return CoordinatePolar(relativePosition);
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract PolarCoordinate CoordinatePolar(double relativePosition);
        #endregion
    }
}
