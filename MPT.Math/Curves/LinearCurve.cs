// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark P Thomas
// Created          : 06-07-2020
//
// Last Modified By : Mark P Thomas
// Last Modified On : 11-22-2020
// ***********************************************************************
// <copyright file="LinearCurve.cs" company="Mark P Thomas, Inc.">
//     Copyright (c) 2020. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.Curves.Parametrics;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using Trig = MPT.Math.Trigonometry.TrigonometryLibrary;
using System;
using MPT.Math.Curves.Parametrics.Linear;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Class LinearCurve.
    /// Implements the <see cref="MPT.Math.Curves.ICurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ICurve" />
    public class LinearCurve : Curve, 
        ICurveLimits, 
        ICurvePositionCartesian, ICurvePositionPolar,
        IPerpendicularProjections
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
                setTolerances(value);
            }
        }

        /// <summary>
        /// The control point i
        /// </summary>
        protected CartesianCoordinate _controlPointI;
        /// <summary>
        /// Gets the control point i.
        /// </summary>
        /// <value>The control point i.</value>
        public CartesianCoordinate ControlPointI => _controlPointI;

        /// <summary>
        /// The control point j
        /// </summary>
        protected CartesianCoordinate _controlPointJ;
        /// <summary>
        /// Gets the control point j.
        /// </summary>
        /// <value>The control point j.</value>
        public CartesianCoordinate ControlPointJ => _controlPointJ;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the linear curve to pass through the provided points.
        /// </summary>
        /// <param name="i">First point of the line.</param>
        /// <param name="j">Second point of the line.</param>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        public LinearCurve(CartesianCoordinate i, CartesianCoordinate j, double tolerance = DEFAULT_TOLERANCE) : base(tolerance)
        {
            _controlPointI = i;
            _controlPointJ = j;
            _limitStartDefault = _controlPointI;
            _limitEndDefault = _controlPointJ;

            setTolerances(tolerance);
        }

        /// <summary>
        /// Sets the tolerances.
        /// </summary>
        /// <param name="tolerance">Tolerance to apply to the curve.</param>
        protected void setTolerances(double tolerance = DEFAULT_TOLERANCE)
        {
            _controlPointI.Tolerance = tolerance;
            _controlPointJ.Tolerance = tolerance;
        }

        /// <summary>
        /// Creates the parametric vector.
        /// </summary>
        /// <returns>VectorParametric.</returns>
        protected override LinearParametricEquation createParametricEquation()
        {
            return new LinearParametric(this);
        }

        /// <summary>
        /// Returns a curve based on the slope and y-intercept.
        /// </summary>
        /// <param name="slope">The slope.</param>
        /// <param name="yIntercept">The y intercept.</param>
        /// <returns>LinearCurve.</returns>
        public static LinearCurve CurveByYIntercept(double slope, double yIntercept)
        {
            return new LinearCurve(new CartesianCoordinate(0, yIntercept), new CartesianCoordinate(1, yIntercept + slope));
        }

        /// <summary>
        /// Returns a curve based on the slope and x-intercept.
        /// </summary>
        /// <param name="slope">The slope.</param>
        /// <param name="xIntercept">The x intercept.</param>
        /// <returns>LinearCurve.</returns>
        public static LinearCurve CurveByXIntercept(double slope, double xIntercept)
        {
            return new LinearCurve(new CartesianCoordinate(xIntercept, 0), new CartesianCoordinate(xIntercept + 1, slope));
        }
        #endregion

        #region Methods: Query
        /// <summary>
        /// Determines whether this instance is horizontal.
        /// </summary>
        /// <returns><c>true</c> if this instance is horizontal; otherwise, <c>false</c>.</returns>
        public bool IsHorizontal()
        {
            return IsHorizontal(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// Determines whether this instance is vertical.
        /// </summary>
        /// <returns><c>true</c> if this instance is vertical; otherwise, <c>false</c>.</returns>
        public bool IsVertical()
        {
            return IsVertical(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// Lines are parallel to each other.
        /// </summary>
        /// <param name="otherLine">The other line.</param>
        /// <returns><c>true</c> if the specified other line is parallel; otherwise, <c>false</c>.</returns>
        public bool IsParallel(LinearCurve otherLine)
        {
            double slope = Slope();
            double otherSlope = otherLine.Slope();
            if (double.IsInfinity(slope) && double.IsInfinity(otherSlope))
            {
                return true;
            }

            double tolerance = Generics.GetTolerance(otherLine, Tolerance);
            return (Slope() - otherLine.Slope()).IsZeroSign(tolerance);
        }

        /// <summary>
        /// Lines are perpendicular to each other.
        /// </summary>
        /// <param name="otherLine">The other line.</param>
        /// <returns><c>true</c> if the specified other line is perpendicular; otherwise, <c>false</c>.</returns>
        public bool IsPerpendicular(LinearCurve otherLine)
        {
            double slope = Slope();
            double otherSlope = otherLine.Slope();
            double tolerance = Generics.GetTolerance(otherLine, Tolerance);
            if ((double.IsInfinity(slope) && otherSlope.IsZeroSign(tolerance)) ||
                (double.IsInfinity(otherSlope) && slope.IsZeroSign(tolerance)))
            {
                return true;
            }

            return (Slope() * otherLine.Slope()).IsEqualTo(-1, tolerance);
        }

        /// <summary>
        /// Provided line segment intersects an infinitely long line projecting off of the line segment.
        /// It isn't necessarily intersecting between the defining points.
        /// </summary>
        /// <param name="otherLine">The other line.</param>
        /// <returns><c>true</c> if [is intersecting curve] [the specified other line]; otherwise, <c>false</c>.</returns>
        public bool IsIntersectingCurve(LinearCurve otherLine)
        {
            return !IsParallel(otherLine);
        }
        #endregion

        #region Methods: Properties

        /// <summary>
        /// Slope of the line.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Slope()
        {
            return Slope(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// Curvature of the line.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Curvature()
        {
            return 0;
        }

        /// <summary>
        /// X-Intercept of the line.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double InterceptX()
        {
            return InterceptX(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// Y-Intercept of the line.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double InterceptY()
        {
            return InterceptY(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// Vector that is tangential to the line connecting the defining points.
        /// </summary>
        /// <returns>Vector.</returns>
        public Vector TangentVector()
        {
            return Vector.UnitTangentVector(ControlPointI, ControlPointJ);
        }

        /// <summary>
        /// Vector that is normal to the line connecting the defining points.
        /// </summary>
        /// <returns>Vector.</returns>
        public Vector NormalVector()
        {
            return Vector.UnitNormalVector(ControlPointI, ControlPointJ);
        }
        #endregion

        #region ICurvePositionPolar
        /// <summary>
        /// The radius measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public double RadiusAboutOrigin(double angleRadians)
        {
            return RadiiAboutOrigin(angleRadians)[0];
        }

        /// <summary>
        /// The radii measured from the local coordinate origin as a function of the angle in local coordinates.
        /// </summary>
        /// <param name="angleRadians">The angle in radians in local coordinates.</param>
        /// <returns>System.Double.</returns>
        public double[] RadiiAboutOrigin(double angleRadians)
        {
            if (Numbers.IsEqualTo(Trig.Sin(angleRadians) - Trig.Cos(angleRadians), 0, Tolerance) ||
                Numbers.IsNegativeSign(Trig.Sin(angleRadians) - Trig.Cos(angleRadians), Tolerance))
            {
                throw new ArgumentOutOfRangeException($"Angle {angleRadians} provided will never intersect line.");
            }
            return new double[] { System.Math.Abs(InterceptY() / (Trig.Sin(angleRadians) - Slope() * Trig.Cos(angleRadians))) };
        }
        #endregion

        #region ICurvePositionCartesian
        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double XatY(double y)
        {
            return XsAtY(y)[0];
        }

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double YatX(double x)
        {
            return YsAtX(x)[0];
        }

        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double[] XsAtY(double y)
        {
            if (IsHorizontal() && !y.IsEqualTo(InterceptY(), Tolerance))
            {
                throw new ArgumentOutOfRangeException($"Coordinate {y} does not lie on horizontal line {this}");
            }
            return new double[] { (InterceptX() + y / Slope()) };
        }

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns>System.Double.</returns>
        public double[] YsAtX(double x)
        {
            return new double[] { (InterceptY() + Slope() * x)} ;
        }

        /// <summary>
        /// Provided point lies on the curve.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns><c>true</c> if [is intersecting coordinate] [the specified coordinate]; otherwise, <c>false</c>.</returns>
        public bool IsIntersectingCoordinate(CartesianCoordinate coordinate)
        {
            double tolerance = Generics.GetTolerance(coordinate, Tolerance);
            double y = YatX(coordinate.X);
            if (IsVertical())
            {
                return ControlPointI.X.IsEqualTo(coordinate.X, tolerance);
            }
            return y.IsEqualTo(coordinate.Y, tolerance);
        }
        #endregion

        #region ICurveLimits
        /// <summary>
        /// Length of the line segment.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Length()
        {
            return Length(Range.Start.Limit, Range.End.Limit);
        }

        /// <summary>
        /// Length of the curve between two points.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public double LengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double ChordLength()
        {
            return Length();
        }

        /// <summary>
        /// The length of the chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the length measurement is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the length measurement is ended.</param>
        /// <returns>System.Double.</returns>
        public double ChordLengthBetween(double relativePositionStart, double relativePositionEnd)
        {
            return LengthBetween(relativePositionStart, relativePositionEnd);
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <returns>LinearCurve.</returns>
        public LinearCurve Chord()
        {
            return CloneCurve();
        }

        /// <summary>
        /// The chord connecting the start and end limits.
        /// </summary>
        /// <param name="relativePositionStart">Relative position along the path at which the linear curve is started.</param>
        /// <param name="relativePositionEnd">Relative position along the path at which the linear curve is ended.</param>
        /// <returns>LinearCurve.</returns>
        public LinearCurve ChordBetween(double relativePositionStart, double relativePositionEnd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        public Vector TangentVector(double relativePosition)
        {
            return TangentVector();
        }

        /// <summary>
        /// Vector that is tangential to the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the tangent vector is desired.</param>
        /// <returns>Vector.</returns>
        public Vector NormalVector(double relativePosition)
        {
            return NormalVector();
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateCartesian(double relativePosition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Coordinate of the curve at the specified position.
        /// If the shape is a closed shape, <paramref name="relativePosition" /> = {any integer} where <paramref name="relativePosition" /> = 0.
        /// </summary>
        /// <param name="relativePosition">Relative position along the path at which the coordinate is desired.</param>
        /// <returns>CartesianCoordinate.</returns>
        public PolarCoordinate CoordinatePolar(double relativePosition)
        {
            return CoordinateCartesian(relativePosition);
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return typeof(LinearCurve).Name + " - X-Intercept: " + InterceptX() + ", Y-Intercept: " + InterceptY() + ", Slope: " + Slope();
        }

        /// <summary>
        /// Returns a point where the line segment intersects the provided line segment.
        /// </summary>
        /// <param name="otherLine">Line segment that intersects the current line segment.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate IntersectionCoordinate(LinearCurve otherLine)
        {
            return LineIntersect(Slope(), InterceptX(), InterceptY(),
                                 otherLine.Slope(), otherLine.InterceptX(), otherLine.InterceptY());
        }

        /// <summary>
        /// Coordinate of where a perpendicular projection intersects the provided coordinate.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>CartesianCoordinate.</returns>
        public CartesianCoordinate CoordinateOfPerpendicularProjection(CartesianCoordinate point)
        {
            return CoordinateOfPerpendicularProjection(point, this);
        }
        #endregion

        #region Methods: Static        
        /// <summary>
        /// The length between the provided points along a linear curve.
        /// </summary>
        /// <param name="pointI">Point i.</param>
        /// <param name="pointJ">Point j.</param>
        /// <returns>System.Double.</returns>
        public static double Length(CartesianCoordinate pointI, CartesianCoordinate pointJ)
        {
            return AlgebraLibrary.SRSS((pointJ.X - pointI.X), (pointJ.Y - pointI.Y));
        }

        #region Alignment
        /// <summary>
        /// Determines if the line is horizontal.
        /// </summary>
        /// <param name="ptI">The point i.</param>
        /// <param name="ptJ">The point j.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the shape segment is horizontal, <c>false</c> otherwise.</returns>
        public static bool IsHorizontal(CartesianCoordinate ptI,
            CartesianCoordinate ptJ,
            double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Generics.GetTolerance(ptI, ptJ, tolerance);
            return ptI.Y.IsEqualTo(ptJ.Y, tolerance); ;
        }

        /// <summary>
        /// Determines whether the specified slope is vertical.
        /// </summary>
        /// <param name="slope">The slope.</param>
        /// <returns><c>true</c> if the specified slope is vertical; otherwise, <c>false</c>.</returns>
        public static bool IsHorizontal(double slope)
        {
            return (slope.IsZeroSign());
        }

        /// <summary>
        /// Determines if the segment is vertical.
        /// </summary>
        /// <param name="ptI">The point i.</param>
        /// <param name="ptJ">The point j.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the segment is vertical, <c>false</c> otherwise.</returns>
        public static bool IsVertical(CartesianCoordinate ptI,
            CartesianCoordinate ptJ,
            double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Generics.GetTolerance(ptI, ptJ, tolerance);
            return ptI.X.IsEqualTo(ptJ.X, tolerance); ;
        }

        /// <summary>
        /// Determines whether the specified slope is vertical.
        /// </summary>
        /// <param name="slope">The slope.</param>
        /// <returns><c>true</c> if the specified slope is vertical; otherwise, <c>false</c>.</returns>
        public static bool IsVertical(double slope)
        {
            return (slope.IsEqualTo(double.PositiveInfinity) || slope.IsEqualTo(double.NegativeInfinity));
        }

        /// <summary>
        /// True: Slopes are parallel.
        /// </summary>
        /// <param name="slope1">The slope1.</param>
        /// <param name="slope2">The slope2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the specified slope1 is parallel; otherwise, <c>false</c>.</returns>
        public static bool IsParallel(double slope1, double slope2, double tolerance = Numbers.ZeroTolerance)
        {
            if (double.IsNegativeInfinity(slope1) && double.IsPositiveInfinity(slope2)) { return true; }
            if (double.IsNegativeInfinity(slope2) && double.IsPositiveInfinity(slope1)) { return true; }
            return slope1.IsEqualTo(slope2, tolerance);
        }

        /// <summary>
        /// True: Slopes are perpendicular.
        /// </summary>
        /// <param name="slope1">The slope1.</param>
        /// <param name="slope2">The slope2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the specified slope1 is perpendicular; otherwise, <c>false</c>.</returns>
        public static bool IsPerpendicular(double slope1, double slope2, double tolerance = Numbers.ZeroTolerance)
        {
            if (double.IsNegativeInfinity(slope1) && slope2.IsZeroSign(tolerance)) { return true; }
            if (double.IsNegativeInfinity(slope2) && slope1.IsZeroSign(tolerance)) { return true; }
            if (double.IsPositiveInfinity(slope1) && slope2.IsZeroSign(tolerance)) { return true; }
            if (double.IsPositiveInfinity(slope2) && slope1.IsZeroSign(tolerance)) { return true; }
            return ((slope1 * slope2).IsEqualTo(-1));
        }
        #endregion

        #region Slope
        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="rise">Difference in y-coordinate values or equivalent.</param>
        /// <param name="run">Difference in x-coordinate values or equivalent.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="ArgumentException">Rise &amp; run are both zero. Cannot determine a slope for a point.</exception>
        public static double Slope(double rise, double run, double tolerance = Numbers.ZeroTolerance)
        {
            if (run.IsZeroSign(tolerance) && rise.IsPositiveSign()) { return double.PositiveInfinity; }
            if (run.IsZeroSign(tolerance) && rise.IsNegativeSign()) { return double.NegativeInfinity; }
            if (run.IsZeroSign(tolerance) && rise.IsZeroSign(tolerance)) { throw new ArgumentException("Rise & run are both zero. Cannot determine a slope for a point."); }
            return (rise / run);
        }

        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double Slope(double x1, double y1, double x2, double y2, double tolerance = Numbers.ZeroTolerance)
        {
            return Slope((y2 - y1),
                         (x2 - x1), tolerance);
        }


        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="point1">First point.</param>
        /// <param name="point2">Second point.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double Slope(CartesianCoordinate point1, CartesianCoordinate point2, double tolerance = Numbers.ZeroTolerance)
        {
            return Slope((point2.Y - point1.Y),
                         (point2.X - point1.X), tolerance);
        }

        /// <summary>
        /// Returns the slope of a line (y2-y1)/(x2-x1).
        /// </summary>
        /// <param name="delta">The difference between two points.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double Slope(CartesianOffset delta, double tolerance = Numbers.ZeroTolerance)
        {
            return Slope((delta.J.Y - delta.I.Y),
                         (delta.J.X - delta.I.X), tolerance);
        }
        #endregion

        #region Intercept
        /// <summary>
        /// Returns the x-intercept.
        /// Returns +infinity if line is horizontal.
        /// </summary>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double InterceptX(double x1, double y1, double x2, double y2, double tolerance = Numbers.ZeroTolerance)
        {
            if (y1.IsZeroSign(tolerance)) { return x1; }
            if (y2.IsZeroSign(tolerance)) { return x2; }
            if (y1.IsEqualTo(y2, tolerance)) { return double.PositiveInfinity; }
            return (-y1 / Slope(x1, y1, x2, y2, tolerance) + x1);
        }

        /// <summary>
        /// Returns the x-intercept.
        /// </summary>
        /// <param name="point1">First point defining a line.</param>
        /// <param name="point2">Second point defining a line.</param>
        /// <param name="tolerance">&gt;Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double InterceptX(CartesianCoordinate point1, CartesianCoordinate point2, double tolerance = Numbers.ZeroTolerance)
        {
            return InterceptX(point1.X, point1.Y, point2.X, point2.Y, tolerance);
        }

        /// <summary>
        /// Returns the y-intercept.
        /// Returns +infinity if line is vertical.
        /// </summary>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double InterceptY(double x1, double y1, double x2, double y2, double tolerance = Numbers.ZeroTolerance)
        {
            if (x1.IsZeroSign(tolerance)) { return y1; }
            if (x2.IsZeroSign(tolerance)) { return y2; }
            if (x1.IsEqualTo(x2, tolerance)) { return double.PositiveInfinity; }
            return (-x1 * Slope(x1, y1, x2, y2, tolerance) + y1);
        }

        /// <summary>
        /// Returns the y-intercept.
        /// </summary>
        /// <param name="point1">First point defining a line.</param>
        /// <param name="point2">Second point defining a line.</param>
        /// <param name="tolerance">&gt;Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double InterceptY(CartesianCoordinate point1, CartesianCoordinate point2, double tolerance = Numbers.ZeroTolerance)
        {
            return InterceptY(point1.X, point1.Y, point2.X, point2.Y, tolerance);
        }
        #endregion

        #region Intersect

        /// <summary>
        /// The lines intersect.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AreLinesIntersecting(double slope1, double slope2, double tolerance = Numbers.ZeroTolerance)
        {
            return (!IsParallel(slope1, slope2, tolerance));
        }

        /// <summary>
        /// The x-coordinate of the intersection of two lines.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="xIntercept1">X-intercept of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="xIntercept2">X-intercept of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double LineIntersectX(double slope1, double xIntercept1, double slope2, double xIntercept2, double tolerance = Numbers.ZeroTolerance)
        {
            if (IsParallel(slope1, slope2, tolerance))
            {
                return double.PositiveInfinity;
            }
            if (xIntercept1.IsEqualTo(double.PositiveInfinity) || 
                slope2.IsEqualTo(double.PositiveInfinity) || 
                slope2.IsEqualTo(double.NegativeInfinity))
            {
                return xIntercept2;
            }
            if (xIntercept2.IsEqualTo(double.PositiveInfinity) ||
                slope1.IsEqualTo(double.PositiveInfinity) ||
                slope1.IsEqualTo(double.NegativeInfinity))
            {
                return xIntercept1;
            }
            return (xIntercept1 + (xIntercept1 - xIntercept2) * (slope2 / (slope1 - slope2)));
        }

        /// <summary>
        /// The y-coordinate of the intersection of two lines.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="yIntercept1">Y-intercept of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="yIntercept2">Y-intercept of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double LineIntersectY(double slope1, double yIntercept1, double slope2, double yIntercept2, double tolerance = Numbers.ZeroTolerance)
        {
            if (IsParallel(slope1, slope2, tolerance))
            {
                return double.PositiveInfinity;
            }
            if (yIntercept1.IsEqualTo(double.PositiveInfinity))
            {
                return yIntercept2;
            }
            if (yIntercept2.IsEqualTo(double.PositiveInfinity))
            {
                return yIntercept1;
            }
            return (yIntercept1 + (yIntercept1 - yIntercept2) * (slope1 / (slope2 - slope1)));
        }

        /// <summary>
        /// The coordinates of the intersection of two lines.
        /// </summary>
        /// <param name="slope1">Slope of the first line.</param>
        /// <param name="xIntercept1">X-intercept of the first line.</param>
        /// <param name="yIntercept1">Y-intercept of the first line.</param>
        /// <param name="slope2">Slope of the second line.</param>
        /// <param name="xIntercept2">X-intercept of the second line.</param>
        /// <param name="yIntercept2">Y-intercept of the second line.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate LineIntersect(
            double slope1, double xIntercept1, double yIntercept1,
            double slope2, double xIntercept2, double yIntercept2, double tolerance = Numbers.ZeroTolerance)
        {
            // check for vertical lines and handle those cases here
            // in sub-functions, throw exceptions
            if (IsVertical(slope1) && !IsVertical(slope2))
            {
                LinearCurve curve2 = CurveByYIntercept(slope2, yIntercept2);
                return new CartesianCoordinate(xIntercept1, curve2.YatX(xIntercept1));
            }
            if (!IsVertical(slope1) && IsVertical(slope2))
            {
                LinearCurve curve2 = CurveByYIntercept(slope1, yIntercept1);
                return new CartesianCoordinate(xIntercept2, curve2.YatX(xIntercept2));
            }
            if (IsHorizontal(slope1) && !IsHorizontal(slope2))
            {
                LinearCurve curve2 = CurveByYIntercept(slope2, yIntercept2);
                return new CartesianCoordinate(curve2.XatY(yIntercept1), yIntercept1);
            }
            if (!IsHorizontal(slope1) && IsHorizontal(slope2))
            {
                LinearCurve curve2 = CurveByYIntercept(slope1, yIntercept1);
                return new CartesianCoordinate(curve2.XatY(yIntercept2), yIntercept2);
            }

            return new CartesianCoordinate(
                LineIntersectX(slope1, xIntercept1, slope2, xIntercept2, tolerance),
                LineIntersectY(slope1, yIntercept1, slope2, yIntercept2, tolerance));
        }

        #endregion

        #region Projection
        /// <summary>
        /// Coordinate of where a perpendicular projection intersects the provided coordinate.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="referenceLine">The line to which a perpendicular projection is drawn.</param>
        /// <returns>CartesianCoordinate.</returns>
        public static CartesianCoordinate CoordinateOfPerpendicularProjection(CartesianCoordinate point, LinearCurve referenceLine)
        {
            // 1. Get normal vector to curve
            Vector normalVector = referenceLine.NormalVector();

            // 2. Create new curve B by applying normal vector to point
            LinearCurve offsetCurve = new LinearCurve(point, point + new CartesianOffset(normalVector.Xcomponent, normalVector.Ycomponent));

            // 3. Return intersection of curve B to current segment curve
            return referenceLine.IntersectionCoordinate(offsetCurve);
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
        public LinearCurve CloneCurve()
        {
            LinearCurve curve = new LinearCurve(ControlPointI, ControlPointJ);
            curve.Tolerance = Tolerance;
            curve._range = Range.CloneRange();
            return curve;
        }
        #endregion
    }
}
