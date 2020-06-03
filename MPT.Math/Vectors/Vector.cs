// ***********************************************************************
// Assembly         : MPT.Math
// Author           : Mark Thomas
// Created          : 01-28-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 05-26-2020
// ***********************************************************************
// <copyright file="Vector.cs" company="Mark P Thomas, Inc.">
//     Copyright ©  2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.Math.Algebra;
using MPT.Math.Coordinates;
using MPT.Math.NumberTypeExtensions;
using System;
using NMath = System.Math;

namespace MPT.Math.Vectors
{
    /// <summary>
    /// Library of methods related to vectors.
    /// </summary>
    /// <seealso cref="System.IEquatable{Vector}" />
    public class Vector : IEquatable<Vector>, ITolerance
    {
        #region Properties
        /// <summary>
        /// Tolerance to use in all calculations with double types.
        /// </summary>
        /// <value>The tolerance.</value>
        public double Tolerance { get; set; } = 0.00001;

        /// <summary>
        /// Gets the x-component of this vector.
        /// </summary>
        /// <value>The xcomponent.</value>
        public double Xcomponent { get; private set; }

        /// <summary>
        /// Gets the y-component of this vector.
        /// </summary>
        /// <value>The ycomponent.</value>
        public double Ycomponent { get; private set; }

        /// <summary>
        /// The location
        /// </summary>
        private CartesianCoordinate _location;
        /// <summary>
        /// Gets the location of this vector in Euclidean space.
        /// </summary>
        /// <value>The location.</value>
        public CartesianCoordinate Location => _location;

        /// <summary>
        /// Length of this vector.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Magnitude()
        {
            //=> (Xcomponent.IsZeroSign() && Ycomponent.IsZeroSign()) ? 0 : getMagnitude(Xcomponent, Ycomponent);
            return getMagnitude(Xcomponent, Ycomponent);
        }

        /// <summary>
        /// Gets the square of the length of this vector.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double MagnitudeSquared => Xcomponent.Squared() + Ycomponent.Squared();
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the class with a vector structure.
        /// </summary>
        /// <param name="xMagnitude">The x magnitude.</param>
        /// <param name="yMagnitude">The y magnitude.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector(
            double xMagnitude, 
            double yMagnitude,
            double tolerance = Numbers.ZeroTolerance)
        {
            _location = new CartesianCoordinate();
            Xcomponent = xMagnitude;
            Ycomponent = yMagnitude;
            
            if (tolerance != Numbers.ZeroTolerance)
            {
                Tolerance = tolerance;
            }
        }

