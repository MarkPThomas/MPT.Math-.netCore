// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="EllipticalCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// An ellipse is the set of all points for which the sum of the distances from two fixed points (the foci) is constant. In the case of an ellipse, there are two foci, and two directrices.
    /// Implements the <see cref="MPT.Math.Curves.ConicSectionEllipticCurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ConicSectionEllipticCurve" />
    public class EllipticalCurve : ConicSectionEllipticCurve
    {
        #region Properties
        /// <summary>
        /// Distance from local origin to the focus, c.
        /// </summary>
        /// <value>The distance from focus to origin.</value>
        public override double DistanceFromFocusToLocalOrigin => DistanceFromVertexMajorToLocalOrigin - CartesianOffset.Separation(_focus, _vertexMajor);

        /// <summary>
        /// Distance from the focus to the curve along a line perpendicular to the major axis and the focus, p.
        /// </summary>
        /// <value>The p.</value>
        public override double SemilatusRectumDistance => DistanceFromVertexMajorToLocalOrigin * (1 - Eccentricity.Squared());

        /// <summary>
        /// Gets the second focus, which lies to the right of the local origin.
        /// </summary>
        /// <value>The focus2.</value>
        public CartesianCoordinate Focus2 => _focus.OffsetCoordinate(-2 * DistanceFromFocusToLocalOrigin, _rotation);

        /// <summary>
        /// Gets the second directrix, Xe, which lies to the right of the local origin.
        /// </summary>
        /// <value>The directrix.</value>
        public virtual LinearCurve Directrix2
        {
            get
            {
                Tuple<CartesianCoordinate, CartesianCoordinate> directrices = getVerticesDirectrix();
                return new LinearCurve(
                    directrices.Item1.OffsetCoordinate(-2 * DistanceFromDirectrixToLocalOrigin, _rotation),
                    directrices.Item2.OffsetCoordinate(-2 * DistanceFromDirectrixToLocalOrigin, _rotation), 
                    _tolerance);
            }
        }
        #endregion

        #region Initialization                
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurve" /> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, M.</param>
        /// <param name="b">Distance, b, from local origin to minor vertex, m.</param>
        /// <param name="center">The coordinate of the local origin.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public EllipticalCurve(
            CartesianCoordinate vertexMajor,
            double b,
            CartesianCoordinate center,
            double tolerance = DEFAULT_TOLERANCE) 
            : base(
                  vertexMajor,
                  getDistanceFromMajorVertexToFocus(vertexMajor, center, b),
                  getDistanceFromMajorVertexToLocalOrigin(vertexMajor, center),
                  getRotation(vertexMajor, center),
                  tolerance)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurve" /> class.
        /// </summary>
        /// <param name="a">Distance, a, from local origin to major vertex, M.</param>
        /// <param name="b">Distance, b, from local origin to minor vertex, m.</param>
        /// <param name="center">The coordinate of the local origin.</param>
        /// <param name="rotation">The rotation offset from the horizontal x-axis.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public EllipticalCurve(
            double a, 
            double b, 
            CartesianCoordinate center, 
            Angle rotation,
            double tolerance = DEFAULT_TOLERANCE) 
            : base(
                  getMajorVertex(center, a, rotation),
                  getDistanceFromMajorVertexToFocus(a, b), 
                  a, 
                  rotation,
                  tolerance)
        {

        }
        #endregion

        #region Curve Position
        /// <summary>
        /// +X-coordinate on the curve that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public override double XatY(double y)
        {
            return DistanceFromVertexMajorToLocalOrigin * (1 - (y / DistanceFromVertexMinorToMajorAxis).Squared()).Sqrt();
        }

        /// <summary>
        /// +Y-coordinate on the curve that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return DistanceFromVertexMinorToMajorAxis * (1 - (x / DistanceFromVertexMajorToLocalOrigin).Squared()).Sqrt();
        }
        #endregion

        #region Methods: Properties
        /// <summary>
        /// The length within the provided rotation along an elliptical curve.
        /// </summary>
        /// <param name="rotation">Rotation to get arc length between.</param>
        /// <returns>System.Double.</returns>
        public override double LengthBetween(AngularOffset rotation)
        {
            return LengthBetween(rotation, DistanceFromVertexMajorToLocalOrigin, DistanceFromVertexMinorToMajorAxis);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return typeof(EllipticalCurve).Name
                + " - Center: {X: " + LocalOrigin.X + ", Y: " + LocalOrigin.Y + "}"
                + ", Rotation: " + _rotation.Radians + " rad"
                + ", a: " + DistanceFromVertexMajorToLocalOrigin
                + ", b: " + DistanceFromVertexMinorToMajorAxis
                + ", I: {X: " + _limitStartDefault.X + ", Y: " + _limitStartDefault.Y + "}, J: {X: " + _limitEndDefault.X + ", Y: " + _limitEndDefault.Y + "}";
        }
        #endregion

        #region ICurveLimits 
        /// <summary>
        /// Length of the curve between the limits.
        /// <a href="https://www.mathsisfun.com/geometry/ellipse-perimeter.html">Reference</a>.
        /// </summary>
        /// <returns>System.Double.</returns>
        public override double Length()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Length of the curve between two points.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public override double LengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative length along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Vector TangentVector(double relativePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative length along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Vector NormalVector(double relativePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override PolarCoordinate CoordinatePolar(double relativePosition)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ICurvePositionCartesian
        /// <summary>
        /// X-coordinates on the curve that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which x-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        public override double[] XsAtY(double y)
        {
            double x = XatY(y);
            return new[] { x, -x };
        }

        /// <summary>
        /// Y-coordinates on the curve that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which y-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        public override double[] YsAtX(double x)
        {
            double y = YatX(x);
            return new[] { y, -y };
        }
        #endregion

        #region ICurvePositionPolar
        /// <summary>
        /// The radii measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double[] RadiiAboutOrigin(double angleRadians)
        {
            double radiusLeft = RadiusAboutFocusLeft(RotationAboutFocusLeftByRotationAboutOrigin(angleRadians));
            double radiusRight = RadiusAboutFocusRight(RotationAboutFocusRightByRotationAboutOrigin(angleRadians));
            return new[] { radiusLeft, radiusRight };
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// The length within the provided rotation along an elliptical curve.
        /// </summary>
        /// <param name="rotation">Rotation to get arc length between.</param>
        /// <param name="majorAxisLength">The length from the origin to major vertex, a.</param>
        /// <param name="minorAxisLength">The length from the origin to minor vertex, b.</param>
        /// <returns>System.Double.</returns>
        public static double LengthBetween(AngularOffset rotation, double majorAxisLength, double minorAxisLength)
        {
            // https://math.stackexchange.com/questions/2901129/what-is-the-fastest-way-to-estimate-the-arc-length-of-an-ellipse
            throw new NotImplementedException();
        }

        #region Protected
        /// <summary>
        /// Distances from focus to origin.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        protected static double distanceFromFocusToOrigin(double a, double b)
        {
            return (a.Squared() - b.Squared()).Sqrt();
        }

        /// <summary>
        /// Gets the distance from major vertex to focus.
        /// </summary>
        /// <param name="vertexMajor">The vertex major.</param>
        /// <param name="localOrigin">The local origin.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        protected static double getDistanceFromMajorVertexToFocus(CartesianCoordinate vertexMajor, CartesianCoordinate localOrigin, double b)
        {
            double a = getDistanceFromMajorVertexToLocalOrigin(vertexMajor, localOrigin);
            return getDistanceFromMajorVertexToFocus(a, b);
        }

        /// <summary>
        /// Gets the distance from major vertex to focus.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        protected static double getDistanceFromMajorVertexToFocus(double a, double b)
        {
            return a - distanceFromFocusToOrigin(a, b);
        }
        #endregion
        #endregion

        #region Methods: Protected
        /// <summary>
        /// The coordinate of the local origin.
        /// </summary>
        /// <value>The local origin.</value>
        protected override CartesianCoordinate getLocalOrigin()
        {
            return _focus.OffsetCoordinate(-DistanceFromFocusToLocalOrigin, _rotation);
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return CloneCurve();
        }

        /// <summary>
        /// Clones the curve.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public EllipticalCurve CloneCurve()
        {
            EllipticalCurve curve = new EllipticalCurve(_vertexMajor, DistanceFromVertexMinorToMajorAxis, LocalOrigin, _tolerance);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
