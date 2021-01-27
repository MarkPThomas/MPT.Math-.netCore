// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-16-2020
// ***********************************************************************
// <copyright file="HyperbolicCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics.Components;
using MPT.Math.Curves.Parametrics.ConicSectionCurves;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Trigonometry;
using MPT.Math.Vectors;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// A hyperbola is the set of all points where the difference between their distances from two fixed points (the foci) is constant. 
    /// In the case of a hyperbola, there are two foci and two directrices. Hyperbolas also have two asymptotes.
    /// Implements the <see cref="MPT.Math.Curves.ConicSectionCurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ConicSectionCurve" />
    public class HyperbolicCurve : ConicSectionCurve
    {
        #region Properties        
        /// <summary>
        /// Distance from local origin to the focus, c.
        /// </summary>
        /// <value>The distance from focus to origin.</value>
        public override double DistanceFromFocusToLocalOrigin => distanceFromFocusToVertexMajor() + DistanceFromVertexMajorToLocalOrigin;

        /// <summary>
        /// Distance from the focus to the curve along a line perpendicular to the major axis and the focus, p.
        /// </summary>
        /// <value>The p.</value>
        public override double SemilatusRectumDistance => DistanceFromVertexMajorToLocalOrigin * (Eccentricity.Squared() - 1);

        /// <summary>
        /// Gets the asymptotes.
        /// </summary>
        /// <value>The asymptotes.</value>
        public Tuple<LinearCurve, LinearCurve> Asymptotes
        {
            get
            {
                Tuple<CartesianCoordinate, CartesianCoordinate> minorVertices = getVerticesMinor();
                return new Tuple<LinearCurve, LinearCurve>
                    (
                        new LinearCurve(LocalOrigin, minorVertices.Item1, Tolerance),
                        new LinearCurve(LocalOrigin, minorVertices.Item2, Tolerance)
                    );
            }
        }

        /// <summary>
        /// Gets the second focus, which lies to the left of the local origin.
        /// </summary>
        /// <value>The focus2.</value>
        public CartesianCoordinate Focus2 => CartesianCoordinate.OffsetCoordinate(_focus, -2 * DistanceFromFocusToLocalOrigin, _rotation);

        /// <summary>
        /// Gets the second set of minor vertices, b which lie to the left of the local origin, along a line perpendicular to a line passing through the major vertex, a, and focus.
        /// </summary>
        /// <value>The conjugate vertices.</value>
        public virtual Tuple<CartesianCoordinate, CartesianCoordinate> VerticesMinor2
        {
            get
            {
                Tuple<CartesianCoordinate, CartesianCoordinate> minorVertices = getVerticesMinor();
                return new Tuple < CartesianCoordinate, CartesianCoordinate >(
                    CartesianCoordinate.OffsetCoordinate(minorVertices.Item1, -2 * DistanceFromVertexMajorToLocalOrigin, _rotation),
                    CartesianCoordinate.OffsetCoordinate(minorVertices.Item2, -2 * DistanceFromVertexMajorToLocalOrigin, _rotation));
            }
        }

        /// <summary>
        /// Gets the second directrix, Xe, which lies to the left of the local origin.
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
        /// Initializes a new instance of the <see cref="HyperbolicCurve"/> class.
        /// </summary>
        /// <param name="a">Distance, a, from local origin to major vertex, M, which lies at the apex of the curve.</param>
        /// <param name="vertexMajor">The major vertex, M, which lies at the peak of the parabola.</param>
        /// <param name="focus">The focus, f.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public HyperbolicCurve(
            double a,
            CartesianCoordinate vertexMajor,
            CartesianCoordinate focus,
            double tolerance = DEFAULT_TOLERANCE) 
            : base(
                  vertexMajor, 
                  focus, 
                  a, 
                  tolerance)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalCurve" /> class.
        /// </summary>
        /// <param name="a">Distance, a, from local origin to major vertex, M, which lies at the apex of the curve.</param>
        /// <param name="b">Distance, b, from local origin to minor vertex, M, which defines the asymptote that passes through the center.</param>
        /// <param name="center">The center.</param>
        /// <param name="rotation">The rotation offset from the horizontal x-axis.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public HyperbolicCurve(
            double a,
            double b,
            CartesianCoordinate center,
            Angle rotation,
            double tolerance = DEFAULT_TOLERANCE)
            : base(
                  center.OffsetCoordinate(a, rotation), 
                  distanceFromFocusToOrigin(a, b) - a, 
                  a,    
                  rotation, 
                  tolerance)
        {
            _focus = center.OffsetCoordinate(DistanceFromFocusToLocalOrigin, rotation);
            _focus.Tolerance = tolerance;
        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override CartesianParametricEquationXY createParametricEquation()
        {
            return new HyperbolicCurveParametric(this);
        }
        #endregion

        #region Curve Position
        /// <summary>
        /// +X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which a +x-coordinate is desired.</param>
        /// <returns></returns>
        public override double XatY(double y)
        {
            return DistanceFromVertexMajorToLocalOrigin * (1 + (y / DistanceFromVertexMinorToMajorAxis).Squared()).Sqrt();
        }

        /// <summary>
        /// +Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a +y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return DistanceFromVertexMinorToMajorAxis * ((x / DistanceFromVertexMajorToLocalOrigin).Squared() - 1).Sqrt();
        }

        /// <summary>
        /// X-coordinate on the right curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public double XbyRotationAboutOriginRight(double angleRadians)
        {
            return XbyRotationAboutOrigin(angleRadians);
        }

        /// <summary>
        /// X-coordinate on the left curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// <a href="https://www.ck12.org/book/ck-12-calculus-concepts/section/10.3/">Reference</a>.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public double XbyRotationAboutOriginLeft(double angleRadians)
        {
            return -1 * XbyRotationAboutOrigin(angleRadians);
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

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return typeof(HyperbolicCurve).Name
                + " - Center: {X: " + LocalOrigin.X + ", Y: " + LocalOrigin.Y + "}"
                + ", Rotation: " + _rotation.Radians + " rad"
                + ", a: " + DistanceFromVertexMajorToLocalOrigin
                + ", b: " + DistanceFromVertexMinorToMajorAxis
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

        #region Methods: Protected
        /// <summary>
        /// The coordinate of the local origin.
        /// </summary>
        /// <value>The local origin.</value>
        protected override CartesianCoordinate getLocalOrigin()
        {
            return _vertexMajor.OffsetCoordinate(-DistanceFromVertexMajorToLocalOrigin, _rotation);
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
        /// Gets the minor vertices.
        /// </summary>
        /// <returns>Tuple&lt;CartesianCoordinate, CartesianCoordinate&gt;.</returns>
        protected override Tuple<CartesianCoordinate, CartesianCoordinate> getVerticesMinor()
        {
            return getVerticesMinor(_vertexMajor);
        }

        /// <summary>
        /// Distance, b, from minor vertex, m, to the major axis, which is a line that passes through the major vertex, M, and the focus, f.
        /// </summary>
        /// <param name="a">Distance, a, from local origin to major vertex, M.</param>
        /// <param name="c">Distance, c, from local origin to the focus, f.</param>
        /// <returns>System.Double.</returns>
        protected override double distanceFromVertexMinorToMajorAxis(double a, double c)
        {
            return (c.Squared() - a.Squared()).Sqrt();
        }
        #endregion

        #region Methods: Static
        #region Protected
        /// <summary>
        /// Distance, c, from focus,f, to origin.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Double.</returns>
        protected static double distanceFromFocusToOrigin(double a, double b)
        {
            return AlgebraLibrary.SRSS(a, b);
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
        public HyperbolicCurve CloneCurve()
        {
            HyperbolicCurve curve = new HyperbolicCurve(DistanceFromVertexMajorToLocalOrigin, _vertexMajor, _focus, _tolerance);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
