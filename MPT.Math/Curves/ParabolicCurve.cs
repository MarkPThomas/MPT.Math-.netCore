// ***********************************************************************
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
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics;
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
        /// Distance from local origin to the focus, c.
        /// </summary>
        /// <value>The distance from focus to origin.</value>
        public override double DistanceFromFocusToLocalOrigin => distanceFromFocusToVertexMajor();

        /// <summary>
        /// Distance from local origin to the directrix line, Xe.
        /// </summary>
        /// <value>The distance from directrix to origin.</value>
        public override double DistanceFromDirectrixToLocalOrigin => DistanceFromFocusToLocalOrigin;

        /// <summary>
        /// Distance from the focus to the directrix, p.
        /// </summary>
        /// <value>The distance from focus to directrix.</value>
        public override double DistanceFromFocusToDirectrix => SemilatusRectumDistance;

        /// <summary>
        /// The eccentricity, e.
        /// A measure of how much the conic section deviates from being circular.
        /// Distance from any point on the conic section to its focus, divided by the perpendicular distance from that point to the nearest directrix.
        /// </summary>
        /// <value>The eccentricity.</value>
        public override double Eccentricity => 1;

        /// <summary>
        /// Distance from the focus to the curve along a line perpendicular to the major axis and the focus, p.
        /// </summary>
        /// <value>The p.</value>
        public override double SemilatusRectumDistance => 2 * DistanceFromFocusToLocalOrigin;
        #endregion

        #region Initialization            
        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicCurve"/> class.
        /// </summary>
        /// <param name="vertexMajor">The major vertex, M, which lies at the peak of the parabola.</param>
        /// <param name="focus">The focus, f.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public ParabolicCurve(
            CartesianCoordinate vertexMajor, 
            CartesianCoordinate focus, 
            double tolerance = DEFAULT_TOLERANCE) : base(vertexMajor, focus, 0, tolerance)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParabolicCurve" /> class.
        /// </summary>
        /// <param name="a">Distance, a, from major vertex, M, to the focus, f.</param>
        /// <param name="center">The center.</param>
        /// <param name="rotation">The rotation offset from the horizontal x-axis.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public ParabolicCurve(
            double a,
            CartesianCoordinate center,
            Angle rotation,
            double tolerance = DEFAULT_TOLERANCE) : base(center, a, 0, rotation, tolerance)
        {
            _focus = _vertexMajor.OffsetCoordinate(a, rotation);
        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override CartesianParametricEquationXY createParametricEquation()
        {
            return new ParabolicCurveParametric(this);
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
            return y.Squared() / (4 * DistanceFromVertexMajorToLocalOrigin);
        }

        /// <summary>
        /// +Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a +y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return 2 * (DistanceFromVertexMajorToLocalOrigin * x).Sqrt();
        }

        /// <summary>
        /// X-coordinate on the curve in local coordinates about the focus that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Angle of rotation about the focus, in radians.</param>
        /// <returns></returns>
        public override double XbyRotationAboutFocusRight(double angleRadians)
        {
            return DistanceFromVertexMajorToLocalOrigin + RadiusAboutFocusRight(angleRadians) * TrigonometryLibrary.Cos(angleRadians);
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
            return typeof(ParabolicCurve).Name
                + " - Center: {X: " + LocalOrigin.X + ", Y: " + LocalOrigin.Y + "}"
                + ", Rotation: " + _rotation.Radians + " rad"
                + ", c: " + DistanceFromFocusToLocalOrigin
                + ", I: {X: " + _limitStartDefault.X + ", Y: " + _limitStartDefault.Y + "}, J: {X: " + _limitEndDefault.X + ", Y: " + _limitEndDefault.Y + "}";
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
        #endregion

        #region Methods: Static
        #region Protected

        #endregion
        #endregion

        #region Methods: Protected  
        /// <summary>
        /// The coordinate of the local origin.
        /// </summary>
        /// <value>The local origin.</value>
        protected override CartesianCoordinate getLocalOrigin()
        {
            return _vertexMajor;
        }

        /// <summary>
        /// Gets the minor vertices.
        /// </summary>
        /// <returns>Tuple&lt;CartesianCoordinate, CartesianCoordinate&gt;.</returns>
        protected override Tuple<CartesianCoordinate, CartesianCoordinate> getVerticesMinor()
        {
            return getVerticesMinor(_focus);
        }

        /// <summary>
        /// Distance from local origin to minor Vertex, b.
        /// </summary>
        /// <param name="a">Distance, a, from local origin to major vertex, M.</param>
        /// <param name="c">Distance, c, from local origin to the focus, f.</param>
        /// <returns>System.Double.</returns>
        protected override double distanceFromVertexMinorToMajorAxis(double a, double c)
        {
            return 2 * c;
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
            ParabolicCurve curve = new ParabolicCurve(_vertexMajor, _focus, _tolerance);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