        /// <summary>
        /// Initializes the class with a vector structure and a point coinciding with the location of the vector.
        /// </summary>
        /// <param name="xMagnitude">The x magnitude.</param>
        /// <param name="yMagnitude">The y magnitude.</param>
        /// <param name="location">The location.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector(
            double xMagnitude, 
            double yMagnitude,
            CartesianCoordinate location,
            double tolerance = Numbers.ZeroTolerance)
        {
            Xcomponent = xMagnitude;
            Ycomponent = yMagnitude;
            _location = location;

            if (tolerance != Numbers.ZeroTolerance)
            {
                Tolerance = tolerance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector" /> struct.
        /// </summary>
        /// <param name="pointI">The starting point, i.</param>
        /// <param name="pointJ">The ending point, j.</param>
        /// <param name="tolerance">The tolerance.</param>
        public Vector(
            CartesianCoordinate pointI,
            CartesianCoordinate pointJ,
            double tolerance = Numbers.ZeroTolerance)
        {
            Xcomponent = getXComponent(pointI, pointJ);
            Ycomponent = getYComponent(pointI, pointJ);
            _location = pointI;

            if (tolerance != Numbers.ZeroTolerance)
            {
                Tolerance = tolerance;
            }
        }
        #endregion

        #region Inherit Methods
        // https://msdn.microsoft.com/en-us/library/system.windows.vector(v=vs.110).aspx


        #endregion

        #region Methods      
        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the same direction.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if [is collinear same direction] [the specified vector]; otherwise, <c>false</c>.</returns>
        public bool IsCollinearSameDirection(Vector vector)
        {
            return IsCollinearSameDirection(this, vector);
        }

        /// <summary>
        /// Vectors form a concave angle.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if the specified vector is concave; otherwise, <c>false</c>.</returns>
        public bool IsConcave(Vector vector)
        {
            return IsConcave(this, vector);
        }

        /// <summary>
        /// Vectors form a 90 degree angle.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if the specified vector is orthogonal; otherwise, <c>false</c>.</returns>
        public bool IsOrthogonal(Vector vector)
        {
            return IsOrthogonal(this, vector);
        }

        /// <summary>
        /// Vectors form a convex angle.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if the specified vector is convex; otherwise, <c>false</c>.</returns>
        public bool IsConvex(Vector vector)
        {
            return IsConvex(this, vector);
        }

        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the opposite direction.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if [is collinear opposite direction] [the specified vector]; otherwise, <c>false</c>.</returns>
        public bool IsCollinearOppositeDirection(Vector vector)
        {
            return IsCollinearOppositeDirection(this, vector);
        }


        /// <summary>
        /// True: The concave side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool IsConcaveInside(Vector vector)
        {
            return IsConcaveInside(this, vector);
        }

        /// <summary>
        /// True: The convex side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool IsConvexInside(Vector vector)
        {
            return IsConvexInside(this, vector);
        }

        /// <summary>
        /// Returns a value indicating the concavity of the vectors.
        /// 1 = Pointing the same way.
        /// &gt; 0 = Concave.
        /// 0 = Orthogonal.
        /// &lt; 0 = Convex.
        /// -1 = Pointing the exact opposite way.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double ConcavityCollinearity(Vector vector)
        {
            return ConcavityCollinearity(this, vector);
        }

        /// <summary>
        /// Dot product of two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double DotProduct(Vector vector)
        {
            return VectorLibrary.DotProduct(Xcomponent, Ycomponent, vector.Xcomponent, vector.Ycomponent);
        }

        /// <summary>
        /// Cross-product of two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double CrossProduct(Vector vector)
        {
            return VectorLibrary.CrossProduct(Xcomponent, Ycomponent, vector.Xcomponent, vector.Ycomponent);
        }



        /// <summary>
        /// Returns the angle [radians] of a vector from the origin axis (x-axis, positive for counter-clockwise).
        /// </summary>
        /// <returns>System.Double.</returns>
        public double Angle()
        {
            return Coordinates.Angle.AsRadians(Xcomponent, Ycomponent);
        }

        /// <summary>
        /// Returns the angle [radians] between the two vectors, which is a value between 0 and +π.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double Angle(Vector vector)
        {
            return Angle(this, vector);
        }

        /// <summary>
        /// Returns the area between two vectors.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>System.Double.</returns>
        public double Area(Vector vector)
        {
            return (0.5 * (CrossProduct(vector)));
        }
        #endregion

        #region Methods: Static        
        /// <summary>
        /// Returns a normalized vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>Vector.</returns>
        /// <exception cref="Exception">Ill-formed vector. Vector magnitude cannot be zero.</exception>
        public static Vector UnitVector(CartesianCoordinate i, CartesianCoordinate j, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(i, j, tolerance);
            double xComponent = getXComponent(i, j);
            double yComponent = getYComponent(i, j);

            return getUnitVector(xComponent, yComponent, tolerance);
        }


        /// <summary>
        /// Returns the tangent vector to the supplied points.
        /// </summary>
        /// <param name="i">First point.</param>
        /// <param name="j">Second point.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>Vector.</returns>
        public static Vector UnitTangentVector(CartesianCoordinate i, CartesianCoordinate j, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(i, j, tolerance);
            return UnitVector(i, j, tolerance);
        }

        /// <summary>
        /// Returns a normal vector to a line connecting two points.
        /// </summary>
        /// <param name="i">First point.</param>
        /// <param name="j">Second point.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>Vector.</returns>
        public static Vector UnitNormalVector(CartesianCoordinate i, CartesianCoordinate j, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(i, j, tolerance);
            double xComponent = getXComponent(i, j);
            double yComponent = getYComponent(i, j);
            double magnitude = getMagnitude(xComponent, yComponent, tolerance);

            return new Vector(-yComponent / magnitude, xComponent / magnitude, tolerance);
        }


        /// <summary>
        /// Returns the angle [radians] between the two vectors, which is a value between 0 and +π.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns>System.Double.</returns>
        public static double Angle(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            return NMath.Acos(ConcavityCollinearity(vector1, vector2));
        }


        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the same direction.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if [is collinear same direction] [the specified vector1]; otherwise, <c>false</c>.</returns>
        public static bool IsCollinearSameDirection(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            return ConcavityCollinearity(vector1, vector2).IsEqualTo(1, Helper.GetTolerance(vector1, vector2, tolerance));
        }

        /// <summary>
        /// Vectors form a concave angle.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the specified vector1 is concave; otherwise, <c>false</c>.</returns>
        public static bool IsConcave(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            double concavityCollinearity = ConcavityCollinearity(vector1, vector2);
            return concavityCollinearity.IsPositiveSign(tolerance) && !concavityCollinearity.IsEqualTo(1, tolerance);
        }

        /// <summary>
        /// Vectors form a 90 degree angle.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the specified vector1 is orthogonal; otherwise, <c>false</c>.</returns>
        public static bool IsOrthogonal(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            return ConcavityCollinearity(vector1, vector2).IsZeroSign(tolerance);
        }

        /// <summary>
        /// Vectors form a convex angle.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if the specified vector1 is convex; otherwise, <c>false</c>.</returns>
        public static bool IsConvex(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            double concavityCollinearity = ConcavityCollinearity(vector1, vector2);
            return concavityCollinearity.IsNegativeSign(tolerance) && !concavityCollinearity.IsEqualTo(-1, tolerance);
        }

        /// <summary>
        /// True: Segments are parallel, on the same line, oriented in the opposite direction.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if [is collinear opposite direction] [the specified vector1]; otherwise, <c>false</c>.</returns>
        public static bool IsCollinearOppositeDirection(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            return ConcavityCollinearity(vector1, vector2).IsEqualTo(-1, tolerance);
        }



        /// <summary>
        /// True: The concave side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if [is concave inside] [the specified vector1]; otherwise, <c>false</c>.</returns>
        public static bool IsConcaveInside(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            return vector1.Area(vector2).IsPositiveSign(tolerance);
        }

        /// <summary>
        /// True: The convex side of the vector is inside the shape.
        /// This is determined by the direction of the vector.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <param name="tolerance">Tolerance by which a double is considered to be zero or equal.</param>
        /// <returns><c>true</c> if [is convex inside] [the specified vector1]; otherwise, <c>false</c>.</returns>
        public static bool IsConvexInside(Vector vector1, Vector vector2, double tolerance = Numbers.ZeroTolerance)
        {
            tolerance = Helper.GetTolerance(vector1, vector2, tolerance);
            return vector1.Area(vector2).IsNegativeSign(tolerance);
        }

        /// <summary>
        /// Returns a value indicating the concavity of the vectors.
        /// 1 = Pointing the same way.
        /// &gt; 0 = Concave.
        /// 0 = Orthogonal.
        /// &lt; 0 = Convex.
        /// -1 = Pointing the exact opposite way.
        /// </summary>
        /// <param name="vector1">The vector1.</param>
        /// <param name="vector2">The vector2.</param>
        /// <returns>System.Double.</returns>
        public static double ConcavityCollinearity(Vector vector1, Vector vector2)
        {
            double magnitude1 = vector1.Magnitude();
            double magnitude2 = vector2.Magnitude();
            return vector1.DotProduct(vector2) / (magnitude1 * magnitude2);
        }
        #endregion

        #region Private
        /// <summary>
        /// Gets the x component.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>System.Double.</returns>
        private static double getXComponent(CartesianCoordinate i, CartesianCoordinate j)
        {
            return j.X - i.X;
        }
        /// <summary>
        /// Gets the y component.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>System.Double.</returns>
        private static double getYComponent(CartesianCoordinate i, CartesianCoordinate j)
        {
            return j.Y - i.Y;
        }

        /// <summary>
        /// Gets the magnitude.
        /// </summary>
        /// <param name="xComponent">The x component.</param>
        /// <param name="yComponent">The y component.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="Exception">Ill-formed vector. Vector magnitude cannot be zero.</exception>
        private static double getMagnitude(double xComponent, double yComponent, double tolerance = Numbers.ZeroTolerance)
        {
            double magnitude = AlgebraLibrary.SRSS(xComponent, yComponent);
            if (magnitude.IsZeroSign(tolerance)) { throw new Exception("Ill-formed vector. Vector magnitude cannot be zero."); }
            return magnitude;
        }

        /// <summary>
        /// Returns a normalized vector.
        /// </summary>
        /// <param name="xComponent">The x component.</param>
        /// <param name="yComponent">The y component.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>Vector.</returns>
        /// <exception cref="Exception">Ill-formed vector. Vector magnitude cannot be zero.</exception>
        private static Vector getUnitVector(double xComponent, double yComponent, double tolerance = Numbers.ZeroTolerance)
        {
            double magnitude = getMagnitude(xComponent, yComponent);

            xComponent /= magnitude;
            yComponent /= magnitude;

            return new Vector(xComponent, yComponent, tolerance);
        }
        #endregion

        #region Operators & Equals


        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(Vector other)
        {
            return (NMath.Abs(Xcomponent - other.Xcomponent) < Tolerance) &&
                   (NMath.Abs(Ycomponent - other.Ycomponent) < Tolerance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector) { return Equals((Vector)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Xcomponent.GetHashCode() ^ Ycomponent.GetHashCode();
        }


        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Vector a, Vector b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Vector a, Vector b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.Xcomponent + b.Xcomponent, a.Ycomponent + b.Ycomponent);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.Xcomponent - b.Xcomponent, a.Ycomponent - b.Ycomponent);
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator *(Vector a, double b)
        {
            return new Vector(a.Xcomponent * b, a.Ycomponent * b);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator *(Vector a, Vector b)
        {
            return a.DotProduct(b);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static Vector operator /(Vector a, double b)
        {
            return new Vector(a.Xcomponent / b, a.Ycomponent / b);
        }

        
        #endregion
    }
}
