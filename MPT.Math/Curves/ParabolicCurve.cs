﻿// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="ParabolicCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics;
using MPT.Math.Curves.Parametrics.ConicSectionCurves.Parabolics;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Trigonometry;
using MPT.Math.Vectors;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// A parabola is the set of all points whose distance from a fixed point, called the focus, is equal to the distance from a fixed line, called the directrix.
    /// Implements the <see cref="MPT.Math.Curves.ConicSectionCurve" />
    /// <a href="https://en.wikipedia.org/wiki/Parabola">Wikipedia</a>.
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ConicSectionCurve" />
    public class ParabolicCurve : ConicSectionCurve
    {
        #region Properties
        /// <summary>
        /// Gets the directrices, in local coordinates.
        /// </summary>
        /// <value>The directrices.</value>
        public override Tuple<LinearCurve, LinearCurve> Directrices
        {
            get
            {
                return new Tuple<LinearCurve, LinearCurve>
                    (
                    new LinearCurve(_directrixILocal, _directrixJLocal),
                    new LinearCurve(_directrixILocal, _directrixJLocal)
                    );
            }
        }
        /// <summary>
        /// Distance from local origin to the directrix line, Xe.
        /// </summary>
        /// <value>The distance from directrix to origin.</value>
        public override double DistanceFromDirectrixToOrigin => -1 * DistanceFromVertexMajorToOrigin;

        /// <summary>
        /// Gets the major vertices, a, in local coordinates, which are the points on a conic section that lie closest to the directrices.
        /// </summary>
        /// <value>The vertices.</value>
        public override Tuple<CartesianCoordinate, CartesianCoordinate> VerticesMajor
        {
            get
            {
                return new Tuple<CartesianCoordinate, CartesianCoordinate>(_vertexMajorLocal, _vertexMajorLocal);
            }
        }

        /// <summary>
        /// Distance from the focus to the directrix, p.
        /// </summary>
        /// <value>The distance from focus to directrix.</value>
        public override double DistanceFromFocusToDirectrix => 2 * DistanceFromVertexMajorToOrigin;


        /// <summary>
        /// The eccentricity, e.
        /// A measure of how much the conic section deviates from being circular.
        /// Distance from any point on the conic section to its focus, divided by the perpendicular distance from that point to the nearest directrix.
        /// </summary>
        /// <value>The eccentricity.</value>
        public override double Eccentricity => 1;
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicCurve" /> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, a.</param>
        /// <param name="distanceFromFocusToLocalOrigin">The distance from focus, c, to local origin.</param>
        /// <param name="localOrigin">The coordinate of the local origin.</param>
        public ParabolicCurve(
            CartesianCoordinate vertexMajor,
            double distanceFromFocusToLocalOrigin,
            CartesianCoordinate localOrigin)
            : base(vertexMajor, distanceFromFocusToLocalOrigin, localOrigin)
        {
            initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurve" /> class.
        /// </summary>
        /// <param name="a">Distance from local origin to major vertex, a.</param>
        /// <param name="center">The center.</param>
        /// <param name="rotation">The rotation.</param>
        public ParabolicCurve(double a, CartesianCoordinate center, Angle rotation) : base(center.OffsetCoordinate(a, rotation), a, center)
        {
            initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void initialize()
        {
            _directrixILocal = new CartesianCoordinate(-1 * DistanceFromDirectrixToOrigin, 1);
            _directrixJLocal = new CartesianCoordinate(-1 * DistanceFromDirectrixToOrigin, -1);
        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override LinearParametricEquation createParametricEquation()
        {
            return new ParabolicParametric(this);
        }
        #endregion

        #region Curve Position
        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public override double XatY(double y)
        {
            return y.Squared() / (4 * DistanceFromVertexMajorToOrigin);
        }

        /// <summary>
        /// +Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a +y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return 2 * (DistanceFromVertexMajorToOrigin * x).Sqrt();
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusRight(double angleRadians)
        {
            return DistanceFromVertexMajorToOrigin + RadiusAboutFocusRight(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the left (-X) focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the left (-X) focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusLeft(double angleRadians)
        {
            return XbyRotationAboutFocusRight(angleRadians);
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
                +", I: " + _limitStartDefault + ", J: " + _limitEndDefault;
        }
        #endregion
        
        #region ICurveLimits  
        /// <summary>
        /// Length of the curve.
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
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public override double ChordLength()
        {
            return LinearCurve.Length(_range.Start.Limit, _range.End.Limit);
        }

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public override double ChordLengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public override LinearCurve Chord()
        {
            return new LinearCurve(_range.Start.Limit, _range.End.Limit);
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the linear curve is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the linear curve is ended.</param>
        /// <returns>LinearCurve.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        public override Vector TangentVector(double relativePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
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
        public override PolarCoordinate CoordinatePolar(double relativePosition)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ICurvePositionCartesian        
        /// <summary>
        /// X-coordinates on the curve that correspond to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which x-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        public override double[] XsAtY(double y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Y-coordinates on the curve that correspond to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public override double[] YsAtX(double x)
        {
            throw new NotImplementedException();
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

        #region Methods: Static
        #region Protected

        #endregion
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
            return 0;
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
        public ParabolicCurve CloneCurve()
        {
            ParabolicCurve curve = new ParabolicCurve(_vertexMajorLocal, DistanceFromFocusToOrigin, _originLocal);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
