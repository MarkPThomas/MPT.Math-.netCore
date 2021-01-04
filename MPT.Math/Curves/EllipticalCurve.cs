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
        #region Initialization                
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurve" /> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, a.</param>
        /// <param name="distanceFromFocusToLocalOrigin">The distance from focus, c, to local origin.</param>
        /// <param name="localOrigin">The coordinate of the local origin.</param>
        public EllipticalCurve(
            CartesianCoordinate vertexMajor, 
            double distanceFromFocusToLocalOrigin, 
            CartesianCoordinate localOrigin) 
            : base(vertexMajor, distanceFromFocusToLocalOrigin, localOrigin)
        {
            _limitStartDefault = vertexMajor;
            _limitEndDefault = _limitStartDefault;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurve" /> class.
        /// </summary>
        /// <param name="a">Distance from local origin to major vertex, a.</param>
        /// <param name="b">Distance from local origin to minor vertex, b.</param>
        /// <param name="center">The center.</param>
        /// <param name="rotation">The rotation.</param>
        public EllipticalCurve(
            double a, 
            double b, 
            CartesianCoordinate center, 
            Angle rotation) 
            : base(center.OffsetCoordinate(a, rotation), distanceFromFocusToOrigin(a, b), center)
        {
            _limitStartDefault = center.OffsetCoordinate(a, rotation);
            _limitEndDefault = _limitStartDefault;
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
            return DistanceFromVertexMajorToOrigin * (1 - (y / DistanceFromVertexMinorToOrigin).Squared()).Sqrt();
        }

        /// <summary>
        /// +Y-coordinate on the curve that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return DistanceFromVertexMinorToOrigin * (1 - (x / DistanceFromVertexMajorToOrigin).Squared()).Sqrt();
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
            return LengthBetween(rotation, DistanceFromVertexMajorToOrigin, DistanceFromVertexMinorToOrigin);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() 
                + " - Center: " + _originLocal
                + ", - Rotation: " + _localRotation
                + ", a: " + DistanceFromVertexMajorToOrigin
                + ", b: " + DistanceFromVertexMinorToOrigin
                + ", I: " + _limitStartDefault + ", J: " + _limitEndDefault;
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

        /// <summary>
        /// Provided point lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if [is intersecting coordinate] [the specified coordinate]; otherwise, <c>false</c>.</returns>
        public override bool IsIntersectingCoordinate(CartesianCoordinate coordinate)
        {
            throw new NotImplementedException();
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
        #endregion
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
            EllipticalCurve curve = new EllipticalCurve(_vertexMajorLocal, DistanceFromFocusToOrigin, _originLocal);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
