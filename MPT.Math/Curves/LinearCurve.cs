using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using MPT.Math.Vectors;
using System;

namespace MPT.Math.Curves
{
    /// <summary>
    /// Class LinearCurve.
    /// Implements the <see cref="MPT.Math.Curves.ICurve" />
    /// </summary>
    /// <seealso cref="MPT.Math.Curves.ICurve" />
    public class LinearCurve : ICurve
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        public double Tolerance { get; set; } = 10E-6;

        /// <summary>
        /// Gets the control point i.
        /// </summary>
        /// <value>The control point i.</value>
        public CartesianCoordinate ControlPointI { get; set; }

        /// <summary>
        /// Gets the control point j.
        /// </summary>
        /// <value>The control point j.</value>
        public CartesianCoordinate ControlPointJ { get; set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the line segment to span between the provided points.
        /// </summary>
        /// <param name="i">First point of the line.</param>
        /// <param name="j">Second point of the line.</param>
        public LinearCurve(CartesianCoordinate i, CartesianCoordinate j) 
        {
            ControlPointI = i;
            ControlPointJ = j;
            LimitMin = i;
            LimitMax = j;
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
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsParallel(LinearCurve otherLine)
        {
            double slope = Slope();
            double otherSlope = otherLine.Slope();
            if (double.IsInfinity(slope) && double.IsInfinity(otherSlope))
            {
                return true;
            }

            double tolerance = Helper.GetTolerance(otherLine, Tolerance);
            return (Slope() - otherLine.Slope()).IsZeroSign(tolerance);
        }

        /// <summary>
        /// Lines are perpendicular to each other.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsPerpendicular(LinearCurve otherLine)
        {
            double slope = Slope();
            double otherSlope = otherLine.Slope();
            double tolerance = Helper.GetTolerance(otherLine, Tolerance);
            if ((double.IsInfinity(slope) && otherSlope.IsZeroSign(tolerance)) ||
                (double.IsInfinity(otherSlope) && slope.IsZeroSign(tolerance)))
            {
                return true;
            }

            return (Slope() * otherLine.Slope()).IsEqualTo(-1, tolerance);
        }

        /// <summary>
        /// Provided point lies on an infinitely long line projecting off of the line segment. 
        /// It isn't necessarily intersecting between the defining points.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool IsIntersectingCoordinate(CartesianCoordinate coordinate)
        {
            double tolerance = Helper.GetTolerance(coordinate, Tolerance);
            double y = Y(coordinate.X);
            if (IsVertical())
            {
                return ControlPointI.X.IsEqualTo(coordinate.X, tolerance);
            }
            return y.IsEqualTo(coordinate.Y, tolerance);
        }

        /// <summary>
        /// Provided line segment intersects an infinitely long line projecting off of the line segment. 
        /// It isn't necessarily intersecting between the defining points.
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool IsIntersectingCurve(LinearCurve otherLine)
        {
            return !IsParallel(otherLine);
        }
        #endregion

        #region Methods: Properties
        /// <summary>
        /// Slope of the line segment.
        /// </summary>
        /// <returns></returns>
        public double Slope()
        {
            return Slope(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// X-Intercept of the line segment.
        /// </summary>
        /// <returns></returns>
        public double InterceptX()
        {
            return InterceptX(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// Y-Intercept of the line segment.
        /// </summary>
        /// <returns></returns>
        public double InterceptY()
        {
            return InterceptY(ControlPointI, ControlPointJ, Tolerance);
        }

        /// <summary>
        /// Vector that is tangential to the line connecting the defining points.
        /// </summary>
        /// <returns></returns>
        public Vector TangentVector()
        {
            return Vector.UnitTangentVector(ControlPointI, ControlPointJ);
        }

        /// <summary>
        /// Vector that is normal to the line connecting the defining points.
        /// </summary>
        /// <returns></returns>
        public Vector NormalVector()
        {
            return Vector.UnitNormalVector(ControlPointI, ControlPointJ);
        }

        /// <summary>
        /// X-coordinate on the line segment that corresponds to the y-coordinate given.
        /// </summary>
        /// <param name="y">Y-coordinate for which an x-coordinate is desired.</param>
        /// <returns></returns>
        public double X(double y)
        {
            return (InterceptX() + y / Slope());
        }

        /// <summary>
        /// Y-coordinate on the line segment that corresponds to the x-coordinate given.
        /// </summary>
        /// <param name="x">X-coordinate for which a y-coordinate is desired.</param>
        /// <returns></returns>
        public double Y(double x)
        {
            return (InterceptY() + Slope() * x);
        }
        #endregion

        #region Methods: Properties Derived with Limits
        /// <summary>
        /// First coordinate control value.
        /// </summary>
        public CartesianCoordinate LimitMin { get; }

        /// <summary>
        /// Second coordinate control value.
        /// </summary>
        public CartesianCoordinate LimitMax { get; }

        /// <summary>
        /// Length of the line segment.
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return AlgebraLibrary.SRSS((LimitMax.X - LimitMin.X), (LimitMax.Y - LimitMin.Y));
        }
        #endregion

        #region Methods: Public
        /// <summary>
        /// Returns a point where the line segment intersects the provided line segment.
        /// </summary>
        /// <param name="otherLine">Line segment that intersects the current line segment.</param>
        /// <returns></returns>
        public CartesianCoordinate IntersectionCoordinate(LinearCurve otherLine)
        {
            return LineIntersect(Slope(), InterceptX(), InterceptY(),
                                 otherLine.Slope(), otherLine.InterceptX(), otherLine.InterceptY());
        }
        #endregion

        #region Methods: Static

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
            tolerance = Helper.GetTolerance(ptI, ptJ, tolerance);
            return ptI.Y.IsEqualTo(ptJ.Y, tolerance); ;
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
            tolerance = Helper.GetTolerance(ptI, ptJ, tolerance);
            return ptI.X.IsEqualTo(ptJ.X, tolerance); ;
        }

        /// <summary>
        /// True: Slopes are parallel.
        /// </summary>
        /// <param name="slope1"></param>
        /// <param name="slope2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static bool IsParallel(double slope1, double slope2, double tolerance = Numbers.ZeroTolerance)
        {
            if (double.IsNegativeInfinity(slope1) && double.IsPositiveInfinity(slope2)) { return true; }
            if (double.IsNegativeInfinity(slope2) && double.IsPositiveInfinity(slope1)) { return true; }
            return slope1.IsEqualTo(slope2, tolerance);
        }

        /// <summary>
        /// True: Slopes are perpendicular.
        /// </summary>
        /// <param name="slope1"></param>
        /// <param name="slope2"></param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
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
        /// <param name="tolerance">>Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
        public static double InterceptX(CartesianCoordinate point1, CartesianCoordinate point2, double tolerance = Numbers.ZeroTolerance)
        {
            return InterceptX(point1.X, point1.Y, point2.X, point2.Y, tolerance);
        }

        /// <summary>
        /// Returns the y-intercept.
        /// Returns +infinity if line is vertical.
        /// </summary>
        /// <param name="y1">First y-coordinate.</param>
        /// <param name="y2">Second y-coordinate.</param>
        /// <param name="x1">First x-coordinate.</param>
        /// <param name="x2">Second x-coordinate.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns></returns>
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
        /// <param name="tolerance">>Tolerance by which a double is considered to be zero or equal.</param>
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
        /// <returns></returns>
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
        /// <returns></returns>
        public static double LineIntersectX(double slope1, double xIntercept1, double slope2, double xIntercept2, double tolerance = Numbers.ZeroTolerance)
        {
            if (IsParallel(slope1, slope2, tolerance))
            {
                return double.PositiveInfinity;
            }
            if (xIntercept1.IsEqualTo(double.PositiveInfinity))
            {
                return xIntercept2;
            }
            if (xIntercept2.IsEqualTo(double.PositiveInfinity))
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
        /// <returns></returns>
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
        /// <returns></returns>
        public static CartesianCoordinate LineIntersect(
            double slope1, double xIntercept1, double yIntercept1,
            double slope2, double xIntercept2, double yIntercept2, double tolerance = Numbers.ZeroTolerance)
        {
            return new CartesianCoordinate(
                LineIntersectX(slope1, xIntercept1, slope2, xIntercept2, tolerance),
                LineIntersectY(slope1, yIntercept1, slope2, yIntercept2, tolerance));
        }

        #endregion

        #endregion
    }
}
