// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-17-2020
// ***********************************************************************
// <copyright file="CircularCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Coordinates;
using MPT.Math.Curves.Tools.Intersections;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Trigonometry;
using MPT.Math.Vectors;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Class CircularCurve.
    /// Implements the <see cref="MPT.Math.Curves.ConicSectionEllipticCurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ConicSectionEllipticCurve" />
    public class CircularCurve : ConicSectionEllipticCurve
    {
        #region Properties        
        /// <summary>
        /// Gets the center control point.
        /// </summary>
        /// <value>The center control point</value>
        public CartesianCoordinate Center => _focusLocal;

        /// <summary>
        /// Gets the radius.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius => DistanceFromVertexMajorToOrigin;

        /// <summary>
        /// Gets the curvature.
        /// </summary>
        /// <value>The curvature.</value>
        public double Curvature => 1 / Radius;


        /// <summary>
        /// The eccentricity, e.
        /// A measure of how much the conic section deviates from being circular.
        /// Distance from any point on the conic section to its focus, divided by the perpendicular distance from that point to the nearest directrix.
        /// </summary>
        /// <value>The eccentricity.</value>
        public override double Eccentricity => 0;
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CircularCurve" /> class.
        /// </summary>
        /// <param name="vertex">Any vertex on the curve.</param>
        /// <param name="center">The center.</param>
        public CircularCurve(CartesianCoordinate vertex, CartesianCoordinate center) : base(vertex, 0, center)
        {
            _limitStartDefault = vertex;
            _limitEndDefault = _limitStartDefault;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularCurve" /> class.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="center">The center.</param>
        public CircularCurve(double radius, CartesianCoordinate center) : base(vertexMajor(radius, center), 0, center)
        {
            _limitStartDefault = vertexMajor(radius, CartesianCoordinate.Origin()); //center);
            _limitEndDefault = _limitStartDefault;
        }
        #endregion

        #region Curve Position
        /// <summary>
        /// +X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public override double XatY(double y)
        {
            return XsAtY(y)[0];
        }

        /// <summary>
        /// +Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        public override double YatX(double x)
        {
            return YsAtX(x)[0];
        }

        #endregion

        #region Methods: Query            
        /// <summary>
        /// Determines whether this instance has a chord.
        /// </summary>
        /// <returns><c>true</c> if this instance has a chord; otherwise, <c>false</c>.</returns>
        public bool HasChord()
        {
            return !IsCircle();
        }

        /// <summary>
        /// Determines whether this instance is a circle.
        /// </summary>
        /// <returns><c>true</c> if this instance is a circle; otherwise, <c>false</c>.</returns>
        public bool IsCircle()
        {
            return IsClosedCurve();
        }

        /// <summary>
        /// Determines whether the specified curve is intersecting.
        /// </summary>
        /// <param name="curve">The curve.</param>
        /// <returns><c>true</c> if the specified curve is intersecting; otherwise, <c>false</c>.</returns>
        public bool IsIntersecting(CircularCurve curve)
        {
            return AreIntersecting(this, curve);
        }

        /// <summary>
        /// Determines whether the specified curve is tangent.
        /// </summary>
        /// <param name="curve">The curve.</param>
        /// <returns><c>true</c> if the specified curve is tangent; otherwise, <c>false</c>.</returns>
        public bool IsTangent(CircularCurve curve)
        {
            return AreTangent(this, curve);
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
            return Radius;
        }

        /// <summary>
        /// The differential change in radius corresponding with a differential change in the angle, measured from the right focus (+X) as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        protected override double radiusAboutFocusRightPrime(double angleRadians)
        {
            return Radius;
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
            return Radius;
        }

        /// <summary>
        /// The radius measured from the left (-X) major vertex as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double RadiusAboutVertexMajorLeft(double angleRadians)
        {
            return 2 * Radius * TrigonometryLibrary.Cos(angleRadians);
        }
        #endregion
        /// <summary>
        /// The radius measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public override double RadiusAboutOrigin(double angleRadians)
        {
            return Radius;
        }
        #endregion

        /// <summary>
        /// Curvature of the curve in local coordinates about the local origin that corresponds to the parametric coordinate given.
        /// </summary>
        /// <param name="angleRadians">Parametric coordinate in radians.</param>
        /// <returns></returns>
        public override double CurvatureByAngle(double angleRadians)
        {
            return 1 / Radius;
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
                + " - Center: " + Center
                + ", Radius: " + Radius
                + ", I: " + _limitStartDefault + ", I: " + _limitEndDefault;
        }

        /// <summary>
        /// Returns points where the circular curve intersects the provided linear curve.
        /// </summary>
        /// <param name="otherLine">Linear curve that intersects the current linear curve.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate[] IntersectionCoordinate(LinearCurve otherLine)
        {
            return IntersectionLinearCircular.IntersectionCoordinates(otherLine, this);
        }

        /// <summary>
        /// Returns points where the circular curve intersects the provided circular curve.
        /// </summary>
        /// <param name="otherLine">Circular curve that intersects the current circular curve.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate[] IntersectionCoordinate(CircularCurve otherLine)
        {
            return IntersectionCircularCircular.IntersectionCoordinates(otherLine, this);
        }

        /// <summary>
        /// Coordinate of where a perpendicular projection intersects the provided coordinate.
        /// Returns infinity if the point is coincident with the circular curve center.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate[] CoordinateOfPerpendicularProjection(CartesianCoordinate point)
        {
            return CoordinateOfPerpendicularProjection(point, this);
        }

        /// <summary>
        /// The length within the provided rotation along a circular curve.
        /// </summary>
        /// <param name="rotation">Rotation to get arc length between.</param>
        /// <returns>System.Double.</returns>
        public override double LengthBetween(AngularOffset rotation)
        {
            return LengthBetween(rotation, Radius);
        }
        #endregion

        #region ICurveLimits
        /// <summary>
        /// Length of the curve between the limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public override double Length()
        {
            return 2 * Numbers.Pi * Radius;
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
        /// X-coordinates on the curve that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which x-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        public override double[] XsAtY(double y)
        {
            double x = (Radius.Squared() - y.Squared()).Sqrt();
            return new[] { x, -x };
        }

        /// <summary>
        /// Y-coordinates on the curve that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which y-coordinates are desired.</param>
        /// <returns>System.Double.</returns>
        public override double[] YsAtX(double x)
        {
            double y = (Radius.Squared() - x.Squared()).Sqrt();
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
            return new[] { Radius };
        }
        #endregion

        #region Methods: Static      
        /// <summary>
        /// The length between the provided points along a circular curve, assumed to be about the origin.
        /// </summary>
        /// <param name="pointI">Point i.</param>
        /// <param name="pointJ">Point j.</param>
        /// <returns>System.Double.</returns>
        public static double LengthBetween(CartesianCoordinate pointI, CartesianCoordinate pointJ)
        {
            AngularOffset angle = AngularOffset.CreateFromPoints(pointI, CartesianCoordinate.Origin(), pointJ);
            double radius = pointI.OffsetFrom(CartesianCoordinate.Origin()).Length();
            return LengthBetween(angle, radius);
        }

        /// <summary>
        /// The length between the provided points along a circular curve.
        /// </summary>
        /// <param name="pointI">Point i.</param>
        /// <param name="pointJ">Point j.</param>
        /// <param name="radius">Arc radius</param>
        /// <returns>System.Double.</returns>
        public static double LengthBetween(CartesianCoordinate pointI, CartesianCoordinate pointJ, double radius)
        {
            IntersectionCircularCircular intersection = new IntersectionCircularCircular(
                new CircularCurve(radius, pointI),
                new CircularCurve(radius, pointJ));

            // Shape is symmetric, so it doesn't matter if the 1st or 2nd intersection coordinate is taken.
            CartesianCoordinate center = intersection.IntersectionCoordinates()[0]; 
            AngularOffset angle = AngularOffset.CreateFromPoints(pointI, center, pointJ);
            return LengthBetween(angle, radius);
        }

        /// <summary>
        /// The length within the provided rotation along a circular curve.
        /// </summary>
        /// <param name="rotation">Rotation to get arc length between.</param>
        /// <param name="radius">Arc radius</param>
        /// <returns>System.Double.</returns>
        public static double LengthBetween(AngularOffset rotation, double radius)
        {
            return rotation.LengthArc(radius);
        }

        #region Aligment        
        /// <summary>
        /// Determines if the curves are tangent to each other.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool AreTangent(CircularCurve curve1, CircularCurve curve2)
        {
            return IntersectionCircularCircular.AreTangent(curve1, curve2);
        }
        #endregion

        #region Intersect        
        /// <summary>
        /// Determines if the curves intersect each other.
        /// </summary>
        /// <param name="curve1">The curve1.</param>
        /// <param name="curve2">The curve2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool AreIntersecting(CircularCurve curve1, CircularCurve curve2)
        {
            return IntersectionCircularCircular.AreIntersecting(curve1, curve2);
        }
        #endregion

        #region Projection
        /// <summary>
        /// Coordinate of where a perpendicular projection intersects the provided coordinate.
        /// Returns infinity if the point is coincident with the circular curve center.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="referenceArc">The line to which a perpendicular projection is drawn.</param>
        /// <returns>CartesianCoordinate.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static CartesianCoordinate[] CoordinateOfPerpendicularProjection(CartesianCoordinate point, CircularCurve referenceArc)
        {
            if (point == referenceArc.Center)
            {
                return new CartesianCoordinate[]
                    {
                    new CartesianCoordinate(double.PositiveInfinity, double.PositiveInfinity),
                    new CartesianCoordinate(double.NegativeInfinity, double.NegativeInfinity)
                    };
            }

            LinearCurve ray = new LinearCurve(referenceArc.Center, point);
            return referenceArc.IntersectionCoordinate(ray);
        }
        #endregion

        #region Protected
        /// <summary>
        /// Gets the major vertex.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <param name="center">The center.</param>
        /// <returns>CartesianCoordinate.</returns>
        protected static CartesianCoordinate vertexMajor(double radius, CartesianCoordinate center)
        {
            return new CartesianCoordinate(center.X + radius, center.Y);
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
        public CircularCurve CloneCurve()
        {
            CircularCurve curve = new CircularCurve(_vertexMajorLocal, _focusLocal);
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
